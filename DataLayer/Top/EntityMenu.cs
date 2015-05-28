using System.Collections.Generic;
using DataLayer.Logic;

namespace DataLayer.Top
{
    public class EntityMenu
    {
        private readonly IEntityDataProvider _provider;
        private readonly IStateManager _stateManager;

        public EntityMenu(IEntityDataProvider provider, IStateManager stateManager)
        {
            _provider = provider;
            _stateManager = stateManager;
        }

        public string DescritptionBarContent
        {
            get
            {
                var currentRoom = _provider.CurrentEntity;
                return currentRoom.Description; 
                
            }
        }

        public IList<IDecision> GetAvailableDecisions()
        {
            var currentRoom = _provider.CurrentEntity;
            return currentRoom.Decisions;
        }

        public void Decide(IDecision decision)
        {
            decision.Effect(_stateManager);
            PerformTransition(decision.Destination);
        }

        public void PerformTransition(string path)
        {
            _provider.PerformEntityTransition(path, _stateManager);
        }
    }
}
