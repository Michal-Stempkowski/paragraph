using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Logic;
using NUnit.Framework;

namespace Tests
{
    public class StateManagerTests
    {
        private IStateManager _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new StateManager();
        }

        [Test]
        public void state_handling_should_work()
        {
            const string exampleVariableName = "exampleVariableName";
            const string exampleVariableValue = "44";

            Assert.That(_sut.HasVariable(exampleVariableName), Is.False);
            Assert.That(_sut.GetString(exampleVariableName), Is.EqualTo(null));

            _sut.SetString(exampleVariableName, exampleVariableValue);

            Assert.That(_sut.HasVariable(exampleVariableName), Is.True);
            Assert.That(_sut.GetString(exampleVariableName), Is.EqualTo(exampleVariableValue));
        }

        [Test]
        public void get_float_epsilon_value_should_return_default_value_if_not_specified()
        {
            Assert.That(_sut.GetFloatEpsilonValue(), Is.EqualTo(StateManager.DefaultFloatEpsilonValue));
        }

        [Test]
        public void should_be_able_to_change_float_epsilon_value()
        {
            Assert.That(_sut.GetFloatEpsilonValue(), Is.EqualTo(StateManager.DefaultFloatEpsilonValue));

            string newEpsilonValueString = 0.001.ToString(CultureInfo.InvariantCulture.NumberFormat);

            _sut.SetString(StateManager.FloatEpsilonValueId, newEpsilonValueString);
            Assert.That(_sut.GetFloatEpsilonValue().ToString(CultureInfo.InvariantCulture.NumberFormat), 
                Is.EqualTo(newEpsilonValueString));
        }
    }
}
