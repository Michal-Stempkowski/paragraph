using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Core;
using DataLayer.Logic;
using DataLayer.Schema;
using DataLayer.Schema.Variable;
using DataLayer.Schema.Variable.Mutable;
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
            _stateManager.GetFloatEpsilonValue().Returns(0.1f);
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
                "ExpressionIntCheck",
                "ExpressionStringCheck",
                "ExpressionFloatCheck",
                "ExpressionNot",
                "ExpressionAssign"
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
        public void expression_not_should_negate_value()
        {
            _sut.InitializeUnit(_sut.GetType().Assembly);
            BoolExpandableExpression expr = new ExpressionNot();

            expr.Args.Insert(0, new ExpressionTrue());
            Assert.That(_sut.ExpandToBool(expr, _stateManager), Is.False);

            expr.Args.Insert(0, new ExpressionFalse());
            Assert.That(_sut.ExpandToBool(expr, _stateManager), Is.True);
        }

        [Test]
        public void expression_variable_exists_should_return_whether_variable_exists()
        {
            _sut.InitializeUnit(_sut.GetType().Assembly);
            Assert.That(_sut.ExpandToBool(new ExpressionVariableExists(NotExistingVariable), _stateManager), Is.EqualTo(false));
            Assert.That(_sut.ExpandToBool(new ExpressionVariableExists(ExistingVariable), _stateManager), Is.EqualTo(true));
        }

        [Test]
        public void expression_int_variable_equal_should_work()
        {
            var expr = new ExpressionIntCheck { VariableName = ExistingVariable, Value = 5, OperType = OperType.Equal };

            var resultMap = GenerateResultMap("4", "5", "6");

            _sut.InitializeUnit(_sut.GetType().Assembly);

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

        [Test]
        public void expression_string_variable_equal_should_work()
        {
            var expr = new ExpressionStringCheck { VariableName = ExistingVariable, Value = "Bob", OperType = OperType.Equal };

            var resultMap = GenerateResultMap("Ala", "Bob", "Fred");

            _sut.InitializeUnit(_sut.GetType().Assembly);

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

        [Test]
        public void expression_float_variable_equal_should_work()
        {
            var expr = new ExpressionFloatCheck { VariableName = ExistingVariable, Value = 5.0f, OperType = OperType.Equal };

            var resultMap = GenerateResultMap("4.0", "5.05", "6.0");

            _sut.InitializeUnit(_sut.GetType().Assembly);

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

        [Test]
        public void expression_assign_value()
        {
            var expr = ExpressionAssign.Create(ExistingVariable, 5);

            _sut.InitializeUnit(_sut.GetType().Assembly);

            Assert.That(_sut.ExpandToBool(expr, _stateManager), Is.True);

            _stateManager.Received().SetString(ExistingVariable, "5");
        }

        private static Dictionary<string, Dictionary<OperType, bool>> GenerateResultMap(string lesserValueString, string expectedValueString, string greaterValueString)
        {
            return new Dictionary<string, Dictionary<OperType, bool>>
            {
                {
                    lesserValueString,
                    new Dictionary<OperType, bool>
                    {
                        {OperType.Equal, false},
                        {OperType.Greater, false},
                        {OperType.GreaterEqual, false},
                        {OperType.Lesser, true},
                        {OperType.LesserEqual, true}
                    }
                },
                {
                    expectedValueString,
                    new Dictionary<OperType, bool>
                    {
                        {OperType.Equal, true},
                        {OperType.Greater, false},
                        {OperType.GreaterEqual, true},
                        {OperType.Lesser, false},
                        {OperType.LesserEqual, true}
                    }
                },
                {
                    greaterValueString,
                    new Dictionary<OperType, bool>
                    {
                        {OperType.Equal, false},
                        {OperType.Greater, true},
                        {OperType.GreaterEqual, true},
                        {OperType.Lesser, false},
                        {OperType.LesserEqual, false}
                    }
                }
            };
        }
    }
}
