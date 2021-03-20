using CRM.Common;
using CRM.Entities;
using CRM.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.company
{
    public partial class CompanyController : RpcController
    {
        [Route(CompanyRoute.CountActivity), HttpPost]
        public async Task<ActionResult<int>> CountActivity([FromBody] Company_CompanyActivityFilterDTO Company_CompanyActivityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyActivityFilter CompanyActivityFilter = ConvertFilterDTOToFilterEntity(Company_CompanyActivityFilterDTO);
            CompanyActivityFilter = await CompanyActivityService.ToFilter(CompanyActivityFilter);
            int count = await CompanyActivityService.Count(CompanyActivityFilter);
            return count;
        }

        [Route(CompanyRoute.ListActivity), HttpPost]
        public async Task<ActionResult<List<Company_CompanyActivityDTO>>> ListActivity([FromBody] Company_CompanyActivityFilterDTO Company_CompanyActivityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyActivityFilter CompanyActivityFilter = ConvertFilterDTOToFilterEntity(Company_CompanyActivityFilterDTO);
            CompanyActivityFilter = await CompanyActivityService.ToFilter(CompanyActivityFilter);
            List<CompanyActivity> CompanyActivities = await CompanyActivityService.List(CompanyActivityFilter);
            List<Company_CompanyActivityDTO> Company_CompanyActivityDTOs = CompanyActivities
                .Select(c => new Company_CompanyActivityDTO(c)).ToList();
            return Company_CompanyActivityDTOs;
        }

        [Route(CompanyRoute.GetActivity), HttpPost]
        public async Task<ActionResult<Company_CompanyActivityDTO>> GetActivity([FromBody] Company_CompanyActivityDTO Company_CompanyActivityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyActivity CompanyActivity = await CompanyActivityService.Get(Company_CompanyActivityDTO.Id);
            return new Company_CompanyActivityDTO(CompanyActivity);
        }

        private CompanyActivityFilter ConvertFilterDTOToFilterEntity(Company_CompanyActivityFilterDTO Company_CompanyActivityFilterDTO)
        {
            CompanyActivityFilter CompanyActivityFilter = new CompanyActivityFilter();
            CompanyActivityFilter.Selects = CompanyActivitySelect.ALL;
            CompanyActivityFilter.Skip = Company_CompanyActivityFilterDTO.Skip;
            CompanyActivityFilter.Take = Company_CompanyActivityFilterDTO.Take;
            CompanyActivityFilter.OrderBy = Company_CompanyActivityFilterDTO.OrderBy;
            CompanyActivityFilter.OrderType = Company_CompanyActivityFilterDTO.OrderType;

            CompanyActivityFilter.Id = Company_CompanyActivityFilterDTO.Id;
            CompanyActivityFilter.Title = Company_CompanyActivityFilterDTO.Title;
            CompanyActivityFilter.FromDate = Company_CompanyActivityFilterDTO.FromDate;
            CompanyActivityFilter.ToDate = Company_CompanyActivityFilterDTO.ToDate;
            CompanyActivityFilter.ActivityTypeId = Company_CompanyActivityFilterDTO.ActivityTypeId;
            CompanyActivityFilter.ActivityPriorityId = Company_CompanyActivityFilterDTO.ActivityPriorityId;
            CompanyActivityFilter.Description = Company_CompanyActivityFilterDTO.Description;
            CompanyActivityFilter.Address = Company_CompanyActivityFilterDTO.Address;
            CompanyActivityFilter.CompanyId = Company_CompanyActivityFilterDTO.CompanyId;
            CompanyActivityFilter.AppUserId = Company_CompanyActivityFilterDTO.AppUserId;
            CompanyActivityFilter.ActivityStatusId = Company_CompanyActivityFilterDTO.ActivityStatusId;
            CompanyActivityFilter.CreatedAt = Company_CompanyActivityFilterDTO.CreatedAt;
            CompanyActivityFilter.UpdatedAt = Company_CompanyActivityFilterDTO.UpdatedAt;
            return CompanyActivityFilter;
        }

        #region CallLog
        [Route(CompanyRoute.CountCallLog), HttpPost]
        public async Task<ActionResult<int>> CountCallLog([FromBody] Company_CallLogFilterDTO Company_CallLogFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLogFilter CallLogFilter = ConvertFilterCallLog(Company_CallLogFilterDTO);
            CallLogFilter = await CallLogService.ToFilter(CallLogFilter);
            int count = await CallLogService.Count(CallLogFilter);
            return count;
        }

        [Route(CompanyRoute.ListCallLog), HttpPost]
        public async Task<ActionResult<List<Company_CallLogDTO>>> ListCallLog([FromBody] Company_CallLogFilterDTO Company_CallLogFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLogFilter CallLogFilter = ConvertFilterCallLog(Company_CallLogFilterDTO);
            CallLogFilter = await CallLogService.ToFilter(CallLogFilter);
            List<CallLog> CallLogs = await CallLogService.List(CallLogFilter);
            List<Company_CallLogDTO> Company_CallLogDTOs = CallLogs
                .Select(c => new Company_CallLogDTO(c)).ToList();
            return Company_CallLogDTOs;
        }

        [Route(CompanyRoute.GetCallLog), HttpPost]
        public async Task<ActionResult<Company_CallLogDTO>> GetCallLog([FromBody] Company_CallLogDTO Company_CallLogDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLog CallLog = await CallLogService.Get(Company_CallLogDTO.Id);
            return new Company_CallLogDTO(CallLog);
        }

        private CallLogFilter ConvertFilterCallLog(Company_CallLogFilterDTO Company_CallLogFilterDTO)
        {
            CallLogFilter CallLogFilter = new CallLogFilter();
            CallLogFilter.Selects = CallLogSelect.ALL;
            CallLogFilter.Skip = Company_CallLogFilterDTO.Skip;
            CallLogFilter.Take = Company_CallLogFilterDTO.Take;
            CallLogFilter.OrderBy = Company_CallLogFilterDTO.OrderBy;
            CallLogFilter.OrderType = Company_CallLogFilterDTO.OrderType;
            CallLogFilter.Id = Company_CallLogFilterDTO.Id;
            CallLogFilter.EntityReferenceId = Company_CallLogFilterDTO.EntityReferenceId;
            CallLogFilter.CallTypeId = Company_CallLogFilterDTO.CallTypeId;
            CallLogFilter.AppUserId = Company_CallLogFilterDTO.AppUserId;
            CallLogFilter.Title = Company_CallLogFilterDTO.Title;
            CallLogFilter.Content = Company_CallLogFilterDTO.Content;
            CallLogFilter.Phone = Company_CallLogFilterDTO.Phone;
            CallLogFilter.CallTime = Company_CallLogFilterDTO.CallTime;
            return CallLogFilter;
        }
        #endregion

        #region Item
        [Route(CompanyRoute.CountItem), HttpPost]
        public async Task<ActionResult<long>> CountItem([FromBody] Company_ItemFilterDTO Company_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Id = Company_ItemFilterDTO.Id;
            ItemFilter.ProductId = Company_ItemFilterDTO.ProductId;
            ItemFilter.ProductTypeId = Company_ItemFilterDTO.ProductTypeId;
            ItemFilter.ProductGroupingId = Company_ItemFilterDTO.ProductGroupingId;
            ItemFilter.Code = Company_ItemFilterDTO.Code;
            ItemFilter.Name = Company_ItemFilterDTO.Name;
            ItemFilter.ScanCode = Company_ItemFilterDTO.ScanCode;
            ItemFilter.OtherName = Company_ItemFilterDTO.OtherName;
            ItemFilter.SalePrice = Company_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = Company_ItemFilterDTO.RetailPrice;
            ItemFilter.Search = Company_ItemFilterDTO.Search;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            return await ItemService.Count(ItemFilter);
        }

        [Route(CompanyRoute.ListItem), HttpPost]
        public async Task<ActionResult<List<Company_ItemDTO>>> ListItem([FromBody] Company_ItemFilterDTO Company_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = Company_ItemFilterDTO.Skip;
            ItemFilter.Take = Company_ItemFilterDTO.Take;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            ItemFilter.Id = Company_ItemFilterDTO.Id;
            ItemFilter.ProductId = Company_ItemFilterDTO.ProductId;
            ItemFilter.ProductTypeId = Company_ItemFilterDTO.ProductTypeId;
            ItemFilter.ProductGroupingId = Company_ItemFilterDTO.ProductGroupingId;
            ItemFilter.Code = Company_ItemFilterDTO.Code;
            ItemFilter.Name = Company_ItemFilterDTO.Name;
            ItemFilter.ScanCode = Company_ItemFilterDTO.ScanCode;
            ItemFilter.OtherName = Company_ItemFilterDTO.OtherName;
            ItemFilter.SalePrice = Company_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = Company_ItemFilterDTO.RetailPrice;
            ItemFilter.Search = Company_ItemFilterDTO.Search;
            ItemFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<Item> Items = await ItemService.List(ItemFilter);
            List<Company_ItemDTO> Company_ItemDTOs = Items
                .Select(x => new Company_ItemDTO(x)).ToList();
            return Company_ItemDTOs;
        }
        #endregion

        #region Contact
        [Route(CompanyRoute.CountContact), HttpPost]
        public async Task<ActionResult<int>> CountContact([FromBody] Company_ContactFilterDTO Company_ContactFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactFilter ContactFilter = ConvertFilterContact(Company_ContactFilterDTO);
            ContactFilter = await ContactService.ToFilter(ContactFilter);
            int count = await ContactService.Count(ContactFilter);
            return count;
        }

        [Route(CompanyRoute.ListContact), HttpPost]
        public async Task<ActionResult<List<Company_ContactDTO>>> ListContact([FromBody] Company_ContactFilterDTO Company_ContactFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactFilter ContactFilter = ConvertFilterContact(Company_ContactFilterDTO);
            ContactFilter = await ContactService.ToFilter(ContactFilter);
            List<Contact> Contacts = await ContactService.List(ContactFilter);
            List<Company_ContactDTO> Company_ContactDTOs = Contacts
                .Select(c => new Company_ContactDTO(c)).ToList();
            return Company_ContactDTOs;
        }

        [Route(CompanyRoute.GetContact), HttpPost]
        public async Task<ActionResult<Company_ContactDTO>> GetContact([FromBody] Company_ContactDTO Company_ContactDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Contact Contact = await ContactService.Get(Company_ContactDTO.Id);
            return new Company_ContactDTO(Contact);
        }

        private ContactFilter ConvertFilterContact(Company_ContactFilterDTO Company_ContactFilterDTO)
        {
            ContactFilter ContactFilter = new ContactFilter();
            ContactFilter.Selects = ContactSelect.ALL;
            ContactFilter.Skip = Company_ContactFilterDTO.Skip;
            ContactFilter.Take = Company_ContactFilterDTO.Take;
            ContactFilter.OrderBy = Company_ContactFilterDTO.OrderBy;
            ContactFilter.OrderType = Company_ContactFilterDTO.OrderType;

            ContactFilter.Id = Company_ContactFilterDTO.Id;
            ContactFilter.Name = Company_ContactFilterDTO.Name;
            ContactFilter.ProfessionId = Company_ContactFilterDTO.ProfessionId;
            ContactFilter.CompanyId = Company_ContactFilterDTO.CompanyId;
            ContactFilter.ProvinceId = Company_ContactFilterDTO.ProvinceId;
            ContactFilter.DistrictId = Company_ContactFilterDTO.DistrictId;
            ContactFilter.NationId = Company_ContactFilterDTO.NationId;
            ContactFilter.CustomerLeadId = Company_ContactFilterDTO.CustomerLeadId;
            ContactFilter.ImageId = Company_ContactFilterDTO.ImageId;
            ContactFilter.Description = Company_ContactFilterDTO.Description;
            ContactFilter.Address = Company_ContactFilterDTO.Address;
            ContactFilter.EmailOther = Company_ContactFilterDTO.EmailOther;
            ContactFilter.DateOfBirth = Company_ContactFilterDTO.DateOfBirth;
            ContactFilter.Phone = Company_ContactFilterDTO.Phone;
            ContactFilter.PhoneHome = Company_ContactFilterDTO.PhoneHome;
            ContactFilter.FAX = Company_ContactFilterDTO.FAX;
            ContactFilter.Email = Company_ContactFilterDTO.Email;
            ContactFilter.ZIPCode = Company_ContactFilterDTO.ZIPCode;
            ContactFilter.SexId = Company_ContactFilterDTO.SexId;
            ContactFilter.AppUserId = Company_ContactFilterDTO.AppUserId;
            ContactFilter.PositionId = Company_ContactFilterDTO.PositionId;
            ContactFilter.Department = Company_ContactFilterDTO.Department;
            ContactFilter.ContactStatusId = Company_ContactFilterDTO.ContactStatusId;
            ContactFilter.CreatedAt = Company_ContactFilterDTO.CreatedAt;
            ContactFilter.UpdatedAt = Company_ContactFilterDTO.UpdatedAt;
            return ContactFilter;
        }
        #endregion

        [Route(CompanyRoute.CountContract), HttpPost]
        public async Task<ActionResult<int>> CountContract([FromBody] Company_ContractFilterDTO Company_ContractFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContractFilter ContractFilter = ConvertContractFilterEntity(Company_ContractFilterDTO);
            ContractFilter = await ContractService.ToFilter(ContractFilter);
            int count = await ContractService.Count(ContractFilter);
            return count;
        }

        [Route(CompanyRoute.ListContract), HttpPost]
        public async Task<ActionResult<List<Company_ContractDTO>>> ListContract([FromBody] Company_ContractFilterDTO Company_ContractFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContractFilter ContractFilter = ConvertContractFilterEntity(Company_ContractFilterDTO);
            ContractFilter = await ContractService.ToFilter(ContractFilter);
            List<Contract> Contracts = await ContractService.List(ContractFilter);
            List<Company_ContractDTO> Company_ContractDTOs = Contracts
                .Select(c => new Company_ContractDTO(c)).ToList();
            return Company_ContractDTOs;
        }

        [Route(CompanyRoute.GetContract), HttpPost]
        public async Task<ActionResult<Company_ContractDTO>> GetContract([FromBody] Company_ContractDTO Company_ContractDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Contract Contract = await ContractService.Get(Company_ContractDTO.Id);
            return new Company_ContractDTO(Contract);
        }

        private ContractFilter ConvertContractFilterEntity(Company_ContractFilterDTO Company_ContractFilterDTO)
        {
            ContractFilter ContractFilter = new ContractFilter();
            ContractFilter.Selects = ContractSelect.ALL;
            ContractFilter.Skip = Company_ContractFilterDTO.Skip;
            ContractFilter.Take = Company_ContractFilterDTO.Take;
            ContractFilter.OrderBy = Company_ContractFilterDTO.OrderBy;
            ContractFilter.OrderType = Company_ContractFilterDTO.OrderType;

            ContractFilter.Id = Company_ContractFilterDTO.Id;
            ContractFilter.Code = Company_ContractFilterDTO.Code;
            ContractFilter.Name = Company_ContractFilterDTO.Name;
            ContractFilter.CompanyId = Company_ContractFilterDTO.CompanyId;
            ContractFilter.OpportunityId = Company_ContractFilterDTO.OpportunityId;
            ContractFilter.CompanyId = Company_ContractFilterDTO.CompanyId;
            ContractFilter.ContractTypeId = Company_ContractFilterDTO.ContractTypeId;
            ContractFilter.TotalValue = Company_ContractFilterDTO.TotalValue;
            ContractFilter.CurrencyId = Company_ContractFilterDTO.CurrencyId;
            ContractFilter.ValidityDate = Company_ContractFilterDTO.ValidityDate;
            ContractFilter.ExpirationDate = Company_ContractFilterDTO.ExpirationDate;
            ContractFilter.AppUserId = Company_ContractFilterDTO.AppUserId;
            ContractFilter.DeliveryUnit = Company_ContractFilterDTO.DeliveryUnit;
            ContractFilter.ContractStatusId = Company_ContractFilterDTO.ContractStatusId;
            ContractFilter.PaymentStatusId = Company_ContractFilterDTO.PaymentStatusId;
            ContractFilter.InvoiceAddress = Company_ContractFilterDTO.InvoiceAddress;
            ContractFilter.InvoiceNationId = Company_ContractFilterDTO.InvoiceNationId;
            ContractFilter.InvoiceProvinceId = Company_ContractFilterDTO.InvoiceProvinceId;
            ContractFilter.InvoiceDistrictId = Company_ContractFilterDTO.InvoiceDistrictId;
            ContractFilter.InvoiceZipCode = Company_ContractFilterDTO.InvoiceZipCode;
            ContractFilter.ReceiveAddress = Company_ContractFilterDTO.ReceiveAddress;
            ContractFilter.ReceiveNationId = Company_ContractFilterDTO.ReceiveNationId;
            ContractFilter.ReceiveProvinceId = Company_ContractFilterDTO.ReceiveProvinceId;
            ContractFilter.ReceiveDistrictId = Company_ContractFilterDTO.ReceiveDistrictId;
            ContractFilter.ReceiveZipCode = Company_ContractFilterDTO.ReceiveZipCode;
            ContractFilter.TermAndCondition = Company_ContractFilterDTO.TermAndCondition;
            ContractFilter.CreatorId = Company_ContractFilterDTO.CreatorId;
            ContractFilter.OrganizationId = Company_ContractFilterDTO.OrganizationId;
            return ContractFilter;
        }

        #region Order Qoute
        [Route(CompanyRoute.CountOrderQuote), HttpPost]
        public async Task<ActionResult<int>> CountOrderQuote([FromBody] Company_OrderQuoteFilterDTO Company_OrderQuoteFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuoteFilter OrderQuoteFilter = ConvertFilterOrderQuote(Company_OrderQuoteFilterDTO);
            OrderQuoteFilter = await OrderQuoteService.ToFilter(OrderQuoteFilter);
            int count = await OrderQuoteService.Count(OrderQuoteFilter);
            return count;
        }

        [Route(CompanyRoute.ListOrderQuote), HttpPost]
        public async Task<ActionResult<List<Company_OrderQuoteDTO>>> ListOrderQuote([FromBody] Company_OrderQuoteFilterDTO Company_OrderQuoteFilterDTO)
        {
            if (UnAuthorization) return Forbid(); if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuoteFilter OrderQuoteFilter = ConvertFilterOrderQuote(Company_OrderQuoteFilterDTO);
            OrderQuoteFilter = await OrderQuoteService.ToFilter(OrderQuoteFilter);
            List<OrderQuote> OrderQuotes = await OrderQuoteService.List(OrderQuoteFilter);
            List<Company_OrderQuoteDTO> Company_OrderQuoteDTOs = OrderQuotes
                .Select(c => new Company_OrderQuoteDTO(c)).ToList();
            return Company_OrderQuoteDTOs;
        }

        [Route(CompanyRoute.GetOrderQuote), HttpPost]
        public async Task<ActionResult<Company_OrderQuoteDTO>> GetOrderQuote([FromBody] Company_OrderQuoteDTO Company_OrderQuoteDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuote OrderQuote = await OrderQuoteService.Get(Company_OrderQuoteDTO.Id);
            return new Company_OrderQuoteDTO(OrderQuote);
        }

        private OrderQuoteFilter ConvertFilterOrderQuote(Company_OrderQuoteFilterDTO Company_OrderQuoteFilterDTO)
        {
            OrderQuoteFilter OrderQuoteFilter = new OrderQuoteFilter();
            OrderQuoteFilter.Selects = OrderQuoteSelect.ALL;
            OrderQuoteFilter.Skip = Company_OrderQuoteFilterDTO.Skip;
            OrderQuoteFilter.Take = Company_OrderQuoteFilterDTO.Take;
            OrderQuoteFilter.OrderBy = Company_OrderQuoteFilterDTO.OrderBy;
            OrderQuoteFilter.OrderType = Company_OrderQuoteFilterDTO.OrderType;

            OrderQuoteFilter.Id = Company_OrderQuoteFilterDTO.Id;
            OrderQuoteFilter.Subject = Company_OrderQuoteFilterDTO.Subject;
            OrderQuoteFilter.NationId = Company_OrderQuoteFilterDTO.NationId;
            OrderQuoteFilter.ProvinceId = Company_OrderQuoteFilterDTO.ProvinceId;
            OrderQuoteFilter.DistrictId = Company_OrderQuoteFilterDTO.DistrictId;
            OrderQuoteFilter.Address = Company_OrderQuoteFilterDTO.Address;
            OrderQuoteFilter.InvoiceAddress = Company_OrderQuoteFilterDTO.InvoiceAddress;
            OrderQuoteFilter.InvoiceProvinceId = Company_OrderQuoteFilterDTO.InvoiceProvinceId;
            OrderQuoteFilter.InvoiceDistrictId = Company_OrderQuoteFilterDTO.InvoiceDistrictId;
            OrderQuoteFilter.InvoiceNationId = Company_OrderQuoteFilterDTO.InvoiceNationId;
            OrderQuoteFilter.ZIPCode = Company_OrderQuoteFilterDTO.ZIPCode;
            OrderQuoteFilter.InvoiceZIPCode = Company_OrderQuoteFilterDTO.InvoiceZIPCode;
            OrderQuoteFilter.AppUserId = Company_OrderQuoteFilterDTO.AppUserId;
            OrderQuoteFilter.ContactId = Company_OrderQuoteFilterDTO.ContactId;
            OrderQuoteFilter.CompanyId = Company_OrderQuoteFilterDTO.CompanyId;
            OrderQuoteFilter.OpportunityId = Company_OrderQuoteFilterDTO.OpportunityId;
            OrderQuoteFilter.OrderQuoteStatusId = Company_OrderQuoteFilterDTO.OrderQuoteStatusId;
            OrderQuoteFilter.SubTotal = Company_OrderQuoteFilterDTO.SubTotal;
            OrderQuoteFilter.Total = Company_OrderQuoteFilterDTO.Total;
            OrderQuoteFilter.TotalTaxAmount = Company_OrderQuoteFilterDTO.TotalTaxAmount;
            OrderQuoteFilter.GeneralDiscountPercentage = Company_OrderQuoteFilterDTO.GeneralDiscountPercentage;
            OrderQuoteFilter.GeneralDiscountAmount = Company_OrderQuoteFilterDTO.GeneralDiscountAmount;
            OrderQuoteFilter.CreatedAt = Company_OrderQuoteFilterDTO.CreatedAt;
            return OrderQuoteFilter;
        }
        #endregion

        #region Order
        [Route(CompanyRoute.CountCustomerSalesOrder), HttpPost]
        public async Task<ActionResult<int>> CountCustomerSalesOrder([FromBody] Company_CustomerSalesOrderFilterDTO Company_CustomerSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerSalesOrderFilter CustomerSalesOrderFilter = ConvertFilterCustomerSalesOrder(Company_CustomerSalesOrderFilterDTO);
            CustomerSalesOrderFilter = await CustomerSalesOrderService.ToFilter(CustomerSalesOrderFilter);
            int count = await CustomerSalesOrderService.Count(CustomerSalesOrderFilter);
            return count;
        }

        [Route(CompanyRoute.ListCustomerSalesOrder), HttpPost]
        public async Task<ActionResult<List<Company_CustomerSalesOrderDTO>>> ListCustomerSalesOrder([FromBody] Company_CustomerSalesOrderFilterDTO Company_CustomerSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerSalesOrderFilter CustomerSalesOrderFilter = ConvertFilterCustomerSalesOrder(Company_CustomerSalesOrderFilterDTO);
            CustomerSalesOrderFilter = await CustomerSalesOrderService.ToFilter(CustomerSalesOrderFilter);
            List<CustomerSalesOrder> CustomerSalesOrders = await CustomerSalesOrderService.List(CustomerSalesOrderFilter);
            List<Company_CustomerSalesOrderDTO> Company_CustomerSalesOrderDTOs = CustomerSalesOrders
                .Select(c => new Company_CustomerSalesOrderDTO(c)).ToList();
            return Company_CustomerSalesOrderDTOs;
        }

        [Route(CompanyRoute.GetCustomerSalesOrder), HttpPost]
        public async Task<ActionResult<Company_CustomerSalesOrderDTO>> GetCustomerSalesOrder([FromBody] Company_CustomerSalesOrderDTO Company_CustomerSalesOrderDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerSalesOrder CustomerSalesOrder = await CustomerSalesOrderService.Get(Company_CustomerSalesOrderDTO.Id);
            return new Company_CustomerSalesOrderDTO(CustomerSalesOrder);
        }

        private CustomerSalesOrderFilter ConvertFilterCustomerSalesOrder(Company_CustomerSalesOrderFilterDTO Company_CustomerSalesOrderFilterDTO)
        {
            CustomerSalesOrderFilter CustomerSalesOrderFilter = new CustomerSalesOrderFilter();
            CustomerSalesOrderFilter.Selects = CustomerSalesOrderSelect.ALL;
            CustomerSalesOrderFilter.Skip = Company_CustomerSalesOrderFilterDTO.Skip;
            CustomerSalesOrderFilter.Take = Company_CustomerSalesOrderFilterDTO.Take;
            CustomerSalesOrderFilter.OrderBy = Company_CustomerSalesOrderFilterDTO.OrderBy;
            CustomerSalesOrderFilter.OrderType = Company_CustomerSalesOrderFilterDTO.OrderType;

            CustomerSalesOrderFilter.Id = Company_CustomerSalesOrderFilterDTO.Id;
            CustomerSalesOrderFilter.Code = Company_CustomerSalesOrderFilterDTO.Code;
            CustomerSalesOrderFilter.CustomerTypeId = Company_CustomerSalesOrderFilterDTO.CustomerTypeId;
            CustomerSalesOrderFilter.CustomerId = Company_CustomerSalesOrderFilterDTO.CustomerId;
            CustomerSalesOrderFilter.OpportunityId = Company_CustomerSalesOrderFilterDTO.OpportunityId;
            CustomerSalesOrderFilter.ContractId = Company_CustomerSalesOrderFilterDTO.ContractId;
            CustomerSalesOrderFilter.OrderPaymentStatusId = Company_CustomerSalesOrderFilterDTO.OrderPaymentStatusId;
            CustomerSalesOrderFilter.RequestStateId = Company_CustomerSalesOrderFilterDTO.RequestStateId;
            CustomerSalesOrderFilter.EditedPriceStatusId = Company_CustomerSalesOrderFilterDTO.EditedPriceStatusId;
            CustomerSalesOrderFilter.ShippingName = Company_CustomerSalesOrderFilterDTO.ShippingName;
            CustomerSalesOrderFilter.OrderDate = Company_CustomerSalesOrderFilterDTO.OrderDate;
            CustomerSalesOrderFilter.DeliveryDate = Company_CustomerSalesOrderFilterDTO.DeliveryDate;
            CustomerSalesOrderFilter.SalesEmployeeId = Company_CustomerSalesOrderFilterDTO.SalesEmployeeId;
            CustomerSalesOrderFilter.Note = Company_CustomerSalesOrderFilterDTO.Note;
            CustomerSalesOrderFilter.InvoiceAddress = Company_CustomerSalesOrderFilterDTO.InvoiceAddress;
            CustomerSalesOrderFilter.InvoiceNationId = Company_CustomerSalesOrderFilterDTO.InvoiceNationId;
            CustomerSalesOrderFilter.InvoiceProvinceId = Company_CustomerSalesOrderFilterDTO.InvoiceProvinceId;
            CustomerSalesOrderFilter.InvoiceDistrictId = Company_CustomerSalesOrderFilterDTO.InvoiceDistrictId;
            CustomerSalesOrderFilter.InvoiceWardId = Company_CustomerSalesOrderFilterDTO.InvoiceWardId;
            CustomerSalesOrderFilter.InvoiceZIPCode = Company_CustomerSalesOrderFilterDTO.InvoiceZIPCode;
            CustomerSalesOrderFilter.DeliveryAddress = Company_CustomerSalesOrderFilterDTO.DeliveryAddress;
            CustomerSalesOrderFilter.DeliveryNationId = Company_CustomerSalesOrderFilterDTO.DeliveryNationId;
            CustomerSalesOrderFilter.DeliveryProvinceId = Company_CustomerSalesOrderFilterDTO.DeliveryProvinceId;
            CustomerSalesOrderFilter.DeliveryDistrictId = Company_CustomerSalesOrderFilterDTO.DeliveryDistrictId;
            CustomerSalesOrderFilter.DeliveryWardId = Company_CustomerSalesOrderFilterDTO.DeliveryWardId;
            CustomerSalesOrderFilter.DeliveryZIPCode = Company_CustomerSalesOrderFilterDTO.DeliveryZIPCode;
            CustomerSalesOrderFilter.SubTotal = Company_CustomerSalesOrderFilterDTO.SubTotal;
            CustomerSalesOrderFilter.GeneralDiscountPercentage = Company_CustomerSalesOrderFilterDTO.GeneralDiscountPercentage;
            CustomerSalesOrderFilter.GeneralDiscountAmount = Company_CustomerSalesOrderFilterDTO.GeneralDiscountAmount;
            CustomerSalesOrderFilter.TotalTaxOther = Company_CustomerSalesOrderFilterDTO.TotalTaxOther;
            CustomerSalesOrderFilter.TotalTax = Company_CustomerSalesOrderFilterDTO.TotalTax;
            CustomerSalesOrderFilter.Total = Company_CustomerSalesOrderFilterDTO.Total;
            CustomerSalesOrderFilter.CreatorId = Company_CustomerSalesOrderFilterDTO.CreatorId;
            CustomerSalesOrderFilter.OrganizationId = Company_CustomerSalesOrderFilterDTO.OrganizationId;
            CustomerSalesOrderFilter.RowId = Company_CustomerSalesOrderFilterDTO.RowId;
            CustomerSalesOrderFilter.CompanyId = Company_CustomerSalesOrderFilterDTO.CompanyId;
            CustomerSalesOrderFilter.CreatedAt = Company_CustomerSalesOrderFilterDTO.CreatedAt;
            CustomerSalesOrderFilter.UpdatedAt = Company_CustomerSalesOrderFilterDTO.UpdatedAt;
            return CustomerSalesOrderFilter;
        }

        [Route(CompanyRoute.CountDirectSalesOrder), HttpPost]
        public async Task<ActionResult<int>> CountDirectSalesOrder([FromBody] Company_DirectSalesOrderFilterDTO Company_DirectSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DirectSalesOrderFilter DirectSalesOrderFilter = ConvertFilterDirectSalesOrder(Company_DirectSalesOrderFilterDTO);
            DirectSalesOrderFilter = await DirectSalesOrderService.ToFilter(DirectSalesOrderFilter);
            int count = await DirectSalesOrderService.Count(DirectSalesOrderFilter);
            return count;
        }

        [Route(CompanyRoute.ListDirectSalesOrder), HttpPost]
        public async Task<ActionResult<List<Company_DirectSalesOrderDTO>>> ListDirectSalesOrder([FromBody] Company_DirectSalesOrderFilterDTO Company_DirectSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DirectSalesOrderFilter DirectSalesOrderFilter = ConvertFilterDirectSalesOrder(Company_DirectSalesOrderFilterDTO);
            DirectSalesOrderFilter = await DirectSalesOrderService.ToFilter(DirectSalesOrderFilter);
            List<DirectSalesOrder> DirectSalesOrders = await DirectSalesOrderService.List(DirectSalesOrderFilter);
            List<Company_DirectSalesOrderDTO> Company_DirectSalesOrderDTOs = DirectSalesOrders
                .Select(c => new Company_DirectSalesOrderDTO(c)).ToList();
            return Company_DirectSalesOrderDTOs;
        }

        [Route(CompanyRoute.GetDirectSalesOrder), HttpPost]
        public async Task<ActionResult<Company_DirectSalesOrderDTO>> GetDirectSalesOrder([FromBody] Company_DirectSalesOrderDTO Company_DirectSalesOrderDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DirectSalesOrder DirectSalesOrder = await DirectSalesOrderService.Get(Company_DirectSalesOrderDTO.Id);
            List<TaxType> TaxTypes = await TaxTypeService.List(new TaxTypeFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = TaxTypeSelect.ALL
            });
            Company_DirectSalesOrderDTO = new Company_DirectSalesOrderDTO(DirectSalesOrder);
            foreach (var DirectSalesOrderContent in Company_DirectSalesOrderDTO.DirectSalesOrderContents)
            {
                TaxType TaxType = TaxTypes.Where(x => x.Percentage == DirectSalesOrderContent.TaxPercentage).FirstOrDefault();
                DirectSalesOrderContent.TaxType = new Company_TaxTypeDTO(TaxType);
            }
            return Company_DirectSalesOrderDTO;
        }

        private DirectSalesOrderFilter ConvertFilterDirectSalesOrder(Company_DirectSalesOrderFilterDTO Company_DirectSalesOrderFilterDTO)
        {
            DirectSalesOrderFilter DirectSalesOrderFilter = new DirectSalesOrderFilter();
            DirectSalesOrderFilter.Selects = DirectSalesOrderSelect.ALL;
            DirectSalesOrderFilter.Skip = Company_DirectSalesOrderFilterDTO.Skip;
            DirectSalesOrderFilter.Take = Company_DirectSalesOrderFilterDTO.Take;
            DirectSalesOrderFilter.OrderBy = Company_DirectSalesOrderFilterDTO.OrderBy;
            DirectSalesOrderFilter.OrderType = Company_DirectSalesOrderFilterDTO.OrderType;

            DirectSalesOrderFilter.Id = Company_DirectSalesOrderFilterDTO.Id;
            DirectSalesOrderFilter.OrganizationId = Company_DirectSalesOrderFilterDTO.OrganizationId;
            DirectSalesOrderFilter.Code = Company_DirectSalesOrderFilterDTO.Code;
            DirectSalesOrderFilter.BuyerStoreId = Company_DirectSalesOrderFilterDTO.BuyerStoreId;
            DirectSalesOrderFilter.PhoneNumber = Company_DirectSalesOrderFilterDTO.PhoneNumber;
            DirectSalesOrderFilter.StoreAddress = Company_DirectSalesOrderFilterDTO.StoreAddress;
            DirectSalesOrderFilter.DeliveryAddress = Company_DirectSalesOrderFilterDTO.DeliveryAddress;
            DirectSalesOrderFilter.AppUserId = Company_DirectSalesOrderFilterDTO.AppUserId;
            DirectSalesOrderFilter.OrderDate = Company_DirectSalesOrderFilterDTO.OrderDate;
            DirectSalesOrderFilter.DeliveryDate = Company_DirectSalesOrderFilterDTO.DeliveryDate;
            DirectSalesOrderFilter.RequestStateId = Company_DirectSalesOrderFilterDTO.RequestStateId;
            DirectSalesOrderFilter.EditedPriceStatusId = Company_DirectSalesOrderFilterDTO.EditedPriceStatusId;
            DirectSalesOrderFilter.Note = Company_DirectSalesOrderFilterDTO.Note;
            DirectSalesOrderFilter.SubTotal = Company_DirectSalesOrderFilterDTO.SubTotal;
            DirectSalesOrderFilter.GeneralDiscountPercentage = Company_DirectSalesOrderFilterDTO.GeneralDiscountPercentage;
            DirectSalesOrderFilter.GeneralDiscountAmount = Company_DirectSalesOrderFilterDTO.GeneralDiscountAmount;
            DirectSalesOrderFilter.TotalTaxAmount = Company_DirectSalesOrderFilterDTO.TotalTaxAmount;
            DirectSalesOrderFilter.Total = Company_DirectSalesOrderFilterDTO.Total;
            DirectSalesOrderFilter.StoreStatusId = Company_DirectSalesOrderFilterDTO.StoreStatusId;
            return DirectSalesOrderFilter;
        }
        #endregion

        #region opportunity
        [Route(CompanyRoute.ListOpportunity), HttpPost]
        public async Task<ActionResult<List<Company_OpportunityDTO>>> ListOpportunity([FromBody] Company_OpportunityFilterDTO Company_OpportunityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityFilter OpportunityFilter = ConvertFilterOpportunity(Company_OpportunityFilterDTO);
            OpportunityFilter = await OpportunityService.ToFilter(OpportunityFilter);
            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            List<Company_OpportunityDTO> Company_OpportunityDTOs = Opportunities
                .Select(c => new Company_OpportunityDTO(c)).ToList();
            return Company_OpportunityDTOs;
        }

        [Route(CompanyRoute.CountOpportunity), HttpPost]
        public async Task<ActionResult<int>> CountOpportunity([FromBody] Company_OpportunityFilterDTO Company_OpportunityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityFilter OpportunityFilter = ConvertFilterOpportunity(Company_OpportunityFilterDTO);
            OpportunityFilter = await OpportunityService.ToFilter(OpportunityFilter);
            int count = await OpportunityService.Count(OpportunityFilter);
            return count;
        }

        [Route(CompanyRoute.GetOpportunity), HttpPost]
        public async Task<ActionResult<Company_OpportunityDTO>> GetOpportunity([FromBody] Company_OpportunityDTO Company_OpportunityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Opportunity Opportunity = await OpportunityService.Get(Company_OpportunityDTO.Id);
            return new Company_OpportunityDTO(Opportunity);
        }
        private OpportunityFilter ConvertFilterOpportunity(Company_OpportunityFilterDTO Company_OpportunityFilterDTO)
        {
            OpportunityFilter OpportunityFilter = new OpportunityFilter();
            OpportunityFilter.Selects = OpportunitySelect.ALL;
            OpportunityFilter.Skip = Company_OpportunityFilterDTO.Skip;
            OpportunityFilter.Take = Company_OpportunityFilterDTO.Take;
            OpportunityFilter.OrderBy = Company_OpportunityFilterDTO.OrderBy;
            OpportunityFilter.OrderType = Company_OpportunityFilterDTO.OrderType;

            OpportunityFilter.Id = Company_OpportunityFilterDTO.Id;
            OpportunityFilter.Name = Company_OpportunityFilterDTO.Name;
            OpportunityFilter.CompanyId = Company_OpportunityFilterDTO.CompanyId;
            OpportunityFilter.CustomerLeadId = Company_OpportunityFilterDTO.CustomerLeadId;
            OpportunityFilter.ClosingDate = Company_OpportunityFilterDTO.ClosingDate;
            OpportunityFilter.SaleStageId = Company_OpportunityFilterDTO.SaleStageId;
            OpportunityFilter.ProbabilityId = Company_OpportunityFilterDTO.ProbabilityId;
            OpportunityFilter.PotentialResultId = Company_OpportunityFilterDTO.PotentialResultId;
            OpportunityFilter.LeadSourceId = Company_OpportunityFilterDTO.LeadSourceId;
            OpportunityFilter.AppUserId = Company_OpportunityFilterDTO.AppUserId;
            OpportunityFilter.CurrencyId = Company_OpportunityFilterDTO.CurrencyId;
            OpportunityFilter.Amount = Company_OpportunityFilterDTO.Amount;
            OpportunityFilter.ForecastAmount = Company_OpportunityFilterDTO.ForecastAmount;
            OpportunityFilter.Description = Company_OpportunityFilterDTO.Description;
            OpportunityFilter.OpportunityResultTypeId = Company_OpportunityFilterDTO.OpportunityResultTypeId;
            OpportunityFilter.CreatorId = Company_OpportunityFilterDTO.CreatorId;
            OpportunityFilter.CreatedAt = Company_OpportunityFilterDTO.CreatedAt;
            OpportunityFilter.UpdatedAt = Company_OpportunityFilterDTO.UpdatedAt;
            return OpportunityFilter;
        }
        #endregion

        #region Email
        [Route(CompanyRoute.GetCompanyEmail), HttpPost]
        public async Task<ActionResult<Company_CompanyEmailDTO>> GetCompanyEmail([FromBody] Company_CompanyEmailDTO Company_CompanyEmailDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyEmail CompanyEmail = await CompanyEmailService.Get(Company_CompanyEmailDTO.Id);
            return new Company_CompanyEmailDTO(CompanyEmail);
        }

        [Route(CompanyRoute.CountCompanyEmail), HttpPost]
        public async Task<ActionResult<long>> CountCompanyEmail([FromBody] Company_CompanyEmailFilterDTO Company_CompanyEmailFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            CompanyEmailFilter CompanyEmailFilter = new CompanyEmailFilter();
            CompanyEmailFilter.Id = Company_CompanyEmailFilterDTO.Id;
            CompanyEmailFilter.Title = Company_CompanyEmailFilterDTO.Title;
            CompanyEmailFilter.Content = Company_CompanyEmailFilterDTO.Content;
            CompanyEmailFilter.CreatorId = Company_CompanyEmailFilterDTO.CreatorId;
            CompanyEmailFilter.CreatedAt = Company_CompanyEmailFilterDTO.CreatedAt;
            CompanyEmailFilter.CompanyId = Company_CompanyEmailFilterDTO.CompanyId;
            CompanyEmailFilter.EmailStatusId = Company_CompanyEmailFilterDTO.EmailStatusId;
            CompanyEmailFilter.Reciepient = Company_CompanyEmailFilterDTO.Reciepient;

            return await CompanyEmailService.Count(CompanyEmailFilter);
        }

        [Route(CompanyRoute.ListCompanyEmail), HttpPost]
        public async Task<ActionResult<List<Company_CompanyEmailDTO>>> ListCompanyEmail([FromBody] Company_CompanyEmailFilterDTO Company_CompanyEmailFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            CompanyEmailFilter CompanyEmailFilter = new CompanyEmailFilter();
            CompanyEmailFilter.Skip = Company_CompanyEmailFilterDTO.Skip;
            CompanyEmailFilter.Take = Company_CompanyEmailFilterDTO.Take;
            CompanyEmailFilter.OrderBy = CompanyEmailOrder.Id;
            CompanyEmailFilter.OrderType = OrderType.ASC;
            CompanyEmailFilter.Selects = CompanyEmailSelect.ALL;
            CompanyEmailFilter.Id = Company_CompanyEmailFilterDTO.Id;
            CompanyEmailFilter.Title = Company_CompanyEmailFilterDTO.Title;
            CompanyEmailFilter.Content = Company_CompanyEmailFilterDTO.Content;
            CompanyEmailFilter.CreatorId = Company_CompanyEmailFilterDTO.CreatorId;
            CompanyEmailFilter.CreatedAt = Company_CompanyEmailFilterDTO.CreatedAt;
            CompanyEmailFilter.CompanyId = Company_CompanyEmailFilterDTO.CompanyId;
            CompanyEmailFilter.EmailStatusId = Company_CompanyEmailFilterDTO.EmailStatusId;
            CompanyEmailFilter.Reciepient = Company_CompanyEmailFilterDTO.Reciepient;

            List<CompanyEmail> CompanyEmails = await CompanyEmailService.List(CompanyEmailFilter);
            List<Company_CompanyEmailDTO> Company_CompanyEmailDTOs = CompanyEmails
                .Select(x => new Company_CompanyEmailDTO(x)).ToList();
            return Company_CompanyEmailDTOs;
        }
        #endregion
    }
}