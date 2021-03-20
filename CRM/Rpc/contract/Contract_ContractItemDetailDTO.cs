using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contract
{
    public class Contract_ContractItemDetailDTO : DataDTO
    {
        public long Id { get; set; }
        public long ContractId { get; set; }
        public long ItemId { get; set; }
        public long UnitOfMeasureId { get; set; }
        public long PrimaryUnitOfMeasureId { get; set; }
        public long Quantity { get; set; }
        public long RequestQuantity { get; set; }
        public decimal PrimaryPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? GeneralDiscountPercentage { get; set; }
        public decimal? GeneralDiscountAmount { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TaxPercentageOther { get; set; }
        public decimal? TaxAmountOther { get; set; }
        public decimal TotalPrice { get; set; }
        public long? Factor { get; set; }
        public long TaxTypeId { get; set; }
        public Contract_ItemDTO Item { get; set; }   
        public Contract_UnitOfMeasureDTO PrimaryUnitOfMeasure { get; set; }   
        public Contract_UnitOfMeasureDTO UnitOfMeasure { get; set; }   
        public Contract_TaxTypeDTO TaxType { get; set; }   

        public Contract_ContractItemDetailDTO() {}
        public Contract_ContractItemDetailDTO(ContractItemDetail ContractItemDetail)
        {
            this.Id = ContractItemDetail.Id;
            this.ContractId = ContractItemDetail.ContractId;
            this.ItemId = ContractItemDetail.ItemId;
            this.UnitOfMeasureId = ContractItemDetail.UnitOfMeasureId;
            this.PrimaryUnitOfMeasureId = ContractItemDetail.PrimaryUnitOfMeasureId;
            this.Quantity = ContractItemDetail.Quantity;
            this.RequestQuantity = ContractItemDetail.RequestQuantity;
            this.PrimaryPrice = ContractItemDetail.PrimaryPrice;
            this.SalePrice = ContractItemDetail.SalePrice;
            this.DiscountPercentage = ContractItemDetail.DiscountPercentage;
            this.DiscountAmount = ContractItemDetail.DiscountAmount;
            this.GeneralDiscountPercentage = ContractItemDetail.GeneralDiscountPercentage;
            this.GeneralDiscountAmount = ContractItemDetail.GeneralDiscountAmount;
            this.TaxPercentage = ContractItemDetail.TaxPercentage;
            this.TaxAmount = ContractItemDetail.TaxAmount;
            this.TaxPercentageOther = ContractItemDetail.TaxPercentageOther;
            this.TaxAmountOther = ContractItemDetail.TaxAmountOther;
            this.TotalPrice = ContractItemDetail.TotalPrice;
            this.Factor = ContractItemDetail.Factor;
            this.TaxTypeId = ContractItemDetail.TaxTypeId;
            this.Item = ContractItemDetail.Item == null ? null : new Contract_ItemDTO(ContractItemDetail.Item);
            this.PrimaryUnitOfMeasure = ContractItemDetail.PrimaryUnitOfMeasure == null ? null : new Contract_UnitOfMeasureDTO(ContractItemDetail.PrimaryUnitOfMeasure);
            this.UnitOfMeasure = ContractItemDetail.UnitOfMeasure == null ? null : new Contract_UnitOfMeasureDTO(ContractItemDetail.UnitOfMeasure);
            this.TaxType = ContractItemDetail.TaxType == null ? null : new Contract_TaxTypeDTO(ContractItemDetail.TaxType);
            this.Errors = ContractItemDetail.Errors;
        }
    }

    public class Contract_ContractItemDetailFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public IdFilter ContractId { get; set; }
        
        public IdFilter ItemId { get; set; }
        
        public IdFilter UnitOfMeasureId { get; set; }
        
        public IdFilter PrimaryUnitOfMeasureId { get; set; }
        
        public LongFilter Quantity { get; set; }
        
        public LongFilter RequestQuantity { get; set; }
        
        public DecimalFilter PrimaryPrice { get; set; }
        
        public DecimalFilter SalePrice { get; set; }

        public DecimalFilter DiscountPercentage { get; set; }
        public DecimalFilter DiscountAmount { get; set; }
        public DecimalFilter GeneralDiscountPercentage { get; set; }
        public DecimalFilter GeneralDiscountAmount { get; set; }
        public DecimalFilter TaxPercentage { get; set; }
        public DecimalFilter TaxAmount { get; set; }
        public DecimalFilter TaxPercentageOther { get; set; }
        public DecimalFilter TaxAmountOther { get; set; }

        public DecimalFilter TotalPrice { get; set; }
        
        public LongFilter Factor { get; set; }
        public IdFilter TaxTypeId { get; set; }
        
        public ContractItemDetailOrder OrderBy { get; set; }
    }
}