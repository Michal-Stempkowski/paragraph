using System.Collections;
using System.Collections.Generic;
using DataLayer;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    class RoomMenuTests
    {
        private readonly IRoomDataProvider _provider;
        private readonly IStateManager _stateManager;
        private readonly RoomMenu _sut;

        public RoomMenuTests()
        {
            _provider = Substitute.For<IRoomDataProvider>();
            _stateManager = Substitute.For<IStateManager>();
            _sut = new RoomMenu(_provider, _stateManager);
        }

        [Test]
        public void should_be_able_to_show_description_bar_content()
        {
            const string description = "description";

            _provider.GetDescriptionBarContent().Returns(description);

            Assert.That(_sut.ShowDescritptionBarContent(), Is.EqualTo(description));
        }

        [Test]
        public void should_be_able_to_list_all_available_decisions()
        {
            var decision = Substitute.For<IDecision>();
            var availableDecisions = new List<IDecision> {decision};

            _provider.GetAvailableDecisions(_stateManager).Returns(availableDecisions);

            Assert.That(_sut.GetAvailableDecisions(), Is.EquivalentTo(availableDecisions));
        }

        [Test]
        public void should_be_able_to_serve_decision_selection_well()
        {
            var decision = Substitute.For<IDecision>();
            var availableDecisions = new List<IDecision> { decision };

            Assert.That(_sut.Decide(decision), Is.True);
        }
    }
}
