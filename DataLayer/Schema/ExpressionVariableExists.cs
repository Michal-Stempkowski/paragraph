using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Schema
{
    public class ExpressionVariableExists : BoolExpandableExpression
    {
        public ExpressionVariableExists()
        {
            
        }

        public ExpressionVariableExists(string name)
        {
            SimpleArgs.Add(name);
        }
    }
}
