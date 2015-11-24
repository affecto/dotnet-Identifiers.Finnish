using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.Identifiers.Finnish.Tests
{
    [TestClass]
    public class PostalCodeTests
    {
        private const string ValidPostalCode = "20610";

        private PostalCode sut;
        
        [TestMethod]
        public void FiveDigitPostalCode()
        {
            sut = PostalCode.Create(ValidPostalCode);

            Assert.AreEqual(ValidPostalCode, sut.ToString());
        }

        [TestMethod]
        public void TryFiveDigitPostalCode()
        {
            string reasonForFailure;

            Assert.IsTrue(PostalCode.TryCreate(ValidPostalCode, out sut, out reasonForFailure));
            Assert.AreEqual(ValidPostalCode, sut.ToString());
            Assert.IsTrue(string.IsNullOrWhiteSpace(reasonForFailure));
        }

        [TestMethod]
        public void FiveDigitPostalCodeSpecification()
        {
            var specification = new PostalCodeSpecification();

            Assert.IsTrue(specification.IsSatisfiedBy(ValidPostalCode));
            Assert.IsTrue(string.IsNullOrWhiteSpace(specification.GetReasonsForDissatisfactionSeparatedWithNewLine()));
        }

        [TestMethod]
        public void DoesNotEqualToDifferentTypeObject()
        {
            sut = PostalCode.Create(ValidPostalCode);

            Assert.AreNotEqual(new DateTime(), sut);
        }

        [TestMethod]
        public void EqualityIsDeterminedByContent()
        {
            sut = PostalCode.Create(ValidPostalCode);
            PostalCode other = PostalCode.Create(ValidPostalCode);

            Assert.AreEqual(other, sut);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooLongPostalCode()
        {
            PostalCode.Create("206100");
        }

        [TestMethod]
        public void TryTooLongPostalCode()
        {
            AssertInvalidPostalCode("220610");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TooShortPostalCode()
        {
            PostalCode.Create("51");
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
            PostalCode.Create(null);
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
            PostalCode.Create(string.Empty);
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
            PostalCode.Create("viisi");
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
            Assert.IsFalse(PostalCode.TryCreate(postalCode, out sut, out failureReason));
            Assert.IsFalse(string.IsNullOrWhiteSpace(failureReason));
            Assert.IsNull(sut);
        }

        private static void AssertFailedSpecification(string postalCode)
        {
            var specification = new PostalCodeSpecification();
            Assert.IsFalse(specification.IsSatisfiedBy(postalCode));
            Assert.IsFalse(string.IsNullOrWhiteSpace(specification.GetReasonsForDissatisfactionSeparatedWithNewLine()));
        }
    }
}