using System;
using Ninject;
using Quartz;
using Quartz.Simpl;
using Quartz.Spi;

namespace Eventer.WindowsService
{
    public class NinjectJobFactory : SimpleJobFactory
    {
        private readonly IKernel _kernel;

        public NinjectJobFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public override IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                return (IJob)_kernel.Get(bundle.JobDetail.JobType);
            }
            catch (Exception e)
            {
                throw new SchedulerException(
                    $"Problem with instantiating job '{bundle.JobDetail.Key}' from NinjectJobFactory.",
                    e);
            }
        }
    }
}