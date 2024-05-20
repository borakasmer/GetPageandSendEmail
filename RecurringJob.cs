using GetPageandSendEmail;
using GetPageandSendEmail.Service;
using Hangfire;

namespace GetPageandSendEmail
{
    public static class RecurringJobs
    {
        [Obsolete]
        public static void GetWeeklyWebReport(ICoreService service, string url)
        {
            RecurringJob.RemoveIfExists(nameof(service.SendMailWebReport));
            RecurringJob.AddOrUpdate<ICoreService>(nameof(service.SendMailWebReport), x => x.SendMailWebReport(url), 
                Cron.Weekly(DayOfWeek.Friday,17, 30), TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time")); //17:30        
        }
    }
}

