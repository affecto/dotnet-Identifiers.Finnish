using Affecto.Patterns.Specification;

namespace Affecto.Identifiers.Finnish
{
    public class PostOfficeBoxPostalCodeSpecification : RegexSpecification
    {
        private const string Pattern = @"^\d{4}[1-9]$";

        public PostOfficeBoxPostalCodeSpecification()
            : base(Pattern, "Post box postal code '{0}' is of invalid format.")
        {
        }
    }
}