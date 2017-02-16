using System;
using Eventer.WindowsService.Service.Infrasturcture.Interface;

namespace Eventer.WindowsService.Service.Infrasturcture
{
    public class DateHelper : IDateHelper
    {
        public DateTime GetEventDateFromString(string eventDate)
        {
            return DateTime.Parse(eventDate);
        }
    }
}