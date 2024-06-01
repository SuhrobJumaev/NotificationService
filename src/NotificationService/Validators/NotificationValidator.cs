using FluentValidation;
using NotificationService.Dtos;
using NotificationService.Helpers;
using NotificationService.Models.Enums;
using System.Reflection;

namespace NotificationService.Validators
{
    public class NotificationValidator : AbstractValidator<NotificationEventDto>
    {
        public NotificationValidator()
        {
            RuleSet(Utils.CreateValidationRuleSetName, () =>
            {
                RuleFor(x => x.OrderType).Must(BeValidOrderType);
                RuleFor(x => x.Card).NotEmpty();
                RuleFor(x => x.SessionId).NotEmpty();
                RuleFor(x => x.EventDate).Must(BeValidEventDate);
                RuleFor(x => x.WebsiteUrl).NotEmpty();

            });
        }

        private bool BeValidOrderType(string orderType)
        {
            if (!Enum.TryParse(orderType, out OrderType convertedOrderType))
                return false;

            return true;
        }

        private bool BeValidEventDate(string eventDate)
        {
            if(!DateTime.TryParse(eventDate, out DateTime convertedEventDate))
                return false;

            return true;
        }
    }
}
