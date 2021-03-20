using CRM.Common;
using CRM.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.repair_ticket
{
    public partial class RepairTicketController : RpcController
    {
        [Route(RepairTicketRoute.FilterListAppUser), HttpPost]
        public async Task<ActionResult<List<RepairTicket_AppUserDTO>>> FilterListAppUser([FromBody] RepairTicket_AppUserFilterDTO RepairTicket_AppUserFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = RepairTicket_AppUserFilterDTO.Id;
            AppUserFilter.Username = RepairTicket_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = RepairTicket_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = RepairTicket_AppUserFilterDTO.Address;
            AppUserFilter.Email = RepairTicket_AppUserFilterDTO.Email;
            AppUserFilter.Phone = RepairTicket_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = RepairTicket_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = RepairTicket_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = RepairTicket_AppUserFilterDTO.Avatar;
            AppUserFilter.Department = RepairTicket_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = RepairTicket_AppUserFilterDTO.OrganizationId;
            AppUserFilter.Longitude = RepairTicket_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = RepairTicket_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = RepairTicket_AppUserFilterDTO.StatusId;

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<RepairTicket_AppUserDTO> RepairTicket_AppUserDTOs = AppUsers
                .Select(x => new RepairTicket_AppUserDTO(x)).ToList();
            return RepairTicket_AppUserDTOs;
        }
        [Route(RepairTicketRoute.FilterListCustomer), HttpPost]
        public async Task<ActionResult<List<RepairTicket_CustomerDTO>>> FilterListCustomer([FromBody] RepairTicket_CustomerFilterDTO RepairTicket_CustomerFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Skip = 0;
            CustomerFilter.Take = 20;
            CustomerFilter.OrderBy = CustomerOrder.Id;
            CustomerFilter.OrderType = OrderType.ASC;
            CustomerFilter.Selects = CustomerSelect.ALL;
            CustomerFilter.Id = RepairTicket_CustomerFilterDTO.Id;
            CustomerFilter.Code = RepairTicket_CustomerFilterDTO.Code;
            CustomerFilter.Name = RepairTicket_CustomerFilterDTO.FullName;
            CustomerFilter.Phone = RepairTicket_CustomerFilterDTO.Phone;
            CustomerFilter.Email = RepairTicket_CustomerFilterDTO.Email;
            CustomerFilter.Address = RepairTicket_CustomerFilterDTO.Address;
            CustomerFilter.StatusId = RepairTicket_CustomerFilterDTO.StatusId;
            CustomerFilter.NationId = RepairTicket_CustomerFilterDTO.NationId;
            CustomerFilter.ProvinceId = RepairTicket_CustomerFilterDTO.ProvinceId;
            CustomerFilter.DistrictId = RepairTicket_CustomerFilterDTO.DistrictId;
            CustomerFilter.WardId = RepairTicket_CustomerFilterDTO.WardId;
            CustomerFilter.ProfessionId = RepairTicket_CustomerFilterDTO.ProfessionId;

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<RepairTicket_CustomerDTO> RepairTicket_CustomerDTOs = Customers
                .Select(x => new RepairTicket_CustomerDTO(x)).ToList();
            return RepairTicket_CustomerDTOs;
        }
        [Route(RepairTicketRoute.FilterListOrderCategory), HttpPost]
        public async Task<ActionResult<List<RepairTicket_OrderCategoryDTO>>> FilterListOrderCategory([FromBody] RepairTicket_OrderCategoryFilterDTO RepairTicket_OrderCategoryFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderCategoryFilter OrderCategoryFilter = new OrderCategoryFilter();
            OrderCategoryFilter.Skip = 0;
            OrderCategoryFilter.Take = int.MaxValue;
            OrderCategoryFilter.Take = 20;
            OrderCategoryFilter.OrderBy = OrderCategoryOrder.Id;
            OrderCategoryFilter.OrderType = OrderType.ASC;
            OrderCategoryFilter.Selects = OrderCategorySelect.ALL;

            List<OrderCategory> OrderCategorys = await OrderCategoryService.List(OrderCategoryFilter);
            List<RepairTicket_OrderCategoryDTO> RepairTicket_OrderCategoryDTOs = OrderCategorys
                .Select(x => new RepairTicket_OrderCategoryDTO(x)).ToList();
            return RepairTicket_OrderCategoryDTOs;
        }
        
        [Route(RepairTicketRoute.FilterListRepairStatus), HttpPost]
        public async Task<ActionResult<List<RepairTicket_RepairStatusDTO>>> FilterListRepairStatus([FromBody] RepairTicket_RepairStatusFilterDTO RepairTicket_RepairStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RepairStatusFilter RepairStatusFilter = new RepairStatusFilter();
            RepairStatusFilter.Skip = 0;
            RepairStatusFilter.Take = int.MaxValue;
            RepairStatusFilter.Take = 20;
            RepairStatusFilter.OrderBy = RepairStatusOrder.Id;
            RepairStatusFilter.OrderType = OrderType.ASC;
            RepairStatusFilter.Selects = RepairStatusSelect.ALL;

            List<RepairStatus> RepairStatuses = await RepairStatusService.List(RepairStatusFilter);
            List<RepairTicket_RepairStatusDTO> RepairTicket_RepairStatusDTOs = RepairStatuses
                .Select(x => new RepairTicket_RepairStatusDTO(x)).ToList();
            return RepairTicket_RepairStatusDTOs;
        }
        [Route(RepairTicketRoute.FilterListPaymentStatus), HttpPost]
        public async Task<ActionResult<List<RepairTicket_PaymentStatusDTO>>> FilterListPaymentStatus([FromBody] RepairTicket_PaymentStatusFilterDTO RepairTicket_PaymentStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            PaymentStatusFilter PaymentStatusFilter = new PaymentStatusFilter();
            PaymentStatusFilter.Skip = 0;
            PaymentStatusFilter.Take = int.MaxValue;
            PaymentStatusFilter.Take = 20;
            PaymentStatusFilter.OrderBy = PaymentStatusOrder.Id;
            PaymentStatusFilter.OrderType = OrderType.ASC;
            PaymentStatusFilter.Selects = PaymentStatusSelect.ALL;

            List<PaymentStatus> PaymentStatuses = await PaymentStatusService.List(PaymentStatusFilter);
            List<RepairTicket_PaymentStatusDTO> RepairTicket_PaymentStatusDTOs = PaymentStatuses
                .Select(x => new RepairTicket_PaymentStatusDTO(x)).ToList();
            return RepairTicket_PaymentStatusDTOs;
        }

        [Route(RepairTicketRoute.SingleListAppUser), HttpPost]
        public async Task<ActionResult<List<RepairTicket_AppUserDTO>>> SingleListAppUser([FromBody] RepairTicket_AppUserFilterDTO RepairTicket_AppUserFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AppUserFilter AppUserFilter = new AppUserFilter();
            AppUserFilter.Skip = 0;
            AppUserFilter.Take = 20;
            AppUserFilter.OrderBy = AppUserOrder.Id;
            AppUserFilter.OrderType = OrderType.ASC;
            AppUserFilter.Selects = AppUserSelect.ALL;
            AppUserFilter.Id = RepairTicket_AppUserFilterDTO.Id;
            AppUserFilter.Username = RepairTicket_AppUserFilterDTO.Username;
            AppUserFilter.DisplayName = RepairTicket_AppUserFilterDTO.DisplayName;
            AppUserFilter.Address = RepairTicket_AppUserFilterDTO.Address;
            AppUserFilter.Email = RepairTicket_AppUserFilterDTO.Email;
            AppUserFilter.Phone = RepairTicket_AppUserFilterDTO.Phone;
            AppUserFilter.SexId = RepairTicket_AppUserFilterDTO.SexId;
            AppUserFilter.Birthday = RepairTicket_AppUserFilterDTO.Birthday;
            AppUserFilter.Avatar = RepairTicket_AppUserFilterDTO.Avatar;
            AppUserFilter.Department = RepairTicket_AppUserFilterDTO.Department;
            AppUserFilter.OrganizationId = RepairTicket_AppUserFilterDTO.OrganizationId;
            AppUserFilter.Longitude = RepairTicket_AppUserFilterDTO.Longitude;
            AppUserFilter.Latitude = RepairTicket_AppUserFilterDTO.Latitude;
            AppUserFilter.StatusId = RepairTicket_AppUserFilterDTO.StatusId;

            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            List<RepairTicket_AppUserDTO> RepairTicket_AppUserDTOs = AppUsers
                .Select(x => new RepairTicket_AppUserDTO(x)).ToList();
            return RepairTicket_AppUserDTOs;
        }
        [Route(RepairTicketRoute.SingleListCustomer), HttpPost]
        public async Task<ActionResult<List<RepairTicket_CustomerDTO>>> SingleListCustomer([FromBody] RepairTicket_CustomerFilterDTO RepairTicket_CustomerFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Skip = 0;
            CustomerFilter.Take = 20;
            CustomerFilter.OrderBy = CustomerOrder.Id;
            CustomerFilter.OrderType = OrderType.ASC;
            CustomerFilter.Selects = CustomerSelect.ALL;
            CustomerFilter.Id = RepairTicket_CustomerFilterDTO.Id;
            CustomerFilter.Code = RepairTicket_CustomerFilterDTO.Code;
            CustomerFilter.Name = RepairTicket_CustomerFilterDTO.FullName;
            CustomerFilter.Phone = RepairTicket_CustomerFilterDTO.Phone;
            CustomerFilter.Email = RepairTicket_CustomerFilterDTO.Email;
            CustomerFilter.Address = RepairTicket_CustomerFilterDTO.Address;
            CustomerFilter.StatusId = RepairTicket_CustomerFilterDTO.StatusId;
            CustomerFilter.NationId = RepairTicket_CustomerFilterDTO.NationId;
            CustomerFilter.ProvinceId = RepairTicket_CustomerFilterDTO.ProvinceId;
            CustomerFilter.DistrictId = RepairTicket_CustomerFilterDTO.DistrictId;
            CustomerFilter.WardId = RepairTicket_CustomerFilterDTO.WardId;
            CustomerFilter.ProfessionId = RepairTicket_CustomerFilterDTO.ProfessionId;

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<RepairTicket_CustomerDTO> RepairTicket_CustomerDTOs = Customers
                .Select(x => new RepairTicket_CustomerDTO(x)).ToList();
            return RepairTicket_CustomerDTOs;
        }
        [Route(RepairTicketRoute.SingleListItem), HttpPost]
        public async Task<ActionResult<List<RepairTicket_ItemDTO>>> SingleListItem([FromBody] RepairTicket_ItemFilterDTO RepairTicket_ItemFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ItemFilter ItemFilter = new ItemFilter();
            ItemFilter.Skip = 0;
            ItemFilter.Take = 20;
            ItemFilter.OrderBy = ItemOrder.Id;
            ItemFilter.OrderType = OrderType.ASC;
            ItemFilter.Selects = ItemSelect.ALL;
            ItemFilter.Id = RepairTicket_ItemFilterDTO.Id;
            ItemFilter.ProductId = RepairTicket_ItemFilterDTO.ProductId;
            ItemFilter.Code = RepairTicket_ItemFilterDTO.Code;
            ItemFilter.Name = RepairTicket_ItemFilterDTO.Name;
            ItemFilter.ScanCode = RepairTicket_ItemFilterDTO.ScanCode;
            ItemFilter.SalePrice = RepairTicket_ItemFilterDTO.SalePrice;
            ItemFilter.RetailPrice = RepairTicket_ItemFilterDTO.RetailPrice;
            ItemFilter.StatusId = RepairTicket_ItemFilterDTO.StatusId;

            List<Item> Items = await RepairTicketService.ListItem(ItemFilter, RepairTicket_ItemFilterDTO.OrderCategoryId, RepairTicket_ItemFilterDTO.OrderId);
            List<RepairTicket_ItemDTO> RepairTicket_ItemDTOs = Items
                .Select(x => new RepairTicket_ItemDTO(x)).ToList();
            return RepairTicket_ItemDTOs;
        }
        [Route(RepairTicketRoute.SingleListOrderCategory), HttpPost]
        public async Task<ActionResult<List<RepairTicket_OrderCategoryDTO>>> SingleListOrderCategory([FromBody] RepairTicket_OrderCategoryFilterDTO RepairTicket_OrderCategoryFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderCategoryFilter OrderCategoryFilter = new OrderCategoryFilter();
            OrderCategoryFilter.Skip = 0;
            OrderCategoryFilter.Take = int.MaxValue;
            OrderCategoryFilter.Take = 20;
            OrderCategoryFilter.OrderBy = OrderCategoryOrder.Id;
            OrderCategoryFilter.OrderType = OrderType.ASC;
            OrderCategoryFilter.Selects = OrderCategorySelect.ALL;

            List<OrderCategory> OrderCategorys = await OrderCategoryService.List(OrderCategoryFilter);
            List<RepairTicket_OrderCategoryDTO> RepairTicket_OrderCategoryDTOs = OrderCategorys
                .Select(x => new RepairTicket_OrderCategoryDTO(x)).ToList();
            return RepairTicket_OrderCategoryDTOs;
        }
        [Route(RepairTicketRoute.SingleListPaymentStatus), HttpPost]
        public async Task<ActionResult<List<RepairTicket_PaymentStatusDTO>>> SingleListPaymentStatus([FromBody] RepairTicket_PaymentStatusFilterDTO RepairTicket_PaymentStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            PaymentStatusFilter PaymentStatusFilter = new PaymentStatusFilter();
            PaymentStatusFilter.Skip = 0;
            PaymentStatusFilter.Take = int.MaxValue;
            PaymentStatusFilter.Take = 20;
            PaymentStatusFilter.OrderBy = PaymentStatusOrder.Id;
            PaymentStatusFilter.OrderType = OrderType.ASC;
            PaymentStatusFilter.Selects = PaymentStatusSelect.ALL;

            List<PaymentStatus> PaymentStatuses = await PaymentStatusService.List(PaymentStatusFilter);
            List<RepairTicket_PaymentStatusDTO> RepairTicket_PaymentStatusDTOs = PaymentStatuses
                .Select(x => new RepairTicket_PaymentStatusDTO(x)).ToList();
            return RepairTicket_PaymentStatusDTOs;
        }
        [Route(RepairTicketRoute.SingleListRepairStatus), HttpPost]
        public async Task<ActionResult<List<RepairTicket_RepairStatusDTO>>> SingleListRepairStatus([FromBody] RepairTicket_RepairStatusFilterDTO RepairTicket_RepairStatusFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RepairStatusFilter RepairStatusFilter = new RepairStatusFilter();
            RepairStatusFilter.Skip = 0;
            RepairStatusFilter.Take = int.MaxValue;
            RepairStatusFilter.Take = 20;
            RepairStatusFilter.OrderBy = RepairStatusOrder.Id;
            RepairStatusFilter.OrderType = OrderType.ASC;
            RepairStatusFilter.Selects = RepairStatusSelect.ALL;

            List<RepairStatus> RepairStatuses = await RepairStatusService.List(RepairStatusFilter);
            List<RepairTicket_RepairStatusDTO> RepairTicket_RepairStatusDTOs = RepairStatuses
                .Select(x => new RepairTicket_RepairStatusDTO(x)).ToList();
            return RepairTicket_RepairStatusDTOs;
        }

        [Route(RepairTicketRoute.SingleListDirectSalesOrder), HttpPost]
        public async Task<ActionResult<List<RepairTicket_DirectSalesOrderDTO>>> SingleListDirectSalesOrder([FromBody] RepairTicket_DirectSalesOrderFilterDTO RepairTicket_DirectSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DirectSalesOrderFilter DirectSalesOrderFilter = new DirectSalesOrderFilter();
            DirectSalesOrderFilter.Skip = 0;
            DirectSalesOrderFilter.Take = int.MaxValue;
            DirectSalesOrderFilter.Take = 20;
            DirectSalesOrderFilter.OrderBy = DirectSalesOrderOrder.Id;
            DirectSalesOrderFilter.OrderType = OrderType.ASC;
            DirectSalesOrderFilter.Selects = DirectSalesOrderSelect.ALL;
            DirectSalesOrderFilter.Code = RepairTicket_DirectSalesOrderFilterDTO.Code;

            List<DirectSalesOrder> DirectSalesOrderes = await DirectSalesOrderService.List(DirectSalesOrderFilter);
            List<RepairTicket_DirectSalesOrderDTO> RepairTicket_DirectSalesOrderDTOs = DirectSalesOrderes
                .Select(x => new RepairTicket_DirectSalesOrderDTO(x)).ToList();
            return RepairTicket_DirectSalesOrderDTOs;
        }

        [Route(RepairTicketRoute.SingleListCustomerSalesOrder), HttpPost]
        public async Task<ActionResult<List<RepairTicket_CustomerSalesOrderDTO>>> SingleListCustomerSalesOrder([FromBody] RepairTicket_CustomerSalesOrderFilterDTO RepairTicket_CustomerSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerSalesOrderFilter CustomerSalesOrderFilter = new CustomerSalesOrderFilter();
            CustomerSalesOrderFilter.Skip = 0;
            CustomerSalesOrderFilter.Take = int.MaxValue;
            CustomerSalesOrderFilter.Take = 20;
            CustomerSalesOrderFilter.OrderBy = CustomerSalesOrderOrder.Id;
            CustomerSalesOrderFilter.OrderType = OrderType.ASC;
            CustomerSalesOrderFilter.Selects = CustomerSalesOrderSelect.ALL;
            CustomerSalesOrderFilter.Code = RepairTicket_CustomerSalesOrderFilterDTO.Code;

            List<CustomerSalesOrder> CustomerSalesOrderes = await CustomerSalesOrderService.List(CustomerSalesOrderFilter);
            List<RepairTicket_CustomerSalesOrderDTO> RepairTicket_CustomerSalesOrderDTOs = CustomerSalesOrderes
                .Select(x => new RepairTicket_CustomerSalesOrderDTO(x)).ToList();
            return RepairTicket_CustomerSalesOrderDTOs;
        }
    }
}

