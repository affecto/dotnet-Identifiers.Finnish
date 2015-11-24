using System.Text.RegularExpressions;
using Affecto.Patterns.Specification;

namespace Affecto.Identifiers.Finnish
{
    public class BusinessIdentifierSpecification : Specification<string>
    {
        private const string BusinessIdPattern = @"^(\d{7})-(\d)$";

        protected override bool IsSatisfied(string entity)
        {
            if (entity == null)
            {
                AddReasonForDissatisfaction(InvalidBusinessIdentifierReason.Format);
                return false;
            }
            
            Match businessIdMatch = Regex.Match(entity, BusinessIdPattern);
            if (businessIdMatch.Success)
            {
                string orderNumber = businessIdMatch.Groups[1].Value;
                string expectedCheckSumDigit = businessIdMatch.Groups[2].Value;
                int actualCheckSumDigit = CalculateCheckSumDigit(orderNumber);
                if (expectedCheckSumDigit.Equals(actualCheckSumDigit.ToString()))
                {
                    return true;
                }
                if (actualCheckSumDigit < 0)
                {
                    AddReasonForDissatisfaction(InvalidBusinessIdentifierReason.OrderNumber);
                    return false;                    
                }
                AddReasonForDissatisfaction(InvalidBusinessIdentifierReason.CheckSum);
                return false;
            }
            AddReasonForDissatisfaction(InvalidBusinessIdentifierReason.Format);
            return false;
        }

        private static int CalculateCheckSumDigit(string orderNumber)
        {
            const int orderNumberPartSumDivider = 11;

            int orderNumbersMultiplied = int.Parse(orderNumber[0].ToString()) * 7 + int.Parse(orderNumber[1].ToString()) * 9 + int.Parse(orderNumber[2].ToString()) * 10
                + int.Parse(orderNumber[3].ToString()) * 5 + int.Parse(orderNumber[4].ToString()) * 8 + int.Parse(orderNumber[5].ToString()) * 4
                + int.Parse(orderNumber[6].ToString()) * 2;

            int orderNumberPartSumRemainder = orderNumbersMultiplied % orderNumberPartSumDivider;
            if (orderNumberPartSumRemainder == 1)
            {
                // indicates errorneous business id
                return -1;
            }
            if (orderNumberPartSumRemainder == 0)
            {
                return 0;
            }
            return orderNumberPartSumDivider - orderNumberPartSumRemainder;
        }
    }
}