
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess
{
    
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            BuildFollowEntity(builder);
            BuildPhotoEntity(builder);
            BuildPhotoLikeEntity(builder);
            BuildCommentEntity(builder);
            BuildMessageEntity(builder);

        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PhotoLike> PhotoLikes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Conversation> Conversations { get; set; }

        private static void BuildMessageEntity(ModelBuilder builder)
        {
            builder.Entity<Message>()
                .HasOne(x => x.Sender)
                .WithMany(x => x.MessagesSent)
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Message>()
                .HasOne(x => x.Receiver)
                .WithMany(x => x.MessagesReceived)
                .HasForeignKey(x => x.ReceiverId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void BuildCommentEntity(ModelBuilder builder)
        {
            builder.Entity<Comment>()
                .HasOne(x => x.Photo)
                .WithMany(x => x.CommentsReceived)
                .HasForeignKey(x => x.PhotoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasOne(x => x.User)
                .WithMany(x => x.CommentsSent)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void BuildPhotoLikeEntity(ModelBuilder builder)
        {
            builder.Entity<PhotoLike>()
                .HasKey(x => new { x.PhotoId, x.LikerId });

            builder.Entity<PhotoLike>()
                .HasOne(x => x.Photo)
                .WithMany(x => x.LikesReceived)
                .HasForeignKey(x => x.PhotoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PhotoLike>()
                .HasOne(x => x.Liker)
                .WithMany(x => x.LikesSent)
                .HasForeignKey(x => x.LikerId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void BuildPhotoEntity(ModelBuilder builder)
        {
            builder.Entity<Photo>()
                .HasKey(x => x.Id);

            builder.Entity<Photo>()
                .HasOne(x => x.User)
                .WithMany(x => x.Photos)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void BuildFollowEntity(ModelBuilder builder)
        {
            builder.Entity<Follow>()
                .HasKey(x => new { x.FollowerId, x.FolloweeId });

            builder.Entity<Follow>()
                .HasOne(f => f.Follower)
                .WithMany(x => x.Followees)
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Follow>()
                .HasOne(f => f.Followee)
                .WithMany(x => x.Followers)
                .HasForeignKey(f => f.FolloweeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}