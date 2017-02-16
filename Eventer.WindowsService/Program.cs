using Eventer.Utility.CustomLogger;
using Eventer.WindowsService.Service.Client;
using Eventer.WindowsService.Service.Client.Interface;
using Eventer.WindowsService.Service.Infrasturcture;
using Eventer.WindowsService.Service.Infrasturcture.Interface;
using Quartz;
using Ninject;
using Quartz.Impl;

namespace Eventer.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var kernel = InitializeNinjectKernel();
            var scheduler = kernel.Get<IScheduler>();

#if DEBUG
            var myService = new EventerService(scheduler);
            myService.OnDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else
                        System.ServiceProcess.ServiceBase[] ServicesToRun;
                        ServicesToRun = new System.ServiceProcess.ServiceBase[]
                        {
                            new EventerService(scheduler)
                        };
                        System.ServiceProcess.ServiceBase.Run(ServicesToRun);
#endif
        }

        static IKernel InitializeNinjectKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IScheduler>().ToMethod(x =>
            {
                var scheduler = new StdSchedulerFactory().GetScheduler();
                scheduler.JobFactory = new NinjectJobFactory(kernel);
                return scheduler;
            });

            kernel.Bind<IEventerApiService>().To<EventerApiService>();
            kernel.Bind<IWroclawGoApiService>().To<WroclawGoApiService>();
            kernel.Bind<IPoznanApiService>().To<PoznanApiService>();
            kernel.Bind<IDateHelper>().To<DateHelper>();
            kernel.Bind<ICustomLogger>().To<CustomLogger>();

            return kernel;
        }
    }
}
