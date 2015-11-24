using System;

namespace Affecto.Identifiers.Finnish
{
    public class PostOfficeBoxPostalCode : PostalCode
    {
        private PostOfficeBoxPostalCode(string postalCode)
            : base(postalCode)
        {
        }

        public new static PostOfficeBoxPostalCode Create(string postalCode)
        {
            if (postalCode == null)
            {
                throw new ArgumentNullException("postalCode");
            }

            var specification = new PostOfficeBoxPostalCodeSpecification();
            if (specification.IsSatisfiedBy(postalCode))
            {
                return new PostOfficeBoxPostalCode(postalCode);                
            }
            throw new ArgumentException(string.Format("Post box postal code '{0}' does not satisfy specification.", postalCode), "postalCode");
        }

        public new static bool TryCreate(string postalCode, out PostalCode result, out string failureReason)
        {
            var specification = new PostOfficeBoxPostalCodeSpecification();
            if (specification.IsSatisfiedBy(postalCode))
            {
                result = new PostOfficeBoxPostalCode(postalCode);
                failureReason = string.Empty;
                return true;
            }

            result = null;
            failureReason = specification.GetReasonsForDissatisfactionSeparatedWithNewLine();
            return false;
        }
    }
}