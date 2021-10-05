using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserLike> Likes {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserLike>()  // combine sourceUsrId and likeUsrId
                .HasKey(k => new {k.SourceUserId, k.LikedUserId});  // form a primary key for UserLike table

            builder.Entity<UserLike>()
                .HasOne(s => s.SourceUser)  
                .WithMany(l => l.LikedUsers)  // sourceUsr can like many usrs
                .HasForeignKey(s => s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);  // delete a user will delete related entities
                // Important!! Sql server need to set DeleteBehaviour to DeleteBehaviour.NoAction
                // otherwise u will get migration errors

            builder.Entity<UserLike>()
                .HasOne(s => s.LikedUser)
                .WithMany(l => l.LikedByUsers)  // in AppUser collection
                .HasForeignKey(s => s.LikedUserId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}