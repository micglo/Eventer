using System.ServiceProcess;
using Eventer.WindowsService.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Collection;

namespace Eventer.WindowsService
{
    public partial class EventerService : ServiceBase
    {
        private IScheduler _scheduler;

        public EventerService(IScheduler scheduler)
        {
            _scheduler = scheduler;
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            OnServiceStart(args);
        }

        protected override void OnStop()
        {
            OnServiceStop();
        }

        private void OnServiceStart(string[] args)
        {
            Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter
            {
                Level = Common.Logging.LogLevel.Info
            };

            _scheduler = StdSchedulerFactory.GetDefaultScheduler();

            _scheduler.Start();

            IJobDetail takeEventsJob = JobBuilder.Create<TakeEventsJob>()
                .WithIdentity("TakeEventsJob", "TakeEventsGroup")
                .Build();

            ITrigger takeEventsTrigger = TriggerBuilder.Create()
                .WithIdentity("TakeEventsTrigger", "TakeEventsGroup")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInHours(6).RepeatForever()).Build();

            _scheduler.ScheduleJobs(new System.Collections.Generic.Dictionary<IJobDetail, ISet<ITrigger>>
            {
                {takeEventsJob, new HashSet<ITrigger> {takeEventsTrigger}}
            }, false);
        }

        private void OnServiceStop()
        {
            _scheduler.Shutdown();
        }
    }
}
