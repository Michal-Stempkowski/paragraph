using DataLayer.Core;
using DataLayer.Logic;

namespace DataLayer.Schema
{
    [BoolTranslatable]
    public class ExpressionFalse : BoolExpandableExpressionImpl<ExpressionFalse>
    {
        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager, ICoreTranslator translator)
        {
            return false;
        }
    }
}
