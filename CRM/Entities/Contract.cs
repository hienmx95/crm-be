using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class Contract : DataEntity,  IEquatable<Contract>
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
        public AppUser AppUser { get; set; }
        public Company Company { get; set; }
        public ContractStatus ContractStatus { get; set; }
        public ContractType ContractType { get; set; }
        public AppUser Creator { get; set; }
        public Currency Currency { get; set; }
        public Customer Customer { get; set; }
        public District InvoiceDistrict { get; set; }
        public Nation InvoiceNation { get; set; }
        public Province InvoiceProvince { get; set; }
        public Opportunity Opportunity { get; set; }
        public Organization Organization { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public District ReceiveDistrict { get; set; }
        public Nation ReceiveNation { get; set; }
        public Province ReceiveProvince { get; set; }
        public List<ContractContactMapping> ContractContactMappings { get; set; }
        public List<ContractFileGrouping> ContractFileGroupings { get; set; }
        public List<ContractItemDetail> ContractItemDetails { get; set; }
        public List<ContractPaymentHistory> ContractPaymentHistories { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public bool Equals(Contract other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class ContractFilter : FilterEntity
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
        public List<ContractFilter> OrFilter { get; set; }
        public ContractOrder OrderBy {get; set;}
        public ContractSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContractOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
        Company = 3,
        Opportunity = 4,
        Customer = 5,
        ContractType = 6,
        TotalValue = 7,
        Currency = 8,
        ValidityDate = 9,
        ExpirationDate = 10,
        AppUser = 11,
        DeliveryUnit = 12,
        ContractStatus = 13,
        PaymentStatus = 14,
        InvoiceAddress = 15,
        InvoiceNation = 16,
        InvoiceProvince = 17,
        InvoiceDistrict = 18,
        InvoiceZipCode = 19,
        ReceiveAddress = 20,
        ReceiveNation = 21,
        ReceiveProvince = 22,
        ReceiveDistrict = 23,
        ReceiveZipCode = 24,
        SubTotal = 25,
        GeneralDiscountPercentage = 26,
        GeneralDiscountAmount = 27,
        TotalTaxAmountOther = 28,
        TotalTaxAmount = 29,
        Total = 30,
        TermAndCondition = 31,
        Creator = 32,
        Organization = 33,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum ContractSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
        Company = E._3,
        Opportunity = E._4,
        Customer = E._5,
        ContractType = E._6,
        TotalValue = E._7,
        Currency = E._8,
        ValidityDate = E._9,
        ExpirationDate = E._10,
        AppUser = E._11,
        DeliveryUnit = E._12,
        ContractStatus = E._13,
        PaymentStatus = E._14,
        InvoiceAddress = E._15,
        InvoiceNation = E._16,
        InvoiceProvince = E._17,
        InvoiceDistrict = E._18,
        InvoiceZipCode = E._19,
        ReceiveAddress = E._20,
        ReceiveNation = E._21,
        ReceiveProvince = E._22,
        ReceiveDistrict = E._23,
        ReceiveZipCode = E._24,
        SubTotal = E._25,
        GeneralDiscountPercentage = E._26,
        GeneralDiscountAmount = E._27,
        TotalTaxAmountOther = E._28,
        TotalTaxAmount = E._29,
        Total = E._30,
        TermAndCondition = E._31,
        Creator = E._32,
        Organization = E._33,
    }
}
