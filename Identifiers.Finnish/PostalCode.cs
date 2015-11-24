using System;

namespace Affecto.Identifiers.Finnish
{
    public class PostalCode
    {
        private readonly string postalCode;

        protected PostalCode(string postalCode)
        {
            this.postalCode = postalCode;
        }

        public static PostalCode Create(string postalCode)
        {
            if (postalCode == null)
            {
                throw new ArgumentNullException("postalCode");
            }

            var specification = new PostalCodeSpecification();
            if (specification.IsSatisfiedBy(postalCode))
            {
                return new PostalCode(postalCode);                
            }
            throw new ArgumentException(string.Format("Postal code '{0}' does not satisfy specification.", postalCode), "postalCode");
        }

        public static bool TryCreate(string postalCode, out PostalCode result, out string failureReason)
        {
            var specification = new PostalCodeSpecification();
            if (specification.IsSatisfiedBy(postalCode))
            {
                result = new PostalCode(postalCode);
                failureReason = string.Empty;
                return true;
            }

            result = null;
            failureReason = specification.GetReasonsForDissatisfactionSeparatedWithNewLine();
            return false;
        }

        public override string ToString()
        {
            return postalCode;
        }

        public override bool Equals(object obj)
        {
            if (obj is PostalCode)
            {
                return Equals((PostalCode) obj);
            }
            return false;
        }

        protected bool Equals(PostalCode other)
        {
            return postalCode.Equals(other.postalCode);
        }

        public override int GetHashCode()
        {
            return postalCode.GetHashCode();
        }
    }
}