using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Data;
using DataLayer.Logic;
using DataLayer.Schema;
using DataLayer.Schema.Variable;
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

        [Test]
        public void expression_assign_should_be_serializable()
        {
            var value = "5";
            var sut = new ExpressionAssign(VariableName, value);
            var result = BoolExpandableExpression.Convert<ExpressionAssign>(
                SerializeAndDeserialize(sut));

            Assert.That(result.VariableName, Is.EqualTo(sut.VariableName));
            Assert.That(result.Value, Is.EqualTo(sut.Value));
        }

        [Test]
        public void expression_float_modify_should_be_serializable()
        {
            const string value = "5";
            const string variableName = "result";
            const string paramName = "a";

            var sut = new ExpressionFloatModify
            {
                Left = new ExprParam
                {
                    ParamSource = ExprParam.Source.WorldState,
                    Value = paramName
                },
                Right = new ExprParam
                {
                    ParamSource = ExprParam.Source.Constant,
                    Value = value
                },
                OperType = ModifyOperType.Add,
                VariableName = variableName
            };
            var result = BoolExpandableExpression.Convert<ExpressionFloatModify>(
                SerializeAndDeserialize(sut));

            Assert.That(result.VariableName, Is.EqualTo(sut.VariableName));
            Assert.That(result.Left.ParamSource, Is.EqualTo(ExprParam.Source.WorldState));
            Assert.That(result.Left.Value, Is.EqualTo(paramName));
            Assert.That(result.Right.ParamSource, Is.EqualTo(ExprParam.Source.Constant));
            Assert.That(result.Right.Value, Is.EqualTo(value));
            Assert.That(result.OperType, Is.EqualTo(ModifyOperType.Add));
        }

        [Test]
        public void expression_int_modify_should_be_serializable()
        {
            const string value = "5";
            const string variableName = "result";
            const string paramName = "a";

            var sut = new ExpressionIntModify
            {
                Left = new ExprParam
                {
                    ParamSource = ExprParam.Source.WorldState,
                    Value = paramName
                },
                Right = new ExprParam
                {
                    ParamSource = ExprParam.Source.Constant,
                    Value = value
                },
                OperType = ModifyOperType.Divide,
                VariableName = variableName
            };
            var result = BoolExpandableExpression.Convert<ExpressionIntModify>(
                SerializeAndDeserialize(sut));

            Assert.That(result.VariableName, Is.EqualTo(sut.VariableName));
            Assert.That(result.Left.ParamSource, Is.EqualTo(ExprParam.Source.WorldState));
            Assert.That(result.Left.Value, Is.EqualTo(paramName));
            Assert.That(result.Right.ParamSource, Is.EqualTo(ExprParam.Source.Constant));
            Assert.That(result.Right.Value, Is.EqualTo(value));
            Assert.That(result.OperType, Is.EqualTo(ModifyOperType.Divide));
        }

        [Test]
        public void expression_float_check_should_be_serializable()
        {
            const string variableName = "result";
            const float value = 5f;
            const CheckOperType checkOperType = CheckOperType.Equal;

            var sut = new ExpressionFloatCheck
            {
                VariableName = variableName,
                Value = value,
                OperType = checkOperType
            };
            var result = BoolExpandableExpression.Convert<ExpressionFloatCheck>(
                SerializeAndDeserialize(sut));

            Assert.That(result.VariableName, Is.EqualTo(sut.VariableName));
            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.OperType, Is.EqualTo(checkOperType));
        }

        [Test]
        public void expression_int_check_should_be_serializable()
        {
            const string variableName = "result";
            const int value = 2;
            const CheckOperType checkOperType = CheckOperType.LesserEqual;

            var sut = new ExpressionIntCheck
            {
                VariableName = variableName,
                Value = value,
                OperType = checkOperType
            };
            var result = BoolExpandableExpression.Convert<ExpressionIntCheck>(
                SerializeAndDeserialize(sut));

            Assert.That(result.VariableName, Is.EqualTo(sut.VariableName));
            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.OperType, Is.EqualTo(checkOperType));
        }

        [Test]
        public void expression_string_check_should_be_serializable()
        {
            const string variableName = "result";
            const string value = "ogre";
            const CheckOperType checkOperType = CheckOperType.Equal;

            var sut = new ExpressionStringCheck
            {
                VariableName = variableName,
                Value = value,
                OperType = checkOperType
            };
            var result = BoolExpandableExpression.Convert<ExpressionStringCheck>(
                SerializeAndDeserialize(sut));

            Assert.That(result.VariableName, Is.EqualTo(sut.VariableName));
            Assert.That(result.Value, Is.EqualTo(value));
            Assert.That(result.OperType, Is.EqualTo(checkOperType));
        }

        [Test]
        public void expression_variable_exists_should_be_serializable()
        {
            const string variableName = "result";

            var sut = new ExpressionVariableExists
            {
                VariableName = variableName
            };
            var result = BoolExpandableExpression.Convert<ExpressionVariableExists>(
                SerializeAndDeserialize(sut));

            Assert.That(result.VariableName, Is.EqualTo(sut.VariableName));
        }
    }
}
