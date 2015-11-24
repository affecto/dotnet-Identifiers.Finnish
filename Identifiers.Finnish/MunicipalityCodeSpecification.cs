using Affecto.Patterns.Specification;

namespace Affecto.Identifiers.Finnish
{
    public class MunicipalityCodeSpecification : RegexSpecification
    {
        private const string Pattern = @"^\d{1,3}$";

        public MunicipalityCodeSpecification()
            : base(Pattern, "Municipality id '{0}' is of invalid format.")
        {
        }
    }
}