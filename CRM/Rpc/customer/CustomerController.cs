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
using System.Dynamic;
using CRM.Entities;
using CRM.Services.MCustomer;
using CRM.Services.MBusinessType;
using CRM.Services.MCompany;
using CRM.Services.MCallCategory;
using CRM.Services.MCallStatus;
using CRM.Services.MCallType;
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
using CRM.Services.MEmailStatus;
using CRM.Services.MCustomerEmailHistory;
using CRM.Services.MDirectSalesOrder;
using CRM.Services.MCustomerSalesOrder;
using CRM.Services.MTaxType;
using CRM.Services.MContract;
using CRM.Services.MContractType;
using CRM.Services.MContractStatus;
using CRM.Services.MCurrency;
using CRM.Services.MPaymentStatus;
using CRM.Services.MOrderCategory;
using CRM.Services.MRepairStatus;
using CRM.Services.MCustomerGrouping;
using CRM.Services.MTicket;
using CRM.Services.MTicketIssueLevel;
using CRM.Services.MTicketPriority;
using CRM.Services.MTicketSource;
using CRM.Services.MTicketStatus;
using CRM.Services.MTicketGroup;
using CRM.Services.MTicketType;
using CRM.Services.MTicketResolveType;
using CRM.Services.MMailTemplate;
using CRM.Services.MRequestState;
using CRM.Services.MOpportunity;
using CRM.Services.MEditedPriceStatus;
using CRM.Services.MOrganization;
using CRM.Models;

namespace CRM.Rpc.customer
{
    public partial class CustomerController : RpcController
    {
        private IBusinessTypeService BusinessTypeService;
        private ICompanyService CompanyService;
        private ICallCategoryService CallCategoryService;
        private ICallStatusService CallStatusService;
        private ICallTypeService CallTypeService;
        private IAppUserService AppUserService;
        private ICustomerResourceService CustomerResourceService;
        private ICustomerTypeService CustomerTypeService;
        private IDistrictService DistrictService;
        private IEmailStatusService EmailStatusService;
        private INationService NationService;
        private IProfessionService ProfessionService;
        private IProvinceService ProvinceService;
        private ISexService SexService;
        private IStatusService StatusService;
        private IWardService WardService;
        private ICustomerEmailService CustomerEmailService;
        private IEmailTypeService EmailTypeService;
        private ICustomerFeedbackService CustomerFeedbackService;
        private ICustomerFeedbackTypeService CustomerFeedbackTypeService;
        private ICustomerPhoneService CustomerPhoneService;
        private IPhoneTypeService PhoneTypeService;
        private ICustomerPointHistoryService CustomerPointHistoryService;
        private IRepairTicketService RepairTicketService;
        private IStoreService StoreService;
        private ICustomerService CustomerService;
        private ICustomerEmailHistoryService CustomerEmailHistoryService;
        private ICustomerSalesOrderService CustomerSalesOrderService;
        private IDirectSalesOrderService DirectSalesOrderService;
        private ITaxTypeService TaxTypeService;
        private IContractService ContractService;
        private IContractTypeService ContractTypeService;
        private IContractStatusService ContractStatusService;
        private ICurrencyService CurrencyService;
        private IPaymentStatusService PaymentStatusService;
        private IOrderCategoryService OrderCategoryService;
        private IRepairStatusService RepairStatusService;
        private ICustomerGroupingService CustomerGroupingService;
        private ITicketService TicketService;
        private ITicketIssueLevelService TicketIssueLevelService;
        private ITicketPriorityService TicketPriorityService;
        private ITicketSourceService TicketSourceService;
        private ITicketStatusService TicketStatusService;
        private ITicketGroupService TicketGroupService;
        private ITicketTypeService TicketTypeService;
        private ITicketResolveTypeService TicketResolveTypeService;
        private IMailTemplateService MailTemplateService;
        private IRequestStateService RequestStateService;
        private IOpportunityService OpportunityService;
        private IEditedPriceStatusService EditedPriceStatusService;
        private IOrganizationService OrganizationService;
        private ICurrentContext CurrentContext;
        public CustomerController(
            IBusinessTypeService BusinessTypeService,
            ICallCategoryService CallCategoryService,
            ICallStatusService CallStatusService,
            ICallTypeService CallTypeService,
            ICompanyService CompanyService,
            IAppUserService AppUserService,
            ICustomerResourceService CustomerResourceService,
            ICustomerTypeService CustomerTypeService,
            IDistrictService DistrictService,
            IEmailStatusService EmailStatusService,
            INationService NationService,
            IProfessionService ProfessionService,
            IProvinceService ProvinceService,
            ISexService SexService,
            IStatusService StatusService,
            IWardService WardService,
            ICustomerEmailService CustomerEmailService,
            IEmailTypeService EmailTypeService,
            ICustomerFeedbackService CustomerFeedbackService,
            ICustomerFeedbackTypeService CustomerFeedbackTypeService,
            ICustomerPhoneService CustomerPhoneService,
            IPhoneTypeService PhoneTypeService,
            ICustomerPointHistoryService CustomerPointHistoryService,
            IRepairTicketService RepairTicketService,
            IStoreService StoreService,
            ICustomerService CustomerService,
            ICustomerEmailHistoryService CustomerEmailHistoryService,
            ICustomerSalesOrderService CustomerSalesOrderService,
            IDirectSalesOrderService DirectSalesOrderService,
            ITaxTypeService TaxTypeService,
            IContractService ContractService,
            IContractTypeService ContractTypeService,
            IContractStatusService ContractStatusService,
            ICurrencyService CurrencyService,
            IPaymentStatusService PaymentStatusService,
            IOrderCategoryService OrderCategoryService,
            IRepairStatusService RepairStatusService,
            ICustomerGroupingService CustomerGroupingService,
            ITicketService TicketService,
            ITicketIssueLevelService TicketIssueLevelService,
            ITicketPriorityService TicketPriorityService,
            ITicketSourceService TicketSourceService,
            ITicketStatusService TicketStatusService,
            ITicketGroupService TicketGroupService,
            ITicketTypeService TicketTypeService,
            ITicketResolveTypeService TicketResolveTypeService,
            IMailTemplateService MailTemplateService,
            IRequestStateService RequestStateService,
            IOpportunityService OpportunityService,
            IEditedPriceStatusService EditedPriceStatusService,
            IOrganizationService OrganizationService,
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.BusinessTypeService = BusinessTypeService;
            this.CompanyService = CompanyService;
            this.CallCategoryService = CallCategoryService;
            this.CallStatusService = CallStatusService;
            this.CallTypeService = CallTypeService;
            this.AppUserService = AppUserService;
            this.CustomerResourceService = CustomerResourceService;
            this.CustomerTypeService = CustomerTypeService;
            this.DistrictService = DistrictService;
            this.EmailStatusService = EmailStatusService;
            this.NationService = NationService;
            this.ProfessionService = ProfessionService;
            this.ProvinceService = ProvinceService;
            this.SexService = SexService;
            this.StatusService = StatusService;
            this.WardService = WardService;
            this.CustomerEmailService = CustomerEmailService;
            this.EmailTypeService = EmailTypeService;
            this.CustomerFeedbackService = CustomerFeedbackService;
            this.CustomerFeedbackTypeService = CustomerFeedbackTypeService;
            this.CustomerPhoneService = CustomerPhoneService;
            this.PhoneTypeService = PhoneTypeService;
            this.CustomerPointHistoryService = CustomerPointHistoryService;
            this.RepairTicketService = RepairTicketService;
            this.StoreService = StoreService;
            this.CustomerService = CustomerService;
            this.CustomerEmailHistoryService = CustomerEmailHistoryService;
            this.CustomerSalesOrderService = CustomerSalesOrderService;
            this.DirectSalesOrderService = DirectSalesOrderService;
            this.TaxTypeService = TaxTypeService;
            this.ContractService = ContractService;
            this.ContractTypeService = ContractTypeService;
            this.ContractStatusService = ContractStatusService;
            this.CurrencyService = CurrencyService;
            this.PaymentStatusService = PaymentStatusService;
            this.OrderCategoryService = OrderCategoryService;
            this.RepairStatusService = RepairStatusService;
            this.CustomerGroupingService = CustomerGroupingService;
            this.TicketService = TicketService;
            this.TicketIssueLevelService = TicketIssueLevelService;
            this.TicketPriorityService = TicketPriorityService;
            this.TicketSourceService = TicketSourceService;
            this.TicketStatusService = TicketStatusService;
            this.TicketGroupService = TicketGroupService;
            this.TicketTypeService = TicketTypeService;
            this.TicketResolveTypeService = TicketResolveTypeService;
            this.MailTemplateService = MailTemplateService;
            this.RequestStateService = RequestStateService;
            this.OpportunityService = OpportunityService;
            this.EditedPriceStatusService = EditedPriceStatusService;
            this.OrganizationService = OrganizationService;
            this.CurrentContext = CurrentContext;
        }

        [Route(CustomerRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] Customer_CustomerFilterDTO Customer_CustomerFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFilter CustomerFilter = ConvertFilterDTOToFilterEntity(Customer_CustomerFilterDTO);
            CustomerFilter = await CustomerService.ToFilter(CustomerFilter);
            int count = await CustomerService.Count(CustomerFilter);
            return count;
        }

        [Route(CustomerRoute.List), HttpPost]
        public async Task<ActionResult<List<Customer_CustomerDTO>>> List([FromBody] Customer_CustomerFilterDTO Customer_CustomerFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFilter CustomerFilter = ConvertFilterDTOToFilterEntity(Customer_CustomerFilterDTO);
            CustomerFilter = await CustomerService.ToFilter(CustomerFilter);
            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            List<Customer_CustomerDTO> Customer_CustomerDTOs = Customers
                .Select(c => new Customer_CustomerDTO(c)).ToList();
            return Customer_CustomerDTOs;
        }

        [Route(CustomerRoute.Get), HttpPost]
        public async Task<ActionResult<Customer_CustomerDTO>> Get([FromBody]Customer_CustomerDTO Customer_CustomerDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Customer_CustomerDTO.Id))
                return Forbid();

            Customer Customer = await CustomerService.Get(Customer_CustomerDTO.Id);
            return new Customer_CustomerDTO(Customer);
        }

        [Route(CustomerRoute.Create), HttpPost]
        public async Task<ActionResult<Customer_CustomerDTO>> Create([FromBody] Customer_CustomerDTO Customer_CustomerDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(Customer_CustomerDTO.Id))
                return Forbid();

            Customer Customer = ConvertDTOToEntity(Customer_CustomerDTO);
            Customer = await CustomerService.Create(Customer);
            Customer_CustomerDTO = new Customer_CustomerDTO(Customer);
            if (Customer.IsValidated)
                return Customer_CustomerDTO;
            else
                return BadRequest(Customer_CustomerDTO);
        }

        [Route(CustomerRoute.Update), HttpPost]
        public async Task<ActionResult<Customer_CustomerDTO>> Update([FromBody] Customer_CustomerDTO Customer_CustomerDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(Customer_CustomerDTO.Id))
                return Forbid();

            Customer Customer = ConvertDTOToEntity(Customer_CustomerDTO);
            Customer = await CustomerService.Update(Customer);
            Customer_CustomerDTO = new Customer_CustomerDTO(Customer);
            if (Customer.IsValidated)
                return Customer_CustomerDTO;
            else
                return BadRequest(Customer_CustomerDTO);
        }

        [Route(CustomerRoute.Delete), HttpPost]
        public async Task<ActionResult<Customer_CustomerDTO>> Delete([FromBody] Customer_CustomerDTO Customer_CustomerDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Customer_CustomerDTO.Id))
                return Forbid();

            Customer Customer = ConvertDTOToEntity(Customer_CustomerDTO);
            Customer = await CustomerService.Delete(Customer);
            Customer_CustomerDTO = new Customer_CustomerDTO(Customer);
            if (Customer.IsValidated)
                return Customer_CustomerDTO;
            else
                return BadRequest(Customer_CustomerDTO);
        }
        
        [Route(CustomerRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter = await CustomerService.ToFilter(CustomerFilter);
            CustomerFilter.Id = new IdFilter { In = Ids };
            CustomerFilter.Selects = CustomerSelect.Id;
            CustomerFilter.Skip = 0;
            CustomerFilter.Take = int.MaxValue;

            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            Customers = await CustomerService.BulkDelete(Customers);
            if (Customers.Any(x => !x.IsValidated))
                return BadRequest(Customers.Where(x => !x.IsValidated));
            return true;
        }
        
        [Route(CustomerRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            BusinessTypeFilter BusinessTypeFilter = new BusinessTypeFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = BusinessTypeSelect.ALL
            };
            List<BusinessType> BusinessTypes = await BusinessTypeService.List(BusinessTypeFilter);
            CustomerResourceFilter CustomerResourceFilter = new CustomerResourceFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CustomerResourceSelect.ALL
            };
            List<CustomerResource> CustomerResources = await CustomerResourceService.List(CustomerResourceFilter);
            CustomerTypeFilter CustomerTypeFilter = new CustomerTypeFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CustomerTypeSelect.ALL
            };
            List<CustomerType> CustomerTypes = await CustomerTypeService.List(CustomerTypeFilter);
            DistrictFilter DistrictFilter = new DistrictFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = DistrictSelect.ALL
            };
            List<District> Districts = await DistrictService.List(DistrictFilter);
            NationFilter NationFilter = new NationFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = NationSelect.ALL
            };
            List<Nation> Nations = await NationService.List(NationFilter);
            ProfessionFilter ProfessionFilter = new ProfessionFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = ProfessionSelect.ALL
            };
            List<Profession> Professions = await ProfessionService.List(ProfessionFilter);
            ProvinceFilter ProvinceFilter = new ProvinceFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = ProvinceSelect.ALL
            };
            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            SexFilter SexFilter = new SexFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = SexSelect.ALL
            };
            List<Sex> Sexes = await SexService.List(SexFilter);
            StatusFilter StatusFilter = new StatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = StatusSelect.ALL
            };
            List<Status> Statuses = await StatusService.List(StatusFilter);
            WardFilter WardFilter = new WardFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = WardSelect.ALL
            };
            List<Ward> Wards = await WardService.List(WardFilter);
            List<Customer> Customers = new List<Customer>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(Customers);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int CodeColumn = 1 + StartColumn;
                int NameColumn = 2 + StartColumn;
                int PhoneColumn = 3 + StartColumn;
                int AddressColumn = 4 + StartColumn;
                int NationIdColumn = 5 + StartColumn;
                int ProvinceIdColumn = 6 + StartColumn;
                int DistrictIdColumn = 7 + StartColumn;
                int WardIdColumn = 8 + StartColumn;
                int CustomerTypeIdColumn = 9 + StartColumn;
                int BirthdayColumn = 10 + StartColumn;
                int EmailColumn = 11 + StartColumn;
                int ProfessionIdColumn = 12 + StartColumn;
                int CustomerResourceIdColumn = 13 + StartColumn;
                int SexIdColumn = 14 + StartColumn;
                int StatusIdColumn = 15 + StartColumn;
                int CompanyIdColumn = 16 + StartColumn;
                int ParentCompanyIdColumn = 17 + StartColumn;
                int TaxCodeColumn = 18 + StartColumn;
                int FaxColumn = 19 + StartColumn;
                int WebsiteColumn = 20 + StartColumn;
                int NumberOfEmployeeColumn = 21 + StartColumn;
                int BusinessTypeIdColumn = 22 + StartColumn;
                int InvestmentColumn = 23 + StartColumn;
                int RevenueAnnualColumn = 24 + StartColumn;
                int IsSupplierColumn = 25 + StartColumn;
                int DescreptionColumn = 26 + StartColumn;
                int UsedColumn = 30 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string CodeValue = worksheet.Cells[i + StartRow, CodeColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string PhoneValue = worksheet.Cells[i + StartRow, PhoneColumn].Value?.ToString();
                    string AddressValue = worksheet.Cells[i + StartRow, AddressColumn].Value?.ToString();
                    string NationIdValue = worksheet.Cells[i + StartRow, NationIdColumn].Value?.ToString();
                    string ProvinceIdValue = worksheet.Cells[i + StartRow, ProvinceIdColumn].Value?.ToString();
                    string DistrictIdValue = worksheet.Cells[i + StartRow, DistrictIdColumn].Value?.ToString();
                    string WardIdValue = worksheet.Cells[i + StartRow, WardIdColumn].Value?.ToString();
                    string CustomerTypeIdValue = worksheet.Cells[i + StartRow, CustomerTypeIdColumn].Value?.ToString();
                    string BirthdayValue = worksheet.Cells[i + StartRow, BirthdayColumn].Value?.ToString();
                    string EmailValue = worksheet.Cells[i + StartRow, EmailColumn].Value?.ToString();
                    string ProfessionIdValue = worksheet.Cells[i + StartRow, ProfessionIdColumn].Value?.ToString();
                    string CustomerResourceIdValue = worksheet.Cells[i + StartRow, CustomerResourceIdColumn].Value?.ToString();
                    string SexIdValue = worksheet.Cells[i + StartRow, SexIdColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    string CompanyIdValue = worksheet.Cells[i + StartRow, CompanyIdColumn].Value?.ToString();
                    string ParentCompanyIdValue = worksheet.Cells[i + StartRow, ParentCompanyIdColumn].Value?.ToString();
                    string TaxCodeValue = worksheet.Cells[i + StartRow, TaxCodeColumn].Value?.ToString();
                    string FaxValue = worksheet.Cells[i + StartRow, FaxColumn].Value?.ToString();
                    string WebsiteValue = worksheet.Cells[i + StartRow, WebsiteColumn].Value?.ToString();
                    string NumberOfEmployeeValue = worksheet.Cells[i + StartRow, NumberOfEmployeeColumn].Value?.ToString();
                    string BusinessTypeIdValue = worksheet.Cells[i + StartRow, BusinessTypeIdColumn].Value?.ToString();
                    string InvestmentValue = worksheet.Cells[i + StartRow, InvestmentColumn].Value?.ToString();
                    string RevenueAnnualValue = worksheet.Cells[i + StartRow, RevenueAnnualColumn].Value?.ToString();
                    string IsSupplierValue = worksheet.Cells[i + StartRow, IsSupplierColumn].Value?.ToString();
                    string DescreptionValue = worksheet.Cells[i + StartRow, DescreptionColumn].Value?.ToString();
                    string UsedValue = worksheet.Cells[i + StartRow, UsedColumn].Value?.ToString();
                    
                    Customer Customer = new Customer();
                    Customer.Code = CodeValue;
                    Customer.Name = NameValue;
                    Customer.Phone = PhoneValue;
                    Customer.Address = AddressValue;
                    Customer.Birthday = DateTime.TryParse(BirthdayValue, out DateTime Birthday) ? Birthday : DateTime.Now;
                    Customer.Email = EmailValue;
                    Customer.TaxCode = TaxCodeValue;
                    Customer.Fax = FaxValue;
                    Customer.Website = WebsiteValue;
                    Customer.NumberOfEmployee = long.TryParse(NumberOfEmployeeValue, out long NumberOfEmployee) ? NumberOfEmployee : 0;
                    Customer.Investment = decimal.TryParse(InvestmentValue, out decimal Investment) ? Investment : 0;
                    Customer.RevenueAnnual = decimal.TryParse(RevenueAnnualValue, out decimal RevenueAnnual) ? RevenueAnnual : 0;
                    Customer.Descreption = DescreptionValue;
                    BusinessType BusinessType = BusinessTypes.Where(x => x.Id.ToString() == BusinessTypeIdValue).FirstOrDefault();
                    Customer.BusinessTypeId = BusinessType == null ? 0 : BusinessType.Id;
                    Customer.BusinessType = BusinessType;
                    CustomerResource CustomerResource = CustomerResources.Where(x => x.Id.ToString() == CustomerResourceIdValue).FirstOrDefault();
                    Customer.CustomerResourceId = CustomerResource == null ? 0 : CustomerResource.Id;
                    Customer.CustomerResource = CustomerResource;
                    CustomerType CustomerType = CustomerTypes.Where(x => x.Id.ToString() == CustomerTypeIdValue).FirstOrDefault();
                    Customer.CustomerTypeId = CustomerType == null ? 0 : CustomerType.Id;
                    Customer.CustomerType = CustomerType;
                    District District = Districts.Where(x => x.Id.ToString() == DistrictIdValue).FirstOrDefault();
                    Customer.DistrictId = District == null ? 0 : District.Id;
                    Customer.District = District;
                    Nation Nation = Nations.Where(x => x.Id.ToString() == NationIdValue).FirstOrDefault();
                    Customer.NationId = Nation == null ? 0 : Nation.Id;
                    Customer.Nation = Nation;
                    Profession Profession = Professions.Where(x => x.Id.ToString() == ProfessionIdValue).FirstOrDefault();
                    Customer.ProfessionId = Profession == null ? 0 : Profession.Id;
                    Customer.Profession = Profession;
                    Province Province = Provinces.Where(x => x.Id.ToString() == ProvinceIdValue).FirstOrDefault();
                    Customer.ProvinceId = Province == null ? 0 : Province.Id;
                    Customer.Province = Province;
                    Sex Sex = Sexes.Where(x => x.Id.ToString() == SexIdValue).FirstOrDefault();
                    Customer.SexId = Sex == null ? 0 : Sex.Id;
                    Customer.Sex = Sex;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    Customer.StatusId = Status == null ? 0 : Status.Id;
                    Customer.Status = Status;
                    Ward Ward = Wards.Where(x => x.Id.ToString() == WardIdValue).FirstOrDefault();
                    Customer.WardId = Ward == null ? 0 : Ward.Id;
                    Customer.Ward = Ward;
                    
                    Customers.Add(Customer);
                }
            }
            Customers = await CustomerService.Import(Customers);
            if (Customers.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < Customers.Count; i++)
                {
                    Customer Customer = Customers[i];
                    if (!Customer.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (Customer.Errors.ContainsKey(nameof(Customer.Id)))
                            Error += Customer.Errors[nameof(Customer.Id)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.Code)))
                            Error += Customer.Errors[nameof(Customer.Code)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.Name)))
                            Error += Customer.Errors[nameof(Customer.Name)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.Phone)))
                            Error += Customer.Errors[nameof(Customer.Phone)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.Address)))
                            Error += Customer.Errors[nameof(Customer.Address)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.NationId)))
                            Error += Customer.Errors[nameof(Customer.NationId)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.ProvinceId)))
                            Error += Customer.Errors[nameof(Customer.ProvinceId)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.DistrictId)))
                            Error += Customer.Errors[nameof(Customer.DistrictId)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.WardId)))
                            Error += Customer.Errors[nameof(Customer.WardId)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.CustomerTypeId)))
                            Error += Customer.Errors[nameof(Customer.CustomerTypeId)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.Birthday)))
                            Error += Customer.Errors[nameof(Customer.Birthday)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.Email)))
                            Error += Customer.Errors[nameof(Customer.Email)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.ProfessionId)))
                            Error += Customer.Errors[nameof(Customer.ProfessionId)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.CustomerResourceId)))
                            Error += Customer.Errors[nameof(Customer.CustomerResourceId)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.SexId)))
                            Error += Customer.Errors[nameof(Customer.SexId)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.StatusId)))
                            Error += Customer.Errors[nameof(Customer.StatusId)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.CompanyId)))
                            Error += Customer.Errors[nameof(Customer.CompanyId)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.ParentCompanyId)))
                            Error += Customer.Errors[nameof(Customer.ParentCompanyId)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.TaxCode)))
                            Error += Customer.Errors[nameof(Customer.TaxCode)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.Fax)))
                            Error += Customer.Errors[nameof(Customer.Fax)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.Website)))
                            Error += Customer.Errors[nameof(Customer.Website)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.NumberOfEmployee)))
                            Error += Customer.Errors[nameof(Customer.NumberOfEmployee)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.BusinessTypeId)))
                            Error += Customer.Errors[nameof(Customer.BusinessTypeId)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.Investment)))
                            Error += Customer.Errors[nameof(Customer.Investment)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.RevenueAnnual)))
                            Error += Customer.Errors[nameof(Customer.RevenueAnnual)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.IsSupplier)))
                            Error += Customer.Errors[nameof(Customer.IsSupplier)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.Descreption)))
                            Error += Customer.Errors[nameof(Customer.Descreption)];
                        if (Customer.Errors.ContainsKey(nameof(Customer.Used)))
                            Error += Customer.Errors[nameof(Customer.Used)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(CustomerRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] Customer_CustomerFilterDTO Customer_CustomerFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region Customer
                var CustomerFilter = ConvertFilterDTOToFilterEntity(Customer_CustomerFilterDTO);
                CustomerFilter.Skip = 0;
                CustomerFilter.Take = int.MaxValue;
                CustomerFilter = await CustomerService.ToFilter(CustomerFilter);
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
                    });
                }
                excel.GenerateWorksheet("Customer", CustomerHeaders, CustomerData);
                #endregion
                
                #region BusinessType
                var BusinessTypeFilter = new BusinessTypeFilter();
                BusinessTypeFilter.Selects = BusinessTypeSelect.ALL;
                BusinessTypeFilter.OrderBy = BusinessTypeOrder.Id;
                BusinessTypeFilter.OrderType = OrderType.ASC;
                BusinessTypeFilter.Skip = 0;
                BusinessTypeFilter.Take = int.MaxValue;
                List<BusinessType> BusinessTypes = await BusinessTypeService.List(BusinessTypeFilter);

                var BusinessTypeHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> BusinessTypeData = new List<object[]>();
                for (int i = 0; i < BusinessTypes.Count; i++)
                {
                    var BusinessType = BusinessTypes[i];
                    BusinessTypeData.Add(new Object[]
                    {
                        BusinessType.Id,
                        BusinessType.Code,
                        BusinessType.Name,
                    });
                }
                excel.GenerateWorksheet("BusinessType", BusinessTypeHeaders, BusinessTypeData);
                #endregion
                #region Company
                var CompanyFilter = new CompanyFilter();
                CompanyFilter.Selects = CompanySelect.ALL;
                CompanyFilter.OrderBy = CompanyOrder.Id;
                CompanyFilter.OrderType = OrderType.ASC;
                CompanyFilter.Skip = 0;
                CompanyFilter.Take = int.MaxValue;
                List<Company> Companies = await CompanyService.List(CompanyFilter);

                var CompanyHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Name",
                        "Phone",
                        "FAX",
                        "PhoneOther",
                        "Email",
                        "EmailOther",
                        "ZIPCode",
                        "Revenue",
                        "Website",
                        "Address",
                        "NationId",
                        "ProvinceId",
                        "DistrictId",
                        "NumberOfEmployee",
                        "RefuseReciveEmail",
                        "RefuseReciveSMS",
                        "CustomerLeadId",
                        "ParentId",
                        "Path",
                        "Level",
                        "ProfessionId",
                        "AppUserId",
                        "CreatorId",
                        "CurrencyId",
                        "CompanyStatusId",
                        "Description",
                        "RowId",
                    }
                };
                List<object[]> CompanyData = new List<object[]>();
                for (int i = 0; i < Companies.Count; i++)
                {
                    var Company = Companies[i];
                    CompanyData.Add(new Object[]
                    {
                        Company.Id,
                        Company.Name,
                        Company.Phone,
                        Company.FAX,
                        Company.PhoneOther,
                        Company.Email,
                        Company.EmailOther,
                        Company.ZIPCode,
                        Company.Revenue,
                        Company.Website,
                        Company.Address,
                        Company.NationId,
                        Company.ProvinceId,
                        Company.DistrictId,
                        Company.NumberOfEmployee,
                        Company.RefuseReciveEmail,
                        Company.RefuseReciveSMS,
                        Company.CustomerLeadId,
                        Company.ParentId,
                        Company.Path,
                        Company.Level,
                        Company.ProfessionId,
                        Company.AppUserId,
                        Company.CreatorId,
                        Company.CurrencyId,
                        Company.CompanyStatusId,
                        Company.Description,
                        Company.RowId,
                    });
                }
                excel.GenerateWorksheet("Company", CompanyHeaders, CompanyData);
                #endregion
                #region CustomerResource
                var CustomerResourceFilter = new CustomerResourceFilter();
                CustomerResourceFilter.Selects = CustomerResourceSelect.ALL;
                CustomerResourceFilter.OrderBy = CustomerResourceOrder.Id;
                CustomerResourceFilter.OrderType = OrderType.ASC;
                CustomerResourceFilter.Skip = 0;
                CustomerResourceFilter.Take = int.MaxValue;
                List<CustomerResource> CustomerResources = await CustomerResourceService.List(CustomerResourceFilter);

                var CustomerResourceHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "StatusId",
                        "Description",
                        "Used",
                        "RowId",
                    }
                };
                List<object[]> CustomerResourceData = new List<object[]>();
                for (int i = 0; i < CustomerResources.Count; i++)
                {
                    var CustomerResource = CustomerResources[i];
                    CustomerResourceData.Add(new Object[]
                    {
                        CustomerResource.Id,
                        CustomerResource.Code,
                        CustomerResource.Name,
                        CustomerResource.StatusId,
                        CustomerResource.Description,
                        CustomerResource.Used,
                        CustomerResource.RowId,
                    });
                }
                excel.GenerateWorksheet("CustomerResource", CustomerResourceHeaders, CustomerResourceData);
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
                #region Profession
                var ProfessionFilter = new ProfessionFilter();
                ProfessionFilter.Selects = ProfessionSelect.ALL;
                ProfessionFilter.OrderBy = ProfessionOrder.Id;
                ProfessionFilter.OrderType = OrderType.ASC;
                ProfessionFilter.Skip = 0;
                ProfessionFilter.Take = int.MaxValue;
                List<Profession> Professions = await ProfessionService.List(ProfessionFilter);

                var ProfessionHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "StatusId",
                        "RowId",
                        "Used",
                    }
                };
                List<object[]> ProfessionData = new List<object[]>();
                for (int i = 0; i < Professions.Count; i++)
                {
                    var Profession = Professions[i];
                    ProfessionData.Add(new Object[]
                    {
                        Profession.Id,
                        Profession.Code,
                        Profession.Name,
                        Profession.StatusId,
                        Profession.RowId,
                        Profession.Used,
                    });
                }
                excel.GenerateWorksheet("Profession", ProfessionHeaders, ProfessionData);
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
                #region Sex
                var SexFilter = new SexFilter();
                SexFilter.Selects = SexSelect.ALL;
                SexFilter.OrderBy = SexOrder.Id;
                SexFilter.OrderType = OrderType.ASC;
                SexFilter.Skip = 0;
                SexFilter.Take = int.MaxValue;
                List<Sex> Sexes = await SexService.List(SexFilter);

                var SexHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> SexData = new List<object[]>();
                for (int i = 0; i < Sexes.Count; i++)
                {
                    var Sex = Sexes[i];
                    SexData.Add(new Object[]
                    {
                        Sex.Id,
                        Sex.Code,
                        Sex.Name,
                    });
                }
                excel.GenerateWorksheet("Sex", SexHeaders, SexData);
                #endregion
                #region Status
                var StatusFilter = new StatusFilter();
                StatusFilter.Selects = StatusSelect.ALL;
                StatusFilter.OrderBy = StatusOrder.Id;
                StatusFilter.OrderType = OrderType.ASC;
                StatusFilter.Skip = 0;
                StatusFilter.Take = int.MaxValue;
                List<Status> Statuses = await StatusService.List(StatusFilter);

                var StatusHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> StatusData = new List<object[]>();
                for (int i = 0; i < Statuses.Count; i++)
                {
                    var Status = Statuses[i];
                    StatusData.Add(new Object[]
                    {
                        Status.Id,
                        Status.Code,
                        Status.Name,
                    });
                }
                excel.GenerateWorksheet("Status", StatusHeaders, StatusData);
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
                #region CustomerEmail
                var CustomerEmailFilter = new CustomerEmailFilter();
                CustomerEmailFilter.Selects = CustomerEmailSelect.ALL;
                CustomerEmailFilter.OrderBy = CustomerEmailOrder.Id;
                CustomerEmailFilter.OrderType = OrderType.ASC;
                CustomerEmailFilter.Skip = 0;
                CustomerEmailFilter.Take = int.MaxValue;
                List<CustomerEmail> CustomerEmails = await CustomerEmailService.List(CustomerEmailFilter);

                var CustomerEmailHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "CustomerId",
                        "Email",
                        "EmailTypeId",
                    }
                };
                List<object[]> CustomerEmailData = new List<object[]>();
                for (int i = 0; i < CustomerEmails.Count; i++)
                {
                    var CustomerEmail = CustomerEmails[i];
                    CustomerEmailData.Add(new Object[]
                    {
                        CustomerEmail.Id,
                        CustomerEmail.CustomerId,
                        CustomerEmail.Email,
                        CustomerEmail.EmailTypeId,
                    });
                }
                excel.GenerateWorksheet("CustomerEmail", CustomerEmailHeaders, CustomerEmailData);
                #endregion
                #region EmailType
                var EmailTypeFilter = new EmailTypeFilter();
                EmailTypeFilter.Selects = EmailTypeSelect.ALL;
                EmailTypeFilter.OrderBy = EmailTypeOrder.Id;
                EmailTypeFilter.OrderType = OrderType.ASC;
                EmailTypeFilter.Skip = 0;
                EmailTypeFilter.Take = int.MaxValue;
                List<EmailType> EmailTypes = await EmailTypeService.List(EmailTypeFilter);

                var EmailTypeHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> EmailTypeData = new List<object[]>();
                for (int i = 0; i < EmailTypes.Count; i++)
                {
                    var EmailType = EmailTypes[i];
                    EmailTypeData.Add(new Object[]
                    {
                        EmailType.Id,
                        EmailType.Code,
                        EmailType.Name,
                    });
                }
                excel.GenerateWorksheet("EmailType", EmailTypeHeaders, EmailTypeData);
                #endregion
                #region CustomerFeedback
                var CustomerFeedbackFilter = new CustomerFeedbackFilter();
                CustomerFeedbackFilter.Selects = CustomerFeedbackSelect.ALL;
                CustomerFeedbackFilter.OrderBy = CustomerFeedbackOrder.Id;
                CustomerFeedbackFilter.OrderType = OrderType.ASC;
                CustomerFeedbackFilter.Skip = 0;
                CustomerFeedbackFilter.Take = int.MaxValue;
                List<CustomerFeedback> CustomerFeedbacks = await CustomerFeedbackService.List(CustomerFeedbackFilter);

                var CustomerFeedbackHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "IsSystemCustomer",
                        "CustomerId",
                        "FullName",
                        "Email",
                        "PhoneNumber",
                        "CustomerFeedbackTypeId",
                        "Title",
                        "SendDate",
                        "Content",
                        "StatusId",
                    }
                };
                List<object[]> CustomerFeedbackData = new List<object[]>();
                for (int i = 0; i < CustomerFeedbacks.Count; i++)
                {
                    var CustomerFeedback = CustomerFeedbacks[i];
                    CustomerFeedbackData.Add(new Object[]
                    {
                        CustomerFeedback.Id,
                        CustomerFeedback.IsSystemCustomer,
                        CustomerFeedback.CustomerId,
                        CustomerFeedback.FullName,
                        CustomerFeedback.Email,
                        CustomerFeedback.PhoneNumber,
                        CustomerFeedback.CustomerFeedbackTypeId,
                        CustomerFeedback.Title,
                        CustomerFeedback.SendDate,
                        CustomerFeedback.Content,
                        CustomerFeedback.StatusId,
                    });
                }
                excel.GenerateWorksheet("CustomerFeedback", CustomerFeedbackHeaders, CustomerFeedbackData);
                #endregion
                #region CustomerFeedbackType
                var CustomerFeedbackTypeFilter = new CustomerFeedbackTypeFilter();
                CustomerFeedbackTypeFilter.Selects = CustomerFeedbackTypeSelect.ALL;
                CustomerFeedbackTypeFilter.OrderBy = CustomerFeedbackTypeOrder.Id;
                CustomerFeedbackTypeFilter.OrderType = OrderType.ASC;
                CustomerFeedbackTypeFilter.Skip = 0;
                CustomerFeedbackTypeFilter.Take = int.MaxValue;
                List<CustomerFeedbackType> CustomerFeedbackTypes = await CustomerFeedbackTypeService.List(CustomerFeedbackTypeFilter);

                var CustomerFeedbackTypeHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> CustomerFeedbackTypeData = new List<object[]>();
                for (int i = 0; i < CustomerFeedbackTypes.Count; i++)
                {
                    var CustomerFeedbackType = CustomerFeedbackTypes[i];
                    CustomerFeedbackTypeData.Add(new Object[]
                    {
                        CustomerFeedbackType.Id,
                        CustomerFeedbackType.Code,
                        CustomerFeedbackType.Name,
                    });
                }
                excel.GenerateWorksheet("CustomerFeedbackType", CustomerFeedbackTypeHeaders, CustomerFeedbackTypeData);
                #endregion
                #region CustomerPhone
                var CustomerPhoneFilter = new CustomerPhoneFilter();
                CustomerPhoneFilter.Selects = CustomerPhoneSelect.ALL;
                CustomerPhoneFilter.OrderBy = CustomerPhoneOrder.Id;
                CustomerPhoneFilter.OrderType = OrderType.ASC;
                CustomerPhoneFilter.Skip = 0;
                CustomerPhoneFilter.Take = int.MaxValue;
                List<CustomerPhone> CustomerPhones = await CustomerPhoneService.List(CustomerPhoneFilter);

                var CustomerPhoneHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "CustomerId",
                        "Phone",
                        "PhoneTypeId",
                    }
                };
                List<object[]> CustomerPhoneData = new List<object[]>();
                for (int i = 0; i < CustomerPhones.Count; i++)
                {
                    var CustomerPhone = CustomerPhones[i];
                    CustomerPhoneData.Add(new Object[]
                    {
                        CustomerPhone.Id,
                        CustomerPhone.CustomerId,
                        CustomerPhone.Phone,
                        CustomerPhone.PhoneTypeId,
                    });
                }
                excel.GenerateWorksheet("CustomerPhone", CustomerPhoneHeaders, CustomerPhoneData);
                #endregion
                #region PhoneType
                var PhoneTypeFilter = new PhoneTypeFilter();
                PhoneTypeFilter.Selects = PhoneTypeSelect.ALL;
                PhoneTypeFilter.OrderBy = PhoneTypeOrder.Id;
                PhoneTypeFilter.OrderType = OrderType.ASC;
                PhoneTypeFilter.Skip = 0;
                PhoneTypeFilter.Take = int.MaxValue;
                List<PhoneType> PhoneTypes = await PhoneTypeService.List(PhoneTypeFilter);

                var PhoneTypeHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "StatusId",
                        "Used",
                        "RowId",
                    }
                };
                List<object[]> PhoneTypeData = new List<object[]>();
                for (int i = 0; i < PhoneTypes.Count; i++)
                {
                    var PhoneType = PhoneTypes[i];
                    PhoneTypeData.Add(new Object[]
                    {
                        PhoneType.Id,
                        PhoneType.Code,
                        PhoneType.Name,
                        PhoneType.StatusId,
                        PhoneType.Used,
                        PhoneType.RowId,
                    });
                }
                excel.GenerateWorksheet("PhoneType", PhoneTypeHeaders, PhoneTypeData);
                #endregion
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "Customer.xlsx");
        }

        [Route(CustomerRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] Customer_CustomerFilterDTO Customer_CustomerFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            string path = "Templates/Customer_Template.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "Customer.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter = await CustomerService.ToFilter(CustomerFilter);
            if (Id == 0)
            {

            }
            else
            {
                CustomerFilter.Id = new IdFilter { Equal = Id };
                int count = await CustomerService.Count(CustomerFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private Customer ConvertDTOToEntity(Customer_CustomerDTO Customer_CustomerDTO)
        {
            Customer Customer = new Customer();
            Customer.Id = Customer_CustomerDTO.Id;
            Customer.Code = Customer_CustomerDTO.Code;
            Customer.Name = Customer_CustomerDTO.Name;
            Customer.Phone = Customer_CustomerDTO.Phone;
            Customer.Address = Customer_CustomerDTO.Address;
            Customer.NationId = Customer_CustomerDTO.NationId;
            Customer.ProvinceId = Customer_CustomerDTO.ProvinceId;
            Customer.DistrictId = Customer_CustomerDTO.DistrictId;
            Customer.WardId = Customer_CustomerDTO.WardId;
            Customer.CustomerTypeId = Customer_CustomerDTO.CustomerTypeId;
            Customer.Birthday = Customer_CustomerDTO.Birthday;
            Customer.Email = Customer_CustomerDTO.Email;
            Customer.ProfessionId = Customer_CustomerDTO.ProfessionId;
            Customer.CustomerResourceId = Customer_CustomerDTO.CustomerResourceId;
            Customer.SexId = Customer_CustomerDTO.SexId;
            Customer.StatusId = Customer_CustomerDTO.StatusId;
            Customer.CompanyId = Customer_CustomerDTO.CompanyId;
            Customer.ParentCompanyId = Customer_CustomerDTO.ParentCompanyId;
            Customer.TaxCode = Customer_CustomerDTO.TaxCode;
            Customer.Fax = Customer_CustomerDTO.Fax;
            Customer.Website = Customer_CustomerDTO.Website;
            Customer.NumberOfEmployee = Customer_CustomerDTO.NumberOfEmployee;
            Customer.BusinessTypeId = Customer_CustomerDTO.BusinessTypeId;
            Customer.Investment = Customer_CustomerDTO.Investment;
            Customer.RevenueAnnual = Customer_CustomerDTO.RevenueAnnual;
            Customer.IsSupplier = Customer_CustomerDTO.IsSupplier;
            Customer.Descreption = Customer_CustomerDTO.Descreption;
            Customer.AppUserId = Customer_CustomerDTO.AppUserId;
            Customer.CreatorId = Customer_CustomerDTO.CreatorId;
            Customer.Used = Customer_CustomerDTO.Used;
            Customer.RowId = Customer_CustomerDTO.RowId;
            Customer.BusinessType = Customer_CustomerDTO.BusinessType == null ? null : new BusinessType
            {
                Id = Customer_CustomerDTO.BusinessType.Id,
                Code = Customer_CustomerDTO.BusinessType.Code,
                Name = Customer_CustomerDTO.BusinessType.Name,
            };
            Customer.Company = Customer_CustomerDTO.Company == null ? null : new Company
            {
                Id = Customer_CustomerDTO.Company.Id,
                Name = Customer_CustomerDTO.Company.Name,
                Phone = Customer_CustomerDTO.Company.Phone,
                FAX = Customer_CustomerDTO.Company.FAX,
                PhoneOther = Customer_CustomerDTO.Company.PhoneOther,
                Email = Customer_CustomerDTO.Company.Email,
                EmailOther = Customer_CustomerDTO.Company.EmailOther,
                ZIPCode = Customer_CustomerDTO.Company.ZIPCode,
                Revenue = Customer_CustomerDTO.Company.Revenue,
                Website = Customer_CustomerDTO.Company.Website,
                Address = Customer_CustomerDTO.Company.Address,
                NationId = Customer_CustomerDTO.Company.NationId,
                ProvinceId = Customer_CustomerDTO.Company.ProvinceId,
                DistrictId = Customer_CustomerDTO.Company.DistrictId,
                NumberOfEmployee = Customer_CustomerDTO.Company.NumberOfEmployee,
                RefuseReciveEmail = Customer_CustomerDTO.Company.RefuseReciveEmail,
                RefuseReciveSMS = Customer_CustomerDTO.Company.RefuseReciveSMS,
                CustomerLeadId = Customer_CustomerDTO.Company.CustomerLeadId,
                ParentId = Customer_CustomerDTO.Company.ParentId,
                Path = Customer_CustomerDTO.Company.Path,
                Level = Customer_CustomerDTO.Company.Level,
                ProfessionId = Customer_CustomerDTO.Company.ProfessionId,
                AppUserId = Customer_CustomerDTO.Company.AppUserId,
                CreatorId = Customer_CustomerDTO.Company.CreatorId,
                CurrencyId = Customer_CustomerDTO.Company.CurrencyId,
                CompanyStatusId = Customer_CustomerDTO.Company.CompanyStatusId,
                Description = Customer_CustomerDTO.Company.Description,
                RowId = Customer_CustomerDTO.Company.RowId,
            };
            Customer.AppUser = Customer_CustomerDTO.AppUser == null ? null : new AppUser
            {
                Id = Customer_CustomerDTO.AppUser.Id,
                Username = Customer_CustomerDTO.AppUser.Username,
                DisplayName = Customer_CustomerDTO.AppUser.DisplayName,
                Address = Customer_CustomerDTO.AppUser.Address,
                Email = Customer_CustomerDTO.AppUser.Email,
                Phone = Customer_CustomerDTO.AppUser.Phone,
                SexId = Customer_CustomerDTO.AppUser.SexId,
                Birthday = Customer_CustomerDTO.AppUser.Birthday,
                Avatar = Customer_CustomerDTO.AppUser.Avatar,
                Department = Customer_CustomerDTO.AppUser.Department,
                OrganizationId = Customer_CustomerDTO.AppUser.OrganizationId,
                Longitude = Customer_CustomerDTO.AppUser.Longitude,
                Latitude = Customer_CustomerDTO.AppUser.Latitude,
                StatusId = Customer_CustomerDTO.AppUser.StatusId,
                RowId = Customer_CustomerDTO.AppUser.RowId,
                Used = Customer_CustomerDTO.AppUser.Used,
            };
            Customer.Creator = Customer_CustomerDTO.Creator == null ? null : new AppUser
            {
                Id = Customer_CustomerDTO.Creator.Id,
                Username = Customer_CustomerDTO.Creator.Username,
                DisplayName = Customer_CustomerDTO.Creator.DisplayName,
                Address = Customer_CustomerDTO.Creator.Address,
                Email = Customer_CustomerDTO.Creator.Email,
                Phone = Customer_CustomerDTO.Creator.Phone,
                SexId = Customer_CustomerDTO.Creator.SexId,
                Birthday = Customer_CustomerDTO.Creator.Birthday,
                Avatar = Customer_CustomerDTO.Creator.Avatar,
                Department = Customer_CustomerDTO.Creator.Department,
                OrganizationId = Customer_CustomerDTO.Creator.OrganizationId,
                Longitude = Customer_CustomerDTO.Creator.Longitude,
                Latitude = Customer_CustomerDTO.Creator.Latitude,
                StatusId = Customer_CustomerDTO.Creator.StatusId,
                RowId = Customer_CustomerDTO.Creator.RowId,
                Used = Customer_CustomerDTO.Creator.Used,
            };
            Customer.CustomerResource = Customer_CustomerDTO.CustomerResource == null ? null : new CustomerResource
            {
                Id = Customer_CustomerDTO.CustomerResource.Id,
                Code = Customer_CustomerDTO.CustomerResource.Code,
                Name = Customer_CustomerDTO.CustomerResource.Name,
                StatusId = Customer_CustomerDTO.CustomerResource.StatusId,
                Description = Customer_CustomerDTO.CustomerResource.Description,
                Used = Customer_CustomerDTO.CustomerResource.Used,
                RowId = Customer_CustomerDTO.CustomerResource.RowId,
            };
            Customer.CustomerType = Customer_CustomerDTO.CustomerType == null ? null : new CustomerType
            {
                Id = Customer_CustomerDTO.CustomerType.Id,
                Code = Customer_CustomerDTO.CustomerType.Code,
                Name = Customer_CustomerDTO.CustomerType.Name,
            };
            Customer.District = Customer_CustomerDTO.District == null ? null : new District
            {
                Id = Customer_CustomerDTO.District.Id,
                Code = Customer_CustomerDTO.District.Code,
                Name = Customer_CustomerDTO.District.Name,
                Priority = Customer_CustomerDTO.District.Priority,
                ProvinceId = Customer_CustomerDTO.District.ProvinceId,
                StatusId = Customer_CustomerDTO.District.StatusId,
                RowId = Customer_CustomerDTO.District.RowId,
                Used = Customer_CustomerDTO.District.Used,
            };
            Customer.Nation = Customer_CustomerDTO.Nation == null ? null : new Nation
            {
                Id = Customer_CustomerDTO.Nation.Id,
                Code = Customer_CustomerDTO.Nation.Code,
                Name = Customer_CustomerDTO.Nation.Name,
                Priority = Customer_CustomerDTO.Nation.Priority,
                StatusId = Customer_CustomerDTO.Nation.StatusId,
                Used = Customer_CustomerDTO.Nation.Used,
                RowId = Customer_CustomerDTO.Nation.RowId,
            };
            Customer.ParentCompany = Customer_CustomerDTO.ParentCompany == null ? null : new Company
            {
                Id = Customer_CustomerDTO.ParentCompany.Id,
                Name = Customer_CustomerDTO.ParentCompany.Name,
                Phone = Customer_CustomerDTO.ParentCompany.Phone,
                FAX = Customer_CustomerDTO.ParentCompany.FAX,
                PhoneOther = Customer_CustomerDTO.ParentCompany.PhoneOther,
                Email = Customer_CustomerDTO.ParentCompany.Email,
                EmailOther = Customer_CustomerDTO.ParentCompany.EmailOther,
                ZIPCode = Customer_CustomerDTO.ParentCompany.ZIPCode,
                Revenue = Customer_CustomerDTO.ParentCompany.Revenue,
                Website = Customer_CustomerDTO.ParentCompany.Website,
                Address = Customer_CustomerDTO.ParentCompany.Address,
                NationId = Customer_CustomerDTO.ParentCompany.NationId,
                ProvinceId = Customer_CustomerDTO.ParentCompany.ProvinceId,
                DistrictId = Customer_CustomerDTO.ParentCompany.DistrictId,
                NumberOfEmployee = Customer_CustomerDTO.ParentCompany.NumberOfEmployee,
                RefuseReciveEmail = Customer_CustomerDTO.ParentCompany.RefuseReciveEmail,
                RefuseReciveSMS = Customer_CustomerDTO.ParentCompany.RefuseReciveSMS,
                CustomerLeadId = Customer_CustomerDTO.ParentCompany.CustomerLeadId,
                ParentId = Customer_CustomerDTO.ParentCompany.ParentId,
                Path = Customer_CustomerDTO.ParentCompany.Path,
                Level = Customer_CustomerDTO.ParentCompany.Level,
                ProfessionId = Customer_CustomerDTO.ParentCompany.ProfessionId,
                AppUserId = Customer_CustomerDTO.ParentCompany.AppUserId,
                CreatorId = Customer_CustomerDTO.ParentCompany.CreatorId,
                CurrencyId = Customer_CustomerDTO.ParentCompany.CurrencyId,
                CompanyStatusId = Customer_CustomerDTO.ParentCompany.CompanyStatusId,
                Description = Customer_CustomerDTO.ParentCompany.Description,
                RowId = Customer_CustomerDTO.ParentCompany.RowId,
            };
            Customer.Profession = Customer_CustomerDTO.Profession == null ? null : new Profession
            {
                Id = Customer_CustomerDTO.Profession.Id,
                Code = Customer_CustomerDTO.Profession.Code,
                Name = Customer_CustomerDTO.Profession.Name,
                StatusId = Customer_CustomerDTO.Profession.StatusId,
                RowId = Customer_CustomerDTO.Profession.RowId,
                Used = Customer_CustomerDTO.Profession.Used,
            };
            Customer.Province = Customer_CustomerDTO.Province == null ? null : new Province
            {
                Id = Customer_CustomerDTO.Province.Id,
                Code = Customer_CustomerDTO.Province.Code,
                Name = Customer_CustomerDTO.Province.Name,
                Priority = Customer_CustomerDTO.Province.Priority,
                StatusId = Customer_CustomerDTO.Province.StatusId,
                RowId = Customer_CustomerDTO.Province.RowId,
                Used = Customer_CustomerDTO.Province.Used,
            };
            Customer.Sex = Customer_CustomerDTO.Sex == null ? null : new Sex
            {
                Id = Customer_CustomerDTO.Sex.Id,
                Code = Customer_CustomerDTO.Sex.Code,
                Name = Customer_CustomerDTO.Sex.Name,
            };
            Customer.Status = Customer_CustomerDTO.Status == null ? null : new Status
            {
                Id = Customer_CustomerDTO.Status.Id,
                Code = Customer_CustomerDTO.Status.Code,
                Name = Customer_CustomerDTO.Status.Name,
            };
            Customer.Ward = Customer_CustomerDTO.Ward == null ? null : new Ward
            {
                Id = Customer_CustomerDTO.Ward.Id,
                Code = Customer_CustomerDTO.Ward.Code,
                Name = Customer_CustomerDTO.Ward.Name,
                Priority = Customer_CustomerDTO.Ward.Priority,
                DistrictId = Customer_CustomerDTO.Ward.DistrictId,
                StatusId = Customer_CustomerDTO.Ward.StatusId,
                RowId = Customer_CustomerDTO.Ward.RowId,
                Used = Customer_CustomerDTO.Ward.Used,
            };
            Customer.CustomerCustomerGroupingMappings = Customer_CustomerDTO.CustomerCustomerGroupingMappings?
                .Select(x => new CustomerCustomerGroupingMapping
                {
                    CustomerGroupingId = x.CustomerGroupingId,
                    CustomerGrouping = x.CustomerGrouping == null ? null : new CustomerGrouping
                    {
                        Id = x.CustomerGrouping.Id,
                        Code = x.CustomerGrouping.Code,
                        Name = x.CustomerGrouping.Name,
                        CustomerTypeId = x.CustomerGrouping.CustomerTypeId,
                        ParentId = x.CustomerGrouping.ParentId,
                        Path = x.CustomerGrouping.Path,
                        Level = x.CustomerGrouping.Level,
                        StatusId = x.CustomerGrouping.StatusId,
                        Description = x.CustomerGrouping.Description,
                    },
                }).ToList();
            Customer.CustomerEmails = Customer_CustomerDTO.CustomerEmails?
                .Select(x => new CustomerEmail
                {
                    Id = x.Id,
                    Email = x.Email,
                    EmailTypeId = x.EmailTypeId,
                    EmailType = x.EmailType == null ? null : new EmailType
                    {
                        Id = x.EmailType.Id,
                        Code = x.EmailType.Code,
                        Name = x.EmailType.Name,
                    },
                }).ToList();
            Customer.CustomerPhones = Customer_CustomerDTO.CustomerPhones?
                .Select(x => new CustomerPhone
                {
                    Id = x.Id,
                    Phone = x.Phone,
                    PhoneTypeId = x.PhoneTypeId,
                    PhoneType = x.PhoneType == null ? null : new PhoneType
                    {
                        Id = x.PhoneType.Id,
                        Code = x.PhoneType.Code,
                        Name = x.PhoneType.Name,
                        StatusId = x.PhoneType.StatusId,
                        Used = x.PhoneType.Used,
                        RowId = x.PhoneType.RowId,
                    },
                }).ToList();
            Customer.BaseLanguage = CurrentContext.Language;
            return Customer;
        }

        private CustomerFilter ConvertFilterDTOToFilterEntity(Customer_CustomerFilterDTO Customer_CustomerFilterDTO)
        {
            CustomerFilter CustomerFilter = new CustomerFilter();
            CustomerFilter.Selects = CustomerSelect.ALL;
            CustomerFilter.Skip = Customer_CustomerFilterDTO.Skip;
            CustomerFilter.Take = Customer_CustomerFilterDTO.Take;
            CustomerFilter.OrderBy = Customer_CustomerFilterDTO.OrderBy;
            CustomerFilter.OrderType = Customer_CustomerFilterDTO.OrderType;

            CustomerFilter.Id = Customer_CustomerFilterDTO.Id;
            CustomerFilter.Code = Customer_CustomerFilterDTO.Code;
            CustomerFilter.Name = Customer_CustomerFilterDTO.Name;
            CustomerFilter.Phone = Customer_CustomerFilterDTO.Phone;
            CustomerFilter.Address = Customer_CustomerFilterDTO.Address;
            CustomerFilter.NationId = Customer_CustomerFilterDTO.NationId;
            CustomerFilter.ProvinceId = Customer_CustomerFilterDTO.ProvinceId;
            CustomerFilter.DistrictId = Customer_CustomerFilterDTO.DistrictId;
            CustomerFilter.WardId = Customer_CustomerFilterDTO.WardId;
            CustomerFilter.CustomerTypeId = Customer_CustomerFilterDTO.CustomerTypeId;
            CustomerFilter.Birthday = Customer_CustomerFilterDTO.Birthday;
            CustomerFilter.Email = Customer_CustomerFilterDTO.Email;
            CustomerFilter.ProfessionId = Customer_CustomerFilterDTO.ProfessionId;
            CustomerFilter.CustomerResourceId = Customer_CustomerFilterDTO.CustomerResourceId;
            CustomerFilter.SexId = Customer_CustomerFilterDTO.SexId;
            CustomerFilter.StatusId = Customer_CustomerFilterDTO.StatusId;
            CustomerFilter.CompanyId = Customer_CustomerFilterDTO.CompanyId;
            CustomerFilter.ParentCompanyId = Customer_CustomerFilterDTO.ParentCompanyId;
            CustomerFilter.TaxCode = Customer_CustomerFilterDTO.TaxCode;
            CustomerFilter.Fax = Customer_CustomerFilterDTO.Fax;
            CustomerFilter.Website = Customer_CustomerFilterDTO.Website;
            CustomerFilter.NumberOfEmployee = Customer_CustomerFilterDTO.NumberOfEmployee;
            CustomerFilter.BusinessTypeId = Customer_CustomerFilterDTO.BusinessTypeId;
            CustomerFilter.Investment = Customer_CustomerFilterDTO.Investment;
            CustomerFilter.RevenueAnnual = Customer_CustomerFilterDTO.RevenueAnnual;
            CustomerFilter.Descreption = Customer_CustomerFilterDTO.Descreption;
            CustomerFilter.AppUserId = Customer_CustomerFilterDTO.AppUserId;
            CustomerFilter.CreatorId = Customer_CustomerFilterDTO.CreatorId;
            CustomerFilter.CreatedAt = Customer_CustomerFilterDTO.CreatedAt;
            CustomerFilter.UpdatedAt = Customer_CustomerFilterDTO.UpdatedAt;
            return CustomerFilter;
        }

        #region Email
        [Route(CustomerRoute.CreateEmail), HttpPost]
        public async Task<ActionResult<Customer_CustomerEmailHistoryDTO>> CreateEmail([FromBody] Customer_CustomerEmailHistoryDTO Customer_CustomerEmailHistoryDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerEmailHistory CustomerEmailHistory = ConvertCustomerEmailHistory(Customer_CustomerEmailHistoryDTO);
            CustomerEmailHistory = await CustomerEmailHistoryService.Create(CustomerEmailHistory);
            Customer_CustomerEmailHistoryDTO = new Customer_CustomerEmailHistoryDTO(CustomerEmailHistory);
            if (CustomerEmailHistory.IsValidated)
                return Customer_CustomerEmailHistoryDTO;
            else
                return BadRequest(Customer_CustomerEmailHistoryDTO);
        }

        [Route(CustomerRoute.UpdateEmail), HttpPost]
        public async Task<ActionResult<Customer_CustomerEmailHistoryDTO>> UpdateEmail([FromBody] Customer_CustomerEmailHistoryDTO Customer_CustomerEmailHistoryDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerEmailHistory CustomerEmailHistory = ConvertCustomerEmailHistory(Customer_CustomerEmailHistoryDTO);
            CustomerEmailHistory = await CustomerEmailHistoryService.Update(CustomerEmailHistory);
            Customer_CustomerEmailHistoryDTO = new Customer_CustomerEmailHistoryDTO(CustomerEmailHistory);
            if (CustomerEmailHistory.IsValidated)
                return Customer_CustomerEmailHistoryDTO;
            else
                return BadRequest(Customer_CustomerEmailHistoryDTO);
        }

        [Route(CustomerRoute.SendEmail), HttpPost]
        public async Task<ActionResult<bool>> SendEmail([FromBody] Customer_CustomerEmailHistoryDTO Customer_CustomerEmailHistoryDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            CustomerEmailHistory CustomerEmailHistory = ConvertCustomerEmailHistory(Customer_CustomerEmailHistoryDTO);
            CustomerEmailHistory = await CustomerEmailHistoryService.Send(CustomerEmailHistory);
            if (CustomerEmailHistory.IsValidated)
                return Ok();
            else
                return BadRequest(Customer_CustomerEmailHistoryDTO);
        }

        private CustomerEmailHistory ConvertCustomerEmailHistory(Customer_CustomerEmailHistoryDTO Customer_CustomerEmailHistoryDTO)
        {
            CustomerEmailHistory CustomerEmailHistory = new CustomerEmailHistory();
            CustomerEmailHistory.Id = Customer_CustomerEmailHistoryDTO.Id;
            CustomerEmailHistory.Reciepient = Customer_CustomerEmailHistoryDTO.Reciepient;
            CustomerEmailHistory.Title = Customer_CustomerEmailHistoryDTO.Title;
            CustomerEmailHistory.Content = Customer_CustomerEmailHistoryDTO.Content;
            CustomerEmailHistory.CreatorId = Customer_CustomerEmailHistoryDTO.CreatorId;
            CustomerEmailHistory.CustomerId = Customer_CustomerEmailHistoryDTO.CustomerId;
            CustomerEmailHistory.EmailStatusId = Customer_CustomerEmailHistoryDTO.EmailStatusId;
            CustomerEmailHistory.EmailStatus = Customer_CustomerEmailHistoryDTO.EmailStatus == null ? null : new EmailStatus
            {
                Id = Customer_CustomerEmailHistoryDTO.EmailStatus.Id,
                Code = Customer_CustomerEmailHistoryDTO.EmailStatus.Code,
                Name = Customer_CustomerEmailHistoryDTO.EmailStatus.Name,
            };
            CustomerEmailHistory.CustomerCCEmailHistories = Customer_CustomerEmailHistoryDTO.CustomerCCEmailHistories?.Select(x => new CustomerCCEmailHistory
            {
                Id = x.Id,
                CustomerEmailHistoryId = x.CustomerEmailHistoryId,
                CCEmail = x.CCEmail
            }).ToList();
            CustomerEmailHistory.BaseLanguage = CurrentContext.Language;
            return CustomerEmailHistory;
        }
        #endregion

        [Route(CustomerRoute.CreateCustomerPointHistory), HttpPost]
        public async Task<ActionResult<Customer_CustomerPointHistoryDTO>> CreateCustomerPointHistory([FromBody] Customer_CustomerPointHistoryDTO Customer_CustomerPointHistoryDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerPointHistory CustomerPointHistory = ConvertCustomerPointHistory(Customer_CustomerPointHistoryDTO);
            CustomerPointHistory = await CustomerPointHistoryService.Create(CustomerPointHistory);
            Customer_CustomerPointHistoryDTO = new Customer_CustomerPointHistoryDTO(CustomerPointHistory);
            if (CustomerPointHistory.IsValidated)
                return Customer_CustomerPointHistoryDTO;
            else
                return BadRequest(Customer_CustomerPointHistoryDTO);
        }

        private CustomerPointHistory ConvertCustomerPointHistory(Customer_CustomerPointHistoryDTO Customer_CustomerPointHistoryDTO)
        {
            CustomerPointHistory CustomerPointHistory = new CustomerPointHistory();
            CustomerPointHistory.Id = Customer_CustomerPointHistoryDTO.Id;
            CustomerPointHistory.CustomerId = Customer_CustomerPointHistoryDTO.CustomerId;
            CustomerPointHistory.TotalPoint = Customer_CustomerPointHistoryDTO.TotalPoint;
            CustomerPointHistory.CurrentPoint = Customer_CustomerPointHistoryDTO.CurrentPoint;
            CustomerPointHistory.ChangePoint = Customer_CustomerPointHistoryDTO.ChangePoint;
            CustomerPointHistory.IsIncrease = Customer_CustomerPointHistoryDTO.IsIncrease;
            CustomerPointHistory.Description = Customer_CustomerPointHistoryDTO.Description;
            CustomerPointHistory.ReduceTotal = Customer_CustomerPointHistoryDTO.ReduceTotal;
            CustomerPointHistory.Customer = Customer_CustomerPointHistoryDTO.Customer == null ? null : new Customer
            {
                Id = Customer_CustomerPointHistoryDTO.Customer.Id,
                Code = Customer_CustomerPointHistoryDTO.Customer.Code,
                Name = Customer_CustomerPointHistoryDTO.Customer.Name,
                Phone = Customer_CustomerPointHistoryDTO.Customer.Phone,
                Address = Customer_CustomerPointHistoryDTO.Customer.Address,
                NationId = Customer_CustomerPointHistoryDTO.Customer.NationId,
                ProvinceId = Customer_CustomerPointHistoryDTO.Customer.ProvinceId,
                DistrictId = Customer_CustomerPointHistoryDTO.Customer.DistrictId,
                WardId = Customer_CustomerPointHistoryDTO.Customer.WardId,
                CustomerTypeId = Customer_CustomerPointHistoryDTO.Customer.CustomerTypeId,
                Birthday = Customer_CustomerPointHistoryDTO.Customer.Birthday,
                Email = Customer_CustomerPointHistoryDTO.Customer.Email,
                ProfessionId = Customer_CustomerPointHistoryDTO.Customer.ProfessionId,
                CustomerResourceId = Customer_CustomerPointHistoryDTO.Customer.CustomerResourceId,
                SexId = Customer_CustomerPointHistoryDTO.Customer.SexId,
                StatusId = Customer_CustomerPointHistoryDTO.Customer.StatusId,
                CompanyId = Customer_CustomerPointHistoryDTO.Customer.CompanyId,
                ParentCompanyId = Customer_CustomerPointHistoryDTO.Customer.ParentCompanyId,
                TaxCode = Customer_CustomerPointHistoryDTO.Customer.TaxCode,
                Fax = Customer_CustomerPointHistoryDTO.Customer.Fax,
                Website = Customer_CustomerPointHistoryDTO.Customer.Website,
                NumberOfEmployee = Customer_CustomerPointHistoryDTO.Customer.NumberOfEmployee,
                BusinessTypeId = Customer_CustomerPointHistoryDTO.Customer.BusinessTypeId,
                Investment = Customer_CustomerPointHistoryDTO.Customer.Investment,
                RevenueAnnual = Customer_CustomerPointHistoryDTO.Customer.RevenueAnnual,
                IsSupplier = Customer_CustomerPointHistoryDTO.Customer.IsSupplier,
                Descreption = Customer_CustomerPointHistoryDTO.Customer.Descreption,
                CreatorId = Customer_CustomerPointHistoryDTO.Customer.CreatorId,
                Used = Customer_CustomerPointHistoryDTO.Customer.Used,
                RowId = Customer_CustomerPointHistoryDTO.Customer.RowId,
            };
            CustomerPointHistory.BaseLanguage = CurrentContext.Language;
            return CustomerPointHistory;
        }

        [Route(CustomerRoute.CreateContract), HttpPost]
        public async Task<ActionResult<Customer_ContractDTO>> CreateContract([FromBody] Customer_ContractDTO Customer_ContractDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Contract Contract = ConvertContract(Customer_ContractDTO);
            Contract = await ContractService.Create(Contract);
            Customer_ContractDTO = new Customer_ContractDTO(Contract);
            if (Contract.IsValidated)
                return Customer_ContractDTO;
            else
                return BadRequest(Customer_ContractDTO);
        }

        [Route(CustomerRoute.UpdateContract), HttpPost]
        public async Task<ActionResult<Customer_ContractDTO>> UpdateContract([FromBody] Customer_ContractDTO Customer_ContractDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Contract Contract = ConvertContract(Customer_ContractDTO);
            Contract = await ContractService.Update(Contract);
            Customer_ContractDTO = new Customer_ContractDTO(Contract);
            if (Contract.IsValidated)
                return Customer_ContractDTO;
            else
                return BadRequest(Customer_ContractDTO);
        }

        [Route(CustomerRoute.DeleteContract), HttpPost]
        public async Task<ActionResult<Customer_ContractDTO>> DeleteContract([FromBody] Customer_ContractDTO Customer_ContractDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Contract Contract = ConvertContract(Customer_ContractDTO);
            Contract = await ContractService.Delete(Contract);
            Customer_ContractDTO = new Customer_ContractDTO(Contract);
            if (Contract.IsValidated)
                return Customer_ContractDTO;
            else
                return BadRequest(Customer_ContractDTO);
        }

        [Route(CustomerRoute.BulkDeleteContract), HttpPost]
        public async Task<ActionResult<bool>> BulkDeleteContract([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContractFilter ContractFilter = new ContractFilter();
            ContractFilter = await ContractService.ToFilter(ContractFilter);
            ContractFilter.Id = new IdFilter { In = Ids };
            ContractFilter.Selects = ContractSelect.Id;
            ContractFilter.Skip = 0;
            ContractFilter.Take = int.MaxValue;

            List<Contract> Contracts = await ContractService.List(ContractFilter);
            Contracts = await ContractService.BulkDelete(Contracts);
            if (Contracts.Any(x => !x.IsValidated))
                return BadRequest(Contracts.Where(x => !x.IsValidated));
            return true;
        }

        private Contract ConvertContract(Customer_ContractDTO Customer_ContractDTO)
        {
            Contract Contract = new Contract();
            Contract.Id = Customer_ContractDTO.Id;
            Contract.Code = Customer_ContractDTO.Code;
            Contract.Name = Customer_ContractDTO.Name;
            Contract.CompanyId = Customer_ContractDTO.CompanyId;
            Contract.OpportunityId = Customer_ContractDTO.OpportunityId;
            Contract.CustomerId = Customer_ContractDTO.CustomerId;
            Contract.ContractTypeId = Customer_ContractDTO.ContractTypeId;
            Contract.TotalValue = Customer_ContractDTO.TotalValue;
            Contract.CurrencyId = Customer_ContractDTO.CurrencyId;
            Contract.ValidityDate = Customer_ContractDTO.ValidityDate;
            Contract.ExpirationDate = Customer_ContractDTO.ExpirationDate;
            Contract.AppUserId = Customer_ContractDTO.AppUserId;
            Contract.DeliveryUnit = Customer_ContractDTO.DeliveryUnit;
            Contract.ContractStatusId = Customer_ContractDTO.ContractStatusId;
            Contract.PaymentStatusId = Customer_ContractDTO.PaymentStatusId;
            Contract.InvoiceAddress = Customer_ContractDTO.InvoiceAddress;
            Contract.InvoiceNationId = Customer_ContractDTO.InvoiceNationId;
            Contract.InvoiceProvinceId = Customer_ContractDTO.InvoiceProvinceId;
            Contract.InvoiceDistrictId = Customer_ContractDTO.InvoiceDistrictId;
            Contract.InvoiceZipCode = Customer_ContractDTO.InvoiceZipCode;
            Contract.ReceiveAddress = Customer_ContractDTO.ReceiveAddress;
            Contract.ReceiveNationId = Customer_ContractDTO.ReceiveNationId;
            Contract.ReceiveProvinceId = Customer_ContractDTO.ReceiveProvinceId;
            Contract.ReceiveDistrictId = Customer_ContractDTO.ReceiveDistrictId;
            Contract.ReceiveZipCode = Customer_ContractDTO.ReceiveZipCode;
            Contract.SubTotal = Customer_ContractDTO.SubTotal;
            Contract.GeneralDiscountPercentage = Customer_ContractDTO.GeneralDiscountPercentage;
            Contract.GeneralDiscountAmount = Customer_ContractDTO.GeneralDiscountAmount;
            Contract.TotalTaxAmountOther = Customer_ContractDTO.TotalTaxAmountOther;
            Contract.TotalTaxAmount = Customer_ContractDTO.TotalTaxAmount;
            Contract.Total = Customer_ContractDTO.Total;
            Contract.TermAndCondition = Customer_ContractDTO.TermAndCondition;
            Contract.CreatorId = Customer_ContractDTO.CreatorId;
            Contract.OrganizationId = Customer_ContractDTO.OrganizationId;
            Contract.AppUser = Customer_ContractDTO.AppUser == null ? null : new AppUser
            {
                Id = Customer_ContractDTO.AppUser.Id,
                Username = Customer_ContractDTO.AppUser.Username,
                DisplayName = Customer_ContractDTO.AppUser.DisplayName,
                Address = Customer_ContractDTO.AppUser.Address,
                Email = Customer_ContractDTO.AppUser.Email,
                Phone = Customer_ContractDTO.AppUser.Phone,
                SexId = Customer_ContractDTO.AppUser.SexId,
                Birthday = Customer_ContractDTO.AppUser.Birthday,
                Avatar = Customer_ContractDTO.AppUser.Avatar,
                Department = Customer_ContractDTO.AppUser.Department,
                OrganizationId = Customer_ContractDTO.AppUser.OrganizationId,
                Longitude = Customer_ContractDTO.AppUser.Longitude,
                Latitude = Customer_ContractDTO.AppUser.Latitude,
                StatusId = Customer_ContractDTO.AppUser.StatusId,
            };
            Contract.Company = Customer_ContractDTO.Company == null ? null : new Company
            {
                Id = Customer_ContractDTO.Company.Id,
                Name = Customer_ContractDTO.Company.Name,
                Phone = Customer_ContractDTO.Company.Phone,
                FAX = Customer_ContractDTO.Company.FAX,
                PhoneOther = Customer_ContractDTO.Company.PhoneOther,
                Email = Customer_ContractDTO.Company.Email,
                EmailOther = Customer_ContractDTO.Company.EmailOther,
                ZIPCode = Customer_ContractDTO.Company.ZIPCode,
                Revenue = Customer_ContractDTO.Company.Revenue,
                Website = Customer_ContractDTO.Company.Website,
                Address = Customer_ContractDTO.Company.Address,
                NationId = Customer_ContractDTO.Company.NationId,
                ProvinceId = Customer_ContractDTO.Company.ProvinceId,
                DistrictId = Customer_ContractDTO.Company.DistrictId,
                NumberOfEmployee = Customer_ContractDTO.Company.NumberOfEmployee,
                RefuseReciveEmail = Customer_ContractDTO.Company.RefuseReciveEmail,
                RefuseReciveSMS = Customer_ContractDTO.Company.RefuseReciveSMS,
                CustomerLeadId = Customer_ContractDTO.Company.CustomerLeadId,
                ParentId = Customer_ContractDTO.Company.ParentId,
                Path = Customer_ContractDTO.Company.Path,
                Level = Customer_ContractDTO.Company.Level,
                ProfessionId = Customer_ContractDTO.Company.ProfessionId,
                AppUserId = Customer_ContractDTO.Company.AppUserId,
                CurrencyId = Customer_ContractDTO.Company.CurrencyId,
                CompanyStatusId = Customer_ContractDTO.Company.CompanyStatusId,
                Description = Customer_ContractDTO.Company.Description,
                RowId = Customer_ContractDTO.Company.RowId,
            };
            Contract.ContractStatus = Customer_ContractDTO.ContractStatus == null ? null : new ContractStatus
            {
                Id = Customer_ContractDTO.ContractStatus.Id,
                Name = Customer_ContractDTO.ContractStatus.Name,
                Code = Customer_ContractDTO.ContractStatus.Code,
            };
            Contract.ContractType = Customer_ContractDTO.ContractType == null ? null : new ContractType
            {
                Id = Customer_ContractDTO.ContractType.Id,
                Name = Customer_ContractDTO.ContractType.Name,
                Code = Customer_ContractDTO.ContractType.Code,
            };
            Contract.Creator = Customer_ContractDTO.Creator == null ? null : new AppUser
            {
                Id = Customer_ContractDTO.Creator.Id,
                Username = Customer_ContractDTO.Creator.Username,
                DisplayName = Customer_ContractDTO.Creator.DisplayName,
                Address = Customer_ContractDTO.Creator.Address,
                Email = Customer_ContractDTO.Creator.Email,
                Phone = Customer_ContractDTO.Creator.Phone,
                SexId = Customer_ContractDTO.Creator.SexId,
                Birthday = Customer_ContractDTO.Creator.Birthday,
                Avatar = Customer_ContractDTO.Creator.Avatar,
                Department = Customer_ContractDTO.Creator.Department,
                OrganizationId = Customer_ContractDTO.Creator.OrganizationId,
                Longitude = Customer_ContractDTO.Creator.Longitude,
                Latitude = Customer_ContractDTO.Creator.Latitude,
                StatusId = Customer_ContractDTO.Creator.StatusId,
            };
            Contract.Currency = Customer_ContractDTO.Currency == null ? null : new Currency
            {
                Id = Customer_ContractDTO.Currency.Id,
                Code = Customer_ContractDTO.Currency.Code,
                Name = Customer_ContractDTO.Currency.Name,
            };
            Contract.Customer = Customer_ContractDTO.Customer == null ? null : new Customer
            {
                Id = Customer_ContractDTO.Customer.Id,
                Code = Customer_ContractDTO.Customer.Code,
                Name = Customer_ContractDTO.Customer.Name,
                Phone = Customer_ContractDTO.Customer.Phone,
                Address = Customer_ContractDTO.Customer.Address,
                NationId = Customer_ContractDTO.Customer.NationId,
                ProvinceId = Customer_ContractDTO.Customer.ProvinceId,
                DistrictId = Customer_ContractDTO.Customer.DistrictId,
                WardId = Customer_ContractDTO.Customer.WardId,
                CustomerTypeId = Customer_ContractDTO.Customer.CustomerTypeId,
                Birthday = Customer_ContractDTO.Customer.Birthday,
                Email = Customer_ContractDTO.Customer.Email,
                ProfessionId = Customer_ContractDTO.Customer.ProfessionId,
                CustomerResourceId = Customer_ContractDTO.Customer.CustomerResourceId,
                SexId = Customer_ContractDTO.Customer.SexId,
                StatusId = Customer_ContractDTO.Customer.StatusId,
                CompanyId = Customer_ContractDTO.Customer.CompanyId,
                ParentCompanyId = Customer_ContractDTO.Customer.ParentCompanyId,
                TaxCode = Customer_ContractDTO.Customer.TaxCode,
                Fax = Customer_ContractDTO.Customer.Fax,
                Website = Customer_ContractDTO.Customer.Website,
                NumberOfEmployee = Customer_ContractDTO.Customer.NumberOfEmployee,
                BusinessTypeId = Customer_ContractDTO.Customer.BusinessTypeId,
                Investment = Customer_ContractDTO.Customer.Investment,
                RevenueAnnual = Customer_ContractDTO.Customer.RevenueAnnual,
                IsSupplier = Customer_ContractDTO.Customer.IsSupplier,
                Descreption = Customer_ContractDTO.Customer.Descreption,
                CreatorId = Customer_ContractDTO.Customer.CreatorId,
                Used = Customer_ContractDTO.Customer.Used,
                RowId = Customer_ContractDTO.Customer.RowId,
            };
            Contract.InvoiceDistrict = Customer_ContractDTO.InvoiceDistrict == null ? null : new District
            {
                Id = Customer_ContractDTO.InvoiceDistrict.Id,
                Code = Customer_ContractDTO.InvoiceDistrict.Code,
                Name = Customer_ContractDTO.InvoiceDistrict.Name,
                Priority = Customer_ContractDTO.InvoiceDistrict.Priority,
                ProvinceId = Customer_ContractDTO.InvoiceDistrict.ProvinceId,
                StatusId = Customer_ContractDTO.InvoiceDistrict.StatusId,
            };
            Contract.InvoiceNation = Customer_ContractDTO.InvoiceNation == null ? null : new Nation
            {
                Id = Customer_ContractDTO.InvoiceNation.Id,
                Code = Customer_ContractDTO.InvoiceNation.Code,
                Name = Customer_ContractDTO.InvoiceNation.Name,
                StatusId = Customer_ContractDTO.InvoiceNation.StatusId,
            };
            Contract.InvoiceProvince = Customer_ContractDTO.InvoiceProvince == null ? null : new Province
            {
                Id = Customer_ContractDTO.InvoiceProvince.Id,
                Code = Customer_ContractDTO.InvoiceProvince.Code,
                Name = Customer_ContractDTO.InvoiceProvince.Name,
                Priority = Customer_ContractDTO.InvoiceProvince.Priority,
                StatusId = Customer_ContractDTO.InvoiceProvince.StatusId,
            };
            Contract.Opportunity = Customer_ContractDTO.Opportunity == null ? null : new Opportunity
            {
                Id = Customer_ContractDTO.Opportunity.Id,
                Name = Customer_ContractDTO.Opportunity.Name,
                CompanyId = Customer_ContractDTO.Opportunity.CompanyId,
                CustomerLeadId = Customer_ContractDTO.Opportunity.CustomerLeadId,
                ClosingDate = Customer_ContractDTO.Opportunity.ClosingDate,
                SaleStageId = Customer_ContractDTO.Opportunity.SaleStageId,
                ProbabilityId = Customer_ContractDTO.Opportunity.ProbabilityId,
                PotentialResultId = Customer_ContractDTO.Opportunity.PotentialResultId,
                LeadSourceId = Customer_ContractDTO.Opportunity.LeadSourceId,
                AppUserId = Customer_ContractDTO.Opportunity.AppUserId,
                CurrencyId = Customer_ContractDTO.Opportunity.CurrencyId,
                Amount = Customer_ContractDTO.Opportunity.Amount,
                ForecastAmount = Customer_ContractDTO.Opportunity.ForecastAmount,
                Description = Customer_ContractDTO.Opportunity.Description,
                RefuseReciveSMS = Customer_ContractDTO.Opportunity.RefuseReciveSMS,
                RefuseReciveEmail = Customer_ContractDTO.Opportunity.RefuseReciveEmail,
                OpportunityResultTypeId = Customer_ContractDTO.Opportunity.OpportunityResultTypeId,
                CreatorId = Customer_ContractDTO.Opportunity.CreatorId,
            };
            Contract.Organization = Customer_ContractDTO.Organization == null ? null : new Organization
            {
                Id = Customer_ContractDTO.Organization.Id,
                Code = Customer_ContractDTO.Organization.Code,
                Name = Customer_ContractDTO.Organization.Name,
                ParentId = Customer_ContractDTO.Organization.ParentId,
                Path = Customer_ContractDTO.Organization.Path,
                Level = Customer_ContractDTO.Organization.Level,
                StatusId = Customer_ContractDTO.Organization.StatusId,
                Phone = Customer_ContractDTO.Organization.Phone,
                Email = Customer_ContractDTO.Organization.Email,
                Address = Customer_ContractDTO.Organization.Address,
            };
            Contract.PaymentStatus = Customer_ContractDTO.PaymentStatus == null ? null : new PaymentStatus
            {
                Id = Customer_ContractDTO.PaymentStatus.Id,
                Code = Customer_ContractDTO.PaymentStatus.Code,
                Name = Customer_ContractDTO.PaymentStatus.Name,
            };
            Contract.ReceiveDistrict = Customer_ContractDTO.ReceiveDistrict == null ? null : new District
            {
                Id = Customer_ContractDTO.ReceiveDistrict.Id,
                Code = Customer_ContractDTO.ReceiveDistrict.Code,
                Name = Customer_ContractDTO.ReceiveDistrict.Name,
                Priority = Customer_ContractDTO.ReceiveDistrict.Priority,
                ProvinceId = Customer_ContractDTO.ReceiveDistrict.ProvinceId,
                StatusId = Customer_ContractDTO.ReceiveDistrict.StatusId,
            };
            Contract.ReceiveNation = Customer_ContractDTO.ReceiveNation == null ? null : new Nation
            {
                Id = Customer_ContractDTO.ReceiveNation.Id,
                Code = Customer_ContractDTO.ReceiveNation.Code,
                Name = Customer_ContractDTO.ReceiveNation.Name,
                StatusId = Customer_ContractDTO.ReceiveNation.StatusId,
            };
            Contract.ReceiveProvince = Customer_ContractDTO.ReceiveProvince == null ? null : new Province
            {
                Id = Customer_ContractDTO.ReceiveProvince.Id,
                Code = Customer_ContractDTO.ReceiveProvince.Code,
                Name = Customer_ContractDTO.ReceiveProvince.Name,
                Priority = Customer_ContractDTO.ReceiveProvince.Priority,
                StatusId = Customer_ContractDTO.ReceiveProvince.StatusId,
            };
            Contract.BaseLanguage = CurrentContext.Language;
            return Contract;
        }

        [Route(CustomerRoute.CreateRepairTicket), HttpPost]
        public async Task<ActionResult<Customer_RepairTicketDTO>> CreateRepairTicket([FromBody] Customer_RepairTicketDTO Customer_RepairTicketDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RepairTicket RepairTicket = ConvertRepairTicket(Customer_RepairTicketDTO);
            RepairTicket = await RepairTicketService.Create(RepairTicket);
            Customer_RepairTicketDTO = new Customer_RepairTicketDTO(RepairTicket);
            if (RepairTicket.IsValidated)
                return Customer_RepairTicketDTO;
            else
                return BadRequest(Customer_RepairTicketDTO);
        }

        [Route(CustomerRoute.UpdateRepairTicket), HttpPost]
        public async Task<ActionResult<Customer_RepairTicketDTO>> UpdateRepairTicket([FromBody] Customer_RepairTicketDTO Customer_RepairTicketDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RepairTicket RepairTicket = ConvertRepairTicket(Customer_RepairTicketDTO);
            RepairTicket = await RepairTicketService.Update(RepairTicket);
            Customer_RepairTicketDTO = new Customer_RepairTicketDTO(RepairTicket);
            if (RepairTicket.IsValidated)
                return Customer_RepairTicketDTO;
            else
                return BadRequest(Customer_RepairTicketDTO);
        }

        [Route(CustomerRoute.DeleteRepairTicket), HttpPost]
        public async Task<ActionResult<Customer_RepairTicketDTO>> DeleteRepairTicket([FromBody] Customer_RepairTicketDTO Customer_RepairTicketDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            RepairTicket RepairTicket = ConvertRepairTicket(Customer_RepairTicketDTO);
            RepairTicket = await RepairTicketService.Delete(RepairTicket);
            Customer_RepairTicketDTO = new Customer_RepairTicketDTO(RepairTicket);
            if (RepairTicket.IsValidated)
                return Customer_RepairTicketDTO;
            else
                return BadRequest(Customer_RepairTicketDTO);
        }

        [Route(CustomerRoute.BulkDeleteRepairTicket), HttpPost]
        public async Task<ActionResult<bool>> BulkDeleteRepairTicket([FromBody] List<long> Ids)
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

        private RepairTicket ConvertRepairTicket(Customer_RepairTicketDTO Customer_RepairTicketDTO)
        {
            RepairTicket RepairTicket = new RepairTicket();
            RepairTicket.Id = Customer_RepairTicketDTO.Id;
            RepairTicket.Code = Customer_RepairTicketDTO.Code;
            RepairTicket.DeviceSerial = Customer_RepairTicketDTO.DeviceSerial;
            RepairTicket.OrderId = Customer_RepairTicketDTO.OrderId;
            RepairTicket.OrderCategoryId = Customer_RepairTicketDTO.OrderCategoryId;
            RepairTicket.RepairDueDate = Customer_RepairTicketDTO.RepairDueDate;
            RepairTicket.ItemId = Customer_RepairTicketDTO.ItemId;
            RepairTicket.IsRejectRepair = Customer_RepairTicketDTO.IsRejectRepair;
            RepairTicket.RejectReason = Customer_RepairTicketDTO.RejectReason;
            RepairTicket.DeviceState = Customer_RepairTicketDTO.DeviceState;
            RepairTicket.RepairStatusId = Customer_RepairTicketDTO.RepairStatusId;
            RepairTicket.RepairAddess = Customer_RepairTicketDTO.RepairAddess;
            RepairTicket.ReceiveUser = Customer_RepairTicketDTO.ReceiveUser;
            RepairTicket.ReceiveDate = Customer_RepairTicketDTO.ReceiveDate;
            RepairTicket.RepairDate = Customer_RepairTicketDTO.RepairDate;
            RepairTicket.ReturnDate = Customer_RepairTicketDTO.ReturnDate;
            RepairTicket.RepairSolution = Customer_RepairTicketDTO.RepairSolution;
            RepairTicket.Note = Customer_RepairTicketDTO.Note;
            RepairTicket.RepairCost = Customer_RepairTicketDTO.RepairCost;
            RepairTicket.PaymentStatusId = Customer_RepairTicketDTO.PaymentStatusId;
            RepairTicket.CustomerId = Customer_RepairTicketDTO.CustomerId;
            RepairTicket.CreatorId = Customer_RepairTicketDTO.CreatorId;
            RepairTicket.Creator = Customer_RepairTicketDTO.Creator == null ? null : new AppUser
            {
                Id = Customer_RepairTicketDTO.Creator.Id,
                Username = Customer_RepairTicketDTO.Creator.Username,
                DisplayName = Customer_RepairTicketDTO.Creator.DisplayName,
                Address = Customer_RepairTicketDTO.Creator.Address,
                Email = Customer_RepairTicketDTO.Creator.Email,
                Phone = Customer_RepairTicketDTO.Creator.Phone,
                SexId = Customer_RepairTicketDTO.Creator.SexId,
                Birthday = Customer_RepairTicketDTO.Creator.Birthday,
                Avatar = Customer_RepairTicketDTO.Creator.Avatar,
                Department = Customer_RepairTicketDTO.Creator.Department,
                OrganizationId = Customer_RepairTicketDTO.Creator.OrganizationId,
                Longitude = Customer_RepairTicketDTO.Creator.Longitude,
                Latitude = Customer_RepairTicketDTO.Creator.Latitude,
                StatusId = Customer_RepairTicketDTO.Creator.StatusId,
                RowId = Customer_RepairTicketDTO.Creator.RowId,
                Used = Customer_RepairTicketDTO.Creator.Used,
            };
            RepairTicket.Customer = Customer_RepairTicketDTO.Customer == null ? null : new Customer
            {
                Id = Customer_RepairTicketDTO.Customer.Id,
                Code = Customer_RepairTicketDTO.Customer.Code,
                Name = Customer_RepairTicketDTO.Customer.Name,
                Phone = Customer_RepairTicketDTO.Customer.Phone,
                Email = Customer_RepairTicketDTO.Customer.Email,
                Address = Customer_RepairTicketDTO.Customer.Address,
                StatusId = Customer_RepairTicketDTO.Customer.StatusId,
                NationId = Customer_RepairTicketDTO.Customer.NationId,
                ProvinceId = Customer_RepairTicketDTO.Customer.ProvinceId,
                DistrictId = Customer_RepairTicketDTO.Customer.DistrictId,
                WardId = Customer_RepairTicketDTO.Customer.WardId,
                ProfessionId = Customer_RepairTicketDTO.Customer.ProfessionId,
            };
            RepairTicket.Item = Customer_RepairTicketDTO.Item == null ? null : new Item
            {
                Id = Customer_RepairTicketDTO.Item.Id,
                ProductId = Customer_RepairTicketDTO.Item.ProductId,
                Code = Customer_RepairTicketDTO.Item.Code,
                Name = Customer_RepairTicketDTO.Item.Name,
                ScanCode = Customer_RepairTicketDTO.Item.ScanCode,
                SalePrice = Customer_RepairTicketDTO.Item.SalePrice,
                RetailPrice = Customer_RepairTicketDTO.Item.RetailPrice,
                StatusId = Customer_RepairTicketDTO.Item.StatusId,
            };
            RepairTicket.OrderCategory = Customer_RepairTicketDTO.OrderCategory == null ? null : new OrderCategory
            {
                Id = Customer_RepairTicketDTO.OrderCategory.Id,
                Code = Customer_RepairTicketDTO.OrderCategory.Code,
                Name = Customer_RepairTicketDTO.OrderCategory.Name,
            };
            RepairTicket.PaymentStatus = Customer_RepairTicketDTO.PaymentStatus == null ? null : new PaymentStatus
            {
                Id = Customer_RepairTicketDTO.PaymentStatus.Id,
                Code = Customer_RepairTicketDTO.PaymentStatus.Code,
                Name = Customer_RepairTicketDTO.PaymentStatus.Name,
            };
            RepairTicket.RepairStatus = Customer_RepairTicketDTO.RepairStatus == null ? null : new RepairStatus
            {
                Id = Customer_RepairTicketDTO.RepairStatus.Id,
                Name = Customer_RepairTicketDTO.RepairStatus.Name,
                Code = Customer_RepairTicketDTO.RepairStatus.Code,
            };
            RepairTicket.BaseLanguage = CurrentContext.Language;
            return RepairTicket;
        }

        [Route(CustomerRoute.CreateTicket), HttpPost]
        public async Task<ActionResult<Customer_TicketDTO>> CreateTicket([FromBody] Customer_TicketDTO Customer_TicketDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Ticket Ticket = ConvertDTOToEntity(Customer_TicketDTO);
            Ticket = await TicketService.Create(Ticket);
            Customer_TicketDTO = new Customer_TicketDTO(Ticket);
            if (Ticket.IsValidated)
                return Customer_TicketDTO;
            else
                return BadRequest(Customer_TicketDTO);
        }

        [Route(CustomerRoute.UpdateTicket), HttpPost]
        public async Task<ActionResult<Customer_TicketDTO>> UpdateTicket([FromBody] Customer_TicketDTO Customer_TicketDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Ticket Ticket = ConvertDTOToEntity(Customer_TicketDTO);
            Ticket = await TicketService.Update(Ticket);
            Customer_TicketDTO = new Customer_TicketDTO(Ticket);
            if (Ticket.IsValidated)
                return Customer_TicketDTO;
            else
                return BadRequest(Customer_TicketDTO);
        }

        [Route(CustomerRoute.DeleteTicket), HttpPost]
        public async Task<ActionResult<Customer_TicketDTO>> DeleteTicket([FromBody] Customer_TicketDTO Customer_TicketDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Ticket Ticket = ConvertDTOToEntity(Customer_TicketDTO);
            Ticket = await TicketService.Delete(Ticket);
            Customer_TicketDTO = new Customer_TicketDTO(Ticket);
            if (Ticket.IsValidated)
                return Customer_TicketDTO;
            else
                return BadRequest(Customer_TicketDTO);
        }

        private Ticket ConvertDTOToEntity(Customer_TicketDTO Customer_TicketDTO)
        {
            Ticket Ticket = new Ticket();
            Ticket.Id = Customer_TicketDTO.Id;
            Ticket.Name = Customer_TicketDTO.Name;
            Ticket.Phone = Customer_TicketDTO.Phone;
            Ticket.CustomerId = Customer_TicketDTO.CustomerId;
            Ticket.UserId = Customer_TicketDTO.UserId;
            Ticket.CustomerTypeId = Customer_TicketDTO.CustomerTypeId;
            Ticket.CreatorId = Customer_TicketDTO.CreatorId;
            Ticket.ProductId = Customer_TicketDTO.ProductId;
            Ticket.ReceiveDate = Customer_TicketDTO.ReceiveDate;
            Ticket.ProcessDate = Customer_TicketDTO.ProcessDate;
            Ticket.FinishDate = Customer_TicketDTO.FinishDate;
            Ticket.Subject = Customer_TicketDTO.Subject;
            Ticket.Content = Customer_TicketDTO.Content;
            Ticket.TicketIssueLevelId = Customer_TicketDTO.TicketIssueLevelId;
            Ticket.TicketPriorityId = Customer_TicketDTO.TicketPriorityId;
            Ticket.TicketStatusId = Customer_TicketDTO.TicketStatusId;
            Ticket.TicketSourceId = Customer_TicketDTO.TicketSourceId;
            Ticket.TicketNumber = Customer_TicketDTO.TicketNumber;
            Ticket.DepartmentId = Customer_TicketDTO.DepartmentId;
            Ticket.RelatedTicketId = Customer_TicketDTO.RelatedTicketId;
            Ticket.SLA = Customer_TicketDTO.SLA;
            Ticket.RelatedCallLogId = Customer_TicketDTO.RelatedCallLogId;
            Ticket.ResponseMethodId = Customer_TicketDTO.ResponseMethodId;
            Ticket.StatusId = Customer_TicketDTO.StatusId;
            Ticket.Used = Customer_TicketDTO.Used;
            Ticket.IsAlerted = Customer_TicketDTO.IsAlerted;
            Ticket.IsAlertedFRT = Customer_TicketDTO.IsAlertedFRT;
            Ticket.IsEscalated = Customer_TicketDTO.IsEscalated;
            Ticket.IsEscalatedFRT = Customer_TicketDTO.IsEscalatedFRT;
            Ticket.TicketResolveTypeId = Customer_TicketDTO.TicketResolveTypeId;
            Ticket.ResolveContent = Customer_TicketDTO.ResolveContent;
            Ticket.closedAt = Customer_TicketDTO.closedAt;
            Ticket.AppUserClosedId = Customer_TicketDTO.AppUserClosedId;
            Ticket.FirstResponseAt = Customer_TicketDTO.FirstResponseAt;
            Ticket.LastResponseAt = Customer_TicketDTO.LastResponseAt;
            Ticket.LastHoldingAt = Customer_TicketDTO.LastHoldingAt;
            Ticket.ReraisedTimes = Customer_TicketDTO.ReraisedTimes;
            Ticket.ResolvedAt = Customer_TicketDTO.ResolvedAt;
            Ticket.AppUserResolvedId = Customer_TicketDTO.AppUserResolvedId;
            Ticket.IsClose = Customer_TicketDTO.IsClose;
            Ticket.IsOpen = Customer_TicketDTO.IsOpen;
            Ticket.IsWait = Customer_TicketDTO.IsWait;
            Ticket.IsWork = Customer_TicketDTO.IsWork;
            Ticket.SLAPolicyId = Customer_TicketDTO.SLAPolicyId;
            Ticket.HoldingTime = Customer_TicketDTO.HoldingTime;
            Ticket.FirstResponeTime = Customer_TicketDTO.FirstResponeTime;
            Ticket.ResolveTime = Customer_TicketDTO.ResolveTime;
            Ticket.FirstRespTimeRemaining = Customer_TicketDTO.FirstRespTimeRemaining;
            Ticket.ResolveTimeRemaining = Customer_TicketDTO.ResolveTimeRemaining;
            Ticket.SLAStatusId = Customer_TicketDTO.SLAStatusId;
            Ticket.SLAOverTime = Customer_TicketDTO.SLAOverTime;
            Ticket.AppUserClosed = Customer_TicketDTO.AppUserClosed == null ? null : new AppUser
            {
                Id = Customer_TicketDTO.AppUserClosed.Id,
                Username = Customer_TicketDTO.AppUserClosed.Username,
                DisplayName = Customer_TicketDTO.AppUserClosed.DisplayName,
                Address = Customer_TicketDTO.AppUserClosed.Address,
                Email = Customer_TicketDTO.AppUserClosed.Email,
                Phone = Customer_TicketDTO.AppUserClosed.Phone,
                SexId = Customer_TicketDTO.AppUserClosed.SexId,
                Birthday = Customer_TicketDTO.AppUserClosed.Birthday,
                Avatar = Customer_TicketDTO.AppUserClosed.Avatar,
                Department = Customer_TicketDTO.AppUserClosed.Department,
                OrganizationId = Customer_TicketDTO.AppUserClosed.OrganizationId,
                Longitude = Customer_TicketDTO.AppUserClosed.Longitude,
                Latitude = Customer_TicketDTO.AppUserClosed.Latitude,
                StatusId = Customer_TicketDTO.AppUserClosed.StatusId,
            };
            Ticket.AppUserResolved = Customer_TicketDTO.AppUserResolved == null ? null : new AppUser
            {
                Id = Customer_TicketDTO.AppUserResolved.Id,
                Username = Customer_TicketDTO.AppUserResolved.Username,
                DisplayName = Customer_TicketDTO.AppUserResolved.DisplayName,
                Address = Customer_TicketDTO.AppUserResolved.Address,
                Email = Customer_TicketDTO.AppUserResolved.Email,
                Phone = Customer_TicketDTO.AppUserResolved.Phone,
                SexId = Customer_TicketDTO.AppUserResolved.SexId,
                Birthday = Customer_TicketDTO.AppUserResolved.Birthday,
                Avatar = Customer_TicketDTO.AppUserResolved.Avatar,
                Department = Customer_TicketDTO.AppUserResolved.Department,
                OrganizationId = Customer_TicketDTO.AppUserResolved.OrganizationId,
                Longitude = Customer_TicketDTO.AppUserResolved.Longitude,
                Latitude = Customer_TicketDTO.AppUserResolved.Latitude,
                StatusId = Customer_TicketDTO.AppUserResolved.StatusId,
            };
            Ticket.SLAPolicy = Customer_TicketDTO.SLAPolicy == null ? null : new SLAPolicy
            {
                Id = Customer_TicketDTO.SLAPolicy.Id,
                TicketIssueLevelId = Customer_TicketDTO.SLAPolicy.TicketIssueLevelId,
                TicketPriorityId = Customer_TicketDTO.SLAPolicy.TicketPriorityId,
                FirstResponseTime = Customer_TicketDTO.SLAPolicy.FirstResponseTime,
                FirstResponseUnitId = Customer_TicketDTO.SLAPolicy.FirstResponseUnitId,
                ResolveTime = Customer_TicketDTO.SLAPolicy.ResolveTime,
                ResolveUnitId = Customer_TicketDTO.SLAPolicy.ResolveUnitId,
                IsAlert = Customer_TicketDTO.SLAPolicy.IsAlert,
                IsAlertFRT = Customer_TicketDTO.SLAPolicy.IsAlertFRT,
                IsEscalation = Customer_TicketDTO.SLAPolicy.IsEscalation,
                IsEscalationFRT = Customer_TicketDTO.SLAPolicy.IsEscalationFRT,
            };
            Ticket.TicketResolveType = Customer_TicketDTO.TicketResolveType == null ? null : new TicketResolveType
            {
                Id = Customer_TicketDTO.TicketResolveType.Id,
                Code = Customer_TicketDTO.TicketResolveType.Code,
                Name = Customer_TicketDTO.TicketResolveType.Name,
            };
            Ticket.Customer = Customer_TicketDTO.Customer == null ? null : new Customer
            {
                Id = Customer_TicketDTO.Customer.Id,
                Code = Customer_TicketDTO.Customer.Code,
            };
            Ticket.CustomerType = Customer_TicketDTO.CustomerType == null ? null : new CustomerType
            {
                Id = Customer_TicketDTO.CustomerType.Id,
                Name = Customer_TicketDTO.CustomerType.Name,
                Code = Customer_TicketDTO.CustomerType.Code,
            };
            Ticket.Department = Customer_TicketDTO.Department == null ? null : new Organization
            {
                Id = Customer_TicketDTO.Department.Id,
                Name = Customer_TicketDTO.Department.Name,
            };
            Ticket.Product = Customer_TicketDTO.Product == null ? null : new Product
            {
                Id = Customer_TicketDTO.Product.Id,
                Name = Customer_TicketDTO.Product.Name,
            };
            Ticket.RelatedCallLog = Customer_TicketDTO.RelatedCallLog == null ? null : new CallLog
            {
                Id = Customer_TicketDTO.RelatedCallLog.Id,
                EntityReferenceId = Customer_TicketDTO.RelatedCallLog.EntityReferenceId,
                CallTypeId = Customer_TicketDTO.RelatedCallLog.CallTypeId,
                CallEmotionId = Customer_TicketDTO.RelatedCallLog.CallEmotionId,
                AppUserId = Customer_TicketDTO.RelatedCallLog.AppUserId,
                Title = Customer_TicketDTO.RelatedCallLog.Title,
                Content = Customer_TicketDTO.RelatedCallLog.Content,
                Phone = Customer_TicketDTO.RelatedCallLog.Phone,
                CallTime = Customer_TicketDTO.RelatedCallLog.CallTime,
            };
            Ticket.RelatedTicket = Customer_TicketDTO.RelatedTicket == null ? null : new Ticket
            {
                Id = Customer_TicketDTO.RelatedTicket.Id,
                Name = Customer_TicketDTO.RelatedTicket.Name,
                Phone = Customer_TicketDTO.RelatedTicket.Phone,
                CustomerId = Customer_TicketDTO.RelatedTicket.CustomerId,
                UserId = Customer_TicketDTO.RelatedTicket.UserId,
                ProductId = Customer_TicketDTO.RelatedTicket.ProductId,
                ReceiveDate = Customer_TicketDTO.RelatedTicket.ReceiveDate,
                ProcessDate = Customer_TicketDTO.RelatedTicket.ProcessDate,
                FinishDate = Customer_TicketDTO.RelatedTicket.FinishDate,
                Subject = Customer_TicketDTO.RelatedTicket.Subject,
                Content = Customer_TicketDTO.RelatedTicket.Content,
                TicketIssueLevelId = Customer_TicketDTO.RelatedTicket.TicketIssueLevelId,
                TicketPriorityId = Customer_TicketDTO.RelatedTicket.TicketPriorityId,
                TicketStatusId = Customer_TicketDTO.RelatedTicket.TicketStatusId,
                TicketSourceId = Customer_TicketDTO.RelatedTicket.TicketSourceId,
                TicketNumber = Customer_TicketDTO.RelatedTicket.TicketNumber,
                DepartmentId = Customer_TicketDTO.RelatedTicket.DepartmentId,
                RelatedTicketId = Customer_TicketDTO.RelatedTicket.RelatedTicketId,
                SLA = Customer_TicketDTO.RelatedTicket.SLA,
                RelatedCallLogId = Customer_TicketDTO.RelatedTicket.RelatedCallLogId,
                ResponseMethodId = Customer_TicketDTO.RelatedTicket.ResponseMethodId,
                StatusId = Customer_TicketDTO.RelatedTicket.StatusId,
                Used = Customer_TicketDTO.RelatedTicket.Used,
            };
            Ticket.Status = Customer_TicketDTO.Status == null ? null : new Status
            {
                Id = Customer_TicketDTO.Status.Id,
                Code = Customer_TicketDTO.Status.Code,
                Name = Customer_TicketDTO.Status.Name,
            };
            Ticket.TicketIssueLevel = Customer_TicketDTO.TicketIssueLevel == null ? null : new TicketIssueLevel
            {
                Id = Customer_TicketDTO.TicketIssueLevel.Id,
                Name = Customer_TicketDTO.TicketIssueLevel.Name,
                OrderNumber = Customer_TicketDTO.TicketIssueLevel.OrderNumber,
                TicketGroupId = Customer_TicketDTO.TicketIssueLevel.TicketGroupId,
                StatusId = Customer_TicketDTO.TicketIssueLevel.StatusId,
                SLA = Customer_TicketDTO.TicketIssueLevel.SLA,
                Used = Customer_TicketDTO.TicketIssueLevel.Used,
            };
            Ticket.TicketPriority = Customer_TicketDTO.TicketPriority == null ? null : new TicketPriority
            {
                Id = Customer_TicketDTO.TicketPriority.Id,
                Name = Customer_TicketDTO.TicketPriority.Name,
                OrderNumber = Customer_TicketDTO.TicketPriority.OrderNumber,
                ColorCode = Customer_TicketDTO.TicketPriority.ColorCode,
                StatusId = Customer_TicketDTO.TicketPriority.StatusId,
                Used = Customer_TicketDTO.TicketPriority.Used,
            };
            Ticket.TicketSource = Customer_TicketDTO.TicketSource == null ? null : new TicketSource
            {
                Id = Customer_TicketDTO.TicketSource.Id,
                Name = Customer_TicketDTO.TicketSource.Name,
                OrderNumber = Customer_TicketDTO.TicketSource.OrderNumber,
                StatusId = Customer_TicketDTO.TicketSource.StatusId,
                Used = Customer_TicketDTO.TicketSource.Used,
            };
            Ticket.TicketStatus = Customer_TicketDTO.TicketStatus == null ? null : new TicketStatus
            {
                Id = Customer_TicketDTO.TicketStatus.Id,
                Name = Customer_TicketDTO.TicketStatus.Name,
                OrderNumber = Customer_TicketDTO.TicketStatus.OrderNumber,
                ColorCode = Customer_TicketDTO.TicketStatus.ColorCode,
                StatusId = Customer_TicketDTO.TicketStatus.StatusId,
                Used = Customer_TicketDTO.TicketStatus.Used,
            };
            Ticket.User = Customer_TicketDTO.User == null ? null : new AppUser
            {
                Id = Customer_TicketDTO.User.Id,
                Username = Customer_TicketDTO.User.Username,
            };
            Ticket.Creator = Customer_TicketDTO.Creator == null ? null : new AppUser
            {
                Id = Customer_TicketDTO.Creator.Id,
                Username = Customer_TicketDTO.Creator.Username,
            };
            Ticket.SLAStatus = Customer_TicketDTO.SLAStatus == null ? null : new SLAStatus
            {
                Id = Customer_TicketDTO.SLAStatus.Id,
                Code = Customer_TicketDTO.SLAStatus.Code,
                Name = Customer_TicketDTO.SLAStatus.Name,
            };
            Ticket.BaseLanguage = CurrentContext.Language;
            return Ticket;
        }

        [Route(CustomerRoute.BulkMergeStore), HttpPost]
        public async Task<ActionResult<List<Customer_StoreDTO>>> BulkMergeStore([FromBody] List<Customer_StoreDTO> Customer_StoreDTOs)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            List<Store> Stores = Customer_StoreDTOs.Select(x => ConvertDTOToEntity(x)).ToList();
            Stores = await StoreService.AddToCustomer(Stores);
            if (Stores.Any(x => !x.IsValidated))
                return BadRequest(Stores.Where(x => !x.IsValidated));
            Customer_StoreDTOs = Stores.Select(x => new Customer_StoreDTO(x)).ToList();
            return Customer_StoreDTOs;
        }

        [Route(CustomerRoute.DeleteStore), HttpPost]
        public async Task<ActionResult<Customer_StoreDTO>> DeleteStore([FromBody] Customer_StoreDTO Customer_StoreDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Store Store = ConvertDTOToEntity(Customer_StoreDTO);
            Store = await StoreService.RemoveFromCustomer(Store);
            Customer_StoreDTO = new Customer_StoreDTO(Store);
            if (Store.IsValidated)
                return Customer_StoreDTO;
            else
                return BadRequest(Customer_StoreDTO);
        }

        [Route(CustomerRoute.BulkDeleteStore), HttpPost]
        public async Task<ActionResult<bool>> BulkDeleteStore([FromBody] long CustomerId, List<Customer_StoreDTO> Customer_StoreDTOs)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            List<Store> Stores = Customer_StoreDTOs.Select(x => ConvertDTOToEntity(x)).ToList();
            Stores = await StoreService.BulkRemove(Stores);
            if (Stores.Any(x => !x.IsValidated))
                return BadRequest(Stores.Where(x => !x.IsValidated));
            return true;
        }

        private Store ConvertDTOToEntity(Customer_StoreDTO Customer_StoreDTO)
        {
            Store Store = new Store();
            Store.Id = Customer_StoreDTO.Id;
            Store.Code = Customer_StoreDTO.Code;
            Store.CodeDraft = Customer_StoreDTO.CodeDraft;
            Store.Name = Customer_StoreDTO.Name;
            Store.UnsignName = Customer_StoreDTO.UnsignName;
            Store.ParentStoreId = Customer_StoreDTO.ParentStoreId;
            Store.OrganizationId = Customer_StoreDTO.OrganizationId;
            Store.StoreTypeId = Customer_StoreDTO.StoreTypeId;
            Store.StoreGroupingId = Customer_StoreDTO.StoreGroupingId;
            Store.Telephone = Customer_StoreDTO.Telephone;
            Store.ProvinceId = Customer_StoreDTO.ProvinceId;
            Store.DistrictId = Customer_StoreDTO.DistrictId;
            Store.WardId = Customer_StoreDTO.WardId;
            Store.Address = Customer_StoreDTO.Address;
            Store.UnsignAddress = Customer_StoreDTO.UnsignAddress;
            Store.DeliveryAddress = Customer_StoreDTO.DeliveryAddress;
            Store.Latitude = Customer_StoreDTO.Latitude;
            Store.Longitude = Customer_StoreDTO.Longitude;
            Store.DeliveryLatitude = Customer_StoreDTO.DeliveryLatitude;
            Store.DeliveryLongitude = Customer_StoreDTO.DeliveryLongitude;
            Store.OwnerName = Customer_StoreDTO.OwnerName;
            Store.OwnerPhone = Customer_StoreDTO.OwnerPhone;
            Store.OwnerEmail = Customer_StoreDTO.OwnerEmail;
            Store.TaxCode = Customer_StoreDTO.TaxCode;
            Store.LegalEntity = Customer_StoreDTO.LegalEntity;
            Store.AppUserId = Customer_StoreDTO.AppUserId;
            Store.StatusId = Customer_StoreDTO.StatusId;
            Store.Used = Customer_StoreDTO.Used;
            Store.StoreStatusId = Customer_StoreDTO.StoreStatusId;
            Store.CustomerId = Customer_StoreDTO.CustomerId;
            Store.AppUser = Customer_StoreDTO.AppUser == null ? null : new AppUser
            {
                Id = Customer_StoreDTO.AppUser.Id,
                Username = Customer_StoreDTO.AppUser.Username,
                DisplayName = Customer_StoreDTO.AppUser.DisplayName,
                Address = Customer_StoreDTO.AppUser.Address,
                Email = Customer_StoreDTO.AppUser.Email,
                Phone = Customer_StoreDTO.AppUser.Phone,
                SexId = Customer_StoreDTO.AppUser.SexId,
                Birthday = Customer_StoreDTO.AppUser.Birthday,
                Avatar = Customer_StoreDTO.AppUser.Avatar,
                Department = Customer_StoreDTO.AppUser.Department,
                OrganizationId = Customer_StoreDTO.AppUser.OrganizationId,
                Longitude = Customer_StoreDTO.AppUser.Longitude,
                Latitude = Customer_StoreDTO.AppUser.Latitude,
                StatusId = Customer_StoreDTO.AppUser.StatusId,
            };
            Store.Customer = Customer_StoreDTO.Customer == null ? null : new Customer
            {
                Id = Customer_StoreDTO.Customer.Id,
                Code = Customer_StoreDTO.Customer.Code,
                Name = Customer_StoreDTO.Customer.Name,
                Phone = Customer_StoreDTO.Customer.Phone,
                Address = Customer_StoreDTO.Customer.Address,
                NationId = Customer_StoreDTO.Customer.NationId,
                ProvinceId = Customer_StoreDTO.Customer.ProvinceId,
                DistrictId = Customer_StoreDTO.Customer.DistrictId,
                WardId = Customer_StoreDTO.Customer.WardId,
                CustomerTypeId = Customer_StoreDTO.Customer.CustomerTypeId,
                Birthday = Customer_StoreDTO.Customer.Birthday,
                Email = Customer_StoreDTO.Customer.Email,
                ProfessionId = Customer_StoreDTO.Customer.ProfessionId,
                CustomerResourceId = Customer_StoreDTO.Customer.CustomerResourceId,
                SexId = Customer_StoreDTO.Customer.SexId,
                StatusId = Customer_StoreDTO.Customer.StatusId,
                CompanyId = Customer_StoreDTO.Customer.CompanyId,
                ParentCompanyId = Customer_StoreDTO.Customer.ParentCompanyId,
                TaxCode = Customer_StoreDTO.Customer.TaxCode,
                Fax = Customer_StoreDTO.Customer.Fax,
                Website = Customer_StoreDTO.Customer.Website,
                NumberOfEmployee = Customer_StoreDTO.Customer.NumberOfEmployee,
                BusinessTypeId = Customer_StoreDTO.Customer.BusinessTypeId,
                Investment = Customer_StoreDTO.Customer.Investment,
                RevenueAnnual = Customer_StoreDTO.Customer.RevenueAnnual,
                IsSupplier = Customer_StoreDTO.Customer.IsSupplier,
                Descreption = Customer_StoreDTO.Customer.Descreption,
                Used = Customer_StoreDTO.Customer.Used,
                RowId = Customer_StoreDTO.Customer.RowId,
            };
            Store.District = Customer_StoreDTO.District == null ? null : new District
            {
                Id = Customer_StoreDTO.District.Id,
                Code = Customer_StoreDTO.District.Code,
                Name = Customer_StoreDTO.District.Name,
                Priority = Customer_StoreDTO.District.Priority,
                ProvinceId = Customer_StoreDTO.District.ProvinceId,
                StatusId = Customer_StoreDTO.District.StatusId,
            };
            Store.Organization = Customer_StoreDTO.Organization == null ? null : new Organization
            {
                Id = Customer_StoreDTO.Organization.Id,
                Code = Customer_StoreDTO.Organization.Code,
                Name = Customer_StoreDTO.Organization.Name,
                ParentId = Customer_StoreDTO.Organization.ParentId,
                Path = Customer_StoreDTO.Organization.Path,
                Level = Customer_StoreDTO.Organization.Level,
                StatusId = Customer_StoreDTO.Organization.StatusId,
                Phone = Customer_StoreDTO.Organization.Phone,
                Email = Customer_StoreDTO.Organization.Email,
                Address = Customer_StoreDTO.Organization.Address,
            };
            Store.ParentStore = Customer_StoreDTO.ParentStore == null ? null : new Store
            {
                Id = Customer_StoreDTO.ParentStore.Id,
                Code = Customer_StoreDTO.ParentStore.Code,
                CodeDraft = Customer_StoreDTO.ParentStore.CodeDraft,
                Name = Customer_StoreDTO.ParentStore.Name,
                UnsignName = Customer_StoreDTO.ParentStore.UnsignName,
                ParentStoreId = Customer_StoreDTO.ParentStore.ParentStoreId,
                OrganizationId = Customer_StoreDTO.ParentStore.OrganizationId,
                StoreTypeId = Customer_StoreDTO.ParentStore.StoreTypeId,
                StoreGroupingId = Customer_StoreDTO.ParentStore.StoreGroupingId,
                Telephone = Customer_StoreDTO.ParentStore.Telephone,
                ProvinceId = Customer_StoreDTO.ParentStore.ProvinceId,
                DistrictId = Customer_StoreDTO.ParentStore.DistrictId,
                WardId = Customer_StoreDTO.ParentStore.WardId,
                Address = Customer_StoreDTO.ParentStore.Address,
                UnsignAddress = Customer_StoreDTO.ParentStore.UnsignAddress,
                DeliveryAddress = Customer_StoreDTO.ParentStore.DeliveryAddress,
                Latitude = Customer_StoreDTO.ParentStore.Latitude,
                Longitude = Customer_StoreDTO.ParentStore.Longitude,
                DeliveryLatitude = Customer_StoreDTO.ParentStore.DeliveryLatitude,
                DeliveryLongitude = Customer_StoreDTO.ParentStore.DeliveryLongitude,
                OwnerName = Customer_StoreDTO.ParentStore.OwnerName,
                OwnerPhone = Customer_StoreDTO.ParentStore.OwnerPhone,
                OwnerEmail = Customer_StoreDTO.ParentStore.OwnerEmail,
                TaxCode = Customer_StoreDTO.ParentStore.TaxCode,
                LegalEntity = Customer_StoreDTO.ParentStore.LegalEntity,
                AppUserId = Customer_StoreDTO.ParentStore.AppUserId,
                StatusId = Customer_StoreDTO.ParentStore.StatusId,
                Used = Customer_StoreDTO.ParentStore.Used,
                StoreStatusId = Customer_StoreDTO.ParentStore.StoreStatusId,
            };
            Store.Province = Customer_StoreDTO.Province == null ? null : new Province
            {
                Id = Customer_StoreDTO.Province.Id,
                Code = Customer_StoreDTO.Province.Code,
                Name = Customer_StoreDTO.Province.Name,
                Priority = Customer_StoreDTO.Province.Priority,
                StatusId = Customer_StoreDTO.Province.StatusId,
            };
            Store.Status = Customer_StoreDTO.Status == null ? null : new Status
            {
                Id = Customer_StoreDTO.Status.Id,
                Code = Customer_StoreDTO.Status.Code,
                Name = Customer_StoreDTO.Status.Name,
            };
            Store.StoreGrouping = Customer_StoreDTO.StoreGrouping == null ? null : new StoreGrouping
            {
                Id = Customer_StoreDTO.StoreGrouping.Id,
                Code = Customer_StoreDTO.StoreGrouping.Code,
                Name = Customer_StoreDTO.StoreGrouping.Name,
                ParentId = Customer_StoreDTO.StoreGrouping.ParentId,
                Path = Customer_StoreDTO.StoreGrouping.Path,
                Level = Customer_StoreDTO.StoreGrouping.Level,
                StatusId = Customer_StoreDTO.StoreGrouping.StatusId,
            };
            Store.StoreStatus = Customer_StoreDTO.StoreStatus == null ? null : new StoreStatus
            {
                Id = Customer_StoreDTO.StoreStatus.Id,
                Code = Customer_StoreDTO.StoreStatus.Code,
                Name = Customer_StoreDTO.StoreStatus.Name,
            };
            Store.StoreType = Customer_StoreDTO.StoreType == null ? null : new StoreType
            {
                Id = Customer_StoreDTO.StoreType.Id,
                Code = Customer_StoreDTO.StoreType.Code,
                Name = Customer_StoreDTO.StoreType.Name,
                ColorId = Customer_StoreDTO.StoreType.ColorId,
                StatusId = Customer_StoreDTO.StoreType.StatusId,
                Used = Customer_StoreDTO.StoreType.Used,
            };
            Store.Ward = Customer_StoreDTO.Ward == null ? null : new Ward
            {
                Id = Customer_StoreDTO.Ward.Id,
                Code = Customer_StoreDTO.Ward.Code,
                Name = Customer_StoreDTO.Ward.Name,
                Priority = Customer_StoreDTO.Ward.Priority,
                DistrictId = Customer_StoreDTO.Ward.DistrictId,
                StatusId = Customer_StoreDTO.Ward.StatusId,
            };
            Store.BaseLanguage = CurrentContext.Language;
            return Store;
        }
    }
}

