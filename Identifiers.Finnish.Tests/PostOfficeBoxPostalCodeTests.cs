using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.Identifiers.Finnish.Tests
{
    [TestClass]
    public class PostOfficeBoxPostalCodeTests
    {
        private const string ValidPostalCode = "20615";

        private PostOfficeBoxPostalCode sut;
        
        [TestMethod]
        public void FiveDigitPostalCodeThatDoesntEndWithZero()
        {
            sut = PostOfficeBoxPostalCode.Create(ValidPostalCode);

            Assert.AreEqual(ValidPostalCode, sut.ToString());
        }

        [TestMethod]
        public void TryFiveDigitPostalCodeThatDoesntEndWithZero()
        {
            string reasonForFailure;
            PostalCode result;

            Assert.IsTrue(PostOfficeBoxPostalCode.TryCreate(ValidPostalCode, out result, out reasonForFailure));
            Assert.AreEqual(ValidPostalCode, result.ToString());
            Assert.IsTrue(string.IsNullOrWhiteSpace(reasonForFailure));
        }

        [TestMethod]
        public void FiveDigitPostalCodeThatDoesntEndWithZeroSpecification()
        {
            var specification = new PostOfficeBoxPostalCodeSpecification();

            Assert.IsTrue(specification.IsSatisfiedBy(ValidPostalCode));
            Assert.IsTrue(string.IsNullOrWhiteSpace(specification.GetReasonsForDissatisfactionSeparatedWithNewLine()));
        }

        [TestMethod]
        public void DoesNotEqualToDifferentTypeObject()
        {
            sut = PostOfficeBoxPostalCode.Create(ValidPostalCode);

            Assert.AreNotEqual(ValidPostalCode, sut);
        }

        [TestMethod]
        public void EqualityIsDeterminedByContent()
        {
            sut = PostOfficeBoxPostalCode.Create(ValidPostalCode);
            PostOfficeBoxPostalCode other = PostOfficeBoxPostalCode.Create(ValidPostalCode);

            Assert.AreEqual(other, sut);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooLongPostalCode()
        {
            PostOfficeBoxPostalCode.Create("206100");
        }

        [TestMethod]
        public void TryTooLongPostalCode()
        {
            AssertInvalidPostalCode("220610");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FiveDigitPostalCodeThatEndsWithZero()
        {
            PostOfficeBoxPostalCode.Create("20610");
        }

        [TestMethod]
        public void TryFiveDigitPostalCodeThatEndsWithZero()
        {
            AssertInvalidPostalCode("20540");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooShortPostalCode()
        {
            PostOfficeBoxPostalCode.Create("51");
        }

        [TestMethod]
        public void TryTooShortPostalCode()
        {
            AssertInvalidPostalCode("51");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullPostalCode()
        {
            PostOfficeBoxPostalCode.Create(null);
        }

        [TestMethod]
        public void TryNullPostalCode()
        {
            AssertInvalidPostalCode(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyPostalCode()
        {
            PostOfficeBoxPostalCode.Create(string.Empty);
        }

        [TestMethod]
        public void TryEmptyPostalCode()
        {
            AssertInvalidPostalCode(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NonNumericPostalCode()
        {
            PostOfficeBoxPostalCode.Create("viisi");
        }

        [TestMethod]
        public void TryNonNumericPostalCode()
        {
            AssertInvalidPostalCode("neljä");
        }

        private void AssertInvalidPostalCode(string postalCode)
        {
            AssertFailedTryCreate(postalCode);
            AssertFailedSpecification(postalCode);
        }

        private void AssertFailedTryCreate(string postalCode)
        {
            string failureReason;
            PostalCode result;
            Assert.IsFalse(PostOfficeBoxPostalCode.TryCreate(postalCode, out result, out failureReason));
            Assert.IsFalse(string.IsNullOrWhiteSpace(failureReason));
            Assert.IsNull(result);
        }

        private static void AssertFailedSpecification(string postalCode)
        {
            var specification = new PostOfficeBoxPostalCodeSpecification();
            Assert.IsFalse(specification.IsSatisfiedBy(postalCode));
            Assert.IsFalse(string.IsNullOrWhiteSpace(specification.GetReasonsForDissatisfactionSeparatedWithNewLine()));
        }
    }
}