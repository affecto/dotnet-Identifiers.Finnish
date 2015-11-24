using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.Identifiers.Finnish.Tests
{
    [TestClass]
    public class MunicipalityCodeTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullMunicipalityId()
        {
            MunicipalityCode.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyMunicipalityId()
        {
            MunicipalityCode.Create(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NonNumericMunicipalityId()
        {
            MunicipalityCode.Create("abc");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooLongMunicipalityId()
        {
            MunicipalityCode.Create("0833");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooLongNumericMunicipalityId()
        {
            MunicipalityCode.Create(8301);
        }

        [TestMethod]
        public void MunicipalityIdIsLeftPaddedWithZeros()
        {
            MunicipalityCode sut = MunicipalityCode.Create("83");
            
            Assert.AreEqual("083", sut.ToString());
        }

        [TestMethod]
        public void NumericMunicipalityIdIsLeftPaddedWithZeros()
        {
            MunicipalityCode sut = MunicipalityCode.Create(83);

            Assert.AreEqual("083", sut.ToString());
        }

        [TestMethod]
        public void TryNullMunicipalityId()
        {
            AssertInvalidValue(null);
        }

        [TestMethod]
        public void TryEmptyMunicipalityId()
        {
            AssertInvalidValue(string.Empty);
        }

        [TestMethod]
        public void TryNonNumericMunicipalityId()
        {
            AssertInvalidValue("abc");
        }

        [TestMethod]
        public void TryTooLongMunicipalityId()
        {
            AssertInvalidValue("0833");
        }

        [TestMethod]
        public void TryTooLongNumericMunicipalityId()
        {
            AssertInvalidValue(8301);
        }

        [TestMethod]
        public void EqaulsComparesMunicipalityIdContent()
        {
            const string municipalityId = "123";
            MunicipalityCode sut = MunicipalityCode.Create(municipalityId);
            MunicipalityCode other = MunicipalityCode.Create(municipalityId);
            
            Assert.AreEqual(other, sut);
        }

        [TestMethod]
        public void MunicipalityIdIsNotEqualWithDifferentTypesOfObjects()
        {
            MunicipalityCode sut = MunicipalityCode.Create("83");

            Assert.AreNotEqual(DateTime.Today, sut);
        }

        private static void AssertInvalidValue(string value)
        {
            AssertFailedTryCreate(value);
            AssertDissatisfiedSpecification(value);
        }

        private static void AssertDissatisfiedSpecification(string value)
        {
            var specification = new MunicipalityCodeSpecification();
            Assert.IsFalse(specification.IsSatisfiedBy(value));
            Assert.IsFalse(string.IsNullOrWhiteSpace(specification.GetReasonsForDissatisfactionSeparatedWithNewLine()));
        }

        private static void AssertFailedTryCreate(string value)
        {
            string failureReason;
            MunicipalityCode result;
            Assert.IsFalse(MunicipalityCode.TryCreate(value, out result, out failureReason));
            Assert.IsNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(failureReason));
        }

        private static void AssertInvalidValue(int value)
        {
            AssertFailedTryCreate(value);
        }

        private static void AssertFailedTryCreate(int value)
        {
            string failureReason;
            MunicipalityCode result;
            Assert.IsFalse(MunicipalityCode.TryCreate(value, out result, out failureReason));
            Assert.IsNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(failureReason));
        }
    }
}