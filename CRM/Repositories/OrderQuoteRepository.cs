using CRM.Common;
using CRM.Helpers;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public interface IOrderQuoteRepository
    {
        Task<int> Count(OrderQuoteFilter OrderQuoteFilter);
        Task<List<OrderQuote>> List(OrderQuoteFilter OrderQuoteFilter);
        Task<List<OrderQuote>> List(List<long> Ids);
        Task<OrderQuote> Get(long Id);
        Task<bool> Create(OrderQuote OrderQuote);
        Task<bool> Update(OrderQuote OrderQuote);
        Task<bool> Delete(OrderQuote OrderQuote);
        Task<bool> BulkMerge(List<OrderQuote> OrderQuotes);
        Task<bool> BulkDelete(List<OrderQuote> OrderQuotes);
    }
    public class OrderQuoteRepository : IOrderQuoteRepository
    {
        private DataContext DataContext;
        public OrderQuoteRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<OrderQuoteDAO> DynamicFilter(IQueryable<OrderQuoteDAO> query, OrderQuoteFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            if (filter.CreatedAt != null && filter.CreatedAt.HasValue)
                query = query.Where(q => q.CreatedAt, filter.CreatedAt);
            if (filter.UpdatedAt != null && filter.UpdatedAt.HasValue)
                query = query.Where(q => q.UpdatedAt, filter.UpdatedAt);
            if (filter.Id != null && filter.Id.HasValue)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Subject != null && filter.Subject.HasValue)
                query = query.Where(q => q.Subject, filter.Subject);
            if (filter.CompanyId != null && filter.CompanyId.HasValue)
                query = query.Where(q => q.CompanyId, filter.CompanyId);
            if (filter.ContactId != null && filter.ContactId.HasValue)
                query = query.Where(q => q.ContactId, filter.ContactId);
            if (filter.OpportunityId != null && filter.OpportunityId.HasValue)
                query = query.Where(q => q.OpportunityId.HasValue).Where(q => q.OpportunityId, filter.OpportunityId);
            if (filter.EditedPriceStatusId != null && filter.EditedPriceStatusId.HasValue)
                query = query.Where(q => q.EditedPriceStatusId, filter.EditedPriceStatusId);
            if (filter.EndAt != null && filter.EndAt.HasValue)
                query = query.Where(q => q.EndAt, filter.EndAt);
            if (filter.AppUserId != null && filter.AppUserId.HasValue)
                query = query.Where(q => q.AppUserId, filter.AppUserId);
            if (filter.OrderQuoteStatusId != null && filter.OrderQuoteStatusId.HasValue)
                query = query.Where(q => q.OrderQuoteStatusId, filter.OrderQuoteStatusId);
            if (filter.Note != null && filter.Note.HasValue)
                query = query.Where(q => q.Note, filter.Note);
            if (filter.InvoiceAddress != null && filter.InvoiceAddress.HasValue)
                query = query.Where(q => q.InvoiceAddress, filter.InvoiceAddress);
            if (filter.InvoiceNationId != null && filter.InvoiceNationId.HasValue)
                query = query.Where(q => q.InvoiceNationId.HasValue).Where(q => q.InvoiceNationId, filter.InvoiceNationId);
            if (filter.InvoiceProvinceId != null && filter.InvoiceProvinceId.HasValue)
                query = query.Where(q => q.InvoiceProvinceId.HasValue).Where(q => q.InvoiceProvinceId, filter.InvoiceProvinceId);
            if (filter.InvoiceDistrictId != null && filter.InvoiceDistrictId.HasValue)
                query = query.Where(q => q.InvoiceDistrictId.HasValue).Where(q => q.InvoiceDistrictId, filter.InvoiceDistrictId);
            if (filter.InvoiceZIPCode != null && filter.InvoiceZIPCode.HasValue)
                query = query.Where(q => q.InvoiceZIPCode, filter.InvoiceZIPCode);
            if (filter.Address != null && filter.Address.HasValue)
                query = query.Where(q => q.Address, filter.Address);
            if (filter.NationId != null && filter.NationId.HasValue)
                query = query.Where(q => q.NationId.HasValue).Where(q => q.NationId, filter.NationId);
            if (filter.ProvinceId != null && filter.ProvinceId.HasValue)
                query = query.Where(q => q.ProvinceId.HasValue).Where(q => q.ProvinceId, filter.ProvinceId);
            if (filter.DistrictId != null && filter.DistrictId.HasValue)
                query = query.Where(q => q.DistrictId.HasValue).Where(q => q.DistrictId, filter.DistrictId);
            if (filter.ZIPCode != null && filter.ZIPCode.HasValue)
                query = query.Where(q => q.ZIPCode, filter.ZIPCode);
            if (filter.SubTotal != null && filter.SubTotal.HasValue)
                query = query.Where(q => q.SubTotal, filter.SubTotal);
            if (filter.GeneralDiscountPercentage != null && filter.GeneralDiscountPercentage.HasValue)
                query = query.Where(q => q.GeneralDiscountPercentage, filter.GeneralDiscountPercentage);
            if (filter.GeneralDiscountAmount != null && filter.GeneralDiscountAmount.HasValue)
                query = query.Where(q => q.GeneralDiscountAmount, filter.GeneralDiscountAmount);
            if (filter.TotalTaxAmountOther != null && filter.TotalTaxAmountOther.HasValue)
                query = query.Where(q => q.TotalTaxAmountOther.HasValue).Where(q => q.TotalTaxAmountOther, filter.TotalTaxAmountOther);
            if (filter.TotalTaxAmount != null && filter.TotalTaxAmount.HasValue)
                query = query.Where(q => q.TotalTaxAmount, filter.TotalTaxAmount);
            if (filter.Total != null && filter.Total.HasValue)
                query = query.Where(q => q.Total, filter.Total);
            if (filter.CreatorId != null && filter.CreatorId.HasValue)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
     
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<OrderQuoteDAO> OrFilter(IQueryable<OrderQuoteDAO> query, OrderQuoteFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<OrderQuoteDAO> initQuery = query.Where(q => false);
            foreach (OrderQuoteFilter OrderQuoteFilter in filter.OrFilter)
            {
                IQueryable<OrderQuoteDAO> queryable = query;
                if (OrderQuoteFilter.Id != null && OrderQuoteFilter.Id.HasValue)
                    queryable = queryable.Where(q => q.Id, OrderQuoteFilter.Id);
                if (OrderQuoteFilter.Subject != null && OrderQuoteFilter.Subject.HasValue)
                    queryable = queryable.Where(q => q.Subject, OrderQuoteFilter.Subject);
                if (OrderQuoteFilter.CompanyId != null && OrderQuoteFilter.CompanyId.HasValue)
                    queryable = queryable.Where(q => q.CompanyId, OrderQuoteFilter.CompanyId);
                if (OrderQuoteFilter.ContactId != null && OrderQuoteFilter.ContactId.HasValue)
                    queryable = queryable.Where(q => q.ContactId, OrderQuoteFilter.ContactId);
                if (OrderQuoteFilter.OpportunityId != null && OrderQuoteFilter.OpportunityId.HasValue)
                    queryable = queryable.Where(q => q.OpportunityId.HasValue).Where(q => q.OpportunityId, OrderQuoteFilter.OpportunityId);
                if (OrderQuoteFilter.EditedPriceStatusId != null && OrderQuoteFilter.EditedPriceStatusId.HasValue)
                    queryable = queryable.Where(q => q.EditedPriceStatusId, OrderQuoteFilter.EditedPriceStatusId);
                if (OrderQuoteFilter.EndAt != null && OrderQuoteFilter.EndAt.HasValue)
                    queryable = queryable.Where(q => q.EndAt, OrderQuoteFilter.EndAt);
                if (OrderQuoteFilter.AppUserId != null && OrderQuoteFilter.AppUserId.HasValue)
                    queryable = queryable.Where(q => q.AppUserId, OrderQuoteFilter.AppUserId);
                if (OrderQuoteFilter.OrderQuoteStatusId != null && OrderQuoteFilter.OrderQuoteStatusId.HasValue)
                    queryable = queryable.Where(q => q.OrderQuoteStatusId, OrderQuoteFilter.OrderQuoteStatusId);
                if (OrderQuoteFilter.Note != null && OrderQuoteFilter.Note.HasValue)
                    queryable = queryable.Where(q => q.Note, OrderQuoteFilter.Note);
                if (OrderQuoteFilter.InvoiceAddress != null && OrderQuoteFilter.InvoiceAddress.HasValue)
                    queryable = queryable.Where(q => q.InvoiceAddress, OrderQuoteFilter.InvoiceAddress);
                if (OrderQuoteFilter.InvoiceNationId != null && OrderQuoteFilter.InvoiceNationId.HasValue)
                    queryable = queryable.Where(q => q.InvoiceNationId.HasValue).Where(q => q.InvoiceNationId, OrderQuoteFilter.InvoiceNationId);
                if (OrderQuoteFilter.InvoiceProvinceId != null && OrderQuoteFilter.InvoiceProvinceId.HasValue)
                    queryable = queryable.Where(q => q.InvoiceProvinceId.HasValue).Where(q => q.InvoiceProvinceId, OrderQuoteFilter.InvoiceProvinceId);
                if (OrderQuoteFilter.InvoiceDistrictId != null && OrderQuoteFilter.InvoiceDistrictId.HasValue)
                    queryable = queryable.Where(q => q.InvoiceDistrictId.HasValue).Where(q => q.InvoiceDistrictId, OrderQuoteFilter.InvoiceDistrictId);
                if (OrderQuoteFilter.InvoiceZIPCode != null && OrderQuoteFilter.InvoiceZIPCode.HasValue)
                    queryable = queryable.Where(q => q.InvoiceZIPCode, OrderQuoteFilter.InvoiceZIPCode);
                if (OrderQuoteFilter.Address != null && OrderQuoteFilter.Address.HasValue)
                    queryable = queryable.Where(q => q.Address, OrderQuoteFilter.Address);
                if (OrderQuoteFilter.NationId != null && OrderQuoteFilter.NationId.HasValue)
                    queryable = queryable.Where(q => q.NationId.HasValue).Where(q => q.NationId, OrderQuoteFilter.NationId);
                if (OrderQuoteFilter.ProvinceId != null && OrderQuoteFilter.ProvinceId.HasValue)
                    queryable = queryable.Where(q => q.ProvinceId.HasValue).Where(q => q.ProvinceId, OrderQuoteFilter.ProvinceId);
                if (OrderQuoteFilter.DistrictId != null && OrderQuoteFilter.DistrictId.HasValue)
                    queryable = queryable.Where(q => q.DistrictId.HasValue).Where(q => q.DistrictId, OrderQuoteFilter.DistrictId);
                if (OrderQuoteFilter.ZIPCode != null && OrderQuoteFilter.ZIPCode.HasValue)
                    queryable = queryable.Where(q => q.ZIPCode, OrderQuoteFilter.ZIPCode);
                if (OrderQuoteFilter.SubTotal != null && OrderQuoteFilter.SubTotal.HasValue)
                    queryable = queryable.Where(q => q.SubTotal, OrderQuoteFilter.SubTotal);
                if (OrderQuoteFilter.GeneralDiscountPercentage != null && OrderQuoteFilter.GeneralDiscountPercentage.HasValue)
                    queryable = queryable.Where(q => q.GeneralDiscountPercentage, OrderQuoteFilter.GeneralDiscountPercentage);
                if (OrderQuoteFilter.GeneralDiscountAmount != null && OrderQuoteFilter.GeneralDiscountAmount.HasValue)
                    queryable = queryable.Where(q => q.GeneralDiscountAmount, OrderQuoteFilter.GeneralDiscountAmount);
                if (OrderQuoteFilter.TotalTaxAmountOther != null && OrderQuoteFilter.TotalTaxAmountOther.HasValue)
                    queryable = queryable.Where(q => q.TotalTaxAmountOther.HasValue).Where(q => q.TotalTaxAmountOther, OrderQuoteFilter.TotalTaxAmountOther);
                if (OrderQuoteFilter.TotalTaxAmount != null && OrderQuoteFilter.TotalTaxAmount.HasValue)
                    queryable = queryable.Where(q => q.TotalTaxAmount, OrderQuoteFilter.TotalTaxAmount);
                if (OrderQuoteFilter.Total != null && OrderQuoteFilter.Total.HasValue)
                    queryable = queryable.Where(q => q.Total, OrderQuoteFilter.Total);
                if (OrderQuoteFilter.CreatorId != null && OrderQuoteFilter.CreatorId.HasValue)
                    queryable = queryable.Where(q => q.CreatorId, OrderQuoteFilter.CreatorId);
            
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<OrderQuoteDAO> DynamicOrder(IQueryable<OrderQuoteDAO> query, OrderQuoteFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case OrderQuoteOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case OrderQuoteOrder.Subject:
                            query = query.OrderBy(q => q.Subject);
                            break;
                        case OrderQuoteOrder.Company:
                            query = query.OrderBy(q => q.CompanyId);
                            break;
                        case OrderQuoteOrder.Contact:
                            query = query.OrderBy(q => q.ContactId);
                            break;
                        case OrderQuoteOrder.Opportunity:
                            query = query.OrderBy(q => q.OpportunityId);
                            break;
                        case OrderQuoteOrder.EditedPriceStatus:
                            query = query.OrderBy(q => q.EditedPriceStatusId);
                            break;
                        case OrderQuoteOrder.EndAt:
                            query = query.OrderBy(q => q.EndAt);
                            break;
                        case OrderQuoteOrder.AppUser:
                            query = query.OrderBy(q => q.AppUserId);
                            break;
                        case OrderQuoteOrder.OrderQuoteStatus:
                            query = query.OrderBy(q => q.OrderQuoteStatusId);
                            break;
                        case OrderQuoteOrder.Note:
                            query = query.OrderBy(q => q.Note);
                            break;
                        case OrderQuoteOrder.InvoiceAddress:
                            query = query.OrderBy(q => q.InvoiceAddress);
                            break;
                        case OrderQuoteOrder.InvoiceNation:
                            query = query.OrderBy(q => q.InvoiceNationId);
                            break;
                        case OrderQuoteOrder.InvoiceProvince:
                            query = query.OrderBy(q => q.InvoiceProvinceId);
                            break;
                        case OrderQuoteOrder.InvoiceDistrict:
                            query = query.OrderBy(q => q.InvoiceDistrictId);
                            break;
                        case OrderQuoteOrder.InvoiceZIPCode:
                            query = query.OrderBy(q => q.InvoiceZIPCode);
                            break;
                        case OrderQuoteOrder.Address:
                            query = query.OrderBy(q => q.Address);
                            break;
                        case OrderQuoteOrder.Nation:
                            query = query.OrderBy(q => q.NationId);
                            break;
                        case OrderQuoteOrder.Province:
                            query = query.OrderBy(q => q.ProvinceId);
                            break;
                        case OrderQuoteOrder.District:
                            query = query.OrderBy(q => q.DistrictId);
                            break;
                        case OrderQuoteOrder.ZIPCode:
                            query = query.OrderBy(q => q.ZIPCode);
                            break;
                        case OrderQuoteOrder.SubTotal:
                            query = query.OrderBy(q => q.SubTotal);
                            break;
                        case OrderQuoteOrder.GeneralDiscountPercentage:
                            query = query.OrderBy(q => q.GeneralDiscountPercentage);
                            break;
                        case OrderQuoteOrder.GeneralDiscountAmount:
                            query = query.OrderBy(q => q.GeneralDiscountAmount);
                            break;
                        case OrderQuoteOrder.TotalTaxAmountOther:
                            query = query.OrderBy(q => q.TotalTaxAmountOther);
                            break;
                        case OrderQuoteOrder.TotalTaxAmount:
                            query = query.OrderBy(q => q.TotalTaxAmount);
                            break;
                        case OrderQuoteOrder.Total:
                            query = query.OrderBy(q => q.Total);
                            break;
                        case OrderQuoteOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case OrderQuoteOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case OrderQuoteOrder.Subject:
                            query = query.OrderByDescending(q => q.Subject);
                            break;
                        case OrderQuoteOrder.Company:
                            query = query.OrderByDescending(q => q.CompanyId);
                            break;
                        case OrderQuoteOrder.Contact:
                            query = query.OrderByDescending(q => q.ContactId);
                            break;
                        case OrderQuoteOrder.Opportunity:
                            query = query.OrderByDescending(q => q.OpportunityId);
                            break;
                        case OrderQuoteOrder.EditedPriceStatus:
                            query = query.OrderByDescending(q => q.EditedPriceStatusId);
                            break;
                        case OrderQuoteOrder.EndAt:
                            query = query.OrderByDescending(q => q.EndAt);
                            break;
                        case OrderQuoteOrder.AppUser:
                            query = query.OrderByDescending(q => q.AppUserId);
                            break;
                        case OrderQuoteOrder.OrderQuoteStatus:
                            query = query.OrderByDescending(q => q.OrderQuoteStatusId);
                            break;
                        case OrderQuoteOrder.Note:
                            query = query.OrderByDescending(q => q.Note);
                            break;
                        case OrderQuoteOrder.InvoiceAddress:
                            query = query.OrderByDescending(q => q.InvoiceAddress);
                            break;
                        case OrderQuoteOrder.InvoiceNation:
                            query = query.OrderByDescending(q => q.InvoiceNationId);
                            break;
                        case OrderQuoteOrder.InvoiceProvince:
                            query = query.OrderByDescending(q => q.InvoiceProvinceId);
                            break;
                        case OrderQuoteOrder.InvoiceDistrict:
                            query = query.OrderByDescending(q => q.InvoiceDistrictId);
                            break;
                        case OrderQuoteOrder.InvoiceZIPCode:
                            query = query.OrderByDescending(q => q.InvoiceZIPCode);
                            break;
                        case OrderQuoteOrder.Address:
                            query = query.OrderByDescending(q => q.Address);
                            break;
                        case OrderQuoteOrder.Nation:
                            query = query.OrderByDescending(q => q.NationId);
                            break;
                        case OrderQuoteOrder.Province:
                            query = query.OrderByDescending(q => q.ProvinceId);
                            break;
                        case OrderQuoteOrder.District:
                            query = query.OrderByDescending(q => q.DistrictId);
                            break;
                        case OrderQuoteOrder.ZIPCode:
                            query = query.OrderByDescending(q => q.ZIPCode);
                            break;
                        case OrderQuoteOrder.SubTotal:
                            query = query.OrderByDescending(q => q.SubTotal);
                            break;
                        case OrderQuoteOrder.GeneralDiscountPercentage:
                            query = query.OrderByDescending(q => q.GeneralDiscountPercentage);
                            break;
                        case OrderQuoteOrder.GeneralDiscountAmount:
                            query = query.OrderByDescending(q => q.GeneralDiscountAmount);
                            break;
                        case OrderQuoteOrder.TotalTaxAmountOther:
                            query = query.OrderByDescending(q => q.TotalTaxAmountOther);
                            break;
                        case OrderQuoteOrder.TotalTaxAmount:
                            query = query.OrderByDescending(q => q.TotalTaxAmount);
                            break;
                        case OrderQuoteOrder.Total:
                            query = query.OrderByDescending(q => q.Total);
                            break;
                        case OrderQuoteOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<OrderQuote>> DynamicSelect(IQueryable<OrderQuoteDAO> query, OrderQuoteFilter filter)
        {
            List<OrderQuote> OrderQuotes = await query.Select(q => new OrderQuote()
            {
                Id = filter.Selects.Contains(OrderQuoteSelect.Id) ? q.Id : default(long),
                Subject = filter.Selects.Contains(OrderQuoteSelect.Subject) ? q.Subject : default(string),
                CompanyId = filter.Selects.Contains(OrderQuoteSelect.Company) ? q.CompanyId : default(long),
                ContactId = filter.Selects.Contains(OrderQuoteSelect.Contact) ? q.ContactId : default(long),
                OpportunityId = filter.Selects.Contains(OrderQuoteSelect.Opportunity) ? q.OpportunityId : default(long?),
                EditedPriceStatusId = filter.Selects.Contains(OrderQuoteSelect.EditedPriceStatus) ? q.EditedPriceStatusId : default(long),
                EndAt = filter.Selects.Contains(OrderQuoteSelect.EndAt) ? q.EndAt : default(DateTime),
                AppUserId = filter.Selects.Contains(OrderQuoteSelect.AppUser) ? q.AppUserId : default(long),
                OrderQuoteStatusId = filter.Selects.Contains(OrderQuoteSelect.OrderQuoteStatus) ? q.OrderQuoteStatusId : default(long),
                Note = filter.Selects.Contains(OrderQuoteSelect.Note) ? q.Note : default(string),
                InvoiceAddress = filter.Selects.Contains(OrderQuoteSelect.InvoiceAddress) ? q.InvoiceAddress : default(string),
                InvoiceNationId = filter.Selects.Contains(OrderQuoteSelect.InvoiceNation) ? q.InvoiceNationId : default(long?),
                InvoiceProvinceId = filter.Selects.Contains(OrderQuoteSelect.InvoiceProvince) ? q.InvoiceProvinceId : default(long?),
                InvoiceDistrictId = filter.Selects.Contains(OrderQuoteSelect.InvoiceDistrict) ? q.InvoiceDistrictId : default(long?),
                InvoiceZIPCode = filter.Selects.Contains(OrderQuoteSelect.InvoiceZIPCode) ? q.InvoiceZIPCode : default(string),
                Address = filter.Selects.Contains(OrderQuoteSelect.Address) ? q.Address : default(string),
                NationId = filter.Selects.Contains(OrderQuoteSelect.Nation) ? q.NationId : default(long?),
                ProvinceId = filter.Selects.Contains(OrderQuoteSelect.Province) ? q.ProvinceId : default(long?),
                DistrictId = filter.Selects.Contains(OrderQuoteSelect.District) ? q.DistrictId : default(long?),
                ZIPCode = filter.Selects.Contains(OrderQuoteSelect.ZIPCode) ? q.ZIPCode : default(string),
                SubTotal = filter.Selects.Contains(OrderQuoteSelect.SubTotal) ? q.SubTotal : default(decimal),
                GeneralDiscountPercentage = filter.Selects.Contains(OrderQuoteSelect.GeneralDiscountPercentage) ? q.GeneralDiscountPercentage : default(decimal),
                GeneralDiscountAmount = filter.Selects.Contains(OrderQuoteSelect.GeneralDiscountAmount) ? q.GeneralDiscountAmount : default(decimal),
                TotalTaxAmountOther = filter.Selects.Contains(OrderQuoteSelect.TotalTaxAmountOther) ? q.TotalTaxAmountOther : default(decimal?),
                TotalTaxAmount = filter.Selects.Contains(OrderQuoteSelect.TotalTaxAmount) ? q.TotalTaxAmount : default(decimal),
                Total = filter.Selects.Contains(OrderQuoteSelect.Total) ? q.Total : default(decimal),
                CreatorId = filter.Selects.Contains(OrderQuoteSelect.Creator) ? q.CreatorId : default(long),
                AppUser = filter.Selects.Contains(OrderQuoteSelect.AppUser) && q.AppUser != null ? new AppUser
                {
                    Id = q.AppUser.Id,
                    Username = q.AppUser.Username,
                    DisplayName = q.AppUser.DisplayName,
                    Address = q.AppUser.Address,
                    Email = q.AppUser.Email,
                    Phone = q.AppUser.Phone,
                    SexId = q.AppUser.SexId,
                    Birthday = q.AppUser.Birthday,
                    Avatar = q.AppUser.Avatar,
                    Department = q.AppUser.Department,
                    OrganizationId = q.AppUser.OrganizationId,
                    Longitude = q.AppUser.Longitude,
                    Latitude = q.AppUser.Latitude,
                    StatusId = q.AppUser.StatusId,
                    RowId = q.AppUser.RowId,
                    Used = q.AppUser.Used,
                } : null,
                Company = filter.Selects.Contains(OrderQuoteSelect.Company) && q.Company != null ? new Company
                {
                    Id = q.Company.Id,
                    Name = q.Company.Name,
                    Phone = q.Company.Phone,
                    FAX = q.Company.FAX,
                    PhoneOther = q.Company.PhoneOther,
                    Email = q.Company.Email,
                    EmailOther = q.Company.EmailOther,
                    ZIPCode = q.Company.ZIPCode,
                    Revenue = q.Company.Revenue,
                    Website = q.Company.Website,
                    Address = q.Company.Address,
                    NationId = q.Company.NationId,
                    ProvinceId = q.Company.ProvinceId,
                    DistrictId = q.Company.DistrictId,
                    NumberOfEmployee = q.Company.NumberOfEmployee,
                    RefuseReciveEmail = q.Company.RefuseReciveEmail,
                    RefuseReciveSMS = q.Company.RefuseReciveSMS,
                    CustomerLeadId = q.Company.CustomerLeadId,
                    ParentId = q.Company.ParentId,
                    Path = q.Company.Path,
                    Level = q.Company.Level,
                    ProfessionId = q.Company.ProfessionId,
                    AppUserId = q.Company.AppUserId,
                    CreatorId = q.Company.CreatorId,
                    CurrencyId = q.Company.CurrencyId,
                    CompanyStatusId = q.Company.CompanyStatusId,
                    Description = q.Company.Description,
                    RowId = q.Company.RowId,
                } : null,
                Contact = filter.Selects.Contains(OrderQuoteSelect.Contact) && q.Contact != null ? new Contact
                {
                    Id = q.Contact.Id,
                    Name = q.Contact.Name,
                    ProfessionId = q.Contact.ProfessionId,
                    CompanyId = q.Contact.CompanyId,
                    ContactStatusId = q.Contact.ContactStatusId,
                    Address = q.Contact.Address,
                    NationId = q.Contact.NationId,
                    ProvinceId = q.Contact.ProvinceId,
                    DistrictId = q.Contact.DistrictId,
                    CustomerLeadId = q.Contact.CustomerLeadId,
                    ImageId = q.Contact.ImageId,
                    Description = q.Contact.Description,
                    EmailOther = q.Contact.EmailOther,
                    DateOfBirth = q.Contact.DateOfBirth,
                    Phone = q.Contact.Phone,
                    PhoneHome = q.Contact.PhoneHome,
                    FAX = q.Contact.FAX,
                    Email = q.Contact.Email,
                    Department = q.Contact.Department,
                    ZIPCode = q.Contact.ZIPCode,
                    SexId = q.Contact.SexId,
                    AppUserId = q.Contact.AppUserId,
                    RefuseReciveEmail = q.Contact.RefuseReciveEmail,
                    RefuseReciveSMS = q.Contact.RefuseReciveSMS,
                    PositionId = q.Contact.PositionId,
                    CreatorId = q.Contact.CreatorId,
                } : null,
                Creator = filter.Selects.Contains(OrderQuoteSelect.Creator) && q.Creator != null ? new AppUser
                {
                    Id = q.Creator.Id,
                    Username = q.Creator.Username,
                    DisplayName = q.Creator.DisplayName,
                    Address = q.Creator.Address,
                    Email = q.Creator.Email,
                    Phone = q.Creator.Phone,
                    SexId = q.Creator.SexId,
                    Birthday = q.Creator.Birthday,
                    Avatar = q.Creator.Avatar,
                    Department = q.Creator.Department,
                    OrganizationId = q.Creator.OrganizationId,
                    Longitude = q.Creator.Longitude,
                    Latitude = q.Creator.Latitude,
                    StatusId = q.Creator.StatusId,
                    RowId = q.Creator.RowId,
                    Used = q.Creator.Used,
                } : null,
                District = filter.Selects.Contains(OrderQuoteSelect.District) && q.District != null ? new District
                {
                    Id = q.District.Id,
                    Code = q.District.Code,
                    Name = q.District.Name,
                    Priority = q.District.Priority,
                    ProvinceId = q.District.ProvinceId,
                    StatusId = q.District.StatusId,
                    RowId = q.District.RowId,
                    Used = q.District.Used,
                } : null,
                EditedPriceStatus = filter.Selects.Contains(OrderQuoteSelect.EditedPriceStatus) && q.EditedPriceStatus != null ? new EditedPriceStatus
                {
                    Id = q.EditedPriceStatus.Id,
                    Code = q.EditedPriceStatus.Code,
                    Name = q.EditedPriceStatus.Name,
                } : null,
                InvoiceDistrict = filter.Selects.Contains(OrderQuoteSelect.InvoiceDistrict) && q.InvoiceDistrict != null ? new District
                {
                    Id = q.InvoiceDistrict.Id,
                    Code = q.InvoiceDistrict.Code,
                    Name = q.InvoiceDistrict.Name,
                    Priority = q.InvoiceDistrict.Priority,
                    ProvinceId = q.InvoiceDistrict.ProvinceId,
                    StatusId = q.InvoiceDistrict.StatusId,
                    RowId = q.InvoiceDistrict.RowId,
                    Used = q.InvoiceDistrict.Used,
                } : null,
                InvoiceNation = filter.Selects.Contains(OrderQuoteSelect.InvoiceNation) && q.InvoiceNation != null ? new Nation
                {
                    Id = q.InvoiceNation.Id,
                    Code = q.InvoiceNation.Code,
                    Name = q.InvoiceNation.Name,
                    Priority = q.InvoiceNation.Priority,
                    StatusId = q.InvoiceNation.StatusId,
                    Used = q.InvoiceNation.Used,
                    RowId = q.InvoiceNation.RowId,
                } : null,
                InvoiceProvince = filter.Selects.Contains(OrderQuoteSelect.InvoiceProvince) && q.InvoiceProvince != null ? new Province
                {
                    Id = q.InvoiceProvince.Id,
                    Code = q.InvoiceProvince.Code,
                    Name = q.InvoiceProvince.Name,
                    Priority = q.InvoiceProvince.Priority,
                    StatusId = q.InvoiceProvince.StatusId,
                    RowId = q.InvoiceProvince.RowId,
                    Used = q.InvoiceProvince.Used,
                } : null,
                Nation = filter.Selects.Contains(OrderQuoteSelect.Nation) && q.Nation != null ? new Nation
                {
                    Id = q.Nation.Id,
                    Code = q.Nation.Code,
                    Name = q.Nation.Name,
                    Priority = q.Nation.Priority,
                    StatusId = q.Nation.StatusId,
                    Used = q.Nation.Used,
                    RowId = q.Nation.RowId,
                } : null,
             
                Opportunity = filter.Selects.Contains(OrderQuoteSelect.Opportunity) && q.Opportunity != null ? new Opportunity
                {
                    Id = q.Opportunity.Id,
                    Name = q.Opportunity.Name,
                    CompanyId = q.Opportunity.CompanyId,
                    CustomerLeadId = q.Opportunity.CustomerLeadId,
                    ClosingDate = q.Opportunity.ClosingDate,
                    SaleStageId = q.Opportunity.SaleStageId,
                    ProbabilityId = q.Opportunity.ProbabilityId,
                    PotentialResultId = q.Opportunity.PotentialResultId,
                    LeadSourceId = q.Opportunity.LeadSourceId,
                    AppUserId = q.Opportunity.AppUserId,
                    CurrencyId = q.Opportunity.CurrencyId,
                    Amount = q.Opportunity.Amount,
                    ForecastAmount = q.Opportunity.ForecastAmount,
                    Description = q.Opportunity.Description,
                    RefuseReciveSMS = q.Opportunity.RefuseReciveSMS,
                    RefuseReciveEmail = q.Opportunity.RefuseReciveEmail,
                    OpportunityResultTypeId = q.Opportunity.OpportunityResultTypeId,
                    CreatorId = q.Opportunity.CreatorId,
                } : null,
                OrderQuoteStatus = filter.Selects.Contains(OrderQuoteSelect.OrderQuoteStatus) && q.OrderQuoteStatus != null ? new OrderQuoteStatus
                {
                    Id = q.OrderQuoteStatus.Id,
                    Code = q.OrderQuoteStatus.Code,
                    Name = q.OrderQuoteStatus.Name,
                } : null,
                Province = filter.Selects.Contains(OrderQuoteSelect.Province) && q.Province != null ? new Province
                {
                    Id = q.Province.Id,
                    Code = q.Province.Code,
                    Name = q.Province.Name,
                    Priority = q.Province.Priority,
                    StatusId = q.Province.StatusId,
                    RowId = q.Province.RowId,
                    Used = q.Province.Used,
                } : null,
            }).ToListAsync();
            return OrderQuotes;
        }

        public async Task<int> Count(OrderQuoteFilter filter)
        {
            IQueryable<OrderQuoteDAO> OrderQuotes = DataContext.OrderQuote.AsNoTracking();
            OrderQuotes = DynamicFilter(OrderQuotes, filter);
            return await OrderQuotes.CountAsync();
        }

        public async Task<List<OrderQuote>> List(OrderQuoteFilter filter)
        {
            if (filter == null) return new List<OrderQuote>();
            IQueryable<OrderQuoteDAO> OrderQuoteDAOs = DataContext.OrderQuote.AsNoTracking();
            OrderQuoteDAOs = DynamicFilter(OrderQuoteDAOs, filter);
            OrderQuoteDAOs = DynamicOrder(OrderQuoteDAOs, filter);
            List<OrderQuote> OrderQuotes = await DynamicSelect(OrderQuoteDAOs, filter);
            return OrderQuotes;
        }

        public async Task<List<OrderQuote>> List(List<long> Ids)
        {
            List<OrderQuote> OrderQuotes = await DataContext.OrderQuote.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new OrderQuote()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Subject = x.Subject,
                CompanyId = x.CompanyId,
                ContactId = x.ContactId,
                OpportunityId = x.OpportunityId,
                EditedPriceStatusId = x.EditedPriceStatusId,
                EndAt = x.EndAt,
                AppUserId = x.AppUserId,
                OrderQuoteStatusId = x.OrderQuoteStatusId,
                Note = x.Note,
                InvoiceAddress = x.InvoiceAddress,
                InvoiceNationId = x.InvoiceNationId,
                InvoiceProvinceId = x.InvoiceProvinceId,
                InvoiceDistrictId = x.InvoiceDistrictId,
                InvoiceZIPCode = x.InvoiceZIPCode,
                Address = x.Address,
                NationId = x.NationId,
                ProvinceId = x.ProvinceId,
                DistrictId = x.DistrictId,
                ZIPCode = x.ZIPCode,
                SubTotal = x.SubTotal,
                GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                GeneralDiscountAmount = x.GeneralDiscountAmount,
                TotalTaxAmountOther = x.TotalTaxAmountOther,
                TotalTaxAmount = x.TotalTaxAmount,
                Total = x.Total,
                CreatorId = x.CreatorId,
                AppUser = x.AppUser == null ? null : new AppUser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Address = x.AppUser.Address,
                    Email = x.AppUser.Email,
                    Phone = x.AppUser.Phone,
                    SexId = x.AppUser.SexId,
                    Birthday = x.AppUser.Birthday,
                    Avatar = x.AppUser.Avatar,
                    Department = x.AppUser.Department,
                    OrganizationId = x.AppUser.OrganizationId,
                    Longitude = x.AppUser.Longitude,
                    Latitude = x.AppUser.Latitude,
                    StatusId = x.AppUser.StatusId,
                    RowId = x.AppUser.RowId,
                    Used = x.AppUser.Used,
                },
                Company = x.Company == null ? null : new Company
                {
                    Id = x.Company.Id,
                    Name = x.Company.Name,
                    Phone = x.Company.Phone,
                    FAX = x.Company.FAX,
                    PhoneOther = x.Company.PhoneOther,
                    Email = x.Company.Email,
                    EmailOther = x.Company.EmailOther,
                    ZIPCode = x.Company.ZIPCode,
                    Revenue = x.Company.Revenue,
                    Website = x.Company.Website,
                    Address = x.Company.Address,
                    NationId = x.Company.NationId,
                    ProvinceId = x.Company.ProvinceId,
                    DistrictId = x.Company.DistrictId,
                    NumberOfEmployee = x.Company.NumberOfEmployee,
                    RefuseReciveEmail = x.Company.RefuseReciveEmail,
                    RefuseReciveSMS = x.Company.RefuseReciveSMS,
                    CustomerLeadId = x.Company.CustomerLeadId,
                    ParentId = x.Company.ParentId,
                    Path = x.Company.Path,
                    Level = x.Company.Level,
                    ProfessionId = x.Company.ProfessionId,
                    AppUserId = x.Company.AppUserId,
                    CreatorId = x.Company.CreatorId,
                    CurrencyId = x.Company.CurrencyId,
                    CompanyStatusId = x.Company.CompanyStatusId,
                    Description = x.Company.Description,
                    RowId = x.Company.RowId,
                },
                Contact = x.Contact == null ? null : new Contact
                {
                    Id = x.Contact.Id,
                    Name = x.Contact.Name,
                    ProfessionId = x.Contact.ProfessionId,
                    CompanyId = x.Contact.CompanyId,
                    ContactStatusId = x.Contact.ContactStatusId,
                    Address = x.Contact.Address,
                    NationId = x.Contact.NationId,
                    ProvinceId = x.Contact.ProvinceId,
                    DistrictId = x.Contact.DistrictId,
                    CustomerLeadId = x.Contact.CustomerLeadId,
                    ImageId = x.Contact.ImageId,
                    Description = x.Contact.Description,
                    EmailOther = x.Contact.EmailOther,
                    DateOfBirth = x.Contact.DateOfBirth,
                    Phone = x.Contact.Phone,
                    PhoneHome = x.Contact.PhoneHome,
                    FAX = x.Contact.FAX,
                    Email = x.Contact.Email,
                    Department = x.Contact.Department,
                    ZIPCode = x.Contact.ZIPCode,
                    SexId = x.Contact.SexId,
                    AppUserId = x.Contact.AppUserId,
                    RefuseReciveEmail = x.Contact.RefuseReciveEmail,
                    RefuseReciveSMS = x.Contact.RefuseReciveSMS,
                    PositionId = x.Contact.PositionId,
                    CreatorId = x.Contact.CreatorId,
                },
                Creator = x.Creator == null ? null : new AppUser
                {
                    Id = x.Creator.Id,
                    Username = x.Creator.Username,
                    DisplayName = x.Creator.DisplayName,
                    Address = x.Creator.Address,
                    Email = x.Creator.Email,
                    Phone = x.Creator.Phone,
                    SexId = x.Creator.SexId,
                    Birthday = x.Creator.Birthday,
                    Avatar = x.Creator.Avatar,
                    Department = x.Creator.Department,
                    OrganizationId = x.Creator.OrganizationId,
                    Longitude = x.Creator.Longitude,
                    Latitude = x.Creator.Latitude,
                    StatusId = x.Creator.StatusId,
                    RowId = x.Creator.RowId,
                    Used = x.Creator.Used,
                },
                District = x.District == null ? null : new District
                {
                    Id = x.District.Id,
                    Code = x.District.Code,
                    Name = x.District.Name,
                    Priority = x.District.Priority,
                    ProvinceId = x.District.ProvinceId,
                    StatusId = x.District.StatusId,
                    RowId = x.District.RowId,
                    Used = x.District.Used,
                },
                EditedPriceStatus = x.EditedPriceStatus == null ? null : new EditedPriceStatus
                {
                    Id = x.EditedPriceStatus.Id,
                    Code = x.EditedPriceStatus.Code,
                    Name = x.EditedPriceStatus.Name,
                },
                InvoiceDistrict = x.InvoiceDistrict == null ? null : new District
                {
                    Id = x.InvoiceDistrict.Id,
                    Code = x.InvoiceDistrict.Code,
                    Name = x.InvoiceDistrict.Name,
                    Priority = x.InvoiceDistrict.Priority,
                    ProvinceId = x.InvoiceDistrict.ProvinceId,
                    StatusId = x.InvoiceDistrict.StatusId,
                    RowId = x.InvoiceDistrict.RowId,
                    Used = x.InvoiceDistrict.Used,
                },
                InvoiceNation = x.InvoiceNation == null ? null : new Nation
                {
                    Id = x.InvoiceNation.Id,
                    Code = x.InvoiceNation.Code,
                    Name = x.InvoiceNation.Name,
                    Priority = x.InvoiceNation.Priority,
                    StatusId = x.InvoiceNation.StatusId,
                    Used = x.InvoiceNation.Used,
                    RowId = x.InvoiceNation.RowId,
                },
                InvoiceProvince = x.InvoiceProvince == null ? null : new Province
                {
                    Id = x.InvoiceProvince.Id,
                    Code = x.InvoiceProvince.Code,
                    Name = x.InvoiceProvince.Name,
                    Priority = x.InvoiceProvince.Priority,
                    StatusId = x.InvoiceProvince.StatusId,
                    RowId = x.InvoiceProvince.RowId,
                    Used = x.InvoiceProvince.Used,
                },
                Nation = x.Nation == null ? null : new Nation
                {
                    Id = x.Nation.Id,
                    Code = x.Nation.Code,
                    Name = x.Nation.Name,
                    Priority = x.Nation.Priority,
                    StatusId = x.Nation.StatusId,
                    Used = x.Nation.Used,
                    RowId = x.Nation.RowId,
                },
              
                Opportunity = x.Opportunity == null ? null : new Opportunity
                {
                    Id = x.Opportunity.Id,
                    Name = x.Opportunity.Name,
                    CompanyId = x.Opportunity.CompanyId,
                    CustomerLeadId = x.Opportunity.CustomerLeadId,
                    ClosingDate = x.Opportunity.ClosingDate,
                    SaleStageId = x.Opportunity.SaleStageId,
                    ProbabilityId = x.Opportunity.ProbabilityId,
                    PotentialResultId = x.Opportunity.PotentialResultId,
                    LeadSourceId = x.Opportunity.LeadSourceId,
                    AppUserId = x.Opportunity.AppUserId,
                    CurrencyId = x.Opportunity.CurrencyId,
                    Amount = x.Opportunity.Amount,
                    ForecastAmount = x.Opportunity.ForecastAmount,
                    Description = x.Opportunity.Description,
                    RefuseReciveSMS = x.Opportunity.RefuseReciveSMS,
                    RefuseReciveEmail = x.Opportunity.RefuseReciveEmail,
                    OpportunityResultTypeId = x.Opportunity.OpportunityResultTypeId,
                    CreatorId = x.Opportunity.CreatorId,
                },
                OrderQuoteStatus = x.OrderQuoteStatus == null ? null : new OrderQuoteStatus
                {
                    Id = x.OrderQuoteStatus.Id,
                    Code = x.OrderQuoteStatus.Code,
                    Name = x.OrderQuoteStatus.Name,
                },
                Province = x.Province == null ? null : new Province
                {
                    Id = x.Province.Id,
                    Code = x.Province.Code,
                    Name = x.Province.Name,
                    Priority = x.Province.Priority,
                    StatusId = x.Province.StatusId,
                    RowId = x.Province.RowId,
                    Used = x.Province.Used,
                },
            }).ToListAsync();

            List<OrderQuoteContent> OrderQuoteContents = await DataContext.OrderQuoteContent.AsNoTracking()
                .Where(x => Ids.Contains(x.OrderQuoteId))
                .Select(x => new OrderQuoteContent
                {
                    Id = x.Id,
                    OrderQuoteId = x.OrderQuoteId,
                    ItemId = x.ItemId,
                    UnitOfMeasureId = x.UnitOfMeasureId,
                    Quantity = x.Quantity,
                    RequestedQuantity = x.RequestedQuantity,
                    PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                    PrimaryPrice = x.PrimaryPrice,
                    SalePrice = x.SalePrice,
                    DiscountPercentage = x.DiscountPercentage,
                    DiscountAmount = x.DiscountAmount,
                    GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                    GeneralDiscountAmount = x.GeneralDiscountAmount,
                    TaxPercentage = x.TaxPercentage,
                    TaxAmount = x.TaxAmount,
                    TaxAmountOther = x.TaxAmountOther,
                    TaxPercentageOther = x.TaxPercentageOther,
                    Amount = x.Amount,
                    Factor = x.Factor,
                    EditedPriceStatusId = x.EditedPriceStatusId,
                    EditedPriceStatus = new EditedPriceStatus
                    {
                        Id = x.EditedPriceStatus.Id,
                        Code = x.EditedPriceStatus.Code,
                        Name = x.EditedPriceStatus.Name,
                    },
                    Item = new Item
                    {
                        Id = x.Item.Id,
                        ProductId = x.Item.ProductId,
                        Code = x.Item.Code,
                        Name = x.Item.Name,
                        ScanCode = x.Item.ScanCode,
                        SalePrice = x.Item.SalePrice,
                        RetailPrice = x.Item.RetailPrice,
                        StatusId = x.Item.StatusId,
                        Used = x.Item.Used,
                        RowId = x.Item.RowId,
                    },
                    PrimaryUnitOfMeasure = new UnitOfMeasure
                    {
                        Id = x.PrimaryUnitOfMeasure.Id,
                        Code = x.PrimaryUnitOfMeasure.Code,
                        Name = x.PrimaryUnitOfMeasure.Name,
                        Description = x.PrimaryUnitOfMeasure.Description,
                        StatusId = x.PrimaryUnitOfMeasure.StatusId,
                        Used = x.PrimaryUnitOfMeasure.Used,
                        RowId = x.PrimaryUnitOfMeasure.RowId,
                    },
                    UnitOfMeasure = new UnitOfMeasure
                    {
                        Id = x.UnitOfMeasure.Id,
                        Code = x.UnitOfMeasure.Code,
                        Name = x.UnitOfMeasure.Name,
                        Description = x.UnitOfMeasure.Description,
                        StatusId = x.UnitOfMeasure.StatusId,
                        Used = x.UnitOfMeasure.Used,
                        RowId = x.UnitOfMeasure.RowId,
                    },
                }).ToListAsync();
            foreach (OrderQuote OrderQuote in OrderQuotes)
            {
                OrderQuote.OrderQuoteContents = OrderQuoteContents
                    .Where(x => x.OrderQuoteId == OrderQuote.Id)
                    .ToList();
            }

            return OrderQuotes;
        }

        public async Task<OrderQuote> Get(long Id)
        {
            OrderQuote OrderQuote = await DataContext.OrderQuote.AsNoTracking()
            .Where(x => x.Id == Id)
            .Where(x => x.DeletedAt == null)
            .Select(x => new OrderQuote()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Subject = x.Subject,
                CompanyId = x.CompanyId,
                ContactId = x.ContactId,
                OpportunityId = x.OpportunityId,
                EditedPriceStatusId = x.EditedPriceStatusId,
                EndAt = x.EndAt,
                AppUserId = x.AppUserId,
                OrderQuoteStatusId = x.OrderQuoteStatusId,
                Note = x.Note,
                InvoiceAddress = x.InvoiceAddress,
                InvoiceNationId = x.InvoiceNationId,
                InvoiceProvinceId = x.InvoiceProvinceId,
                InvoiceDistrictId = x.InvoiceDistrictId,
                InvoiceZIPCode = x.InvoiceZIPCode,
                Address = x.Address,
                NationId = x.NationId,
                ProvinceId = x.ProvinceId,
                DistrictId = x.DistrictId,
                ZIPCode = x.ZIPCode,
                SubTotal = x.SubTotal,
                GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                GeneralDiscountAmount = x.GeneralDiscountAmount,
                TotalTaxAmountOther = x.TotalTaxAmountOther,
                TotalTaxAmount = x.TotalTaxAmount,
                Total = x.Total,
                CreatorId = x.CreatorId,
                AppUser = x.AppUser == null ? null : new AppUser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Address = x.AppUser.Address,
                    Email = x.AppUser.Email,
                    Phone = x.AppUser.Phone,
                    SexId = x.AppUser.SexId,
                    Birthday = x.AppUser.Birthday,
                    Avatar = x.AppUser.Avatar,
                    Department = x.AppUser.Department,
                    OrganizationId = x.AppUser.OrganizationId,
                    Longitude = x.AppUser.Longitude,
                    Latitude = x.AppUser.Latitude,
                    StatusId = x.AppUser.StatusId,
                    RowId = x.AppUser.RowId,
                    Used = x.AppUser.Used,
                },
                Company = x.Company == null ? null : new Company
                {
                    Id = x.Company.Id,
                    Name = x.Company.Name,
                    Phone = x.Company.Phone,
                    FAX = x.Company.FAX,
                    PhoneOther = x.Company.PhoneOther,
                    Email = x.Company.Email,
                    EmailOther = x.Company.EmailOther,
                    ZIPCode = x.Company.ZIPCode,
                    Revenue = x.Company.Revenue,
                    Website = x.Company.Website,
                    Address = x.Company.Address,
                    NationId = x.Company.NationId,
                    ProvinceId = x.Company.ProvinceId,
                    DistrictId = x.Company.DistrictId,
                    NumberOfEmployee = x.Company.NumberOfEmployee,
                    RefuseReciveEmail = x.Company.RefuseReciveEmail,
                    RefuseReciveSMS = x.Company.RefuseReciveSMS,
                    CustomerLeadId = x.Company.CustomerLeadId,
                    ParentId = x.Company.ParentId,
                    Path = x.Company.Path,
                    Level = x.Company.Level,
                    ProfessionId = x.Company.ProfessionId,
                    AppUserId = x.Company.AppUserId,
                    CreatorId = x.Company.CreatorId,
                    CurrencyId = x.Company.CurrencyId,
                    CompanyStatusId = x.Company.CompanyStatusId,
                    Description = x.Company.Description,
                    RowId = x.Company.RowId,
                },
                Contact = x.Contact == null ? null : new Contact
                {
                    Id = x.Contact.Id,
                    Name = x.Contact.Name,
                    ProfessionId = x.Contact.ProfessionId,
                    CompanyId = x.Contact.CompanyId,
                    ContactStatusId = x.Contact.ContactStatusId,
                    Address = x.Contact.Address,
                    NationId = x.Contact.NationId,
                    ProvinceId = x.Contact.ProvinceId,
                    DistrictId = x.Contact.DistrictId,
                    CustomerLeadId = x.Contact.CustomerLeadId,
                    ImageId = x.Contact.ImageId,
                    Description = x.Contact.Description,
                    EmailOther = x.Contact.EmailOther,
                    DateOfBirth = x.Contact.DateOfBirth,
                    Phone = x.Contact.Phone,
                    PhoneHome = x.Contact.PhoneHome,
                    FAX = x.Contact.FAX,
                    Email = x.Contact.Email,
                    Department = x.Contact.Department,
                    ZIPCode = x.Contact.ZIPCode,
                    SexId = x.Contact.SexId,
                    AppUserId = x.Contact.AppUserId,
                    RefuseReciveEmail = x.Contact.RefuseReciveEmail,
                    RefuseReciveSMS = x.Contact.RefuseReciveSMS,
                    PositionId = x.Contact.PositionId,
                    CreatorId = x.Contact.CreatorId,
                },
                Creator = x.Creator == null ? null : new AppUser
                {
                    Id = x.Creator.Id,
                    Username = x.Creator.Username,
                    DisplayName = x.Creator.DisplayName,
                    Address = x.Creator.Address,
                    Email = x.Creator.Email,
                    Phone = x.Creator.Phone,
                    SexId = x.Creator.SexId,
                    Birthday = x.Creator.Birthday,
                    Avatar = x.Creator.Avatar,
                    Department = x.Creator.Department,
                    OrganizationId = x.Creator.OrganizationId,
                    Longitude = x.Creator.Longitude,
                    Latitude = x.Creator.Latitude,
                    StatusId = x.Creator.StatusId,
                    RowId = x.Creator.RowId,
                    Used = x.Creator.Used,
                },
                District = x.District == null ? null : new District
                {
                    Id = x.District.Id,
                    Code = x.District.Code,
                    Name = x.District.Name,
                    Priority = x.District.Priority,
                    ProvinceId = x.District.ProvinceId,
                    StatusId = x.District.StatusId,
                    RowId = x.District.RowId,
                    Used = x.District.Used,
                },
                EditedPriceStatus = x.EditedPriceStatus == null ? null : new EditedPriceStatus
                {
                    Id = x.EditedPriceStatus.Id,
                    Code = x.EditedPriceStatus.Code,
                    Name = x.EditedPriceStatus.Name,
                },
                InvoiceDistrict = x.InvoiceDistrict == null ? null : new District
                {
                    Id = x.InvoiceDistrict.Id,
                    Code = x.InvoiceDistrict.Code,
                    Name = x.InvoiceDistrict.Name,
                    Priority = x.InvoiceDistrict.Priority,
                    ProvinceId = x.InvoiceDistrict.ProvinceId,
                    StatusId = x.InvoiceDistrict.StatusId,
                    RowId = x.InvoiceDistrict.RowId,
                    Used = x.InvoiceDistrict.Used,
                },
                InvoiceNation = x.InvoiceNation == null ? null : new Nation
                {
                    Id = x.InvoiceNation.Id,
                    Code = x.InvoiceNation.Code,
                    Name = x.InvoiceNation.Name,
                    Priority = x.InvoiceNation.Priority,
                    StatusId = x.InvoiceNation.StatusId,
                    Used = x.InvoiceNation.Used,
                    RowId = x.InvoiceNation.RowId,
                },
                InvoiceProvince = x.InvoiceProvince == null ? null : new Province
                {
                    Id = x.InvoiceProvince.Id,
                    Code = x.InvoiceProvince.Code,
                    Name = x.InvoiceProvince.Name,
                    Priority = x.InvoiceProvince.Priority,
                    StatusId = x.InvoiceProvince.StatusId,
                    RowId = x.InvoiceProvince.RowId,
                    Used = x.InvoiceProvince.Used,
                },
                Nation = x.Nation == null ? null : new Nation
                {
                    Id = x.Nation.Id,
                    Code = x.Nation.Code,
                    Name = x.Nation.Name,
                    Priority = x.Nation.Priority,
                    StatusId = x.Nation.StatusId,
                    Used = x.Nation.Used,
                    RowId = x.Nation.RowId,
                },
               
                Opportunity = x.Opportunity == null ? null : new Opportunity
                {
                    Id = x.Opportunity.Id,
                    Name = x.Opportunity.Name,
                    CompanyId = x.Opportunity.CompanyId,
                    CustomerLeadId = x.Opportunity.CustomerLeadId,
                    ClosingDate = x.Opportunity.ClosingDate,
                    SaleStageId = x.Opportunity.SaleStageId,
                    ProbabilityId = x.Opportunity.ProbabilityId,
                    PotentialResultId = x.Opportunity.PotentialResultId,
                    LeadSourceId = x.Opportunity.LeadSourceId,
                    AppUserId = x.Opportunity.AppUserId,
                    CurrencyId = x.Opportunity.CurrencyId,
                    Amount = x.Opportunity.Amount,
                    ForecastAmount = x.Opportunity.ForecastAmount,
                    Description = x.Opportunity.Description,
                    RefuseReciveSMS = x.Opportunity.RefuseReciveSMS,
                    RefuseReciveEmail = x.Opportunity.RefuseReciveEmail,
                    OpportunityResultTypeId = x.Opportunity.OpportunityResultTypeId,
                    CreatorId = x.Opportunity.CreatorId,
                },
                OrderQuoteStatus = x.OrderQuoteStatus == null ? null : new OrderQuoteStatus
                {
                    Id = x.OrderQuoteStatus.Id,
                    Code = x.OrderQuoteStatus.Code,
                    Name = x.OrderQuoteStatus.Name,
                },
                Province = x.Province == null ? null : new Province
                {
                    Id = x.Province.Id,
                    Code = x.Province.Code,
                    Name = x.Province.Name,
                    Priority = x.Province.Priority,
                    StatusId = x.Province.StatusId,
                    RowId = x.Province.RowId,
                    Used = x.Province.Used,
                },
            }).FirstOrDefaultAsync();

            if (OrderQuote == null)
                return null;
            OrderQuote.OrderQuoteContents = await DataContext.OrderQuoteContent.AsNoTracking()
                .Where(x => x.OrderQuoteId == OrderQuote.Id)
                .Select(x => new OrderQuoteContent
                {
                    Id = x.Id,
                    OrderQuoteId = x.OrderQuoteId,
                    ItemId = x.ItemId,
                    UnitOfMeasureId = x.UnitOfMeasureId,
                    Quantity = x.Quantity,
                    RequestedQuantity = x.RequestedQuantity,
                    PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                    PrimaryPrice = x.PrimaryPrice,
                    SalePrice = x.SalePrice,
                    DiscountPercentage = x.DiscountPercentage,
                    DiscountAmount = x.DiscountAmount,
                    GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                    GeneralDiscountAmount = x.GeneralDiscountAmount,
                    TaxPercentage = x.TaxPercentage,
                    TaxAmount = x.TaxAmount,
                    TaxAmountOther = x.TaxAmountOther,
                    TaxPercentageOther = x.TaxPercentageOther,
                    Amount = x.Amount,
                    Factor = x.Factor,
                    EditedPriceStatusId = x.EditedPriceStatusId,
                    TaxTypeId = x.TaxTypeId,
                    EditedPriceStatus = new EditedPriceStatus
                    {
                        Id = x.EditedPriceStatus.Id,
                        Code = x.EditedPriceStatus.Code,
                        Name = x.EditedPriceStatus.Name,
                    },
                    Item = new Item
                    {
                        Id = x.Item.Id,
                        ProductId = x.Item.ProductId,
                        Code = x.Item.Code,
                        Name = x.Item.Name,
                        ScanCode = x.Item.ScanCode,
                        SalePrice = x.Item.SalePrice,
                        RetailPrice = x.Item.RetailPrice,
                        StatusId = x.Item.StatusId,
                        Used = x.Item.Used,
                        RowId = x.Item.RowId,
                    },
                    PrimaryUnitOfMeasure = new UnitOfMeasure
                    {
                        Id = x.PrimaryUnitOfMeasure.Id,
                        Code = x.PrimaryUnitOfMeasure.Code,
                        Name = x.PrimaryUnitOfMeasure.Name,
                        Description = x.PrimaryUnitOfMeasure.Description,
                        StatusId = x.PrimaryUnitOfMeasure.StatusId,
                        Used = x.PrimaryUnitOfMeasure.Used,
                        RowId = x.PrimaryUnitOfMeasure.RowId,
                    },
                    TaxType = new TaxType
                    {
                        Id = x.TaxType.Id,
                        Code = x.TaxType.Code,
                        Name = x.TaxType.Name,
                    },
                    UnitOfMeasure = new UnitOfMeasure
                    {
                        Id = x.UnitOfMeasure.Id,
                        Code = x.UnitOfMeasure.Code,
                        Name = x.UnitOfMeasure.Name,
                        Description = x.UnitOfMeasure.Description,
                        StatusId = x.UnitOfMeasure.StatusId,
                        Used = x.UnitOfMeasure.Used,
                        RowId = x.UnitOfMeasure.RowId,
                    },
                }).ToListAsync();

            return OrderQuote;
        }
        public async Task<bool> Create(OrderQuote OrderQuote)
        {
            OrderQuoteDAO OrderQuoteDAO = new OrderQuoteDAO();
            OrderQuoteDAO.Id = OrderQuote.Id;
            OrderQuoteDAO.Subject = OrderQuote.Subject;
            OrderQuoteDAO.CompanyId = OrderQuote.CompanyId;
            OrderQuoteDAO.ContactId = OrderQuote.ContactId;
            OrderQuoteDAO.OpportunityId = OrderQuote.OpportunityId;
            OrderQuoteDAO.EditedPriceStatusId = OrderQuote.EditedPriceStatusId;
            OrderQuoteDAO.EndAt = OrderQuote.EndAt;
            OrderQuoteDAO.AppUserId = OrderQuote.AppUserId;
            OrderQuoteDAO.OrderQuoteStatusId = OrderQuote.OrderQuoteStatusId;
            OrderQuoteDAO.Note = OrderQuote.Note;
            OrderQuoteDAO.InvoiceAddress = OrderQuote.InvoiceAddress;
            OrderQuoteDAO.InvoiceNationId = OrderQuote.InvoiceNationId;
            OrderQuoteDAO.InvoiceProvinceId = OrderQuote.InvoiceProvinceId;
            OrderQuoteDAO.InvoiceDistrictId = OrderQuote.InvoiceDistrictId;
            OrderQuoteDAO.InvoiceZIPCode = OrderQuote.InvoiceZIPCode;
            OrderQuoteDAO.Address = OrderQuote.Address;
            OrderQuoteDAO.NationId = OrderQuote.NationId;
            OrderQuoteDAO.ProvinceId = OrderQuote.ProvinceId;
            OrderQuoteDAO.DistrictId = OrderQuote.DistrictId;
            OrderQuoteDAO.ZIPCode = OrderQuote.ZIPCode;
            OrderQuoteDAO.SubTotal = OrderQuote.SubTotal;
            OrderQuoteDAO.GeneralDiscountPercentage = OrderQuote.GeneralDiscountPercentage;
            OrderQuoteDAO.GeneralDiscountAmount = OrderQuote.GeneralDiscountAmount;
            OrderQuoteDAO.TotalTaxAmountOther = OrderQuote.TotalTaxAmountOther;
            OrderQuoteDAO.TotalTaxAmount = OrderQuote.TotalTaxAmount;
            OrderQuoteDAO.Total = OrderQuote.Total;
            OrderQuoteDAO.CreatorId = OrderQuote.CreatorId;
            OrderQuoteDAO.CreatedAt = StaticParams.DateTimeNow;
            OrderQuoteDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.OrderQuote.Add(OrderQuoteDAO);
            await DataContext.SaveChangesAsync();
            OrderQuote.Id = OrderQuoteDAO.Id;
            await SaveReference(OrderQuote);
            return true;
        }

        public async Task<bool> Update(OrderQuote OrderQuote)
        {
            OrderQuoteDAO OrderQuoteDAO = DataContext.OrderQuote.Where(x => x.Id == OrderQuote.Id).FirstOrDefault();
            if (OrderQuoteDAO == null)
                return false;
            OrderQuoteDAO.Id = OrderQuote.Id;
            OrderQuoteDAO.Subject = OrderQuote.Subject;
            OrderQuoteDAO.CompanyId = OrderQuote.CompanyId;
            OrderQuoteDAO.ContactId = OrderQuote.ContactId;
            OrderQuoteDAO.OpportunityId = OrderQuote.OpportunityId;
            OrderQuoteDAO.EditedPriceStatusId = OrderQuote.EditedPriceStatusId;
            OrderQuoteDAO.EndAt = OrderQuote.EndAt;
            OrderQuoteDAO.AppUserId = OrderQuote.AppUserId;
            OrderQuoteDAO.OrderQuoteStatusId = OrderQuote.OrderQuoteStatusId;
            OrderQuoteDAO.Note = OrderQuote.Note;
            OrderQuoteDAO.InvoiceAddress = OrderQuote.InvoiceAddress;
            OrderQuoteDAO.InvoiceNationId = OrderQuote.InvoiceNationId;
            OrderQuoteDAO.InvoiceProvinceId = OrderQuote.InvoiceProvinceId;
            OrderQuoteDAO.InvoiceDistrictId = OrderQuote.InvoiceDistrictId;
            OrderQuoteDAO.InvoiceZIPCode = OrderQuote.InvoiceZIPCode;
            OrderQuoteDAO.Address = OrderQuote.Address;
            OrderQuoteDAO.NationId = OrderQuote.NationId;
            OrderQuoteDAO.ProvinceId = OrderQuote.ProvinceId;
            OrderQuoteDAO.DistrictId = OrderQuote.DistrictId;
            OrderQuoteDAO.ZIPCode = OrderQuote.ZIPCode;
            OrderQuoteDAO.SubTotal = OrderQuote.SubTotal;
            OrderQuoteDAO.GeneralDiscountPercentage = OrderQuote.GeneralDiscountPercentage;
            OrderQuoteDAO.GeneralDiscountAmount = OrderQuote.GeneralDiscountAmount;
            OrderQuoteDAO.TotalTaxAmountOther = OrderQuote.TotalTaxAmountOther;
            OrderQuoteDAO.TotalTaxAmount = OrderQuote.TotalTaxAmount;
            OrderQuoteDAO.Total = OrderQuote.Total;
            OrderQuoteDAO.CreatorId = OrderQuote.CreatorId;
            OrderQuoteDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(OrderQuote);
            return true;
        }

        public async Task<bool> Delete(OrderQuote OrderQuote)
        {
            await DataContext.OrderQuote.Where(x => x.Id == OrderQuote.Id).UpdateFromQueryAsync(x => new OrderQuoteDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<OrderQuote> OrderQuotes)
        {
            List<OrderQuoteDAO> OrderQuoteDAOs = new List<OrderQuoteDAO>();
            foreach (OrderQuote OrderQuote in OrderQuotes)
            {
                OrderQuoteDAO OrderQuoteDAO = new OrderQuoteDAO();
                OrderQuoteDAO.Id = OrderQuote.Id;
                OrderQuoteDAO.Subject = OrderQuote.Subject;
                OrderQuoteDAO.CompanyId = OrderQuote.CompanyId;
                OrderQuoteDAO.ContactId = OrderQuote.ContactId;
                OrderQuoteDAO.OpportunityId = OrderQuote.OpportunityId;
                OrderQuoteDAO.EditedPriceStatusId = OrderQuote.EditedPriceStatusId;
                OrderQuoteDAO.EndAt = OrderQuote.EndAt;
                OrderQuoteDAO.AppUserId = OrderQuote.AppUserId;
                OrderQuoteDAO.OrderQuoteStatusId = OrderQuote.OrderQuoteStatusId;
                OrderQuoteDAO.Note = OrderQuote.Note;
                OrderQuoteDAO.InvoiceAddress = OrderQuote.InvoiceAddress;
                OrderQuoteDAO.InvoiceNationId = OrderQuote.InvoiceNationId;
                OrderQuoteDAO.InvoiceProvinceId = OrderQuote.InvoiceProvinceId;
                OrderQuoteDAO.InvoiceDistrictId = OrderQuote.InvoiceDistrictId;
                OrderQuoteDAO.InvoiceZIPCode = OrderQuote.InvoiceZIPCode;
                OrderQuoteDAO.Address = OrderQuote.Address;
                OrderQuoteDAO.NationId = OrderQuote.NationId;
                OrderQuoteDAO.ProvinceId = OrderQuote.ProvinceId;
                OrderQuoteDAO.DistrictId = OrderQuote.DistrictId;
                OrderQuoteDAO.ZIPCode = OrderQuote.ZIPCode;
                OrderQuoteDAO.SubTotal = OrderQuote.SubTotal;
                OrderQuoteDAO.GeneralDiscountPercentage = OrderQuote.GeneralDiscountPercentage;
                OrderQuoteDAO.GeneralDiscountAmount = OrderQuote.GeneralDiscountAmount;
                OrderQuoteDAO.TotalTaxAmountOther = OrderQuote.TotalTaxAmountOther;
                OrderQuoteDAO.TotalTaxAmount = OrderQuote.TotalTaxAmount;
                OrderQuoteDAO.Total = OrderQuote.Total;
                OrderQuoteDAO.CreatorId = OrderQuote.CreatorId;
                OrderQuoteDAO.CreatedAt = StaticParams.DateTimeNow;
                OrderQuoteDAO.UpdatedAt = StaticParams.DateTimeNow;
                OrderQuoteDAOs.Add(OrderQuoteDAO);
            }
            await DataContext.BulkMergeAsync(OrderQuoteDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<OrderQuote> OrderQuotes)
        {
            List<long> Ids = OrderQuotes.Select(x => x.Id).ToList();
            await DataContext.OrderQuote
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new OrderQuoteDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(OrderQuote OrderQuote)
        {
            await DataContext.OrderQuoteContent
                .Where(x => x.OrderQuoteId == OrderQuote.Id)
                .DeleteFromQueryAsync();
            List<OrderQuoteContentDAO> OrderQuoteContentDAOs = new List<OrderQuoteContentDAO>();
            if (OrderQuote.OrderQuoteContents != null)
            {
                foreach (OrderQuoteContent OrderQuoteContent in OrderQuote.OrderQuoteContents)
                {
                    OrderQuoteContentDAO OrderQuoteContentDAO = new OrderQuoteContentDAO();
                    OrderQuoteContentDAO.Id = OrderQuoteContent.Id;
                    OrderQuoteContentDAO.OrderQuoteId = OrderQuote.Id;
                    OrderQuoteContentDAO.ItemId = OrderQuoteContent.ItemId;
                    OrderQuoteContentDAO.UnitOfMeasureId = OrderQuoteContent.UnitOfMeasureId;
                    OrderQuoteContentDAO.Quantity = OrderQuoteContent.Quantity;
                    OrderQuoteContentDAO.RequestedQuantity = OrderQuoteContent.RequestedQuantity;
                    OrderQuoteContentDAO.PrimaryUnitOfMeasureId = OrderQuoteContent.PrimaryUnitOfMeasureId;
                    OrderQuoteContentDAO.PrimaryPrice = OrderQuoteContent.PrimaryPrice;
                    OrderQuoteContentDAO.SalePrice = OrderQuoteContent.SalePrice;
                    OrderQuoteContentDAO.DiscountPercentage = OrderQuoteContent.DiscountPercentage;
                    OrderQuoteContentDAO.DiscountAmount = OrderQuoteContent.DiscountAmount;
                    OrderQuoteContentDAO.GeneralDiscountPercentage = OrderQuoteContent.GeneralDiscountPercentage;
                    OrderQuoteContentDAO.GeneralDiscountAmount = OrderQuoteContent.GeneralDiscountAmount;
                    OrderQuoteContentDAO.TaxPercentage = OrderQuoteContent.TaxPercentage;
                    OrderQuoteContentDAO.TaxAmount = OrderQuoteContent.TaxAmount;
                    OrderQuoteContentDAO.TaxAmountOther = OrderQuoteContent.TaxAmountOther;
                    OrderQuoteContentDAO.TaxPercentageOther = OrderQuoteContent.TaxPercentageOther;
                    OrderQuoteContentDAO.Amount = OrderQuoteContent.Amount;
                    OrderQuoteContentDAO.Factor = OrderQuoteContent.Factor;
                    OrderQuoteContentDAO.EditedPriceStatusId = OrderQuoteContent.EditedPriceStatusId;
                    OrderQuoteContentDAO.TaxTypeId = OrderQuoteContent.TaxTypeId;
                    OrderQuoteContentDAOs.Add(OrderQuoteContentDAO);
                }
                await DataContext.OrderQuoteContent.BulkMergeAsync(OrderQuoteContentDAOs);
            }
        }

    }
}
