using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace RiverPuzzle1.ValidationAttributes
{
    public class EnsureCollectionSizeAttribute: ValidationAttribute
    {
        private readonly int _count;

        public EnsureCollectionSizeAttribute(int count)
        {
            _count = count;
        }

        public override bool IsValid(object value)
        {
            var list = value as ICollection;
            return list.Count == _count;
        }
    }
   
}
