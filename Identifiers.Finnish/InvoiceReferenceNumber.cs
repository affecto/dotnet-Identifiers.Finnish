using System;
using System.Text.RegularExpressions;

namespace Affecto.Identifiers.Finnish
{
    public class InvoiceReferenceNumber
    {
        private readonly string invoiceReferenceNumber;

        private InvoiceReferenceNumber(string invoiceReferenceNumber)
        {
            this.invoiceReferenceNumber = Compress(invoiceReferenceNumber);
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

            var specification = new InvoiceReferenceNumberSpecification();
            if (specification.IsSatisfiedBy(invoiceReferenceNumber))
            {
                return new InvoiceReferenceNumber(invoiceReferenceNumber);
            }
            throw new ArgumentException(string.Format("Invoice reference number '{0}' does not satisfy specification.", invoiceReferenceNumber), "invoiceReferenceNumber");
        }

        public static bool TryCreate(int invoiceReferenceNumber, out InvoiceReferenceNumber result, out string failureReason)
        {
            return TryCreate(invoiceReferenceNumber.ToString(), out result, out failureReason);
        }

        public static bool TryCreate(string invoiceReferenceNumber, out InvoiceReferenceNumber result, out string failureReason)
        {
            var specification = new InvoiceReferenceNumberSpecification();
            if (specification.IsSatisfiedBy(invoiceReferenceNumber))
            {
                result = new InvoiceReferenceNumber(invoiceReferenceNumber);
                failureReason = string.Empty;
                return true;
            }

            result = null;
            failureReason = specification.GetReasonsForDissatisfactionSeparatedWithNewLine();
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

        private static string Compress(string invoiceReferenceNumber)
        {
            return invoiceReferenceNumber.Replace(" ", "").TrimStart('0');
        }
    }
}