using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class StoreRepresentDAO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public long? PositionId { get; set; }
        public long StoreId { get; set; }

        public virtual PositionDAO Position { get; set; }
        public virtual StoreDAO Store { get; set; }
    }
}
