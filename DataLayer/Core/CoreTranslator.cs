using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using DataLayer.Logic;
using DataLayer.Schema;

namespace DataLayer.Core
{
    using TranslatorFuncType = Func<BoolExpandableExpression, IStateManager, ICoreTranslator, bool>;
    using CreatorFuncType = Func<BoolExpandableExpression>;

    public class CoreTranslator : ICoreTranslator
    {
        
        private readonly IDictionary<string, TranslatorFuncType> _translatorFuncs;
        private readonly IDictionary<string, CreatorFuncType> _creatorFuncs;

        public CoreTranslator()
        {
            _translatorFuncs = new Dictionary<string, TranslatorFuncType>();
            _creatorFuncs = new Dictionary<string, CreatorFuncType>();
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

                    var creator = type.BaseType.GetMethods()
                        .Where(m => m.Name == "CreateInstance")
                        .Single(m => m.GetParameters().Length == 0)
                        .Invoke(null, null)
                        as CreatorFuncType;

                    _translatorFuncs.Add(type.Name, translator);
                    _creatorFuncs.Add(type.Name, creator);
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

            return translator(expr, stateManager, this);
        }

        public T CreateInstance<T>(BoolExpandableExpression templ = null)
            where T : BoolExpandableExpression
        {
            CreatorFuncType creator;
            var typeName = typeof(T).Name;

            if (!_creatorFuncs.TryGetValue(typeName, out creator))
            {
                throw new NotRegisteredExpressionException(typeName);
            }

            var result = creator() as T;

            if (result == null)
            {
                throw new NotRegisteredExpressionException(typeName);
            }

            if (templ != null)
            {
                result.SimpleArgs = new Dictionary<int, string>(templ.SimpleArgs);
                result.Args = new Dictionary<int, BoolExpandableExpression>(templ.Args);
            }

            return result;
        }

        public BoolExpandableExpression CreateInstanceByName(string name)
        {
            CreatorFuncType creator;

            if (!_creatorFuncs.TryGetValue(name, out creator))
            {
                throw new NotRegisteredExpressionException(name);
            }

            var result = creator();

            if (result == null)
            {
                throw new NotRegisteredExpressionException(name);
            }

            return result;
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