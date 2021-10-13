using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helper;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MessageRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AddGroup(Group group)
        {
            _context.Groups.Add(group);
        }

        public void AddMessage(Message messgae)
        {
            _context.Messages.Add(messgae);
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Connection> GetConnection(string connectionId)
        {
            return await _context.Connections.FindAsync(connectionId); 
        }

        public async Task<Group> GetGroupForConnection(string connectionId)
        {
            // throw new NotImplementedException();
            return await _context.Groups
                .Include(c => c.Connections)
                .Where(c => c.Connections.Any(x => x.ConnectionId == connectionId))
                .FirstOrDefaultAsync();
        }

        public async Task<Message> GetMessage(int id)
        {
            // return await _context.Messages.FindAsync(id);
            return await _context.Messages
              .Include(x => x.Sender)
              .Include(x => x.Recipient)
              .SingleOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Group> GetMessageGroup(string groupName)
        {
            return await _context.Groups
                    .Include(x => x.Connections)
                    .FirstOrDefaultAsync(x => x.Name == groupName);
        }

        public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {   // get all message by sent time desc
            var query = _context.Messages
                .OrderByDescending(m => m.MessageSent)
                .AsQueryable();

            // container-switch: which message in container determine which msg we return 
            query = messageParams.Container switch
            {   // inbox is if we are the recipient of msg and we have read it
                "Inbox" => query.Where(u => u.Recipient.UserName == messageParams.Username && u.RecipientDeleted == false),  // all msg reciver = para.usr
                "Outbox" => query.Where(u => u.Sender.UserName == messageParams.Username && u.SenderDeleted == false),  // all msg sender = para.usr
                _ => query.Where(u => u.Recipient.UserName ==
                    messageParams.Username && u.RecipientDeleted == false && u.DateRead == null)  // all msg reciver = para.user && msg_unread
            };
            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);  // query result tran to Dto
            return await PagedList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUserName)
        {
            // get msg not read and we mark them to read in here
            // get msg in memory,then do sth for these msg, then tran to dto
            var messages = await _context.Messages
                .Include(u => u.Sender).ThenInclude(p=>p.Photos)
                .Include(u => u.Recipient).ThenInclude(p=>p.Photos)
                // when find msg bewteen curr-usr and sec-usr, pick out
                .Where(m => m.Recipient.UserName == currentUsername && m.RecipientDeleted == false // |and& mix calcu & first then |
                        && m.Sender.UserName == recipientUserName
                        || 
                        m.Recipient.UserName == recipientUserName
                        && m.Sender.UserName == currentUsername && m.SenderDeleted == false 
                )
                .OrderBy(m => m.MessageSent)
                .ToListAsync();
            
            var unreadMessages = messages.Where(m => m.DateRead == null
                && m.Recipient.UserName == currentUsername).ToList(); 
            
            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.DateRead = DateTime.Now;
                }

                await _context.SaveChangesAsync();
            }
            
            return _mapper.Map<IEnumerable<MessageDto>>(messages);  // return msg-dtos
        }

        public void RemoveConnection(Connection connection)
        {
            _context.Connections.Remove(connection);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}