using System;

namespace Eventer.WindowsService.Service.Infrasturcture.Interface
{
    public interface IDateHelper
    {
        DateTime GetEventDateFromString(string eventDate);
    }
}