using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eventer.Model.Dto.Event;
using Eventer.Model.QueryString.Event;

namespace Eventer.Utility.EventQueryStringPatternMatch
{
    public class PatternMatch
    {
        private readonly Func<EventQueryModel, bool> _patternQueryStringEvent;
        private readonly Func<EventQueryModel, Task<IEnumerable<EventDto>>> _matchQueryStringEventAsync;

        public PatternMatch(Func<EventQueryModel, bool> pattern
            , Func<EventQueryModel, Task<IEnumerable<EventDto>>> match)
        {
            _patternQueryStringEvent = pattern;
            _matchQueryStringEventAsync = match;
        }

        public bool CheckPatternQueryStringEvent(EventQueryModel queryModel) => _patternQueryStringEvent(queryModel);
        public Task<IEnumerable<EventDto>> UseMatchQueryStringEventAsync(EventQueryModel queryModel) => _matchQueryStringEventAsync(queryModel);
    }
}