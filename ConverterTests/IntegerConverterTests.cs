using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Converter.Tests
{
    [TestFixture]
    public class IntegerConverterTests
    {
        Dictionary<int, string> conversions;

        [SetUp]
        public void Init()
        {
            /*
                2:Doo
                3:Fizz
                5:Buzz
                7:Wap
            */
            conversions = new Dictionary<int, string>();
            conversions.Add(2, "Doo");
            conversions.Add(3, "Fizz");
            conversions.Add(5, "Buzz");
            conversions.Add(7, "Wap");
        }

        [Test]
        //[ExpectedException(typeof(ArgumentException))] -- for older versions of NUnit
        public void ConvertNumbersWithLowerBoundGreaterThanUpperBound()
        {
            // for the older NUnit versions with expected exception attribute
            //List<string> results = IntegerConverter.convert(10, 5, conversions).ToList();

            Assert.Throws<ArgumentException>(() => IntegerConverter.convert(10, 5, conversions).ToList());
        }

        [Test]
        public void ConvertNumbersWithEqualLowerAndUpperBound()
        {
            List<string> results = IntegerConverter.convert(5, 5, conversions).ToList();

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Buzz", results[0]);

            results.Clear();

            Assert.AreEqual(0, results.Count);

            results = IntegerConverter.convert(1, 1, conversions).ToList();

            Assert.AreEqual(1, results.Count);

            Assert.AreEqual("1", results[0]);
        }

        [Test]
        public void ConverNumbersWithNegativeIntegers()
        {
            List<string> results = IntegerConverter.convert(-3, -1, conversions).ToList();

            Assert.AreEqual(3, results.Count);

            int i = -3;

            foreach (string result in results)
            {
                if (conversions.Keys.Contains(Math.Abs(i)))
                {
                    Assert.AreEqual(conversions[Math.Abs(i)], result);
                }
                else
                {
                    Assert.AreEqual(i.ToString(), result);
                }

                i++;
            }
        }

        [Test]
        public void ConvertNumbersWithoutConversions()
        {
            List<string> results = IntegerConverter.convert(1, 5, null).ToList();

            int i = 1;

            foreach (string result in results)
            {
                Assert.AreEqual(i.ToString(), result);
                i++;
            }
        }

        [Test]
        public void ConvertNumbersWithSingleConversion()
        {
            List<string> results = IntegerConverter.convert(3, 5, conversions).ToList();

            Assert.AreEqual(3, results.Count);
            Assert.AreEqual("Fizz", results[0]);
            Assert.AreEqual("Doo", results[1]);
            Assert.AreEqual("Buzz", results[2]);
        }

        [Test]
        public void ConvertNumbersWithMultipleConversion()
        {
            List<string> results = IntegerConverter.convert(1, 7, conversions).ToList();

            Assert.AreEqual(7, results.Count);

            // 1
            Assert.AreEqual("1", results[0]);
            // 2
            Assert.AreEqual("Doo", results[1]);
            // 3
            Assert.AreEqual("Fizz", results[2]);
            // 4
            Assert.AreEqual("Doo", results[3]);
            // 5
            Assert.AreEqual("Buzz", results[4]);
            // 6
            Assert.AreEqual("DooFizz", results[5]);
            // 7
            Assert.AreEqual("Wap", results[6]);
        }


        // Test written during zoom interview using MSTest
        //[TestMethod()]
        //public void convertTest()
        //{
        //    List<string> results = Converter.IntegerConverter.convert(15, 15).ToList();

        //    Assert.AreEqual(1, results.Count);
        //    Assert.AreEqual("FizzBuzz", results[0]);
        //}
    }
}