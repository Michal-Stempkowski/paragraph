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
        private const string CurrentRoomDescription = "Current room description.";
        private const string _decisionPath = @"AMN/NORTHERN/TAVERN/GUILDMASTER_TALK00";
        private readonly IRoomDataProvider _provider;
        private readonly IStateManager _stateManager;
        private readonly RoomMenu _sut;
        private readonly IDecision _decision;
        private readonly IRoom _currentRoom;

        public RoomMenuTests()
        {
            _provider = Substitute.For<IRoomDataProvider>();
            _stateManager = Substitute.For<IStateManager>();
            _sut = new RoomMenu(_provider, _stateManager);
            _decision = Substitute.For<IDecision>();
            _currentRoom = Substitute.For<IRoom>();

            _provider.CurrentRoom.Returns(_currentRoom);
            _currentRoom.Description.Returns(CurrentRoomDescription);
            

            var availableDecisions = new List<IDecision> { _decision };

            _currentRoom.Decisions.Returns(availableDecisions);

            _decision.Path.Returns(_decisionPath);
        }

        [Test]
        public void should_be_able_to_show_description_bar_content()
        {
            Assert.That(_sut.DescritptionBarContent, Is.EqualTo(CurrentRoomDescription));
        }

        [Test]
        public void should_be_able_to_list_all_available_decisions()
        {
            Assert.That(_sut.GetAvailableDecisions(), Is.EquivalentTo(new List<IDecision> { _decision }));
        }

        [Test]
        public void should_handle_proper_room_transition_well()
        {
            _sut.PerformTransition(_decisionPath);

            _provider.Received().PerformRoomTransition(_decisionPath, _stateManager);
        }

        [Test]
        public void should_be_able_to_serve_decision_selection_well()
        {
            var availableDecisions = new List<IDecision> { _decision };

            var result = "";
            Assert.That(result, Is.Empty);
            var expectedResult = "expected";
            _decision.Effect = x => result = expectedResult;

            _sut.Decide(_decision);

            _provider.Received().PerformRoomTransition(_decisionPath, _stateManager);
            Assert.That(result, Is.EqualTo(expectedResult));
            
        }
    }
}
