using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_OrderQuoteDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Subject { get; set; }
        public long? NationId { get; set; }
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public string Address { get; set; }
        public string InvoiceAddress { get; set; }
        public long? InvoiceProvinceId { get; set; }
        public long? InvoiceDistrictId { get; set; }
        public long? InvoiceNationId { get; set; }
        public string ZIPCode { get; set; }
        public string InvoiceZIPCode { get; set; }
        public long UserId { get; set; }
        public long ContactId { get; set; }
        public long CompanyId { get; set; }
        public long? OpportunityId { get; set; }
        public DateTime Quotestage { get; set; }
        public long OrderQuoteStatusId { get; set; }
        public string TermsCoditions { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal TotalTaxAmount { get; set; }
        public decimal? TotalTaxAmountOther { get; set; }
        public decimal GeneralDiscountPercentage { get; set; }
        public decimal GeneralDiscountAmount { get; set; }
        public long EditedPriceStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Company_EditedPriceStatusDTO EditedPriceStatus { get; set; }
        public Company_CompanyDTO Company { get; set; }
        public Company_ContactDTO Contact { get; set; }
        public Company_DistrictDTO District { get; set; }
        public Company_DistrictDTO InvoiceDistrict { get; set; }
        public Company_NationDTO InvoiceNation { get; set; }
        public Company_ProvinceDTO InvoiceProvince { get; set; }
        public Company_NationDTO Nation { get; set; }
        public Company_OpportunityDTO Opportunity { get; set; }
        public Company_OrderQuoteStatusDTO OrderQuoteStatus { get; set; }
        public List<Company_OrderQuoteContentDTO> OrderQuoteContents { get; set; }
        public Company_ProvinceDTO Province { get; set; }
        public Company_AppUserDTO AppUser { get; set; }
        public Company_OrderQuoteDTO() {}
        public Company_OrderQuoteDTO(OrderQuote OrderQuote)
        {
            this.Id = OrderQuote.Id;
            this.Subject = OrderQuote.Subject;
            this.NationId = OrderQuote.NationId;
            this.ProvinceId = OrderQuote.ProvinceId;
            this.DistrictId = OrderQuote.DistrictId;
            this.Address = OrderQuote.Address;
            this.InvoiceAddress = OrderQuote.InvoiceAddress;
            this.InvoiceProvinceId = OrderQuote.InvoiceProvinceId;
            this.InvoiceDistrictId = OrderQuote.InvoiceDistrictId;
            this.InvoiceNationId = OrderQuote.InvoiceNationId;
            this.ZIPCode = OrderQuote.ZIPCode;
            this.InvoiceZIPCode = OrderQuote.InvoiceZIPCode;
            this.UserId = OrderQuote.AppUserId;
            this.ContactId = OrderQuote.ContactId;
            this.CompanyId = OrderQuote.CompanyId;
            this.OpportunityId = OrderQuote.OpportunityId;
            this.EditedPriceStatusId = OrderQuote.EditedPriceStatusId;
            this.OrderQuoteStatusId = OrderQuote.OrderQuoteStatusId;
            this.SubTotal = OrderQuote.SubTotal;
            this.Total = OrderQuote.Total;
            this.TotalTaxAmount = OrderQuote.TotalTaxAmount;
            this.TotalTaxAmountOther = OrderQuote.TotalTaxAmountOther;
            this.GeneralDiscountPercentage = OrderQuote.GeneralDiscountPercentage;
            this.GeneralDiscountAmount = OrderQuote.GeneralDiscountAmount;
            this.CreatedAt = OrderQuote.CreatedAt;
            this.Company = OrderQuote.Company == null ? null : new Company_CompanyDTO(OrderQuote.Company);
            this.Contact = OrderQuote.Contact == null ? null : new Company_ContactDTO(OrderQuote.Contact);
            this.District = OrderQuote.District == null ? null : new Company_DistrictDTO(OrderQuote.District);
            this.InvoiceDistrict = OrderQuote.InvoiceDistrict == null ? null : new Company_DistrictDTO(OrderQuote.InvoiceDistrict);
            this.InvoiceNation = OrderQuote.InvoiceNation == null ? null : new Company_NationDTO(OrderQuote.InvoiceNation);
            this.InvoiceProvince = OrderQuote.InvoiceProvince == null ? null : new Company_ProvinceDTO(OrderQuote.InvoiceProvince);
            this.Nation = OrderQuote.Nation == null ? null : new Company_NationDTO(OrderQuote.Nation);
            this.Opportunity = OrderQuote.Opportunity == null ? null : new Company_OpportunityDTO(OrderQuote.Opportunity);
            this.OrderQuoteStatus = OrderQuote.OrderQuoteStatus == null ? null : new Company_OrderQuoteStatusDTO(OrderQuote.OrderQuoteStatus);
            this.OrderQuoteContents = OrderQuote.OrderQuoteContents?.Select(x => new Company_OrderQuoteContentDTO(x)).ToList();
            this.Province = OrderQuote.Province == null ? null : new Company_ProvinceDTO(OrderQuote.Province);
            this.AppUser = OrderQuote.AppUser == null ? null : new Company_AppUserDTO(OrderQuote.AppUser);
            this.EditedPriceStatus = OrderQuote.EditedPriceStatus == null ? null : new Company_EditedPriceStatusDTO(OrderQuote.EditedPriceStatus);
            this.Errors = OrderQuote.Errors;
        }
    }

    public class Company_OrderQuoteFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Subject { get; set; }
        public IdFilter NationId { get; set; }
        public IdFilter ProvinceId { get; set; }
        public IdFilter DistrictId { get; set; }
        public StringFilter Address { get; set; }
        public StringFilter InvoiceAddress { get; set; }
        public IdFilter InvoiceProvinceId { get; set; }
        public IdFilter InvoiceDistrictId { get; set; }
        public IdFilter InvoiceNationId { get; set; }
        public IdFilter EditedPriceStatusId { get; set; }
        public StringFilter ZIPCode { get; set; }
        public StringFilter InvoiceZIPCode { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter ContactId { get; set; }
        public IdFilter CompanyId { get; set; }
        public IdFilter OpportunityId { get; set; }
        public DateFilter Quotestage { get; set; }
        public IdFilter OrderQuoteStatusId { get; set; }
        public StringFilter TermsCoditions { get; set; }
        public DecimalFilter SubTotal { get; set; }
        public DecimalFilter Total { get; set; }
        public DecimalFilter TotalTaxAmount { get; set; }
        public DecimalFilter TotalTaxAmountOther { get; set; }
        public DecimalFilter GeneralDiscountPercentage { get; set; }
        public DecimalFilter GeneralDiscountAmount { get; set; }
        public DateFilter CreatedAt { get; set; }
        public OrderQuoteOrder OrderBy { get; set; }
    }
}
