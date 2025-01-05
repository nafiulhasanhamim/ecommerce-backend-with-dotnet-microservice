using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NotificationAPI.data;
using NotificationAPI.DTOs;
using NotificationAPI.Interfaces;
using NotificationAPI.Models;
using NotificationAPI.SignalRHub;

namespace NotificationAPI.Services
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IHubContext<ChatHub> _chatHubContext;


        public NotificationService(AppDbContext appDbContext, IMapper mapper,
        IHubContext<NotificationHub> hubContext, IUserService userService, IHubContext<ChatHub> chatHubContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
            _hubContext = hubContext;
            _chatHubContext = chatHubContext;
            _userService = userService;
        }

        public async Task<bool> CreateNotification(EventDto notificationData)
        {
            if (notificationData.Whom == "Admin")
            {
                IEnumerable<AdminDto> admins = await _userService.GetAdmins();
                foreach (var admin in admins)
                {
                    var newNotification = new Notification
                    {
                        NotificationId = Guid.NewGuid().ToString(),
                        UserId = admin.UserId,
                        Entity = notificationData.Entity!,
                        EntityId = notificationData.EntityId!,
                        IsRead = false,
                        Title = notificationData.Title!,
                        Message = notificationData.Message!,
                        CreatedAt = DateTime.UtcNow,
                    };
                    await _appDbContext.Notifications.AddAsync(newNotification);
                    await _appDbContext.SaveChangesAsync();
                }
                await _hubContext.Clients.Group("admin").SendAsync("ReceiveMessage", "DemoMessage");
            }
            else if (notificationData.Whom == "User")
            {
                foreach (var userId in notificationData.UserId!)
                {
                    var newNotification = new Notification
                    {
                        NotificationId = Guid.NewGuid().ToString(),
                        UserId = userId,
                        Entity = notificationData.Entity!,
                        EntityId = notificationData.EntityId!,
                        IsRead = false,
                        Title = notificationData.Title!,
                        Message = notificationData.Message!,
                        CreatedAt = DateTime.UtcNow,
                    };
                    await _appDbContext.Notifications.AddAsync(newNotification);
                    await _appDbContext.SaveChangesAsync();
                    await _hubContext.Clients.Group($"user:{userId}").SendAsync("ReceiveMessage", "DemoMessage");
                }
            }
            return true;
        }

    }


}