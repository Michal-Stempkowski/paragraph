using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Logic;

namespace DataLayer.Schema
{
    public class BoolExpandableExpression : ISchema
    {
        protected BoolExpandableExpression()
        {
            Name = this.GetType().Name;
            Args = new List<BoolExpandableExpression>();
            SimpleArgs = new List<string>();
        }

        public string Name { get; set; }
        public List<BoolExpandableExpression> Args { get; set; }
        public List<string> SimpleArgs { get; set; }

        public virtual bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            throw new YouShouldUseProperTranslatorInsteadOfCallingThisFunctionDirectlyException();
        }
    }

    public class BoolExpandableExpressionImpl<T> : BoolExpandableExpression where T : BoolExpandableExpression, new()
    {
        public static Func<BoolExpandableExpression, IStateManager, bool> GetBoolTranslator()
        {
            return (expr, stateManager) => new T().TranslateToBool(expr, stateManager);
        }
    }

    public class YouShouldUseProperTranslatorInsteadOfCallingThisFunctionDirectlyException : Exception
    {
    }
}
