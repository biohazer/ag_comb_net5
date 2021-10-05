using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helper;

namespace API.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);  // get this by PK
        Task<AppUser> GetUserWithLikes(int userId);
        // Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId);  
        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
        // return a spec user's a list of users been like or liked by
    }
}