using DigitalBookStoreManagement.Exceptions;
using DigitalBookStoreManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBookStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[EnableCors("AllowAllOrigins")]
    public class NotificationController : ControllerBase
    {
        private readonly I_NotificationRepository _notificationRepository;

        public NotificationController(I_NotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllNotifications()
        {
            var notifications = await _notificationRepository.GetAllNotificationsAsync();
            return Ok(notifications);
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            try
            {
                await _notificationRepository.DeleteNotificationAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}