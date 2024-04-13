using Restaurant.Core.Domain.Core;

namespace Restaurant.Core.Domain.Entities
{
    public class TableStatus : BaseEntity
    {
        public string Description { get; set; }
        public ICollection<Tables>?  Tables { get; set; }
    }
}
