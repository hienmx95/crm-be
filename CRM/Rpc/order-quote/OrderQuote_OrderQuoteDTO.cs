using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.order_quote
{
    public class OrderQuote_OrderQuoteDTO : DataDTO
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
        public OrderQuote_AppUserDTO AppUser { get; set; }
        public OrderQuote_CompanyDTO Company { get; set; }
        public OrderQuote_ContactDTO Contact { get; set; }
        public OrderQuote_AppUserDTO Creator { get; set; }
        public OrderQuote_DistrictDTO District { get; set; }
        public OrderQuote_EditedPriceStatusDTO EditedPriceStatus { get; set; }
        public OrderQuote_DistrictDTO InvoiceDistrict { get; set; }
        public OrderQuote_NationDTO InvoiceNation { get; set; }
        public OrderQuote_ProvinceDTO InvoiceProvince { get; set; }
        public OrderQuote_NationDTO Nation { get; set; }
        public OrderQuote_OpportunityDTO Opportunity { get; set; }
        public OrderQuote_OrderQuoteStatusDTO OrderQuoteStatus { get; set; }
        public OrderQuote_ProvinceDTO Province { get; set; }
        public List<OrderQuote_OrderQuoteContentDTO> OrderQuoteContents { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public OrderQuote_OrderQuoteDTO() { }
        public OrderQuote_OrderQuoteDTO(OrderQuote OrderQuote)
        {
            this.Id = OrderQuote.Id;
            this.Subject = OrderQuote.Subject;
            this.CompanyId = OrderQuote.CompanyId;
            this.ContactId = OrderQuote.ContactId;
            this.OpportunityId = OrderQuote.OpportunityId;
            this.EditedPriceStatusId = OrderQuote.EditedPriceStatusId;
            this.EndAt = OrderQuote.EndAt;
            this.AppUserId = OrderQuote.AppUserId;
            this.OrderQuoteStatusId = OrderQuote.OrderQuoteStatusId;
            this.Note = OrderQuote.Note;
            this.InvoiceAddress = OrderQuote.InvoiceAddress;
            this.InvoiceNationId = OrderQuote.InvoiceNationId;
            this.InvoiceProvinceId = OrderQuote.InvoiceProvinceId;
            this.InvoiceDistrictId = OrderQuote.InvoiceDistrictId;
            this.InvoiceZIPCode = OrderQuote.InvoiceZIPCode;
            this.Address = OrderQuote.Address;
            this.NationId = OrderQuote.NationId;
            this.ProvinceId = OrderQuote.ProvinceId;
            this.DistrictId = OrderQuote.DistrictId;
            this.ZIPCode = OrderQuote.ZIPCode;
            this.SubTotal = OrderQuote.SubTotal;
            this.GeneralDiscountPercentage = OrderQuote.GeneralDiscountPercentage;
            this.GeneralDiscountAmount = OrderQuote.GeneralDiscountAmount;
            this.TotalTaxAmountOther = OrderQuote.TotalTaxAmountOther;
            this.TotalTaxAmount = OrderQuote.TotalTaxAmount;
            this.Total = OrderQuote.Total;
            this.CreatorId = OrderQuote.CreatorId;
            this.AppUser = OrderQuote.AppUser == null ? null : new OrderQuote_AppUserDTO(OrderQuote.AppUser);
            this.Company = OrderQuote.Company == null ? null : new OrderQuote_CompanyDTO(OrderQuote.Company);
            this.Contact = OrderQuote.Contact == null ? null : new OrderQuote_ContactDTO(OrderQuote.Contact);
            this.Creator = OrderQuote.Creator == null ? null : new OrderQuote_AppUserDTO(OrderQuote.Creator);
            this.District = OrderQuote.District == null ? null : new OrderQuote_DistrictDTO(OrderQuote.District);
            this.EditedPriceStatus = OrderQuote.EditedPriceStatus == null ? null : new OrderQuote_EditedPriceStatusDTO(OrderQuote.EditedPriceStatus);
            this.InvoiceDistrict = OrderQuote.InvoiceDistrict == null ? null : new OrderQuote_DistrictDTO(OrderQuote.InvoiceDistrict);
            this.InvoiceNation = OrderQuote.InvoiceNation == null ? null : new OrderQuote_NationDTO(OrderQuote.InvoiceNation);
            this.InvoiceProvince = OrderQuote.InvoiceProvince == null ? null : new OrderQuote_ProvinceDTO(OrderQuote.InvoiceProvince);
            this.Nation = OrderQuote.Nation == null ? null : new OrderQuote_NationDTO(OrderQuote.Nation);
            this.Opportunity = OrderQuote.Opportunity == null ? null : new OrderQuote_OpportunityDTO(OrderQuote.Opportunity);
            this.OrderQuoteStatus = OrderQuote.OrderQuoteStatus == null ? null : new OrderQuote_OrderQuoteStatusDTO(OrderQuote.OrderQuoteStatus);
            this.Province = OrderQuote.Province == null ? null : new OrderQuote_ProvinceDTO(OrderQuote.Province);
            this.OrderQuoteContents = OrderQuote.OrderQuoteContents?.Select(x => new OrderQuote_OrderQuoteContentDTO(x)).ToList();
            this.CreatedAt = OrderQuote.CreatedAt;
            this.UpdatedAt = OrderQuote.UpdatedAt;
            this.Errors = OrderQuote.Errors;
        }
    }

    public class OrderQuote_OrderQuoteFilterDTO : FilterDTO
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
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public OrderQuoteOrder OrderBy { get; set; }
    }
}
