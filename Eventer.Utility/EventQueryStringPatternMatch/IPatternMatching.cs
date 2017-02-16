using System.Collections.Generic;
using System.Threading.Tasks;
using Eventer.Model.Dto.Event;
using Eventer.Model.QueryString.Event;

namespace Eventer.Utility.EventQueryStringPatternMatch
{
    public interface IPatternMatching
    {
        Task<ICollection<EventDto>> PatternMatchingQueryStringEventAsync(EventQueryModel queryModel);
    }
}