using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper
{
    public class MessageParams : PaginationParams
    {
        public string Username { get; set; }  // curr user
        public string Container { get; set; } = "Unread";  // return defu unread msg to user
    }
}