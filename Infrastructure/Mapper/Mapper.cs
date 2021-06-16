using System;
using System.Collections.Generic;
using Domain.Dtos;
using Application.Mapper;
using Domain.Entities;

namespace Infrastructure.Mappers
{
    public class Mapper : IMapper
    {
        public void MapUserToRegisterToAppUserEntity(UserToRegisterRequest source, out AppUser destination)
        {
            destination = new AppUser()
            {
                City = source.City.ToLower(),
                Email = source.Email.ToLower(),
                UserName = source.UserName.ToLower(),
                FirstName = source.FirstName.ToLower(),
                LastName = source.LastName.ToLower(),
                BirthDate = source.BirthDate
            };
        }

        public void MapAppUserEntityToUserDetailResponse(AppUser source, out UserDetailResponse destination)
        {
            destination = new UserDetailResponse
            {
                Id = source.Id,
                Email = source.Email,
                UserName = source.UserName,
                Description = source.Description,
                City = source.City,
                FirstName = source.FirstName,
                LastName = source.LastName,
                BirthDate = source.BirthDate,
                JoinDate = source.JoinDate,
                MainPhotoUrl = source.MainPhotoUrl
            };
        }

        public void MapAppUserEntityToUserDetailResponse(IEnumerable<AppUser> sources, out IEnumerable<UserDetailResponse> destinations)
        {
            var list = new List<UserDetailResponse>();

            destinations = list;

            if (sources is null)
            {
                return;
            }

            foreach (var source in sources)
            {
                list.Add(new UserDetailResponse
                {
                    Id = source.Id,
                    Email = source.Email,
                    UserName = source.UserName,
                    Description = source.Description,
                    City = source.City,
                    FirstName = source.FirstName,
                    LastName = source.LastName,
                    BirthDate = source.BirthDate,
                    JoinDate = source.JoinDate,
                    MainPhotoUrl = source.MainPhotoUrl
                });
            }
        }

        public void MapFollowEntityToFollowerDetailResponse(IEnumerable<Follow> sources, out IEnumerable<UserDetailResponse> destinations)
        {
            var list = new List<UserDetailResponse>();

            destinations = list;

            if (sources is null)
            {
                return;
            }

            foreach (var source in sources)
            {
                list.Add(new UserDetailResponse
                {
                    Id = source.Follower.Id,
                    Email = source.Follower.Email,
                    UserName = source.Follower.UserName,
                    Description = source.Follower.Description,
                    City = source.Follower.City,
                    FirstName = source.Follower.FirstName,
                    LastName = source.Follower.LastName,
                    BirthDate = source.Follower.BirthDate,
                    JoinDate = source.Follower.JoinDate,
                    MainPhotoUrl = source.Follower.MainPhotoUrl
                });
            }
        }

        public void MapFollowEntityToFolloweeDetailResponse(IEnumerable<Follow> sources, out IEnumerable<UserDetailResponse> destinations)
        {
            var list = new List<UserDetailResponse>();

            destinations = list;

            if (sources is null)
            {
                return;
            }

            foreach (var source in sources)
            {
                list.Add(new UserDetailResponse
                {
                    Id = source.Followee.Id,
                    Email = source.Followee.Email,
                    UserName = source.Followee.UserName,
                    Description = source.Followee.Description,
                    City = source.Followee.City,
                    FirstName = source.Followee.FirstName,
                    LastName = source.Followee.LastName,
                    BirthDate = source.Followee.BirthDate,
                    JoinDate = source.Followee.JoinDate,
                    MainPhotoUrl = source.Followee.MainPhotoUrl
                });
            }
        }

        public void MapPhotoEntityToPhotoToReturnResponse(IEnumerable<Photo> sources, out IEnumerable<PhotoToReturnResponse> destinations)
        {
            var list = new List<PhotoToReturnResponse>();

            destinations = list;

            if (sources is null)
            {
                return;
            }

            foreach (var source in sources)
            {
                list.Add(new PhotoToReturnResponse
                {
                    Id = source.Id,
                    Url = source.Url,
                    Description = source.Description,
                    DateAdded = source.DateAdded,
                    UserId = source.UserId,
                    UserUserName = source.User.UserName,
                    UserFirstName = source.User.FirstName,
                    UserLastName = source.User.LastName,
                    UserPhotoUrl = source.User.MainPhotoUrl
                });
            }
        }

        public void MapUserToUpdateRequestToAppUserEntity(UserToUpdateRequest source, ref AppUser destination)
        {
            destination.Email = source.Email;
            destination.UserName = source.UserName;
            destination.Description = source.Description;
            destination.City = source.City;
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.BirthDate = source.BirthDate;
        }

        public void MapPhotoLikeRequestToPhotoLikeEntity(PhotoLikeRequest source, out PhotoLike destination)
        {
            destination = new PhotoLike()
            {
                LikerId = source.UserId,
                PhotoId = source.PhotoId
            };
        }

        public void MapPhotoLikeEntityToLikerResponse(IEnumerable<PhotoLike> source, out IEnumerable<LikerResponse> destination)
        {
            var list = new List<LikerResponse>();

            destination = list;

            if (source is null)
            {
                return;
            }

            foreach (var like in source)
            {
                var user = like.Liker;

                list.Add(new LikerResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    MainPhotoUrl = user.MainPhotoUrl,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }
        }
        
        public void MapCommentToAddRequestToCommentEntity(CommentToAddRequest source, out Comment destination)
        {
            destination = new Comment()
            {
                PhotoId = source.PhotoId,
                Content = source.Content
            };
        }

        public void MapCommentEntityToCommentResponse(IEnumerable<Comment> source, out IEnumerable<CommentResponse> destination)
        {
            var list = new List<CommentResponse>();

            destination = list;

            if(source is null)
            {
                 return;
            }

            foreach(var comment in source)
            {
                var user = comment.User;

                list.Add(new CommentResponse
                {
                    Content = comment.Content,
                    DateAdded = comment.DateAdded,
                    Id = comment.Id,
                    UserFirstName = user.FirstName,
                    UserLastName = user.LastName,
                    UserMainPhotoUrl = user.MainPhotoUrl,
                    UserUserName = user.UserName
                });
            }
        }

        public void MapMessageRequestToMessageEntity(MessageRequest source, ref Message destination)
        {
            destination = new Message
            {
                SenderId = source.SenderId,
                ReceiverId = source.ReceiverId,
                Content = source.Content
            };
        }

        public void MapMessageEntityToMessageResponse(IEnumerable<Message> source, out IEnumerable<MessageResponse> destination)
        {
            var list = new List<MessageResponse>();

            destination = list;

            if(source is null)
            {
                 return;
            }

            foreach(var message in source)
            {
                var sender = message.Sender;
                var receiver = message.Receiver;

                list.Add(new MessageResponse
                {
                    Id = message.Id,
                    SenderId = message.SenderId,
                    ReceiverId = message.ReceiverId,
                    Content = message.Content,
                    DateSent = message.DateSent,
                    DateRead = message.DateRead,
                    SenderFirstName = sender.FirstName,
                    SenderLastName = sender.LastName,
                    SenderPhotoUrl = sender.MainPhotoUrl,
                    SenderUserName = sender.UserName,
                    ReceiverFirstName = receiver.FirstName,
                    ReceiverLastName = receiver.LastName,
                    ReceiverPhotoUrl = receiver.MainPhotoUrl,
                    ReceiverUserName = receiver.UserName
                });
            }
        }

        public void MapMessageEntityToMessageResponse(Message source, out MessageResponse destination)
        {
            var sender = source.Sender;
            var receiver = source.Receiver;

            destination = new MessageResponse
            {
                Id = source.Id,
                SenderId = source.SenderId,
                ReceiverId = source.ReceiverId,
                Content = source.Content,
                DateSent = source.DateSent,
                DateRead = source.DateRead,
                SenderFirstName = sender.FirstName,
                SenderLastName = sender.LastName,
                SenderPhotoUrl = sender.MainPhotoUrl,
                SenderUserName = sender.UserName,
                ReceiverFirstName = receiver.FirstName,
                ReceiverLastName = receiver.LastName,
                ReceiverPhotoUrl = receiver.MainPhotoUrl,
                ReceiverUserName = receiver.UserName
            };
        }
    }
}