using System;
using System.Text.RegularExpressions;

namespace Affecto.Identifiers.Finnish
{
    public class InvoiceReferenceNumber
    {
        private readonly string invoiceReferenceNumber;

        private InvoiceReferenceNumber(string invoiceReferenceNumber)
        {
            this.invoiceReferenceNumber = invoiceReferenceNumber;
        }

        public static InvoiceReferenceNumber Create(int invoiceReferenceNumber)
        {
            return Create(invoiceReferenceNumber.ToString());
        }

        public static InvoiceReferenceNumber Create(string invoiceReferenceNumber)
        {
            if (invoiceReferenceNumber == null)
            {
                throw new ArgumentNullException("invoiceReferenceNumber");
            }

            if (invoiceReferenceNumber.Length < 4)
            {
                throw new ArgumentException(string.Format("InvoiceReferenceNumber '{0}' is too short.", invoiceReferenceNumber), invoiceReferenceNumber);
            }

            if (invoiceReferenceNumber.Length > 20)
            {
                throw new ArgumentException(string.Format("InvoiceReferenceNumber '{0}' is too long.", invoiceReferenceNumber), invoiceReferenceNumber);
            }

            if (!Regex.IsMatch(invoiceReferenceNumber, @"^[\d ]+$"))
            {
                throw new ArgumentException(string.Format("InvoiceReferenceNumber '{0}' contains illegal characters.", invoiceReferenceNumber), invoiceReferenceNumber);
            }

            invoiceReferenceNumber = invoiceReferenceNumber.Replace(" ", "");
            invoiceReferenceNumber = invoiceReferenceNumber.TrimStart('0');
            int givenChecksum = int.Parse(invoiceReferenceNumber[invoiceReferenceNumber.Length - 1].ToString());
            int calculatedChecksum = Calculate731Checksum(invoiceReferenceNumber.Substring(0, invoiceReferenceNumber.Length - 1));

            if (givenChecksum != calculatedChecksum)
            {
                throw new ArgumentException(string.Format("InvoiceReferenceNumber '{0}' contains an invalid checksum.", invoiceReferenceNumber), invoiceReferenceNumber);
            }

            return new InvoiceReferenceNumber(invoiceReferenceNumber);                
        }

        public static bool TryCreate(int invoiceReferenceNumber, out InvoiceReferenceNumber result, out string failureReason)
        {
            return TryCreate(invoiceReferenceNumber.ToString(), out result, out failureReason);
        }

        public static bool TryCreate(string invoiceReferenceNumber, out InvoiceReferenceNumber result, out string failureReason)
        {
            try
            {
                result = Create(invoiceReferenceNumber);
                failureReason = string.Empty;
                return true;
            }
            catch (ArgumentNullException)
            {
                failureReason = "Argument 'invoiceReferenceNumber' cannot be null.";
            }
            catch (ArgumentException ex)
            {
                failureReason = ex.Message;
            }
            catch (Exception ex)
            {
                failureReason = ex.Message;
            }
            result = null;
            return false;
        }

        public override string ToString()
        {
            return invoiceReferenceNumber;
        }

        public override bool Equals(object obj)
        {
            if (obj is InvoiceReferenceNumber)
            {
                return Equals((InvoiceReferenceNumber) obj);
            }
            return false;
        }
      
        public override int GetHashCode()
        {
            return invoiceReferenceNumber.GetHashCode();
        }

        protected bool Equals(InvoiceReferenceNumber other)
        {
            return invoiceReferenceNumber.Equals(other.invoiceReferenceNumber);
        }

        private static int Calculate731Checksum(string body)
        {
            int sum = 0;
            int[] multipliers = { 7, 3, 1 };
            int multiplierPosition = 0;
            for (int i = body.Length - 1; i >= 0; i--)
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

            int checksum = 10 - (sum % 10);
            if (checksum == 10)
            {
                checksum = 0;
            }

            return checksum;
        }
    }
}