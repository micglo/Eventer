using System;
using System.Threading.Tasks;
using Eventer.Model.QueryString.Event;

namespace Eventer.Utility.EventQueryStringPatternMatch
{
    public class PatternMatchCount
    {
        private readonly Func<EventQueryModel, bool> _patternQueryStringEvent;
        private readonly Func<EventQueryModel, Task<long>> _matchQueryStringEventAsync;

        public PatternMatchCount(Func<EventQueryModel, bool> pattern
            , Func<EventQueryModel, Task<long>> match)
        {
            _patternQueryStringEvent = pattern;
            _matchQueryStringEventAsync = match;
        }

        public bool CheckPatternQueryStringEvent(EventQueryModel queryModel) => _patternQueryStringEvent(queryModel);
        public Task<long> UseMatchQueryStringEventCount(EventQueryModel queryModel) => _matchQueryStringEventAsync(queryModel);
    }
}