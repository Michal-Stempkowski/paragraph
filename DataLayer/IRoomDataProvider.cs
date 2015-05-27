using System.Collections.Generic;

namespace DataLayer
{
    public interface IRoomDataProvider
    {
        string GetDescriptionBarContent();
        IList<IDecision> GetAvailableDecisions(IStateManager stateManager);
    }
}