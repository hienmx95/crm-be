using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CustomerSalesOrder : DataEntity,  IEquatable<CustomerSalesOrder>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public long CustomerTypeId { get; set; }
        public long CustomerId { get; set; }
        public long? OpportunityId { get; set; }
        public long? ContractId { get; set; }
        public long? OrderPaymentStatusId { get; set; }
        public long? RequestStateId { get; set; }
        public long EditedPriceStatusId { get; set; }
        public string ShippingName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public long SalesEmployeeId { get; set; }
        public string Note { get; set; }
        public string InvoiceAddress { get; set; }
        public long? InvoiceNationId { get; set; }
        public long? InvoiceProvinceId { get; set; }
        public long? InvoiceDistrictId { get; set; }
        public long? InvoiceWardId { get; set; }
        public string InvoiceZIPCode { get; set; }
        public string DeliveryAddress { get; set; }
        public long? DeliveryNationId { get; set; }
        public long? DeliveryProvinceId { get; set; }
        public long? DeliveryDistrictId { get; set; }
        public long? DeliveryWardId { get; set; }
        public string DeliveryZIPCode { get; set; }
        public decimal SubTotal { get; set; }
        public decimal? GeneralDiscountPercentage { get; set; }
        public decimal? GeneralDiscountAmount { get; set; }
        public decimal TotalTaxOther { get; set; }
        public decimal TotalTax { get; set; }
        public decimal Total { get; set; }
        public long CreatorId { get; set; }
        public long OrganizationId { get; set; }
        public Guid RowId { get; set; }
        public Contract Contract { get; set; }
        public AppUser Creator { get; set; }
        public Customer Customer { get; set; }
        public CustomerType CustomerType { get; set; }
        public District DeliveryDistrict { get; set; }
        public Nation DeliveryNation { get; set; }
        public Province DeliveryProvince { get; set; }
        public Ward DeliveryWard { get; set; }
        public EditedPriceStatus EditedPriceStatus { get; set; }
        public District InvoiceDistrict { get; set; }
        public Nation InvoiceNation { get; set; }
        public Province InvoiceProvince { get; set; }
        public Ward InvoiceWard { get; set; }
        public Opportunity Opportunity { get; set; }
        public OrderPaymentStatus OrderPaymentStatus { get; set; }
        public Organization Organization { get; set; }
        public RequestState RequestState { get; set; }
        public AppUser SalesEmployee { get; set; }
        public List<CustomerSalesOrderContent> CustomerSalesOrderContents { get; set; }
        public List<CustomerSalesOrderPaymentHistory> CustomerSalesOrderPaymentHistories { get; set; }
        public List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public bool Equals(CustomerSalesOrder other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class CustomerSalesOrderFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter CustomerTypeId { get; set; }
        public IdFilter CustomerId { get; set; }
        public IdFilter OpportunityId { get; set; }
        public IdFilter ContractId { get; set; }
        public IdFilter OrderPaymentStatusId { get; set; }
        public IdFilter RequestStateId { get; set; }
        public IdFilter EditedPriceStatusId { get; set; }
        public StringFilter ShippingName { get; set; }
        public DateFilter OrderDate { get; set; }
        public DateFilter DeliveryDate { get; set; }
        public IdFilter SalesEmployeeId { get; set; }
        public StringFilter Note { get; set; }
        public StringFilter InvoiceAddress { get; set; }
        public IdFilter InvoiceNationId { get; set; }
        public IdFilter InvoiceProvinceId { get; set; }
        public IdFilter InvoiceDistrictId { get; set; }
        public IdFilter InvoiceWardId { get; set; }
        public StringFilter InvoiceZIPCode { get; set; }
        public StringFilter DeliveryAddress { get; set; }
        public IdFilter DeliveryNationId { get; set; }
        public IdFilter DeliveryProvinceId { get; set; }
        public IdFilter DeliveryDistrictId { get; set; }
        public IdFilter DeliveryWardId { get; set; }
        public StringFilter DeliveryZIPCode { get; set; }
        public DecimalFilter SubTotal { get; set; }
        public DecimalFilter GeneralDiscountPercentage { get; set; }
        public DecimalFilter GeneralDiscountAmount { get; set; }
        public DecimalFilter TotalTaxOther { get; set; }
        public DecimalFilter TotalTax { get; set; }
        public DecimalFilter Total { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter OrganizationId { get; set; }
        public IdFilter CompanyId { get; set; }
        public IdFilter ContactId { get; set; }
        public GuidFilter RowId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<CustomerSalesOrderFilter> OrFilter { get; set; }
        public CustomerSalesOrderOrder OrderBy {get; set;}
        public CustomerSalesOrderSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerSalesOrderOrder
    {
        Id = 0,
        Code = 1,
        CustomerType = 2,
        Customer = 3,
        Opportunity = 4,
        Contract = 5,
        OrderPaymentStatus = 6,
        RequestState = 7,
        EditedPriceStatus = 8,
        ShippingName = 9,
        OrderDate = 10,
        DeliveryDate = 11,
        SalesEmployee = 12,
        Note = 13,
        InvoiceAddress = 14,
        InvoiceNation = 15,
        InvoiceProvince = 16,
        InvoiceDistrict = 17,
        InvoiceWard = 18,
        InvoiceZIPCode = 19,
        DeliveryAddress = 20,
        DeliveryNation = 21,
        DeliveryProvince = 22,
        DeliveryDistrict = 23,
        DeliveryWard = 24,
        DeliveryZIPCode = 25,
        SubTotal = 26,
        GeneralDiscountPercentage = 27,
        GeneralDiscountAmount = 28,
        TotalTaxOther = 29,
        TotalTax = 30,
        Total = 31,
        Creator = 32,
        Organization = 33,
        Row = 37,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum CustomerSalesOrderSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        CustomerType = E._2,
        Customer = E._3,
        Opportunity = E._4,
        Contract = E._5,
        OrderPaymentStatus = E._6,
        RequestState = E._7,
        EditedPriceStatus = E._8,
        ShippingName = E._9,
        OrderDate = E._10,
        DeliveryDate = E._11,
        SalesEmployee = E._12,
        Note = E._13,
        InvoiceAddress = E._14,
        InvoiceNation = E._15,
        InvoiceProvince = E._16,
        InvoiceDistrict = E._17,
        InvoiceWard = E._18,
        InvoiceZIPCode = E._19,
        DeliveryAddress = E._20,
        DeliveryNation = E._21,
        DeliveryProvince = E._22,
        DeliveryDistrict = E._23,
        DeliveryWard = E._24,
        DeliveryZIPCode = E._25,
        SubTotal = E._26,
        GeneralDiscountPercentage = E._27,
        GeneralDiscountAmount = E._28,
        TotalTaxOther = E._29,
        TotalTax = E._30,
        Total = E._31,
        Creator = E._32,
        Organization = E._33,
        Row = E._37,
    }
}
