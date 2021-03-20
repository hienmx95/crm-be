using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using Microsoft.AspNetCore.Mvc;
using CRM.Entities;


namespace CRM.Rpc.contact
{
    public partial class ContactController : RpcController
    {
        [Route(ContactRoute.CountActivity), HttpPost]
        public async Task<ActionResult<int>> CountActivity([FromBody] Contact_ContactActivityFilterDTO Contact_ContactActivityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactActivityFilter ContactActivityFilter = ConvertFilterDTOToFilterEntity(Contact_ContactActivityFilterDTO);
            ContactActivityFilter = await ContactActivityService.ToFilter(ContactActivityFilter);
            int count = await ContactActivityService.Count(ContactActivityFilter);
            return count;
        }

        [Route(ContactRoute.ListActivity), HttpPost]
        public async Task<ActionResult<List<Contact_ContactActivityDTO>>> ListActivity([FromBody] Contact_ContactActivityFilterDTO Contact_ContactActivityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactActivityFilter ContactActivityFilter = ConvertFilterDTOToFilterEntity(Contact_ContactActivityFilterDTO);
            ContactActivityFilter = await ContactActivityService.ToFilter(ContactActivityFilter);
            List<ContactActivity> ContactActivities = await ContactActivityService.List(ContactActivityFilter);
            List<Contact_ContactActivityDTO> Contact_ContactActivityDTOs = ContactActivities
                .Select(c => new Contact_ContactActivityDTO(c)).ToList();
            return Contact_ContactActivityDTOs;
        }

        [Route(ContactRoute.GetActivity), HttpPost]
        public async Task<ActionResult<Contact_ContactActivityDTO>> GetActivity([FromBody] Contact_ContactActivityDTO Contact_ContactActivityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactActivity ContactActivity = await ContactActivityService.Get(Contact_ContactActivityDTO.Id);
            return new Contact_ContactActivityDTO(ContactActivity);
        }

        private ContactActivityFilter ConvertFilterDTOToFilterEntity(Contact_ContactActivityFilterDTO Contact_ContactActivityFilterDTO)
        {
            ContactActivityFilter ContactActivityFilter = new ContactActivityFilter();
            ContactActivityFilter.Selects = ContactActivitySelect.ALL;
            ContactActivityFilter.Skip = Contact_ContactActivityFilterDTO.Skip;
            ContactActivityFilter.Take = Contact_ContactActivityFilterDTO.Take;
            ContactActivityFilter.OrderBy = Contact_ContactActivityFilterDTO.OrderBy;
            ContactActivityFilter.OrderType = Contact_ContactActivityFilterDTO.OrderType;

            ContactActivityFilter.Id = Contact_ContactActivityFilterDTO.Id;
            ContactActivityFilter.Title = Contact_ContactActivityFilterDTO.Title;
            ContactActivityFilter.FromDate = Contact_ContactActivityFilterDTO.FromDate;
            ContactActivityFilter.ToDate = Contact_ContactActivityFilterDTO.ToDate;
            ContactActivityFilter.ActivityTypeId = Contact_ContactActivityFilterDTO.ActivityTypeId;
            ContactActivityFilter.ActivityPriorityId = Contact_ContactActivityFilterDTO.ActivityPriorityId;
            ContactActivityFilter.Description = Contact_ContactActivityFilterDTO.Description;
            ContactActivityFilter.Address = Contact_ContactActivityFilterDTO.Address;
            ContactActivityFilter.ContactId = Contact_ContactActivityFilterDTO.ContactId;
            ContactActivityFilter.AppUserId = Contact_ContactActivityFilterDTO.AppUserId;
            ContactActivityFilter.ActivityStatusId = Contact_ContactActivityFilterDTO.ActivityStatusId;
            ContactActivityFilter.CreatedAt = Contact_ContactActivityFilterDTO.CreatedAt;
            ContactActivityFilter.UpdatedAt = Contact_ContactActivityFilterDTO.UpdatedAt;
            return ContactActivityFilter;
        }

        #region callLog
        [Route(ContactRoute.CountCallLog), HttpPost]
        public async Task<ActionResult<int>> CountCallLog([FromBody] Contact_CallLogFilterDTO Contact_CallLogFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLogFilter CallLogFilter = ConvertFilterCallLog(Contact_CallLogFilterDTO);
            CallLogFilter = await CallLogService.ToFilter(CallLogFilter);
            int count = await CallLogService.Count(CallLogFilter);
            return count;
        }

        [Route(ContactRoute.ListCallLog), HttpPost]
        public async Task<ActionResult<List<Contact_CallLogDTO>>> ListCallLog([FromBody] Contact_CallLogFilterDTO Contact_CallLogFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLogFilter CallLogFilter = ConvertFilterCallLog(Contact_CallLogFilterDTO);
            CallLogFilter = await CallLogService.ToFilter(CallLogFilter);
            List<CallLog> CallLogs = await CallLogService.List(CallLogFilter);
            List<Contact_CallLogDTO> Contact_CallLogDTOs = CallLogs
                .Select(c => new Contact_CallLogDTO(c)).ToList();
            return Contact_CallLogDTOs;
        }

        [Route(ContactRoute.GetCallLog), HttpPost]
        public async Task<ActionResult<Contact_CallLogDTO>> GetCallLog([FromBody] Contact_CallLogDTO Contact_CallLogDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLog CallLog = await CallLogService.Get(Contact_CallLogDTO.Id);
            return new Contact_CallLogDTO(CallLog);
        }

        private CallLogFilter ConvertFilterCallLog(Contact_CallLogFilterDTO Contact_CallLogFilterDTO)
        {
            CallLogFilter CallLogFilter = new CallLogFilter();
            CallLogFilter.Selects = CallLogSelect.ALL;
            CallLogFilter.Skip = Contact_CallLogFilterDTO.Skip;
            CallLogFilter.Take = Contact_CallLogFilterDTO.Take;
            CallLogFilter.OrderBy = Contact_CallLogFilterDTO.OrderBy;
            CallLogFilter.OrderType = Contact_CallLogFilterDTO.OrderType;

            CallLogFilter.Id = Contact_CallLogFilterDTO.Id;
            CallLogFilter.Title = Contact_CallLogFilterDTO.Title;
            CallLogFilter.Content = Contact_CallLogFilterDTO.Content;
            CallLogFilter.Phone = Contact_CallLogFilterDTO.Phone;
            CallLogFilter.CallTime = Contact_CallLogFilterDTO.CallTime;
            CallLogFilter.EntityReferenceId = Contact_CallLogFilterDTO.EntityReferenceId;
            CallLogFilter.EntityId = Contact_CallLogFilterDTO.EntityId;
            CallLogFilter.CallTypeId = Contact_CallLogFilterDTO.CallTypeId;
            CallLogFilter.CallEmotionId = Contact_CallLogFilterDTO.CallEmotionId;
            CallLogFilter.AppUserId = Contact_CallLogFilterDTO.AppUserId;
            CallLogFilter.CreatorId = Contact_CallLogFilterDTO.CreatorId;
            return CallLogFilter;
        }
        #endregion

        #region opportunity
        [Route(ContactRoute.ListOpportunity), HttpPost]
        public async Task<ActionResult<List<Contact_OpportunityDTO>>> ListOpportunity([FromBody] Contact_OpportunityFilterDTO Contact_OpportunityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityFilter OpportunityFilter = ConvertFilterOpportunity(Contact_OpportunityFilterDTO);
            OpportunityFilter = await OpportunityService.ToFilter(OpportunityFilter);
            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            List<Contact_OpportunityDTO> Contact_OpportunityDTOs = Opportunities
                .Select(c => new Contact_OpportunityDTO(c)).ToList();
            return Contact_OpportunityDTOs;
        }

        [Route(ContactRoute.CountOpportunity), HttpPost]
        public async Task<ActionResult<int>> CountOpportunity([FromBody] Contact_OpportunityFilterDTO Contact_OpportunityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityFilter OpportunityFilter = ConvertFilterOpportunity(Contact_OpportunityFilterDTO);
            OpportunityFilter = await OpportunityService.ToFilter(OpportunityFilter);
            int count = await OpportunityService.Count(OpportunityFilter);
            return count;
        }

        [Route(ContactRoute.GetOpportunity), HttpPost]
        public async Task<ActionResult<Contact_OpportunityDTO>> GetOpportunity([FromBody] Contact_OpportunityDTO Contact_OpportunityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Opportunity Opportunity = await OpportunityService.Get(Contact_OpportunityDTO.Id);
            return new Contact_OpportunityDTO(Opportunity);
        }

        private OpportunityFilter ConvertFilterOpportunity(Contact_OpportunityFilterDTO Contact_OpportunityFilterDTO)
        {
            OpportunityFilter OpportunityFilter = new OpportunityFilter();
            OpportunityFilter.Selects = OpportunitySelect.ALL;
            OpportunityFilter.Skip = Contact_OpportunityFilterDTO.Skip;
            OpportunityFilter.Take = Contact_OpportunityFilterDTO.Take;
            OpportunityFilter.OrderBy = Contact_OpportunityFilterDTO.OrderBy;
            OpportunityFilter.OrderType = Contact_OpportunityFilterDTO.OrderType;

            OpportunityFilter.Id = Contact_OpportunityFilterDTO.Id;
            OpportunityFilter.Name = Contact_OpportunityFilterDTO.Name;
            OpportunityFilter.CompanyId = Contact_OpportunityFilterDTO.CompanyId;
            OpportunityFilter.CustomerLeadId = Contact_OpportunityFilterDTO.CustomerLeadId;
            OpportunityFilter.ClosingDate = Contact_OpportunityFilterDTO.ClosingDate;
            OpportunityFilter.SaleStageId = Contact_OpportunityFilterDTO.SaleStageId;
            OpportunityFilter.ProbabilityId = Contact_OpportunityFilterDTO.ProbabilityId;
            OpportunityFilter.PotentialResultId = Contact_OpportunityFilterDTO.PotentialResultId;
            OpportunityFilter.LeadSourceId = Contact_OpportunityFilterDTO.LeadSourceId;
            OpportunityFilter.AppUserId = Contact_OpportunityFilterDTO.AppUserId;
            OpportunityFilter.CurrencyId = Contact_OpportunityFilterDTO.CurrencyId;
            OpportunityFilter.Amount = Contact_OpportunityFilterDTO.Amount;
            OpportunityFilter.ForecastAmount = Contact_OpportunityFilterDTO.ForecastAmount;
            OpportunityFilter.Description = Contact_OpportunityFilterDTO.Description;
            OpportunityFilter.OpportunityResultTypeId = Contact_OpportunityFilterDTO.OpportunityResultTypeId;
            OpportunityFilter.CreatorId = Contact_OpportunityFilterDTO.CreatorId;
            OpportunityFilter.ContactId = Contact_OpportunityFilterDTO.ContactId;
            OpportunityFilter.CreatedAt = Contact_OpportunityFilterDTO.CreatedAt;
            OpportunityFilter.UpdatedAt = Contact_OpportunityFilterDTO.UpdatedAt;
            return OpportunityFilter;
        }
        #endregion

        #region orderQuote
        [Route(ContactRoute.ListOrderQuote), HttpPost]
        public async Task<ActionResult<List<Contact_OrderQuoteDTO>>> ListOrderQuote([FromBody] Contact_OrderQuoteFilterDTO Contact_OrderQuoteFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuoteFilter OrderQuoteFilter = ConvertFilterOrderQuote(Contact_OrderQuoteFilterDTO);
            OrderQuoteFilter = await OrderQuoteService.ToFilter(OrderQuoteFilter);
            List<OrderQuote> OrderQuotes = await OrderQuoteService.List(OrderQuoteFilter);
            List<Contact_OrderQuoteDTO> Contact_OrderQuoteDTOs = OrderQuotes
                .Select(c => new Contact_OrderQuoteDTO(c)).ToList();
            return Contact_OrderQuoteDTOs;
        }

        [Route(ContactRoute.CountOrderQuote), HttpPost]
        public async Task<ActionResult<int>> CountOrderQuote([FromBody] Contact_OrderQuoteFilterDTO Contact_OrderQuoteFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuoteFilter OrderQuoteFilter = ConvertFilterOrderQuote(Contact_OrderQuoteFilterDTO);
            OrderQuoteFilter = await OrderQuoteService.ToFilter(OrderQuoteFilter);
            int count = await OrderQuoteService.Count(OrderQuoteFilter);
            return count;
        }

        [Route(ContactRoute.GetOrderQuote), HttpPost]
        public async Task<ActionResult<Contact_OrderQuoteDTO>> GetOrderQuote([FromBody] Contact_OrderQuoteDTO Contact_OrderQuoteDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuote OrderQuote = await OrderQuoteService.Get(Contact_OrderQuoteDTO.Id);
            return new Contact_OrderQuoteDTO(OrderQuote);
        }

        private OrderQuoteFilter ConvertFilterOrderQuote(Contact_OrderQuoteFilterDTO Contact_OrderQuoteFilterDTO)
        {
            OrderQuoteFilter OrderQuoteFilter = new OrderQuoteFilter();
            OrderQuoteFilter.Selects = OrderQuoteSelect.ALL;
            OrderQuoteFilter.Skip = Contact_OrderQuoteFilterDTO.Skip;
            OrderQuoteFilter.Take = Contact_OrderQuoteFilterDTO.Take;
            OrderQuoteFilter.OrderBy = Contact_OrderQuoteFilterDTO.OrderBy;
            OrderQuoteFilter.OrderType = Contact_OrderQuoteFilterDTO.OrderType;

            OrderQuoteFilter.Id = Contact_OrderQuoteFilterDTO.Id;
            OrderQuoteFilter.Subject = Contact_OrderQuoteFilterDTO.Subject;
            OrderQuoteFilter.NationId = Contact_OrderQuoteFilterDTO.NationId;
            OrderQuoteFilter.ProvinceId = Contact_OrderQuoteFilterDTO.ProvinceId;
            OrderQuoteFilter.DistrictId = Contact_OrderQuoteFilterDTO.DistrictId;
            OrderQuoteFilter.Address = Contact_OrderQuoteFilterDTO.Address;
            OrderQuoteFilter.InvoiceAddress = Contact_OrderQuoteFilterDTO.InvoiceAddress;
            OrderQuoteFilter.InvoiceProvinceId = Contact_OrderQuoteFilterDTO.InvoiceProvinceId;
            OrderQuoteFilter.InvoiceDistrictId = Contact_OrderQuoteFilterDTO.InvoiceDistrictId;
            OrderQuoteFilter.InvoiceNationId = Contact_OrderQuoteFilterDTO.InvoiceNationId;
            OrderQuoteFilter.ZIPCode = Contact_OrderQuoteFilterDTO.ZIPCode;
            OrderQuoteFilter.InvoiceZIPCode = Contact_OrderQuoteFilterDTO.InvoiceZIPCode;
            OrderQuoteFilter.AppUserId = Contact_OrderQuoteFilterDTO.AppUserId;
            OrderQuoteFilter.ContactId = Contact_OrderQuoteFilterDTO.ContactId;
            OrderQuoteFilter.CompanyId = Contact_OrderQuoteFilterDTO.CompanyId;
            OrderQuoteFilter.OpportunityId = Contact_OrderQuoteFilterDTO.OpportunityId;
            OrderQuoteFilter.OrderQuoteStatusId = Contact_OrderQuoteFilterDTO.OrderQuoteStatusId;
            OrderQuoteFilter.SubTotal = Contact_OrderQuoteFilterDTO.SubTotal;
            OrderQuoteFilter.Total = Contact_OrderQuoteFilterDTO.Total;
            OrderQuoteFilter.TotalTaxAmount = Contact_OrderQuoteFilterDTO.TotalTaxAmount;
            OrderQuoteFilter.GeneralDiscountPercentage = Contact_OrderQuoteFilterDTO.GeneralDiscountPercentage;
            OrderQuoteFilter.GeneralDiscountAmount = Contact_OrderQuoteFilterDTO.GeneralDiscountAmount;
            OrderQuoteFilter.CreatedAt = Contact_OrderQuoteFilterDTO.CreatedAt;
            return OrderQuoteFilter;
        }
        #endregion

        #region DirectSalesOrder
        [Route(ContactRoute.ListDirectSalesOrder), HttpPost]
        public async Task<ActionResult<List<Contact_DirectSalesOrderDTO>>> ListDirectSalesOrder([FromBody] Contact_DirectSalesOrderFilterDTO Contact_DirectSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DirectSalesOrderFilter DirectSalesOrderFilter = ConvertFilterDirectSalesOrder(Contact_DirectSalesOrderFilterDTO);
            DirectSalesOrderFilter = await DirectSalesOrderService.ToFilter(DirectSalesOrderFilter);
            List<DirectSalesOrder> DirectSalesOrders = await DirectSalesOrderService.List(DirectSalesOrderFilter);
            List<Contact_DirectSalesOrderDTO> Contact_DirectSalesOrderDTOs = DirectSalesOrders
                .Select(c => new Contact_DirectSalesOrderDTO(c)).ToList();
            return Contact_DirectSalesOrderDTOs;
        }

        [Route(ContactRoute.CountDirectSalesOrder), HttpPost]
        public async Task<ActionResult<int>> CountDirectSalesOrder([FromBody] Contact_DirectSalesOrderFilterDTO Contact_DirectSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DirectSalesOrderFilter DirectSalesOrderFilter = ConvertFilterDirectSalesOrder(Contact_DirectSalesOrderFilterDTO);
            DirectSalesOrderFilter = await DirectSalesOrderService.ToFilter(DirectSalesOrderFilter);
            int count = await DirectSalesOrderService.Count(DirectSalesOrderFilter);
            return count;
        }

        [Route(ContactRoute.GetDirectSalesOrder), HttpPost]
        public async Task<ActionResult<Contact_DirectSalesOrderDTO>> GetDirectSalesOrder([FromBody] Contact_DirectSalesOrderDTO Contact_DirectSalesOrderDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DirectSalesOrder DirectSalesOrder = await DirectSalesOrderService.Get(Contact_DirectSalesOrderDTO.Id);
            return new Contact_DirectSalesOrderDTO(DirectSalesOrder);
        }

        private DirectSalesOrderFilter ConvertFilterDirectSalesOrder(Contact_DirectSalesOrderFilterDTO Contact_DirectSalesOrderFilterDTO)
        {
            DirectSalesOrderFilter DirectSalesOrderFilter = new DirectSalesOrderFilter();
            DirectSalesOrderFilter.Selects = DirectSalesOrderSelect.ALL;
            DirectSalesOrderFilter.Skip = Contact_DirectSalesOrderFilterDTO.Skip;
            DirectSalesOrderFilter.Take = Contact_DirectSalesOrderFilterDTO.Take;
            DirectSalesOrderFilter.OrderBy = Contact_DirectSalesOrderFilterDTO.OrderBy;
            DirectSalesOrderFilter.OrderType = Contact_DirectSalesOrderFilterDTO.OrderType;
            DirectSalesOrderFilter.Id = Contact_DirectSalesOrderFilterDTO.Id;
            DirectSalesOrderFilter.OrganizationId = Contact_DirectSalesOrderFilterDTO.OrganizationId;
            DirectSalesOrderFilter.Code = Contact_DirectSalesOrderFilterDTO.Code;
            DirectSalesOrderFilter.BuyerStoreId = Contact_DirectSalesOrderFilterDTO.BuyerStoreId;
            DirectSalesOrderFilter.PhoneNumber = Contact_DirectSalesOrderFilterDTO.PhoneNumber;
            DirectSalesOrderFilter.StoreAddress = Contact_DirectSalesOrderFilterDTO.StoreAddress;
            DirectSalesOrderFilter.DeliveryAddress = Contact_DirectSalesOrderFilterDTO.DeliveryAddress;
            DirectSalesOrderFilter.AppUserId = Contact_DirectSalesOrderFilterDTO.AppUserId;
            DirectSalesOrderFilter.OrderDate = Contact_DirectSalesOrderFilterDTO.OrderDate;
            DirectSalesOrderFilter.DeliveryDate = Contact_DirectSalesOrderFilterDTO.DeliveryDate;
            DirectSalesOrderFilter.RequestStateId = Contact_DirectSalesOrderFilterDTO.RequestStateId;
            DirectSalesOrderFilter.EditedPriceStatusId = Contact_DirectSalesOrderFilterDTO.EditedPriceStatusId;
            DirectSalesOrderFilter.Note = Contact_DirectSalesOrderFilterDTO.Note;
            DirectSalesOrderFilter.SubTotal = Contact_DirectSalesOrderFilterDTO.SubTotal;
            DirectSalesOrderFilter.GeneralDiscountPercentage = Contact_DirectSalesOrderFilterDTO.GeneralDiscountPercentage;
            DirectSalesOrderFilter.GeneralDiscountAmount = Contact_DirectSalesOrderFilterDTO.GeneralDiscountAmount;
            DirectSalesOrderFilter.TotalTaxAmount = Contact_DirectSalesOrderFilterDTO.TotalTaxAmount;
            DirectSalesOrderFilter.Total = Contact_DirectSalesOrderFilterDTO.Total;
            DirectSalesOrderFilter.StoreStatusId = Contact_DirectSalesOrderFilterDTO.StoreStatusId;
            return DirectSalesOrderFilter;
        }
        #endregion

        [Route(ContactRoute.CountCustomerSalesOrder), HttpPost]
        public async Task<ActionResult<int>> CountCustomerSalesOrder([FromBody] Contact_CustomerSalesOrderFilterDTO Contact_CustomerSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerSalesOrderFilter CustomerSalesOrderFilter = ConvertFilterCustomerSalesOrder(Contact_CustomerSalesOrderFilterDTO);
            CustomerSalesOrderFilter = await CustomerSalesOrderService.ToFilter(CustomerSalesOrderFilter);
            int count = await CustomerSalesOrderService.Count(CustomerSalesOrderFilter);
            return count;
        }

        [Route(ContactRoute.ListCustomerSalesOrder), HttpPost]
        public async Task<ActionResult<List<Contact_CustomerSalesOrderDTO>>> ListCustomerSalesOrder([FromBody] Contact_CustomerSalesOrderFilterDTO Contact_CustomerSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerSalesOrderFilter CustomerSalesOrderFilter = ConvertFilterCustomerSalesOrder(Contact_CustomerSalesOrderFilterDTO);
            CustomerSalesOrderFilter = await CustomerSalesOrderService.ToFilter(CustomerSalesOrderFilter);
            List<CustomerSalesOrder> CustomerSalesOrders = await CustomerSalesOrderService.List(CustomerSalesOrderFilter);
            List<Contact_CustomerSalesOrderDTO> Contact_CustomerSalesOrderDTOs = CustomerSalesOrders
                .Select(c => new Contact_CustomerSalesOrderDTO(c)).ToList();
            return Contact_CustomerSalesOrderDTOs;
        }

        [Route(ContactRoute.GetCustomerSalesOrder), HttpPost]
        public async Task<ActionResult<Contact_CustomerSalesOrderDTO>> GetCustomerSalesOrder([FromBody] Contact_CustomerSalesOrderDTO Contact_CustomerSalesOrderDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerSalesOrder CustomerSalesOrder = await CustomerSalesOrderService.Get(Contact_CustomerSalesOrderDTO.Id);
            return new Contact_CustomerSalesOrderDTO(CustomerSalesOrder);
        }

        private CustomerSalesOrderFilter ConvertFilterCustomerSalesOrder(Contact_CustomerSalesOrderFilterDTO Contact_CustomerSalesOrderFilterDTO)
        {
            CustomerSalesOrderFilter CustomerSalesOrderFilter = new CustomerSalesOrderFilter();
            CustomerSalesOrderFilter.Selects = CustomerSalesOrderSelect.ALL;
            CustomerSalesOrderFilter.Skip = Contact_CustomerSalesOrderFilterDTO.Skip;
            CustomerSalesOrderFilter.Take = Contact_CustomerSalesOrderFilterDTO.Take;
            CustomerSalesOrderFilter.OrderBy = Contact_CustomerSalesOrderFilterDTO.OrderBy;
            CustomerSalesOrderFilter.OrderType = Contact_CustomerSalesOrderFilterDTO.OrderType;

            CustomerSalesOrderFilter.Id = Contact_CustomerSalesOrderFilterDTO.Id;
            CustomerSalesOrderFilter.Code = Contact_CustomerSalesOrderFilterDTO.Code;
            CustomerSalesOrderFilter.CustomerTypeId = Contact_CustomerSalesOrderFilterDTO.CustomerTypeId;
            CustomerSalesOrderFilter.CustomerId = Contact_CustomerSalesOrderFilterDTO.CustomerId;
            CustomerSalesOrderFilter.OpportunityId = Contact_CustomerSalesOrderFilterDTO.OpportunityId;
            CustomerSalesOrderFilter.ContractId = Contact_CustomerSalesOrderFilterDTO.ContractId;
            CustomerSalesOrderFilter.OrderPaymentStatusId = Contact_CustomerSalesOrderFilterDTO.OrderPaymentStatusId;
            CustomerSalesOrderFilter.RequestStateId = Contact_CustomerSalesOrderFilterDTO.RequestStateId;
            CustomerSalesOrderFilter.EditedPriceStatusId = Contact_CustomerSalesOrderFilterDTO.EditedPriceStatusId;
            CustomerSalesOrderFilter.ShippingName = Contact_CustomerSalesOrderFilterDTO.ShippingName;
            CustomerSalesOrderFilter.OrderDate = Contact_CustomerSalesOrderFilterDTO.OrderDate;
            CustomerSalesOrderFilter.DeliveryDate = Contact_CustomerSalesOrderFilterDTO.DeliveryDate;
            CustomerSalesOrderFilter.SalesEmployeeId = Contact_CustomerSalesOrderFilterDTO.SalesEmployeeId;
            CustomerSalesOrderFilter.Note = Contact_CustomerSalesOrderFilterDTO.Note;
            CustomerSalesOrderFilter.InvoiceAddress = Contact_CustomerSalesOrderFilterDTO.InvoiceAddress;
            CustomerSalesOrderFilter.InvoiceNationId = Contact_CustomerSalesOrderFilterDTO.InvoiceNationId;
            CustomerSalesOrderFilter.InvoiceProvinceId = Contact_CustomerSalesOrderFilterDTO.InvoiceProvinceId;
            CustomerSalesOrderFilter.InvoiceDistrictId = Contact_CustomerSalesOrderFilterDTO.InvoiceDistrictId;
            CustomerSalesOrderFilter.InvoiceWardId = Contact_CustomerSalesOrderFilterDTO.InvoiceWardId;
            CustomerSalesOrderFilter.InvoiceZIPCode = Contact_CustomerSalesOrderFilterDTO.InvoiceZIPCode;
            CustomerSalesOrderFilter.DeliveryAddress = Contact_CustomerSalesOrderFilterDTO.DeliveryAddress;
            CustomerSalesOrderFilter.DeliveryNationId = Contact_CustomerSalesOrderFilterDTO.DeliveryNationId;
            CustomerSalesOrderFilter.DeliveryProvinceId = Contact_CustomerSalesOrderFilterDTO.DeliveryProvinceId;
            CustomerSalesOrderFilter.DeliveryDistrictId = Contact_CustomerSalesOrderFilterDTO.DeliveryDistrictId;
            CustomerSalesOrderFilter.DeliveryWardId = Contact_CustomerSalesOrderFilterDTO.DeliveryWardId;
            CustomerSalesOrderFilter.DeliveryZIPCode = Contact_CustomerSalesOrderFilterDTO.DeliveryZIPCode;
            CustomerSalesOrderFilter.SubTotal = Contact_CustomerSalesOrderFilterDTO.SubTotal;
            CustomerSalesOrderFilter.GeneralDiscountPercentage = Contact_CustomerSalesOrderFilterDTO.GeneralDiscountPercentage;
            CustomerSalesOrderFilter.GeneralDiscountAmount = Contact_CustomerSalesOrderFilterDTO.GeneralDiscountAmount;
            CustomerSalesOrderFilter.TotalTaxOther = Contact_CustomerSalesOrderFilterDTO.TotalTaxOther;
            CustomerSalesOrderFilter.TotalTax = Contact_CustomerSalesOrderFilterDTO.TotalTax;
            CustomerSalesOrderFilter.Total = Contact_CustomerSalesOrderFilterDTO.Total;
            CustomerSalesOrderFilter.CreatorId = Contact_CustomerSalesOrderFilterDTO.CreatorId;
            CustomerSalesOrderFilter.OrganizationId = Contact_CustomerSalesOrderFilterDTO.OrganizationId;
            CustomerSalesOrderFilter.RowId = Contact_CustomerSalesOrderFilterDTO.RowId;
            CustomerSalesOrderFilter.CreatedAt = Contact_CustomerSalesOrderFilterDTO.CreatedAt;
            CustomerSalesOrderFilter.UpdatedAt = Contact_CustomerSalesOrderFilterDTO.UpdatedAt;
            return CustomerSalesOrderFilter;
        }
        #region Email
        [Route(ContactRoute.GetContactEmail), HttpPost]
        public async Task<ActionResult<Contact_ContactEmailDTO>> GetContactEmail([FromBody] Contact_ContactEmailDTO Contact_ContactEmailDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactEmail ContactEmail = await ContactEmailService.Get(Contact_ContactEmailDTO.Id);
            return new Contact_ContactEmailDTO(ContactEmail);
        }

        [Route(ContactRoute.CountContactEmail), HttpPost]
        public async Task<ActionResult<long>> CountContactEmail([FromBody] Contact_ContactEmailFilterDTO Contact_ContactEmailFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            ContactEmailFilter ContactEmailFilter = new ContactEmailFilter();
            ContactEmailFilter.Id = Contact_ContactEmailFilterDTO.Id;
            ContactEmailFilter.Title = Contact_ContactEmailFilterDTO.Title;
            ContactEmailFilter.Content = Contact_ContactEmailFilterDTO.Content;
            ContactEmailFilter.CreatorId = Contact_ContactEmailFilterDTO.CreatorId;
            ContactEmailFilter.CreatedAt = Contact_ContactEmailFilterDTO.CreatedAt;
            ContactEmailFilter.ContactId = Contact_ContactEmailFilterDTO.ContactId;
            ContactEmailFilter.EmailStatusId = Contact_ContactEmailFilterDTO.EmailStatusId;
            ContactEmailFilter.Reciepient = Contact_ContactEmailFilterDTO.Reciepient;

            return await ContactEmailService.Count(ContactEmailFilter);
        }

        [Route(ContactRoute.ListContactEmail), HttpPost]
        public async Task<ActionResult<List<Contact_ContactEmailDTO>>> ListContactEmail([FromBody] Contact_ContactEmailFilterDTO Contact_ContactEmailFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            ContactEmailFilter ContactEmailFilter = new ContactEmailFilter();
            ContactEmailFilter.Skip = Contact_ContactEmailFilterDTO.Skip;
            ContactEmailFilter.Take = Contact_ContactEmailFilterDTO.Take;
            ContactEmailFilter.OrderBy = ContactEmailOrder.Id;
            ContactEmailFilter.OrderType = OrderType.ASC;
            ContactEmailFilter.Selects = ContactEmailSelect.ALL;
            ContactEmailFilter.Id = Contact_ContactEmailFilterDTO.Id;
            ContactEmailFilter.Title = Contact_ContactEmailFilterDTO.Title;
            ContactEmailFilter.Content = Contact_ContactEmailFilterDTO.Content;
            ContactEmailFilter.CreatorId = Contact_ContactEmailFilterDTO.CreatorId;
            ContactEmailFilter.CreatedAt = Contact_ContactEmailFilterDTO.CreatedAt;
            ContactEmailFilter.ContactId = Contact_ContactEmailFilterDTO.ContactId;
            ContactEmailFilter.EmailStatusId = Contact_ContactEmailFilterDTO.EmailStatusId;
            ContactEmailFilter.Reciepient = Contact_ContactEmailFilterDTO.Reciepient;

            List<ContactEmail> ContactEmails = await ContactEmailService.List(ContactEmailFilter);
            List<Contact_ContactEmailDTO> Contact_ContactEmailDTOs = ContactEmails
                .Select(x => new Contact_ContactEmailDTO(x)).ToList();
            return Contact_ContactEmailDTOs;
        }
        #endregion
    }
}

