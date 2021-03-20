using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_ContractDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal? TotalValue { get; set; }
        public DateTime? ValidityDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string DeliveryUnit { get; set; }
        public string InvoiceAddress { get; set; }
        public string InvoiceZipCode { get; set; }
        public string ReceiveAddress { get; set; }
        public string ReceiveZipCode { get; set; }
        public string TermAndCondition { get; set; }
        public long? InvoiceNationId { get; set; }
        public long? InvoiceProvinceId { get; set; }
        public long? InvoiceDistrictId { get; set; }
        public long? ReceiveNationId { get; set; }
        public long? ReceiveProvinceId { get; set; }
        public long? ReceiveDistrictId { get; set; }
        public long? ContractTypeId { get; set; }
        public long? CompanyId { get; set; }
        public long? OpportunityId { get; set; }
        public long? OrganizationId { get; set; }
        public long? AppUserId { get; set; }
        public long? ContractStatusId { get; set; }
        public long CreatorId { get; set; }
        public long? CustomerId { get; set; }
        public long? CurrencyId { get; set; }
        public long? PaymentStatusId { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? GeneralDiscountPercentage { get; set; }
        public decimal? GeneralDiscountAmount { get; set; }
        public decimal? TotalTaxAmountOther { get; set; }
        public decimal? TotalTaxAmount { get; set; }
        public decimal? Total { get; set; }
        public Contact_CompanyDTO Company { get; set; }
        public Contact_AppUserDTO AppUser { get; set; }
        public Contact_AppUserDTO Creator { get; set; }
        public Contact_CustomerDTO Customer { get; set; }
        public Contact_DistrictDTO InvoiceDistrict { get; set; }
        public Contact_NationDTO InvoiceNation { get; set; }
        public Contact_ProvinceDTO InvoiceProvince { get; set; }
        public Contact_OpportunityDTO Opportunity { get; set; }
        public Contact_OrganizationDTO Organization { get; set; }
        public Contact_DistrictDTO ReceiveDistrict { get; set; }
        public Contact_NationDTO ReceiveNation { get; set; }
        public Contact_ProvinceDTO ReceiveProvince { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Contact_ContractDTO() { }
        public Contact_ContractDTO(Contract Contract)
        {
            this.Id = Contract.Id;
            this.Code = Contract.Code;
            this.Name = Contract.Name;
            this.TotalValue = Contract.TotalValue;
            this.ValidityDate = Contract.ValidityDate;
            this.ExpirationDate = Contract.ExpirationDate;
            this.DeliveryUnit = Contract.DeliveryUnit;
            this.InvoiceAddress = Contract.InvoiceAddress;
            this.InvoiceZipCode = Contract.InvoiceZipCode;
            this.ReceiveAddress = Contract.ReceiveAddress;
            this.ReceiveZipCode = Contract.ReceiveZipCode;
            this.TermAndCondition = Contract.TermAndCondition;
            this.InvoiceNationId = Contract.InvoiceNationId;
            this.InvoiceProvinceId = Contract.InvoiceProvinceId;
            this.InvoiceDistrictId = Contract.InvoiceDistrictId;
            this.ReceiveNationId = Contract.ReceiveNationId;
            this.ReceiveProvinceId = Contract.ReceiveProvinceId;
            this.ReceiveDistrictId = Contract.ReceiveDistrictId;
            this.ContractTypeId = Contract.ContractTypeId;
            this.CompanyId = Contract.CompanyId;
            this.OpportunityId = Contract.OpportunityId;
            this.OrganizationId = Contract.OrganizationId;
            this.AppUserId = Contract.AppUserId;
            this.ContractStatusId = Contract.ContractStatusId;
            this.CreatorId = Contract.CreatorId;
            this.CustomerId = Contract.CustomerId;
            this.CurrencyId = Contract.CurrencyId;
            this.PaymentStatusId = Contract.PaymentStatusId;
            this.Company = Contract.Company == null ? null : new Contact_CompanyDTO(Contract.Company);
            this.AppUser = Contract.AppUser == null ? null : new Contact_AppUserDTO(Contract.AppUser);
            this.Creator = Contract.Creator == null ? null : new Contact_AppUserDTO(Contract.Creator);
            this.Customer = Contract.Customer == null ? null : new Contact_CustomerDTO(Contract.Customer);
            this.InvoiceDistrict = Contract.InvoiceDistrict == null ? null : new Contact_DistrictDTO(Contract.InvoiceDistrict);
            this.InvoiceNation = Contract.InvoiceNation == null ? null : new Contact_NationDTO(Contract.InvoiceNation);
            this.InvoiceProvince = Contract.InvoiceProvince == null ? null : new Contact_ProvinceDTO(Contract.InvoiceProvince);
            this.Opportunity = Contract.Opportunity == null ? null : new Contact_OpportunityDTO(Contract.Opportunity);
            this.Organization = Contract.Organization == null ? null : new Contact_OrganizationDTO(Contract.Organization);
            this.ReceiveDistrict = Contract.ReceiveDistrict == null ? null : new Contact_DistrictDTO(Contract.ReceiveDistrict);
            this.ReceiveNation = Contract.ReceiveNation == null ? null : new Contact_NationDTO(Contract.ReceiveNation);
            this.ReceiveProvince = Contract.ReceiveProvince == null ? null : new Contact_ProvinceDTO(Contract.ReceiveProvince);
            this.CreatedAt = Contract.CreatedAt;
            this.UpdatedAt = Contract.UpdatedAt;
            this.Errors = Contract.Errors;

            this.SubTotal = Contract.SubTotal;
            this.GeneralDiscountPercentage = Contract.GeneralDiscountPercentage;
            this.GeneralDiscountAmount = Contract.GeneralDiscountAmount;
            this.TotalTaxAmountOther = Contract.TotalTaxAmountOther;
            this.TotalTaxAmount = Contract.TotalTaxAmount;
            this.Total = Contract.Total;
        }
    }

    public class Contact_ContractFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public DecimalFilter TotalValue { get; set; }
        public DateFilter ValidityDate { get; set; }
        public DateFilter ExpirationDate { get; set; }
        public StringFilter DeliveryUnit { get; set; }
        public StringFilter InvoiceAddress { get; set; }
        public StringFilter InvoiceZipCode { get; set; }
        public StringFilter ReceiveAddress { get; set; }
        public StringFilter ReceiveZipCode { get; set; }
        public StringFilter TermAndCondition { get; set; }
        public IdFilter InvoiceNationId { get; set; }
        public IdFilter InvoiceProvinceId { get; set; }
        public IdFilter InvoiceDistrictId { get; set; }
        public IdFilter ReceiveNationId { get; set; }
        public IdFilter ReceiveProvinceId { get; set; }
        public IdFilter ReceiveDistrictId { get; set; }
        public IdFilter ContractTypeId { get; set; }
        public IdFilter ContactId { get; set; }
        public IdFilter CompanyId { get; set; }
        public IdFilter OpportunityId { get; set; }
        public IdFilter OrganizationId { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter ContractStatusId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter CustomerId { get; set; }
        public IdFilter CurrencyId { get; set; }
        public IdFilter PaymentStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public ContractOrder OrderBy { get; set; }

        public IdFilter CustomerAgentId { get; set; }

    }
}
