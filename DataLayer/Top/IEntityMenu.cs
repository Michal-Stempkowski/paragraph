using System.Collections.Generic;
using DataLayer.Logic;

namespace DataLayer.Top
{
    public interface IEntityMenu
    {
        string DescritptionBarContent { get; }
        IList<IDecision> GetAvailableDecisions();
        void Decide(IDecision decision);
        void PerformTransition(string path);
        IStateManager StateManager { get; }
        void SaveGame(string path);
    }
}