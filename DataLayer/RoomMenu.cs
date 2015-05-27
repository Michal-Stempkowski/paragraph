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

        public string ShowDescritptionBarContent()
        {
            return _provider.GetDescriptionBarContent();
        }

        public IList<IDecision> GetAvailableDecisions()
        {
            return _provider.GetAvailableDecisions(_stateManager);
        }

        public bool Decide(IDecision decision)
        {
            throw new NotImplementedException();
        }
    }
}
