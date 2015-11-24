using Affecto.Patterns.Specification;

namespace Affecto.Identifiers.Finnish
{
    public class PhoneNumberSpecification : RegexSpecification
    {
        private const string Pattern = @"^((([\+][\s]{0,1})|([0]{2}[\s-]{0,1}))(358)([\s-]{0,1})|([0]{1}))(([1-9]{1}[0-9]{0,2})([\s-]{0,1})([0-9]{2,4})([\s-]{0,1})([0-9]{2,4})([\s-]{0,1}))([0-9]{0,3}){1}$";

        public PhoneNumberSpecification()
            : base(Pattern, "Phone number '{0}' is of invalid format.")
        {
        }
    }
}