using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.Identifiers.Finnish.Tests
{
    [TestClass]
    public class InvoiceReferenceNumberTests
    {
        // TODO: Test TryCreate

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NulViitenumero()
        {
            InvoiceReferenceNumber.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NonNumericViitenumero()
        {
            InvoiceReferenceNumber.Create("abcdefg");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyViitenumero()
        {
            InvoiceReferenceNumber.Create(string.Empty);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooShortViitenumero()
        {
            InvoiceReferenceNumber.Create("12");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooLongViitenumero()
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
        public void ValidChecksum()
        {
            InvoiceReferenceNumber.Create("1234561");
        }

        [TestMethod]
        public void ValidChecksumWithSpaces()
        {
            InvoiceReferenceNumber.Create("12345 61");
        }

        [TestMethod]
        public void ZeroPadded()
        {
            InvoiceReferenceNumber.Create("0000000001234561");
        }

        [TestMethod]
        public void MaximumWidth()
        {
            InvoiceReferenceNumber.Create("12345678901234567894");
        }

        [TestMethod]
        public void MinWidth()
        {
            InvoiceReferenceNumber.Create("1232");
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
        public void BusinessIdIsNotEqualWithDifferentTypesOfObjects()
        {
            InvoiceReferenceNumber sut = InvoiceReferenceNumber.Create("1234561");
            Assert.AreNotEqual(DateTime.Today, sut);
        }
        
        private static void AssertFailedTryCreate(string value)
        {
            string failureReason;
            InvoiceReferenceNumber result;
            Assert.IsFalse(InvoiceReferenceNumber.TryCreate(value, out result, out failureReason));
            Assert.IsNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(failureReason));
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