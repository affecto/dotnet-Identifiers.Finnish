using System;

namespace Affecto.Identifiers.Finnish
{
    public class PhoneNumber
    {
        private readonly string phoneNumber;

        private PhoneNumber(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
        }

        public static PhoneNumber Create(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                throw new ArgumentNullException("phoneNumber");
            }

            var specification = new PhoneNumberSpecification();
            if (specification.IsSatisfiedBy(phoneNumber))
            {
                return new PhoneNumber(phoneNumber);
            }
            throw new ArgumentException(string.Format("Phone number '{0}' doesn't satisfy specification.", phoneNumber), "phoneNumber");
        }

        public static bool TryCreate(string phoneNumber, out PhoneNumber result, out string failureReason)
        {
            var specification = new PhoneNumberSpecification();
            if (specification.IsSatisfiedBy(phoneNumber))
            {
                result = new PhoneNumber(phoneNumber);
                failureReason = string.Empty;
                return true;
            }

            result = null;
            failureReason = specification.GetReasonsForDissatisfactionSeparatedWithNewLine();
            return false;
        }

        public override string ToString()
        {
            return phoneNumber;
        }

        public override bool Equals(object obj)
        {
            if (obj is PhoneNumber)
            {
                return Equals(obj as PhoneNumber);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return phoneNumber.GetHashCode();
        }

        protected bool Equals(PhoneNumber other)
        {
            return phoneNumber.Equals(other.phoneNumber);
        }
    }
}