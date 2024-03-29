﻿using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using PutridParrot.Randomizer;
using Telerik.JustMock;

namespace Tests.PutridParrot.Randomizer
{
    [ExcludeFromCodeCoverage]
    public class RandomizerExtensionTests
    {
        [Test]
        public void NextBool_IfNextInt0_ExpectFalse()
        {
            var mock = Mock.Create<IRandomizer>();

            Mock.Arrange(() => mock.NextInt(0, 2)).Returns(0);

            Assert.IsFalse(mock.NextBool());
        }

        [Test]
        public void NextBool_IfNextInt1_ExpectTrue()
        {
            var mock = Mock.Create<IRandomizer>();

            Mock.Arrange(() => mock.NextInt(0, 2)).Returns(1);

            Assert.IsTrue(mock.NextBool());
        }

        [Test]
        public void NextDateTime_WithKnownValue_ExpectCorrespondingDateTime()
        {
            var mock = Mock.Create<IRandomizer>();

            var startDate = DateTime.Now.Date;
            var endDate = startDate.AddDays(5);

            Mock.Arrange(() => mock.NextInt(0, 5)).Returns(2);
            Assert.AreEqual(startDate.AddDays(2).Date, mock.NextDateTime(startDate, endDate));
        }

        [Test]
        public void NextItem_WithKnownValue_ExpectCorrespondingItem()
        {
            var mock = Mock.Create<IRandomizer>();

            var array = new[]
            {
                "A", "B", "C"
            };

            Mock.Arrange(() => mock.NextInt(0, 3)).Returns(1);

            Assert.AreEqual("B", mock.NextItem(array));
        }

        [Test]
        public void NextItem_WithKnownValueAndMaxIndexInRange_ExpectCorrespondingItem()
        {
            var mock = Mock.Create<IRandomizer>();

            var array = new[]
            {
                "A", "B", "C"
            };

            Mock.Arrange(() => mock.NextInt(0, 3)).Returns(0);

            Assert.AreEqual("A", mock.NextItem(array, 2));
        }

        [Test]
        public void NextItem_WithKnownValueUsingMinIndexAndMaxIndexInRange_ExpectCorrespondingItem()
        {
            var mock = Mock.Create<IRandomizer>();

            var array = new[]
            {
                "A", "B", "C"
            };

            Mock.Arrange(() => mock.NextInt(0, 3)).Returns(2);

            Assert.AreEqual("C", mock.NextItem(array, 0, 3));
        }

        [Test]
        public void NextInt_ExpectMockValue()
        {
            var mock = Mock.Create<IRandomizer>();

            Mock.Arrange(() => mock.NextInt(0, int.MaxValue)).Returns(2);

            Assert.AreEqual(2, mock.NextInt());
        }

        [Test]
        public void NextLong_ExpectMockValue()
        {
            var mock = Mock.Create<IRandomizer>();

            Mock.Arrange(() => mock.NextDouble(0, 1)).Returns(0.001);

            Assert.AreEqual(9223372036854776, mock.NextLong());
        }

        [Test]
        public void NextLong_WithMax()
        {
            var mock = Mock.Create<IRandomizer>();

            Mock.Arrange(() => mock.NextDouble(0, 1)).Returns(0.1);

            Assert.AreEqual(10, mock.NextLong(100));
        }

        [Test]
        public void NextLong_WithMinAndMax()
        {
            var mock = Mock.Create<IRandomizer>();

            Mock.Arrange(() => mock.NextDouble(0, 1)).Returns(0.1);

            Assert.AreEqual(11, mock.NextLong(1, 100));
        }



        [Test]
        public void NextDouble_ExpectMockValue()
        {
            var mock = Mock.Create<IRandomizer>();

            Mock.Arrange(() => mock.NextDouble(0.0, 1.0)).Returns(0.45);

            Assert.AreEqual(0.45, mock.NextDouble());
        }

        [Test]
        public void NextDoubleWithMax_ExpectMockValue()
        {
            var mock = Mock.Create<IRandomizer>();

            Mock.Arrange(() => mock.NextDouble(0, 5)).Returns(3.91);

            Assert.AreEqual(3.91, mock.NextDouble(5));
        }

        [Test]
        public void NextIntWithRange_ExpectMockValue()
        {
            var mock = Mock.Create<IRandomizer>();

            Mock.Arrange(() => mock.NextInt(1, 5)).Returns(2);

            Assert.AreEqual(2, mock.NextInt(1..5));
        }

        [Test]
        public void Shuffle_WithKnownRndValues_ExpectArrayReversed()
        {
            var mock = Mock.Create<IRandomizer>();

            Mock.Arrange(() => mock.NextInt(0, 3)).Returns(0);
            Mock.Arrange(() => mock.NextInt(0, 2)).Returns(1);
            Mock.Arrange(() => mock.NextInt(0, 1)).Returns(2);

            Assert.AreEqual(new[] { 3, 2, 1 }, mock.Shuffle(new[] { 1, 2, 3 }));
        }

        private enum TestEnum
        {
            First,
            Second,
            Third
        }

        [Test]
        [Repeat(100)]
        public void NextItem_WithEnum()
        {
            var randomizer = new PseudoRandomizer(0);
            Assert.AreEqual(TestEnum.Third, randomizer.NextEnum<TestEnum>());
        }

        [Test]
        [Repeat(100)]
        public void NextItem_WithParams()
        {
            var randomizer = new PseudoRandomizer(0);

            Assert.AreEqual(4, randomizer.NextItem(1, 2, 3, 4, 5));
        }

        [Test]
        public void NextString_WithCharSet()
        {
            var mock = Mock.Create<IRandomizer>();

            Mock.Arrange(() => mock.NextInt(0, 6)).Returns(2).InSequence();
            Mock.Arrange(() => mock.NextInt(0, 6)).Returns(4).InSequence();
            Mock.Arrange(() => mock.NextInt(0, 6)).Returns(5).InSequence();

            Assert.AreEqual("c23", mock.NextString(3, "abc123"));
        }

        [Test]
        public void NextBool_WithProbability()
        {
            var mock = Mock.Create<IRandomizer>();

            Mock.Arrange(() => mock.NextInt(0, 2)).Returns(0);

            Assert.IsFalse(mock.NextBool(0.3));
        }

        [Test]
        public void NextBool_WithProbability_Impossibility()
        {
            var randomizer = new PseudoRandomizer();

            Assert.IsFalse(randomizer.NextBool(0.0));
        }

        [Test]
        public void NextBool_WithProbability_Certainty()
        {
            var randomizer = new PseudoRandomizer();

            Assert.IsTrue(randomizer.NextBool(1.0));
        }
    }
}
