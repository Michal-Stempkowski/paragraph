using DataLayer.Exceptions;
using DataLayer.Logic;
using DataLayer.Room;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    class EntityDataProviderTests
    {
        private const string InvalidPath = @"path/to/not/existing/file";
        private const string RoomPath = @"path/to/room.room";
        private const string NotExistingRoomPath = @"path/to/not_existing_room.room";
        private IEntityDataProvider _sut;
        private IStateManager _stateManager;
        private IRoomDataProvider _roomDataProvider;

        [SetUp]
        public void SetUp()
        {
            _roomDataProvider = Substitute.For<IRoomDataProvider>();
            _sut = new EntityDataProvider(_roomDataProvider);
            _stateManager = Substitute.For<IStateManager>();
        }

        [Test]
        public void by_default_current_room_should_return_null()
        {
            Assert.That(_sut.CurrentEntity, Is.EqualTo(null));
        }

        [Test]
        public void if_file_not_avalable_transition_should_return_false()
        {
            Assert.That(_sut.PerformEntityTransition(InvalidPath, _stateManager), Is.False);
        }

        [Test]
        public void room_should_be_served_by_room_provider()
        {
            IEntity newEntity = Substitute.For<IEntity>();
            _roomDataProvider.LoadRoom(RoomPath, _stateManager).Returns(newEntity);
            Assert.That(_sut.CurrentEntity, Is.EqualTo(null));

            Assert.That(_sut.PerformEntityTransition(RoomPath, _stateManager), Is.True);

            Assert.That(_sut.CurrentEntity, Is.EqualTo(newEntity));
        }

        [Test]
        public void on_room_load_fail_entity_transition_should_return_false()
        {
            IEntity newEntity = Substitute.For<IEntity>();
            _roomDataProvider.When(x => x.LoadRoom(NotExistingRoomPath, _stateManager)).Do(x =>
            {
                throw new LoadFailedException();
            });
            _roomDataProvider.LoadRoom(RoomPath, _stateManager).Returns(newEntity);
            Assert.That(_sut.CurrentEntity, Is.EqualTo(null));

            Assert.That(_sut.PerformEntityTransition(NotExistingRoomPath, _stateManager), Is.False);

            Assert.That(_sut.CurrentEntity, Is.EqualTo(null));

            Assert.That(_sut.PerformEntityTransition(RoomPath, _stateManager), Is.True);

            Assert.That(_sut.CurrentEntity, Is.EqualTo(newEntity));
        }
    }
}
