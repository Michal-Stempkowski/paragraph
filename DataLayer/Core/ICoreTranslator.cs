using System.Collections.Generic;
using System.Reflection;
using DataLayer.Logic;
using DataLayer.Schema;

namespace DataLayer.Core
{
    public interface ICoreTranslator
    {
        void InitializeUnit(Assembly assembly);
        List<string> GetRegisteredClassnames();
        bool ExpandToBool(BoolExpandableExpression expr, IStateManager stateManager);
    }
}