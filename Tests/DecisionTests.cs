﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    class DecisionTests
    {
        private readonly IDecision _sut;

        public DecisionTests()
        {
            _sut = new Decision();
        }

        [Test]
        public void on_default_should_be_visible()
        {
            Assert.That(_sut.IsVisible, Is.True);
        }

        [Test]
        public void visibility_should_be_mutable()
        {
            _sut.IsVisible = false;
            Assert.That(_sut.IsVisible, Is.False);

            _sut.IsVisible = true;
            Assert.That(_sut.IsVisible, Is.True);
        }

        [Test]
        public void description_should_be_mutable()
        {
            Assert.That(_sut.Description, Is.EqualTo(""));

            var description = "This is description";

            _sut.Description = description;
            Assert.That(_sut.Description, Is.EqualTo(description));
        }

        [Test]
        public void destination_should_be_mutable()
        {
            Assert.That(_sut.Destination, Is.EqualTo(""));

            var destination = "AMN//NORTHERN//TAVERN//GUILDMASTER_TALK00";

            _sut.Destination = destination;
            Assert.That(_sut.Destination, Is.EqualTo(destination));
        }

        [Test]
        public void effect_should_be_mutable()
        {
            Assert.That(_sut.Effect, Is.EqualTo(_sut.NoEffect));

            var result = "";

            Assert.That(result, Is.Empty);

            var expectedResult = "expected";
            _sut.Effect = x => result = expectedResult;

            var stateManager = Substitute.For<IStateManager>();
            _sut.Effect(stateManager);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
