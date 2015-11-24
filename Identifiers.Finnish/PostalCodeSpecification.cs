using Affecto.Patterns.Specification;

namespace Affecto.Identifiers.Finnish
{
    public class PostalCodeSpecification : RegexSpecification
    {
        private const string Pattern = @"^\d{5}$";

        public PostalCodeSpecification()
            : base(Pattern, "Postal code '{0}' is of invalid format.")
        {
        }
    }
}