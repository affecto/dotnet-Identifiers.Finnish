using System;
using System.Text.RegularExpressions;
using Affecto.Patterns.Specification;

namespace Affecto.Identifiers.Finnish
{
    public class InvoiceReferenceNumberSpecification : Specification<string>, ISpecification<int>
    {
        protected override bool IsSatisfied(string entity)
        {
            if (entity == null)
            {
                AddReasonForDissatisfaction("Reference number is null.");
                return false;
            }
            entity = entity.Trim();

            if (entity.Length < 4)
            {
                AddReasonForDissatisfaction(string.Format("Reference number '{0}' is too short.", entity));
                return false;
            }
            if (entity.Length > 20)
            {
                AddReasonForDissatisfaction(string.Format("Reference number '{0}' is too long.", entity));
                return false;
            }
            if (!Regex.IsMatch(entity, @"^[\d ]+$"))
            {
                AddReasonForDissatisfaction(string.Format("Reference number '{0}' contains illegal characters.", entity));
                return false;
            }

            int givenChecksum = int.Parse(entity[entity.Length - 1].ToString());
            int calculatedChecksum = Calculate731Checksum(entity.Substring(0, entity.Length - 1));
            if (givenChecksum != calculatedChecksum)
            {
                AddReasonForDissatisfaction(string.Format("Reference number '{0}' contains an invalid checksum.", entity));
                return false;
            }

            return true;
        }

        public bool IsSatisfiedBy(int entity)
        {
            return IsSatisfied(entity.ToString());
        }

        private static int Calculate731Checksum(string body)
        {
            int sum = 0;
            int[] multipliers = { 7, 3, 1 };
            int multiplierPosition = 0;
            for (int i = body.Length - 1; i >= 0; i--)
            {
                if (body[i] != ' ')
                {
                    int current = int.Parse(body[i].ToString());
                    int multiplier = multipliers[multiplierPosition];

                    sum += current * multiplier;

                    multiplierPosition++;
                    if (multiplierPosition > 2)
                    {
                        multiplierPosition = 0;
                    }
                }
            }

            int checksum = 10 - (sum % 10);
            if (checksum == 10)
            {
                checksum = 0;
            }

            return checksum;
        }
    }
}
