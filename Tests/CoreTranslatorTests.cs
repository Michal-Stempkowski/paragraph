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
        private ICoreTranslator _sut;
        private IStateManager _stateManager;

        [SetUp]
        public void SetUp()
        {
            _stateManager = Substitute.For<IStateManager>();
            _sut = new CoreTranslator();
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
                "ExpressionAnd"
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
    }
}
