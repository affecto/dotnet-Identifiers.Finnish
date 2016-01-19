using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.Identifiers.Finnish.Tests
{
    [TestClass]
    public class InvoiceReferenceNumberTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullReferenceNumber()
        {
            InvoiceReferenceNumber.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NonNumericReferenceNumber()
        {
            InvoiceReferenceNumber.Create("abcdefg");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyReferenceNumber()
        {
            InvoiceReferenceNumber.Create(string.Empty);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooShortReferenceNumber()
        {
            InvoiceReferenceNumber.Create("12");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooLongReferenceNumber()
        {
            InvoiceReferenceNumber.Create("123456789012345678901");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidChecksum()
        {
            InvoiceReferenceNumber.Create("1234567");
        }

        [TestMethod]
        public void TryNullReferenceNumber()
        {
            AssertInvalidValue(null);
        }

        [TestMethod]
        public void TryNonNumericReferenceNumber()
        {
            AssertInvalidValue("abcdefg");
        }

        [TestMethod]
        public void TryEmptyReferenceNumber()
        {
            AssertInvalidValue(string.Empty);
        }

        [TestMethod]
        public void TryTooShortReferenceNumber()
        {
            AssertInvalidValue("12");
            AssertInvalidValue(12);
        }

        [TestMethod]
        public void TryTooLongReferenceNumber()
        {
            AssertInvalidValue("123456789012345678901");
        }

        [TestMethod]
        public void TryInvalidChecksum()
        {
            AssertInvalidValue("1234567");
            AssertInvalidValue(1234567);
        }

        [TestMethod]
        public void ValidReferenceNumber()
        {
            AssertValidValue("1234561");
        }

        [TestMethod]
        public void IntegerReferenceNumber()
        {
            AssertValidValue(1234561);
        }

        [TestMethod]
        public void ValidReferenceNumberWithSpaces()
        {
            AssertValidValue("12345 61");
        }

        [TestMethod]
        public void ValidReferenceNumberBeginningWithSpaces()
        {
            AssertValidValue(" 1234561");
        }

        [TestMethod]
        public void ValidReferenceNumberEndingWithSpaces()
        {
            AssertValidValue("1234561 ");
        }


        [TestMethod]
        public void ZeroPadded()
        {
            AssertValidValue("0000000001234561");
        }

        [TestMethod]
        public void MaximumWidth()
        {
            AssertValidValue("12345678901234567894");
        }

        [TestMethod]
        public void MinWidth()
        {
            AssertValidValue("1232");
        }       

        [TestMethod]
        public void ToStringIsContentWithoutSpaces()
        {
            const string reference = "12345 61";
            const string expected = "1234561";
            InvoiceReferenceNumber sut = InvoiceReferenceNumber.Create(reference);
            Assert.AreEqual(expected, sut.ToString());
        }

        [TestMethod]
        public void ToStringIsContentWithoutZeroPadding()
        {
            const string reference = "00012 34561";
            const string expected = "1234561";
            InvoiceReferenceNumber sut = InvoiceReferenceNumber.Create(reference);
            Assert.AreEqual(expected, sut.ToString());
        }        
        
        [TestMethod]
        public void EqualsComparesCorrectly()
        {
            const string first = "00012 34561";
            const string second = "1234561";
            InvoiceReferenceNumber sut = InvoiceReferenceNumber.Create(first);
            InvoiceReferenceNumber other = InvoiceReferenceNumber.Create(second);

            Assert.AreEqual(other, sut);
        }

        [TestMethod]
        public void InvoiceReferenceNumberIsNotEqualWithDifferentTypesOfObjects()
        {
            InvoiceReferenceNumber sut = InvoiceReferenceNumber.Create("1234561");
            Assert.AreNotEqual(DateTime.Today, sut);
        }
        
        private static void AssertInvalidValue(string value)
        {
            AssertFailedTryCreate(value);
            AssertDissatisfiedSpecification(value);
        }

        private static void AssertInvalidValue(int value)
        {
            AssertFailedTryCreate(value);
            AssertDissatisfiedSpecification(value);
        }

        private static void AssertValidValue(string value)
        {
            InvoiceReferenceNumber.Create(value);
            var specification = new InvoiceReferenceNumberSpecification();
            Assert.IsTrue(specification.IsSatisfiedBy(value));
        }

        private void AssertValidValue(int value)
        {
            InvoiceReferenceNumber.Create(value);
            var specification = new InvoiceReferenceNumberSpecification();
            Assert.IsTrue(specification.IsSatisfiedBy(value));
        }

        private static void AssertDissatisfiedSpecification(string value)
        {
            var specification = new InvoiceReferenceNumberSpecification();
            Assert.IsFalse(specification.IsSatisfiedBy(value));
            Assert.IsFalse(string.IsNullOrWhiteSpace(specification.GetReasonsForDissatisfactionSeparatedWithNewLine()));
        }

        private static void AssertFailedTryCreate(string value)
        {
            string failureReason;
            InvoiceReferenceNumber result;
            Assert.IsFalse(InvoiceReferenceNumber.TryCreate(value, out result, out failureReason));
            Assert.IsNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(failureReason));
        }

        private static void AssertDissatisfiedSpecification(int value)
        {
            var specification = new InvoiceReferenceNumberSpecification();
            Assert.IsFalse(specification.IsSatisfiedBy(value));
            Assert.IsFalse(string.IsNullOrWhiteSpace(specification.GetReasonsForDissatisfactionSeparatedWithNewLine()));
        }

        private static void AssertFailedTryCreate(int value)
        {
            string failureReason;
            InvoiceReferenceNumber result;
            Assert.IsFalse(InvoiceReferenceNumber.TryCreate(value, out result, out failureReason));
            Assert.IsNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(failureReason));
        }
    }
}