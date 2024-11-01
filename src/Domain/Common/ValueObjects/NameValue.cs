using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficePerformanceReview.Domain.Common.ValueObjects
{
    public class NameValue : ValueObject
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        protected NameValue() { }

        public NameValue(int staffId, string name)
        {
            Id = staffId;
            Name = Guard.Against.NullOrEmpty(name);

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
        }
    }
}
