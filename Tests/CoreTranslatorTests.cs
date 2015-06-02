using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Core;
using DataLayer.Logic;
using DataLayer.Schema;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    class CoreTranslatorTests
    {
        private const string ExistingVariable = "ExistingVariable";
        private const string LesserVariable = "LesserVariable";
        private const string GreaterVariable = "GreaterVariable";
        private const string NotExistingVariable = "NotExistingVariable";

        private ICoreTranslator _sut;
        private IStateManager _stateManager;

        [SetUp]
        public void SetUp()
        {
            _stateManager = Substitute.For<IStateManager>();
            _sut = new CoreTranslator();
            _stateManager.HasVariable(ExistingVariable).Returns(true);
            _stateManager.HasVariable(NotExistingVariable).Returns(false);
        }

        [Test]
        public void should_be_able_to_get_registered_classnames()
        {
            Assert.That(_sut.GetRegisteredClassnames(), Is.EquivalentTo(new List<string>()));
            _sut.InitializeUnit(_sut.GetType().Assembly);
            Assert.That(_sut.GetRegisteredClassnames(), Is.EquivalentTo(new List<string>
            {
                "ExpressionTrue", 
                "ExpressionFalse", 
                "ExpressionOr",
                "ExpressionAnd",
                "ExpressionVariableExists",
                "ExpressionInt"
            }));
        }

        [Test]
        public void should_be_able_to_expand_type_into_bool()
        {
            _sut.InitializeUnit(_sut.GetType().Assembly);
            BoolExpandableExpression expr = new ExpressionTrue();
            Assert.That(_sut.ExpandToBool(expr, _stateManager), Is.True);
        }

        [Test]
        [ExpectedException(typeof(NotRegisteredExpressionException))]
        public void should_throw_exception_on_unknown_type()
        {
            BoolExpandableExpression expr = new ExpressionTrue();
            _sut.ExpandToBool(expr, _stateManager);
        }

        [Test]
        public void expression_false_should_evaluate_into_false()
        {
            _sut.InitializeUnit(_sut.GetType().Assembly);
            BoolExpandableExpression expr = new ExpressionFalse();
            Assert.That(_sut.ExpandToBool(expr, _stateManager), Is.False);
        }

        [Test]
        public void expression_or_with_no_args_should_return_false()
        {
            _sut.InitializeUnit(_sut.GetType().Assembly);
            BoolExpandableExpression expr = new ExpressionOr();
            Assert.That(_sut.ExpandToBool(expr, _stateManager), Is.False);
        }

        [Test]
        public void expression_or_with_false_should_return_false()
        {
            _sut.InitializeUnit(_sut.GetType().Assembly);
            BoolExpandableExpression expr = new ExpressionOr();
            expr.Args.Add(new ExpressionFalse());
            Assert.That(_sut.ExpandToBool(expr, _stateManager), Is.False);
        }

        [Test]
        public void expression_or_with_false_and_true_should_return_true()
        {
            _sut.InitializeUnit(_sut.GetType().Assembly);
            BoolExpandableExpression expr = new ExpressionOr();
            expr.Args.Add(new ExpressionFalse());
            expr.Args.Add(new ExpressionTrue());
            Assert.That(_sut.ExpandToBool(expr, _stateManager), Is.True);
        }

        [Test]
        public void expression_and_with_no_args_should_return_true()
        {
            _sut.InitializeUnit(_sut.GetType().Assembly);
            BoolExpandableExpression expr = new ExpressionAnd();
            Assert.That(_sut.ExpandToBool(expr, _stateManager), Is.True);
        }

        [Test]
        public void expression_and_with_true_should_return_true()
        {
            _sut.InitializeUnit(_sut.GetType().Assembly);
            BoolExpandableExpression expr = new ExpressionAnd();
            expr.Args.Add(new ExpressionTrue());
            Assert.That(_sut.ExpandToBool(expr, _stateManager), Is.True);
        }

        [Test]
        public void expression_and_with_true_and_false_should_return_false()
        {
            _sut.InitializeUnit(_sut.GetType().Assembly);
            BoolExpandableExpression expr = new ExpressionAnd();
            expr.Args.Add(new ExpressionTrue());
            expr.Args.Add(new ExpressionFalse());
            Assert.That(_sut.ExpandToBool(expr, _stateManager), Is.False);
        }

        [Test]
        public void expression_variable_exists_should_return_whether_variable_exists()
        {
            _sut.InitializeUnit(_sut.GetType().Assembly);
            Assert.That(_sut.ExpandToBool(new ExpressionVariableExists(NotExistingVariable), _stateManager), Is.EqualTo(false));
            Assert.That(_sut.ExpandToBool(new ExpressionVariableExists(ExistingVariable), _stateManager), Is.EqualTo(true));
        }

        [Test]
        public void expression_variable_equal_should_work()
        {
            var expectedValueString = "5";

            var lesserValueString = "4";

            var greaterValueString = "6";

            var expr = new ExpressionInt {VariableName = ExistingVariable, Value = 5, OperType = OperType.Equal};

            _sut.InitializeUnit(_sut.GetType().Assembly);

            var resultMap = new Dictionary<string, Dictionary<OperType, bool>>
            {
                {
                    lesserValueString,
                    new Dictionary<OperType, bool>
                    {
                        {OperType.Equal, false}
                    }
                },
                {
                    expectedValueString,
                    new Dictionary<OperType, bool>
                    {
                        {OperType.Equal, true}
                    }
                },
                {
                    greaterValueString,
                    new Dictionary<OperType, bool>
                    {
                        {OperType.Equal, false}
                    }
                }
            };

            foreach (var value in resultMap.Keys)
            {
                foreach (var oper in resultMap[value].Keys)
                {
                    _stateManager.GetString(ExistingVariable).Returns(value);
                    expr.OperType = oper;
                    Assert.That(_sut.ExpandToBool(expr, _stateManager), Is.EqualTo(resultMap[value][oper]));
                }
            }
        }
    }
}
