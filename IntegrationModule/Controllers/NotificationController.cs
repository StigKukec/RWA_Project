using AutoMapper;
using DataLayer.EmailSenderService;
using DataLayer.Repositories;
using IntegrationModule.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace IntegrationModule.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly IOptions<MFakeSMTP> _config;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public NotificationController(IOptions<MFakeSMTP> config, ILogger<NotificationController> logger, IMapper mapper, IUserRepository userRepository)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPost("[action]")]
        public void SendAllNotification()
        {
            var notifications = GetUnsentNotifications();
            SmtpClient smtp = new()
            {
                Port = _config.Value.Port,
                UseDefaultCredentials = true,
                Host = _config.Value.Host
            };
            try
            {
                notifications.ToList().ForEach(x => smtp.Send(x.Sender, x.Receiver, x.Subject, x.Body));
                _userRepository.SendNotifications();
            }
            catch (Exception)
            {

                throw new Exception("Sending email or database storing failed to execute!");
            }
        }

        [HttpGet("[action]")]
        public IEnumerable<MNotification> GetAllNotifications()
        {
            var dalNotifications = _userRepository.GetAllNotifications();
            var notifications = _mapper.Map<IEnumerable<MNotification>>(dalNotifications);

            return notifications;
        }
        [HttpGet("[action]")]
        public IEnumerable<MNotification> GetUnsentNotifications()
        {
            var dalNotifications = _userRepository.GetAllNotifications();
            var notSentNotifications = dalNotifications.Where(x => x.SentAt == null);
            var notifications = _mapper.Map<IEnumerable<MNotification>>(notSentNotifications);

            return notifications;
        }


        [HttpGet("[action]/{id}")]
        public ActionResult<MNotification> GetNotification(int id)
        {
            try
            {
                var dalNotification = _userRepository.GetNotification(id);
                var notification = _mapper.Map<MNotification>(dalNotification);
                if (notification == null)
                {
                    return NotFound();
                }
                return notification;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult CreateNotification(MNotificationManager notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userRepository.CreateNotification(notification.Receiver, notification.Subject, notification.Body);

            return Ok(notification);
        }

        [HttpPut("[action]")]
        public ActionResult UpdateNotification([FromBody] MNotificationManager notification, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userRepository.UpdateNotification(id, notification.Receiver, notification.Subject, notification.Body);

            return Ok(notification);
        }

        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteNotification(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userRepository.DeleteNotification(id);

            return Ok();
        }
    }
}
