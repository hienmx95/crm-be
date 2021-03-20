using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class OrderQuote : DataEntity, IEquatable<OrderQuote>
    {
        public long Id { get; set; }
        public string Subject { get; set; }
        public long CompanyId { get; set; }
        public long ContactId { get; set; }
        public long? OpportunityId { get; set; }
        public long EditedPriceStatusId { get; set; }
        public DateTime EndAt { get; set; }
        public long AppUserId { get; set; }
        public long OrderQuoteStatusId { get; set; }
        public string Note { get; set; }
        public string InvoiceAddress { get; set; }
        public long? InvoiceNationId { get; set; }
        public long? InvoiceProvinceId { get; set; }
        public long? InvoiceDistrictId { get; set; }
        public string InvoiceZIPCode { get; set; }
        public string Address { get; set; }
        public long? NationId { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public string ZIPCode { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GeneralDiscountPercentage { get; set; }
        public decimal GeneralDiscountAmount { get; set; }
        public decimal? TotalTaxAmountOther { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal Total { get; set; }
        public long CreatorId { get; set; }
        public long OrganizationId { get; set; }
        public AppUser AppUser { get; set; }
        public Company Company { get; set; }
        public Contact Contact { get; set; }
        public AppUser Creator { get; set; }
        public District District { get; set; }
        public EditedPriceStatus EditedPriceStatus { get; set; }
        public District InvoiceDistrict { get; set; }
        public Nation InvoiceNation { get; set; }
        public Province InvoiceProvince { get; set; }
        public Nation Nation { get; set; }
        public Opportunity Opportunity { get; set; }
        public Organization Organization { get; set; }
        public OrderQuoteStatus OrderQuoteStatus { get; set; }
        public Province Province { get; set; }
        public List<OrderQuoteContent> OrderQuoteContents { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool Equals(OrderQuote other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class OrderQuoteFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Subject { get; set; }
        public IdFilter CompanyId { get; set; }
        public IdFilter ContactId { get; set; }
        public IdFilter OpportunityId { get; set; }
        public IdFilter EditedPriceStatusId { get; set; }
        public DateFilter EndAt { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter OrderQuoteStatusId { get; set; }
        public StringFilter Note { get; set; }
        public StringFilter InvoiceAddress { get; set; }
        public IdFilter InvoiceNationId { get; set; }
        public IdFilter InvoiceProvinceId { get; set; }
        public IdFilter InvoiceDistrictId { get; set; }
        public StringFilter InvoiceZIPCode { get; set; }
        public StringFilter Address { get; set; }
        public IdFilter NationId { get; set; }
        public IdFilter ProvinceId { get; set; }
        public IdFilter DistrictId { get; set; }
        public StringFilter ZIPCode { get; set; }
        public DecimalFilter SubTotal { get; set; }
        public DecimalFilter GeneralDiscountPercentage { get; set; }
        public DecimalFilter GeneralDiscountAmount { get; set; }
        public DecimalFilter TotalTaxAmountOther { get; set; }
        public DecimalFilter TotalTaxAmount { get; set; }
        public DecimalFilter Total { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter OrganizationId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<OrderQuoteFilter> OrFilter { get; set; }
        public OrderQuoteOrder OrderBy { get; set; }
        public OrderQuoteSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderQuoteOrder
    {
        Id = 0,
        Subject = 1,
        Company = 2,
        Contact = 3,
        Opportunity = 4,
        EditedPriceStatus = 5,
        EndAt = 6,
        AppUser = 7,
        OrderQuoteStatus = 8,
        Note = 9,
        InvoiceAddress = 10,
        InvoiceNation = 11,
        InvoiceProvince = 12,
        InvoiceDistrict = 13,
        InvoiceZIPCode = 14,
        Address = 15,
        Nation = 16,
        Province = 17,
        District = 18,
        ZIPCode = 19,
        SubTotal = 20,
        GeneralDiscountPercentage = 21,
        GeneralDiscountAmount = 22,
        TotalTaxAmountOther = 23,
        TotalTaxAmount = 24,
        Total = 25,
        Creator = 26,
        Organization = 27,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum OrderQuoteSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Subject = E._1,
        Company = E._2,
        Contact = E._3,
        Opportunity = E._4,
        EditedPriceStatus = E._5,
        EndAt = E._6,
        AppUser = E._7,
        OrderQuoteStatus = E._8,
        Note = E._9,
        InvoiceAddress = E._10,
        InvoiceNation = E._11,
        InvoiceProvince = E._12,
        InvoiceDistrict = E._13,
        InvoiceZIPCode = E._14,
        Address = E._15,
        Nation = E._16,
        Province = E._17,
        District = E._18,
        ZIPCode = E._19,
        SubTotal = E._20,
        GeneralDiscountPercentage = E._21,
        GeneralDiscountAmount = E._22,
        TotalTaxAmountOther = E._23,
        TotalTaxAmount = E._24,
        Total = E._25,
        Creator = E._26,
        Organization = E._27,
    }
}
