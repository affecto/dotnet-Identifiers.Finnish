using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Affecto.Identifiers.Finnish.Tests
{
    [TestClass]
    public class PhoneNumberTests
    {
        private PhoneNumber sut;

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullPhoneNumber()
        {
            PhoneNumber.Create(null);
        }

        [TestMethod]
        public void TryNullPhoneNumber()
        {
            AssertInvalidValue(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyPhoneNumber()
        {
            PhoneNumber.Create(string.Empty);
        }

        [TestMethod]
        public void TryEmptyPhoneNumber()
        {
            AssertInvalidValue(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TextualPhoneNumber()
        {
            PhoneNumber.Create("number");
        }

        [TestMethod]
        public void TryTextualPhoneNumber()
        {
            AssertInvalidValue("number");
        }

        [TestMethod]
        public void NumericPhoneNumber()
        {
            const string phoneNumber = "0100100";

            sut = PhoneNumber.Create(phoneNumber);

            Assert.AreEqual(phoneNumber, sut.ToString());
        }

        [TestMethod]
        public void TryNumericPhoneNumber()
        {
            AssertValidValue("0100100");
        }

        [TestMethod]
        public void NumericPhoneNumberWithWhiteSpaces()
        {
            const string phoneNumber = "02 588 4430";

            sut = PhoneNumber.Create(phoneNumber);

            Assert.AreEqual(phoneNumber, sut.ToString());
        }

        [TestMethod]
        public void TryNumericPhoneNumberWithWhiteSpaces()
        {
            AssertValidValue("02 5884430");
        }

        [TestMethod]
        public void CellPhoneNumberWithCountryPrefix()
        {
            const string phoneNumber = "+35850 123 4567";

            sut = PhoneNumber.Create(phoneNumber);

            Assert.AreEqual(phoneNumber, sut.ToString());
        }

        [TestMethod]
        public void TryCellPhoneNumberWithCountryPrefix()
        {
            AssertValidValue("+358501234567");
        }

        [TestMethod]
        public void LandLineNumberWithCountryPrefix()
        {
            const string phoneNumber = "+358 9 451 4319";

            sut = PhoneNumber.Create(phoneNumber);

            Assert.AreEqual(phoneNumber, sut.ToString());
        }

        [TestMethod]
        public void TryLandLineNumberWithCountryPrefix()
        {
            AssertValidValue("+358 9 451 4319");
        }

        [TestMethod]
        public void CellPhoneNumberWithWhiteSpaces()
        {
            const string phoneNumber = "050 123 4567";

            sut = PhoneNumber.Create(phoneNumber);

            Assert.AreEqual(phoneNumber, sut.ToString());
        }

        [TestMethod]
        public void TryCellPhoneNumberWithWhiteSpaces()
        {
            AssertValidValue("050 123 4567");
        }

        [TestMethod]
        public void PhoneNumberWithManyNumbersBeforeTheFirstWhiteSpace()
        {
            const string phoneNumber = "0295 50 3000";

            sut = PhoneNumber.Create(phoneNumber);

            Assert.AreEqual(phoneNumber, sut.ToString());
        }

        [TestMethod]
        public void TryPhoneNumberWithManyNumbersBeforeTheFirstWhiteSpace()
        {
            AssertValidValue("0295 50 3000");
        }

        [TestMethod]
        public void CellPhoneNumberWithLine()
        {
            const string phoneNumber = "050-1234567";

            sut = PhoneNumber.Create(phoneNumber);

            Assert.AreEqual(phoneNumber, sut.ToString());
        }

        [TestMethod]
        public void TryCellPhoneNumberWithLine()
        {
            AssertValidValue("050-123 4567");
        }

        private static void AssertValidValue(string phoneNumber)
        {
            AssertSuccessfulTryCreate(phoneNumber);
            AssertSatisfiedSpecification(phoneNumber);
        }

        private static void AssertSatisfiedSpecification(string phoneNumber)
        {
            var specification = new PhoneNumberSpecification();
            Assert.IsTrue(specification.IsSatisfiedBy(phoneNumber));
            Assert.IsTrue(string.IsNullOrWhiteSpace(specification.GetReasonsForDissatisfactionSeparatedWithNewLine()));
        }

        private static void AssertSuccessfulTryCreate(string phoneNumber)
        {
            string failureReason;
            PhoneNumber result;
            Assert.IsTrue(PhoneNumber.TryCreate(phoneNumber, out result, out failureReason));
            Assert.AreEqual(phoneNumber, result.ToString());
            Assert.IsTrue(string.IsNullOrWhiteSpace(failureReason));
        }

        private static void AssertInvalidValue(string phoneNumber)
        {
            AssertFailedTryCreate(phoneNumber);
            AssertDissatisfiedSpecification(phoneNumber);
        }

        private static void AssertDissatisfiedSpecification(string phoneNumber)
        {
            var specification = new PhoneNumberSpecification();
            Assert.IsFalse(specification.IsSatisfiedBy(phoneNumber));
            Assert.IsFalse(string.IsNullOrWhiteSpace(specification.GetReasonsForDissatisfactionSeparatedWithNewLine()));
        }

        private static void AssertFailedTryCreate(string phoneNumber)
        {
            string failureReason;
            PhoneNumber result;
            Assert.IsFalse(PhoneNumber.TryCreate(phoneNumber, out result, out failureReason));
            Assert.IsNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(failureReason));
        }
    }
}