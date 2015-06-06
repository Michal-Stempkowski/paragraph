using DataLayer.Logic;

namespace DataLayer.Schema.Variable
{
    [BoolTranslatable]
    public class ExpressionVariableExists : BoolExpandableExpressionImpl<ExpressionVariableExists>
    {
        public ExpressionVariableExists()
        {
            
        }

        public ExpressionVariableExists(string name)
        {
            SimpleArgs.Add(name);
        }

        public override bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            return stateManager.HasVariable(expr.SimpleArgs[0]);
        }
    }
}
