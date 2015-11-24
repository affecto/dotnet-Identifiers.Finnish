using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.Identifiers.Finnish.Tests
{
    [TestClass]
    public class BusinessIdentifierTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullBusinessId()
        {
            BusinessIdentifier.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyBusinessId()
        {
            BusinessIdentifier.Create(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooShortBusinessId()
        {
            BusinessIdentifier.Create("123456-7");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooLongBusinessId()
        {
            BusinessIdentifier.Create("12345678-9");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MissingLineInBusinessId()
        {
            BusinessIdentifier.Create("12345678");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MissingChecksumDigitInBusinessId()
        {
            BusinessIdentifier.Create("1234567-");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IncorrectChecksumDigitInBusinessId()
        {
            BusinessIdentifier.Create("1234567-0");
        }

        [TestMethod]
        public void TryNullBusinessId()
        {
            AssertInvalidValue(null);
        }

        [TestMethod]
        public void TryEmptyBusinessId()
        {
            AssertInvalidValue(string.Empty);
        }

        [TestMethod]
        public void TryTooShortBusinessId()
        {
            AssertInvalidValue("123456-7");
        }

        [TestMethod]
        public void TryTooLongBusinessId()
        {
            AssertInvalidValue("12345678-9");
        }

        [TestMethod]
        public void TryMissingLineInBusinessId()
        {
            AssertInvalidValue("12345678");
        }

        [TestMethod]
        public void TryMissingChecksumDigitInBusinessId()
        {
            AssertInvalidValue("1234567-");
        }

        [TestMethod]
        public void TryIncorrectChecksumDigitInBusinessId()
        {
            AssertInvalidValue("1234567-0");
        }

        [TestMethod]
        public void MaximumOrderNumberInBusinessId()
        {
            BusinessIdentifier.Create("9999999-2");
        }

        [TestMethod]
        public void MinimumOrderNumberInBusinessId()
        {
            BusinessIdentifier.Create("0000001-9");
        }

        [TestMethod]
        public void ToStringIsContent()
        {
            const string businessId = "1069622-4";
            BusinessIdentifier sut = BusinessIdentifier.Create(businessId);

            Assert.AreEqual(businessId, sut.ToString());
        }

        [TestMethod]
        public void EqualsComparesContent()
        {
            const string businessId = "1069622-4";
            BusinessIdentifier sut = BusinessIdentifier.Create(businessId);
            BusinessIdentifier other = BusinessIdentifier.Create(businessId);

            Assert.AreEqual(other, sut);
        }

        [TestMethod]
        public void BusinessIdIsNotEqualWithDifferentTypesOfObjects()
        {
            BusinessIdentifier sut = BusinessIdentifier.Create("1069622-4");

            Assert.AreNotEqual(DateTime.Today, sut);
        }

        private static void AssertInvalidValue(string value)
        {
            AssertFailedTryCreate(value);
            AssertDissatisfiedSpecification(value);
        }

        private static void AssertDissatisfiedSpecification(string value)
        {
            var specification = new BusinessIdentifierSpecification();
            Assert.IsFalse(specification.IsSatisfiedBy(value));
            Assert.IsFalse(string.IsNullOrWhiteSpace(specification.GetReasonsForDissatisfactionSeparatedWithNewLine()));
        }

        private static void AssertFailedTryCreate(string value)
        {
            string failureReason;
            BusinessIdentifier result;
            Assert.IsFalse(BusinessIdentifier.TryCreate(value, out result, out failureReason));
            Assert.IsNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(failureReason));
        }
    }
}