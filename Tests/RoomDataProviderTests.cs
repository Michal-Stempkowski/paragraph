using System;
using DataLayer.Exceptions;
using DataLayer.Logic;
using DataLayer.Room;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    class RoomDataProviderTests
    {
        private const string NotExistingRoomFile = @"path/to/not_existing_room.room";
        private IRoomDataProvider _sut;
        private IStateManager _stateManager;
        private IJsonObjectProvider _objectProvider;

        [SetUp]
        public void SetUp()
        {
            _objectProvider = Substitute.For<IJsonObjectProvider>();
            _sut = new RoomDataProvider(_objectProvider);
            _stateManager = Substitute.For<IStateManager>();


        }

        [Test]
        [ExpectedException(typeof(FileDoesNotExistException))]
        public void loading_not_existing_room_should_cause_load_exception()
        {
            _objectProvider.When(x => x.ReadRoom(NotExistingRoomFile)).Do(x =>
            {
                throw new FileDoesNotExistException();
            });
            _sut.LoadRoom(NotExistingRoomFile, _stateManager);
        }
    }
}
