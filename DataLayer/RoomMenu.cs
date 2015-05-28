using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class RoomMenu
    {
        private readonly IRoomDataProvider _provider;
        private readonly IStateManager _stateManager;

        public RoomMenu(IRoomDataProvider provider, IStateManager stateManager)
        {
            _provider = provider;
            _stateManager = stateManager;
        }

        public string DescritptionBarContent
        {
            get
            {
                var currentRoom = _provider.CurrentRoom;
                return currentRoom.Description; 
                
            }
        }

        public IList<IDecision> GetAvailableDecisions()
        {
            var currentRoom = _provider.CurrentRoom;
            return currentRoom.Decisions;
        }

        public void Decide(IDecision decision)
        {
            decision.Effect(_stateManager);
            PerformTransition(decision.Path);
        }

        public void PerformTransition(string path)
        {
            _provider.PerformRoomTransition(path, _stateManager);
        }
    }
}
