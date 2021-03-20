using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_ContractDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long? CompanyId { get; set; }
        public long? OpportunityId { get; set; }
        public long CustomerId { get; set; }
        public long ContractTypeId { get; set; }
        public decimal TotalValue { get; set; }
        public long CurrencyId { get; set; }
        public DateTime ValidityDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public long AppUserId { get; set; }
        public string DeliveryUnit { get; set; }
        public long ContractStatusId { get; set; }
        public long PaymentStatusId { get; set; }
        public string InvoiceAddress { get; set; }
        public long? InvoiceNationId { get; set; }
        public long? InvoiceProvinceId { get; set; }
        public long? InvoiceDistrictId { get; set; }
        public string InvoiceZipCode { get; set; }
        public string ReceiveAddress { get; set; }
        public long? ReceiveNationId { get; set; }
        public long? ReceiveProvinceId { get; set; }
        public long? ReceiveDistrictId { get; set; }
        public string ReceiveZipCode { get; set; }
        public decimal SubTotal { get; set; }
        public decimal? GeneralDiscountPercentage { get; set; }
        public decimal? GeneralDiscountAmount { get; set; }
        public decimal? TotalTaxAmountOther { get; set; }
        public decimal? TotalTaxAmount { get; set; }
        public decimal Total { get; set; }
        public string TermAndCondition { get; set; }
        public long CreatorId { get; set; }
        public long OrganizationId { get; set; }
        public Customer_AppUserDTO AppUser { get; set; }
        public Customer_CompanyDTO Company { get; set; }
        public Customer_ContractStatusDTO ContractStatus { get; set; }
        public Customer_ContractTypeDTO ContractType { get; set; }
        public Customer_AppUserDTO Creator { get; set; }
        public Customer_CurrencyDTO Currency { get; set; }
        public Customer_CustomerDTO Customer { get; set; }
        public Customer_DistrictDTO InvoiceDistrict { get; set; }
        public Customer_NationDTO InvoiceNation { get; set; }
        public Customer_ProvinceDTO InvoiceProvince { get; set; }
        public Customer_OpportunityDTO Opportunity { get; set; }
        public Customer_OrganizationDTO Organization { get; set; }
        public Customer_PaymentStatusDTO PaymentStatus { get; set; }
        public Customer_DistrictDTO ReceiveDistrict { get; set; }
        public Customer_NationDTO ReceiveNation { get; set; }
        public Customer_ProvinceDTO ReceiveProvince { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Customer_ContractDTO() {}
        public Customer_ContractDTO(Contract Contract)
        {
            this.Id = Contract.Id;
            this.Code = Contract.Code;
            this.Name = Contract.Name;
            this.CompanyId = Contract.CompanyId;
            this.OpportunityId = Contract.OpportunityId;
            this.CustomerId = Contract.CustomerId;
            this.ContractTypeId = Contract.ContractTypeId;
            this.TotalValue = Contract.TotalValue;
            this.CurrencyId = Contract.CurrencyId;
            this.ValidityDate = Contract.ValidityDate;
            this.ExpirationDate = Contract.ExpirationDate;
            this.AppUserId = Contract.AppUserId;
            this.DeliveryUnit = Contract.DeliveryUnit;
            this.ContractStatusId = Contract.ContractStatusId;
            this.PaymentStatusId = Contract.PaymentStatusId;
            this.InvoiceAddress = Contract.InvoiceAddress;
            this.InvoiceNationId = Contract.InvoiceNationId;
            this.InvoiceProvinceId = Contract.InvoiceProvinceId;
            this.InvoiceDistrictId = Contract.InvoiceDistrictId;
            this.InvoiceZipCode = Contract.InvoiceZipCode;
            this.ReceiveAddress = Contract.ReceiveAddress;
            this.ReceiveNationId = Contract.ReceiveNationId;
            this.ReceiveProvinceId = Contract.ReceiveProvinceId;
            this.ReceiveDistrictId = Contract.ReceiveDistrictId;
            this.ReceiveZipCode = Contract.ReceiveZipCode;
            this.SubTotal = Contract.SubTotal;
            this.GeneralDiscountPercentage = Contract.GeneralDiscountPercentage;
            this.GeneralDiscountAmount = Contract.GeneralDiscountAmount;
            this.TotalTaxAmountOther = Contract.TotalTaxAmountOther;
            this.TotalTaxAmount = Contract.TotalTaxAmount;
            this.Total = Contract.Total;
            this.TermAndCondition = Contract.TermAndCondition;
            this.CreatorId = Contract.CreatorId;
            this.OrganizationId = Contract.OrganizationId;
            this.AppUser = Contract.AppUser == null ? null : new Customer_AppUserDTO(Contract.AppUser);
            this.Company = Contract.Company == null ? null : new Customer_CompanyDTO(Contract.Company);
            this.ContractStatus = Contract.ContractStatus == null ? null : new Customer_ContractStatusDTO(Contract.ContractStatus);
            this.ContractType = Contract.ContractType == null ? null : new Customer_ContractTypeDTO(Contract.ContractType);
            this.Creator = Contract.Creator == null ? null : new Customer_AppUserDTO(Contract.Creator);
            this.Currency = Contract.Currency == null ? null : new Customer_CurrencyDTO(Contract.Currency);
            this.Customer = Contract.Customer == null ? null : new Customer_CustomerDTO(Contract.Customer);
            this.InvoiceDistrict = Contract.InvoiceDistrict == null ? null : new Customer_DistrictDTO(Contract.InvoiceDistrict);
            this.InvoiceNation = Contract.InvoiceNation == null ? null : new Customer_NationDTO(Contract.InvoiceNation);
            this.InvoiceProvince = Contract.InvoiceProvince == null ? null : new Customer_ProvinceDTO(Contract.InvoiceProvince);
            this.Opportunity = Contract.Opportunity == null ? null : new Customer_OpportunityDTO(Contract.Opportunity);
            this.Organization = Contract.Organization == null ? null : new Customer_OrganizationDTO(Contract.Organization);
            this.PaymentStatus = Contract.PaymentStatus == null ? null : new Customer_PaymentStatusDTO(Contract.PaymentStatus);
            this.ReceiveDistrict = Contract.ReceiveDistrict == null ? null : new Customer_DistrictDTO(Contract.ReceiveDistrict);
            this.ReceiveNation = Contract.ReceiveNation == null ? null : new Customer_NationDTO(Contract.ReceiveNation);
            this.ReceiveProvince = Contract.ReceiveProvince == null ? null : new Customer_ProvinceDTO(Contract.ReceiveProvince);
            this.CreatedAt = Contract.CreatedAt;
            this.UpdatedAt = Contract.UpdatedAt;
            this.Errors = Contract.Errors;
        }
    }

    public class Customer_ContractFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter CompanyId { get; set; }
        public IdFilter OpportunityId { get; set; }
        public IdFilter CustomerId { get; set; }
        public IdFilter ContractTypeId { get; set; }
        public DecimalFilter TotalValue { get; set; }
        public IdFilter CurrencyId { get; set; }
        public DateFilter ValidityDate { get; set; }
        public DateFilter ExpirationDate { get; set; }
        public IdFilter AppUserId { get; set; }
        public StringFilter DeliveryUnit { get; set; }
        public IdFilter ContractStatusId { get; set; }
        public IdFilter PaymentStatusId { get; set; }
        public StringFilter InvoiceAddress { get; set; }
        public IdFilter InvoiceNationId { get; set; }
        public IdFilter InvoiceProvinceId { get; set; }
        public IdFilter InvoiceDistrictId { get; set; }
        public StringFilter InvoiceZipCode { get; set; }
        public StringFilter ReceiveAddress { get; set; }
        public IdFilter ReceiveNationId { get; set; }
        public IdFilter ReceiveProvinceId { get; set; }
        public IdFilter ReceiveDistrictId { get; set; }
        public StringFilter ReceiveZipCode { get; set; }
        public DecimalFilter SubTotal { get; set; }
        public DecimalFilter GeneralDiscountPercentage { get; set; }
        public DecimalFilter GeneralDiscountAmount { get; set; }
        public DecimalFilter TotalTaxAmountOther { get; set; }
        public DecimalFilter TotalTaxAmount { get; set; }
        public DecimalFilter Total { get; set; }
        public StringFilter TermAndCondition { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter OrganizationId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public ContractOrder OrderBy { get; set; }
    }
}
