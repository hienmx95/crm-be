using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_CustomerSalesOrderPromotionDTO : DataDTO
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
        public Company_ItemDTO Item { get; set; }   
        public Company_UnitOfMeasureDTO PrimaryUnitOfMeasure { get; set; }   
        public Company_UnitOfMeasureDTO UnitOfMeasure { get; set; }   

        public Company_CustomerSalesOrderPromotionDTO() {}
        public Company_CustomerSalesOrderPromotionDTO(CustomerSalesOrderPromotion CustomerSalesOrderPromotion)
        {
            this.Id = CustomerSalesOrderPromotion.Id;
            this.CustomerSalesOrderId = CustomerSalesOrderPromotion.CustomerSalesOrderId;
            this.ItemId = CustomerSalesOrderPromotion.ItemId;
            this.UnitOfMeasureId = CustomerSalesOrderPromotion.UnitOfMeasureId;
            this.Quantity = CustomerSalesOrderPromotion.Quantity;
            this.RequestedQuantity = CustomerSalesOrderPromotion.RequestedQuantity;
            this.PrimaryUnitOfMeasureId = CustomerSalesOrderPromotion.PrimaryUnitOfMeasureId;
            this.Factor = CustomerSalesOrderPromotion.Factor;
            this.Note = CustomerSalesOrderPromotion.Note;
            this.Item = CustomerSalesOrderPromotion.Item == null ? null : new Company_ItemDTO(CustomerSalesOrderPromotion.Item);
            this.PrimaryUnitOfMeasure = CustomerSalesOrderPromotion.PrimaryUnitOfMeasure == null ? null : new Company_UnitOfMeasureDTO(CustomerSalesOrderPromotion.PrimaryUnitOfMeasure);
            this.UnitOfMeasure = CustomerSalesOrderPromotion.UnitOfMeasure == null ? null : new Company_UnitOfMeasureDTO(CustomerSalesOrderPromotion.UnitOfMeasure);
            this.Errors = CustomerSalesOrderPromotion.Errors;
        }
    }

    public class Company_CustomerSalesOrderPromotionFilterDTO : FilterDTO
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
        
        public CustomerSalesOrderPromotionOrder OrderBy { get; set; }
    }
}