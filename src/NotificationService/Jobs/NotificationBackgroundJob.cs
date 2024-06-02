using NotificationService.Helpers;
using NotificationService.Interfaces;
using NotificationService.Models;
using NotificationService.Models.Enums;

namespace NotificationService.Jobs
{
    public class NotificationBackgroundJob : BackgroundService
    {
        private readonly ILogger<NotificationBackgroundJob> _log;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public NotificationBackgroundJob(ILogger<NotificationBackgroundJob> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _log = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int period = 10;
            using PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(period));
            try
            {
                while (!stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await using AsyncServiceScope asyncScope = _serviceScopeFactory.CreateAsyncScope();

                    INotificationEventRepository notoficationRepository = asyncScope.ServiceProvider.GetRequiredService<INotificationEventRepository>();

                    IEnumerable<NotificationEventModel> notificationEvents = await notoficationRepository.GetNotificationEventsAsync(stoppingToken);

                    if (!notificationEvents.Any())
                        continue;

                    _log.LogInformation($"Found new enevnts to send notification");

                    foreach (var notificationEvent in notificationEvents)
                    {
                        string message = string.Empty;

                        switch (notificationEvent.OrderType)
                        {
                            case OrderType.Purchase:
                                message = string.Format(Utils.PurchaseMessage, notificationEvent.OrderType, notificationEvent.WebsiteUrl, notificationEvent.Card);
                                break;
                            case OrderType.CardVerify:
                                message = string.Format(Utils.VerifyCard, notificationEvent.OrderType, notificationEvent.WebsiteUrl, notificationEvent.Card);
                                break;
                            case OrderType.SendOtp:
                                message = string.Format(Utils.SendOtp, notificationEvent.Card);
                                break;
                        }

                        _log.LogInformation("Send message " + message);

                        notificationEvent.Status = NotificationStatus.Complete;
                        notificationEvent.SentDate = DateTime.Now;

                        //Here if we will have problems with perfomence we can use bulk update instead of simple update
                        await notoficationRepository.UpdateNotificationStatusByIdAsync(notificationEvent);
                    }

                }
            }
            catch (Exception ex)
            {
                _log.LogError("NotificationBackgroundJob.Error is " + ex);
            }
            
        }
    }
}
 