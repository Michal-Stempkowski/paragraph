using System.Collections.Generic;
using DataLayer.Logic;
using DataLayer.Top;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    class EntityMenuTests
    {
        private const string CurrentRoomDescription = "Current room description.";
        private const string DecisionPath = @"AMN/NORTHERN/TAVERN/GUILDMASTER_TALK00";
        private IEntityDataProvider _provider;
        private IStateManager _stateManager;
        private EntityMenu _sut;
        private IDecision _decision;
        private IEntity _currentEntity;

        [SetUp]
        public void SetUp()
        {
            _provider = Substitute.For<IEntityDataProvider>();
            _stateManager = Substitute.For<IStateManager>();
            _sut = new EntityMenu(_provider, _stateManager);
            _decision = Substitute.For<IDecision>();
            _currentEntity = Substitute.For<IEntity>();

            _provider.CurrentEntity.Returns(_currentEntity);
            _currentEntity.Description.Returns(CurrentRoomDescription);
            

            var availableDecisions = new List<IDecision> { _decision };

            _currentEntity.Decisions.Returns(availableDecisions);

            _decision.Destination.Returns(DecisionPath);
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
            _sut.PerformTransition(DecisionPath);

            _provider.Received().PerformEntityTransition(DecisionPath, _stateManager);
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

            _provider.Received().PerformEntityTransition(DecisionPath, _stateManager);
            Assert.That(result, Is.EqualTo(expectedResult));
            
        }
    }
}
