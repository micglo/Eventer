using System;
using Eventer.Utility.CustomLogger;
using Eventer.WindowsService.Service.Client.Interface;
using Eventer.WindowsService.Service.Infrasturcture.Interface;

namespace Eventer.WindowsService.Service.Client
{
    public abstract class ServiceBase
    {
        protected IEventerApiService EventerApiService;
        protected IDateHelper DateHelper;
        protected ICustomLogger CustomLogger;

        protected string DateFrom { get; } = DateTime.Today.ToString("yyyy-MM-dd");
        protected string DateTo { get; } = DateTime.Today.AddDays(14).ToString("yyyy-MM-dd");

        protected ServiceBase(ICustomLogger customLogger)
        {
            CustomLogger = customLogger;
        }
    }
}