using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Logic;
using DataLayer.Schema;
using DataLayer.Schema.Variable.Mutable;
using DataLayer.Storage;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    class SerializationDeserializationTests
    {
        public const string VariableName = "MyVariable";

        public string SerializeObject(BoolExpandableExpression obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public BoolExpandableExpression DeserializeObject(string serialized)
        {
            return JsonConvert.DeserializeObject<BoolExpandableExpression>(serialized);
        }

        public T SerializeAndDeserialize<T>(T obj) 
            where T : BoolExpandableExpression
        {
            var doubleConverted = DeserializeObject(SerializeObject(obj)) as T;

            Assert.That(doubleConverted, Is.Not.Null);
            // ReSharper disable once PossibleNullReferenceException
            Assert.That(obj.Name, Is.EqualTo(doubleConverted.Name));

            return doubleConverted;
        }

        [Test]
        public void expression_assign_should_be_serializable()
        {
            var value = "5";
            var sut = new ExpressionAssign(VariableName, value);
            var result = SerializeAndDeserialize(sut);

            Assert.That(result.VariableName, Is.EqualTo(sut.VariableName));
            Assert.That(result.Value, Is.EqualTo(sut.Value));
        }
    }
}
