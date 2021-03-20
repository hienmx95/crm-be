using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Common;
using CRM.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using OfficeOpenXml;
using CRM.Entities;
using CRM.Services.MCustomer;
using CRM.Services.MBusinessType;
using CRM.Services.MCompany;
using CRM.Services.MAppUser;
using CRM.Services.MCustomerResource;
using CRM.Services.MCustomerType;
using CRM.Services.MDistrict;
using CRM.Services.MNation;
using CRM.Services.MProfession;
using CRM.Services.MProvince;
using CRM.Services.MSex;
using CRM.Services.MStatus;
using CRM.Services.MWard;
using CRM.Services.MCustomerEmail;
using CRM.Services.MEmailType;
using CRM.Services.MCustomerFeedback;
using CRM.Services.MCustomerFeedbackType;
using CRM.Services.MCustomerPhone;
using CRM.Services.MPhoneType;
using CRM.Services.MCustomerPointHistory;
using CRM.Services.MRepairTicket;
using CRM.Services.MStore;
using CRM.Enums;

namespace CRM.Rpc.customer
{
    public partial class CustomerController : RpcController
    {
        [Route(CustomerRoute.CountContract), HttpPost]
        public async Task<ActionResult<int>> CountContract([FromBody] Customer_ContractFilterDTO Customer_ContractFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContractFilter ContractFilter = ConvertContractFilterEntity(Customer_ContractFilterDTO);
            ContractFilter = await ContractService.ToFilter(ContractFilter);
            int count = await ContractService.Count(ContractFilter);
            return count;
        }

        [Route(CustomerRoute.ListContract), HttpPost]
        public async Task<ActionResult<List<Customer_ContractDTO>>> ListContract([FromBody] Customer_ContractFilterDTO Customer_ContractFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContractFilter ContractFilter = ConvertContractFilterEntity(Customer_ContractFilterDTO);
            ContractFilter = await ContractService.ToFilter(ContractFilter);
            List<Contract> Contracts = await ContractService.List(ContractFilter);
            List<Customer_ContractDTO> Customer_ContractDTOs = Contracts
                .Select(c => new Customer_ContractDTO(c)).ToList();
            return Customer_ContractDTOs;
        }

        [Route(CustomerRoute.GetContract), HttpPost]
        public async Task<ActionResult<Customer_ContractDTO>> GetContract([FromBody] Customer_ContractDTO Customer_ContractDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Contract Contract = await ContractService.Get(Customer_ContractDTO.Id);
            return new Customer_ContractDTO(Contract);
        }

        private ContractFilter ConvertContractFilterEntity(Customer_ContractFilterDTO Customer_ContractFilterDTO)
        {
            ContractFilter ContractFilter = new ContractFilter();
            ContractFilter.Selects = ContractSelect.ALL;
            ContractFilter.Skip = Customer_ContractFilterDTO.Skip;
            ContractFilter.Take = Customer_ContractFilterDTO.Take;
            ContractFilter.OrderBy = Customer_ContractFilterDTO.OrderBy;
            ContractFilter.OrderType = Customer_ContractFilterDTO.OrderType;

            ContractFilter.Id = Customer_ContractFilterDTO.Id;
            ContractFilter.Code = Customer_ContractFilterDTO.Code;
            ContractFilter.Name = Customer_ContractFilterDTO.Name;
            ContractFilter.CompanyId = Customer_ContractFilterDTO.CompanyId;
            ContractFilter.OpportunityId = Customer_ContractFilterDTO.OpportunityId;
            ContractFilter.CustomerId = Customer_ContractFilterDTO.CustomerId;
            ContractFilter.ContractTypeId = Customer_ContractFilterDTO.ContractTypeId;
            ContractFilter.TotalValue = Customer_ContractFilterDTO.TotalValue;
            ContractFilter.CurrencyId = Customer_ContractFilterDTO.CurrencyId;
            ContractFilter.ValidityDate = Customer_ContractFilterDTO.ValidityDate;
            ContractFilter.ExpirationDate = Customer_ContractFilterDTO.ExpirationDate;
            ContractFilter.AppUserId = Customer_ContractFilterDTO.AppUserId;
            ContractFilter.DeliveryUnit = Customer_ContractFilterDTO.DeliveryUnit;
            ContractFilter.ContractStatusId = Customer_ContractFilterDTO.ContractStatusId;
            ContractFilter.PaymentStatusId = Customer_ContractFilterDTO.PaymentStatusId;
            ContractFilter.InvoiceAddress = Customer_ContractFilterDTO.InvoiceAddress;
            ContractFilter.InvoiceNationId = Customer_ContractFilterDTO.InvoiceNationId;
            ContractFilter.InvoiceProvinceId = Customer_ContractFilterDTO.InvoiceProvinceId;
            ContractFilter.InvoiceDistrictId = Customer_ContractFilterDTO.InvoiceDistrictId;
            ContractFilter.InvoiceZipCode = Customer_ContractFilterDTO.InvoiceZipCode;
            ContractFilter.ReceiveAddress = Customer_ContractFilterDTO.ReceiveAddress;
            ContractFilter.ReceiveNationId = Customer_ContractFilterDTO.ReceiveNationId;
            ContractFilter.ReceiveProvinceId = Customer_ContractFilterDTO.ReceiveProvinceId;
            ContractFilter.ReceiveDistrictId = Customer_ContractFilterDTO.ReceiveDistrictId;
            ContractFilter.ReceiveZipCode = Customer_ContractFilterDTO.ReceiveZipCode;
            ContractFilter.SubTotal = Customer_ContractFilterDTO.SubTotal;
            ContractFilter.GeneralDiscountPercentage = Customer_ContractFilterDTO.GeneralDiscountPercentage;
            ContractFilter.GeneralDiscountAmount = Customer_ContractFilterDTO.GeneralDiscountAmount;
            ContractFilter.TotalTaxAmountOther = Customer_ContractFilterDTO.TotalTaxAmountOther;
            ContractFilter.TotalTaxAmount = Customer_ContractFilterDTO.TotalTaxAmount;
            ContractFilter.Total = Customer_ContractFilterDTO.Total;
            ContractFilter.TermAndCondition = Customer_ContractFilterDTO.TermAndCondition;
            ContractFilter.CreatorId = Customer_ContractFilterDTO.CreatorId;
            ContractFilter.OrganizationId = Customer_ContractFilterDTO.OrganizationId;
            ContractFilter.CreatedAt = Customer_ContractFilterDTO.CreatedAt;
            ContractFilter.UpdatedAt = Customer_ContractFilterDTO.UpdatedAt;
            return ContractFilter;
        }

        #region Order
        [Route(CustomerRoute.CountCustomerSalesOrder), HttpPost]
        public async Task<ActionResult<int>> CountCustomerSalesOrder([FromBody] Customer_CustomerSalesOrderFilterDTO Customer_CustomerSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerSalesOrderFilter CustomerSalesOrderFilter = ConvertFilterCustomerSalesOrder(Customer_CustomerSalesOrderFilterDTO);
            CustomerSalesOrderFilter = await CustomerSalesOrderService.ToFilter(CustomerSalesOrderFilter);
            int count = await CustomerSalesOrderService.Count(CustomerSalesOrderFilter);
            return count;
        }

        [Route(CustomerRoute.ListCustomerSalesOrder), HttpPost]
        public async Task<ActionResult<List<Customer_CustomerSalesOrderDTO>>> ListCustomerSalesOrder([FromBody] Customer_CustomerSalesOrderFilterDTO Customer_CustomerSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerSalesOrderFilter CustomerSalesOrderFilter = ConvertFilterCustomerSalesOrder(Customer_CustomerSalesOrderFilterDTO);
            CustomerSalesOrderFilter = await CustomerSalesOrderService.ToFilter(CustomerSalesOrderFilter);
            List<CustomerSalesOrder> CustomerSalesOrders = await CustomerSalesOrderService.List(CustomerSalesOrderFilter);
            List<Customer_CustomerSalesOrderDTO> Customer_CustomerSalesOrderDTOs = CustomerSalesOrders
                .Select(c => new Customer_CustomerSalesOrderDTO(c)).ToList();
            return Customer_CustomerSalesOrderDTOs;
        }

        [Route(CustomerRoute.GetCustomerSalesOrder), HttpPost]
        public async Task<ActionResult<Customer_CustomerSalesOrderDTO>> GetCustomerSalesOrder([FromBody] Customer_CustomerSalesOrderDTO Customer_CustomerSalesOrderDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerSalesOrder CustomerSalesOrder = await CustomerSalesOrderService.Get(Customer_CustomerSalesOrderDTO.Id);
            return new Customer_CustomerSalesOrderDTO(CustomerSalesOrder);
        }

        private CustomerSalesOrderFilter ConvertFilterCustomerSalesOrder(Customer_CustomerSalesOrderFilterDTO Customer_CustomerSalesOrderFilterDTO)
        {
            CustomerSalesOrderFilter CustomerSalesOrderFilter = new CustomerSalesOrderFilter();
            CustomerSalesOrderFilter.Selects = CustomerSalesOrderSelect.ALL;
            CustomerSalesOrderFilter.Skip = Customer_CustomerSalesOrderFilterDTO.Skip;
            CustomerSalesOrderFilter.Take = Customer_CustomerSalesOrderFilterDTO.Take;
            CustomerSalesOrderFilter.OrderBy = Customer_CustomerSalesOrderFilterDTO.OrderBy;
            CustomerSalesOrderFilter.OrderType = Customer_CustomerSalesOrderFilterDTO.OrderType;

            CustomerSalesOrderFilter.Id = Customer_CustomerSalesOrderFilterDTO.Id;
            CustomerSalesOrderFilter.Code = Customer_CustomerSalesOrderFilterDTO.Code;
            CustomerSalesOrderFilter.CustomerTypeId = Customer_CustomerSalesOrderFilterDTO.CustomerTypeId;
            CustomerSalesOrderFilter.CustomerId = Customer_CustomerSalesOrderFilterDTO.CustomerId;
            CustomerSalesOrderFilter.OpportunityId = Customer_CustomerSalesOrderFilterDTO.OpportunityId;
            CustomerSalesOrderFilter.ContractId = Customer_CustomerSalesOrderFilterDTO.ContractId;
            CustomerSalesOrderFilter.OrderPaymentStatusId = Customer_CustomerSalesOrderFilterDTO.OrderPaymentStatusId;
            CustomerSalesOrderFilter.RequestStateId = Customer_CustomerSalesOrderFilterDTO.RequestStateId;
            CustomerSalesOrderFilter.EditedPriceStatusId = Customer_CustomerSalesOrderFilterDTO.EditedPriceStatusId;
            CustomerSalesOrderFilter.ShippingName = Customer_CustomerSalesOrderFilterDTO.ShippingName;
            CustomerSalesOrderFilter.OrderDate = Customer_CustomerSalesOrderFilterDTO.OrderDate;
            CustomerSalesOrderFilter.DeliveryDate = Customer_CustomerSalesOrderFilterDTO.DeliveryDate;
            CustomerSalesOrderFilter.SalesEmployeeId = Customer_CustomerSalesOrderFilterDTO.SalesEmployeeId;
            CustomerSalesOrderFilter.Note = Customer_CustomerSalesOrderFilterDTO.Note;
            CustomerSalesOrderFilter.InvoiceAddress = Customer_CustomerSalesOrderFilterDTO.InvoiceAddress;
            CustomerSalesOrderFilter.InvoiceNationId = Customer_CustomerSalesOrderFilterDTO.InvoiceNationId;
            CustomerSalesOrderFilter.InvoiceProvinceId = Customer_CustomerSalesOrderFilterDTO.InvoiceProvinceId;
            CustomerSalesOrderFilter.InvoiceDistrictId = Customer_CustomerSalesOrderFilterDTO.InvoiceDistrictId;
            CustomerSalesOrderFilter.InvoiceWardId = Customer_CustomerSalesOrderFilterDTO.InvoiceWardId;
            CustomerSalesOrderFilter.InvoiceZIPCode = Customer_CustomerSalesOrderFilterDTO.InvoiceZIPCode;
            CustomerSalesOrderFilter.DeliveryAddress = Customer_CustomerSalesOrderFilterDTO.DeliveryAddress;
            CustomerSalesOrderFilter.DeliveryNationId = Customer_CustomerSalesOrderFilterDTO.DeliveryNationId;
            CustomerSalesOrderFilter.DeliveryProvinceId = Customer_CustomerSalesOrderFilterDTO.DeliveryProvinceId;
            CustomerSalesOrderFilter.DeliveryDistrictId = Customer_CustomerSalesOrderFilterDTO.DeliveryDistrictId;
            CustomerSalesOrderFilter.DeliveryWardId = Customer_CustomerSalesOrderFilterDTO.DeliveryWardId;
            CustomerSalesOrderFilter.DeliveryZIPCode = Customer_CustomerSalesOrderFilterDTO.DeliveryZIPCode;
            CustomerSalesOrderFilter.SubTotal = Customer_CustomerSalesOrderFilterDTO.SubTotal;
            CustomerSalesOrderFilter.GeneralDiscountPercentage = Customer_CustomerSalesOrderFilterDTO.GeneralDiscountPercentage;
            CustomerSalesOrderFilter.GeneralDiscountAmount = Customer_CustomerSalesOrderFilterDTO.GeneralDiscountAmount;
            CustomerSalesOrderFilter.TotalTaxOther = Customer_CustomerSalesOrderFilterDTO.TotalTaxOther;
            CustomerSalesOrderFilter.TotalTax = Customer_CustomerSalesOrderFilterDTO.TotalTax;
            CustomerSalesOrderFilter.Total = Customer_CustomerSalesOrderFilterDTO.Total;
            CustomerSalesOrderFilter.CreatorId = Customer_CustomerSalesOrderFilterDTO.CreatorId;
            CustomerSalesOrderFilter.OrganizationId = Customer_CustomerSalesOrderFilterDTO.OrganizationId;
            CustomerSalesOrderFilter.RowId = Customer_CustomerSalesOrderFilterDTO.RowId;
            CustomerSalesOrderFilter.CreatedAt = Customer_CustomerSalesOrderFilterDTO.CreatedAt;
            CustomerSalesOrderFilter.UpdatedAt = Customer_CustomerSalesOrderFilterDTO.UpdatedAt;
            return CustomerSalesOrderFilter;
        }

        [Route(CustomerRoute.CountDirectSalesOrder), HttpPost]
        public async Task<ActionResult<int>> CountDirectSalesOrder([FromBody] Customer_DirectSalesOrderFilterDTO Customer_DirectSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DirectSalesOrderFilter DirectSalesOrderFilter = ConvertFilterDirectSalesOrder(Customer_DirectSalesOrderFilterDTO);
            DirectSalesOrderFilter = await DirectSalesOrderService.ToFilter(DirectSalesOrderFilter);
            int count = await DirectSalesOrderService.Count(DirectSalesOrderFilter);
            return count;
        }

        [Route(CustomerRoute.ListDirectSalesOrder), HttpPost]
        public async Task<ActionResult<List<Customer_DirectSalesOrderDTO>>> ListDirectSalesOrder([FromBody] Customer_DirectSalesOrderFilterDTO Customer_DirectSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DirectSalesOrderFilter DirectSalesOrderFilter = ConvertFilterDirectSalesOrder(Customer_DirectSalesOrderFilterDTO);
            DirectSalesOrderFilter = await DirectSalesOrderService.ToFilter(DirectSalesOrderFilter);
            List<DirectSalesOrder> DirectSalesOrders = await DirectSalesOrderService.List(DirectSalesOrderFilter);
            List<Customer_DirectSalesOrderDTO> Customer_DirectSalesOrderDTOs = DirectSalesOrders
                .Select(c => new Customer_DirectSalesOrderDTO(c)).ToList();
            return Customer_DirectSalesOrderDTOs;
        }

        [Route(CustomerRoute.GetDirectSalesOrder), HttpPost]
        public async Task<ActionResult<Customer_DirectSalesOrderDTO>> GetDirectSalesOrder([FromBody] Customer_DirectSalesOrderDTO Customer_DirectSalesOrderDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DirectSalesOrder DirectSalesOrder = await DirectSalesOrderService.Get(Customer_DirectSalesOrderDTO.Id);
            List<TaxType> TaxTypes = await TaxTypeService.List(new TaxTypeFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = TaxTypeSelect.ALL
            });
            Customer_DirectSalesOrderDTO = new Customer_DirectSalesOrderDTO(DirectSalesOrder);
            foreach (var DirectSalesOrderContent in Customer_DirectSalesOrderDTO.DirectSalesOrderContents)
            {
                TaxType TaxType = TaxTypes.Where(x => x.Percentage == DirectSalesOrderContent.TaxPercentage).FirstOrDefault();
                DirectSalesOrderContent.TaxType = new Customer_TaxTypeDTO(TaxType);
            }
            return Customer_DirectSalesOrderDTO;
        }

        private DirectSalesOrderFilter ConvertFilterDirectSalesOrder(Customer_DirectSalesOrderFilterDTO Customer_DirectSalesOrderFilterDTO)
        {
            DirectSalesOrderFilter DirectSalesOrderFilter = new DirectSalesOrderFilter();
            DirectSalesOrderFilter.Selects = DirectSalesOrderSelect.ALL;
            DirectSalesOrderFilter.Skip = Customer_DirectSalesOrderFilterDTO.Skip;
            DirectSalesOrderFilter.Take = Customer_DirectSalesOrderFilterDTO.Take;
            DirectSalesOrderFilter.OrderBy = Customer_DirectSalesOrderFilterDTO.OrderBy;
            DirectSalesOrderFilter.OrderType = Customer_DirectSalesOrderFilterDTO.OrderType;

            DirectSalesOrderFilter.Id = Customer_DirectSalesOrderFilterDTO.Id;
            DirectSalesOrderFilter.OrganizationId = Customer_DirectSalesOrderFilterDTO.OrganizationId;
            DirectSalesOrderFilter.Code = Customer_DirectSalesOrderFilterDTO.Code;
            DirectSalesOrderFilter.BuyerStoreId = Customer_DirectSalesOrderFilterDTO.BuyerStoreId;
            DirectSalesOrderFilter.PhoneNumber = Customer_DirectSalesOrderFilterDTO.PhoneNumber;
            DirectSalesOrderFilter.StoreAddress = Customer_DirectSalesOrderFilterDTO.StoreAddress;
            DirectSalesOrderFilter.DeliveryAddress = Customer_DirectSalesOrderFilterDTO.DeliveryAddress;
            DirectSalesOrderFilter.AppUserId = Customer_DirectSalesOrderFilterDTO.AppUserId;
            DirectSalesOrderFilter.OrderDate = Customer_DirectSalesOrderFilterDTO.OrderDate;
            DirectSalesOrderFilter.DeliveryDate = Customer_DirectSalesOrderFilterDTO.DeliveryDate;
            DirectSalesOrderFilter.RequestStateId = Customer_DirectSalesOrderFilterDTO.RequestStateId;
            DirectSalesOrderFilter.EditedPriceStatusId = Customer_DirectSalesOrderFilterDTO.EditedPriceStatusId;
            DirectSalesOrderFilter.Note = Customer_DirectSalesOrderFilterDTO.Note;
            DirectSalesOrderFilter.SubTotal = Customer_DirectSalesOrderFilterDTO.SubTotal;
            DirectSalesOrderFilter.GeneralDiscountPercentage = Customer_DirectSalesOrderFilterDTO.GeneralDiscountPercentage;
            DirectSalesOrderFilter.GeneralDiscountAmount = Customer_DirectSalesOrderFilterDTO.GeneralDiscountAmount;
            DirectSalesOrderFilter.TotalTaxAmount = Customer_DirectSalesOrderFilterDTO.TotalTaxAmount;
            DirectSalesOrderFilter.Total = Customer_DirectSalesOrderFilterDTO.Total;
            DirectSalesOrderFilter.StoreStatusId = Customer_DirectSalesOrderFilterDTO.StoreStatusId;
            DirectSalesOrderFilter.CustomerId = Customer_DirectSalesOrderFilterDTO.CustomerId;
            return DirectSalesOrderFilter;
        }
        #endregion

        [Route(CustomerRoute.CountRepairTicket), HttpPost]
        public async Task<ActionResult<int>> CountRepairTicket([FromBody] Customer_RepairTicketFilterDTO Customer_RepairTicketFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RepairTicketFilter RepairTicketFilter = ConvertFilterRepairTicket(Customer_RepairTicketFilterDTO);
            RepairTicketFilter = await RepairTicketService.ToFilter(RepairTicketFilter);
            int count = await RepairTicketService.Count(RepairTicketFilter);
            return count;
        }

        [Route(CustomerRoute.ListRepairTicket), HttpPost]
        public async Task<ActionResult<List<Customer_RepairTicketDTO>>> ListRepairTicket([FromBody] Customer_RepairTicketFilterDTO Customer_RepairTicketFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RepairTicketFilter RepairTicketFilter = ConvertFilterRepairTicket(Customer_RepairTicketFilterDTO);
            RepairTicketFilter = await RepairTicketService.ToFilter(RepairTicketFilter);
            List<RepairTicket> RepairTickets = await RepairTicketService.List(RepairTicketFilter);
            List<Customer_RepairTicketDTO> Customer_RepairTicketDTOs = RepairTickets
                .Select(c => new Customer_RepairTicketDTO(c)).ToList();
            return Customer_RepairTicketDTOs;
        }

        [Route(CustomerRoute.GetRepairTicket), HttpPost]
        public async Task<ActionResult<Customer_RepairTicketDTO>> GetRepairTicket([FromBody] Customer_RepairTicketDTO Customer_RepairTicketDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RepairTicket RepairTicket = await RepairTicketService.Get(Customer_RepairTicketDTO.Id);
            return new Customer_RepairTicketDTO(RepairTicket);
        }

        private RepairTicketFilter ConvertFilterRepairTicket(Customer_RepairTicketFilterDTO Customer_RepairTicketFilterDTO)
        {
            RepairTicketFilter RepairTicketFilter = new RepairTicketFilter();
            RepairTicketFilter.Selects = RepairTicketSelect.ALL;
            RepairTicketFilter.Skip = Customer_RepairTicketFilterDTO.Skip;
            RepairTicketFilter.Take = Customer_RepairTicketFilterDTO.Take;
            RepairTicketFilter.OrderBy = Customer_RepairTicketFilterDTO.OrderBy;
            RepairTicketFilter.OrderType = Customer_RepairTicketFilterDTO.OrderType;

            RepairTicketFilter.Id = Customer_RepairTicketFilterDTO.Id;
            RepairTicketFilter.Code = Customer_RepairTicketFilterDTO.Code;
            RepairTicketFilter.DeviceSerial = Customer_RepairTicketFilterDTO.DeviceSerial;
            RepairTicketFilter.OrderId = Customer_RepairTicketFilterDTO.OrderId;
            RepairTicketFilter.OrderCategoryId = Customer_RepairTicketFilterDTO.OrderCategoryId;
            RepairTicketFilter.RepairDueDate = Customer_RepairTicketFilterDTO.RepairDueDate;
            RepairTicketFilter.ItemId = Customer_RepairTicketFilterDTO.ItemId;
            RepairTicketFilter.RejectReason = Customer_RepairTicketFilterDTO.RejectReason;
            RepairTicketFilter.DeviceState = Customer_RepairTicketFilterDTO.DeviceState;
            RepairTicketFilter.RepairStatusId = Customer_RepairTicketFilterDTO.RepairStatusId;
            RepairTicketFilter.RepairAddess = Customer_RepairTicketFilterDTO.RepairAddess;
            RepairTicketFilter.ReceiveUser = Customer_RepairTicketFilterDTO.ReceiveUser;
            RepairTicketFilter.ReceiveDate = Customer_RepairTicketFilterDTO.ReceiveDate;
            RepairTicketFilter.RepairDate = Customer_RepairTicketFilterDTO.RepairDate;
            RepairTicketFilter.ReturnDate = Customer_RepairTicketFilterDTO.ReturnDate;
            RepairTicketFilter.RepairSolution = Customer_RepairTicketFilterDTO.RepairSolution;
            RepairTicketFilter.Note = Customer_RepairTicketFilterDTO.Note;
            RepairTicketFilter.RepairCost = Customer_RepairTicketFilterDTO.RepairCost;
            RepairTicketFilter.PaymentStatusId = Customer_RepairTicketFilterDTO.PaymentStatusId;
            RepairTicketFilter.CustomerId = Customer_RepairTicketFilterDTO.CustomerId;
            RepairTicketFilter.CreatorId = Customer_RepairTicketFilterDTO.CreatorId;
            RepairTicketFilter.CreatedAt = Customer_RepairTicketFilterDTO.CreatedAt;
            RepairTicketFilter.UpdatedAt = Customer_RepairTicketFilterDTO.UpdatedAt;
            return RepairTicketFilter;
        }

        [Route(CustomerRoute.CountStore), HttpPost]
        public async Task<ActionResult<long>> CountStore([FromBody] Customer_StoreFilterDTO Customer_StoreFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StoreFilter StoreFilter = new StoreFilter();
            StoreFilter.Id = Customer_StoreFilterDTO.Id;
            StoreFilter.Code = Customer_StoreFilterDTO.Code;
            StoreFilter.CodeDraft = Customer_StoreFilterDTO.CodeDraft;
            StoreFilter.Name = Customer_StoreFilterDTO.Name;
            StoreFilter.UnsignName = Customer_StoreFilterDTO.UnsignName;
            StoreFilter.ParentStoreId = Customer_StoreFilterDTO.ParentStoreId;
            StoreFilter.OrganizationId = Customer_StoreFilterDTO.OrganizationId;
            StoreFilter.StoreTypeId = Customer_StoreFilterDTO.StoreTypeId;
            StoreFilter.StoreGroupingId = Customer_StoreFilterDTO.StoreGroupingId;
            StoreFilter.Telephone = Customer_StoreFilterDTO.Telephone;
            StoreFilter.ProvinceId = Customer_StoreFilterDTO.ProvinceId;
            StoreFilter.DistrictId = Customer_StoreFilterDTO.DistrictId;
            StoreFilter.WardId = Customer_StoreFilterDTO.WardId;
            StoreFilter.Address = Customer_StoreFilterDTO.Address;
            StoreFilter.UnsignAddress = Customer_StoreFilterDTO.UnsignAddress;
            StoreFilter.DeliveryAddress = Customer_StoreFilterDTO.DeliveryAddress;
            StoreFilter.Latitude = Customer_StoreFilterDTO.Latitude;
            StoreFilter.Longitude = Customer_StoreFilterDTO.Longitude;
            StoreFilter.DeliveryLatitude = Customer_StoreFilterDTO.DeliveryLatitude;
            StoreFilter.DeliveryLongitude = Customer_StoreFilterDTO.DeliveryLongitude;
            StoreFilter.OwnerName = Customer_StoreFilterDTO.OwnerName;
            StoreFilter.OwnerPhone = Customer_StoreFilterDTO.OwnerPhone;
            StoreFilter.OwnerEmail = Customer_StoreFilterDTO.OwnerEmail;
            StoreFilter.TaxCode = Customer_StoreFilterDTO.TaxCode;
            StoreFilter.LegalEntity = Customer_StoreFilterDTO.LegalEntity;
            StoreFilter.AppUserId = Customer_StoreFilterDTO.AppUserId;
            StoreFilter.StatusId = Customer_StoreFilterDTO.StatusId;
            StoreFilter.StoreStatusId = Customer_StoreFilterDTO.StoreStatusId;
            StoreFilter.CustomerId = Customer_StoreFilterDTO.CustomerId;
            StoreFilter.isSelected = Customer_StoreFilterDTO.isSelected;

            return await StoreService.Count(StoreFilter);
        }

        [Route(CustomerRoute.ListStore), HttpPost]
        public async Task<ActionResult<List<Customer_StoreDTO>>> ListStore([FromBody] Customer_StoreFilterDTO Customer_StoreFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StoreFilter StoreFilter = new StoreFilter();
            StoreFilter.Skip = Customer_StoreFilterDTO.Skip;
            StoreFilter.Take = Customer_StoreFilterDTO.Take;
            StoreFilter.OrderBy = StoreOrder.Id;
            StoreFilter.OrderType = OrderType.ASC;
            StoreFilter.Selects = StoreSelect.ALL;
            StoreFilter.Id = Customer_StoreFilterDTO.Id;
            StoreFilter.Code = Customer_StoreFilterDTO.Code;
            StoreFilter.CodeDraft = Customer_StoreFilterDTO.CodeDraft;
            StoreFilter.Name = Customer_StoreFilterDTO.Name;
            StoreFilter.UnsignName = Customer_StoreFilterDTO.UnsignName;
            StoreFilter.ParentStoreId = Customer_StoreFilterDTO.ParentStoreId;
            StoreFilter.OrganizationId = Customer_StoreFilterDTO.OrganizationId;
            StoreFilter.StoreTypeId = Customer_StoreFilterDTO.StoreTypeId;
            StoreFilter.StoreGroupingId = Customer_StoreFilterDTO.StoreGroupingId;
            StoreFilter.Telephone = Customer_StoreFilterDTO.Telephone;
            StoreFilter.ProvinceId = Customer_StoreFilterDTO.ProvinceId;
            StoreFilter.DistrictId = Customer_StoreFilterDTO.DistrictId;
            StoreFilter.WardId = Customer_StoreFilterDTO.WardId;
            StoreFilter.Address = Customer_StoreFilterDTO.Address;
            StoreFilter.UnsignAddress = Customer_StoreFilterDTO.UnsignAddress;
            StoreFilter.DeliveryAddress = Customer_StoreFilterDTO.DeliveryAddress;
            StoreFilter.Latitude = Customer_StoreFilterDTO.Latitude;
            StoreFilter.Longitude = Customer_StoreFilterDTO.Longitude;
            StoreFilter.DeliveryLatitude = Customer_StoreFilterDTO.DeliveryLatitude;
            StoreFilter.DeliveryLongitude = Customer_StoreFilterDTO.DeliveryLongitude;
            StoreFilter.OwnerName = Customer_StoreFilterDTO.OwnerName;
            StoreFilter.OwnerPhone = Customer_StoreFilterDTO.OwnerPhone;
            StoreFilter.OwnerEmail = Customer_StoreFilterDTO.OwnerEmail;
            StoreFilter.TaxCode = Customer_StoreFilterDTO.TaxCode;
            StoreFilter.LegalEntity = Customer_StoreFilterDTO.LegalEntity;
            StoreFilter.AppUserId = Customer_StoreFilterDTO.AppUserId;
            StoreFilter.StatusId = Customer_StoreFilterDTO.StatusId;
            StoreFilter.StoreStatusId = Customer_StoreFilterDTO.StoreStatusId;
            StoreFilter.CustomerId = Customer_StoreFilterDTO.CustomerId;
            StoreFilter.isSelected = Customer_StoreFilterDTO.isSelected;

            List<Store> Stores = await StoreService.List(StoreFilter);
            List<Customer_StoreDTO> Customer_StoreDTOs = Stores
                .Select(x => new Customer_StoreDTO(x)).ToList();
            return Customer_StoreDTOs;
        }

        #region Email
        [Route(CustomerRoute.GetCustomerEmailHistory), HttpPost]
        public async Task<ActionResult<Customer_CustomerEmailHistoryDTO>> GetCustomerEmailHistory([FromBody] Customer_CustomerEmailHistoryDTO Customer_CustomerEmailHistoryDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerEmailHistory CustomerEmailHistory = await CustomerEmailHistoryService.Get(Customer_CustomerEmailHistoryDTO.Id);
            return new Customer_CustomerEmailHistoryDTO(CustomerEmailHistory);
        }

        [Route(CustomerRoute.CountCustomerEmailHistory), HttpPost]
        public async Task<ActionResult<long>> CountCustomerEmailHistory([FromBody] Customer_CustomerEmailHistoryFilterDTO Customer_CustomerEmailHistoryFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            CustomerEmailHistoryFilter CustomerEmailHistoryFilter = new CustomerEmailHistoryFilter();
            CustomerEmailHistoryFilter.Id = Customer_CustomerEmailHistoryFilterDTO.Id;
            CustomerEmailHistoryFilter.Title = Customer_CustomerEmailHistoryFilterDTO.Title;
            CustomerEmailHistoryFilter.Content = Customer_CustomerEmailHistoryFilterDTO.Content;
            CustomerEmailHistoryFilter.CreatorId = Customer_CustomerEmailHistoryFilterDTO.CreatorId;
            CustomerEmailHistoryFilter.CreatedAt = Customer_CustomerEmailHistoryFilterDTO.CreatedAt;
            CustomerEmailHistoryFilter.CustomerId = Customer_CustomerEmailHistoryFilterDTO.CustomerId;
            CustomerEmailHistoryFilter.EmailStatusId = Customer_CustomerEmailHistoryFilterDTO.EmailStatusId;
            CustomerEmailHistoryFilter.Reciepient = Customer_CustomerEmailHistoryFilterDTO.Reciepient;

            return await CustomerEmailHistoryService.Count(CustomerEmailHistoryFilter);
        }

        [Route(CustomerRoute.ListCustomerEmailHistory), HttpPost]
        public async Task<ActionResult<List<Customer_CustomerEmailHistoryDTO>>> ListCustomerEmailHistory([FromBody] Customer_CustomerEmailHistoryFilterDTO Customer_CustomerEmailHistoryFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            CustomerEmailHistoryFilter CustomerEmailHistoryFilter = new CustomerEmailHistoryFilter();
            CustomerEmailHistoryFilter.Skip = Customer_CustomerEmailHistoryFilterDTO.Skip;
            CustomerEmailHistoryFilter.Take = Customer_CustomerEmailHistoryFilterDTO.Take;
            CustomerEmailHistoryFilter.OrderBy = CustomerEmailHistoryOrder.Id;
            CustomerEmailHistoryFilter.OrderType = OrderType.ASC;
            CustomerEmailHistoryFilter.Selects = CustomerEmailHistorySelect.ALL;
            CustomerEmailHistoryFilter.Id = Customer_CustomerEmailHistoryFilterDTO.Id;
            CustomerEmailHistoryFilter.Title = Customer_CustomerEmailHistoryFilterDTO.Title;
            CustomerEmailHistoryFilter.Content = Customer_CustomerEmailHistoryFilterDTO.Content;
            CustomerEmailHistoryFilter.CreatorId = Customer_CustomerEmailHistoryFilterDTO.CreatorId;
            CustomerEmailHistoryFilter.CreatedAt = Customer_CustomerEmailHistoryFilterDTO.CreatedAt;
            CustomerEmailHistoryFilter.CustomerId = Customer_CustomerEmailHistoryFilterDTO.CustomerId;
            CustomerEmailHistoryFilter.EmailStatusId = Customer_CustomerEmailHistoryFilterDTO.EmailStatusId;
            CustomerEmailHistoryFilter.Reciepient = Customer_CustomerEmailHistoryFilterDTO.Reciepient;

            List<CustomerEmailHistory> CustomerEmailHistorys = await CustomerEmailHistoryService.List(CustomerEmailHistoryFilter);
            List<Customer_CustomerEmailHistoryDTO> Customer_CustomerEmailHistoryDTOs = CustomerEmailHistorys
                .Select(x => new Customer_CustomerEmailHistoryDTO(x)).ToList();
            return Customer_CustomerEmailHistoryDTOs;
        }
        #endregion

        [Route(CustomerRoute.CountCustomerPointHistory), HttpPost]
        public async Task<ActionResult<int>> CountCustomerPointHistory([FromBody] Customer_CustomerPointHistoryFilterDTO Customer_CustomerPointHistoryFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerPointHistoryFilter CustomerPointHistoryFilter = ConvertCustomerPointHistoryFilter(Customer_CustomerPointHistoryFilterDTO);
            CustomerPointHistoryFilter = await CustomerPointHistoryService.ToFilter(CustomerPointHistoryFilter);
            int count = await CustomerPointHistoryService.Count(CustomerPointHistoryFilter);
            return count;
        }

        [Route(CustomerRoute.ListCustomerPointHistory), HttpPost]
        public async Task<ActionResult<List<Customer_CustomerPointHistoryDTO>>> ListCustomerPointHistory([FromBody] Customer_CustomerPointHistoryFilterDTO Customer_CustomerPointHistoryFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerPointHistoryFilter CustomerPointHistoryFilter = ConvertCustomerPointHistoryFilter(Customer_CustomerPointHistoryFilterDTO);
            CustomerPointHistoryFilter = await CustomerPointHistoryService.ToFilter(CustomerPointHistoryFilter);
            List<CustomerPointHistory> CustomerPointHistories = await CustomerPointHistoryService.List(CustomerPointHistoryFilter);
            List<Customer_CustomerPointHistoryDTO> Customer_CustomerPointHistoryDTOs = CustomerPointHistories
                .Select(c => new Customer_CustomerPointHistoryDTO(c)).ToList();
            return Customer_CustomerPointHistoryDTOs;
        }

        [Route(CustomerRoute.GetCustomerPointHistory), HttpPost]
        public async Task<ActionResult<Customer_CustomerPointHistoryDTO>> GetCustomerPointHistory([FromBody] Customer_CustomerPointHistoryDTO Customer_CustomerPointHistoryDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerPointHistory CustomerPointHistory = await CustomerPointHistoryService.Get(Customer_CustomerPointHistoryDTO.Id);
            return new Customer_CustomerPointHistoryDTO(CustomerPointHistory);
        }

        private CustomerPointHistoryFilter ConvertCustomerPointHistoryFilter(Customer_CustomerPointHistoryFilterDTO Customer_CustomerPointHistoryFilterDTO)
        {
            CustomerPointHistoryFilter CustomerPointHistoryFilter = new CustomerPointHistoryFilter();
            CustomerPointHistoryFilter.Selects = CustomerPointHistorySelect.ALL;
            CustomerPointHistoryFilter.Skip = Customer_CustomerPointHistoryFilterDTO.Skip;
            CustomerPointHistoryFilter.Take = Customer_CustomerPointHistoryFilterDTO.Take;
            CustomerPointHistoryFilter.OrderBy = Customer_CustomerPointHistoryFilterDTO.OrderBy;
            CustomerPointHistoryFilter.OrderType = Customer_CustomerPointHistoryFilterDTO.OrderType;

            CustomerPointHistoryFilter.Id = Customer_CustomerPointHistoryFilterDTO.Id;
            CustomerPointHistoryFilter.CustomerId = Customer_CustomerPointHistoryFilterDTO.CustomerId;
            CustomerPointHistoryFilter.TotalPoint = Customer_CustomerPointHistoryFilterDTO.TotalPoint;
            CustomerPointHistoryFilter.CurrentPoint = Customer_CustomerPointHistoryFilterDTO.CurrentPoint;
            CustomerPointHistoryFilter.ChangePoint = Customer_CustomerPointHistoryFilterDTO.ChangePoint;
            CustomerPointHistoryFilter.Description = Customer_CustomerPointHistoryFilterDTO.Description;
            CustomerPointHistoryFilter.CreatedAt = Customer_CustomerPointHistoryFilterDTO.CreatedAt;
            CustomerPointHistoryFilter.UpdatedAt = Customer_CustomerPointHistoryFilterDTO.UpdatedAt;
            return CustomerPointHistoryFilter;
        }

        [Route(CustomerRoute.CountTicket), HttpPost]
        public async Task<ActionResult<int>> CountTicket([FromBody] Customer_TicketFilterDTO Customer_TicketFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketFilter TicketFilter = ConvertFilterTicket(Customer_TicketFilterDTO);
            TicketFilter = await TicketService.ToFilter(TicketFilter);
            int count = await TicketService.Count(TicketFilter);
            return count;
        }

        [Route(CustomerRoute.ListTicket), HttpPost]
        public async Task<ActionResult<List<Customer_TicketDTO>>> ListTicket([FromBody] Customer_TicketFilterDTO Customer_TicketFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketFilter TicketFilter = ConvertFilterTicket(Customer_TicketFilterDTO);
            TicketFilter = await TicketService.ToFilter(TicketFilter);
            List<Ticket> Tickets = await TicketService.List(TicketFilter);
            List<Customer_TicketDTO> Customer_TicketDTOs = Tickets
                .Select(c => new Customer_TicketDTO(c)).ToList();
            return Customer_TicketDTOs;
        }

        [Route(CustomerRoute.GetTicket), HttpPost]
        public async Task<ActionResult<Customer_TicketDTO>> GetTicket([FromBody] Customer_TicketDTO Customer_TicketDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Ticket Ticket = await TicketService.Get(Customer_TicketDTO.Id);
            return new Customer_TicketDTO(Ticket);
        }

        private TicketFilter ConvertFilterTicket(Customer_TicketFilterDTO Customer_TicketFilterDTO)
        {
            TicketFilter TicketFilter = new TicketFilter();
            TicketFilter.Selects = TicketSelect.ALL;
            TicketFilter.Skip = Customer_TicketFilterDTO.Skip;
            TicketFilter.Take = Customer_TicketFilterDTO.Take;
            TicketFilter.OrderBy = Customer_TicketFilterDTO.OrderBy;
            TicketFilter.OrderType = Customer_TicketFilterDTO.OrderType;
            TicketFilter.Id = Customer_TicketFilterDTO.Id;
            TicketFilter.Name = Customer_TicketFilterDTO.Name;
            TicketFilter.Phone = Customer_TicketFilterDTO.Phone;
            TicketFilter.CustomerId = Customer_TicketFilterDTO.CustomerId;
            TicketFilter.UserId = Customer_TicketFilterDTO.UserId;
            TicketFilter.CustomerTypeId = Customer_TicketFilterDTO.CustomerTypeId;
            TicketFilter.ProductId = Customer_TicketFilterDTO.ProductId;
            TicketFilter.ReceiveDate = Customer_TicketFilterDTO.ReceiveDate;
            TicketFilter.ProcessDate = Customer_TicketFilterDTO.ProcessDate;
            TicketFilter.FinishDate = Customer_TicketFilterDTO.FinishDate;
            TicketFilter.Subject = Customer_TicketFilterDTO.Subject;
            TicketFilter.Content = Customer_TicketFilterDTO.Content;
            TicketFilter.TicketIssueLevelId = Customer_TicketFilterDTO.TicketIssueLevelId;
            TicketFilter.TicketPriorityId = Customer_TicketFilterDTO.TicketPriorityId;
            TicketFilter.TicketStatusId = Customer_TicketFilterDTO.TicketStatusId;
            TicketFilter.TicketSourceId = Customer_TicketFilterDTO.TicketSourceId;
            TicketFilter.TicketNumber = Customer_TicketFilterDTO.TicketNumber;
            TicketFilter.DepartmentId = Customer_TicketFilterDTO.DepartmentId;
            TicketFilter.RelatedTicketId = Customer_TicketFilterDTO.RelatedTicketId;
            TicketFilter.SLA = Customer_TicketFilterDTO.SLA;
            TicketFilter.RelatedCallLogId = Customer_TicketFilterDTO.RelatedCallLogId;
            TicketFilter.ResponseMethodId = Customer_TicketFilterDTO.ResponseMethodId;
            TicketFilter.StatusId = Customer_TicketFilterDTO.StatusId;
            TicketFilter.CreatedAt = Customer_TicketFilterDTO.CreatedAt;
            TicketFilter.UpdatedAt = Customer_TicketFilterDTO.UpdatedAt;
            TicketFilter.TicketResolveTypeId = Customer_TicketFilterDTO.TicketResolveTypeId;
            TicketFilter.ResolveContent = Customer_TicketFilterDTO.ResolveContent;
            TicketFilter.closedAt = Customer_TicketFilterDTO.closedAt;
            TicketFilter.AppUserClosedId = Customer_TicketFilterDTO.AppUserClosedId;
            TicketFilter.FirstResponseAt = Customer_TicketFilterDTO.FirstResponseAt;
            TicketFilter.LastResponseAt = Customer_TicketFilterDTO.LastResponseAt;
            TicketFilter.LastHoldingAt = Customer_TicketFilterDTO.LastHoldingAt;
            TicketFilter.ReraisedTimes = Customer_TicketFilterDTO.ReraisedTimes;
            TicketFilter.ResolvedAt = Customer_TicketFilterDTO.ResolvedAt;
            TicketFilter.AppUserResolvedId = Customer_TicketFilterDTO.AppUserResolvedId;
            TicketFilter.SLAPolicyId = Customer_TicketFilterDTO.SLAPolicyId;
            TicketFilter.HoldingTime = Customer_TicketFilterDTO.HoldingTime;
            TicketFilter.ResolveTime = Customer_TicketFilterDTO.ResolveTime;
            return TicketFilter;
        }
    }
}

