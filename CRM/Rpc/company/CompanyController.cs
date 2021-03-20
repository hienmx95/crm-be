using CRM.Common;
using CRM.Entities;
using CRM.Helpers;
using CRM.Models;
using CRM.Services.MActivityPriority;
using CRM.Services.MActivityStatus;
using CRM.Services.MActivityType;
using CRM.Services.MAppUser;
using CRM.Services.MCallLog;
using CRM.Services.MCompany;
using CRM.Services.MCompanyActivity;
using CRM.Services.MCompanyEmail;
using CRM.Services.MCompanyStatus;
using CRM.Services.MContact;
using CRM.Services.MContract;
using CRM.Services.MContractStatus;
using CRM.Services.MContractType;
using CRM.Services.MCurrency;
using CRM.Services.MCustomerLead;
using CRM.Services.MCustomerLeadLevel;
using CRM.Services.MCustomerLeadSource;
using CRM.Services.MCustomerLeadStatus;
using CRM.Services.MCustomerSalesOrder;
using CRM.Services.MDirectSalesOrder;
using CRM.Services.MDistrict;
using CRM.Services.MEditedPriceStatus;
using CRM.Services.MEmailStatus;
using CRM.Services.MFile;
using CRM.Services.MFileType;
using CRM.Services.MMailTemplate;
using CRM.Services.MNation;
using CRM.Services.MOpportunity;
using CRM.Services.MOpportunityResultType;
using CRM.Services.MOrderPaymentStatus;
using CRM.Services.MOrderQuote;
using CRM.Services.MOrderQuoteStatus;
using CRM.Services.MOrganization;
using CRM.Services.MPaymentStatus;
using CRM.Services.MPosition;
using CRM.Services.MPotentialResult;
using CRM.Services.MProbability;
using CRM.Services.MProduct;
using CRM.Services.MProductGrouping;
using CRM.Services.MProductType;
using CRM.Services.MProfession;
using CRM.Services.MProvince;
using CRM.Services.MRatingStatus;
using CRM.Services.MRequestState;
using CRM.Services.MSaleStage;
using CRM.Services.MSex;
using CRM.Services.MStore;
using CRM.Services.MSupplier;
using CRM.Services.MTaxType;
using CRM.Services.MUnitOfMeasure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.company
{
    public partial class CompanyController : RpcController
    {
        private IActivityStatusService ActivityStatusService;
        private IActivityTypeService ActivityTypeService;
        private IActivityPriorityService ActivityPriorityService;
        private ICompanyService CompanyService;
        private IAppUserService AppUserService;
        private ICompanyStatusService CompanyStatusService;
        private ICustomerLeadSourceService CustomerLeadSourceService;
        private ICurrencyService CurrencyService;
        private ICustomerLeadLevelService CustomerLeadLevelService;
        private ICustomerLeadStatusService CustomerLeadStatusService;
        private ICallLogService CallLogService;
        private ICustomerLeadService CustomerLeadService;
        private IContactService ContactService;
        private IDistrictService DistrictService;
        private IEditedPriceStatusService EditedPriceStatusService;
        private IFileTypeService FileTypeService;
        private IFileService FileService;
        private IOrganizationService OrganizationService;
        private INationService NationService;
        private IProvinceService ProvinceService;
        private IProfessionService ProfessionService;
        private IRatingStatusService RatingStatusService;
        private IUnitOfMeasureService UnitOfMeasureService;
        private IPotentialResultService PotentialResultService;
        private IProbabilityService ProbabilityService;
        private ISaleStageService SaleStageService;
        private IOpportunityService OpportunityService;
        private ISupplierService SupplierService;
        private IProductService ProductService;
        private IOpportunityResultTypeService OpportunityResultTypeService;
        private IItemService ItemService;
        private IOrderQuoteService OrderQuoteService;
        private IProductGroupingService ProductGroupingService;
        private IProductTypeService ProductTypeService;
        private ISexService SexService;
        private IOrderQuoteStatusService OrderQuoteStatusService;
        private ITaxTypeService TaxTypeService;
        private IPositionService PositionService;
        private IDirectSalesOrderService DirectSalesOrderService;
        private ICompanyEmailService CompanyEmailService;
        private ICurrentContext CurrentContext;
        private IMailTemplateService MailTemplateService;
        private IEmailStatusService EmailStatusService;
        private ICompanyActivityService CompanyActivityService;
        private ICustomerSalesOrderService CustomerSalesOrderService;
        private IOrderPaymentStatusService OrderPaymentStatusService;
        private IRequestStateService RequestStateService;
        private IStoreService StoreService;
        private IContractService ContractService;
        private IContractStatusService ContractStatusService;
        private IContractTypeService ContractTypeService;
        private IPaymentStatusService PaymentStatusService;
        public CompanyController(
            IDistrictService DistrictService,
            IOrganizationService OrganizationService,
            INationService NationService,
            IProvinceService ProvinceService,
            IProfessionService ProfessionService,
            ICustomerLeadSourceService CustomerLeadSourceService,
            IRatingStatusService RatingStatusService,
            ICompanyService CompanyService,
            ICurrentContext CurrentContext,
            IAppUserService AppUserService,
            IUnitOfMeasureService UnitOfMeasureService,
            ICurrencyService CurrencyService,
            IFileService FileService,
            ICompanyStatusService CompanyStatusService,
            IContactService ContactService,
            IPotentialResultService PotentialResultService,
            IProbabilityService ProbabilityService,
            ISaleStageService SaleStageService,
            IOpportunityService OpportunityService,
            ISupplierService SupplierService,
            IProductService ProductService,
            IOrderQuoteService OrderQuoteService,
            IOpportunityResultTypeService OpportunityResultTypeService,
            IItemService ItemService,
            IActivityStatusService ActivityStatusService,
            IActivityTypeService ActivityTypeService,
            IActivityPriorityService ActivityPriorityService,
            ICustomerLeadLevelService CustomerLeadLevelService,
            ICustomerLeadStatusService CustomerLeadStatusService,
            ICallLogService CallLogService,
            IFileTypeService FileTypeService,
            ICustomerLeadService CustomerLeadService,
            IProductGroupingService ProductGroupingService,
            IProductTypeService ProductTypeService,
            ISexService SexService,
            IOrderQuoteStatusService OrderQuoteStatusService,
            IEditedPriceStatusService EditedPriceStatusService,
            IPositionService PositionService,
            IDirectSalesOrderService DirectSalesOrderService,
            ITaxTypeService TaxTypeService,
            ICompanyEmailService CompanyEmailService,
            IMailTemplateService MailTemplateService,
            IEmailStatusService EmailStatusService,
            ICompanyActivityService CompanyActivityService,
            ICustomerSalesOrderService CustomerSalesOrderService,
            IOrderPaymentStatusService OrderPaymentStatusService,
            IRequestStateService RequestStateService,
            IStoreService StoreService,
            IContractService ContractService,
            IContractStatusService ContractStatusService,
            IContractTypeService ContractTypeService,
            IPaymentStatusService PaymentStatusService
        ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.CompanyService = CompanyService;
            this.CompanyStatusService = CompanyStatusService;
            this.RatingStatusService = RatingStatusService;
            this.FileService = FileService;
            this.ContactService = ContactService;
            this.PotentialResultService = PotentialResultService;
            this.ProbabilityService = ProbabilityService;
            this.SaleStageService = SaleStageService;
            this.OpportunityService = OpportunityService;
            this.SupplierService = SupplierService;
            this.ProductService = ProductService;
            this.OpportunityResultTypeService = OpportunityResultTypeService;
            this.ItemService = ItemService;
            this.ActivityStatusService = ActivityStatusService;
            this.OrderQuoteService = OrderQuoteService;
            this.ActivityTypeService = ActivityTypeService;
            this.ActivityPriorityService = ActivityPriorityService;
            this.CustomerLeadLevelService = CustomerLeadLevelService;
            this.CustomerLeadStatusService = CustomerLeadStatusService;
            this.CallLogService = CallLogService;
            this.FileTypeService = FileTypeService;
            this.CustomerLeadService = CustomerLeadService;
            this.ProductGroupingService = ProductGroupingService;
            this.ProductTypeService = ProductTypeService;
            this.SexService = SexService;
            this.OrderQuoteStatusService = OrderQuoteStatusService;
            this.EditedPriceStatusService = EditedPriceStatusService;
            this.UnitOfMeasureService = UnitOfMeasureService;
            this.CustomerLeadSourceService = CustomerLeadSourceService;
            this.DistrictService = DistrictService;
            this.ProfessionService = ProfessionService;
            this.OrganizationService = OrganizationService;
            this.ProvinceService = ProvinceService;
            this.AppUserService = AppUserService;
            this.NationService = NationService;
            this.CurrencyService = CurrencyService;
            this.TaxTypeService = TaxTypeService;
            this.PositionService = PositionService;
            this.DirectSalesOrderService = DirectSalesOrderService;
            this.CompanyEmailService = CompanyEmailService;
            this.CurrentContext = CurrentContext;
            this.MailTemplateService = MailTemplateService;
            this.EmailStatusService = EmailStatusService;
            this.CompanyActivityService = CompanyActivityService;
            this.CustomerSalesOrderService = CustomerSalesOrderService;
            this.OrderPaymentStatusService = OrderPaymentStatusService;
            this.RequestStateService = RequestStateService;
            this.StoreService = StoreService;
            this.ContractService = ContractService;
            this.ContractStatusService = ContractStatusService;
            this.ContractTypeService = ContractTypeService;
            this.PaymentStatusService = PaymentStatusService;
        }

        [Route(CompanyRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] Company_CompanyFilterDTO Company_CompanyFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyFilter CompanyFilter = ConvertFilterDTOToFilterEntity(Company_CompanyFilterDTO);
            CompanyFilter = await CompanyService.ToFilter(CompanyFilter);
            int count = await CompanyService.Count(CompanyFilter);
            return count;
        }

        [Route(CompanyRoute.List), HttpPost]
        public async Task<ActionResult<List<Company_CompanyDTO>>> List([FromBody] Company_CompanyFilterDTO Company_CompanyFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyFilter CompanyFilter = ConvertFilterDTOToFilterEntity(Company_CompanyFilterDTO);
            CompanyFilter = await CompanyService.ToFilter(CompanyFilter);
            List<Company> Companies = await CompanyService.List(CompanyFilter);
            List<Company_CompanyDTO> Company_CompanyDTOs = Companies
                .Select(c => new Company_CompanyDTO(c)).ToList();
            return Company_CompanyDTOs;
        }

        [Route(CompanyRoute.Get), HttpPost]
        public async Task<ActionResult<Company_CompanyDTO>> Get([FromBody] Company_CompanyDTO Company_CompanyDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Company_CompanyDTO.Id))
                return Forbid();

            Company Company = await CompanyService.Get(Company_CompanyDTO.Id);
            return new Company_CompanyDTO(Company);
        }

        [Route(CompanyRoute.Create), HttpPost]
        public async Task<ActionResult<Company_CompanyDTO>> Create([FromBody] Company_CompanyDTO Company_CompanyDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Company_CompanyDTO.Id))
                return Forbid();

            Company Company = ConvertDTOToEntity(Company_CompanyDTO);
            Company = await CompanyService.Create(Company);
            Company_CompanyDTO = new Company_CompanyDTO(Company);
            if (Company.IsValidated)
                return Company_CompanyDTO;
            else
                return BadRequest(Company_CompanyDTO);
        }

        [Route(CompanyRoute.Update), HttpPost]
        public async Task<ActionResult<Company_CompanyDTO>> Update([FromBody] Company_CompanyDTO Company_CompanyDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Company_CompanyDTO.Id))
                return Forbid();

            Company Company = ConvertDTOToEntity(Company_CompanyDTO);
            Company = await CompanyService.Update(Company);
            Company_CompanyDTO = new Company_CompanyDTO(Company);
            if (Company.IsValidated)
                return Company_CompanyDTO;
            else
                return BadRequest(Company_CompanyDTO);
        }

        [Route(CompanyRoute.Delete), HttpPost]
        public async Task<ActionResult<Company_CompanyDTO>> Delete([FromBody] Company_CompanyDTO Company_CompanyDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Company_CompanyDTO.Id))
                return Forbid();

            Company Company = ConvertDTOToEntity(Company_CompanyDTO);
            Company = await CompanyService.Delete(Company);
            Company_CompanyDTO = new Company_CompanyDTO(Company);
            if (Company.IsValidated)
                return Company_CompanyDTO;
            else
                return BadRequest(Company_CompanyDTO);
        }

        [Route(CompanyRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyFilter CompanyFilter = new CompanyFilter();
            CompanyFilter = await CompanyService.ToFilter(CompanyFilter);
            CompanyFilter.Id = new IdFilter { In = Ids };
            CompanyFilter.Selects = CompanySelect.Id;
            CompanyFilter.Skip = 0;
            CompanyFilter.Take = int.MaxValue;

            List<Company> Companies = await CompanyService.List(CompanyFilter);
            Companies = await CompanyService.BulkDelete(Companies);
            if (Companies.Any(x => !x.IsValidated))
                return BadRequest(Companies.Where(x => !x.IsValidated));
            return true;
        }

        [Route(CompanyRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            AppUserFilter AppUserFilter = new AppUserFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = AppUserSelect.ALL
            };
            List<AppUser> AppUsers = await AppUserService.List(AppUserFilter);
            CompanyStatusFilter CompanyStatusFilter = new CompanyStatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CompanyStatusSelect.ALL
            };
            List<CompanyStatus> CompanyStatuses = await CompanyStatusService.List(CompanyStatusFilter);
            AppUserFilter CreatorFilter = new AppUserFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = AppUserSelect.ALL
            };
            List<AppUser> Creators = await AppUserService.List(CreatorFilter);
            CurrencyFilter CurrencyFilter = new CurrencyFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CurrencySelect.ALL
            };
            List<Currency> Currencies = await CurrencyService.List(CurrencyFilter);
            CustomerLeadFilter CustomerLeadFilter = new CustomerLeadFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CustomerLeadSelect.ALL
            };
            List<CustomerLead> CustomerLeads = await CustomerLeadService.List(CustomerLeadFilter);
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
            List<Company> Companies = new List<Company>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(Companies);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int NameColumn = 1 + StartColumn;
                int PhoneColumn = 2 + StartColumn;
                int FAXColumn = 3 + StartColumn;
                int PhoneOtherColumn = 4 + StartColumn;
                int EmailColumn = 5 + StartColumn;
                int EmailOtherColumn = 6 + StartColumn;
                int ZIPCodeColumn = 7 + StartColumn;
                int RevenueColumn = 8 + StartColumn;
                int WebsiteColumn = 9 + StartColumn;
                int AddressColumn = 10 + StartColumn;
                int NationIdColumn = 11 + StartColumn;
                int ProvinceIdColumn = 12 + StartColumn;
                int DistrictIdColumn = 13 + StartColumn;
                int NumberOfEmployeeColumn = 14 + StartColumn;
                int RefuseReciveEmailColumn = 15 + StartColumn;
                int RefuseReciveSMSColumn = 16 + StartColumn;
                int CustomerLeadIdColumn = 17 + StartColumn;
                int ParentIdColumn = 18 + StartColumn;
                int PathColumn = 19 + StartColumn;
                int LevelColumn = 20 + StartColumn;
                int ProfessionIdColumn = 21 + StartColumn;
                int AppUserIdColumn = 22 + StartColumn;
                int CreatorIdColumn = 23 + StartColumn;
                int CurrencyIdColumn = 24 + StartColumn;
                int CompanyStatusIdColumn = 25 + StartColumn;
                int DescriptionColumn = 26 + StartColumn;
                int RowIdColumn = 30 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string PhoneValue = worksheet.Cells[i + StartRow, PhoneColumn].Value?.ToString();
                    string FAXValue = worksheet.Cells[i + StartRow, FAXColumn].Value?.ToString();
                    string PhoneOtherValue = worksheet.Cells[i + StartRow, PhoneOtherColumn].Value?.ToString();
                    string EmailValue = worksheet.Cells[i + StartRow, EmailColumn].Value?.ToString();
                    string EmailOtherValue = worksheet.Cells[i + StartRow, EmailOtherColumn].Value?.ToString();
                    string ZIPCodeValue = worksheet.Cells[i + StartRow, ZIPCodeColumn].Value?.ToString();
                    string RevenueValue = worksheet.Cells[i + StartRow, RevenueColumn].Value?.ToString();
                    string WebsiteValue = worksheet.Cells[i + StartRow, WebsiteColumn].Value?.ToString();
                    string AddressValue = worksheet.Cells[i + StartRow, AddressColumn].Value?.ToString();
                    string NationIdValue = worksheet.Cells[i + StartRow, NationIdColumn].Value?.ToString();
                    string ProvinceIdValue = worksheet.Cells[i + StartRow, ProvinceIdColumn].Value?.ToString();
                    string DistrictIdValue = worksheet.Cells[i + StartRow, DistrictIdColumn].Value?.ToString();
                    string NumberOfEmployeeValue = worksheet.Cells[i + StartRow, NumberOfEmployeeColumn].Value?.ToString();
                    string RefuseReciveEmailValue = worksheet.Cells[i + StartRow, RefuseReciveEmailColumn].Value?.ToString();
                    string RefuseReciveSMSValue = worksheet.Cells[i + StartRow, RefuseReciveSMSColumn].Value?.ToString();
                    string CustomerLeadIdValue = worksheet.Cells[i + StartRow, CustomerLeadIdColumn].Value?.ToString();
                    string ParentIdValue = worksheet.Cells[i + StartRow, ParentIdColumn].Value?.ToString();
                    string PathValue = worksheet.Cells[i + StartRow, PathColumn].Value?.ToString();
                    string LevelValue = worksheet.Cells[i + StartRow, LevelColumn].Value?.ToString();
                    string ProfessionIdValue = worksheet.Cells[i + StartRow, ProfessionIdColumn].Value?.ToString();
                    string AppUserIdValue = worksheet.Cells[i + StartRow, AppUserIdColumn].Value?.ToString();
                    string CreatorIdValue = worksheet.Cells[i + StartRow, CreatorIdColumn].Value?.ToString();
                    string CurrencyIdValue = worksheet.Cells[i + StartRow, CurrencyIdColumn].Value?.ToString();
                    string CompanyStatusIdValue = worksheet.Cells[i + StartRow, CompanyStatusIdColumn].Value?.ToString();
                    string DescriptionValue = worksheet.Cells[i + StartRow, DescriptionColumn].Value?.ToString();
                    string RowIdValue = worksheet.Cells[i + StartRow, RowIdColumn].Value?.ToString();

                    Company Company = new Company();
                    Company.Name = NameValue;
                    Company.Phone = PhoneValue;
                    Company.FAX = FAXValue;
                    Company.PhoneOther = PhoneOtherValue;
                    Company.Email = EmailValue;
                    Company.EmailOther = EmailOtherValue;
                    Company.ZIPCode = ZIPCodeValue;
                    Company.Revenue = decimal.TryParse(RevenueValue, out decimal Revenue) ? Revenue : 0;
                    Company.Website = WebsiteValue;
                    Company.Address = AddressValue;
                    Company.NumberOfEmployee = long.TryParse(NumberOfEmployeeValue, out long NumberOfEmployee) ? NumberOfEmployee : 0;
                    Company.Path = PathValue;
                    Company.Level = long.TryParse(LevelValue, out long Level) ? Level : 0;
                    Company.Description = DescriptionValue;
                    AppUser AppUser = AppUsers.Where(x => x.Id.ToString() == AppUserIdValue).FirstOrDefault();
                    Company.AppUserId = AppUser == null ? 0 : AppUser.Id;
                    Company.AppUser = AppUser;
                    CompanyStatus CompanyStatus = CompanyStatuses.Where(x => x.Id.ToString() == CompanyStatusIdValue).FirstOrDefault();
                    Company.CompanyStatusId = CompanyStatus == null ? 0 : CompanyStatus.Id;
                    Company.CompanyStatus = CompanyStatus;
                    AppUser Creator = Creators.Where(x => x.Id.ToString() == CreatorIdValue).FirstOrDefault();
                    Company.CreatorId = Creator == null ? 0 : Creator.Id;
                    Company.Creator = Creator;
                    Currency Currency = Currencies.Where(x => x.Id.ToString() == CurrencyIdValue).FirstOrDefault();
                    Company.CurrencyId = Currency == null ? 0 : Currency.Id;
                    Company.Currency = Currency;
                    CustomerLead CustomerLead = CustomerLeads.Where(x => x.Id.ToString() == CustomerLeadIdValue).FirstOrDefault();
                    Company.CustomerLeadId = CustomerLead == null ? 0 : CustomerLead.Id;
                    Company.CustomerLead = CustomerLead;
                    District District = Districts.Where(x => x.Id.ToString() == DistrictIdValue).FirstOrDefault();
                    Company.DistrictId = District == null ? 0 : District.Id;
                    Company.District = District;
                    Nation Nation = Nations.Where(x => x.Id.ToString() == NationIdValue).FirstOrDefault();
                    Company.NationId = Nation == null ? 0 : Nation.Id;
                    Company.Nation = Nation;
                    Profession Profession = Professions.Where(x => x.Id.ToString() == ProfessionIdValue).FirstOrDefault();
                    Company.ProfessionId = Profession == null ? 0 : Profession.Id;
                    Company.Profession = Profession;
                    Province Province = Provinces.Where(x => x.Id.ToString() == ProvinceIdValue).FirstOrDefault();
                    Company.ProvinceId = Province == null ? 0 : Province.Id;
                    Company.Province = Province;

                    Companies.Add(Company);
                }
            }
            Companies = await CompanyService.Import(Companies);
            if (Companies.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < Companies.Count; i++)
                {
                    Company Company = Companies[i];
                    if (!Company.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (Company.Errors.ContainsKey(nameof(Company.Id)))
                            Error += Company.Errors[nameof(Company.Id)];
                        if (Company.Errors.ContainsKey(nameof(Company.Name)))
                            Error += Company.Errors[nameof(Company.Name)];
                        if (Company.Errors.ContainsKey(nameof(Company.Phone)))
                            Error += Company.Errors[nameof(Company.Phone)];
                        if (Company.Errors.ContainsKey(nameof(Company.FAX)))
                            Error += Company.Errors[nameof(Company.FAX)];
                        if (Company.Errors.ContainsKey(nameof(Company.PhoneOther)))
                            Error += Company.Errors[nameof(Company.PhoneOther)];
                        if (Company.Errors.ContainsKey(nameof(Company.Email)))
                            Error += Company.Errors[nameof(Company.Email)];
                        if (Company.Errors.ContainsKey(nameof(Company.EmailOther)))
                            Error += Company.Errors[nameof(Company.EmailOther)];
                        if (Company.Errors.ContainsKey(nameof(Company.ZIPCode)))
                            Error += Company.Errors[nameof(Company.ZIPCode)];
                        if (Company.Errors.ContainsKey(nameof(Company.Revenue)))
                            Error += Company.Errors[nameof(Company.Revenue)];
                        if (Company.Errors.ContainsKey(nameof(Company.Website)))
                            Error += Company.Errors[nameof(Company.Website)];
                        if (Company.Errors.ContainsKey(nameof(Company.Address)))
                            Error += Company.Errors[nameof(Company.Address)];
                        if (Company.Errors.ContainsKey(nameof(Company.NationId)))
                            Error += Company.Errors[nameof(Company.NationId)];
                        if (Company.Errors.ContainsKey(nameof(Company.ProvinceId)))
                            Error += Company.Errors[nameof(Company.ProvinceId)];
                        if (Company.Errors.ContainsKey(nameof(Company.DistrictId)))
                            Error += Company.Errors[nameof(Company.DistrictId)];
                        if (Company.Errors.ContainsKey(nameof(Company.NumberOfEmployee)))
                            Error += Company.Errors[nameof(Company.NumberOfEmployee)];
                        if (Company.Errors.ContainsKey(nameof(Company.RefuseReciveEmail)))
                            Error += Company.Errors[nameof(Company.RefuseReciveEmail)];
                        if (Company.Errors.ContainsKey(nameof(Company.RefuseReciveSMS)))
                            Error += Company.Errors[nameof(Company.RefuseReciveSMS)];
                        if (Company.Errors.ContainsKey(nameof(Company.CustomerLeadId)))
                            Error += Company.Errors[nameof(Company.CustomerLeadId)];
                        if (Company.Errors.ContainsKey(nameof(Company.ParentId)))
                            Error += Company.Errors[nameof(Company.ParentId)];
                        if (Company.Errors.ContainsKey(nameof(Company.Path)))
                            Error += Company.Errors[nameof(Company.Path)];
                        if (Company.Errors.ContainsKey(nameof(Company.Level)))
                            Error += Company.Errors[nameof(Company.Level)];
                        if (Company.Errors.ContainsKey(nameof(Company.ProfessionId)))
                            Error += Company.Errors[nameof(Company.ProfessionId)];
                        if (Company.Errors.ContainsKey(nameof(Company.AppUserId)))
                            Error += Company.Errors[nameof(Company.AppUserId)];
                        if (Company.Errors.ContainsKey(nameof(Company.CreatorId)))
                            Error += Company.Errors[nameof(Company.CreatorId)];
                        if (Company.Errors.ContainsKey(nameof(Company.CurrencyId)))
                            Error += Company.Errors[nameof(Company.CurrencyId)];
                        if (Company.Errors.ContainsKey(nameof(Company.CompanyStatusId)))
                            Error += Company.Errors[nameof(Company.CompanyStatusId)];
                        if (Company.Errors.ContainsKey(nameof(Company.Description)))
                            Error += Company.Errors[nameof(Company.Description)];
                        if (Company.Errors.ContainsKey(nameof(Company.RowId)))
                            Error += Company.Errors[nameof(Company.RowId)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }

        [Route(CompanyRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] Company_CompanyFilterDTO Company_CompanyFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region Company
                var CompanyFilter = ConvertFilterDTOToFilterEntity(Company_CompanyFilterDTO);
                CompanyFilter.Skip = 0;
                CompanyFilter.Take = int.MaxValue;
                CompanyFilter = await CompanyService.ToFilter(CompanyFilter);
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
                #region CompanyStatus
                var CompanyStatusFilter = new CompanyStatusFilter();
                CompanyStatusFilter.Selects = CompanyStatusSelect.ALL;
                CompanyStatusFilter.OrderBy = CompanyStatusOrder.Id;
                CompanyStatusFilter.OrderType = OrderType.ASC;
                CompanyStatusFilter.Skip = 0;
                CompanyStatusFilter.Take = int.MaxValue;
                List<CompanyStatus> CompanyStatuses = await CompanyStatusService.List(CompanyStatusFilter);

                var CompanyStatusHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> CompanyStatusData = new List<object[]>();
                for (int i = 0; i < CompanyStatuses.Count; i++)
                {
                    var CompanyStatus = CompanyStatuses[i];
                    CompanyStatusData.Add(new Object[]
                    {
                        CompanyStatus.Id,
                        CompanyStatus.Code,
                        CompanyStatus.Name,
                    });
                }
                excel.GenerateWorksheet("CompanyStatus", CompanyStatusHeaders, CompanyStatusData);
                #endregion
                #region Currency
                var CurrencyFilter = new CurrencyFilter();
                CurrencyFilter.Selects = CurrencySelect.ALL;
                CurrencyFilter.OrderBy = CurrencyOrder.Id;
                CurrencyFilter.OrderType = OrderType.ASC;
                CurrencyFilter.Skip = 0;
                CurrencyFilter.Take = int.MaxValue;
                List<Currency> Currencies = await CurrencyService.List(CurrencyFilter);

                var CurrencyHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> CurrencyData = new List<object[]>();
                for (int i = 0; i < Currencies.Count; i++)
                {
                    var Currency = Currencies[i];
                    CurrencyData.Add(new Object[]
                    {
                        Currency.Id,
                        Currency.Code,
                        Currency.Name,
                    });
                }
                excel.GenerateWorksheet("Currency", CurrencyHeaders, CurrencyData);
                #endregion
                #region CustomerLead
                var CustomerLeadFilter = new CustomerLeadFilter();
                CustomerLeadFilter.Selects = CustomerLeadSelect.ALL;
                CustomerLeadFilter.OrderBy = CustomerLeadOrder.Id;
                CustomerLeadFilter.OrderType = OrderType.ASC;
                CustomerLeadFilter.Skip = 0;
                CustomerLeadFilter.Take = int.MaxValue;
                List<CustomerLead> CustomerLeads = await CustomerLeadService.List(CustomerLeadFilter);

                var CustomerLeadHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Name",
                        "CompanyName",
                        "TelePhone",
                        "Phone",
                        "Fax",
                        "Email",
                        "SecondEmail",
                        "Website",
                        "CustomerLeadSourceId",
                        "CustomerLeadLevelId",
                        "CompanyId",
                        "CampaignId",
                        "ProfessionId",
                        "Revenue",
                        "EmployeeQuantity",
                        "Address",
                        "NationId",
                        "ProvinceId",
                        "DistrictId",
                        "CustomerLeadStatusId",
                        "BusinessRegistrationCode",
                        "SexId",
                        "RefuseReciveSMS",
                        "RefuseReciveEmail",
                        "Description",
                        "AppUserId",
                        "CreatorId",
                        "ZipCode",
                        "CurrencyId",
                        "RowId",
                    }
                };
                List<object[]> CustomerLeadData = new List<object[]>();
                for (int i = 0; i < CustomerLeads.Count; i++)
                {
                    var CustomerLead = CustomerLeads[i];
                    CustomerLeadData.Add(new Object[]
                    {
                        CustomerLead.Id,
                        CustomerLead.Name,
                        CustomerLead.CompanyName,
                        CustomerLead.TelePhone,
                        CustomerLead.Phone,
                        CustomerLead.Fax,
                        CustomerLead.Email,
                        CustomerLead.SecondEmail,
                        CustomerLead.Website,
                        CustomerLead.CustomerLeadSourceId,
                        CustomerLead.CustomerLeadLevelId,
                        CustomerLead.CompanyId,
                        CustomerLead.CampaignId,
                        CustomerLead.ProfessionId,
                        CustomerLead.Revenue,
                        CustomerLead.EmployeeQuantity,
                        CustomerLead.Address,
                        CustomerLead.NationId,
                        CustomerLead.ProvinceId,
                        CustomerLead.DistrictId,
                        CustomerLead.CustomerLeadStatusId,
                        CustomerLead.BusinessRegistrationCode,
                        CustomerLead.SexId,
                        CustomerLead.RefuseReciveSMS,
                        CustomerLead.RefuseReciveEmail,
                        CustomerLead.Description,
                        CustomerLead.ZipCode,
                        CustomerLead.CurrencyId,
                        CustomerLead.RowId,
                    });
                }
                excel.GenerateWorksheet("CustomerLead", CustomerLeadHeaders, CustomerLeadData);
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
                #region CompanyActivity
                var CompanyActivityFilter = new CompanyActivityFilter();
                CompanyActivityFilter.Selects = CompanyActivitySelect.ALL;
                CompanyActivityFilter.OrderBy = CompanyActivityOrder.Id;
                CompanyActivityFilter.OrderType = OrderType.ASC;
                CompanyActivityFilter.Skip = 0;
                CompanyActivityFilter.Take = int.MaxValue;
                List<CompanyActivity> CompanyActivities = await CompanyActivityService.List(CompanyActivityFilter);

                var CompanyActivityHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Title",
                        "FromDate",
                        "ToDate",
                        "ActivityTypeId",
                        "ActivityPriorityId",
                        "Description",
                        "Address",
                        "CompanyId",
                        "AppUserId",
                        "ActivityStatusId",
                    }
                };
                List<object[]> CompanyActivityData = new List<object[]>();
                for (int i = 0; i < CompanyActivities.Count; i++)
                {
                    var CompanyActivity = CompanyActivities[i];
                    CompanyActivityData.Add(new Object[]
                    {
                        CompanyActivity.Id,
                        CompanyActivity.Title,
                        CompanyActivity.FromDate,
                        CompanyActivity.ToDate,
                        CompanyActivity.ActivityTypeId,
                        CompanyActivity.ActivityPriorityId,
                        CompanyActivity.Description,
                        CompanyActivity.Address,
                        CompanyActivity.CompanyId,
                        CompanyActivity.AppUserId,
                        CompanyActivity.ActivityStatusId,
                    });
                }
                excel.GenerateWorksheet("CompanyActivity", CompanyActivityHeaders, CompanyActivityData);
                #endregion
                #region ActivityPriority
                var ActivityPriorityFilter = new ActivityPriorityFilter();
                ActivityPriorityFilter.Selects = ActivityPrioritySelect.ALL;
                ActivityPriorityFilter.OrderBy = ActivityPriorityOrder.Id;
                ActivityPriorityFilter.OrderType = OrderType.ASC;
                ActivityPriorityFilter.Skip = 0;
                ActivityPriorityFilter.Take = int.MaxValue;
                List<ActivityPriority> ActivityPriorities = await ActivityPriorityService.List(ActivityPriorityFilter);

                var ActivityPriorityHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> ActivityPriorityData = new List<object[]>();
                for (int i = 0; i < ActivityPriorities.Count; i++)
                {
                    var ActivityPriority = ActivityPriorities[i];
                    ActivityPriorityData.Add(new Object[]
                    {
                        ActivityPriority.Id,
                        ActivityPriority.Code,
                        ActivityPriority.Name,
                    });
                }
                excel.GenerateWorksheet("ActivityPriority", ActivityPriorityHeaders, ActivityPriorityData);
                #endregion
                #region ActivityStatus
                var ActivityStatusFilter = new ActivityStatusFilter();
                ActivityStatusFilter.Selects = ActivityStatusSelect.ALL;
                ActivityStatusFilter.OrderBy = ActivityStatusOrder.Id;
                ActivityStatusFilter.OrderType = OrderType.ASC;
                ActivityStatusFilter.Skip = 0;
                ActivityStatusFilter.Take = int.MaxValue;
                List<ActivityStatus> ActivityStatuses = await ActivityStatusService.List(ActivityStatusFilter);

                var ActivityStatusHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> ActivityStatusData = new List<object[]>();
                for (int i = 0; i < ActivityStatuses.Count; i++)
                {
                    var ActivityStatus = ActivityStatuses[i];
                    ActivityStatusData.Add(new Object[]
                    {
                        ActivityStatus.Id,
                        ActivityStatus.Code,
                        ActivityStatus.Name,
                    });
                }
                excel.GenerateWorksheet("ActivityStatus", ActivityStatusHeaders, ActivityStatusData);
                #endregion
                #region ActivityType
                var ActivityTypeFilter = new ActivityTypeFilter();
                ActivityTypeFilter.Selects = ActivityTypeSelect.ALL;
                ActivityTypeFilter.OrderBy = ActivityTypeOrder.Id;
                ActivityTypeFilter.OrderType = OrderType.ASC;
                ActivityTypeFilter.Skip = 0;
                ActivityTypeFilter.Take = int.MaxValue;
                List<ActivityType> ActivityTypes = await ActivityTypeService.List(ActivityTypeFilter);

                var ActivityTypeHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> ActivityTypeData = new List<object[]>();
                for (int i = 0; i < ActivityTypes.Count; i++)
                {
                    var ActivityType = ActivityTypes[i];
                    ActivityTypeData.Add(new Object[]
                    {
                        ActivityType.Id,
                        ActivityType.Code,
                        ActivityType.Name,
                    });
                }
                excel.GenerateWorksheet("ActivityType", ActivityTypeHeaders, ActivityTypeData);
                #endregion
                #region CallLog
                var CallLogFilter = new CallLogFilter();
                CallLogFilter.Selects = CallLogSelect.ALL;
                CallLogFilter.OrderBy = CallLogOrder.Id;
                CallLogFilter.OrderType = OrderType.ASC;
                CallLogFilter.Skip = 0;
                CallLogFilter.Take = int.MaxValue;
                List<CallLog> CallLogs = await CallLogService.List(CallLogFilter);

                var CallLogHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Title",
                        "Content",
                        "Phone",
                        "CallTime",
                        "EntityReferenceId",
                        "EntityId",
                        "CallTypeId",
                        "CallEmotionId",
                        "AppUserId",
                        "CreatorId",
                    }
                };
                List<object[]> CallLogData = new List<object[]>();
                for (int i = 0; i < CallLogs.Count; i++)
                {
                    var CallLog = CallLogs[i];
                    CallLogData.Add(new Object[]
                    {
                        CallLog.Id,
                        CallLog.Title,
                        CallLog.Content,
                        CallLog.Phone,
                        CallLog.CallTime,
                        CallLog.EntityReferenceId,
                        CallLog.CallTypeId,
                        CallLog.AppUserId,
                        CallLog.CreatorId,
                    });
                }
                excel.GenerateWorksheet("CallLog", CallLogHeaders, CallLogData);
                #endregion
                #region CompanyEmail
                var CompanyEmailFilter = new CompanyEmailFilter();
                CompanyEmailFilter.Selects = CompanyEmailSelect.ALL;
                CompanyEmailFilter.OrderBy = CompanyEmailOrder.Id;
                CompanyEmailFilter.OrderType = OrderType.ASC;
                CompanyEmailFilter.Skip = 0;
                CompanyEmailFilter.Take = int.MaxValue;
                List<CompanyEmail> CompanyEmails = await CompanyEmailService.List(CompanyEmailFilter);

                var CompanyEmailHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Title",
                        "Content",
                        "Reciepient",
                        "CompanyId",
                        "CreatorId",
                        "EmailStatusId",
                    }
                };
                List<object[]> CompanyEmailData = new List<object[]>();
                for (int i = 0; i < CompanyEmails.Count; i++)
                {
                    var CompanyEmail = CompanyEmails[i];
                    CompanyEmailData.Add(new Object[]
                    {
                        CompanyEmail.Id,
                        CompanyEmail.Title,
                        CompanyEmail.Content,
                        CompanyEmail.Reciepient,
                        CompanyEmail.CompanyId,
                        CompanyEmail.CreatorId,
                        CompanyEmail.EmailStatusId,
                    });
                }
                excel.GenerateWorksheet("CompanyEmail", CompanyEmailHeaders, CompanyEmailData);
                #endregion
                #region EmailStatus
                var EmailStatusFilter = new EmailStatusFilter();
                EmailStatusFilter.Selects = EmailStatusSelect.ALL;
                EmailStatusFilter.OrderBy = EmailStatusOrder.Id;
                EmailStatusFilter.OrderType = OrderType.ASC;
                EmailStatusFilter.Skip = 0;
                EmailStatusFilter.Take = int.MaxValue;
                List<EmailStatus> EmailStatuses = await EmailStatusService.List(EmailStatusFilter);

                var EmailStatusHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> EmailStatusData = new List<object[]>();
                for (int i = 0; i < EmailStatuses.Count; i++)
                {
                    var EmailStatus = EmailStatuses[i];
                    EmailStatusData.Add(new Object[]
                    {
                        EmailStatus.Id,
                        EmailStatus.Code,
                        EmailStatus.Name,
                    });
                }
                excel.GenerateWorksheet("EmailStatus", EmailStatusHeaders, EmailStatusData);
                #endregion
                #region FileType
                var FileTypeFilter = new FileTypeFilter();
                FileTypeFilter.Selects = FileTypeSelect.ALL;
                FileTypeFilter.OrderBy = FileTypeOrder.Id;
                FileTypeFilter.OrderType = OrderType.ASC;
                FileTypeFilter.Skip = 0;
                FileTypeFilter.Take = int.MaxValue;
                List<FileType> FileTypes = await FileTypeService.List(FileTypeFilter);

                var FileTypeHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> FileTypeData = new List<object[]>();
                for (int i = 0; i < FileTypes.Count; i++)
                {
                    var FileType = FileTypes[i];
                    FileTypeData.Add(new Object[]
                    {
                        FileType.Id,
                        FileType.Code,
                        FileType.Name,
                    });
                }
                excel.GenerateWorksheet("FileType", FileTypeHeaders, FileTypeData);
                #endregion
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "Company.xlsx");
        }

        [Route(CompanyRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] Company_CompanyFilterDTO Company_CompanyFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            string path = "Templates/Company_Template.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "Company.xlsx");
        }

        [HttpPost]
        [Route(CompanyRoute.UploadFile)]
        public async Task<ActionResult<Company_FileDTO>> UploadFile(IFormFile file)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            MemoryStream memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            CRM.Entities.File File = new CRM.Entities.File
            {
                Name = file.FileName,
                Content = memoryStream.ToArray(),
            };
            File = await CompanyService.UploadFile(File);
            if (File == null)
                return BadRequest();
            Company_FileDTO Company_FileDTO = new Company_FileDTO
            {
                Id = File.Id,
                Name = File.Name,
                Url = File.Url,
                AppUserId = File.AppUserId
            };
            return Ok(Company_FileDTO);
        }

        private async Task<bool> HasPermission(long Id)
        {
            CompanyFilter CompanyFilter = new CompanyFilter();
            CompanyFilter = await CompanyService.ToFilter(CompanyFilter);
            if (Id == 0)
            {

            }
            else
            {
                CompanyFilter.Id = new IdFilter { Equal = Id };
                int count = await CompanyService.Count(CompanyFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private Company ConvertDTOToEntity(Company_CompanyDTO Company_CompanyDTO)
        {
            Company Company = new Company();
            Company.Id = Company_CompanyDTO.Id;
            Company.Name = Company_CompanyDTO.Name;
            Company.Phone = Company_CompanyDTO.Phone;
            Company.FAX = Company_CompanyDTO.FAX;
            Company.PhoneOther = Company_CompanyDTO.PhoneOther;
            Company.Email = Company_CompanyDTO.Email;
            Company.EmailOther = Company_CompanyDTO.EmailOther;
            Company.ZIPCode = Company_CompanyDTO.ZIPCode;
            Company.Revenue = Company_CompanyDTO.Revenue;
            Company.Website = Company_CompanyDTO.Website;
            Company.Address = Company_CompanyDTO.Address;
            Company.NationId = Company_CompanyDTO.NationId;
            Company.ProvinceId = Company_CompanyDTO.ProvinceId;
            Company.DistrictId = Company_CompanyDTO.DistrictId;
            Company.NumberOfEmployee = Company_CompanyDTO.NumberOfEmployee;
            Company.RefuseReciveEmail = Company_CompanyDTO.RefuseReciveEmail;
            Company.RefuseReciveSMS = Company_CompanyDTO.RefuseReciveSMS;
            Company.CustomerLeadId = Company_CompanyDTO.CustomerLeadId;
            Company.ParentId = Company_CompanyDTO.ParentId;
            Company.Path = Company_CompanyDTO.Path;
            Company.Level = Company_CompanyDTO.Level;
            Company.ProfessionId = Company_CompanyDTO.ProfessionId;
            Company.AppUserId = Company_CompanyDTO.AppUserId;
            Company.CreatorId = Company_CompanyDTO.CreatorId;
            Company.CurrencyId = Company_CompanyDTO.CurrencyId;
            Company.CompanyStatusId = Company_CompanyDTO.CompanyStatusId;
            Company.Description = Company_CompanyDTO.Description;
            Company.RowId = Company_CompanyDTO.RowId;
            Company.AppUser = Company_CompanyDTO.AppUser == null ? null : new AppUser
            {
                Id = Company_CompanyDTO.AppUser.Id,
                Username = Company_CompanyDTO.AppUser.Username,
                DisplayName = Company_CompanyDTO.AppUser.DisplayName,
                Address = Company_CompanyDTO.AppUser.Address,
                Email = Company_CompanyDTO.AppUser.Email,
                Phone = Company_CompanyDTO.AppUser.Phone,
                SexId = Company_CompanyDTO.AppUser.SexId,
                Birthday = Company_CompanyDTO.AppUser.Birthday,
                Avatar = Company_CompanyDTO.AppUser.Avatar,
                Department = Company_CompanyDTO.AppUser.Department,
                OrganizationId = Company_CompanyDTO.AppUser.OrganizationId,
                Longitude = Company_CompanyDTO.AppUser.Longitude,
                Latitude = Company_CompanyDTO.AppUser.Latitude,
                StatusId = Company_CompanyDTO.AppUser.StatusId,
                RowId = Company_CompanyDTO.AppUser.RowId,
                Used = Company_CompanyDTO.AppUser.Used,
            };
            Company.CompanyStatus = Company_CompanyDTO.CompanyStatus == null ? null : new CompanyStatus
            {
                Id = Company_CompanyDTO.CompanyStatus.Id,
                Code = Company_CompanyDTO.CompanyStatus.Code,
                Name = Company_CompanyDTO.CompanyStatus.Name,
            };
            Company.Creator = Company_CompanyDTO.Creator == null ? null : new AppUser
            {
                Id = Company_CompanyDTO.Creator.Id,
                Username = Company_CompanyDTO.Creator.Username,
                DisplayName = Company_CompanyDTO.Creator.DisplayName,
                Address = Company_CompanyDTO.Creator.Address,
                Email = Company_CompanyDTO.Creator.Email,
                Phone = Company_CompanyDTO.Creator.Phone,
                SexId = Company_CompanyDTO.Creator.SexId,
                Birthday = Company_CompanyDTO.Creator.Birthday,
                Avatar = Company_CompanyDTO.Creator.Avatar,
                Department = Company_CompanyDTO.Creator.Department,
                OrganizationId = Company_CompanyDTO.Creator.OrganizationId,
                Longitude = Company_CompanyDTO.Creator.Longitude,
                Latitude = Company_CompanyDTO.Creator.Latitude,
                StatusId = Company_CompanyDTO.Creator.StatusId,
                RowId = Company_CompanyDTO.Creator.RowId,
                Used = Company_CompanyDTO.Creator.Used,
            };
            Company.Currency = Company_CompanyDTO.Currency == null ? null : new Currency
            {
                Id = Company_CompanyDTO.Currency.Id,
                Code = Company_CompanyDTO.Currency.Code,
                Name = Company_CompanyDTO.Currency.Name,
            };
            Company.CustomerLead = Company_CompanyDTO.CustomerLead == null ? null : new CustomerLead
            {
                Id = Company_CompanyDTO.CustomerLead.Id,
                Name = Company_CompanyDTO.CustomerLead.Name,
                CompanyName = Company_CompanyDTO.CustomerLead.CompanyName,
                TelePhone = Company_CompanyDTO.CustomerLead.TelePhone,
                Phone = Company_CompanyDTO.CustomerLead.Phone,
                Fax = Company_CompanyDTO.CustomerLead.Fax,
                Email = Company_CompanyDTO.CustomerLead.Email,
                SecondEmail = Company_CompanyDTO.CustomerLead.SecondEmail,
                Website = Company_CompanyDTO.CustomerLead.Website,
                CustomerLeadSourceId = Company_CompanyDTO.CustomerLead.CustomerLeadSourceId,
                CustomerLeadLevelId = Company_CompanyDTO.CustomerLead.CustomerLeadLevelId,
                CompanyId = Company_CompanyDTO.CustomerLead.CompanyId,
                CampaignId = Company_CompanyDTO.CustomerLead.CampaignId,
                ProfessionId = Company_CompanyDTO.CustomerLead.ProfessionId,
                Revenue = Company_CompanyDTO.CustomerLead.Revenue,
                EmployeeQuantity = Company_CompanyDTO.CustomerLead.EmployeeQuantity,
                Address = Company_CompanyDTO.CustomerLead.Address,
                NationId = Company_CompanyDTO.CustomerLead.NationId,
                ProvinceId = Company_CompanyDTO.CustomerLead.ProvinceId,
                DistrictId = Company_CompanyDTO.CustomerLead.DistrictId,
                CustomerLeadStatusId = Company_CompanyDTO.CustomerLead.CustomerLeadStatusId,
                BusinessRegistrationCode = Company_CompanyDTO.CustomerLead.BusinessRegistrationCode,
                SexId = Company_CompanyDTO.CustomerLead.SexId,
                RefuseReciveSMS = Company_CompanyDTO.CustomerLead.RefuseReciveSMS,
                RefuseReciveEmail = Company_CompanyDTO.CustomerLead.RefuseReciveEmail,
                Description = Company_CompanyDTO.CustomerLead.Description,
                ZipCode = Company_CompanyDTO.CustomerLead.ZipCode,
                CurrencyId = Company_CompanyDTO.CustomerLead.CurrencyId,
                RowId = Company_CompanyDTO.CustomerLead.RowId,
            };
            Company.District = Company_CompanyDTO.District == null ? null : new District
            {
                Id = Company_CompanyDTO.District.Id,
                Code = Company_CompanyDTO.District.Code,
                Name = Company_CompanyDTO.District.Name,
                Priority = Company_CompanyDTO.District.Priority,
                ProvinceId = Company_CompanyDTO.District.ProvinceId,
                StatusId = Company_CompanyDTO.District.StatusId,
                RowId = Company_CompanyDTO.District.RowId,
                Used = Company_CompanyDTO.District.Used,
            };
            Company.Nation = Company_CompanyDTO.Nation == null ? null : new Nation
            {
                Id = Company_CompanyDTO.Nation.Id,
                Code = Company_CompanyDTO.Nation.Code,
                Name = Company_CompanyDTO.Nation.Name,
                Priority = Company_CompanyDTO.Nation.Priority,
                StatusId = Company_CompanyDTO.Nation.StatusId,
                Used = Company_CompanyDTO.Nation.Used,
                RowId = Company_CompanyDTO.Nation.RowId,
            };
            Company.Parent = Company_CompanyDTO.Parent == null ? null : new Company
            {
                Id = Company_CompanyDTO.Parent.Id,
                Name = Company_CompanyDTO.Parent.Name,
                Phone = Company_CompanyDTO.Parent.Phone,
                FAX = Company_CompanyDTO.Parent.FAX,
                PhoneOther = Company_CompanyDTO.Parent.PhoneOther,
                Email = Company_CompanyDTO.Parent.Email,
                EmailOther = Company_CompanyDTO.Parent.EmailOther,
                ZIPCode = Company_CompanyDTO.Parent.ZIPCode,
                Revenue = Company_CompanyDTO.Parent.Revenue,
                Website = Company_CompanyDTO.Parent.Website,
                Address = Company_CompanyDTO.Parent.Address,
                NationId = Company_CompanyDTO.Parent.NationId,
                ProvinceId = Company_CompanyDTO.Parent.ProvinceId,
                DistrictId = Company_CompanyDTO.Parent.DistrictId,
                NumberOfEmployee = Company_CompanyDTO.Parent.NumberOfEmployee,
                RefuseReciveEmail = Company_CompanyDTO.Parent.RefuseReciveEmail,
                RefuseReciveSMS = Company_CompanyDTO.Parent.RefuseReciveSMS,
                CustomerLeadId = Company_CompanyDTO.Parent.CustomerLeadId,
                ParentId = Company_CompanyDTO.Parent.ParentId,
                Path = Company_CompanyDTO.Parent.Path,
                Level = Company_CompanyDTO.Parent.Level,
                ProfessionId = Company_CompanyDTO.Parent.ProfessionId,
                AppUserId = Company_CompanyDTO.Parent.AppUserId,
                CreatorId = Company_CompanyDTO.Parent.CreatorId,
                CurrencyId = Company_CompanyDTO.Parent.CurrencyId,
                CompanyStatusId = Company_CompanyDTO.Parent.CompanyStatusId,
                Description = Company_CompanyDTO.Parent.Description,
                RowId = Company_CompanyDTO.Parent.RowId,
            };
            Company.Profession = Company_CompanyDTO.Profession == null ? null : new Profession
            {
                Id = Company_CompanyDTO.Profession.Id,
                Code = Company_CompanyDTO.Profession.Code,
                Name = Company_CompanyDTO.Profession.Name,
                StatusId = Company_CompanyDTO.Profession.StatusId,
                RowId = Company_CompanyDTO.Profession.RowId,
                Used = Company_CompanyDTO.Profession.Used,
            };
            Company.Province = Company_CompanyDTO.Province == null ? null : new Province
            {
                Id = Company_CompanyDTO.Province.Id,
                Code = Company_CompanyDTO.Province.Code,
                Name = Company_CompanyDTO.Province.Name,
                Priority = Company_CompanyDTO.Province.Priority,
                StatusId = Company_CompanyDTO.Province.StatusId,
                RowId = Company_CompanyDTO.Province.RowId,
                Used = Company_CompanyDTO.Province.Used,
            };
            Company.CompanyActivities = Company_CompanyDTO.CompanyActivities?
                .Select(x => new CompanyActivity
                {
                    Id = x.Id,
                    Title = x.Title,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    ActivityTypeId = x.ActivityTypeId,
                    ActivityPriorityId = x.ActivityPriorityId,
                    Description = x.Description,
                    Address = x.Address,
                    AppUserId = x.AppUserId,
                    ActivityStatusId = x.ActivityStatusId,
                    ActivityPriority = x.ActivityPriority == null ? null : new ActivityPriority
                    {
                        Id = x.ActivityPriority.Id,
                        Code = x.ActivityPriority.Code,
                        Name = x.ActivityPriority.Name,
                    },
                    ActivityStatus = x.ActivityStatus == null ? null : new ActivityStatus
                    {
                        Id = x.ActivityStatus.Id,
                        Code = x.ActivityStatus.Code,
                        Name = x.ActivityStatus.Name,
                    },
                    ActivityType = x.ActivityType == null ? null : new ActivityType
                    {
                        Id = x.ActivityType.Id,
                        Code = x.ActivityType.Code,
                        Name = x.ActivityType.Name,
                    },
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
                }).ToList();
            Company.CompanyCallLogMappings = Company_CompanyDTO.CompanyCallLogMappings?
                .Select(x => new CompanyCallLogMapping
                {
                    CallLogId = x.CallLogId,
                    CallLog = x.CallLog == null ? null : new CallLog
                    {
                        Id = x.CallLog.Id,
                        Title = x.CallLog.Title,
                        Content = x.CallLog.Content,
                        Phone = x.CallLog.Phone,
                        CallTime = x.CallLog.CallTime,
                        EntityReferenceId = x.CallLog.EntityReferenceId,
                        CallTypeId = x.CallLog.CallTypeId,
                        AppUserId = x.CallLog.AppUserId,
                        CreatorId = x.CallLog.CreatorId,
                    },
                }).ToList();
            Company.CompanyEmails = Company_CompanyDTO.CompanyEmails?
                .Select(x => new CompanyEmail
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    Reciepient = x.Reciepient,
                    CreatorId = x.CreatorId,
                    EmailStatusId = x.EmailStatusId,
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
                    EmailStatus = x.EmailStatus == null ? null : new EmailStatus
                    {
                        Id = x.EmailStatus.Id,
                        Code = x.EmailStatus.Code,
                        Name = x.EmailStatus.Name,
                    },
                }).ToList();
            Company.CompanyFileGroupings = Company_CompanyDTO.CompanyFileGroupings?
                .Select(x => new CompanyFileGrouping
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CreatorId = x.CreatorId,
                    FileTypeId = x.FileTypeId,
                    RowId = x.RowId,
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
                    FileType = x.FileType == null ? null : new FileType
                    {
                        Id = x.FileType.Id,
                        Code = x.FileType.Code,
                        Name = x.FileType.Name,
                    },
                }).ToList();
            Company.BaseLanguage = CurrentContext.Language;
            return Company;
        }

        private CompanyFilter ConvertFilterDTOToFilterEntity(Company_CompanyFilterDTO Company_CompanyFilterDTO)
        {
            CompanyFilter CompanyFilter = new CompanyFilter();
            CompanyFilter.Selects = CompanySelect.ALL;
            CompanyFilter.Skip = 0;
            CompanyFilter.Take = 99999;
            CompanyFilter.OrderBy = Company_CompanyFilterDTO.OrderBy;
            CompanyFilter.OrderType = Company_CompanyFilterDTO.OrderType;

            CompanyFilter.Id = Company_CompanyFilterDTO.Id;
            CompanyFilter.Name = Company_CompanyFilterDTO.Name;
            CompanyFilter.Phone = Company_CompanyFilterDTO.Phone;
            CompanyFilter.FAX = Company_CompanyFilterDTO.FAX;
            CompanyFilter.PhoneOther = Company_CompanyFilterDTO.PhoneOther;
            CompanyFilter.Email = Company_CompanyFilterDTO.Email;
            CompanyFilter.EmailOther = Company_CompanyFilterDTO.EmailOther;
            CompanyFilter.ZIPCode = Company_CompanyFilterDTO.ZIPCode;
            CompanyFilter.Revenue = Company_CompanyFilterDTO.Revenue;
            CompanyFilter.Website = Company_CompanyFilterDTO.Website;
            CompanyFilter.Address = Company_CompanyFilterDTO.Address;
            CompanyFilter.NationId = Company_CompanyFilterDTO.NationId;
            CompanyFilter.ProvinceId = Company_CompanyFilterDTO.ProvinceId;
            CompanyFilter.DistrictId = Company_CompanyFilterDTO.DistrictId;
            CompanyFilter.NumberOfEmployee = Company_CompanyFilterDTO.NumberOfEmployee;
            CompanyFilter.CustomerLeadId = Company_CompanyFilterDTO.CustomerLeadId;
            CompanyFilter.ParentId = Company_CompanyFilterDTO.ParentId;
            CompanyFilter.Path = Company_CompanyFilterDTO.Path;
            CompanyFilter.Level = Company_CompanyFilterDTO.Level;
            CompanyFilter.ProfessionId = Company_CompanyFilterDTO.ProfessionId;
            CompanyFilter.AppUserId = Company_CompanyFilterDTO.AppUserId;
            CompanyFilter.CreatorId = Company_CompanyFilterDTO.CreatorId;
            CompanyFilter.CurrencyId = Company_CompanyFilterDTO.CurrencyId;
            CompanyFilter.CompanyStatusId = Company_CompanyFilterDTO.CompanyStatusId;
            CompanyFilter.Description = Company_CompanyFilterDTO.Description;
            CompanyFilter.RowId = Company_CompanyFilterDTO.RowId;
            CompanyFilter.CreatedAt = Company_CompanyFilterDTO.CreatedAt;
            CompanyFilter.UpdatedAt = Company_CompanyFilterDTO.UpdatedAt;
            return CompanyFilter;
        }

        #region activity
        [Route(CompanyRoute.CreateActivity), HttpPost]
        public async Task<ActionResult<Company_CompanyActivityDTO>> CreateActivity([FromBody] Company_CompanyActivityDTO Company_CompanyActivityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyActivity CompanyActivity = ConvertDTOToEntity(Company_CompanyActivityDTO);
            CompanyActivity = await CompanyActivityService.Create(CompanyActivity);
            Company_CompanyActivityDTO = new Company_CompanyActivityDTO(CompanyActivity);
            if (CompanyActivity.IsValidated)
                return Company_CompanyActivityDTO;
            else
                return BadRequest(Company_CompanyActivityDTO);
        }

        [Route(CompanyRoute.UpdateActivity), HttpPost]
        public async Task<ActionResult<Company_CompanyActivityDTO>> UpdateActivity([FromBody] Company_CompanyActivityDTO Company_CompanyActivityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyActivity CompanyActivity = ConvertDTOToEntity(Company_CompanyActivityDTO);
            CompanyActivity = await CompanyActivityService.Update(CompanyActivity);
            Company_CompanyActivityDTO = new Company_CompanyActivityDTO(CompanyActivity);
            if (CompanyActivity.IsValidated)
                return Company_CompanyActivityDTO;
            else
                return BadRequest(Company_CompanyActivityDTO);
        }

        [Route(CompanyRoute.DeleteActivity), HttpPost]
        public async Task<ActionResult<Company_CompanyActivityDTO>> DeleteActivity([FromBody] Company_CompanyActivityDTO Company_CompanyActivityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyActivity CompanyActivity = ConvertDTOToEntity(Company_CompanyActivityDTO);
            CompanyActivity = await CompanyActivityService.Delete(CompanyActivity);
            Company_CompanyActivityDTO = new Company_CompanyActivityDTO(CompanyActivity);
            if (CompanyActivity.IsValidated)
                return Company_CompanyActivityDTO;
            else
                return BadRequest(Company_CompanyActivityDTO);
        }

        [Route(CompanyRoute.BulkDeleteActivity), HttpPost]
        public async Task<ActionResult<bool>> BulkDeleteActivity([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyActivityFilter CompanyActivityFilter = new CompanyActivityFilter();
            CompanyActivityFilter = await CompanyActivityService.ToFilter(CompanyActivityFilter);
            CompanyActivityFilter.Id = new IdFilter { In = Ids };
            CompanyActivityFilter.Selects = CompanyActivitySelect.Id;
            CompanyActivityFilter.Skip = 0;
            CompanyActivityFilter.Take = int.MaxValue;

            List<CompanyActivity> CompanyActivities = await CompanyActivityService.List(CompanyActivityFilter);
            CompanyActivities = await CompanyActivityService.BulkDelete(CompanyActivities);
            if (CompanyActivities.Any(x => !x.IsValidated))
                return BadRequest(CompanyActivities.Where(x => !x.IsValidated));
            return true;
        }

        private CompanyActivity ConvertDTOToEntity(Company_CompanyActivityDTO Company_CompanyActivityDTO)
        {
            CompanyActivity CompanyActivity = new CompanyActivity();
            CompanyActivity.Id = Company_CompanyActivityDTO.Id;
            CompanyActivity.Title = Company_CompanyActivityDTO.Title;
            CompanyActivity.FromDate = Company_CompanyActivityDTO.FromDate;
            CompanyActivity.ToDate = Company_CompanyActivityDTO.ToDate;
            CompanyActivity.ActivityTypeId = Company_CompanyActivityDTO.ActivityTypeId;
            CompanyActivity.ActivityPriorityId = Company_CompanyActivityDTO.ActivityPriorityId;
            CompanyActivity.Description = Company_CompanyActivityDTO.Description;
            CompanyActivity.Address = Company_CompanyActivityDTO.Address;
            CompanyActivity.CompanyId = Company_CompanyActivityDTO.CompanyId;
            CompanyActivity.AppUserId = Company_CompanyActivityDTO.AppUserId;
            CompanyActivity.ActivityStatusId = Company_CompanyActivityDTO.ActivityStatusId;
            CompanyActivity.ActivityPriority = Company_CompanyActivityDTO.ActivityPriority == null ? null : new ActivityPriority
            {
                Id = Company_CompanyActivityDTO.ActivityPriority.Id,
                Code = Company_CompanyActivityDTO.ActivityPriority.Code,
                Name = Company_CompanyActivityDTO.ActivityPriority.Name,
            };
            CompanyActivity.ActivityStatus = Company_CompanyActivityDTO.ActivityStatus == null ? null : new ActivityStatus
            {
                Id = Company_CompanyActivityDTO.ActivityStatus.Id,
                Code = Company_CompanyActivityDTO.ActivityStatus.Code,
                Name = Company_CompanyActivityDTO.ActivityStatus.Name,
            };
            CompanyActivity.ActivityType = Company_CompanyActivityDTO.ActivityType == null ? null : new ActivityType
            {
                Id = Company_CompanyActivityDTO.ActivityType.Id,
                Code = Company_CompanyActivityDTO.ActivityType.Code,
                Name = Company_CompanyActivityDTO.ActivityType.Name,
            };
            CompanyActivity.AppUser = Company_CompanyActivityDTO.AppUser == null ? null : new AppUser
            {
                Id = Company_CompanyActivityDTO.AppUser.Id,
                Username = Company_CompanyActivityDTO.AppUser.Username,
                DisplayName = Company_CompanyActivityDTO.AppUser.DisplayName,
                Address = Company_CompanyActivityDTO.AppUser.Address,
                Email = Company_CompanyActivityDTO.AppUser.Email,
                Phone = Company_CompanyActivityDTO.AppUser.Phone,
                SexId = Company_CompanyActivityDTO.AppUser.SexId,
                Birthday = Company_CompanyActivityDTO.AppUser.Birthday,
                Department = Company_CompanyActivityDTO.AppUser.Department,
                OrganizationId = Company_CompanyActivityDTO.AppUser.OrganizationId,
                StatusId = Company_CompanyActivityDTO.AppUser.StatusId,
            };
            CompanyActivity.BaseLanguage = CurrentContext.Language;
            return CompanyActivity;
        }
        #endregion

        #region CallLog
        [Route(CompanyRoute.DeleteCallLog), HttpPost]
        public async Task<ActionResult<Company_CallLogDTO>> DeleteCallLog([FromBody] Company_CallLogDTO Company_CallLogDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLog CallLog = ConvertCallLog(Company_CallLogDTO);
            CallLog = await CallLogService.Delete(CallLog);
            Company_CallLogDTO = new Company_CallLogDTO(CallLog);
            if (CallLog.IsValidated)
                return Company_CallLogDTO;
            else
                return BadRequest(Company_CallLogDTO);
        }

        private CallLog ConvertCallLog(Company_CallLogDTO Company_CallLogDTO)
        {
            CallLog CallLog = new CallLog();
            CallLog.Id = Company_CallLogDTO.Id;
            CallLog.EntityReferenceId = Company_CallLogDTO.EntityReferenceId;
            CallLog.CallTypeId = Company_CallLogDTO.CallTypeId;
            CallLog.CallEmotionId = Company_CallLogDTO.CallEmotionId;
            CallLog.AppUserId = Company_CallLogDTO.AppUserId;
            CallLog.Title = Company_CallLogDTO.Title;
            CallLog.Content = Company_CallLogDTO.Content;
            CallLog.Phone = Company_CallLogDTO.Phone;
            CallLog.CallTime = Company_CallLogDTO.CallTime;
            CallLog.AppUser = Company_CallLogDTO.AppUser == null ? null : new AppUser
            {
                Id = Company_CallLogDTO.AppUser.Id,
                Username = Company_CallLogDTO.AppUser.Username,
                DisplayName = Company_CallLogDTO.AppUser.DisplayName,
                Address = Company_CallLogDTO.AppUser.Address,
                Email = Company_CallLogDTO.AppUser.Email,
                Phone = Company_CallLogDTO.AppUser.Phone,
                SexId = Company_CallLogDTO.AppUser.SexId,
                Birthday = Company_CallLogDTO.AppUser.Birthday,
                Department = Company_CallLogDTO.AppUser.Department,
                OrganizationId = Company_CallLogDTO.AppUser.OrganizationId,
                StatusId = Company_CallLogDTO.AppUser.StatusId,
            };
            CallLog.EntityReference = Company_CallLogDTO.EntityReference == null ? null : new EntityReference
            {
                Id = Company_CallLogDTO.EntityReference.Id,
                Code = Company_CallLogDTO.EntityReference.Code,
                Name = Company_CallLogDTO.EntityReference.Name,
            };
            CallLog.CallType = Company_CallLogDTO.CallType == null ? null : new CallType
            {
                Id = Company_CallLogDTO.CallType.Id,
                Code = Company_CallLogDTO.CallType.Code,
                Name = Company_CallLogDTO.CallType.Name,
                ColorCode = Company_CallLogDTO.CallType.ColorCode,
                StatusId = Company_CallLogDTO.CallType.StatusId,
                Used = Company_CallLogDTO.CallType.Used,
            };
            CallLog.CallEmotion = Company_CallLogDTO.CallEmotion == null ? null : new CallEmotion
            {
                Id = Company_CallLogDTO.CallEmotion.Id,
                Name = Company_CallLogDTO.CallEmotion.Name,
                Code = Company_CallLogDTO.CallEmotion.Code,
                StatusId = Company_CallLogDTO.CallEmotion.StatusId,
                Description = Company_CallLogDTO.CallEmotion.Description,
            };
            CallLog.BaseLanguage = CurrentContext.Language;
            return CallLog;
        }
        #endregion

        #region Contact
        [Route(CompanyRoute.CreateContact), HttpPost]
        public async Task<ActionResult<Company_ContactDTO>> CreateContact([FromBody] Company_ContactDTO Company_ContactDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Contact Contact = ConvertContact(Company_ContactDTO);
            Contact = await ContactService.Create(Contact);
            Company_ContactDTO = new Company_ContactDTO(Contact);
            if (Contact.IsValidated)
                return Company_ContactDTO;
            else
                return BadRequest(Company_ContactDTO);
        }

        [Route(CompanyRoute.UpdateContact), HttpPost]
        public async Task<ActionResult<Company_ContactDTO>> UpdateContact([FromBody] Company_ContactDTO Company_ContactDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Contact Contact = ConvertContact(Company_ContactDTO);
            Contact = await ContactService.Update(Contact);
            Company_ContactDTO = new Company_ContactDTO(Contact);
            if (Contact.IsValidated)
                return Company_ContactDTO;
            else
                return BadRequest(Company_ContactDTO);
        }

        [Route(CompanyRoute.DeleteContact), HttpPost]
        public async Task<ActionResult<Company_ContactDTO>> DeleteContact([FromBody] Company_ContactDTO Company_ContactDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Contact Contact = ConvertContact(Company_ContactDTO);
            Contact = await ContactService.Delete(Contact);
            Company_ContactDTO = new Company_ContactDTO(Contact);
            if (Contact.IsValidated)
                return Company_ContactDTO;
            else
                return BadRequest(Company_ContactDTO);
        }

        [Route(CompanyRoute.BulkDeleteContact), HttpPost]
        public async Task<ActionResult<bool>> BulkDeleteContact([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactFilter ContactFilter = new ContactFilter();
            ContactFilter = await ContactService.ToFilter(ContactFilter);
            ContactFilter.Id = new IdFilter { In = Ids };
            ContactFilter.Selects = ContactSelect.Id;
            ContactFilter.Skip = 0;
            ContactFilter.Take = int.MaxValue;

            List<Contact> Contacts = await ContactService.List(ContactFilter);
            Contacts = await ContactService.BulkDelete(Contacts);
            if (Contacts.Any(x => !x.IsValidated))
                return BadRequest(Contacts.Where(x => !x.IsValidated));
            return true;
        }

        private Contact ConvertContact(Company_ContactDTO Company_ContactDTO)
        {
            Contact Contact = new Contact();
            Contact.Id = Company_ContactDTO.Id;
            Contact.Name = Company_ContactDTO.Name;
            Contact.ProfessionId = Company_ContactDTO.ProfessionId;
            Contact.CompanyId = Company_ContactDTO.CompanyId;
            Contact.ProvinceId = Company_ContactDTO.ProvinceId;
            Contact.DistrictId = Company_ContactDTO.DistrictId;
            Contact.NationId = Company_ContactDTO.NationId;
            Contact.CustomerLeadId = Company_ContactDTO.CustomerLeadId;
            Contact.ImageId = Company_ContactDTO.ImageId;
            Contact.Description = Company_ContactDTO.Description;
            Contact.Address = Company_ContactDTO.Address;
            Contact.EmailOther = Company_ContactDTO.EmailOther;
            Contact.DateOfBirth = Company_ContactDTO.DateOfBirth;
            Contact.Phone = Company_ContactDTO.Phone;
            Contact.PhoneHome = Company_ContactDTO.PhoneHome;
            Contact.FAX = Company_ContactDTO.FAX;
            Contact.Email = Company_ContactDTO.Email;
            Contact.RefuseReciveEmail = Company_ContactDTO.RefuseReciveEmail;
            Contact.RefuseReciveSMS = Company_ContactDTO.RefuseReciveSMS;
            Contact.ZIPCode = Company_ContactDTO.ZIPCode;
            Contact.SexId = Company_ContactDTO.SexId;
            Contact.AppUserId = Company_ContactDTO.AppUserId;
            Contact.PositionId = Company_ContactDTO.PositionId;
            Contact.Department = Company_ContactDTO.Description;
            Contact.ContactStatusId = Company_ContactDTO.ContactStatusId;
            Contact.AppUser = Company_ContactDTO.AppUser == null ? null : new AppUser
            {
                Id = Company_ContactDTO.AppUser.Id,
                Username = Company_ContactDTO.AppUser.Username,
                DisplayName = Company_ContactDTO.AppUser.DisplayName,
                Address = Company_ContactDTO.AppUser.Address,
                Email = Company_ContactDTO.AppUser.Email,
                Phone = Company_ContactDTO.AppUser.Phone,
                SexId = Company_ContactDTO.AppUser.SexId,
                Birthday = Company_ContactDTO.AppUser.Birthday,
                Department = Company_ContactDTO.AppUser.Department,
                OrganizationId = Company_ContactDTO.AppUser.OrganizationId,
                StatusId = Company_ContactDTO.AppUser.StatusId,
            };
            Contact.Company = Company_ContactDTO.Company == null ? null : new Company
            {
                Id = Company_ContactDTO.Company.Id,
                Name = Company_ContactDTO.Company.Name,
                Phone = Company_ContactDTO.Company.Phone,
                FAX = Company_ContactDTO.Company.FAX,
                PhoneOther = Company_ContactDTO.Company.PhoneOther,
                Email = Company_ContactDTO.Company.Email,
                EmailOther = Company_ContactDTO.Company.EmailOther,
                ZIPCode = Company_ContactDTO.Company.ZIPCode,
                Revenue = Company_ContactDTO.Company.Revenue,
                Website = Company_ContactDTO.Company.Website,
                NationId = Company_ContactDTO.Company.NationId,
                ProvinceId = Company_ContactDTO.Company.ProvinceId,
                DistrictId = Company_ContactDTO.Company.DistrictId,
                Address = Company_ContactDTO.Company.Address,
                NumberOfEmployee = Company_ContactDTO.Company.NumberOfEmployee,
                RefuseReciveEmail = Company_ContactDTO.Company.RefuseReciveEmail,
                RefuseReciveSMS = Company_ContactDTO.Company.RefuseReciveSMS,
                CustomerLeadId = Company_ContactDTO.Company.CustomerLeadId,
                ParentId = Company_ContactDTO.Company.ParentId,
                Path = Company_ContactDTO.Company.Path,
                Level = Company_ContactDTO.Company.Level,
                ProfessionId = Company_ContactDTO.Company.ProfessionId,
                AppUserId = Company_ContactDTO.Company.AppUserId,
                CurrencyId = Company_ContactDTO.Company.CurrencyId,
                CompanyStatusId = Company_ContactDTO.Company.CompanyStatusId,
                Description = Company_ContactDTO.Company.Description,
                RowId = Company_ContactDTO.Company.RowId,
            };
            Contact.ContactStatus = Company_ContactDTO.ContactStatus == null ? null : new ContactStatus
            {
                Id = Company_ContactDTO.ContactStatus.Id,
                Code = Company_ContactDTO.ContactStatus.Code,
                Name = Company_ContactDTO.ContactStatus.Name,
            };
            Contact.CustomerLead = Company_ContactDTO.CustomerLead == null ? null : new CustomerLead
            {
                Id = Company_ContactDTO.CustomerLead.Id,
                Name = Company_ContactDTO.CustomerLead.Name,
                TelePhone = Company_ContactDTO.CustomerLead.TelePhone,
                Phone = Company_ContactDTO.CustomerLead.Phone,
                CompanyName = Company_ContactDTO.CustomerLead.CompanyName,
                Fax = Company_ContactDTO.CustomerLead.Fax,
                Email = Company_ContactDTO.CustomerLead.Email,
                SecondEmail = Company_ContactDTO.CustomerLead.SecondEmail,
                Website = Company_ContactDTO.CustomerLead.Website,
                CustomerLeadSourceId = Company_ContactDTO.CustomerLead.CustomerLeadSourceId,
                CustomerLeadLevelId = Company_ContactDTO.CustomerLead.CustomerLeadLevelId,
                CompanyId = Company_ContactDTO.CustomerLead.CompanyId,
                CampaignId = Company_ContactDTO.CustomerLead.CampaignId,
                ProfessionId = Company_ContactDTO.CustomerLead.ProfessionId,
                Revenue = Company_ContactDTO.CustomerLead.Revenue,
                EmployeeQuantity = Company_ContactDTO.CustomerLead.EmployeeQuantity,
                Address = Company_ContactDTO.CustomerLead.Address,
                ProvinceId = Company_ContactDTO.CustomerLead.ProvinceId,
                DistrictId = Company_ContactDTO.CustomerLead.DistrictId,
                CustomerLeadStatusId = Company_ContactDTO.CustomerLead.CustomerLeadStatusId,
                BusinessRegistrationCode = Company_ContactDTO.CustomerLead.BusinessRegistrationCode,
                SexId = Company_ContactDTO.CustomerLead.SexId,
                RefuseReciveSMS = Company_ContactDTO.CustomerLead.RefuseReciveSMS,
                NationId = Company_ContactDTO.CustomerLead.NationId,
                RefuseReciveEmail = Company_ContactDTO.CustomerLead.RefuseReciveEmail,
                Description = Company_ContactDTO.CustomerLead.Description,
                ZipCode = Company_ContactDTO.CustomerLead.ZipCode,
            };
            Contact.District = Company_ContactDTO.District == null ? null : new District
            {
                Id = Company_ContactDTO.District.Id,
                Code = Company_ContactDTO.District.Code,
                Name = Company_ContactDTO.District.Name,
                Priority = Company_ContactDTO.District.Priority,
                ProvinceId = Company_ContactDTO.District.ProvinceId,
                StatusId = Company_ContactDTO.District.StatusId,
            };
            Contact.Image = Company_ContactDTO.Image == null ? null : new Image
            {
                Id = Company_ContactDTO.Image.Id,
                Name = Company_ContactDTO.Image.Name,
                Url = Company_ContactDTO.Image.Url,
            };
            Contact.Nation = Company_ContactDTO.Nation == null ? null : new Nation
            {
                Id = Company_ContactDTO.Nation.Id,
                Code = Company_ContactDTO.Nation.Code,
                Name = Company_ContactDTO.Nation.Name,
                StatusId = Company_ContactDTO.Nation.StatusId,
            };
            Contact.Position = Company_ContactDTO.Position == null ? null : new Position
            {
                Id = Company_ContactDTO.Position.Id,
                Code = Company_ContactDTO.Position.Code,
                Name = Company_ContactDTO.Position.Name,
                StatusId = Company_ContactDTO.Position.StatusId,
            };
            Contact.Profession = Company_ContactDTO.Profession == null ? null : new Profession
            {
                Id = Company_ContactDTO.Profession.Id,
                Code = Company_ContactDTO.Profession.Code,
                Name = Company_ContactDTO.Profession.Name,
            };
            Contact.Province = Company_ContactDTO.Province == null ? null : new Province
            {
                Id = Company_ContactDTO.Province.Id,
                Code = Company_ContactDTO.Province.Code,
                Name = Company_ContactDTO.Province.Name,
                Priority = Company_ContactDTO.Province.Priority,
                StatusId = Company_ContactDTO.Province.StatusId,
            };
            Contact.Sex = Company_ContactDTO.Sex == null ? null : new Sex
            {
                Id = Company_ContactDTO.Sex.Id,
                Code = Company_ContactDTO.Sex.Code,
                Name = Company_ContactDTO.Sex.Name,
            };
            Contact.BaseLanguage = CurrentContext.Language;
            return Contact;
        }
        #endregion

        #region OrderQuote
        [Route(CompanyRoute.CreateOrderQuote), HttpPost]
        public async Task<ActionResult<Company_OrderQuoteDTO>> CreateOrderQuote([FromBody] Company_OrderQuoteDTO Company_OrderQuoteDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuote OrderQuote = ConvertOrderQuote(Company_OrderQuoteDTO);

            OrderQuote.CreatorId = CurrentContext.UserId;
            OrderQuote = await OrderQuoteService.Create(OrderQuote);
            Company_OrderQuoteDTO = new Company_OrderQuoteDTO(OrderQuote);
            if (OrderQuote.IsValidated)
                return Company_OrderQuoteDTO;
            else
                return BadRequest(Company_OrderQuoteDTO);
        }

        [Route(CompanyRoute.UpdateOrderQuote), HttpPost]
        public async Task<ActionResult<Company_OrderQuoteDTO>> UpdateOrderQuote([FromBody] Company_OrderQuoteDTO Company_OrderQuoteDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuote OrderQuote = ConvertOrderQuote(Company_OrderQuoteDTO);
            OrderQuote = await OrderQuoteService.Update(OrderQuote);
            Company_OrderQuoteDTO = new Company_OrderQuoteDTO(OrderQuote);
            if (OrderQuote.IsValidated)
                return Company_OrderQuoteDTO;
            else
                return BadRequest(Company_OrderQuoteDTO);
        }

        [Route(CompanyRoute.DeleteOrderQuote), HttpPost]
        public async Task<ActionResult<Company_OrderQuoteDTO>> DeleteOrderQuote([FromBody] Company_OrderQuoteDTO Company_OrderQuoteDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuote OrderQuote = ConvertOrderQuote(Company_OrderQuoteDTO);
            OrderQuote = await OrderQuoteService.Delete(OrderQuote);
            Company_OrderQuoteDTO = new Company_OrderQuoteDTO(OrderQuote);
            if (OrderQuote.IsValidated)
                return Company_OrderQuoteDTO;
            else
                return BadRequest(Company_OrderQuoteDTO);
        }

        [Route(CompanyRoute.BulkDeleteOrderQuote), HttpPost]
        public async Task<ActionResult<bool>> BulkDeleteOrderQuote([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuoteFilter OrderQuoteFilter = new OrderQuoteFilter();
            OrderQuoteFilter = await OrderQuoteService.ToFilter(OrderQuoteFilter);
            OrderQuoteFilter.Id = new IdFilter { In = Ids };
            OrderQuoteFilter.Selects = OrderQuoteSelect.Id;
            OrderQuoteFilter.Skip = 0;
            OrderQuoteFilter.Take = int.MaxValue;

            List<OrderQuote> OrderQuotes = await OrderQuoteService.List(OrderQuoteFilter);
            OrderQuotes = await OrderQuoteService.BulkDelete(OrderQuotes);
            if (OrderQuotes.Any(x => !x.IsValidated))
                return BadRequest(OrderQuotes.Where(x => !x.IsValidated));
            return true;
        }

        private OrderQuote ConvertOrderQuote(Company_OrderQuoteDTO Company_OrderQuoteDTO)
        {
            OrderQuote OrderQuote = new OrderQuote();
            OrderQuote.Id = Company_OrderQuoteDTO.Id;
            OrderQuote.Subject = Company_OrderQuoteDTO.Subject;
            OrderQuote.NationId = Company_OrderQuoteDTO.NationId;
            OrderQuote.ProvinceId = Company_OrderQuoteDTO.ProvinceId;
            OrderQuote.DistrictId = Company_OrderQuoteDTO.DistrictId;
            OrderQuote.Address = Company_OrderQuoteDTO.Address;
            OrderQuote.InvoiceAddress = Company_OrderQuoteDTO.InvoiceAddress;
            OrderQuote.InvoiceProvinceId = Company_OrderQuoteDTO.InvoiceProvinceId;
            OrderQuote.InvoiceDistrictId = Company_OrderQuoteDTO.InvoiceDistrictId;
            OrderQuote.InvoiceNationId = Company_OrderQuoteDTO.InvoiceNationId;
            OrderQuote.EditedPriceStatusId = Company_OrderQuoteDTO.EditedPriceStatusId;
            OrderQuote.ZIPCode = Company_OrderQuoteDTO.ZIPCode;
            OrderQuote.InvoiceZIPCode = Company_OrderQuoteDTO.InvoiceZIPCode;
            OrderQuote.AppUserId = Company_OrderQuoteDTO.UserId;
            OrderQuote.ContactId = Company_OrderQuoteDTO.ContactId;
            OrderQuote.CompanyId = Company_OrderQuoteDTO.CompanyId;
            OrderQuote.OpportunityId = Company_OrderQuoteDTO.OpportunityId;
            OrderQuote.OrderQuoteStatusId = Company_OrderQuoteDTO.OrderQuoteStatusId;
            OrderQuote.SubTotal = Company_OrderQuoteDTO.SubTotal;
            OrderQuote.Total = Company_OrderQuoteDTO.Total;
            OrderQuote.TotalTaxAmount = Company_OrderQuoteDTO.TotalTaxAmount;
            OrderQuote.TotalTaxAmountOther = Company_OrderQuoteDTO.TotalTaxAmountOther;
            OrderQuote.GeneralDiscountPercentage = Company_OrderQuoteDTO.GeneralDiscountPercentage;
            OrderQuote.GeneralDiscountAmount = Company_OrderQuoteDTO.GeneralDiscountAmount;
            OrderQuote.EditedPriceStatus = Company_OrderQuoteDTO.EditedPriceStatus == null ? null : new EditedPriceStatus
            {
                Id = Company_OrderQuoteDTO.EditedPriceStatus.Id,
                Code = Company_OrderQuoteDTO.EditedPriceStatus.Code,
                Name = Company_OrderQuoteDTO.EditedPriceStatus.Name,
            };
            OrderQuote.Company = Company_OrderQuoteDTO.Company == null ? null : new Company
            {
                Id = Company_OrderQuoteDTO.Company.Id,
                Name = Company_OrderQuoteDTO.Company.Name,
                Phone = Company_OrderQuoteDTO.Company.Phone,
                FAX = Company_OrderQuoteDTO.Company.FAX,
                PhoneOther = Company_OrderQuoteDTO.Company.PhoneOther,
                Email = Company_OrderQuoteDTO.Company.Email,
                EmailOther = Company_OrderQuoteDTO.Company.EmailOther,
            };
            OrderQuote.Contact = Company_OrderQuoteDTO.Contact == null ? null : new Contact
            {
                Id = Company_OrderQuoteDTO.Contact.Id,
                Name = Company_OrderQuoteDTO.Contact.Name,
                ProfessionId = Company_OrderQuoteDTO.Contact.ProfessionId,
                CompanyId = Company_OrderQuoteDTO.Contact.CompanyId,
                RefuseReciveEmail = Company_OrderQuoteDTO.Contact.RefuseReciveEmail,
                RefuseReciveSMS = Company_OrderQuoteDTO.Contact.RefuseReciveSMS,
            };
            OrderQuote.District = Company_OrderQuoteDTO.District == null ? null : new District
            {
                Id = Company_OrderQuoteDTO.District.Id,
                Code = Company_OrderQuoteDTO.District.Code,
                Name = Company_OrderQuoteDTO.District.Name,
                Priority = Company_OrderQuoteDTO.District.Priority,
                ProvinceId = Company_OrderQuoteDTO.District.ProvinceId,
                StatusId = Company_OrderQuoteDTO.District.StatusId,
            };
            OrderQuote.InvoiceDistrict = Company_OrderQuoteDTO.InvoiceDistrict == null ? null : new District
            {
                Id = Company_OrderQuoteDTO.InvoiceDistrict.Id,
                Code = Company_OrderQuoteDTO.InvoiceDistrict.Code,
                Name = Company_OrderQuoteDTO.InvoiceDistrict.Name,
                Priority = Company_OrderQuoteDTO.InvoiceDistrict.Priority,
                ProvinceId = Company_OrderQuoteDTO.InvoiceDistrict.ProvinceId,
                StatusId = Company_OrderQuoteDTO.InvoiceDistrict.StatusId,
            };
            OrderQuote.InvoiceNation = Company_OrderQuoteDTO.InvoiceNation == null ? null : new Nation
            {
                Id = Company_OrderQuoteDTO.InvoiceNation.Id,
                Code = Company_OrderQuoteDTO.InvoiceNation.Code,
                Name = Company_OrderQuoteDTO.InvoiceNation.Name,
                StatusId = Company_OrderQuoteDTO.InvoiceNation.StatusId,
            };
            OrderQuote.InvoiceProvince = Company_OrderQuoteDTO.InvoiceProvince == null ? null : new Province
            {
                Id = Company_OrderQuoteDTO.InvoiceProvince.Id,
                Code = Company_OrderQuoteDTO.InvoiceProvince.Code,
                Name = Company_OrderQuoteDTO.InvoiceProvince.Name,
                Priority = Company_OrderQuoteDTO.InvoiceProvince.Priority,
                StatusId = Company_OrderQuoteDTO.InvoiceProvince.StatusId,
            };
            OrderQuote.Nation = Company_OrderQuoteDTO.Nation == null ? null : new Nation
            {
                Id = Company_OrderQuoteDTO.Nation.Id,
                Code = Company_OrderQuoteDTO.Nation.Code,
                Name = Company_OrderQuoteDTO.Nation.Name,
                StatusId = Company_OrderQuoteDTO.Nation.StatusId,
            };
            OrderQuote.Opportunity = Company_OrderQuoteDTO.Opportunity == null ? null : new Opportunity
            {
                Id = Company_OrderQuoteDTO.Opportunity.Id,
                Name = Company_OrderQuoteDTO.Opportunity.Name,
                CompanyId = Company_OrderQuoteDTO.Opportunity.CompanyId,
                CustomerLeadId = Company_OrderQuoteDTO.Opportunity.CustomerLeadId,
                ClosingDate = Company_OrderQuoteDTO.Opportunity.ClosingDate,
                SaleStageId = Company_OrderQuoteDTO.Opportunity.SaleStageId,
                ProbabilityId = Company_OrderQuoteDTO.Opportunity.ProbabilityId,
                PotentialResultId = Company_OrderQuoteDTO.Opportunity.PotentialResultId,
                LeadSourceId = Company_OrderQuoteDTO.Opportunity.LeadSourceId,
                AppUserId = Company_OrderQuoteDTO.Opportunity.AppUserId,
                CurrencyId = Company_OrderQuoteDTO.Opportunity.CurrencyId,
                Amount = Company_OrderQuoteDTO.Opportunity.Amount,
                ForecastAmount = Company_OrderQuoteDTO.Opportunity.ForecastAmount,
                Description = Company_OrderQuoteDTO.Opportunity.Description,
                RefuseReciveSMS = Company_OrderQuoteDTO.Opportunity.RefuseReciveSMS,
                RefuseReciveEmail = Company_OrderQuoteDTO.Opportunity.RefuseReciveEmail,
                OpportunityResultTypeId = Company_OrderQuoteDTO.Opportunity.OpportunityResultTypeId,
                CreatorId = Company_OrderQuoteDTO.Opportunity.CreatorId,
            };
            OrderQuote.OrderQuoteStatus = Company_OrderQuoteDTO.OrderQuoteStatus == null ? null : new OrderQuoteStatus
            {
                Id = Company_OrderQuoteDTO.OrderQuoteStatus.Id,
                Code = Company_OrderQuoteDTO.OrderQuoteStatus.Code,
                Name = Company_OrderQuoteDTO.OrderQuoteStatus.Name,
            };
            OrderQuote.Province = Company_OrderQuoteDTO.Province == null ? null : new Province
            {
                Id = Company_OrderQuoteDTO.Province.Id,
                Code = Company_OrderQuoteDTO.Province.Code,
                Name = Company_OrderQuoteDTO.Province.Name,
                Priority = Company_OrderQuoteDTO.Province.Priority,
                StatusId = Company_OrderQuoteDTO.Province.StatusId,
            };
            OrderQuote.AppUser = Company_OrderQuoteDTO.AppUser == null ? null : new AppUser
            {
                Id = Company_OrderQuoteDTO.AppUser.Id,
                Username = Company_OrderQuoteDTO.AppUser.Username,
                DisplayName = Company_OrderQuoteDTO.AppUser.DisplayName,
                Address = Company_OrderQuoteDTO.AppUser.Address,
                Email = Company_OrderQuoteDTO.AppUser.Email,
                Phone = Company_OrderQuoteDTO.AppUser.Phone,
                SexId = Company_OrderQuoteDTO.AppUser.SexId,
                Birthday = Company_OrderQuoteDTO.AppUser.Birthday,
                Department = Company_OrderQuoteDTO.AppUser.Department,
                OrganizationId = Company_OrderQuoteDTO.AppUser.OrganizationId,
                StatusId = Company_OrderQuoteDTO.AppUser.StatusId,
            };
            OrderQuote.OrderQuoteContents = Company_OrderQuoteDTO.OrderQuoteContents?
                .Select(x => new OrderQuoteContent
                {
                    Id = x.Id,
                    ItemId = x.ItemId,
                    UnitOfMeasureId = x.UnitOfMeasureId,
                    Quantity = x.Quantity,
                    PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                    RequestedQuantity = x.RequestedQuantity,
                    PrimaryPrice = x.PrimaryPrice,
                    SalePrice = x.SalePrice,
                    EditedPriceStatusId = x.EditedPriceStatusId,
                    DiscountPercentage = x.DiscountPercentage,
                    DiscountAmount = x.DiscountAmount,
                    GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                    GeneralDiscountAmount = x.GeneralDiscountAmount,
                    Amount = x.Amount,
                    TaxPercentage = x.TaxPercentage,
                    TaxPercentageOther = x.TaxPercentageOther,
                    TaxAmount = x.TaxAmount,
                    Factor = x.Factor,
                    EditedPriceStatus = x.EditedPriceStatus == null ? null : new EditedPriceStatus
                    {
                        Id = x.EditedPriceStatus.Id,
                        Code = x.EditedPriceStatus.Code,
                        Name = x.EditedPriceStatus.Name,
                    },
                    Item = x.Item == null ? null : new Item
                    {
                        Id = x.Item.Id,
                        Code = x.Item.Code,
                        Name = x.Item.Name,
                        ProductId = x.Item.ProductId,
                        RetailPrice = x.Item.RetailPrice,
                        SalePrice = x.Item.SalePrice,
                        SaleStock = x.Item.SaleStock,
                        ScanCode = x.Item.ScanCode,
                        StatusId = x.Item.StatusId,
                        Product = x.Item.Product == null ? null : new Product
                        {
                            Id = x.Item.Product.Id,
                            Code = x.Item.Product.Code,
                            SupplierCode = x.Item.Product.SupplierCode,
                            Name = x.Item.Product.Name,
                            Description = x.Item.Product.Description,
                            ScanCode = x.Item.Product.ScanCode,
                            ProductTypeId = x.Item.Product.ProductTypeId,
                            SupplierId = x.Item.Product.SupplierId,
                            BrandId = x.Item.Product.BrandId,
                            UnitOfMeasureId = x.Item.Product.UnitOfMeasureId,
                            UnitOfMeasureGroupingId = x.Item.Product.UnitOfMeasureGroupingId,
                            RetailPrice = x.Item.Product.RetailPrice,
                            TaxTypeId = x.Item.Product.TaxTypeId,
                            StatusId = x.Item.Product.StatusId,
                            ProductType = x.Item.Product.ProductType == null ? null : new ProductType
                            {
                                Id = x.Item.Product.ProductType.Id,
                                Code = x.Item.Product.ProductType.Code,
                                Name = x.Item.Product.ProductType.Name,
                                Description = x.Item.Product.ProductType.Description,
                                StatusId = x.Item.Product.ProductType.StatusId,
                            },
                            TaxType = x.Item.Product.TaxType == null ? null : new TaxType
                            {
                                Id = x.Item.Product.TaxType.Id,
                                Code = x.Item.Product.TaxType.Code,
                                StatusId = x.Item.Product.TaxType.StatusId,
                                Name = x.Item.Product.TaxType.Name,
                                Percentage = x.Item.Product.TaxType.Percentage,
                            },
                            UnitOfMeasure = x.Item.Product.UnitOfMeasure == null ? null : new UnitOfMeasure
                            {
                                Id = x.Item.Product.UnitOfMeasure.Id,
                                Code = x.Item.Product.UnitOfMeasure.Code,
                                Name = x.Item.Product.UnitOfMeasure.Name,
                                Description = x.Item.Product.UnitOfMeasure.Description,
                                StatusId = x.Item.Product.UnitOfMeasure.StatusId,
                            },
                        }
                    },
                    PrimaryUnitOfMeasure = x.PrimaryUnitOfMeasure == null ? null : new UnitOfMeasure
                    {
                        Id = x.PrimaryUnitOfMeasure.Id,
                        Code = x.PrimaryUnitOfMeasure.Code,
                        Name = x.PrimaryUnitOfMeasure.Name,
                        Description = x.PrimaryUnitOfMeasure.Description,
                        StatusId = x.PrimaryUnitOfMeasure.StatusId,
                    },
                    UnitOfMeasure = x.UnitOfMeasure == null ? null : new UnitOfMeasure
                    {
                        Id = x.UnitOfMeasure.Id,
                        Code = x.UnitOfMeasure.Code,
                        Name = x.UnitOfMeasure.Name,
                        Description = x.UnitOfMeasure.Description,
                        StatusId = x.UnitOfMeasure.StatusId,
                    },

                }).ToList();
            OrderQuote.BaseLanguage = CurrentContext.Language;
            return OrderQuote;
        }
        #endregion

        [Route(CompanyRoute.SendSms), HttpPost]
        public async Task<ActionResult<bool>> SendSms([FromBody] Company_SmsQueueDTO Company_SmsQueueDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            SmsQueue SmsQueue = new SmsQueue();
            SmsQueue.Id = Company_SmsQueueDTO.Id;
            SmsQueue.Phone = Company_SmsQueueDTO.Phone;
            SmsQueue.SmsCode = Company_SmsQueueDTO.SmsCode;
            SmsQueue.SmsTitle = Company_SmsQueueDTO.SmsTitle;
            SmsQueue.SmsContent = Company_SmsQueueDTO.SmsContent;
            SmsQueue.SentByAppUserId = CurrentContext.UserId;
            SmsQueue.SmsQueueStatusId = Company_SmsQueueDTO.SmsQueueStatusId;
            SmsQueue.SmsQueueStatus = Company_SmsQueueDTO.SmsQueueStatus == null ? null : new SmsQueueStatus
            {
                Id = Company_SmsQueueDTO.SmsQueueStatus.Id,
                Code = Company_SmsQueueDTO.SmsQueueStatus.Code,
                Name = Company_SmsQueueDTO.SmsQueueStatus.Name,
            };
            SmsQueue.BaseLanguage = CurrentContext.Language;

            SmsQueue.EntityReferenceId = Company_SmsQueueDTO.EntityReferenceId;
            return true;
        }

        #region Email
        [Route(CompanyRoute.CreateEmail), HttpPost]
        public async Task<ActionResult<Company_CompanyEmailDTO>> CreateEmail([FromBody] Company_CompanyEmailDTO Company_CompanyEmailDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CompanyEmail CompanyEmail = ConvertCompanyEmail(Company_CompanyEmailDTO);
            CompanyEmail = await CompanyEmailService.Create(CompanyEmail);
            Company_CompanyEmailDTO = new Company_CompanyEmailDTO(CompanyEmail);
            if (CompanyEmail.IsValidated)
                return Company_CompanyEmailDTO;
            else
                return BadRequest(Company_CompanyEmailDTO);
        }

        [Route(CompanyRoute.SendEmail), HttpPost]
        public async Task<ActionResult<bool>> SendEmail([FromBody] Company_CompanyEmailDTO Company_CompanyEmailDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            CompanyEmail CompanyEmail = ConvertCompanyEmail(Company_CompanyEmailDTO);
            CompanyEmail = await CompanyEmailService.Send(CompanyEmail);
            if (CompanyEmail.IsValidated)
                return Ok();
            else
                return BadRequest(Company_CompanyEmailDTO);
        }

        private CompanyEmail ConvertCompanyEmail(Company_CompanyEmailDTO Company_CompanyEmailDTO)
        {
            CompanyEmail CompanyEmail = new CompanyEmail();
            CompanyEmail.Id = Company_CompanyEmailDTO.Id;
            CompanyEmail.Reciepient = Company_CompanyEmailDTO.Reciepient;
            CompanyEmail.Title = Company_CompanyEmailDTO.Title;
            CompanyEmail.Content = Company_CompanyEmailDTO.Content;
            CompanyEmail.CreatorId = Company_CompanyEmailDTO.CreatorId;
            CompanyEmail.CompanyId = Company_CompanyEmailDTO.CompanyId;
            CompanyEmail.EmailStatusId = Company_CompanyEmailDTO.EmailStatusId;
            CompanyEmail.EmailStatus = Company_CompanyEmailDTO.EmailStatus == null ? null : new EmailStatus
            {
                Id = Company_CompanyEmailDTO.EmailStatus.Id,
                Code = Company_CompanyEmailDTO.EmailStatus.Code,
                Name = Company_CompanyEmailDTO.EmailStatus.Name,
            };
            CompanyEmail.CompanyEmailCCMappings = Company_CompanyEmailDTO.CompanyEmailCCMappings?.Select(x => new CompanyEmailCCMapping
            {
                AppUserId = x.AppUserId,
                CompanyEmailId = x.CompanyEmailId,
                AppUser = x.AppUser == null ? null : new AppUser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Email = x.AppUser.Email,
                },
            }).ToList();
            CompanyEmail.BaseLanguage = CurrentContext.Language;
            return CompanyEmail;
        }
        #endregion

        #region opportunity
        [Route(CompanyRoute.CreateOpportunity), HttpPost]
        public async Task<ActionResult<Company_OpportunityDTO>> CreateOpportunity([FromBody] Company_OpportunityDTO Company_OpportunityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Opportunity Opportunity = ConvertOpportunity(Company_OpportunityDTO);
            Opportunity = await OpportunityService.Create(Opportunity);
            Company_OpportunityDTO = new Company_OpportunityDTO(Opportunity);
            if (Opportunity.IsValidated)
                return Company_OpportunityDTO;
            else
                return BadRequest(Company_OpportunityDTO);
        }

        [Route(CompanyRoute.UpdateOpportunity), HttpPost]
        public async Task<ActionResult<Company_OpportunityDTO>> UpdateOpportunity([FromBody] Company_OpportunityDTO Company_OpportunityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Company_OpportunityDTO.Id))
                return Forbid();

            Opportunity Opportunity = ConvertOpportunity(Company_OpportunityDTO);
            Opportunity = await OpportunityService.Update(Opportunity);
            Company_OpportunityDTO = new Company_OpportunityDTO(Opportunity);
            if (Opportunity.IsValidated)
                return Company_OpportunityDTO;
            else
                return BadRequest(Company_OpportunityDTO);
        }

        [Route(CompanyRoute.BulkDeleteOpportunity), HttpPost]
        public async Task<ActionResult<bool>> BulkDeleteOpportunity([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityFilter OpportunityFilter = new OpportunityFilter();
            OpportunityFilter = await OpportunityService.ToFilter(OpportunityFilter);
            OpportunityFilter.Id = new IdFilter { In = Ids };
            OpportunityFilter.Selects = OpportunitySelect.Id;
            OpportunityFilter.Skip = 0;
            OpportunityFilter.Take = int.MaxValue;

            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            Opportunities = await OpportunityService.BulkDelete(Opportunities);
            if (Opportunities.Any(x => !x.IsValidated))
                return BadRequest(Opportunities.Where(x => !x.IsValidated));
            return true;
        }

        [Route(CompanyRoute.DeleteOpportunity), HttpPost]
        public async Task<ActionResult<Company_OpportunityDTO>> DeleteOpportunity([FromBody] Company_OpportunityDTO Company_OpportunityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Company_OpportunityDTO.Id))
                return Forbid();

            Opportunity Opportunity = ConvertOpportunity(Company_OpportunityDTO);
            Opportunity = await OpportunityService.Delete(Opportunity);
            Company_OpportunityDTO = new Company_OpportunityDTO(Opportunity);
            if (Opportunity.IsValidated)
                return Company_OpportunityDTO;
            else
                return BadRequest(Company_OpportunityDTO);
        }

        private Opportunity ConvertOpportunity(Company_OpportunityDTO Company_OpportunityDTO)
        {
            Opportunity Opportunity = new Opportunity();
            Opportunity.Id = Company_OpportunityDTO.Id;
            Opportunity.Name = Company_OpportunityDTO.Name;
            Opportunity.CompanyId = Company_OpportunityDTO.CompanyId;
            Opportunity.CustomerLeadId = Company_OpportunityDTO.CustomerLeadId;
            Opportunity.ClosingDate = Company_OpportunityDTO.ClosingDate;
            Opportunity.SaleStageId = Company_OpportunityDTO.SaleStageId;
            Opportunity.ProbabilityId = Company_OpportunityDTO.ProbabilityId;
            Opportunity.PotentialResultId = Company_OpportunityDTO.PotentialResultId;
            Opportunity.LeadSourceId = Company_OpportunityDTO.LeadSourceId;
            Opportunity.AppUserId = Company_OpportunityDTO.AppUserId;
            Opportunity.CurrencyId = Company_OpportunityDTO.CurrencyId;
            Opportunity.Amount = Company_OpportunityDTO.Amount;
            Opportunity.ForecastAmount = Company_OpportunityDTO.ForecastAmount;
            Opportunity.Description = Company_OpportunityDTO.Description;
            Opportunity.RefuseReciveSMS = Company_OpportunityDTO.RefuseReciveSMS;
            Opportunity.RefuseReciveEmail = Company_OpportunityDTO.RefuseReciveEmail;
            Opportunity.OpportunityResultTypeId = Company_OpportunityDTO.OpportunityResultTypeId;
            Opportunity.CreatorId = Company_OpportunityDTO.CreatorId;
            Opportunity.AppUser = Company_OpportunityDTO.AppUser == null ? null : new AppUser
            {
                Id = Company_OpportunityDTO.AppUser.Id,
                Username = Company_OpportunityDTO.AppUser.Username,
                DisplayName = Company_OpportunityDTO.AppUser.DisplayName,
                Address = Company_OpportunityDTO.AppUser.Address,
                Email = Company_OpportunityDTO.AppUser.Email,
                Phone = Company_OpportunityDTO.AppUser.Phone,
                SexId = Company_OpportunityDTO.AppUser.SexId,
                Birthday = Company_OpportunityDTO.AppUser.Birthday,
                Avatar = Company_OpportunityDTO.AppUser.Avatar,
                Department = Company_OpportunityDTO.AppUser.Department,
                OrganizationId = Company_OpportunityDTO.AppUser.OrganizationId,
                Longitude = Company_OpportunityDTO.AppUser.Longitude,
                Latitude = Company_OpportunityDTO.AppUser.Latitude,
                StatusId = Company_OpportunityDTO.AppUser.StatusId,
                RowId = Company_OpportunityDTO.AppUser.RowId,
                Used = Company_OpportunityDTO.AppUser.Used,
            };
            Opportunity.Company = Company_OpportunityDTO.Company == null ? null : new Company
            {
                Id = Company_OpportunityDTO.Company.Id,
                Name = Company_OpportunityDTO.Company.Name,
                Phone = Company_OpportunityDTO.Company.Phone,
                FAX = Company_OpportunityDTO.Company.FAX,
                PhoneOther = Company_OpportunityDTO.Company.PhoneOther,
                Email = Company_OpportunityDTO.Company.Email,
                EmailOther = Company_OpportunityDTO.Company.EmailOther,
                ZIPCode = Company_OpportunityDTO.Company.ZIPCode,
                Revenue = Company_OpportunityDTO.Company.Revenue,
                Website = Company_OpportunityDTO.Company.Website,
                Address = Company_OpportunityDTO.Company.Address,
                NationId = Company_OpportunityDTO.Company.NationId,
                ProvinceId = Company_OpportunityDTO.Company.ProvinceId,
                DistrictId = Company_OpportunityDTO.Company.DistrictId,
                NumberOfEmployee = Company_OpportunityDTO.Company.NumberOfEmployee,
                RefuseReciveEmail = Company_OpportunityDTO.Company.RefuseReciveEmail,
                RefuseReciveSMS = Company_OpportunityDTO.Company.RefuseReciveSMS,
                CustomerLeadId = Company_OpportunityDTO.Company.CustomerLeadId,
                ParentId = Company_OpportunityDTO.Company.ParentId,
                Path = Company_OpportunityDTO.Company.Path,
                Level = Company_OpportunityDTO.Company.Level,
                ProfessionId = Company_OpportunityDTO.Company.ProfessionId,
                AppUserId = Company_OpportunityDTO.Company.AppUserId,
                CreatorId = Company_OpportunityDTO.Company.CreatorId,
                CurrencyId = Company_OpportunityDTO.Company.CurrencyId,
                CompanyStatusId = Company_OpportunityDTO.Company.CompanyStatusId,
                Description = Company_OpportunityDTO.Company.Description,
                RowId = Company_OpportunityDTO.Company.RowId,
            };
            Opportunity.Currency = Company_OpportunityDTO.Currency == null ? null : new Currency
            {
                Id = Company_OpportunityDTO.Currency.Id,
                Code = Company_OpportunityDTO.Currency.Code,
                Name = Company_OpportunityDTO.Currency.Name,
            };
            Opportunity.CustomerLead = Company_OpportunityDTO.CustomerLead == null ? null : new CustomerLead
            {
                Id = Company_OpportunityDTO.CustomerLead.Id,
                Name = Company_OpportunityDTO.CustomerLead.Name,
                CompanyName = Company_OpportunityDTO.CustomerLead.CompanyName,
                TelePhone = Company_OpportunityDTO.CustomerLead.TelePhone,
                Phone = Company_OpportunityDTO.CustomerLead.Phone,
                Fax = Company_OpportunityDTO.CustomerLead.Fax,
                Email = Company_OpportunityDTO.CustomerLead.Email,
                SecondEmail = Company_OpportunityDTO.CustomerLead.SecondEmail,
                Website = Company_OpportunityDTO.CustomerLead.Website,
                CustomerLeadSourceId = Company_OpportunityDTO.CustomerLead.CustomerLeadSourceId,
                CustomerLeadLevelId = Company_OpportunityDTO.CustomerLead.CustomerLeadLevelId,
                CompanyId = Company_OpportunityDTO.CustomerLead.CompanyId,
                CampaignId = Company_OpportunityDTO.CustomerLead.CampaignId,
                ProfessionId = Company_OpportunityDTO.CustomerLead.ProfessionId,
                Revenue = Company_OpportunityDTO.CustomerLead.Revenue,
                EmployeeQuantity = Company_OpportunityDTO.CustomerLead.EmployeeQuantity,
                Address = Company_OpportunityDTO.CustomerLead.Address,
                NationId = Company_OpportunityDTO.CustomerLead.NationId,
                ProvinceId = Company_OpportunityDTO.CustomerLead.ProvinceId,
                DistrictId = Company_OpportunityDTO.CustomerLead.DistrictId,
                CustomerLeadStatusId = Company_OpportunityDTO.CustomerLead.CustomerLeadStatusId,
                BusinessRegistrationCode = Company_OpportunityDTO.CustomerLead.BusinessRegistrationCode,
                SexId = Company_OpportunityDTO.CustomerLead.SexId,
                RefuseReciveSMS = Company_OpportunityDTO.CustomerLead.RefuseReciveSMS,
                RefuseReciveEmail = Company_OpportunityDTO.CustomerLead.RefuseReciveEmail,
                Description = Company_OpportunityDTO.CustomerLead.Description,
                AppUserId = Company_OpportunityDTO.CustomerLead.AppUserId,
                CreatorId = Company_OpportunityDTO.CustomerLead.CreatorId,
                ZipCode = Company_OpportunityDTO.CustomerLead.ZipCode,
                CurrencyId = Company_OpportunityDTO.CustomerLead.CurrencyId,
                RowId = Company_OpportunityDTO.CustomerLead.RowId,
            };
            Opportunity.LeadSource = Company_OpportunityDTO.LeadSource == null ? null : new CustomerLeadSource
            {
                Id = Company_OpportunityDTO.LeadSource.Id,
                Code = Company_OpportunityDTO.LeadSource.Code,
                Name = Company_OpportunityDTO.LeadSource.Name,
            };
            Opportunity.OpportunityResultType = Company_OpportunityDTO.OpportunityResultType == null ? null : new OpportunityResultType
            {
                Id = Company_OpportunityDTO.OpportunityResultType.Id,
                Code = Company_OpportunityDTO.OpportunityResultType.Code,
                Name = Company_OpportunityDTO.OpportunityResultType.Name,
            };
            Opportunity.PotentialResult = Company_OpportunityDTO.PotentialResult == null ? null : new PotentialResult
            {
                Id = Company_OpportunityDTO.PotentialResult.Id,
                Code = Company_OpportunityDTO.PotentialResult.Code,
                Name = Company_OpportunityDTO.PotentialResult.Name,
            };
            Opportunity.Probability = Company_OpportunityDTO.Probability == null ? null : new Probability
            {
                Id = Company_OpportunityDTO.Probability.Id,
                Code = Company_OpportunityDTO.Probability.Code,
                Name = Company_OpportunityDTO.Probability.Name,
            };
            Opportunity.SaleStage = Company_OpportunityDTO.SaleStage == null ? null : new SaleStage
            {
                Id = Company_OpportunityDTO.SaleStage.Id,
                Code = Company_OpportunityDTO.SaleStage.Code,
                Name = Company_OpportunityDTO.SaleStage.Name,
            };

            Opportunity.BaseLanguage = CurrentContext.Language;
            return Opportunity;
        }
        #endregion
    }
}

