using FluentValidation;
using NotificationService.Dtos;
using NotificationService.Helpers;
using NotificationService.Interfaces;
using NotificationService.Mappers;
using NotificationService.Models;

namespace NotificationService.Services
{
    public class NotificationEventService : INotificationEventService
    {
        private INotificationEventRepository _notificationRepository;
        private readonly IValidator<NotificationEventDto> _validator;

        public NotificationEventService(
            INotificationEventRepository notificationEventRepository, 
            IValidator<NotificationEventDto> validator)
        {
            _notificationRepository = notificationEventRepository;
            _validator = validator;
        }

        public async Task<NotificationEventDto> CreateNotificationEventAsync(NotificationEventDto notificationEventDto, CancellationToken token = default)
        {
            _validator.Validate(notificationEventDto, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(Utils.CreateValidationRuleSetName);
            });

            NotificationEventModel notificationEvent = notificationEventDto.DtoToNotificationEventModel();

            int eventId = await _notificationRepository.CreateNotificationEventAsync(notificationEvent, token);
            
            return notificationEventDto with { Id = eventId };
        }
    }
}
