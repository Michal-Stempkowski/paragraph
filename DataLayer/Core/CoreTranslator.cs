using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using DataLayer.Logic;
using DataLayer.Schema;

namespace DataLayer.Core
{
    using TranslatorFuncType = Func<BoolExpandableExpression, IStateManager, bool>;
    public class CoreTranslator : ICoreTranslator
    {
        
        private readonly IDictionary<string, TranslatorFuncType> _translatorFuncs;

        public CoreTranslator()
        {
            _translatorFuncs = new Dictionary<string, TranslatorFuncType>();
        }

        public void InitializeUnit(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes()
                .Where(x => x.GetCustomAttributes()
                    .Any(y => y is BoolTranslatableAttribute)))
            {
                try
                {
                    // ReSharper disable once PossibleNullReferenceException
                    var translator = type.BaseType.GetMethods()
                        .Where(m => m.Name == "GetBoolTranslator")
                        .Single(m => m.GetParameters().Length == 0)
                        .Invoke(null, null)
                        as TranslatorFuncType;

                    _translatorFuncs.Add(type.Name, translator);
                }
                catch (InvalidOperationException ex)
                {
                    throw new MalformedSchemaAssemblyException();
                }
                catch (NullReferenceException ex)
                {
                    throw new MalformedSchemaAssemblyException();
                }
                
            }
        }

        public List<string> GetRegisteredClassnames()
        {
            return _translatorFuncs.Keys.ToList();
        }

        public bool ExpandToBool(BoolExpandableExpression expr, IStateManager stateManager)
        {
            TranslatorFuncType translator;
            if (!_translatorFuncs.TryGetValue(expr.Name, out translator))
            {
                throw new NotRegisteredExpressionException(expr.Name);
            }

            return translator(expr, stateManager);
        }
    }

    public class NotRegisteredExpressionException : Exception
    {
        public readonly string Name;

        public NotRegisteredExpressionException(string name) : base(name)
        {
            Name = name;
        }
    }

    public class MalformedSchemaAssemblyException : Exception
    {
    }
}