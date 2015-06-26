using System.Collections.Generic;
using DataLayer.Data;
using DataLayer.Logic;
using DataLayer.Schema;
using DataLayer.Schema.Variable;
using DataLayer.Storage;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    class JsonDaoProviderTests
    {
        private const string ValidRoomPath = @"path/for/new_room.room";
        private const string ValidManagerPath = @"path/for/state_manager.state";
        private IDaoProvider _sut;
        private JsonDaoProvider<StateManager> _typedSut;
        private RoomSchema _room;
        private IStorageSupervisor _storageSupervisor;
        private DecisionSchema _decision1;
        private DecisionSchema _decision2;
        private string _lastDeviceWriteResult;

        private const string SampleRoomName = "SampleRoomName";
        private const string SampleRoomDescription = "This is a sample description.";

        private const string SampleDecision1Description = "Decision no 1";
        private const string SampleDecision2Description = "Decision no 2";

        private const string SampleDecision1Destination = @"path/for/new_room.room";
        private const string SampleDecision2Destination = @"path/for/example_room.room";

        private const string DefaultVariable = "TestVariable";
        private const string DefaultVariableValue = "DefaultValue";

        [SetUp]
        public void SetUp()
        {
            _storageSupervisor = Substitute.For<IStorageSupervisor>();
            _room = new RoomSchema {Name = SampleRoomName, Description = SampleRoomDescription};

            _decision1 = CreateDecision(SampleDecision1Description, SampleDecision1Destination);
            _decision2 = CreateDecision(SampleDecision2Description, SampleDecision2Destination);

            _room.Decisions = new List<DecisionSchema>{ _decision1, _decision2 };

            _typedSut = new JsonDaoProvider<StateManager>(_storageSupervisor);
            _sut = _typedSut;

            _lastDeviceWriteResult = "";
            _storageSupervisor.Write(ValidRoomPath, Arg.Do<string>(x => _lastDeviceWriteResult = x));
        }

        private static DecisionSchema CreateDecision(string description, string destination)
        {
            return new DecisionSchema
            {
                Description = description, 
                Destination = destination, 
                VisibilityRequirements = new ExpressionOr{Args = new Dictionary<int, BoolExpandableExpression>()
                {
                    { 0, new ExpressionVariableExists(DefaultVariable) },
                    { 1, new ExpressionTrue() }
                }}
            };
        }

        [Test]
        public void should_be_able_to_serialize_and_deserialize_valid_room_object()
        {
            _sut.WriteRoom(ValidRoomPath, _room);

            _storageSupervisor.Read(ValidRoomPath).Returns(_lastDeviceWriteResult);

            RoomSchema convertedBack = _sut.ReadRoom(ValidRoomPath);
            Assert.That(convertedBack.Name, Is.EqualTo(SampleRoomName));
            Assert.That(convertedBack.Description, Is.EqualTo(SampleRoomDescription));
            AssertDecision(convertedBack, 0, SampleDecision1Description, SampleDecision1Destination);
            AssertDecision(convertedBack, 1, SampleDecision2Description, SampleDecision2Destination);
        }

        [Test]
        public void should_be_able_to_serialize_and_deserialize_state_manager()
        {
            IStateManager stateManager = new StateManager();
            stateManager.SetString(DefaultVariable, DefaultVariableValue);

            _storageSupervisor.Write(ValidManagerPath, Arg.Do<string>(x => _lastDeviceWriteResult = x));
            _sut.WriteStateManager(ValidManagerPath, stateManager);

            _storageSupervisor.Read(ValidManagerPath).Returns(_lastDeviceWriteResult);
            stateManager = _sut.ReadStateManager(ValidManagerPath);

            Assert.That(stateManager.GetString(DefaultVariable), Is.EqualTo(DefaultVariableValue));
        }

        private static void AssertDecision(RoomSchema convertedBack, int index, string description, string destination)
        {
            Assert.That(convertedBack.Decisions[index].Description, Is.EqualTo(description));
            Assert.That(convertedBack.Decisions[index].Destination, Is.EqualTo(destination));
            Assert.That(convertedBack.Decisions[index].VisibilityRequirements.Name, Is.EqualTo(new ExpressionOr().Name));
            Assert.That(convertedBack.Decisions[index].VisibilityRequirements.Args[0].Name, Is.EqualTo(new ExpressionVariableExists().Name));
            Assert.That(convertedBack.Decisions[index].VisibilityRequirements.Args[0].SimpleArgs[0], Is.EqualTo(DefaultVariable));
            Assert.That(convertedBack.Decisions[index].VisibilityRequirements.Args[1].Name, Is.EqualTo(new ExpressionTrue().Name));
        }
    }
}
