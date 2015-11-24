using System;

namespace Affecto.Identifiers.Finnish
{
    public class BusinessIdentifier
    {
        private readonly string businessId;

        private BusinessIdentifier(string businessId)
        {
            this.businessId = businessId;
        }

        public static BusinessIdentifier Create(string businessId)
        {
            if (businessId == null)
            {
                throw new ArgumentNullException("businessId");
            }

            var specification = new BusinessIdentifierSpecification();
            if (specification.IsSatisfiedBy(businessId))
            {
                return new BusinessIdentifier(businessId);
            }
            throw new ArgumentException(string.Format("Business id '{0}' does not satisfy specification.", businessId), "businessId");
        }

        public static bool TryCreate(string businessId, out BusinessIdentifier result, out string failureReason)
        {
            var specification = new BusinessIdentifierSpecification();
            if (specification.IsSatisfiedBy(businessId))
            {
                result = new BusinessIdentifier(businessId);
                failureReason = string.Empty;
                return true;
            }

            result = null;
            failureReason = specification.GetReasonsForDissatisfactionSeparatedWithNewLine();
            return false;
        }

        public override string ToString()
        {
            return businessId;
        }

        public override bool Equals(object obj)
        {
            if (obj is BusinessIdentifier)
            {
                return Equals((BusinessIdentifier) obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return businessId.GetHashCode();
        }

        protected bool Equals(BusinessIdentifier other)
        {
            return businessId.Equals(other.businessId);
        }
    }
}