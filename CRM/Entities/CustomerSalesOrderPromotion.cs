using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CustomerSalesOrderPromotion : DataEntity,  IEquatable<CustomerSalesOrderPromotion>
    {
        public long Id { get; set; }
        public long CustomerSalesOrderId { get; set; }
        public long ItemId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long Quantity { get; set; }
        public long RequestedQuantity { get; set; }
        public long PrimaryUnitOfMeasureId { get; set; }
        public long? Factor { get; set; }
        public string Note { get; set; }
        public CustomerSalesOrder CustomerSalesOrder { get; set; }
        public Item Item { get; set; }
        public UnitOfMeasure PrimaryUnitOfMeasure { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        
        public bool Equals(CustomerSalesOrderPromotion other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CustomerSalesOrderPromotionFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter CustomerSalesOrderId { get; set; }
        public IdFilter ItemId { get; set; }
        public IdFilter UnitOfMeasureId { get; set; }
        public LongFilter Quantity { get; set; }
        public LongFilter RequestedQuantity { get; set; }
        public IdFilter PrimaryUnitOfMeasureId { get; set; }
        public LongFilter Factor { get; set; }
        public StringFilter Note { get; set; }
        public List<CustomerSalesOrderPromotionFilter> OrFilter { get; set; }
        public CustomerSalesOrderPromotionOrder OrderBy {get; set;}
        public CustomerSalesOrderPromotionSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerSalesOrderPromotionOrder
    {
        Id = 0,
        CustomerSalesOrder = 1,
        Item = 2,
        UnitOfMeasure = 3,
        Quantity = 4,
        RequestedQuantity = 5,
        PrimaryUnitOfMeasure = 6,
        Factor = 7,
        Note = 8,
    }

    [Flags]
    public enum CustomerSalesOrderPromotionSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        CustomerSalesOrder = E._1,
        Item = E._2,
        UnitOfMeasure = E._3,
        Quantity = E._4,
        RequestedQuantity = E._5,
        PrimaryUnitOfMeasure = E._6,
        Factor = E._7,
        Note = E._8,
    }
}
