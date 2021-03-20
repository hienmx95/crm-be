using CRM.Common;
using CRM.Entities;
using CRM.Helpers;
using CRM.Services.MAppUser;
using CRM.Services.MContract;
using CRM.Services.MCustomer;
using CRM.Services.MCustomerSalesOrder;
using CRM.Services.MCustomerSalesOrderContent;
using CRM.Services.MCustomerSalesOrderPaymentHistory;
using CRM.Services.MCustomerSalesOrderPromotion;
using CRM.Services.MCustomerType;
using CRM.Services.MDistrict;
using CRM.Services.MEditedPriceStatus;
using CRM.Services.MNation;
using CRM.Services.MOpportunity;
using CRM.Services.MOrderPaymentStatus;
using CRM.Services.MOrganization;
using CRM.Services.MProduct;
using CRM.Services.MProductGrouping;
using CRM.Services.MProductType;
using CRM.Services.MSupplier;
using CRM.Services.MTaxType;
using CRM.Services.MProvince;
using CRM.Services.MRequestState;
using CRM.Services.MUnitOfMeasure;
using CRM.Services.MWard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;

namespace CRM.Rpc.customer_sales_order
{
    public partial class CustomerSalesOrderController : RpcController
    {
        private IContractService ContractService;
        private IAppUserService AppUserService;
        private ICustomerService CustomerService;
        private ICustomerTypeService CustomerTypeService;
        private IDistrictService DistrictService;
        private INationService NationService;
        private IProvinceService ProvinceService;
        private IWardService WardService;
        private IEditedPriceStatusService EditedPriceStatusService;
        private IOpportunityService OpportunityService;
        private IOrderPaymentStatusService OrderPaymentStatusService;
        private IOrganizationService OrganizationService;
        private IRequestStateService RequestStateService;
        private ICustomerSalesOrderContentService CustomerSalesOrderContentService;
        private IItemService ItemService;
        private IUnitOfMeasureService UnitOfMeasureService;
        private ICustomerSalesOrderPaymentHistoryService CustomerSalesOrderPaymentHistoryService;
        private ICustomerSalesOrderPromotionService CustomerSalesOrderPromotionService;
        private ICustomerSalesOrderService CustomerSalesOrderService;
        private IProductService ProductService;
        private IProductGroupingService ProductGroupingService;
        private IProductTypeService ProductTypeService;
        private ISupplierService SupplierService;
        private ITaxTypeService TaxTypeService;
        private ICurrentContext CurrentContext;
        public CustomerSalesOrderController(
            IContractService ContractService,
            IAppUserService AppUserService,
            ICustomerService CustomerService,
            ICustomerTypeService CustomerTypeService,
            IDistrictService DistrictService,
            INationService NationService,
            IProvinceService ProvinceService,
            IWardService WardService,
            IEditedPriceStatusService EditedPriceStatusService,
            IOpportunityService OpportunityService,
            IOrderPaymentStatusService OrderPaymentStatusService,
            IOrganizationService OrganizationService,
            IRequestStateService RequestStateService,
            ICustomerSalesOrderContentService CustomerSalesOrderContentService,
            IItemService ItemService,
            IUnitOfMeasureService UnitOfMeasureService,
            ICustomerSalesOrderPaymentHistoryService CustomerSalesOrderPaymentHistoryService,
            ICustomerSalesOrderPromotionService CustomerSalesOrderPromotionService,
            ICustomerSalesOrderService CustomerSalesOrderService,
            IProductService ProductService,
            IProductGroupingService ProductGroupingService,
            IProductTypeService ProductTypeService,
            ISupplierService SupplierService,
            ITaxTypeService TaxTypeService,
            ICurrentContext CurrentContext
            ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.ContractService = ContractService;
            this.AppUserService = AppUserService;
            this.CustomerService = CustomerService;
            this.CustomerTypeService = CustomerTypeService;
            this.DistrictService = DistrictService;
            this.NationService = NationService;
            this.ProvinceService = ProvinceService;
            this.WardService = WardService;
            this.EditedPriceStatusService = EditedPriceStatusService;
            this.OpportunityService = OpportunityService;
            this.OrderPaymentStatusService = OrderPaymentStatusService;
            this.OrganizationService = OrganizationService;
            this.RequestStateService = RequestStateService;
            this.CustomerSalesOrderContentService = CustomerSalesOrderContentService;
            this.ItemService = ItemService;
            this.UnitOfMeasureService = UnitOfMeasureService;
            this.CustomerSalesOrderPaymentHistoryService = CustomerSalesOrderPaymentHistoryService;
            this.CustomerSalesOrderPromotionService = CustomerSalesOrderPromotionService;
            this.CustomerSalesOrderService = CustomerSalesOrderService;
            this.ProductService = ProductService;
            this.ProductGroupingService = ProductGroupingService;
            this.ProductTypeService = ProductTypeService;
            this.SupplierService = SupplierService;
            this.TaxTypeService = TaxTypeService;
            this.CurrentContext = CurrentContext;
        }

        [Route(CustomerSalesOrderRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] CustomerSalesOrder_CustomerSalesOrderFilterDTO CustomerSalesOrder_CustomerSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerSalesOrderFilter CustomerSalesOrderFilter = ConvertFilterDTOToFilterEntity(CustomerSalesOrder_CustomerSalesOrderFilterDTO);
            CustomerSalesOrderFilter = await CustomerSalesOrderService.ToFilter(CustomerSalesOrderFilter);
            int count = await CustomerSalesOrderService.Count(CustomerSalesOrderFilter);
            return count;
        }

        [Route(CustomerSalesOrderRoute.List), HttpPost]
        public async Task<ActionResult<List<CustomerSalesOrder_CustomerSalesOrderDTO>>> List([FromBody] CustomerSalesOrder_CustomerSalesOrderFilterDTO CustomerSalesOrder_CustomerSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerSalesOrderFilter CustomerSalesOrderFilter = ConvertFilterDTOToFilterEntity(CustomerSalesOrder_CustomerSalesOrderFilterDTO);
            CustomerSalesOrderFilter = await CustomerSalesOrderService.ToFilter(CustomerSalesOrderFilter);
            List<CustomerSalesOrder> CustomerSalesOrders = await CustomerSalesOrderService.List(CustomerSalesOrderFilter);
            List<CustomerSalesOrder_CustomerSalesOrderDTO> CustomerSalesOrder_CustomerSalesOrderDTOs = CustomerSalesOrders
                .Select(c => new CustomerSalesOrder_CustomerSalesOrderDTO(c)).ToList();
            return CustomerSalesOrder_CustomerSalesOrderDTOs;
        }

        [Route(CustomerSalesOrderRoute.Get), HttpPost]
        public async Task<ActionResult<CustomerSalesOrder_CustomerSalesOrderDTO>> Get([FromBody]CustomerSalesOrder_CustomerSalesOrderDTO CustomerSalesOrder_CustomerSalesOrderDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CustomerSalesOrder_CustomerSalesOrderDTO.Id))
                return Forbid();

            CustomerSalesOrder CustomerSalesOrder = await CustomerSalesOrderService.Get(CustomerSalesOrder_CustomerSalesOrderDTO.Id);
            return new CustomerSalesOrder_CustomerSalesOrderDTO(CustomerSalesOrder);
        }

        [Route(CustomerSalesOrderRoute.Create), HttpPost]
        public async Task<ActionResult<CustomerSalesOrder_CustomerSalesOrderDTO>> Create([FromBody] CustomerSalesOrder_CustomerSalesOrderDTO CustomerSalesOrder_CustomerSalesOrderDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(CustomerSalesOrder_CustomerSalesOrderDTO.Id))
                return Forbid();

            CustomerSalesOrder CustomerSalesOrder = ConvertDTOToEntity(CustomerSalesOrder_CustomerSalesOrderDTO);
            CustomerSalesOrder = await CustomerSalesOrderService.Create(CustomerSalesOrder);
            CustomerSalesOrder_CustomerSalesOrderDTO = new CustomerSalesOrder_CustomerSalesOrderDTO(CustomerSalesOrder);
            if (CustomerSalesOrder.IsValidated)
                return CustomerSalesOrder_CustomerSalesOrderDTO;
            else
                return BadRequest(CustomerSalesOrder_CustomerSalesOrderDTO);
        }

        [Route(CustomerSalesOrderRoute.Update), HttpPost]
        public async Task<ActionResult<CustomerSalesOrder_CustomerSalesOrderDTO>> Update([FromBody] CustomerSalesOrder_CustomerSalesOrderDTO CustomerSalesOrder_CustomerSalesOrderDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(CustomerSalesOrder_CustomerSalesOrderDTO.Id))
                return Forbid();

            CustomerSalesOrder CustomerSalesOrder = ConvertDTOToEntity(CustomerSalesOrder_CustomerSalesOrderDTO);
            CustomerSalesOrder = await CustomerSalesOrderService.Update(CustomerSalesOrder);
            CustomerSalesOrder_CustomerSalesOrderDTO = new CustomerSalesOrder_CustomerSalesOrderDTO(CustomerSalesOrder);
            if (CustomerSalesOrder.IsValidated)
                return CustomerSalesOrder_CustomerSalesOrderDTO;
            else
                return BadRequest(CustomerSalesOrder_CustomerSalesOrderDTO);
        }

        [Route(CustomerSalesOrderRoute.Delete), HttpPost]
        public async Task<ActionResult<CustomerSalesOrder_CustomerSalesOrderDTO>> Delete([FromBody] CustomerSalesOrder_CustomerSalesOrderDTO CustomerSalesOrder_CustomerSalesOrderDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CustomerSalesOrder_CustomerSalesOrderDTO.Id))
                return Forbid();

            CustomerSalesOrder CustomerSalesOrder = ConvertDTOToEntity(CustomerSalesOrder_CustomerSalesOrderDTO);
            CustomerSalesOrder = await CustomerSalesOrderService.Delete(CustomerSalesOrder);
            CustomerSalesOrder_CustomerSalesOrderDTO = new CustomerSalesOrder_CustomerSalesOrderDTO(CustomerSalesOrder);
            if (CustomerSalesOrder.IsValidated)
                return CustomerSalesOrder_CustomerSalesOrderDTO;
            else
                return BadRequest(CustomerSalesOrder_CustomerSalesOrderDTO);
        }
        
        [Route(CustomerSalesOrderRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerSalesOrderFilter CustomerSalesOrderFilter = new CustomerSalesOrderFilter();
            CustomerSalesOrderFilter = await CustomerSalesOrderService.ToFilter(CustomerSalesOrderFilter);
            CustomerSalesOrderFilter.Id = new IdFilter { In = Ids };
            CustomerSalesOrderFilter.Selects = CustomerSalesOrderSelect.Id;
            CustomerSalesOrderFilter.Skip = 0;
            CustomerSalesOrderFilter.Take = int.MaxValue;

            List<CustomerSalesOrder> CustomerSalesOrders = await CustomerSalesOrderService.List(CustomerSalesOrderFilter);
            CustomerSalesOrders = await CustomerSalesOrderService.BulkDelete(CustomerSalesOrders);
            if (CustomerSalesOrders.Any(x => !x.IsValidated))
                return BadRequest(CustomerSalesOrders.Where(x => !x.IsValidated));
            return true;
        }
        
        [Route(CustomerSalesOrderRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            ContractFilter ContractFilter = new ContractFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = ContractSelect.ALL
            };
            List<Contract> Contracts = await ContractService.List(ContractFilter);
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
            CustomerTypeFilter CustomerTypeFilter = new CustomerTypeFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CustomerTypeSelect.ALL
            };
            List<CustomerType> CustomerTypes = await CustomerTypeService.List(CustomerTypeFilter);
            DistrictFilter DeliveryDistrictFilter = new DistrictFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = DistrictSelect.ALL
            };
            List<District> DeliveryDistricts = await DistrictService.List(DeliveryDistrictFilter);
            NationFilter DeliveryNationFilter = new NationFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = NationSelect.ALL
            };
            List<Nation> DeliveryNations = await NationService.List(DeliveryNationFilter);
            ProvinceFilter DeliveryProvinceFilter = new ProvinceFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = ProvinceSelect.ALL
            };
            List<Province> DeliveryProvinces = await ProvinceService.List(DeliveryProvinceFilter);
            WardFilter DeliveryWardFilter = new WardFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = WardSelect.ALL
            };
            List<Ward> DeliveryWards = await WardService.List(DeliveryWardFilter);
            EditedPriceStatusFilter EditedPriceStatusFilter = new EditedPriceStatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = EditedPriceStatusSelect.ALL
            };
            List<EditedPriceStatus> EditedPriceStatuses = await EditedPriceStatusService.List(EditedPriceStatusFilter);
            DistrictFilter InvoiceDistrictFilter = new DistrictFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = DistrictSelect.ALL
            };
            List<District> InvoiceDistricts = await DistrictService.List(InvoiceDistrictFilter);
            NationFilter InvoiceNationFilter = new NationFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = NationSelect.ALL
            };
            List<Nation> InvoiceNations = await NationService.List(InvoiceNationFilter);
            ProvinceFilter InvoiceProvinceFilter = new ProvinceFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = ProvinceSelect.ALL
            };
            List<Province> InvoiceProvinces = await ProvinceService.List(InvoiceProvinceFilter);
            WardFilter InvoiceWardFilter = new WardFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = WardSelect.ALL
            };
            List<Ward> InvoiceWards = await WardService.List(InvoiceWardFilter);
            OpportunityFilter OpportunityFilter = new OpportunityFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = OpportunitySelect.ALL
            };
            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            OrderPaymentStatusFilter OrderPaymentStatusFilter = new OrderPaymentStatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = OrderPaymentStatusSelect.ALL
            };
            List<OrderPaymentStatus> OrderPaymentStatuses = await OrderPaymentStatusService.List(OrderPaymentStatusFilter);
            RequestStateFilter RequestStateFilter = new RequestStateFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = RequestStateSelect.ALL
            };
            List<RequestState> RequestStates = await RequestStateService.List(RequestStateFilter);
            AppUserFilter SalesEmployeeFilter = new AppUserFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = AppUserSelect.ALL
            };
            List<AppUser> SalesEmployees = await AppUserService.List(SalesEmployeeFilter);
            List<CustomerSalesOrder> CustomerSalesOrders = new List<CustomerSalesOrder>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(CustomerSalesOrders);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int CodeColumn = 1 + StartColumn;
                int CustomerTypeIdColumn = 2 + StartColumn;
                int CustomerIdColumn = 3 + StartColumn;
                int OpportunityIdColumn = 4 + StartColumn;
                int ContractIdColumn = 5 + StartColumn;
                int OrderPaymentStatusIdColumn = 6 + StartColumn;
                int RequestStateIdColumn = 7 + StartColumn;
                int EditedPriceStatusIdColumn = 8 + StartColumn;
                int ShippingNameColumn = 9 + StartColumn;
                int OrderDateColumn = 10 + StartColumn;
                int DeliveryDateColumn = 11 + StartColumn;
                int SalesEmployeeIdColumn = 12 + StartColumn;
                int NoteColumn = 13 + StartColumn;
                int InvoiceAddressColumn = 14 + StartColumn;
                int InvoiceNationIdColumn = 15 + StartColumn;
                int InvoiceProvinceIdColumn = 16 + StartColumn;
                int InvoiceDistrictIdColumn = 17 + StartColumn;
                int InvoiceWardIdColumn = 18 + StartColumn;
                int InvoiceZIPCodeColumn = 19 + StartColumn;
                int DeliveryAddressColumn = 20 + StartColumn;
                int DeliveryNationIdColumn = 21 + StartColumn;
                int DeliveryProvinceIdColumn = 22 + StartColumn;
                int DeliveryDistrictIdColumn = 23 + StartColumn;
                int DeliveryWardIdColumn = 24 + StartColumn;
                int DeliveryZIPCodeColumn = 25 + StartColumn;
                int SubTotalColumn = 26 + StartColumn;
                int GeneralDiscountPercentageColumn = 27 + StartColumn;
                int GeneralDiscountAmountColumn = 28 + StartColumn;
                int TotalTaxOtherColumn = 29 + StartColumn;
                int TotalTaxColumn = 30 + StartColumn;
                int TotalColumn = 31 + StartColumn;
                int CreatorIdColumn = 32 + StartColumn;
                int OrganizationIdColumn = 33 + StartColumn;
                int RowIdColumn = 37 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string CodeValue = worksheet.Cells[i + StartRow, CodeColumn].Value?.ToString();
                    string CustomerTypeIdValue = worksheet.Cells[i + StartRow, CustomerTypeIdColumn].Value?.ToString();
                    string CustomerIdValue = worksheet.Cells[i + StartRow, CustomerIdColumn].Value?.ToString();
                    string OpportunityIdValue = worksheet.Cells[i + StartRow, OpportunityIdColumn].Value?.ToString();
                    string ContractIdValue = worksheet.Cells[i + StartRow, ContractIdColumn].Value?.ToString();
                    string OrderPaymentStatusIdValue = worksheet.Cells[i + StartRow, OrderPaymentStatusIdColumn].Value?.ToString();
                    string RequestStateIdValue = worksheet.Cells[i + StartRow, RequestStateIdColumn].Value?.ToString();
                    string EditedPriceStatusIdValue = worksheet.Cells[i + StartRow, EditedPriceStatusIdColumn].Value?.ToString();
                    string ShippingNameValue = worksheet.Cells[i + StartRow, ShippingNameColumn].Value?.ToString();
                    string OrderDateValue = worksheet.Cells[i + StartRow, OrderDateColumn].Value?.ToString();
                    string DeliveryDateValue = worksheet.Cells[i + StartRow, DeliveryDateColumn].Value?.ToString();
                    string SalesEmployeeIdValue = worksheet.Cells[i + StartRow, SalesEmployeeIdColumn].Value?.ToString();
                    string NoteValue = worksheet.Cells[i + StartRow, NoteColumn].Value?.ToString();
                    string InvoiceAddressValue = worksheet.Cells[i + StartRow, InvoiceAddressColumn].Value?.ToString();
                    string InvoiceNationIdValue = worksheet.Cells[i + StartRow, InvoiceNationIdColumn].Value?.ToString();
                    string InvoiceProvinceIdValue = worksheet.Cells[i + StartRow, InvoiceProvinceIdColumn].Value?.ToString();
                    string InvoiceDistrictIdValue = worksheet.Cells[i + StartRow, InvoiceDistrictIdColumn].Value?.ToString();
                    string InvoiceWardIdValue = worksheet.Cells[i + StartRow, InvoiceWardIdColumn].Value?.ToString();
                    string InvoiceZIPCodeValue = worksheet.Cells[i + StartRow, InvoiceZIPCodeColumn].Value?.ToString();
                    string DeliveryAddressValue = worksheet.Cells[i + StartRow, DeliveryAddressColumn].Value?.ToString();
                    string DeliveryNationIdValue = worksheet.Cells[i + StartRow, DeliveryNationIdColumn].Value?.ToString();
                    string DeliveryProvinceIdValue = worksheet.Cells[i + StartRow, DeliveryProvinceIdColumn].Value?.ToString();
                    string DeliveryDistrictIdValue = worksheet.Cells[i + StartRow, DeliveryDistrictIdColumn].Value?.ToString();
                    string DeliveryWardIdValue = worksheet.Cells[i + StartRow, DeliveryWardIdColumn].Value?.ToString();
                    string DeliveryZIPCodeValue = worksheet.Cells[i + StartRow, DeliveryZIPCodeColumn].Value?.ToString();
                    string SubTotalValue = worksheet.Cells[i + StartRow, SubTotalColumn].Value?.ToString();
                    string GeneralDiscountPercentageValue = worksheet.Cells[i + StartRow, GeneralDiscountPercentageColumn].Value?.ToString();
                    string GeneralDiscountAmountValue = worksheet.Cells[i + StartRow, GeneralDiscountAmountColumn].Value?.ToString();
                    string TotalTaxOtherValue = worksheet.Cells[i + StartRow, TotalTaxOtherColumn].Value?.ToString();
                    string TotalTaxValue = worksheet.Cells[i + StartRow, TotalTaxColumn].Value?.ToString();
                    string TotalValue = worksheet.Cells[i + StartRow, TotalColumn].Value?.ToString();
                    string CreatorIdValue = worksheet.Cells[i + StartRow, CreatorIdColumn].Value?.ToString();
                    string OrganizationIdValue = worksheet.Cells[i + StartRow, OrganizationIdColumn].Value?.ToString();
                    string RowIdValue = worksheet.Cells[i + StartRow, RowIdColumn].Value?.ToString();
                    
                    CustomerSalesOrder CustomerSalesOrder = new CustomerSalesOrder();
                    CustomerSalesOrder.Code = CodeValue;
                    CustomerSalesOrder.ShippingName = ShippingNameValue;
                    CustomerSalesOrder.OrderDate = DateTime.TryParse(OrderDateValue, out DateTime OrderDate) ? OrderDate : DateTime.Now;
                    CustomerSalesOrder.DeliveryDate = DateTime.TryParse(DeliveryDateValue, out DateTime DeliveryDate) ? DeliveryDate : DateTime.Now;
                    CustomerSalesOrder.Note = NoteValue;
                    CustomerSalesOrder.InvoiceAddress = InvoiceAddressValue;
                    CustomerSalesOrder.InvoiceZIPCode = InvoiceZIPCodeValue;
                    CustomerSalesOrder.DeliveryAddress = DeliveryAddressValue;
                    CustomerSalesOrder.DeliveryZIPCode = DeliveryZIPCodeValue;
                    CustomerSalesOrder.SubTotal = decimal.TryParse(SubTotalValue, out decimal SubTotal) ? SubTotal : 0;
                    CustomerSalesOrder.GeneralDiscountPercentage = decimal.TryParse(GeneralDiscountPercentageValue, out decimal GeneralDiscountPercentage) ? GeneralDiscountPercentage : 0;
                    CustomerSalesOrder.GeneralDiscountAmount = decimal.TryParse(GeneralDiscountAmountValue, out decimal GeneralDiscountAmount) ? GeneralDiscountAmount : 0;
                    CustomerSalesOrder.TotalTaxOther = decimal.TryParse(TotalTaxOtherValue, out decimal TotalTaxOther) ? TotalTaxOther : 0;
                    CustomerSalesOrder.TotalTax = decimal.TryParse(TotalTaxValue, out decimal TotalTax) ? TotalTax : 0;
                    CustomerSalesOrder.Total = decimal.TryParse(TotalValue, out decimal Total) ? Total : 0;
                    Contract Contract = Contracts.Where(x => x.Id.ToString() == ContractIdValue).FirstOrDefault();
                    CustomerSalesOrder.ContractId = Contract == null ? 0 : Contract.Id;
                    CustomerSalesOrder.Contract = Contract;
                    AppUser Creator = Creators.Where(x => x.Id.ToString() == CreatorIdValue).FirstOrDefault();
                    CustomerSalesOrder.CreatorId = Creator == null ? 0 : Creator.Id;
                    CustomerSalesOrder.Creator = Creator;
                    Customer Customer = Customers.Where(x => x.Id.ToString() == CustomerIdValue).FirstOrDefault();
                    CustomerSalesOrder.CustomerId = Customer == null ? 0 : Customer.Id;
                    CustomerSalesOrder.Customer = Customer;
                    CustomerType CustomerType = CustomerTypes.Where(x => x.Id.ToString() == CustomerTypeIdValue).FirstOrDefault();
                    CustomerSalesOrder.CustomerTypeId = CustomerType == null ? 0 : CustomerType.Id;
                    CustomerSalesOrder.CustomerType = CustomerType;
                    District DeliveryDistrict = DeliveryDistricts.Where(x => x.Id.ToString() == DeliveryDistrictIdValue).FirstOrDefault();
                    CustomerSalesOrder.DeliveryDistrictId = DeliveryDistrict == null ? 0 : DeliveryDistrict.Id;
                    CustomerSalesOrder.DeliveryDistrict = DeliveryDistrict;
                    Nation DeliveryNation = DeliveryNations.Where(x => x.Id.ToString() == DeliveryNationIdValue).FirstOrDefault();
                    CustomerSalesOrder.DeliveryNationId = DeliveryNation == null ? 0 : DeliveryNation.Id;
                    CustomerSalesOrder.DeliveryNation = DeliveryNation;
                    Province DeliveryProvince = DeliveryProvinces.Where(x => x.Id.ToString() == DeliveryProvinceIdValue).FirstOrDefault();
                    CustomerSalesOrder.DeliveryProvinceId = DeliveryProvince == null ? 0 : DeliveryProvince.Id;
                    CustomerSalesOrder.DeliveryProvince = DeliveryProvince;
                    Ward DeliveryWard = DeliveryWards.Where(x => x.Id.ToString() == DeliveryWardIdValue).FirstOrDefault();
                    CustomerSalesOrder.DeliveryWardId = DeliveryWard == null ? 0 : DeliveryWard.Id;
                    CustomerSalesOrder.DeliveryWard = DeliveryWard;
                    EditedPriceStatus EditedPriceStatus = EditedPriceStatuses.Where(x => x.Id.ToString() == EditedPriceStatusIdValue).FirstOrDefault();
                    CustomerSalesOrder.EditedPriceStatusId = EditedPriceStatus == null ? 0 : EditedPriceStatus.Id;
                    CustomerSalesOrder.EditedPriceStatus = EditedPriceStatus;
                    District InvoiceDistrict = InvoiceDistricts.Where(x => x.Id.ToString() == InvoiceDistrictIdValue).FirstOrDefault();
                    CustomerSalesOrder.InvoiceDistrictId = InvoiceDistrict == null ? 0 : InvoiceDistrict.Id;
                    CustomerSalesOrder.InvoiceDistrict = InvoiceDistrict;
                    Nation InvoiceNation = InvoiceNations.Where(x => x.Id.ToString() == InvoiceNationIdValue).FirstOrDefault();
                    CustomerSalesOrder.InvoiceNationId = InvoiceNation == null ? 0 : InvoiceNation.Id;
                    CustomerSalesOrder.InvoiceNation = InvoiceNation;
                    Province InvoiceProvince = InvoiceProvinces.Where(x => x.Id.ToString() == InvoiceProvinceIdValue).FirstOrDefault();
                    CustomerSalesOrder.InvoiceProvinceId = InvoiceProvince == null ? 0 : InvoiceProvince.Id;
                    CustomerSalesOrder.InvoiceProvince = InvoiceProvince;
                    Ward InvoiceWard = InvoiceWards.Where(x => x.Id.ToString() == InvoiceWardIdValue).FirstOrDefault();
                    CustomerSalesOrder.InvoiceWardId = InvoiceWard == null ? 0 : InvoiceWard.Id;
                    CustomerSalesOrder.InvoiceWard = InvoiceWard;
                    Opportunity Opportunity = Opportunities.Where(x => x.Id.ToString() == OpportunityIdValue).FirstOrDefault();
                    CustomerSalesOrder.OpportunityId = Opportunity == null ? 0 : Opportunity.Id;
                    CustomerSalesOrder.Opportunity = Opportunity;
                    OrderPaymentStatus OrderPaymentStatus = OrderPaymentStatuses.Where(x => x.Id.ToString() == OrderPaymentStatusIdValue).FirstOrDefault();
                    CustomerSalesOrder.OrderPaymentStatusId = OrderPaymentStatus == null ? 0 : OrderPaymentStatus.Id;
                    CustomerSalesOrder.OrderPaymentStatus = OrderPaymentStatus;
                    RequestState RequestState = RequestStates.Where(x => x.Id.ToString() == RequestStateIdValue).FirstOrDefault();
                    CustomerSalesOrder.RequestStateId = RequestState == null ? 0 : RequestState.Id;
                    CustomerSalesOrder.RequestState = RequestState;
                    AppUser SalesEmployee = SalesEmployees.Where(x => x.Id.ToString() == SalesEmployeeIdValue).FirstOrDefault();
                    CustomerSalesOrder.SalesEmployeeId = SalesEmployee == null ? 0 : SalesEmployee.Id;
                    CustomerSalesOrder.SalesEmployee = SalesEmployee;
                    
                    CustomerSalesOrders.Add(CustomerSalesOrder);
                }
            }
            CustomerSalesOrders = await CustomerSalesOrderService.Import(CustomerSalesOrders);
            if (CustomerSalesOrders.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < CustomerSalesOrders.Count; i++)
                {
                    CustomerSalesOrder CustomerSalesOrder = CustomerSalesOrders[i];
                    if (!CustomerSalesOrder.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.Id)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.Id)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.Code)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.Code)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.CustomerTypeId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.CustomerTypeId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.CustomerId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.CustomerId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.OpportunityId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.OpportunityId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.ContractId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.ContractId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.OrderPaymentStatusId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.OrderPaymentStatusId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.RequestStateId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.RequestStateId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.EditedPriceStatusId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.EditedPriceStatusId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.ShippingName)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.ShippingName)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.OrderDate)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.OrderDate)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.DeliveryDate)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.DeliveryDate)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.SalesEmployeeId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.SalesEmployeeId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.Note)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.Note)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.InvoiceAddress)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.InvoiceAddress)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.InvoiceNationId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.InvoiceNationId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.InvoiceProvinceId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.InvoiceProvinceId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.InvoiceDistrictId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.InvoiceDistrictId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.InvoiceWardId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.InvoiceWardId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.InvoiceZIPCode)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.InvoiceZIPCode)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.DeliveryAddress)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.DeliveryAddress)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.DeliveryNationId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.DeliveryNationId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.DeliveryProvinceId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.DeliveryProvinceId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.DeliveryDistrictId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.DeliveryDistrictId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.DeliveryWardId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.DeliveryWardId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.DeliveryZIPCode)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.DeliveryZIPCode)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.SubTotal)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.SubTotal)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.GeneralDiscountPercentage)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.GeneralDiscountPercentage)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.GeneralDiscountAmount)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.GeneralDiscountAmount)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.TotalTaxOther)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.TotalTaxOther)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.TotalTax)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.TotalTax)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.Total)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.Total)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.CreatorId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.CreatorId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.OrganizationId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.OrganizationId)];
                        if (CustomerSalesOrder.Errors.ContainsKey(nameof(CustomerSalesOrder.RowId)))
                            Error += CustomerSalesOrder.Errors[nameof(CustomerSalesOrder.RowId)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(CustomerSalesOrderRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] CustomerSalesOrder_CustomerSalesOrderFilterDTO CustomerSalesOrder_CustomerSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region CustomerSalesOrder
                var CustomerSalesOrderFilter = ConvertFilterDTOToFilterEntity(CustomerSalesOrder_CustomerSalesOrderFilterDTO);
                CustomerSalesOrderFilter.Skip = 0;
                CustomerSalesOrderFilter.Take = int.MaxValue;
                CustomerSalesOrderFilter = await CustomerSalesOrderService.ToFilter(CustomerSalesOrderFilter);
                List<CustomerSalesOrder> CustomerSalesOrders = await CustomerSalesOrderService.List(CustomerSalesOrderFilter);

                var CustomerSalesOrderHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "CustomerTypeId",
                        "CustomerId",
                        "OpportunityId",
                        "ContractId",
                        "OrderPaymentStatusId",
                        "RequestStateId",
                        "EditedPriceStatusId",
                        "ShippingName",
                        "OrderDate",
                        "DeliveryDate",
                        "SalesEmployeeId",
                        "Note",
                        "InvoiceAddress",
                        "InvoiceNationId",
                        "InvoiceProvinceId",
                        "InvoiceDistrictId",
                        "InvoiceWardId",
                        "InvoiceZIPCode",
                        "DeliveryAddress",
                        "DeliveryNationId",
                        "DeliveryProvinceId",
                        "DeliveryDistrictId",
                        "DeliveryWardId",
                        "DeliveryZIPCode",
                        "SubTotal",
                        "GeneralDiscountPercentage",
                        "GeneralDiscountAmount",
                        "TotalTaxOther",
                        "TotalTax",
                        "Total",
                        "CreatorId",
                        "OrganizationId",
                        "RowId",
                    }
                };
                List<object[]> CustomerSalesOrderData = new List<object[]>();
                for (int i = 0; i < CustomerSalesOrders.Count; i++)
                {
                    var CustomerSalesOrder = CustomerSalesOrders[i];
                    CustomerSalesOrderData.Add(new Object[]
                    {
                        CustomerSalesOrder.Id,
                        CustomerSalesOrder.Code,
                        CustomerSalesOrder.CustomerTypeId,
                        CustomerSalesOrder.CustomerId,
                        CustomerSalesOrder.OpportunityId,
                        CustomerSalesOrder.ContractId,
                        CustomerSalesOrder.OrderPaymentStatusId,
                        CustomerSalesOrder.RequestStateId,
                        CustomerSalesOrder.EditedPriceStatusId,
                        CustomerSalesOrder.ShippingName,
                        CustomerSalesOrder.OrderDate,
                        CustomerSalesOrder.DeliveryDate,
                        CustomerSalesOrder.SalesEmployeeId,
                        CustomerSalesOrder.Note,
                        CustomerSalesOrder.InvoiceAddress,
                        CustomerSalesOrder.InvoiceNationId,
                        CustomerSalesOrder.InvoiceProvinceId,
                        CustomerSalesOrder.InvoiceDistrictId,
                        CustomerSalesOrder.InvoiceWardId,
                        CustomerSalesOrder.InvoiceZIPCode,
                        CustomerSalesOrder.DeliveryAddress,
                        CustomerSalesOrder.DeliveryNationId,
                        CustomerSalesOrder.DeliveryProvinceId,
                        CustomerSalesOrder.DeliveryDistrictId,
                        CustomerSalesOrder.DeliveryWardId,
                        CustomerSalesOrder.DeliveryZIPCode,
                        CustomerSalesOrder.SubTotal,
                        CustomerSalesOrder.GeneralDiscountPercentage,
                        CustomerSalesOrder.GeneralDiscountAmount,
                        CustomerSalesOrder.TotalTaxOther,
                        CustomerSalesOrder.TotalTax,
                        CustomerSalesOrder.Total,
                        CustomerSalesOrder.CreatorId,
                        CustomerSalesOrder.OrganizationId,
                        CustomerSalesOrder.RowId,
                    });
                }
                excel.GenerateWorksheet("CustomerSalesOrder", CustomerSalesOrderHeaders, CustomerSalesOrderData);
                #endregion
                
                #region Contract
                var ContractFilter = new ContractFilter();
                ContractFilter.Selects = ContractSelect.ALL;
                ContractFilter.OrderBy = ContractOrder.Id;
                ContractFilter.OrderType = OrderType.ASC;
                ContractFilter.Skip = 0;
                ContractFilter.Take = int.MaxValue;
                List<Contract> Contracts = await ContractService.List(ContractFilter);

                var ContractHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "TotalValue",
                        "ValidityDate",
                        "ExpirationDate",
                        "DeliveryUnit",
                        "InvoiceAddress",
                        "InvoiceZipCode",
                        "ReceiveAddress",
                        "ReceiveZipCode",
                        "TermAndCondition",
                        "InvoiceNationId",
                        "InvoiceProvinceId",
                        "InvoiceDistrictId",
                        "ReceiveNationId",
                        "ReceiveProvinceId",
                        "ReceiveDistrictId",
                        "ContractTypeId",
                        "CompanyId",
                        "OpportunityId",
                        "OrganizationId",
                        "AppUserId",
                        "ContractStatusId",
                        "CreatorId",
                        "CustomerId",
                        "CurrencyId",
                        "PaymentStatusId",
                        "EntityReferenceId",
                        "SubTotal",
                        "GeneralDiscountPercentage",
                        "GeneralDiscountAmount",
                        "TotalTaxAmountOther",
                        "TotalTaxAmount",
                        "Total",
                    }
                };
                List<object[]> ContractData = new List<object[]>();
                for (int i = 0; i < Contracts.Count; i++)
                {
                    var Contract = Contracts[i];
                    ContractData.Add(new Object[]
                    {
                        Contract.Id,
                        Contract.Code,
                        Contract.Name,
                        Contract.TotalValue,
                        Contract.ValidityDate,
                        Contract.ExpirationDate,
                        Contract.DeliveryUnit,
                        Contract.InvoiceAddress,
                        Contract.InvoiceZipCode,
                        Contract.ReceiveAddress,
                        Contract.ReceiveZipCode,
                        Contract.TermAndCondition,
                        Contract.InvoiceNationId,
                        Contract.InvoiceProvinceId,
                        Contract.InvoiceDistrictId,
                        Contract.ReceiveNationId,
                        Contract.ReceiveProvinceId,
                        Contract.ReceiveDistrictId,
                        Contract.ContractTypeId,
                        Contract.CompanyId,
                        Contract.OpportunityId,
                        Contract.OrganizationId,
                        Contract.AppUserId,
                        Contract.ContractStatusId,
                        Contract.CreatorId,
                        Contract.CustomerId,
                        Contract.CurrencyId,
                        Contract.PaymentStatusId,
                        Contract.SubTotal,
                        Contract.GeneralDiscountPercentage,
                        Contract.GeneralDiscountAmount,
                        Contract.TotalTaxAmountOther,
                        Contract.TotalTaxAmount,
                        Contract.Total,
                    });
                }
                excel.GenerateWorksheet("Contract", ContractHeaders, ContractData);
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
                        "Name",
                        "Phone",
                        "Address",
                        "NationId",
                        "ProvinceId",
                        "DistrictId",
                        "WardId",
                        "CustomerTypeId",
                        "Birthday",
                        "Email",
                        "ProfessionId",
                        "CustomerResourceId",
                        "SexId",
                        "StatusId",
                        "CompanyId",
                        "ParentCompanyId",
                        "TaxCode",
                        "Fax",
                        "Website",
                        "NumberOfEmployee",
                        "BusinessTypeId",
                        "Investment",
                        "RevenueAnnual",
                        "IsSupplier",
                        "Descreption",
                        "Used",
                        "RowId",
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
                        Customer.Address,
                        Customer.NationId,
                        Customer.ProvinceId,
                        Customer.DistrictId,
                        Customer.WardId,
                        Customer.CustomerTypeId,
                        Customer.Birthday,
                        Customer.Email,
                        Customer.ProfessionId,
                        Customer.CustomerResourceId,
                        Customer.SexId,
                        Customer.StatusId,
                        Customer.CompanyId,
                        Customer.ParentCompanyId,
                        Customer.TaxCode,
                        Customer.Fax,
                        Customer.Website,
                        Customer.NumberOfEmployee,
                        Customer.BusinessTypeId,
                        Customer.Investment,
                        Customer.RevenueAnnual,
                        Customer.IsSupplier,
                        Customer.Descreption,
                        Customer.Used,
                        Customer.RowId,
                    });
                }
                excel.GenerateWorksheet("Customer", CustomerHeaders, CustomerData);
                #endregion
                #region CustomerType
                var CustomerTypeFilter = new CustomerTypeFilter();
                CustomerTypeFilter.Selects = CustomerTypeSelect.ALL;
                CustomerTypeFilter.OrderBy = CustomerTypeOrder.Id;
                CustomerTypeFilter.OrderType = OrderType.ASC;
                CustomerTypeFilter.Skip = 0;
                CustomerTypeFilter.Take = int.MaxValue;
                List<CustomerType> CustomerTypes = await CustomerTypeService.List(CustomerTypeFilter);

                var CustomerTypeHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "Description",
                        "StatusId",
                        "Used",
                        "RowId",
                    }
                };
                List<object[]> CustomerTypeData = new List<object[]>();
                for (int i = 0; i < CustomerTypes.Count; i++)
                {
                    var CustomerType = CustomerTypes[i];
                    CustomerTypeData.Add(new Object[]
                    {
                        CustomerType.Id,
                        CustomerType.Code,
                        CustomerType.Name,
                    });
                }
                excel.GenerateWorksheet("CustomerType", CustomerTypeHeaders, CustomerTypeData);
                #endregion
                #region District
                var DistrictFilter = new DistrictFilter();
                DistrictFilter.Selects = DistrictSelect.ALL;
                DistrictFilter.OrderBy = DistrictOrder.Id;
                DistrictFilter.OrderType = OrderType.ASC;
                DistrictFilter.Skip = 0;
                DistrictFilter.Take = int.MaxValue;
                List<District> Districts = await DistrictService.List(DistrictFilter);

                var DistrictHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "Priority",
                        "ProvinceId",
                        "StatusId",
                        "RowId",
                        "Used",
                    }
                };
                List<object[]> DistrictData = new List<object[]>();
                for (int i = 0; i < Districts.Count; i++)
                {
                    var District = Districts[i];
                    DistrictData.Add(new Object[]
                    {
                        District.Id,
                        District.Code,
                        District.Name,
                        District.Priority,
                        District.ProvinceId,
                        District.StatusId,
                        District.RowId,
                        District.Used,
                    });
                }
                excel.GenerateWorksheet("District", DistrictHeaders, DistrictData);
                #endregion
                #region Nation
                var NationFilter = new NationFilter();
                NationFilter.Selects = NationSelect.ALL;
                NationFilter.OrderBy = NationOrder.Id;
                NationFilter.OrderType = OrderType.ASC;
                NationFilter.Skip = 0;
                NationFilter.Take = int.MaxValue;
                List<Nation> Nations = await NationService.List(NationFilter);

                var NationHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "Priority",
                        "StatusId",
                        "Used",
                        "RowId",
                    }
                };
                List<object[]> NationData = new List<object[]>();
                for (int i = 0; i < Nations.Count; i++)
                {
                    var Nation = Nations[i];
                    NationData.Add(new Object[]
                    {
                        Nation.Id,
                        Nation.Code,
                        Nation.Name,
                        Nation.Priority,
                        Nation.StatusId,
                        Nation.Used,
                        Nation.RowId,
                    });
                }
                excel.GenerateWorksheet("Nation", NationHeaders, NationData);
                #endregion
                #region Province
                var ProvinceFilter = new ProvinceFilter();
                ProvinceFilter.Selects = ProvinceSelect.ALL;
                ProvinceFilter.OrderBy = ProvinceOrder.Id;
                ProvinceFilter.OrderType = OrderType.ASC;
                ProvinceFilter.Skip = 0;
                ProvinceFilter.Take = int.MaxValue;
                List<Province> Provinces = await ProvinceService.List(ProvinceFilter);

                var ProvinceHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "Priority",
                        "StatusId",
                        "RowId",
                        "Used",
                    }
                };
                List<object[]> ProvinceData = new List<object[]>();
                for (int i = 0; i < Provinces.Count; i++)
                {
                    var Province = Provinces[i];
                    ProvinceData.Add(new Object[]
                    {
                        Province.Id,
                        Province.Code,
                        Province.Name,
                        Province.Priority,
                        Province.StatusId,
                        Province.RowId,
                        Province.Used,
                    });
                }
                excel.GenerateWorksheet("Province", ProvinceHeaders, ProvinceData);
                #endregion
                #region Ward
                var WardFilter = new WardFilter();
                WardFilter.Selects = WardSelect.ALL;
                WardFilter.OrderBy = WardOrder.Id;
                WardFilter.OrderType = OrderType.ASC;
                WardFilter.Skip = 0;
                WardFilter.Take = int.MaxValue;
                List<Ward> Wards = await WardService.List(WardFilter);

                var WardHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "Priority",
                        "DistrictId",
                        "StatusId",
                        "RowId",
                        "Used",
                    }
                };
                List<object[]> WardData = new List<object[]>();
                for (int i = 0; i < Wards.Count; i++)
                {
                    var Ward = Wards[i];
                    WardData.Add(new Object[]
                    {
                        Ward.Id,
                        Ward.Code,
                        Ward.Name,
                        Ward.Priority,
                        Ward.DistrictId,
                        Ward.StatusId,
                        Ward.RowId,
                        Ward.Used,
                    });
                }
                excel.GenerateWorksheet("Ward", WardHeaders, WardData);
                #endregion
                #region EditedPriceStatus
                var EditedPriceStatusFilter = new EditedPriceStatusFilter();
                EditedPriceStatusFilter.Selects = EditedPriceStatusSelect.ALL;
                EditedPriceStatusFilter.OrderBy = EditedPriceStatusOrder.Id;
                EditedPriceStatusFilter.OrderType = OrderType.ASC;
                EditedPriceStatusFilter.Skip = 0;
                EditedPriceStatusFilter.Take = int.MaxValue;
                List<EditedPriceStatus> EditedPriceStatuses = await EditedPriceStatusService.List(EditedPriceStatusFilter);

                var EditedPriceStatusHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> EditedPriceStatusData = new List<object[]>();
                for (int i = 0; i < EditedPriceStatuses.Count; i++)
                {
                    var EditedPriceStatus = EditedPriceStatuses[i];
                    EditedPriceStatusData.Add(new Object[]
                    {
                        EditedPriceStatus.Id,
                        EditedPriceStatus.Code,
                        EditedPriceStatus.Name,
                    });
                }
                excel.GenerateWorksheet("EditedPriceStatus", EditedPriceStatusHeaders, EditedPriceStatusData);
                #endregion
                #region Opportunity
                var OpportunityFilter = new OpportunityFilter();
                OpportunityFilter.Selects = OpportunitySelect.ALL;
                OpportunityFilter.OrderBy = OpportunityOrder.Id;
                OpportunityFilter.OrderType = OrderType.ASC;
                OpportunityFilter.Skip = 0;
                OpportunityFilter.Take = int.MaxValue;
                List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);

                var OpportunityHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Name",
                        "CompanyId",
                        "CustomerLeadId",
                        "ClosingDate",
                        "SaleStageId",
                        "ProbabilityId",
                        "PotentialResultId",
                        "LeadSourceId",
                        "AppUserId",
                        "CurrencyId",
                        "Amount",
                        "ForecastAmount",
                        "Description",
                        "CreatorId",
                        "RefuseReciveSMS",
                        "RefuseReciveEmail",
                        "OpportunityResultTypeId",
                        "CreatorId",
                    }
                };
                List<object[]> OpportunityData = new List<object[]>();
                for (int i = 0; i < Opportunities.Count; i++)
                {
                    var Opportunity = Opportunities[i];
                    OpportunityData.Add(new Object[]
                    {
                        Opportunity.Id,
                        Opportunity.Name,
                        Opportunity.CompanyId,
                        Opportunity.CustomerLeadId,
                        Opportunity.ClosingDate,
                        Opportunity.SaleStageId,
                        Opportunity.ProbabilityId,
                        Opportunity.PotentialResultId,
                        Opportunity.LeadSourceId,
                        Opportunity.AppUserId,
                        Opportunity.CurrencyId,
                        Opportunity.Amount,
                        Opportunity.ForecastAmount,
                        Opportunity.Description,
                        Opportunity.RefuseReciveSMS,
                        Opportunity.RefuseReciveEmail,
                        Opportunity.OpportunityResultTypeId,
                        Opportunity.CreatorId,
                    });
                }
                excel.GenerateWorksheet("Opportunity", OpportunityHeaders, OpportunityData);
                #endregion
                #region OrderPaymentStatus
                var OrderPaymentStatusFilter = new OrderPaymentStatusFilter();
                OrderPaymentStatusFilter.Selects = OrderPaymentStatusSelect.ALL;
                OrderPaymentStatusFilter.OrderBy = OrderPaymentStatusOrder.Id;
                OrderPaymentStatusFilter.OrderType = OrderType.ASC;
                OrderPaymentStatusFilter.Skip = 0;
                OrderPaymentStatusFilter.Take = int.MaxValue;
                List<OrderPaymentStatus> OrderPaymentStatuses = await OrderPaymentStatusService.List(OrderPaymentStatusFilter);

                var OrderPaymentStatusHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> OrderPaymentStatusData = new List<object[]>();
                for (int i = 0; i < OrderPaymentStatuses.Count; i++)
                {
                    var OrderPaymentStatus = OrderPaymentStatuses[i];
                    OrderPaymentStatusData.Add(new Object[]
                    {
                        OrderPaymentStatus.Id,
                        OrderPaymentStatus.Code,
                        OrderPaymentStatus.Name,
                    });
                }
                excel.GenerateWorksheet("OrderPaymentStatus", OrderPaymentStatusHeaders, OrderPaymentStatusData);
                #endregion
                #region Organization
                var OrganizationFilter = new OrganizationFilter();
                OrganizationFilter.Selects = OrganizationSelect.ALL;
                OrganizationFilter.OrderBy = OrganizationOrder.Id;
                OrganizationFilter.OrderType = OrderType.ASC;
                OrganizationFilter.Skip = 0;
                OrganizationFilter.Take = int.MaxValue;
                List<Organization> Organizations = await OrganizationService.List(OrganizationFilter);

                var OrganizationHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "ParentId",
                        "Path",
                        "Level",
                        "StatusId",
                        "Phone",
                        "Email",
                        "Address",
                        "RowId",
                        "Used",
                    }
                };
                List<object[]> OrganizationData = new List<object[]>();
                for (int i = 0; i < Organizations.Count; i++)
                {
                    var Organization = Organizations[i];
                    OrganizationData.Add(new Object[]
                    {
                        Organization.Id,
                        Organization.Code,
                        Organization.Name,
                        Organization.ParentId,
                        Organization.Path,
                        Organization.Level,
                        Organization.StatusId,
                        Organization.Phone,
                        Organization.Email,
                        Organization.Address,
                        Organization.RowId,
                        Organization.Used,
                    });
                }
                excel.GenerateWorksheet("Organization", OrganizationHeaders, OrganizationData);
                #endregion
                #region RequestState
                var RequestStateFilter = new RequestStateFilter();
                RequestStateFilter.Selects = RequestStateSelect.ALL;
                RequestStateFilter.OrderBy = RequestStateOrder.Id;
                RequestStateFilter.OrderType = OrderType.ASC;
                RequestStateFilter.Skip = 0;
                RequestStateFilter.Take = int.MaxValue;
                List<RequestState> RequestStates = await RequestStateService.List(RequestStateFilter);

                var RequestStateHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> RequestStateData = new List<object[]>();
                for (int i = 0; i < RequestStates.Count; i++)
                {
                    var RequestState = RequestStates[i];
                    RequestStateData.Add(new Object[]
                    {
                        RequestState.Id,
                        RequestState.Code,
                        RequestState.Name,
                    });
                }
                excel.GenerateWorksheet("RequestState", RequestStateHeaders, RequestStateData);
                #endregion
                #region CustomerSalesOrderContent
                var CustomerSalesOrderContentFilter = new CustomerSalesOrderContentFilter();
                CustomerSalesOrderContentFilter.Selects = CustomerSalesOrderContentSelect.ALL;
                CustomerSalesOrderContentFilter.OrderBy = CustomerSalesOrderContentOrder.Id;
                CustomerSalesOrderContentFilter.OrderType = OrderType.ASC;
                CustomerSalesOrderContentFilter.Skip = 0;
                CustomerSalesOrderContentFilter.Take = int.MaxValue;
                List<CustomerSalesOrderContent> CustomerSalesOrderContents = await CustomerSalesOrderContentService.List(CustomerSalesOrderContentFilter);

                var CustomerSalesOrderContentHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "CustomerSalesOrderId",
                        "ItemId",
                        "UnitOfMeasureId",
                        "Quantity",
                        "RequestedQuantity",
                        "PrimaryUnitOfMeasureId",
                        "SalePrice",
                        "PrimaryPrice",
                        "DiscountPercentage",
                        "DiscountAmount",
                        "GeneralDiscountPercentage",
                        "GeneralDiscountAmount",
                        "TaxPercentage",
                        "TaxAmount",
                        "TaxPercentageOther",
                        "TaxAmountOther",
                        "Amount",
                        "Factor",
                        "EditedPriceStatusId",
                    }
                };
                List<object[]> CustomerSalesOrderContentData = new List<object[]>();
                for (int i = 0; i < CustomerSalesOrderContents.Count; i++)
                {
                    var CustomerSalesOrderContent = CustomerSalesOrderContents[i];
                    CustomerSalesOrderContentData.Add(new Object[]
                    {
                        CustomerSalesOrderContent.Id,
                        CustomerSalesOrderContent.CustomerSalesOrderId,
                        CustomerSalesOrderContent.ItemId,
                        CustomerSalesOrderContent.UnitOfMeasureId,
                        CustomerSalesOrderContent.Quantity,
                        CustomerSalesOrderContent.RequestedQuantity,
                        CustomerSalesOrderContent.PrimaryUnitOfMeasureId,
                        CustomerSalesOrderContent.SalePrice,
                        CustomerSalesOrderContent.PrimaryPrice,
                        CustomerSalesOrderContent.DiscountPercentage,
                        CustomerSalesOrderContent.DiscountAmount,
                        CustomerSalesOrderContent.GeneralDiscountPercentage,
                        CustomerSalesOrderContent.GeneralDiscountAmount,
                        CustomerSalesOrderContent.TaxPercentage,
                        CustomerSalesOrderContent.TaxAmount,
                        CustomerSalesOrderContent.TaxPercentageOther,
                        CustomerSalesOrderContent.TaxAmountOther,
                        CustomerSalesOrderContent.Amount,
                        CustomerSalesOrderContent.Factor,
                        CustomerSalesOrderContent.EditedPriceStatusId,
                    });
                }
                excel.GenerateWorksheet("CustomerSalesOrderContent", CustomerSalesOrderContentHeaders, CustomerSalesOrderContentData);
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
                #region UnitOfMeasure
                var UnitOfMeasureFilter = new UnitOfMeasureFilter();
                UnitOfMeasureFilter.Selects = UnitOfMeasureSelect.ALL;
                UnitOfMeasureFilter.OrderBy = UnitOfMeasureOrder.Id;
                UnitOfMeasureFilter.OrderType = OrderType.ASC;
                UnitOfMeasureFilter.Skip = 0;
                UnitOfMeasureFilter.Take = int.MaxValue;
                List<UnitOfMeasure> UnitOfMeasures = await UnitOfMeasureService.List(UnitOfMeasureFilter);

                var UnitOfMeasureHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "Description",
                        "StatusId",
                        "Used",
                        "RowId",
                    }
                };
                List<object[]> UnitOfMeasureData = new List<object[]>();
                for (int i = 0; i < UnitOfMeasures.Count; i++)
                {
                    var UnitOfMeasure = UnitOfMeasures[i];
                    UnitOfMeasureData.Add(new Object[]
                    {
                        UnitOfMeasure.Id,
                        UnitOfMeasure.Code,
                        UnitOfMeasure.Name,
                        UnitOfMeasure.Description,
                        UnitOfMeasure.StatusId,
                        UnitOfMeasure.Used,
                        UnitOfMeasure.RowId,
                    });
                }
                excel.GenerateWorksheet("UnitOfMeasure", UnitOfMeasureHeaders, UnitOfMeasureData);
                #endregion
                #region CustomerSalesOrderPaymentHistory
                var CustomerSalesOrderPaymentHistoryFilter = new CustomerSalesOrderPaymentHistoryFilter();
                CustomerSalesOrderPaymentHistoryFilter.Selects = CustomerSalesOrderPaymentHistorySelect.ALL;
                CustomerSalesOrderPaymentHistoryFilter.OrderBy = CustomerSalesOrderPaymentHistoryOrder.Id;
                CustomerSalesOrderPaymentHistoryFilter.OrderType = OrderType.ASC;
                CustomerSalesOrderPaymentHistoryFilter.Skip = 0;
                CustomerSalesOrderPaymentHistoryFilter.Take = int.MaxValue;
                List<CustomerSalesOrderPaymentHistory> CustomerSalesOrderPaymentHistories = await CustomerSalesOrderPaymentHistoryService.List(CustomerSalesOrderPaymentHistoryFilter);

                var CustomerSalesOrderPaymentHistoryHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "CustomerSalesOrderId",
                        "PaymentMilestone",
                        "PaymentPercentage",
                        "PaymentAmount",
                        "Description",
                        "IsPaid",
                    }
                };
                List<object[]> CustomerSalesOrderPaymentHistoryData = new List<object[]>();
                for (int i = 0; i < CustomerSalesOrderPaymentHistories.Count; i++)
                {
                    var CustomerSalesOrderPaymentHistory = CustomerSalesOrderPaymentHistories[i];
                    CustomerSalesOrderPaymentHistoryData.Add(new Object[]
                    {
                        CustomerSalesOrderPaymentHistory.Id,
                        CustomerSalesOrderPaymentHistory.CustomerSalesOrderId,
                        CustomerSalesOrderPaymentHistory.PaymentMilestone,
                        CustomerSalesOrderPaymentHistory.PaymentPercentage,
                        CustomerSalesOrderPaymentHistory.PaymentAmount,
                        CustomerSalesOrderPaymentHistory.Description,
                        CustomerSalesOrderPaymentHistory.IsPaid,
                    });
                }
                excel.GenerateWorksheet("CustomerSalesOrderPaymentHistory", CustomerSalesOrderPaymentHistoryHeaders, CustomerSalesOrderPaymentHistoryData);
                #endregion
                #region CustomerSalesOrderPromotion
                var CustomerSalesOrderPromotionFilter = new CustomerSalesOrderPromotionFilter();
                CustomerSalesOrderPromotionFilter.Selects = CustomerSalesOrderPromotionSelect.ALL;
                CustomerSalesOrderPromotionFilter.OrderBy = CustomerSalesOrderPromotionOrder.Id;
                CustomerSalesOrderPromotionFilter.OrderType = OrderType.ASC;
                CustomerSalesOrderPromotionFilter.Skip = 0;
                CustomerSalesOrderPromotionFilter.Take = int.MaxValue;
                List<CustomerSalesOrderPromotion> CustomerSalesOrderPromotions = await CustomerSalesOrderPromotionService.List(CustomerSalesOrderPromotionFilter);

                var CustomerSalesOrderPromotionHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "CustomerSalesOrderId",
                        "ItemId",
                        "UnitOfMeasureId",
                        "Quantity",
                        "RequestedQuantity",
                        "PrimaryUnitOfMeasureId",
                        "Factor",
                        "Note",
                    }
                };
                List<object[]> CustomerSalesOrderPromotionData = new List<object[]>();
                for (int i = 0; i < CustomerSalesOrderPromotions.Count; i++)
                {
                    var CustomerSalesOrderPromotion = CustomerSalesOrderPromotions[i];
                    CustomerSalesOrderPromotionData.Add(new Object[]
                    {
                        CustomerSalesOrderPromotion.Id,
                        CustomerSalesOrderPromotion.CustomerSalesOrderId,
                        CustomerSalesOrderPromotion.ItemId,
                        CustomerSalesOrderPromotion.UnitOfMeasureId,
                        CustomerSalesOrderPromotion.Quantity,
                        CustomerSalesOrderPromotion.RequestedQuantity,
                        CustomerSalesOrderPromotion.PrimaryUnitOfMeasureId,
                        CustomerSalesOrderPromotion.Factor,
                        CustomerSalesOrderPromotion.Note,
                    });
                }
                excel.GenerateWorksheet("CustomerSalesOrderPromotion", CustomerSalesOrderPromotionHeaders, CustomerSalesOrderPromotionData);
                #endregion
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "CustomerSalesOrder.xlsx");
        }

        [Route(CustomerSalesOrderRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] CustomerSalesOrder_CustomerSalesOrderFilterDTO CustomerSalesOrder_CustomerSalesOrderFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            string path = "Templates/CustomerSalesOrder_Template.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "CustomerSalesOrder.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            CustomerSalesOrderFilter CustomerSalesOrderFilter = new CustomerSalesOrderFilter();
            CustomerSalesOrderFilter = await CustomerSalesOrderService.ToFilter(CustomerSalesOrderFilter);
            if (Id == 0)
            {

            }
            else
            {
                CustomerSalesOrderFilter.Id = new IdFilter { Equal = Id };
                int count = await CustomerSalesOrderService.Count(CustomerSalesOrderFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private CustomerSalesOrder ConvertDTOToEntity(CustomerSalesOrder_CustomerSalesOrderDTO CustomerSalesOrder_CustomerSalesOrderDTO)
        {
            CustomerSalesOrder CustomerSalesOrder = new CustomerSalesOrder();
            CustomerSalesOrder.Id = CustomerSalesOrder_CustomerSalesOrderDTO.Id;
            CustomerSalesOrder.Code = CustomerSalesOrder_CustomerSalesOrderDTO.Code;
            CustomerSalesOrder.CustomerTypeId = CustomerSalesOrder_CustomerSalesOrderDTO.CustomerTypeId;
            CustomerSalesOrder.CustomerId = CustomerSalesOrder_CustomerSalesOrderDTO.CustomerId;
            CustomerSalesOrder.OpportunityId = CustomerSalesOrder_CustomerSalesOrderDTO.OpportunityId;
            CustomerSalesOrder.ContractId = CustomerSalesOrder_CustomerSalesOrderDTO.ContractId;
            CustomerSalesOrder.OrderPaymentStatusId = CustomerSalesOrder_CustomerSalesOrderDTO.OrderPaymentStatusId;
            CustomerSalesOrder.RequestStateId = CustomerSalesOrder_CustomerSalesOrderDTO.RequestStateId;
            CustomerSalesOrder.EditedPriceStatusId = CustomerSalesOrder_CustomerSalesOrderDTO.EditedPriceStatusId;
            CustomerSalesOrder.ShippingName = CustomerSalesOrder_CustomerSalesOrderDTO.ShippingName;
            CustomerSalesOrder.OrderDate = CustomerSalesOrder_CustomerSalesOrderDTO.OrderDate;
            CustomerSalesOrder.DeliveryDate = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryDate;
            CustomerSalesOrder.SalesEmployeeId = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployeeId;
            CustomerSalesOrder.Note = CustomerSalesOrder_CustomerSalesOrderDTO.Note;
            CustomerSalesOrder.InvoiceAddress = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceAddress;
            CustomerSalesOrder.InvoiceNationId = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceNationId;
            CustomerSalesOrder.InvoiceProvinceId = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceProvinceId;
            CustomerSalesOrder.InvoiceDistrictId = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceDistrictId;
            CustomerSalesOrder.InvoiceWardId = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceWardId;
            CustomerSalesOrder.InvoiceZIPCode = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceZIPCode;
            CustomerSalesOrder.DeliveryAddress = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryAddress;
            CustomerSalesOrder.DeliveryNationId = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryNationId;
            CustomerSalesOrder.DeliveryProvinceId = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryProvinceId;
            CustomerSalesOrder.DeliveryDistrictId = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryDistrictId;
            CustomerSalesOrder.DeliveryWardId = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryWardId;
            CustomerSalesOrder.DeliveryZIPCode = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryZIPCode;
            CustomerSalesOrder.SubTotal = CustomerSalesOrder_CustomerSalesOrderDTO.SubTotal;
            CustomerSalesOrder.GeneralDiscountPercentage = CustomerSalesOrder_CustomerSalesOrderDTO.GeneralDiscountPercentage;
            CustomerSalesOrder.GeneralDiscountAmount = CustomerSalesOrder_CustomerSalesOrderDTO.GeneralDiscountAmount;
            CustomerSalesOrder.TotalTaxOther = CustomerSalesOrder_CustomerSalesOrderDTO.TotalTaxOther;
            CustomerSalesOrder.TotalTax = CustomerSalesOrder_CustomerSalesOrderDTO.TotalTax;
            CustomerSalesOrder.Total = CustomerSalesOrder_CustomerSalesOrderDTO.Total;
            CustomerSalesOrder.CreatorId = CustomerSalesOrder_CustomerSalesOrderDTO.CreatorId;
            CustomerSalesOrder.OrganizationId = CustomerSalesOrder_CustomerSalesOrderDTO.OrganizationId;
            CustomerSalesOrder.RowId = CustomerSalesOrder_CustomerSalesOrderDTO.RowId;
            CustomerSalesOrder.Contract = CustomerSalesOrder_CustomerSalesOrderDTO.Contract == null ? null : new Contract
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.Id,
                Code = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.Code,
                Name = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.Name,
                TotalValue = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.TotalValue,
                ValidityDate = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.ValidityDate,
                ExpirationDate = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.ExpirationDate,
                DeliveryUnit = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.DeliveryUnit,
                InvoiceAddress = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.InvoiceAddress,
                InvoiceZipCode = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.InvoiceZipCode,
                ReceiveAddress = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.ReceiveAddress,
                ReceiveZipCode = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.ReceiveZipCode,
                TermAndCondition = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.TermAndCondition,
                InvoiceNationId = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.InvoiceNationId,
                InvoiceProvinceId = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.InvoiceProvinceId,
                InvoiceDistrictId = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.InvoiceDistrictId,
                ReceiveNationId = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.ReceiveNationId,
                ReceiveProvinceId = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.ReceiveProvinceId,
                ReceiveDistrictId = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.ReceiveDistrictId,
                ContractTypeId = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.ContractTypeId,
                CompanyId = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.CompanyId,
                OpportunityId = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.OpportunityId,
                OrganizationId = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.OrganizationId,
                AppUserId = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.AppUserId,
                ContractStatusId = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.ContractStatusId,
                CreatorId = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.CreatorId,
                CustomerId = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.CustomerId,
                CurrencyId = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.CurrencyId,
                PaymentStatusId = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.PaymentStatusId,
                SubTotal = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.SubTotal,
                GeneralDiscountPercentage = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.GeneralDiscountPercentage,
                GeneralDiscountAmount = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.GeneralDiscountAmount,
                TotalTaxAmountOther = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.TotalTaxAmountOther,
                TotalTaxAmount = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.TotalTaxAmount,
                Total = CustomerSalesOrder_CustomerSalesOrderDTO.Contract.Total,
            };
            CustomerSalesOrder.Creator = CustomerSalesOrder_CustomerSalesOrderDTO.Creator == null ? null : new AppUser
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.Creator.Id,
                Username = CustomerSalesOrder_CustomerSalesOrderDTO.Creator.Username,
                DisplayName = CustomerSalesOrder_CustomerSalesOrderDTO.Creator.DisplayName,
                Address = CustomerSalesOrder_CustomerSalesOrderDTO.Creator.Address,
                Email = CustomerSalesOrder_CustomerSalesOrderDTO.Creator.Email,
                Phone = CustomerSalesOrder_CustomerSalesOrderDTO.Creator.Phone,
                SexId = CustomerSalesOrder_CustomerSalesOrderDTO.Creator.SexId,
                Birthday = CustomerSalesOrder_CustomerSalesOrderDTO.Creator.Birthday,
                Avatar = CustomerSalesOrder_CustomerSalesOrderDTO.Creator.Avatar,
                Department = CustomerSalesOrder_CustomerSalesOrderDTO.Creator.Department,
                OrganizationId = CustomerSalesOrder_CustomerSalesOrderDTO.Creator.OrganizationId,
                Longitude = CustomerSalesOrder_CustomerSalesOrderDTO.Creator.Longitude,
                Latitude = CustomerSalesOrder_CustomerSalesOrderDTO.Creator.Latitude,
                StatusId = CustomerSalesOrder_CustomerSalesOrderDTO.Creator.StatusId,
                RowId = CustomerSalesOrder_CustomerSalesOrderDTO.Creator.RowId,
                Used = CustomerSalesOrder_CustomerSalesOrderDTO.Creator.Used,
            };
            CustomerSalesOrder.Customer = CustomerSalesOrder_CustomerSalesOrderDTO.Customer == null ? null : new Customer
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.Id,
                Code = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.Code,
                Name = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.Name,
                Phone = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.Phone,
                Address = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.Address,
                NationId = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.NationId,
                ProvinceId = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.ProvinceId,
                DistrictId = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.DistrictId,
                WardId = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.WardId,
                CustomerTypeId = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.CustomerTypeId,
                Birthday = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.Birthday,
                Email = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.Email,
                ProfessionId = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.ProfessionId,
                CustomerResourceId = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.CustomerResourceId,
                SexId = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.SexId,
                StatusId = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.StatusId,
                CompanyId = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.CompanyId,
                ParentCompanyId = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.ParentCompanyId,
                TaxCode = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.TaxCode,
                Fax = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.Fax,
                Website = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.Website,
                NumberOfEmployee = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.NumberOfEmployee,
                BusinessTypeId = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.BusinessTypeId,
                Investment = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.Investment,
                RevenueAnnual = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.RevenueAnnual,
                IsSupplier = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.IsSupplier,
                Descreption = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.Descreption,
                Used = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.Used,
                RowId = CustomerSalesOrder_CustomerSalesOrderDTO.Customer.RowId,
            };
            CustomerSalesOrder.CustomerType = CustomerSalesOrder_CustomerSalesOrderDTO.CustomerType == null ? null : new CustomerType
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.CustomerType.Id,
                Code = CustomerSalesOrder_CustomerSalesOrderDTO.CustomerType.Code,
                Name = CustomerSalesOrder_CustomerSalesOrderDTO.CustomerType.Name,
            };
            CustomerSalesOrder.DeliveryDistrict = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryDistrict == null ? null : new District
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryDistrict.Id,
                Code = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryDistrict.Code,
                Name = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryDistrict.Name,
                Priority = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryDistrict.Priority,
                ProvinceId = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryDistrict.ProvinceId,
                StatusId = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryDistrict.StatusId,
                RowId = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryDistrict.RowId,
                Used = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryDistrict.Used,
            };
            CustomerSalesOrder.DeliveryNation = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryNation == null ? null : new Nation
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryNation.Id,
                Code = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryNation.Code,
                Name = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryNation.Name,
                Priority = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryNation.Priority,
                StatusId = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryNation.StatusId,
                Used = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryNation.Used,
                RowId = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryNation.RowId,
            };
            CustomerSalesOrder.DeliveryProvince = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryProvince == null ? null : new Province
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryProvince.Id,
                Code = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryProvince.Code,
                Name = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryProvince.Name,
                Priority = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryProvince.Priority,
                StatusId = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryProvince.StatusId,
                RowId = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryProvince.RowId,
                Used = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryProvince.Used,
            };
            CustomerSalesOrder.DeliveryWard = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryWard == null ? null : new Ward
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryWard.Id,
                Code = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryWard.Code,
                Name = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryWard.Name,
                Priority = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryWard.Priority,
                DistrictId = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryWard.DistrictId,
                StatusId = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryWard.StatusId,
                RowId = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryWard.RowId,
                Used = CustomerSalesOrder_CustomerSalesOrderDTO.DeliveryWard.Used,
            };
            CustomerSalesOrder.EditedPriceStatus = CustomerSalesOrder_CustomerSalesOrderDTO.EditedPriceStatus == null ? null : new EditedPriceStatus
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.EditedPriceStatus.Id,
                Code = CustomerSalesOrder_CustomerSalesOrderDTO.EditedPriceStatus.Code,
                Name = CustomerSalesOrder_CustomerSalesOrderDTO.EditedPriceStatus.Name,
            };
            CustomerSalesOrder.InvoiceDistrict = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceDistrict == null ? null : new District
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceDistrict.Id,
                Code = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceDistrict.Code,
                Name = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceDistrict.Name,
                Priority = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceDistrict.Priority,
                ProvinceId = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceDistrict.ProvinceId,
                StatusId = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceDistrict.StatusId,
                RowId = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceDistrict.RowId,
                Used = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceDistrict.Used,
            };
            CustomerSalesOrder.InvoiceNation = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceNation == null ? null : new Nation
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceNation.Id,
                Code = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceNation.Code,
                Name = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceNation.Name,
                Priority = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceNation.Priority,
                StatusId = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceNation.StatusId,
                Used = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceNation.Used,
                RowId = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceNation.RowId,
            };
            CustomerSalesOrder.InvoiceProvince = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceProvince == null ? null : new Province
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceProvince.Id,
                Code = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceProvince.Code,
                Name = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceProvince.Name,
                Priority = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceProvince.Priority,
                StatusId = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceProvince.StatusId,
                RowId = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceProvince.RowId,
                Used = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceProvince.Used,
            };
            CustomerSalesOrder.InvoiceWard = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceWard == null ? null : new Ward
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceWard.Id,
                Code = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceWard.Code,
                Name = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceWard.Name,
                Priority = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceWard.Priority,
                DistrictId = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceWard.DistrictId,
                StatusId = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceWard.StatusId,
                RowId = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceWard.RowId,
                Used = CustomerSalesOrder_CustomerSalesOrderDTO.InvoiceWard.Used,
            };
            CustomerSalesOrder.Opportunity = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity == null ? null : new Opportunity
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.Id,
                Name = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.Name,
                CompanyId = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.CompanyId,
                CustomerLeadId = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.CustomerLeadId,
                ClosingDate = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.ClosingDate,
                SaleStageId = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.SaleStageId,
                ProbabilityId = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.ProbabilityId,
                PotentialResultId = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.PotentialResultId,
                LeadSourceId = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.LeadSourceId,
                AppUserId = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.AppUserId,
                CurrencyId = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.CurrencyId,
                Amount = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.Amount,
                ForecastAmount = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.ForecastAmount,
                Description = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.Description,
                RefuseReciveSMS = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.RefuseReciveSMS,
                RefuseReciveEmail = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.RefuseReciveEmail,
                OpportunityResultTypeId = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.OpportunityResultTypeId,
                CreatorId = CustomerSalesOrder_CustomerSalesOrderDTO.Opportunity.CreatorId,
            };
            CustomerSalesOrder.OrderPaymentStatus = CustomerSalesOrder_CustomerSalesOrderDTO.OrderPaymentStatus == null ? null : new OrderPaymentStatus
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.OrderPaymentStatus.Id,
                Code = CustomerSalesOrder_CustomerSalesOrderDTO.OrderPaymentStatus.Code,
                Name = CustomerSalesOrder_CustomerSalesOrderDTO.OrderPaymentStatus.Name,
            };
            CustomerSalesOrder.Organization = CustomerSalesOrder_CustomerSalesOrderDTO.Organization == null ? null : new Organization
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.Organization.Id,
                Code = CustomerSalesOrder_CustomerSalesOrderDTO.Organization.Code,
                Name = CustomerSalesOrder_CustomerSalesOrderDTO.Organization.Name,
                ParentId = CustomerSalesOrder_CustomerSalesOrderDTO.Organization.ParentId,
                Path = CustomerSalesOrder_CustomerSalesOrderDTO.Organization.Path,
                Level = CustomerSalesOrder_CustomerSalesOrderDTO.Organization.Level,
                StatusId = CustomerSalesOrder_CustomerSalesOrderDTO.Organization.StatusId,
                Phone = CustomerSalesOrder_CustomerSalesOrderDTO.Organization.Phone,
                Email = CustomerSalesOrder_CustomerSalesOrderDTO.Organization.Email,
                Address = CustomerSalesOrder_CustomerSalesOrderDTO.Organization.Address,
                RowId = CustomerSalesOrder_CustomerSalesOrderDTO.Organization.RowId,
                Used = CustomerSalesOrder_CustomerSalesOrderDTO.Organization.Used,
            };
            CustomerSalesOrder.RequestState = CustomerSalesOrder_CustomerSalesOrderDTO.RequestState == null ? null : new RequestState
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.RequestState.Id,
                Code = CustomerSalesOrder_CustomerSalesOrderDTO.RequestState.Code,
                Name = CustomerSalesOrder_CustomerSalesOrderDTO.RequestState.Name,
            };
            CustomerSalesOrder.SalesEmployee = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee == null ? null : new AppUser
            {
                Id = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee.Id,
                Username = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee.Username,
                DisplayName = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee.DisplayName,
                Address = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee.Address,
                Email = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee.Email,
                Phone = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee.Phone,
                SexId = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee.SexId,
                Birthday = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee.Birthday,
                Avatar = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee.Avatar,
                Department = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee.Department,
                OrganizationId = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee.OrganizationId,
                Longitude = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee.Longitude,
                Latitude = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee.Latitude,
                StatusId = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee.StatusId,
                RowId = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee.RowId,
                Used = CustomerSalesOrder_CustomerSalesOrderDTO.SalesEmployee.Used,
            };
            CustomerSalesOrder.CustomerSalesOrderContents = CustomerSalesOrder_CustomerSalesOrderDTO.CustomerSalesOrderContents?
                .Select(x => new CustomerSalesOrderContent
                {
                    Id = x.Id,
                    ItemId = x.ItemId,
                    UnitOfMeasureId = x.UnitOfMeasureId,
                    Quantity = x.Quantity,
                    RequestedQuantity = x.RequestedQuantity,
                    PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                    SalePrice = x.SalePrice,
                    PrimaryPrice = x.PrimaryPrice,
                    DiscountPercentage = x.DiscountPercentage,
                    DiscountAmount = x.DiscountAmount,
                    GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                    GeneralDiscountAmount = x.GeneralDiscountAmount,
                    TaxPercentage = x.TaxPercentage,
                    TaxAmount = x.TaxAmount,
                    TaxPercentageOther = x.TaxPercentageOther,
                    TaxAmountOther = x.TaxAmountOther,
                    Amount = x.Amount,
                    Factor = x.Factor,
                    EditedPriceStatusId = x.EditedPriceStatusId,
                    TaxTypeId = x.TaxTypeId,
                    EditedPriceStatus = x.EditedPriceStatus == null ? null : new EditedPriceStatus
                    {
                        Id = x.EditedPriceStatus.Id,
                        Code = x.EditedPriceStatus.Code,
                        Name = x.EditedPriceStatus.Name,
                    },
                    Item = x.Item == null ? null : new Item
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
                    PrimaryUnitOfMeasure = x.PrimaryUnitOfMeasure == null ? null : new UnitOfMeasure
                    {
                        Id = x.PrimaryUnitOfMeasure.Id,
                        Code = x.PrimaryUnitOfMeasure.Code,
                        Name = x.PrimaryUnitOfMeasure.Name,
                        Description = x.PrimaryUnitOfMeasure.Description,
                        StatusId = x.PrimaryUnitOfMeasure.StatusId,
                        Used = x.PrimaryUnitOfMeasure.Used,
                        RowId = x.PrimaryUnitOfMeasure.RowId,
                    },
                    UnitOfMeasure = x.UnitOfMeasure == null ? null : new UnitOfMeasure
                    {
                        Id = x.UnitOfMeasure.Id,
                        Code = x.UnitOfMeasure.Code,
                        Name = x.UnitOfMeasure.Name,
                        Description = x.UnitOfMeasure.Description,
                        StatusId = x.UnitOfMeasure.StatusId,
                        Used = x.UnitOfMeasure.Used,
                        RowId = x.UnitOfMeasure.RowId,
                    },
                    TaxType = x.TaxType == null ? null : new TaxType
                    {
                        Id = x.TaxType.Id,
                        Code = x.TaxType.Code,
                        Name = x.TaxType.Name,
                    }
                }).ToList();
            CustomerSalesOrder.CustomerSalesOrderPaymentHistories = CustomerSalesOrder_CustomerSalesOrderDTO.CustomerSalesOrderPaymentHistories?
                .Select(x => new CustomerSalesOrderPaymentHistory
                {
                    Id = x.Id,
                    PaymentMilestone = x.PaymentMilestone,
                    PaymentPercentage = x.PaymentPercentage,
                    PaymentAmount = x.PaymentAmount,
                    Description = x.Description,
                    IsPaid = x.IsPaid,
                }).ToList();
            CustomerSalesOrder.CustomerSalesOrderPromotions = CustomerSalesOrder_CustomerSalesOrderDTO.CustomerSalesOrderPromotions?
                .Select(x => new CustomerSalesOrderPromotion
                {
                    Id = x.Id,
                    ItemId = x.ItemId,
                    UnitOfMeasureId = x.UnitOfMeasureId,
                    Quantity = x.Quantity,
                    RequestedQuantity = x.RequestedQuantity,
                    PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                    Factor = x.Factor,
                    Note = x.Note,
                    Item = x.Item == null ? null : new Item
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
                    PrimaryUnitOfMeasure = x.PrimaryUnitOfMeasure == null ? null : new UnitOfMeasure
                    {
                        Id = x.PrimaryUnitOfMeasure.Id,
                        Code = x.PrimaryUnitOfMeasure.Code,
                        Name = x.PrimaryUnitOfMeasure.Name,
                        Description = x.PrimaryUnitOfMeasure.Description,
                        StatusId = x.PrimaryUnitOfMeasure.StatusId,
                        Used = x.PrimaryUnitOfMeasure.Used,
                        RowId = x.PrimaryUnitOfMeasure.RowId,
                    },
                    UnitOfMeasure = x.UnitOfMeasure == null ? null : new UnitOfMeasure
                    {
                        Id = x.UnitOfMeasure.Id,
                        Code = x.UnitOfMeasure.Code,
                        Name = x.UnitOfMeasure.Name,
                        Description = x.UnitOfMeasure.Description,
                        StatusId = x.UnitOfMeasure.StatusId,
                        Used = x.UnitOfMeasure.Used,
                        RowId = x.UnitOfMeasure.RowId,
                    },
                }).ToList();
            CustomerSalesOrder.BaseLanguage = CurrentContext.Language;
            return CustomerSalesOrder;
        }

        private CustomerSalesOrderFilter ConvertFilterDTOToFilterEntity(CustomerSalesOrder_CustomerSalesOrderFilterDTO CustomerSalesOrder_CustomerSalesOrderFilterDTO)
        {
            CustomerSalesOrderFilter CustomerSalesOrderFilter = new CustomerSalesOrderFilter();
            CustomerSalesOrderFilter.Selects = CustomerSalesOrderSelect.ALL;
            CustomerSalesOrderFilter.Skip = CustomerSalesOrder_CustomerSalesOrderFilterDTO.Skip;
            CustomerSalesOrderFilter.Take = CustomerSalesOrder_CustomerSalesOrderFilterDTO.Take;
            CustomerSalesOrderFilter.OrderBy = CustomerSalesOrder_CustomerSalesOrderFilterDTO.OrderBy;
            CustomerSalesOrderFilter.OrderType = CustomerSalesOrder_CustomerSalesOrderFilterDTO.OrderType;

            CustomerSalesOrderFilter.Id = CustomerSalesOrder_CustomerSalesOrderFilterDTO.Id;
            CustomerSalesOrderFilter.Code = CustomerSalesOrder_CustomerSalesOrderFilterDTO.Code;
            CustomerSalesOrderFilter.CustomerTypeId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.CustomerTypeId;
            CustomerSalesOrderFilter.CustomerId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.CustomerId;
            CustomerSalesOrderFilter.OpportunityId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.OpportunityId;
            CustomerSalesOrderFilter.ContractId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.ContractId;
            CustomerSalesOrderFilter.OrderPaymentStatusId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.OrderPaymentStatusId;
            CustomerSalesOrderFilter.RequestStateId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.RequestStateId;
            CustomerSalesOrderFilter.EditedPriceStatusId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.EditedPriceStatusId;
            CustomerSalesOrderFilter.ShippingName = CustomerSalesOrder_CustomerSalesOrderFilterDTO.ShippingName;
            CustomerSalesOrderFilter.OrderDate = CustomerSalesOrder_CustomerSalesOrderFilterDTO.OrderDate;
            CustomerSalesOrderFilter.DeliveryDate = CustomerSalesOrder_CustomerSalesOrderFilterDTO.DeliveryDate;
            CustomerSalesOrderFilter.SalesEmployeeId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.SalesEmployeeId;
            CustomerSalesOrderFilter.Note = CustomerSalesOrder_CustomerSalesOrderFilterDTO.Note;
            CustomerSalesOrderFilter.InvoiceAddress = CustomerSalesOrder_CustomerSalesOrderFilterDTO.InvoiceAddress;
            CustomerSalesOrderFilter.InvoiceNationId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.InvoiceNationId;
            CustomerSalesOrderFilter.InvoiceProvinceId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.InvoiceProvinceId;
            CustomerSalesOrderFilter.InvoiceDistrictId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.InvoiceDistrictId;
            CustomerSalesOrderFilter.InvoiceWardId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.InvoiceWardId;
            CustomerSalesOrderFilter.InvoiceZIPCode = CustomerSalesOrder_CustomerSalesOrderFilterDTO.InvoiceZIPCode;
            CustomerSalesOrderFilter.DeliveryAddress = CustomerSalesOrder_CustomerSalesOrderFilterDTO.DeliveryAddress;
            CustomerSalesOrderFilter.DeliveryNationId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.DeliveryNationId;
            CustomerSalesOrderFilter.DeliveryProvinceId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.DeliveryProvinceId;
            CustomerSalesOrderFilter.DeliveryDistrictId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.DeliveryDistrictId;
            CustomerSalesOrderFilter.DeliveryWardId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.DeliveryWardId;
            CustomerSalesOrderFilter.DeliveryZIPCode = CustomerSalesOrder_CustomerSalesOrderFilterDTO.DeliveryZIPCode;
            CustomerSalesOrderFilter.SubTotal = CustomerSalesOrder_CustomerSalesOrderFilterDTO.SubTotal;
            CustomerSalesOrderFilter.GeneralDiscountPercentage = CustomerSalesOrder_CustomerSalesOrderFilterDTO.GeneralDiscountPercentage;
            CustomerSalesOrderFilter.GeneralDiscountAmount = CustomerSalesOrder_CustomerSalesOrderFilterDTO.GeneralDiscountAmount;
            CustomerSalesOrderFilter.TotalTaxOther = CustomerSalesOrder_CustomerSalesOrderFilterDTO.TotalTaxOther;
            CustomerSalesOrderFilter.TotalTax = CustomerSalesOrder_CustomerSalesOrderFilterDTO.TotalTax;
            CustomerSalesOrderFilter.Total = CustomerSalesOrder_CustomerSalesOrderFilterDTO.Total;
            CustomerSalesOrderFilter.CreatorId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.CreatorId;
            CustomerSalesOrderFilter.OrganizationId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.OrganizationId;
            CustomerSalesOrderFilter.RowId = CustomerSalesOrder_CustomerSalesOrderFilterDTO.RowId;
            CustomerSalesOrderFilter.CreatedAt = CustomerSalesOrder_CustomerSalesOrderFilterDTO.CreatedAt;
            CustomerSalesOrderFilter.UpdatedAt = CustomerSalesOrder_CustomerSalesOrderFilterDTO.UpdatedAt;
            return CustomerSalesOrderFilter;
        }
    }
}

