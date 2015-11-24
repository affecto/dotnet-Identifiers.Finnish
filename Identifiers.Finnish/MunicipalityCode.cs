using System;
using System.Globalization;

namespace Affecto.Identifiers.Finnish
{
    public class MunicipalityCode
    {
        private readonly string municipalityId;

        private MunicipalityCode(string municipalityId)
        {
            this.municipalityId = municipalityId.PadLeft(3, '0');
        }

        public static MunicipalityCode Create(int municipalityId)
        {
            return Create(ConvertToString(municipalityId));
        }

        public static MunicipalityCode Create(string municipalityId)
        {
            if (municipalityId == null)
            {
                throw new ArgumentNullException("municipalityId");
            }

            var specification = new MunicipalityCodeSpecification();
            if (specification.IsSatisfiedBy(municipalityId))
            {
                return new MunicipalityCode(municipalityId);                
            }
            throw new ArgumentException(string.Format("Municipality id '{0}' does not satisfy specification.", municipalityId), "municipalityId");
        }

        public static bool TryCreate(int municipalityId, out MunicipalityCode result, out string failureReason)
        {
            return TryCreate(ConvertToString(municipalityId), out result, out failureReason);
        }

        public static bool TryCreate(string municipalityId, out MunicipalityCode result, out string failureReason)
        {
            var specification = new MunicipalityCodeSpecification();
            if (specification.IsSatisfiedBy(municipalityId))
            {
                result = new MunicipalityCode(municipalityId);
                failureReason = string.Empty;
                return true;
            }

            result = null;
            failureReason = specification.GetReasonsForDissatisfactionSeparatedWithNewLine();
            return false;
        }

        public override string ToString()
        {
            return municipalityId;
        }

        public override bool Equals(object obj)
        {
            if (obj is MunicipalityCode)
            {
                return Equals((MunicipalityCode) obj);
            }
            return false;
        }

        protected bool Equals(MunicipalityCode other)
        {
            return municipalityId.Equals(other.municipalityId);
        }

        public override int GetHashCode()
        {
            return municipalityId.GetHashCode();
        }

        private static string ConvertToString(int municipalityId)
        {
            return municipalityId.ToString(CultureInfo.InvariantCulture);
        }
    }
}