using System.Threading.Tasks;
using Eventer.Model.QueryString.Event;

namespace Eventer.Utility.EventQueryStringPatternMatch
{
    public interface IPatternMatchingCount
    {
        Task<long> PatternMatchingQueryStringEventCount(EventQueryModel queryModel);
    }
}