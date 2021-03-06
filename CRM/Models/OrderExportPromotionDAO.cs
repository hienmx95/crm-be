using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class OrderExportPromotionDAO
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long OrderExportId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long Quantity { get; set; }
        public long PrimaryUnitOfMeasureId { get; set; }
        public long RequestedQuantity { get; set; }
        public string Note { get; set; }
        public long? Factor { get; set; }

        public virtual ItemDAO Item { get; set; }
        public virtual OrderExportDAO OrderExport { get; set; }
        public virtual UnitOfMeasureDAO PrimaryUnitOfMeasure { get; set; }
        public virtual UnitOfMeasureDAO UnitOfMeasure { get; set; }
    }
}
