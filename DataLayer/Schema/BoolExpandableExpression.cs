using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Core;
using DataLayer.Logic;

namespace DataLayer.Schema
{
    public class BoolExpandableExpression : ISchema
    {
        protected BoolExpandableExpression()
        {
            Name = this.GetType().Name;
            Args = new Dictionary<int, BoolExpandableExpression>();
            SimpleArgs = new Dictionary<int, string>();
        }

        public string Name { get; set; }
        public Dictionary<int, BoolExpandableExpression> Args { get; set; }
        public Dictionary<int, string> SimpleArgs { get; set; }

        public BoolExpandableExpression DeepCopy()
        {
            return new BoolExpandableExpression()
            {
                Args = Args.ToDictionary(
                    pair => pair.Key, 
                    pair => pair.Value.DeepCopy()),
                Name = Name,
                SimpleArgs = SimpleArgs
            };
        }

        public virtual bool TranslateToBool(BoolExpandableExpression expr, IStateManager stateManager, ICoreTranslator translator)
        {
            throw new YouShouldUseProperTranslatorInsteadOfCallingThisFunctionDirectlyException();
        }

        public static T Convert<T>(BoolExpandableExpression expression)
            where T : BoolExpandableExpression, new()
        {
            return new T()
            {
                Args = expression.Args.ToDictionary(
                    x => x.Key,
                    x => x.Value), // TODO: DO NOT! Add backward translation switch'o'case, use it in validation step (shallow)
                Name = expression.Name,
                SimpleArgs = expression.SimpleArgs
            };
        }

        public T Convert44<T>()
            where T : BoolExpandableExpression, new()
        {
            return new T()
            {
                Args = Args.ToDictionary(
                    x => x.Key,
                    x => x.Value), // TODO: DO NOT! Add backward translation switch'o'case, use it in validation step (shallow)
                Name = Name,
                SimpleArgs = SimpleArgs
            };
        }

        public override string ToString()
        {
            return 
                Name + 
                "(" +
                String.Join(
                    ", ",
                    SimpleArgs
                    .OrderBy(x => x.Key)
                    .Select(x => x.Value)) +
                ")";
        }
    }

    public class BoolExpandableExpressionImpl<T> : BoolExpandableExpression where T : BoolExpandableExpression, new()
    {
        public static Func<BoolExpandableExpression, IStateManager, ICoreTranslator, bool> GetBoolTranslator()
        {
            return (expr, stateManager, coreTranslator) => new T().TranslateToBool(expr, stateManager, coreTranslator);
        }
        public static Func<BoolExpandableExpression> CreateInstance()
        {
            return () => new T();
        }
    }

    public class YouShouldUseProperTranslatorInsteadOfCallingThisFunctionDirectlyException : Exception
    {
    }
}
