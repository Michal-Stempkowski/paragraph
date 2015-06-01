using System;
using System.Collections.Generic;
using DataLayer.Core;
using DataLayer.Data;
using DataLayer.Exceptions;
using DataLayer.Logic;
using DataLayer.Room;
using DataLayer.Schema;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    class RoomDataProviderTests
    {
        private const string SampleRoomFile = @"path/to/sample_room.room";
        private const string NotExistingRoomFile = @"path/to/not_existing_room.room";
        private IRoomDataProvider _sut;
        private IStateManager _stateManager;
        private IDaoProvider _objectProvider;

        private readonly RoomSchema _roomSchema = new RoomSchema
        {
            Name = "RoomName",
            Description = "RoomDescription", 
            Decisions = new List<DecisionSchema>()
        };

        private ICoreTranslator _translator;

        [SetUp]
        public void SetUp()
        {
            _objectProvider = Substitute.For<IDaoProvider>();
            _translator = Substitute.For<ICoreTranslator>();
            _sut = new RoomDataProvider(_objectProvider, _translator);
            _stateManager = Substitute.For<IStateManager>();

            _objectProvider.When(x => x.ReadRoom(NotExistingRoomFile)).Do(x =>
            {
                throw new FileDoesNotExistException();
            });
        }

        [Test]
        [ExpectedException(typeof(FileDoesNotExistException))]
        public void loading_not_existing_room_should_cause_load_exception()
        {
            _sut.LoadRoom(NotExistingRoomFile, _stateManager);
        }

        [Test]
        public void proper_load_should_be_possible()
        {
            _objectProvider.ReadRoom(SampleRoomFile).Returns(_roomSchema);
            
        }
    }
}
