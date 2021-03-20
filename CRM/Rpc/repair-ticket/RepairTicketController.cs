using CRM.Common;
using CRM.Entities;
using CRM.Helpers;
using CRM.Models;
using CRM.Services.MAppUser;
using CRM.Services.MCustomer;
using CRM.Services.MCustomerSalesOrder;
using CRM.Services.MDirectSalesOrder;
using CRM.Services.MOrderCategory;
using CRM.Services.MPaymentStatus;
using CRM.Services.MProduct;
using CRM.Services.MRepairStatus;
using CRM.Services.MRepairTicket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.repair_ticket
{
    public partial class RepairTicketController : RpcController
    {
        private IAppUserService AppUserService;
        private ICustomerService CustomerService;
        private IItemService ItemService;
        private IOrderCategoryService OrderCategoryService;
        private ICustomerSalesOrderService CustomerSalesOrderService;
        private IDirectSalesOrderService DirectSalesOrderService;
        private IPaymentStatusService PaymentStatusService;
        private IRepairStatusService RepairStatusService;
        private IRepairTicketService RepairTicketService;
        private ICurrentContext CurrentContext;
        public RepairTicketController(
            IAppUserService AppUserService,
            ICustomerService CustomerService,
            IItemService ItemService,
            IOrderCategoryService OrderCategoryService,
            ICustomerSalesOrderService CustomerSalesOrderService,
            IDirectSalesOrderService DirectSalesOrderService,
            IPaymentStatusService PaymentStatusService,
            IRepairStatusService RepairStatusService,
            IRepairTicketService RepairTicketService,
            ICurrentContext CurrentContext
        ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.AppUserService = AppUserService;
            this.CustomerService = CustomerService;
            this.ItemService = ItemService;
            this.OrderCategoryService = OrderCategoryService;
            this.CustomerSalesOrderService = CustomerSalesOrderService;
            this.DirectSalesOrderService = DirectSalesOrderService;
            this.PaymentStatusService = PaymentStatusService;
            this.RepairStatusService = RepairStatusService;
            this.RepairTicketService = RepairTicketService;
            this.CurrentContext = CurrentContext;
        }

        [Route(RepairTicketRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] RepairTicket_RepairTicketFilterDTO RepairTicket_RepairTicketFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RepairTicketFilter RepairTicketFilter = ConvertFilterDTOToFilterEntity(RepairTicket_RepairTicketFilterDTO);
            RepairTicketFilter = await RepairTicketService.ToFilter(RepairTicketFilter);
            int count = await RepairTicketService.Count(RepairTicketFilter);
            return count;
        }

        [Route(RepairTicketRoute.List), HttpPost]
        public async Task<ActionResult<List<RepairTicket_RepairTicketDTO>>> List([FromBody] RepairTicket_RepairTicketFilterDTO RepairTicket_RepairTicketFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RepairTicketFilter RepairTicketFilter = ConvertFilterDTOToFilterEntity(RepairTicket_RepairTicketFilterDTO);
            RepairTicketFilter = await RepairTicketService.ToFilter(RepairTicketFilter);
            List<RepairTicket> RepairTickets = await RepairTicketService.List(RepairTicketFilter);
            List<RepairTicket_RepairTicketDTO> RepairTicket_RepairTicketDTOs = RepairTickets
                .Select(c => new RepairTicket_RepairTicketDTO(c)).ToList();
            return RepairTicket_RepairTicketDTOs;
        }

        [Route(RepairTicketRoute.Get), HttpPost]
        public async Task<ActionResult<RepairTicket_RepairTicketDTO>> Get([FromBody] RepairTicket_RepairTicketDTO RepairTicket_RepairTicketDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(RepairTicket_RepairTicketDTO.Id))
                return Forbid();

            RepairTicket RepairTicket = await RepairTicketService.Get(RepairTicket_RepairTicketDTO.Id);
            return new RepairTicket_RepairTicketDTO(RepairTicket);
        }

        [Route(RepairTicketRoute.Create), HttpPost]
        public async Task<ActionResult<RepairTicket_RepairTicketDTO>> Create([FromBody] RepairTicket_RepairTicketDTO RepairTicket_RepairTicketDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(RepairTicket_RepairTicketDTO.Id))
                return Forbid();

            RepairTicket RepairTicket = ConvertDTOToEntity(RepairTicket_RepairTicketDTO);
            RepairTicket = await RepairTicketService.Create(RepairTicket);
            RepairTicket_RepairTicketDTO = new RepairTicket_RepairTicketDTO(RepairTicket);
            if (RepairTicket.IsValidated)
                return RepairTicket_RepairTicketDTO;
            else
                return BadRequest(RepairTicket_RepairTicketDTO);
        }

        [Route(RepairTicketRoute.Update), HttpPost]
        public async Task<ActionResult<RepairTicket_RepairTicketDTO>> Update([FromBody] RepairTicket_RepairTicketDTO RepairTicket_RepairTicketDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(RepairTicket_RepairTicketDTO.Id))
                return Forbid();

            RepairTicket RepairTicket = ConvertDTOToEntity(RepairTicket_RepairTicketDTO);
            RepairTicket = await RepairTicketService.Update(RepairTicket);
            RepairTicket_RepairTicketDTO = new RepairTicket_RepairTicketDTO(RepairTicket);
            if (RepairTicket.IsValidated)
                return RepairTicket_RepairTicketDTO;
            else
                return BadRequest(RepairTicket_RepairTicketDTO);
        }

        [Route(RepairTicketRoute.Delete), HttpPost]
        public async Task<ActionResult<RepairTicket_RepairTicketDTO>> Delete([FromBody] RepairTicket_RepairTicketDTO RepairTicket_RepairTicketDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(RepairTicket_RepairTicketDTO.Id))
                return Forbid();

            RepairTicket RepairTicket = ConvertDTOToEntity(RepairTicket_RepairTicketDTO);
            RepairTicket = await RepairTicketService.Delete(RepairTicket);
            RepairTicket_RepairTicketDTO = new RepairTicket_RepairTicketDTO(RepairTicket);
            if (RepairTicket.IsValidated)
                return RepairTicket_RepairTicketDTO;
            else
                return BadRequest(RepairTicket_RepairTicketDTO);
        }

        [Route(RepairTicketRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RepairTicketFilter RepairTicketFilter = new RepairTicketFilter();
            RepairTicketFilter = await RepairTicketService.ToFilter(RepairTicketFilter);
            RepairTicketFilter.Id = new IdFilter { In = Ids };
            RepairTicketFilter.Selects = RepairTicketSelect.Id;
            RepairTicketFilter.Skip = 0;
            RepairTicketFilter.Take = int.MaxValue;

            List<RepairTicket> RepairTickets = await RepairTicketService.List(RepairTicketFilter);
            RepairTickets = await RepairTicketService.BulkDelete(RepairTickets);
            if (RepairTickets.Any(x => !x.IsValidated))
                return BadRequest(RepairTickets.Where(x => !x.IsValidated));
            return true;
        }

        [Route(RepairTicketRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            AppUserFilter CreatorFilter = new AppUserFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = AppUserSelect.ALL
            };
            List<AppUser> Creators = await AppUserService.List(CreatorFilter);
            CustomerFilter CustomerFilter = new CustomerFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CustomerSelect.ALL
            };
            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            ItemFilter ItemFilter = new ItemFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = ItemSelect.ALL
            };
            List<Item> Items = await ItemService.List(ItemFilter);
            OrderCategoryFilter OrderCategoryFilter = new OrderCategoryFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = OrderCategorySelect.ALL
            };
            List<OrderCategory> OrderCategorys = await OrderCategoryService.List(OrderCategoryFilter);
            PaymentStatusFilter PaymentStatusFilter = new PaymentStatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = PaymentStatusSelect.ALL
            };
            List<PaymentStatus> PaymentStatuses = await PaymentStatusService.List(PaymentStatusFilter);
            RepairStatusFilter RepairStatusFilter = new RepairStatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = RepairStatusSelect.ALL
            };
            List<RepairStatus> RepairStatuses = await RepairStatusService.List(RepairStatusFilter);
            List<RepairTicket> RepairTickets = new List<RepairTicket>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(RepairTickets);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int CodeColumn = 1 + StartColumn;
                int OrderIdColumn = 2 + StartColumn;
                int OrderCategoryIdColumn = 3 + StartColumn;
                int RepairDueDateColumn = 4 + StartColumn;
                int ItemIdColumn = 5 + StartColumn;
                int IsRejectRepairColumn = 6 + StartColumn;
                int RejectReasonColumn = 7 + StartColumn;
                int DeviceStateColumn = 8 + StartColumn;
                int RepairStatusIdColumn = 9 + StartColumn;
                int RepairAddessColumn = 10 + StartColumn;
                int ReceiveUserColumn = 11 + StartColumn;
                int ReceiveDateColumn = 12 + StartColumn;
                int RepairDateColumn = 13 + StartColumn;
                int ReturnDateColumn = 14 + StartColumn;
                int RepairSolutionColumn = 15 + StartColumn;
                int NoteColumn = 16 + StartColumn;
                int RepairCostColumn = 17 + StartColumn;
                int PaymentStatusIdColumn = 18 + StartColumn;
                int CustomerIdColumn = 19 + StartColumn;
                int CreatorIdColumn = 20 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string CodeValue = worksheet.Cells[i + StartRow, CodeColumn].Value?.ToString();
                    string OrderIdValue = worksheet.Cells[i + StartRow, OrderIdColumn].Value?.ToString();
                    string OrderCategoryIdValue = worksheet.Cells[i + StartRow, OrderCategoryIdColumn].Value?.ToString();
                    string RepairDueDateValue = worksheet.Cells[i + StartRow, RepairDueDateColumn].Value?.ToString();
                    string ItemIdValue = worksheet.Cells[i + StartRow, ItemIdColumn].Value?.ToString();
                    string IsRejectRepairValue = worksheet.Cells[i + StartRow, IsRejectRepairColumn].Value?.ToString();
                    string RejectReasonValue = worksheet.Cells[i + StartRow, RejectReasonColumn].Value?.ToString();
                    string DeviceStateValue = worksheet.Cells[i + StartRow, DeviceStateColumn].Value?.ToString();
                    string RepairStatusIdValue = worksheet.Cells[i + StartRow, RepairStatusIdColumn].Value?.ToString();
                    string RepairAddessValue = worksheet.Cells[i + StartRow, RepairAddessColumn].Value?.ToString();
                    string ReceiveUserValue = worksheet.Cells[i + StartRow, ReceiveUserColumn].Value?.ToString();
                    string ReceiveDateValue = worksheet.Cells[i + StartRow, ReceiveDateColumn].Value?.ToString();
                    string RepairDateValue = worksheet.Cells[i + StartRow, RepairDateColumn].Value?.ToString();
                    string ReturnDateValue = worksheet.Cells[i + StartRow, ReturnDateColumn].Value?.ToString();
                    string RepairSolutionValue = worksheet.Cells[i + StartRow, RepairSolutionColumn].Value?.ToString();
                    string NoteValue = worksheet.Cells[i + StartRow, NoteColumn].Value?.ToString();
                    string RepairCostValue = worksheet.Cells[i + StartRow, RepairCostColumn].Value?.ToString();
                    string PaymentStatusIdValue = worksheet.Cells[i + StartRow, PaymentStatusIdColumn].Value?.ToString();
                    string CustomerIdValue = worksheet.Cells[i + StartRow, CustomerIdColumn].Value?.ToString();
                    string CreatorIdValue = worksheet.Cells[i + StartRow, CreatorIdColumn].Value?.ToString();

                    RepairTicket RepairTicket = new RepairTicket();
                    RepairTicket.Code = CodeValue;
                    RepairTicket.RepairDueDate = DateTime.TryParse(RepairDueDateValue, out DateTime RepairDueDate) ? RepairDueDate : DateTime.Now;
                    RepairTicket.RejectReason = RejectReasonValue;
                    RepairTicket.DeviceState = DeviceStateValue;
                    RepairTicket.RepairAddess = RepairAddessValue;
                    RepairTicket.ReceiveUser = ReceiveUserValue;
                    RepairTicket.ReceiveDate = DateTime.TryParse(ReceiveDateValue, out DateTime ReceiveDate) ? ReceiveDate : DateTime.Now;
                    RepairTicket.RepairDate = DateTime.TryParse(RepairDateValue, out DateTime RepairDate) ? RepairDate : DateTime.Now;
                    RepairTicket.ReturnDate = DateTime.TryParse(ReturnDateValue, out DateTime ReturnDate) ? ReturnDate : DateTime.Now;
                    RepairTicket.RepairSolution = RepairSolutionValue;
                    RepairTicket.Note = NoteValue;
                    RepairTicket.RepairCost = decimal.TryParse(RepairCostValue, out decimal RepairCost) ? RepairCost : 0;
                    AppUser Creator = Creators.Where(x => x.Id.ToString() == CreatorIdValue).FirstOrDefault();
                    RepairTicket.CreatorId = Creator == null ? 0 : Creator.Id;
                    RepairTicket.Creator = Creator;
                    Customer Customer = Customers.Where(x => x.Id.ToString() == CustomerIdValue).FirstOrDefault();
                    RepairTicket.CustomerId = Customer == null ? 0 : Customer.Id;
                    RepairTicket.Customer = Customer;
                    Item Item = Items.Where(x => x.Id.ToString() == ItemIdValue).FirstOrDefault();
                    RepairTicket.ItemId = Item == null ? 0 : Item.Id;
                    RepairTicket.Item = Item;
                    OrderCategory OrderCategory = OrderCategorys.Where(x => x.Id.ToString() == OrderCategoryIdValue).FirstOrDefault();
                    RepairTicket.OrderCategoryId = OrderCategory == null ? 0 : OrderCategory.Id;
                    RepairTicket.OrderCategory = OrderCategory;
                    PaymentStatus PaymentStatus = PaymentStatuses.Where(x => x.Id.ToString() == PaymentStatusIdValue).FirstOrDefault();
                    RepairTicket.PaymentStatusId = PaymentStatus == null ? 0 : PaymentStatus.Id;
                    RepairTicket.PaymentStatus = PaymentStatus;
                    RepairStatus RepairStatus = RepairStatuses.Where(x => x.Id.ToString() == RepairStatusIdValue).FirstOrDefault();
                    RepairTicket.RepairStatusId = RepairStatus == null ? 0 : RepairStatus.Id;
                    RepairTicket.RepairStatus = RepairStatus;

                    RepairTickets.Add(RepairTicket);
                }
            }
            RepairTickets = await RepairTicketService.Import(RepairTickets);
            if (RepairTickets.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < RepairTickets.Count; i++)
                {
                    RepairTicket RepairTicket = RepairTickets[i];
                    if (!RepairTicket.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.Id)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.Id)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.Code)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.Code)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.OrderId)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.OrderId)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.OrderCategoryId)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.OrderCategoryId)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.RepairDueDate)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.RepairDueDate)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.ItemId)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.ItemId)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.IsRejectRepair)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.IsRejectRepair)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.RejectReason)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.RejectReason)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.DeviceState)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.DeviceState)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.RepairStatusId)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.RepairStatusId)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.RepairAddess)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.RepairAddess)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.ReceiveUser)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.ReceiveUser)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.ReceiveDate)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.ReceiveDate)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.RepairDate)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.RepairDate)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.ReturnDate)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.ReturnDate)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.RepairSolution)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.RepairSolution)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.Note)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.Note)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.RepairCost)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.RepairCost)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.PaymentStatusId)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.PaymentStatusId)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.CustomerId)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.CustomerId)];
                        if (RepairTicket.Errors.ContainsKey(nameof(RepairTicket.CreatorId)))
                            Error += RepairTicket.Errors[nameof(RepairTicket.CreatorId)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }

        [Route(RepairTicketRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] RepairTicket_RepairTicketFilterDTO RepairTicket_RepairTicketFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region RepairTicket
                var RepairTicketFilter = ConvertFilterDTOToFilterEntity(RepairTicket_RepairTicketFilterDTO);
                RepairTicketFilter.Skip = 0;
                RepairTicketFilter.Take = int.MaxValue;
                RepairTicketFilter = await RepairTicketService.ToFilter(RepairTicketFilter);
                List<RepairTicket> RepairTickets = await RepairTicketService.List(RepairTicketFilter);

                var RepairTicketHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "OrderId",
                        "OrderCategoryId",
                        "RepairDueDate",
                        "ItemId",
                        "IsRejectRepair",
                        "RejectReason",
                        "DeviceState",
                        "RepairStatusId",
                        "RepairAddess",
                        "ReceiveUser",
                        "ReceiveDate",
                        "RepairDate",
                        "ReturnDate",
                        "RepairSolution",
                        "Note",
                        "RepairCost",
                        "PaymentStatusId",
                        "CustomerId",
                        "CreatorId",
                    }
                };
                List<object[]> RepairTicketData = new List<object[]>();
                for (int i = 0; i < RepairTickets.Count; i++)
                {
                    var RepairTicket = RepairTickets[i];
                    RepairTicketData.Add(new Object[]
                    {
                        RepairTicket.Id,
                        RepairTicket.Code,
                        RepairTicket.OrderId,
                        RepairTicket.OrderCategoryId,
                        RepairTicket.RepairDueDate,
                        RepairTicket.ItemId,
                        RepairTicket.IsRejectRepair,
                        RepairTicket.RejectReason,
                        RepairTicket.DeviceState,
                        RepairTicket.RepairStatusId,
                        RepairTicket.RepairAddess,
                        RepairTicket.ReceiveUser,
                        RepairTicket.ReceiveDate,
                        RepairTicket.RepairDate,
                        RepairTicket.ReturnDate,
                        RepairTicket.RepairSolution,
                        RepairTicket.Note,
                        RepairTicket.RepairCost,
                        RepairTicket.PaymentStatusId,
                        RepairTicket.CustomerId,
                        RepairTicket.CreatorId,
                    });
                }
                excel.GenerateWorksheet("RepairTicket", RepairTicketHeaders, RepairTicketData);
                #endregion

                #region AppUser
                var AppUserFilter = new AppUserFilter();
                AppUserFilter.Selects = AppUserSelect.ALL;
                AppUserFilter.OrderBy = AppUserOrder.Id;
                AppUserFilter.OrderType = OrderType.ASC;
                AppUserFilter.Skip = 0;
                AppUserFilter.Take = int.MaxValue;
                List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);

                var AppUserHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Username",
                        "DisplayName",
                        "Address",
                        "Email",
                        "Phone",
                        "SexId",
                        "Birthday",
                        "Avatar",
                        "Department",
                        "OrganizationId",
                        "Longitude",
                        "Latitude",
                        "StatusId",
                        "RowId",
                        "Used",
                    }
                };
                List<object[]> AppUserData = new List<object[]>();
                for (int i = 0; i < AppUsers.Count; i++)
                {
                    var AppUser = AppUsers[i];
                    AppUserData.Add(new Object[]
                    {
                        AppUser.Id,
                        AppUser.Username,
                        AppUser.DisplayName,
                        AppUser.Address,
                        AppUser.Email,
                        AppUser.Phone,
                        AppUser.SexId,
                        AppUser.Birthday,
                        AppUser.Avatar,
                        AppUser.Department,
                        AppUser.OrganizationId,
                        AppUser.Longitude,
                        AppUser.Latitude,
                        AppUser.StatusId,
                        AppUser.RowId,
                        AppUser.Used,
                    });
                }
                excel.GenerateWorksheet("AppUser", AppUserHeaders, AppUserData);
                #endregion
                #region Customer
                var CustomerFilter = new CustomerFilter();
                CustomerFilter.Selects = CustomerSelect.ALL;
                CustomerFilter.OrderBy = CustomerOrder.Id;
                CustomerFilter.OrderType = OrderType.ASC;
                CustomerFilter.Skip = 0;
                CustomerFilter.Take = int.MaxValue;
                List<Customer> Customers = await CustomerService.List(CustomerFilter);

                var CustomerHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "FullName",
                        "Phone",
                        "IdentificationNumber",
                        "Email",
                        "Address",
                        "StatusId",
                        "NationId",
                        "ProvinceId",
                        "DistrictId",
                        "WardId",
                        "CreatorId",
                        "OrganizationId",
                        "ImageId",
                        "ProfessionId",
                        "Used",
                    }
                };
                List<object[]> CustomerData = new List<object[]>();
                for (int i = 0; i < Customers.Count; i++)
                {
                    var Customer = Customers[i];
                    CustomerData.Add(new Object[]
                    {
                        Customer.Id,
                        Customer.Code,
                        Customer.Name,
                        Customer.Phone,
                        Customer.Email,
                        Customer.Address,
                        Customer.StatusId,
                        Customer.NationId,
                        Customer.ProvinceId,
                        Customer.DistrictId,
                        Customer.WardId,
                        Customer.ProfessionId,
                        Customer.Used,
                    });
                }
                excel.GenerateWorksheet("Customer", CustomerHeaders, CustomerData);
                #endregion
                #region Item
                var ItemFilter = new ItemFilter();
                ItemFilter.Selects = ItemSelect.ALL;
                ItemFilter.OrderBy = ItemOrder.Id;
                ItemFilter.OrderType = OrderType.ASC;
                ItemFilter.Skip = 0;
                ItemFilter.Take = int.MaxValue;
                List<Item> Items = await ItemService.List(ItemFilter);

                var ItemHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "ProductId",
                        "Code",
                        "Name",
                        "ScanCode",
                        "SalePrice",
                        "RetailPrice",
                        "StatusId",
                        "Used",
                        "RowId",
                    }
                };
                List<object[]> ItemData = new List<object[]>();
                for (int i = 0; i < Items.Count; i++)
                {
                    var Item = Items[i];
                    ItemData.Add(new Object[]
                    {
                        Item.Id,
                        Item.ProductId,
                        Item.Code,
                        Item.Name,
                        Item.ScanCode,
                        Item.SalePrice,
                        Item.RetailPrice,
                        Item.StatusId,
                        Item.Used,
                        Item.RowId,
                    });
                }
                excel.GenerateWorksheet("Item", ItemHeaders, ItemData);
                #endregion
                #region OrderCategory
                var OrderCategoryFilter = new OrderCategoryFilter();
                OrderCategoryFilter.Selects = OrderCategorySelect.ALL;
                OrderCategoryFilter.OrderBy = OrderCategoryOrder.Id;
                OrderCategoryFilter.OrderType = OrderType.ASC;
                OrderCategoryFilter.Skip = 0;
                OrderCategoryFilter.Take = int.MaxValue;
                List<OrderCategory> OrderCategorys = await OrderCategoryService.List(OrderCategoryFilter);

                var OrderCategoryHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> OrderCategoryData = new List<object[]>();
                for (int i = 0; i < OrderCategorys.Count; i++)
                {
                    var OrderCategory = OrderCategorys[i];
                    OrderCategoryData.Add(new Object[]
                    {
                        OrderCategory.Id,
                        OrderCategory.Code,
                        OrderCategory.Name,
                    });
                }
                excel.GenerateWorksheet("OrderCategory", OrderCategoryHeaders, OrderCategoryData);
                #endregion
                #region PaymentStatus
                var PaymentStatusFilter = new PaymentStatusFilter();
                PaymentStatusFilter.Selects = PaymentStatusSelect.ALL;
                PaymentStatusFilter.OrderBy = PaymentStatusOrder.Id;
                PaymentStatusFilter.OrderType = OrderType.ASC;
                PaymentStatusFilter.Skip = 0;
                PaymentStatusFilter.Take = int.MaxValue;
                List<PaymentStatus> PaymentStatuses = await PaymentStatusService.List(PaymentStatusFilter);

                var PaymentStatusHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> PaymentStatusData = new List<object[]>();
                for (int i = 0; i < PaymentStatuses.Count; i++)
                {
                    var PaymentStatus = PaymentStatuses[i];
                    PaymentStatusData.Add(new Object[]
                    {
                        PaymentStatus.Id,
                        PaymentStatus.Code,
                        PaymentStatus.Name,
                    });
                }
                excel.GenerateWorksheet("PaymentStatus", PaymentStatusHeaders, PaymentStatusData);
                #endregion
                #region RepairStatus
                var RepairStatusFilter = new RepairStatusFilter();
                RepairStatusFilter.Selects = RepairStatusSelect.ALL;
                RepairStatusFilter.OrderBy = RepairStatusOrder.Id;
                RepairStatusFilter.OrderType = OrderType.ASC;
                RepairStatusFilter.Skip = 0;
                RepairStatusFilter.Take = int.MaxValue;
                List<RepairStatus> RepairStatuses = await RepairStatusService.List(RepairStatusFilter);

                var RepairStatusHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Name",
                        "Code",
                    }
                };
                List<object[]> RepairStatusData = new List<object[]>();
                for (int i = 0; i < RepairStatuses.Count; i++)
                {
                    var RepairStatus = RepairStatuses[i];
                    RepairStatusData.Add(new Object[]
                    {
                        RepairStatus.Id,
                        RepairStatus.Name,
                        RepairStatus.Code,
                    });
                }
                excel.GenerateWorksheet("RepairStatus", RepairStatusHeaders, RepairStatusData);
                #endregion
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "RepairTicket.xlsx");
        }

        [Route(RepairTicketRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] RepairTicket_RepairTicketFilterDTO RepairTicket_RepairTicketFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            string path = "Templates/RepairTicket_Template.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "RepairTicket.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            RepairTicketFilter RepairTicketFilter = new RepairTicketFilter();
            RepairTicketFilter = await RepairTicketService.ToFilter(RepairTicketFilter);
            if (Id == 0)
            {

            }
            else
            {
                RepairTicketFilter.Id = new IdFilter { Equal = Id };
                int count = await RepairTicketService.Count(RepairTicketFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private RepairTicket ConvertDTOToEntity(RepairTicket_RepairTicketDTO RepairTicket_RepairTicketDTO)
        {
            RepairTicket RepairTicket = new RepairTicket();
            RepairTicket.Id = RepairTicket_RepairTicketDTO.Id;
            RepairTicket.Code = RepairTicket_RepairTicketDTO.Code;
            RepairTicket.DeviceSerial = RepairTicket_RepairTicketDTO.DeviceSerial;
            RepairTicket.OrderId = RepairTicket_RepairTicketDTO.OrderId;
            RepairTicket.OrderCategoryId = RepairTicket_RepairTicketDTO.OrderCategoryId;
            RepairTicket.RepairDueDate = RepairTicket_RepairTicketDTO.RepairDueDate;
            RepairTicket.ItemId = RepairTicket_RepairTicketDTO.ItemId;
            RepairTicket.IsRejectRepair = RepairTicket_RepairTicketDTO.IsRejectRepair;
            RepairTicket.RejectReason = RepairTicket_RepairTicketDTO.RejectReason;
            RepairTicket.DeviceState = RepairTicket_RepairTicketDTO.DeviceState;
            RepairTicket.RepairStatusId = RepairTicket_RepairTicketDTO.RepairStatusId;
            RepairTicket.RepairAddess = RepairTicket_RepairTicketDTO.RepairAddess;
            RepairTicket.ReceiveUser = RepairTicket_RepairTicketDTO.ReceiveUser;
            RepairTicket.ReceiveDate = RepairTicket_RepairTicketDTO.ReceiveDate;
            RepairTicket.RepairDate = RepairTicket_RepairTicketDTO.RepairDate;
            RepairTicket.ReturnDate = RepairTicket_RepairTicketDTO.ReturnDate;
            RepairTicket.RepairSolution = RepairTicket_RepairTicketDTO.RepairSolution;
            RepairTicket.Note = RepairTicket_RepairTicketDTO.Note;
            RepairTicket.RepairCost = RepairTicket_RepairTicketDTO.RepairCost;
            RepairTicket.PaymentStatusId = RepairTicket_RepairTicketDTO.PaymentStatusId;
            RepairTicket.CustomerId = RepairTicket_RepairTicketDTO.CustomerId;
            RepairTicket.CreatorId = RepairTicket_RepairTicketDTO.CreatorId;
            RepairTicket.Creator = RepairTicket_RepairTicketDTO.Creator == null ? null : new AppUser
            {
                Id = RepairTicket_RepairTicketDTO.Creator.Id,
                Username = RepairTicket_RepairTicketDTO.Creator.Username,
                DisplayName = RepairTicket_RepairTicketDTO.Creator.DisplayName,
                Address = RepairTicket_RepairTicketDTO.Creator.Address,
                Email = RepairTicket_RepairTicketDTO.Creator.Email,
                Phone = RepairTicket_RepairTicketDTO.Creator.Phone,
                SexId = RepairTicket_RepairTicketDTO.Creator.SexId,
                Birthday = RepairTicket_RepairTicketDTO.Creator.Birthday,
                Avatar = RepairTicket_RepairTicketDTO.Creator.Avatar,
                Department = RepairTicket_RepairTicketDTO.Creator.Department,
                OrganizationId = RepairTicket_RepairTicketDTO.Creator.OrganizationId,
                Longitude = RepairTicket_RepairTicketDTO.Creator.Longitude,
                Latitude = RepairTicket_RepairTicketDTO.Creator.Latitude,
                StatusId = RepairTicket_RepairTicketDTO.Creator.StatusId,
                RowId = RepairTicket_RepairTicketDTO.Creator.RowId,
                Used = RepairTicket_RepairTicketDTO.Creator.Used,
            };
            RepairTicket.Customer = RepairTicket_RepairTicketDTO.Customer == null ? null : new Customer
            {
                Id = RepairTicket_RepairTicketDTO.Customer.Id,
                Code = RepairTicket_RepairTicketDTO.Customer.Code,
                Name = RepairTicket_RepairTicketDTO.Customer.FullName,
                Phone = RepairTicket_RepairTicketDTO.Customer.Phone,
                Email = RepairTicket_RepairTicketDTO.Customer.Email,
                Address = RepairTicket_RepairTicketDTO.Customer.Address,
                StatusId = RepairTicket_RepairTicketDTO.Customer.StatusId,
                NationId = RepairTicket_RepairTicketDTO.Customer.NationId,
                ProvinceId = RepairTicket_RepairTicketDTO.Customer.ProvinceId,
                DistrictId = RepairTicket_RepairTicketDTO.Customer.DistrictId,
                WardId = RepairTicket_RepairTicketDTO.Customer.WardId,
                ProfessionId = RepairTicket_RepairTicketDTO.Customer.ProfessionId,
            };
            RepairTicket.Item = RepairTicket_RepairTicketDTO.Item == null ? null : new Item
            {
                Id = RepairTicket_RepairTicketDTO.Item.Id,
                ProductId = RepairTicket_RepairTicketDTO.Item.ProductId,
                Code = RepairTicket_RepairTicketDTO.Item.Code,
                Name = RepairTicket_RepairTicketDTO.Item.Name,
                ScanCode = RepairTicket_RepairTicketDTO.Item.ScanCode,
                SalePrice = RepairTicket_RepairTicketDTO.Item.SalePrice,
                RetailPrice = RepairTicket_RepairTicketDTO.Item.RetailPrice,
                StatusId = RepairTicket_RepairTicketDTO.Item.StatusId,
                Used = RepairTicket_RepairTicketDTO.Item.Used,
                RowId = RepairTicket_RepairTicketDTO.Item.RowId,
            };
            RepairTicket.OrderCategory = RepairTicket_RepairTicketDTO.OrderCategory == null ? null : new OrderCategory
            {
                Id = RepairTicket_RepairTicketDTO.OrderCategory.Id,
                Code = RepairTicket_RepairTicketDTO.OrderCategory.Code,
                Name = RepairTicket_RepairTicketDTO.OrderCategory.Name,
            };
            RepairTicket.PaymentStatus = RepairTicket_RepairTicketDTO.PaymentStatus == null ? null : new PaymentStatus
            {
                Id = RepairTicket_RepairTicketDTO.PaymentStatus.Id,
                Code = RepairTicket_RepairTicketDTO.PaymentStatus.Code,
                Name = RepairTicket_RepairTicketDTO.PaymentStatus.Name,
            };
            RepairTicket.RepairStatus = RepairTicket_RepairTicketDTO.RepairStatus == null ? null : new RepairStatus
            {
                Id = RepairTicket_RepairTicketDTO.RepairStatus.Id,
                Name = RepairTicket_RepairTicketDTO.RepairStatus.Name,
                Code = RepairTicket_RepairTicketDTO.RepairStatus.Code,
            };
            RepairTicket.BaseLanguage = CurrentContext.Language;
            return RepairTicket;
        }

        private RepairTicketFilter ConvertFilterDTOToFilterEntity(RepairTicket_RepairTicketFilterDTO RepairTicket_RepairTicketFilterDTO)
        {
            RepairTicketFilter RepairTicketFilter = new RepairTicketFilter();
            RepairTicketFilter.Selects = RepairTicketSelect.ALL;
            RepairTicketFilter.Skip = RepairTicket_RepairTicketFilterDTO.Skip;
            RepairTicketFilter.Take = RepairTicket_RepairTicketFilterDTO.Take;
            RepairTicketFilter.OrderBy = RepairTicket_RepairTicketFilterDTO.OrderBy;
            RepairTicketFilter.OrderType = RepairTicket_RepairTicketFilterDTO.OrderType;

            RepairTicketFilter.Id = RepairTicket_RepairTicketFilterDTO.Id;
            RepairTicketFilter.Code = RepairTicket_RepairTicketFilterDTO.Code;
            RepairTicketFilter.DeviceSerial = RepairTicket_RepairTicketFilterDTO.DeviceSerial;
            RepairTicketFilter.OrderId = RepairTicket_RepairTicketFilterDTO.OrderId;
            RepairTicketFilter.OrderCategoryId = RepairTicket_RepairTicketFilterDTO.OrderCategoryId;
            RepairTicketFilter.RepairDueDate = RepairTicket_RepairTicketFilterDTO.RepairDueDate;
            RepairTicketFilter.ItemId = RepairTicket_RepairTicketFilterDTO.ItemId;
            RepairTicketFilter.RejectReason = RepairTicket_RepairTicketFilterDTO.RejectReason;
            RepairTicketFilter.DeviceState = RepairTicket_RepairTicketFilterDTO.DeviceState;
            RepairTicketFilter.RepairStatusId = RepairTicket_RepairTicketFilterDTO.RepairStatusId;
            RepairTicketFilter.RepairAddess = RepairTicket_RepairTicketFilterDTO.RepairAddess;
            RepairTicketFilter.ReceiveUser = RepairTicket_RepairTicketFilterDTO.ReceiveUser;
            RepairTicketFilter.ReceiveDate = RepairTicket_RepairTicketFilterDTO.ReceiveDate;
            RepairTicketFilter.RepairDate = RepairTicket_RepairTicketFilterDTO.RepairDate;
            RepairTicketFilter.ReturnDate = RepairTicket_RepairTicketFilterDTO.ReturnDate;
            RepairTicketFilter.RepairSolution = RepairTicket_RepairTicketFilterDTO.RepairSolution;
            RepairTicketFilter.Note = RepairTicket_RepairTicketFilterDTO.Note;
            RepairTicketFilter.RepairCost = RepairTicket_RepairTicketFilterDTO.RepairCost;
            RepairTicketFilter.PaymentStatusId = RepairTicket_RepairTicketFilterDTO.PaymentStatusId;
            RepairTicketFilter.CustomerId = RepairTicket_RepairTicketFilterDTO.CustomerId;
            RepairTicketFilter.CreatorId = RepairTicket_RepairTicketFilterDTO.CreatorId;
            RepairTicketFilter.CreatedAt = RepairTicket_RepairTicketFilterDTO.CreatedAt;
            RepairTicketFilter.UpdatedAt = RepairTicket_RepairTicketFilterDTO.UpdatedAt;
            return RepairTicketFilter;
        }
    }
}

