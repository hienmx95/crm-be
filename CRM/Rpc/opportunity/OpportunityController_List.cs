using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common; 
using Microsoft.AspNetCore.Mvc; 
using CRM.Entities; 
using CRM.Enums;

namespace CRM.Rpc.opportunity
{
    public partial class OpportunityController : RpcController
    {
        [Route(OpportunityRoute.CountActivity), HttpPost]
        public async Task<ActionResult<int>> CountActivity([FromBody] Opportunity_OpportunityActivityFilterDTO Opportunity_OpportunityActivityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityActivityFilter OpportunityActivityFilter = ConvertFilterDTOToFilterEntity(Opportunity_OpportunityActivityFilterDTO);
            OpportunityActivityFilter = await OpportunityActivityService.ToFilter(OpportunityActivityFilter);
            int count = await OpportunityActivityService.Count(OpportunityActivityFilter);
            return count;
        }

        [Route(OpportunityRoute.ListActivity), HttpPost]
        public async Task<ActionResult<List<Opportunity_OpportunityActivityDTO>>> ListActivity([FromBody] Opportunity_OpportunityActivityFilterDTO Opportunity_OpportunityActivityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityActivityFilter OpportunityActivityFilter = ConvertFilterDTOToFilterEntity(Opportunity_OpportunityActivityFilterDTO);
            OpportunityActivityFilter = await OpportunityActivityService.ToFilter(OpportunityActivityFilter);
            List<OpportunityActivity> OpportunityActivities = await OpportunityActivityService.List(OpportunityActivityFilter);
            List<Opportunity_OpportunityActivityDTO> Opportunity_OpportunityActivityDTOs = OpportunityActivities
                .Select(c => new Opportunity_OpportunityActivityDTO(c)).ToList();
            return Opportunity_OpportunityActivityDTOs;
        }

        [Route(OpportunityRoute.GetActivity), HttpPost]
        public async Task<ActionResult<Opportunity_OpportunityActivityDTO>> GetActivity([FromBody] Opportunity_OpportunityActivityDTO Opportunity_OpportunityActivityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityActivity OpportunityActivity = await OpportunityActivityService.Get(Opportunity_OpportunityActivityDTO.Id);
            return new Opportunity_OpportunityActivityDTO(OpportunityActivity);
        }

        private OpportunityActivityFilter ConvertFilterDTOToFilterEntity(Opportunity_OpportunityActivityFilterDTO Opportunity_OpportunityActivityFilterDTO)
        {
            OpportunityActivityFilter OpportunityActivityFilter = new OpportunityActivityFilter();
            OpportunityActivityFilter.Selects = OpportunityActivitySelect.ALL;
            OpportunityActivityFilter.Skip = Opportunity_OpportunityActivityFilterDTO.Skip;
            OpportunityActivityFilter.Take = Opportunity_OpportunityActivityFilterDTO.Take;
            OpportunityActivityFilter.OrderBy = Opportunity_OpportunityActivityFilterDTO.OrderBy;
            OpportunityActivityFilter.OrderType = Opportunity_OpportunityActivityFilterDTO.OrderType;

            OpportunityActivityFilter.Id = Opportunity_OpportunityActivityFilterDTO.Id;
            OpportunityActivityFilter.Title = Opportunity_OpportunityActivityFilterDTO.Title;
            OpportunityActivityFilter.FromDate = Opportunity_OpportunityActivityFilterDTO.FromDate;
            OpportunityActivityFilter.ToDate = Opportunity_OpportunityActivityFilterDTO.ToDate;
            OpportunityActivityFilter.ActivityTypeId = Opportunity_OpportunityActivityFilterDTO.ActivityTypeId;
            OpportunityActivityFilter.ActivityPriorityId = Opportunity_OpportunityActivityFilterDTO.ActivityPriorityId;
            OpportunityActivityFilter.Description = Opportunity_OpportunityActivityFilterDTO.Description;
            OpportunityActivityFilter.Address = Opportunity_OpportunityActivityFilterDTO.Address;
            OpportunityActivityFilter.OpportunityId = Opportunity_OpportunityActivityFilterDTO.OpportunityId;
            OpportunityActivityFilter.AppUserId = Opportunity_OpportunityActivityFilterDTO.AppUserId;
            OpportunityActivityFilter.ActivityStatusId = Opportunity_OpportunityActivityFilterDTO.ActivityStatusId;
            OpportunityActivityFilter.CreatedAt = Opportunity_OpportunityActivityFilterDTO.CreatedAt;
            OpportunityActivityFilter.UpdatedAt = Opportunity_OpportunityActivityFilterDTO.UpdatedAt;
            return OpportunityActivityFilter;
        }

        [Route(OpportunityRoute.CountCallLog), HttpPost]
        public async Task<ActionResult<int>> CountCallLog([FromBody] Opportunity_CallLogFilterDTO Opportunity_CallLogFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLogFilter CallLogFilter = ConvertFilterCallLog(Opportunity_CallLogFilterDTO);
            CallLogFilter = await CallLogService.ToFilter(CallLogFilter);
            int count = await CallLogService.Count(CallLogFilter);
            return count;
        }

        [Route(OpportunityRoute.ListCallLog), HttpPost]
        public async Task<ActionResult<List<Opportunity_CallLogDTO>>> ListCallLog([FromBody] Opportunity_CallLogFilterDTO Opportunity_CallLogFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLogFilter CallLogFilter = ConvertFilterCallLog(Opportunity_CallLogFilterDTO);
            CallLogFilter = await CallLogService.ToFilter(CallLogFilter);
            List<CallLog> CallLogs = await CallLogService.List(CallLogFilter);
            List<Opportunity_CallLogDTO> Opportunity_CallLogDTOs = CallLogs
                .Select(c => new Opportunity_CallLogDTO(c)).ToList();
            return Opportunity_CallLogDTOs;
        }

        [Route(OpportunityRoute.GetCallLog), HttpPost]
        public async Task<ActionResult<Opportunity_CallLogDTO>> GetCallLog([FromBody] Opportunity_CallLogDTO Opportunity_CallLogDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLog CallLog = await CallLogService.Get(Opportunity_CallLogDTO.Id);
            return new Opportunity_CallLogDTO(CallLog);
        }

        private CallLogFilter ConvertFilterCallLog(Opportunity_CallLogFilterDTO Opportunity_CallLogFilterDTO)
        {
            CallLogFilter CallLogFilter = new CallLogFilter();
            CallLogFilter.Selects = CallLogSelect.ALL;
            CallLogFilter.Skip = Opportunity_CallLogFilterDTO.Skip;
            CallLogFilter.Take = Opportunity_CallLogFilterDTO.Take;
            CallLogFilter.OrderBy = Opportunity_CallLogFilterDTO.OrderBy;
            CallLogFilter.OrderType = Opportunity_CallLogFilterDTO.OrderType;

            CallLogFilter.Id = Opportunity_CallLogFilterDTO.Id;
            CallLogFilter.EntityReferenceId = Opportunity_CallLogFilterDTO.EntityReferenceId;
            CallLogFilter.CallTypeId = Opportunity_CallLogFilterDTO.CallTypeId;
            CallLogFilter.CallEmotionId = Opportunity_CallLogFilterDTO.CallEmotionId;
            CallLogFilter.AppUserId = Opportunity_CallLogFilterDTO.AppUserId;
            CallLogFilter.Title = Opportunity_CallLogFilterDTO.Title;
            CallLogFilter.Content = Opportunity_CallLogFilterDTO.Content;
            CallLogFilter.Phone = Opportunity_CallLogFilterDTO.Phone;
            CallLogFilter.CallTime = Opportunity_CallLogFilterDTO.CallTime;
            return CallLogFilter;
        }

        [Route(OpportunityRoute.CountItem), HttpPost]
        public async Task<ActionResult<long>> CountItem([FromBody] Opportunity_ItemFilterDTO Opportunity_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Id = Opportunity_ItemFilterDTO.Id;
            ItemFilter.ProductId = Opportunity_ItemFilterDTO.ProductId;
            ItemFilter.ProductTypeId = Opportunity_ItemFilterDTO.ProductTypeId;
            ItemFilter.ProductGroupingId = Opportunity_ItemFilterDTO.ProductGroupingId;
            ItemFilter.Code = Opportunity_ItemFilterDTO.Code;
            ItemFilter.Name = Opportunity_ItemFilterDTO.Name;
            ItemFilter.ScanCode = Opportunity_ItemFilterDTO.ScanCode;
            ItemFilter.OtherName = Opportunity_ItemFilterDTO.OtherName;
            ItemFilter.SalePrice = Opportunity_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = Opportunity_ItemFilterDTO.RetailPrice;
            ItemFilter.Search = Opportunity_ItemFilterDTO.Search;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            return await ItemService.Count(ItemFilter);
        }

        [Route(OpportunityRoute.ListItem), HttpPost]
        public async Task<ActionResult<List<Opportunity_ItemDTO>>> ListItem([FromBody] Opportunity_ItemFilterDTO Opportunity_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = Opportunity_ItemFilterDTO.Skip;
            ItemFilter.Take = Opportunity_ItemFilterDTO.Take;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            ItemFilter.Id = Opportunity_ItemFilterDTO.Id;
            ItemFilter.ProductId = Opportunity_ItemFilterDTO.ProductId;
            ItemFilter.ProductTypeId = Opportunity_ItemFilterDTO.ProductTypeId;
            ItemFilter.ProductGroupingId = Opportunity_ItemFilterDTO.ProductGroupingId;
            ItemFilter.Code = Opportunity_ItemFilterDTO.Code;
            ItemFilter.Name = Opportunity_ItemFilterDTO.Name;
            ItemFilter.ScanCode = Opportunity_ItemFilterDTO.ScanCode;
            ItemFilter.OtherName = Opportunity_ItemFilterDTO.OtherName;
            ItemFilter.SalePrice = Opportunity_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = Opportunity_ItemFilterDTO.RetailPrice;
            ItemFilter.Search = Opportunity_ItemFilterDTO.Search;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Item> Items = await ItemService.List(ItemFilter);
            List<Opportunity_ItemDTO> Opportunity_ItemDTOs = Items
                .Select(x => new Opportunity_ItemDTO(x)).ToList();
            return Opportunity_ItemDTOs;
        }

        [Route(OpportunityRoute.CountContact), HttpPost]
        public async Task<ActionResult<int>> CountContact([FromBody] Opportunity_ContactFilterDTO Opportunity_ContactFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactFilter ContactFilter = ConvertFilterContact(Opportunity_ContactFilterDTO);
            ContactFilter = await ContactService.ToFilter(ContactFilter);
            int count = await ContactService.Count(ContactFilter);
            return count;
        }

        [Route(OpportunityRoute.ListContact), HttpPost]
        public async Task<ActionResult<List<Opportunity_ContactDTO>>> ListContact([FromBody] Opportunity_ContactFilterDTO Opportunity_ContactFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactFilter ContactFilter = ConvertFilterContact(Opportunity_ContactFilterDTO);
            ContactFilter = await ContactService.ToFilter(ContactFilter);
            List<Contact> Contacts = await ContactService.List(ContactFilter);
            List<Opportunity_ContactDTO> Opportunity_ContactDTOs = Contacts
                .Select(c => new Opportunity_ContactDTO(c)).ToList();
            return Opportunity_ContactDTOs;
        }

        [Route(OpportunityRoute.GetContact), HttpPost]
        public async Task<ActionResult<Opportunity_ContactDTO>> GetContact([FromBody] Opportunity_ContactDTO Opportunity_ContactDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Contact Contact = await ContactService.Get(Opportunity_ContactDTO.Id);
            return new Opportunity_ContactDTO(Contact);
        }

        private ContactFilter ConvertFilterContact(Opportunity_ContactFilterDTO Opportunity_ContactFilterDTO)
        {
            ContactFilter ContactFilter = new ContactFilter();
            ContactFilter.Selects = ContactSelect.ALL;
            ContactFilter.Skip = Opportunity_ContactFilterDTO.Skip;
            ContactFilter.Take = Opportunity_ContactFilterDTO.Take;
            ContactFilter.OrderBy = Opportunity_ContactFilterDTO.OrderBy;
            ContactFilter.OrderType = Opportunity_ContactFilterDTO.OrderType;

            ContactFilter.Id = Opportunity_ContactFilterDTO.Id;
            ContactFilter.Name = Opportunity_ContactFilterDTO.Name;
            ContactFilter.ProfessionId = Opportunity_ContactFilterDTO.ProfessionId;
            ContactFilter.CompanyId = Opportunity_ContactFilterDTO.CompanyId;
            ContactFilter.ProvinceId = Opportunity_ContactFilterDTO.ProvinceId;
            ContactFilter.DistrictId = Opportunity_ContactFilterDTO.DistrictId;
            ContactFilter.NationId = Opportunity_ContactFilterDTO.NationId;
            ContactFilter.ImageId = Opportunity_ContactFilterDTO.ImageId;
            ContactFilter.Description = Opportunity_ContactFilterDTO.Description;
            ContactFilter.Address = Opportunity_ContactFilterDTO.Address;
            ContactFilter.EmailOther = Opportunity_ContactFilterDTO.EmailOther;
            ContactFilter.DateOfBirth = Opportunity_ContactFilterDTO.DateOfBirth;
            ContactFilter.Phone = Opportunity_ContactFilterDTO.Phone;
            ContactFilter.PhoneHome = Opportunity_ContactFilterDTO.PhoneHome;
            ContactFilter.FAX = Opportunity_ContactFilterDTO.FAX;
            ContactFilter.Email = Opportunity_ContactFilterDTO.Email;
            ContactFilter.ZIPCode = Opportunity_ContactFilterDTO.ZIPCode;
            ContactFilter.SexId = Opportunity_ContactFilterDTO.SexId;
            ContactFilter.AppUserId = Opportunity_ContactFilterDTO.AppUserId;
            ContactFilter.CreatedAt = Opportunity_ContactFilterDTO.CreatedAt;
            ContactFilter.UpdatedAt = Opportunity_ContactFilterDTO.UpdatedAt;
            ContactFilter.Department = Opportunity_ContactFilterDTO.Department;
            ContactFilter.PositionId = Opportunity_ContactFilterDTO.PositionId;
            ContactFilter.CustomerLeadId = Opportunity_ContactFilterDTO.CustomerLeadId;
            return ContactFilter;
        }

        [Route(OpportunityRoute.CountOrderQuote), HttpPost]
        public async Task<ActionResult<int>> CountOrderQuote([FromBody] Opportunity_OrderQuoteFilterDTO Opportunity_OrderQuoteFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuoteFilter OrderQuoteFilter = ConvertFilterOrderQuote(Opportunity_OrderQuoteFilterDTO);
            OrderQuoteFilter = await OrderQuoteService.ToFilter(OrderQuoteFilter);
            int count = await OrderQuoteService.Count(OrderQuoteFilter);
            return count;
        }

        [Route(OpportunityRoute.ListOrderQuote), HttpPost]
        public async Task<ActionResult<List<Opportunity_OrderQuoteDTO>>> ListOrderQuote([FromBody] Opportunity_OrderQuoteFilterDTO Opportunity_OrderQuoteFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuoteFilter OrderQuoteFilter = ConvertFilterOrderQuote(Opportunity_OrderQuoteFilterDTO);
            OrderQuoteFilter = await OrderQuoteService.ToFilter(OrderQuoteFilter);
            List<OrderQuote> OrderQuotes = await OrderQuoteService.List(OrderQuoteFilter);
            List<Opportunity_OrderQuoteDTO> Opportunity_OrderQuoteDTOs = OrderQuotes
                .Select(c => new Opportunity_OrderQuoteDTO(c)).ToList();
            return Opportunity_OrderQuoteDTOs;
        }

        [Route(OpportunityRoute.GetOrderQuote), HttpPost]
        public async Task<ActionResult<Opportunity_OrderQuoteDTO>> GetOrderQuote([FromBody] Opportunity_OrderQuoteDTO Opportunity_OrderQuoteDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuote OrderQuote = await OrderQuoteService.Get(Opportunity_OrderQuoteDTO.Id);
            return new Opportunity_OrderQuoteDTO(OrderQuote);
        }
        private OrderQuoteFilter ConvertFilterOrderQuote(Opportunity_OrderQuoteFilterDTO Opportunity_OrderQuoteFilterDTO)
        {
            OrderQuoteFilter OrderQuoteFilter = new OrderQuoteFilter();
            OrderQuoteFilter.Selects = OrderQuoteSelect.ALL;
            OrderQuoteFilter.Skip = Opportunity_OrderQuoteFilterDTO.Skip;
            OrderQuoteFilter.Take = Opportunity_OrderQuoteFilterDTO.Take;
            OrderQuoteFilter.OrderBy = Opportunity_OrderQuoteFilterDTO.OrderBy;
            OrderQuoteFilter.OrderType = Opportunity_OrderQuoteFilterDTO.OrderType;

            OrderQuoteFilter.Id = Opportunity_OrderQuoteFilterDTO.Id;
            OrderQuoteFilter.Subject = Opportunity_OrderQuoteFilterDTO.Subject;
            OrderQuoteFilter.NationId = Opportunity_OrderQuoteFilterDTO.NationId;
            OrderQuoteFilter.ProvinceId = Opportunity_OrderQuoteFilterDTO.ProvinceId;
            OrderQuoteFilter.DistrictId = Opportunity_OrderQuoteFilterDTO.DistrictId;
            OrderQuoteFilter.Address = Opportunity_OrderQuoteFilterDTO.Address;
            OrderQuoteFilter.InvoiceAddress = Opportunity_OrderQuoteFilterDTO.InvoiceAddress;
            OrderQuoteFilter.InvoiceProvinceId = Opportunity_OrderQuoteFilterDTO.InvoiceProvinceId;
            OrderQuoteFilter.InvoiceDistrictId = Opportunity_OrderQuoteFilterDTO.InvoiceDistrictId;
            OrderQuoteFilter.InvoiceNationId = Opportunity_OrderQuoteFilterDTO.InvoiceNationId;
            OrderQuoteFilter.ZIPCode = Opportunity_OrderQuoteFilterDTO.ZIPCode;
            OrderQuoteFilter.InvoiceZIPCode = Opportunity_OrderQuoteFilterDTO.InvoiceZIPCode;
            OrderQuoteFilter.AppUserId = Opportunity_OrderQuoteFilterDTO.AppUserId;
            OrderQuoteFilter.ContactId = Opportunity_OrderQuoteFilterDTO.ContactId;
            OrderQuoteFilter.CompanyId = Opportunity_OrderQuoteFilterDTO.CompanyId;
            OrderQuoteFilter.OpportunityId = Opportunity_OrderQuoteFilterDTO.OpportunityId;
            OrderQuoteFilter.OrderQuoteStatusId = Opportunity_OrderQuoteFilterDTO.OrderQuoteStatusId;
            OrderQuoteFilter.SubTotal = Opportunity_OrderQuoteFilterDTO.SubTotal;
            OrderQuoteFilter.Total = Opportunity_OrderQuoteFilterDTO.Total;
            OrderQuoteFilter.TotalTaxAmount = Opportunity_OrderQuoteFilterDTO.TotalTaxAmount;
            OrderQuoteFilter.GeneralDiscountPercentage = Opportunity_OrderQuoteFilterDTO.GeneralDiscountPercentage;
            OrderQuoteFilter.GeneralDiscountAmount = Opportunity_OrderQuoteFilterDTO.GeneralDiscountAmount;
            OrderQuoteFilter.CreatedAt = Opportunity_OrderQuoteFilterDTO.CreatedAt;
            return OrderQuoteFilter;
        }

        #region Email
        [Route(OpportunityRoute.SingleListEmail), HttpPost]
        public async Task<ActionResult<List<Opportunity_ContactDTO>>> SingleListEmail([FromBody] Opportunity_OpportunityEmailFilterDTO Opportunity_OpportunityEmailFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if(Opportunity_OpportunityEmailFilterDTO.OpportunityId == null || Opportunity_OpportunityEmailFilterDTO.OpportunityId.Equal.HasValue == false)
                return new List<Opportunity_ContactDTO>();
            Opportunity Opportunity = await OpportunityService.Get(Opportunity_OpportunityEmailFilterDTO.OpportunityId.Equal.Value);
            if (Opportunity == null)
                return new List<Opportunity_ContactDTO>();

            List<string> Emails = new List<string>();
            Emails.Add(Opportunity.Company?.Email);
            Emails.AddRange(Opportunity.OpportunityContactMappings?.Select(x => x.Contact?.Email).ToList());
            Emails = Emails.Where(x => x != null).ToList();
            if (Opportunity_OpportunityEmailFilterDTO.Email.HasValue && !string.IsNullOrWhiteSpace(Opportunity_OpportunityEmailFilterDTO.Email.StartWith))
                Emails = Emails.Where(x => x.StartsWith(Opportunity_OpportunityEmailFilterDTO.Email.StartWith)).ToList();
            List<Opportunity_ContactDTO> Results = Emails.Select(x => new Opportunity_ContactDTO { Email = x }).ToList();
            return Results;
        }

        [Route(OpportunityRoute.GetOpportunityEmail), HttpPost]
        public async Task<ActionResult<Opportunity_OpportunityEmailDTO>> GetOpportunityEmail([FromBody] Opportunity_OpportunityEmailDTO Opportunity_OpportunityEmailDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityEmail OpportunityEmail = await OpportunityEmailService.Get(Opportunity_OpportunityEmailDTO.Id);
            return new Opportunity_OpportunityEmailDTO(OpportunityEmail);
        }

        [Route(OpportunityRoute.CountOpportunityEmail), HttpPost]
        public async Task<ActionResult<long>> CountOpportunityEmail([FromBody] Opportunity_OpportunityEmailFilterDTO Opportunity_OpportunityEmailFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            OpportunityEmailFilter OpportunityEmailFilter = new OpportunityEmailFilter();
            OpportunityEmailFilter.Id = Opportunity_OpportunityEmailFilterDTO.Id;
            OpportunityEmailFilter.Title = Opportunity_OpportunityEmailFilterDTO.Title;
            OpportunityEmailFilter.Content = Opportunity_OpportunityEmailFilterDTO.Content;
            OpportunityEmailFilter.CreatorId = Opportunity_OpportunityEmailFilterDTO.CreatorId;
            OpportunityEmailFilter.CreatedAt = Opportunity_OpportunityEmailFilterDTO.CreatedAt;
            OpportunityEmailFilter.OpportunityId = Opportunity_OpportunityEmailFilterDTO.OpportunityId;
            OpportunityEmailFilter.EmailStatusId = Opportunity_OpportunityEmailFilterDTO.EmailStatusId;
            OpportunityEmailFilter.Reciepient = Opportunity_OpportunityEmailFilterDTO.Reciepient;

            return await OpportunityEmailService.Count(OpportunityEmailFilter);
        }

        [Route(OpportunityRoute.ListOpportunityEmail), HttpPost]
        public async Task<ActionResult<List<Opportunity_OpportunityEmailDTO>>> ListOpportunityEmail([FromBody] Opportunity_OpportunityEmailFilterDTO Opportunity_OpportunityEmailFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            OpportunityEmailFilter OpportunityEmailFilter = new OpportunityEmailFilter();
            OpportunityEmailFilter.Skip = Opportunity_OpportunityEmailFilterDTO.Skip;
            OpportunityEmailFilter.Take = Opportunity_OpportunityEmailFilterDTO.Take;
            OpportunityEmailFilter.OrderBy = OpportunityEmailOrder.Id;
            OpportunityEmailFilter.OrderType = OrderType.ASC;
            OpportunityEmailFilter.Selects = OpportunityEmailSelect.ALL;
            OpportunityEmailFilter.Id = Opportunity_OpportunityEmailFilterDTO.Id;
            OpportunityEmailFilter.Title = Opportunity_OpportunityEmailFilterDTO.Title;
            OpportunityEmailFilter.Content = Opportunity_OpportunityEmailFilterDTO.Content;
            OpportunityEmailFilter.CreatorId = Opportunity_OpportunityEmailFilterDTO.CreatorId;
            OpportunityEmailFilter.CreatedAt = Opportunity_OpportunityEmailFilterDTO.CreatedAt;
            OpportunityEmailFilter.OpportunityId = Opportunity_OpportunityEmailFilterDTO.OpportunityId;
            OpportunityEmailFilter.EmailStatusId = Opportunity_OpportunityEmailFilterDTO.EmailStatusId;
            OpportunityEmailFilter.Reciepient = Opportunity_OpportunityEmailFilterDTO.Reciepient;

            List<OpportunityEmail> OpportunityEmails = await OpportunityEmailService.List(OpportunityEmailFilter);
            List<Opportunity_OpportunityEmailDTO> Opportunity_OpportunityEmailDTOs = OpportunityEmails
                .Select(x => new Opportunity_OpportunityEmailDTO(x)).ToList();
            return Opportunity_OpportunityEmailDTOs;
        }
        #endregion
    }
}


