using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helper;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class LikesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public LikesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var sourceUserId = User.GetUserId(); // get current login
            var likedUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);  // get target user instance
            var sourceUser = await _unitOfWork.LikesRepository.GetUserWithLikes(sourceUserId);  // get current user instannce through mid-Like table

            if (likedUser == null) return NotFound();  // no target usr
            if (sourceUser.UserName == username) return BadRequest("You cannot like yourself !!");  // curr name = tar name

            var userLike = await _unitOfWork.LikesRepository.GetUserLike(sourceUserId, likedUser.Id);  // use 2 PK to get userlike instance

            if (userLike != null) return BadRequest("You already liked this user");

            userLike = new UserLike  // base on 2 PK new a UserLike instance
            {
                SourceUserId = sourceUserId,
                LikedUserId = likedUser.Id
            };

            sourceUser.LikedUsers.Add(userLike);  // curr user add UserLike

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to like usr !!");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes([FromQuery] LikesParams likesParams)
        {
            likesParams.UserId = User.GetUserId();
            var users = await _unitOfWork.LikesRepository.GetUserLikes(likesParams);

            Response.AddPaginationHeader(users.CurrentPage,
                users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(users);
        }
    }
}