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
            return JsonConvert.SerializeObject(obj.DeepCopy());
        }

        public BoolExpandableExpression DeserializeObject(string serialized)
        {
            return JsonConvert.DeserializeObject<BoolExpandableExpression>(serialized);
        }

        public BoolExpandableExpression SerializeAndDeserialize<T>(T obj) 
            where T : BoolExpandableExpression
        {
            var doubleConverted = DeserializeObject(SerializeObject(obj as BoolExpandableExpression));

            Assert.That(doubleConverted, Is.Not.Null);
            // ReSharper disable once PossibleNullReferenceException
            Assert.That(obj.Name, Is.EqualTo(doubleConverted.Name));

            return doubleConverted;
        }

//        [Test]
//        public void int_string_dictionary_should_be_serializable()
//        {
//            var test = new Dictionary<int, string>();
//            test.Add(0, "trololo");
//            test.Add(1, "lol");
//
//            var doubleConverted = JsonConvert.DeserializeObject(
//                JsonConvert.SerializeObject(test));
//
//            Assert.That(doubleConverted, Contains.Item(0));
//            Assert.That(doubleConverted, Contains.Item(1));
//        }

        [Test]
        public void expression_assign_should_be_serializable()
        {
            var value = "5";
            var sut = new ExpressionAssign(VariableName, value);
            var result = ExpressionAssign.Convert(SerializeAndDeserialize(sut));

            Assert.That(result.VariableName, Is.EqualTo(sut.VariableName));
            Assert.That(result.Value, Is.EqualTo(sut.Value));
        }
    }
}
