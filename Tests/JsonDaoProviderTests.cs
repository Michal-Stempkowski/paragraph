using System.Collections.Generic;
using DataLayer.Data;
using DataLayer.Schema;
using DataLayer.Storage;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    class JsonDaoProviderTests
    {
        private const string ValidRoomPath = @"path/for/new_room.room";
        private IDaoProvider _sut;
        private JsonDaoProvider _typedSut;
        private RoomSchema _room;
        private IStorageSupervisor _storageSupervisor;
        private DecisionSchema _decision1;
        private DecisionSchema _decision2;
        private const string SampleRoomName = "SampleRoomName";
        private const string SampleRoomDescription = "This is a sample description.";

        private const string SampleDecision1Description = "Decision no 1";
        private const string SampleDecision2Description = "Decision no 2";

        [SetUp]
        public void SetUp()
        {
            _storageSupervisor = Substitute.For<IStorageSupervisor>();
            _room = new RoomSchema();
            _room.Name = SampleRoomName;
            _room.Description = SampleRoomDescription;

            _decision1 = new DecisionSchema();
            _decision1.Description = SampleDecision1Description;
            _decision2 = new DecisionSchema();
            _decision2.Description = SampleDecision2Description;

            _room.Decisions = new List<DecisionSchema>{ _decision1, _decision2 };

            _typedSut = new JsonDaoProvider(_storageSupervisor);
            _sut = _typedSut;
        }

        [Test]
        public void should_be_able_to_serialize_and_deserialize_valid_room_object()
        {
            string result = "";
            _storageSupervisor.Write(ValidRoomPath, Arg.Do<string>(x => result = x));
            _sut.WriteRoom(ValidRoomPath, _room);

            _storageSupervisor.Read(ValidRoomPath).Returns(result);

            RoomSchema convertedBack = _sut.ReadRoom(ValidRoomPath);
            Assert.That(convertedBack.Name, Is.EqualTo(SampleRoomName));
            Assert.That(convertedBack.Description, Is.EqualTo(SampleRoomDescription));
            Assert.That(convertedBack.Decisions[0].Description, Is.EqualTo(SampleDecision1Description));
            Assert.That(convertedBack.Decisions[1].Description, Is.EqualTo(SampleDecision2Description));
        }
    }
}
