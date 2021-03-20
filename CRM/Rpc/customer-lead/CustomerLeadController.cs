using CRM.Common;
using CRM.Entities;
using CRM.Models;
using CRM.Services.MActivityPriority;
using CRM.Services.MActivityStatus;
using CRM.Services.MActivityType;
using CRM.Services.MAppUser;
using CRM.Services.MCallLog;
using CRM.Services.MCompany;
using CRM.Services.MContact;
using CRM.Services.MCurrency;
using CRM.Services.MCustomerLead;
using CRM.Services.MCustomerLeadActivity;
using CRM.Services.MCustomerLeadEmail;
using CRM.Services.MCustomerLeadLevel;
using CRM.Services.MCustomerLeadSource;
using CRM.Services.MCustomerLeadStatus;
using CRM.Services.MDistrict;
using CRM.Services.MEmailStatus;
using CRM.Services.MFileType;
using CRM.Services.MMailTemplate;
using CRM.Services.MNation;
using CRM.Services.MOpportunity;
using CRM.Services.MOrganization;
using CRM.Services.MProbability;
using CRM.Services.MProduct;
using CRM.Services.MProductGrouping;
using CRM.Services.MProductType;
using CRM.Services.MProfession;
using CRM.Services.MProvince;
using CRM.Services.MSex;
using CRM.Services.MSmsTemplate;
using CRM.Services.MSupplier;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Rpc.customer_lead
{
    public partial class CustomerLeadController : RpcController
    {
        private DataContext DataContext;
        private IActivityStatusService ActivityStatusService;
        private IActivityTypeService ActivityTypeService;
        private IActivityPriorityService ActivityPriorityService;
        private ICustomerLeadLevelService CustomerLeadLevelService;
        private ICustomerLeadSourceService CustomerLeadSourceService;
        private ICustomerLeadStatusService CustomerLeadStatusService;
        private ICallLogService CallLogService;
        private IDistrictService DistrictService;
        private IFileTypeService FileTypeService;
        private ICustomerLeadEmailService CustomerLeadEmailService;
        private IMailTemplateService MailTemplateService;
        private IProfessionService ProfessionService;
        private IOrganizationService OrganizationService;
        private IProvinceService ProvinceService;
        private IAppUserService AppUserService;
        private ICustomerLeadService CustomerLeadService;
        private IProductService ProductService;
        private IProductGroupingService ProductGroupingService;
        private IProductTypeService ProductTypeService;
        private IProbabilityService ProbabilityService;
        private ISupplierService SupplierService;
        private ISexService SexService;
        private INationService NationService; 
        private IContactService ContactService; 
        private IOpportunityService OpportunityService; 
        private ICompanyService CompanyService; 
        private IItemService ItemService; 
        private ISmsTemplateService SmsTemplateService; 
        private ICurrencyService CurrencyService;
        private ICurrentContext CurrentContext;
        private IEmailStatusService EmailStatusService;
        private ICustomerLeadActivityService CustomerLeadActivityService;

        public CustomerLeadController(
            DataContext DataContext,
            IActivityStatusService ActivityStatusService,
            IActivityTypeService ActivityTypeService,
            IActivityPriorityService ActivityPriorityService,
            ICustomerLeadLevelService CustomerLeadLevelService,
            ICustomerLeadSourceService CustomerLeadSourceService,
            ICustomerLeadStatusService CustomerLeadStatusService,
            ICallLogService CallLogService,
            IDistrictService DistrictService,
            IFileTypeService FileTypeService,
            ICustomerLeadEmailService CustomerLeadEmailService,
            IMailTemplateService MailTemplateService,
            IProfessionService ProfessionService,
            IOrganizationService OrganizationService,
            IProvinceService ProvinceService,
            IAppUserService AppUserService,
            ICustomerLeadService CustomerLeadService,
            IProductService ProductService,
            IProductGroupingService ProductGroupingService,
            IProductTypeService ProductTypeService,
            IProbabilityService ProbabilityService,
            ISupplierService SupplierService,
            ISexService SexService,
            INationService NationService,
            IContactService ContactService,
            ICompanyService CompanyService,
            IOpportunityService OpportunityService,
            IItemService ItemService,
            ICurrencyService CurrencyService,
            ISmsTemplateService SmsTemplateService,
            ICurrentContext CurrentContext,
            IEmailStatusService EmailStatusService,
            ICustomerLeadActivityService CustomerLeadActivityService
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.DataContext = DataContext;
            this.ActivityStatusService = ActivityStatusService;
            this.ActivityTypeService = ActivityTypeService;
            this.ActivityPriorityService = ActivityPriorityService;
            this.CustomerLeadLevelService = CustomerLeadLevelService;
            this.CustomerLeadSourceService = CustomerLeadSourceService;
            this.CustomerLeadStatusService = CustomerLeadStatusService;
            this.CallLogService = CallLogService;
            this.DistrictService = DistrictService;
            this.FileTypeService = FileTypeService;
            this.CustomerLeadEmailService = CustomerLeadEmailService;
            this.MailTemplateService = MailTemplateService;
            this.ProfessionService = ProfessionService;
            this.OrganizationService = OrganizationService;
            this.ProvinceService = ProvinceService;
            this.AppUserService = AppUserService;
            this.CustomerLeadService = CustomerLeadService;
            this.ProductService = ProductService;
            this.ProductGroupingService = ProductGroupingService;
            this.ProductTypeService = ProductTypeService;
            this.ProbabilityService = ProbabilityService;
            this.SupplierService = SupplierService;
            this.SexService = SexService;
            this.NationService = NationService;
            this.ContactService = ContactService;
            this.CompanyService = CompanyService;
            this.OpportunityService = OpportunityService;
            this.ItemService = ItemService;
            this.CurrencyService = CurrencyService;
            this.SmsTemplateService = SmsTemplateService;
            this.CurrentContext = CurrentContext;
            this.EmailStatusService = EmailStatusService;
            this.CustomerLeadActivityService = CustomerLeadActivityService;
        }

        [Route(CustomerLeadRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] CustomerLead_CustomerLeadFilterDTO CustomerLead_CustomerLeadFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadFilter CustomerLeadFilter = ConvertFilterDTOToFilterEntity(CustomerLead_CustomerLeadFilterDTO);
            CustomerLeadFilter = await CustomerLeadService.ToFilter(CustomerLeadFilter);
            int count = await CustomerLeadService.Count(CustomerLeadFilter);
            return count;
        }

        [Route(CustomerLeadRoute.List), HttpPost]
        public async Task<ActionResult<List<CustomerLead_CustomerLeadDTO>>> List([FromBody] CustomerLead_CustomerLeadFilterDTO CustomerLead_CustomerLeadFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadFilter CustomerLeadFilter = ConvertFilterDTOToFilterEntity(CustomerLead_CustomerLeadFilterDTO);
            CustomerLeadFilter = await CustomerLeadService.ToFilter(CustomerLeadFilter);
            List<CustomerLead> CustomerLeads = await CustomerLeadService.List(CustomerLeadFilter);
            List<CustomerLead_CustomerLeadDTO> CustomerLead_CustomerLeadDTOs = CustomerLeads
                .Select(c => new CustomerLead_CustomerLeadDTO(c)).ToList();
            return CustomerLead_CustomerLeadDTOs;
        }

        [Route(CustomerLeadRoute.Get), HttpPost]
        public async Task<ActionResult<CustomerLead_CustomerLeadDTO>> Get([FromBody] CustomerLead_CustomerLeadDTO CustomerLead_CustomerLeadDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CustomerLead_CustomerLeadDTO.Id))
                return Forbid();

            CustomerLead CustomerLead = await CustomerLeadService.Get(CustomerLead_CustomerLeadDTO.Id);
            return new CustomerLead_CustomerLeadDTO(CustomerLead);
        }

        [Route(CustomerLeadRoute.Create), HttpPost]
        public async Task<ActionResult<CustomerLead_CustomerLeadDTO>> Create([FromBody] CustomerLead_CustomerLeadDTO CustomerLead_CustomerLeadDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CustomerLead_CustomerLeadDTO.Id))
                return Forbid();
            
            CustomerLead CustomerLead = ConvertDTOToEntity(CustomerLead_CustomerLeadDTO);
            CustomerLead = await CustomerLeadService.Create(CustomerLead);
            CustomerLead_CustomerLeadDTO = new CustomerLead_CustomerLeadDTO(CustomerLead);
            if (CustomerLead.IsValidated)
                return CustomerLead_CustomerLeadDTO;
            else
                return BadRequest(CustomerLead_CustomerLeadDTO);
        }

        [Route(CustomerLeadRoute.Convert), HttpPost]
        public async Task<ActionResult<CustomerLead_CustomerLeadDTO>> Convert([FromBody] CustomerLead_CustomerLeadDTO CustomerLead_CustomerLeadDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CustomerLead_CustomerLeadDTO.Id))
                return Forbid();

            CustomerLead CustomerLead = ConvertDTOToEntity(CustomerLead_CustomerLeadDTO);
            CustomerLead = await CustomerLeadService.Convert(CustomerLead);
            CustomerLead_CustomerLeadDTO = new CustomerLead_CustomerLeadDTO(CustomerLead);
            if (CustomerLead.IsValidated)
                return CustomerLead_CustomerLeadDTO;
            else
                return BadRequest(CustomerLead_CustomerLeadDTO);
        }

        [Route(CustomerLeadRoute.Update), HttpPost]
        public async Task<ActionResult<CustomerLead_CustomerLeadDTO>> Update([FromBody] CustomerLead_CustomerLeadDTO CustomerLead_CustomerLeadDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CustomerLead_CustomerLeadDTO.Id))
                return Forbid();

            CustomerLead CustomerLead = ConvertDTOToEntity(CustomerLead_CustomerLeadDTO);
            CustomerLead = await CustomerLeadService.Update(CustomerLead);
            CustomerLead_CustomerLeadDTO = new CustomerLead_CustomerLeadDTO(CustomerLead);
            if (CustomerLead.IsValidated)
                return CustomerLead_CustomerLeadDTO;
            else
                return BadRequest(CustomerLead_CustomerLeadDTO);
        }

        [Route(CustomerLeadRoute.Delete), HttpPost]
        public async Task<ActionResult<CustomerLead_CustomerLeadDTO>> Delete([FromBody] CustomerLead_CustomerLeadDTO CustomerLead_CustomerLeadDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CustomerLead_CustomerLeadDTO.Id))
                return Forbid();

            CustomerLead CustomerLead = ConvertDTOToEntity(CustomerLead_CustomerLeadDTO);
            CustomerLead = await CustomerLeadService.Delete(CustomerLead);
            CustomerLead_CustomerLeadDTO = new CustomerLead_CustomerLeadDTO(CustomerLead);
            if (CustomerLead.IsValidated)
                return CustomerLead_CustomerLeadDTO;
            else
                return BadRequest(CustomerLead_CustomerLeadDTO);
        }

        [Route(CustomerLeadRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadFilter CustomerLeadFilter = new CustomerLeadFilter();
            CustomerLeadFilter = await CustomerLeadService.ToFilter(CustomerLeadFilter);
            CustomerLeadFilter.Id = new IdFilter { In = Ids };
            CustomerLeadFilter.Selects = CustomerLeadSelect.Id;
            CustomerLeadFilter.Skip = 0;
            CustomerLeadFilter.Take = int.MaxValue;

            List<CustomerLead> CustomerLeads = await CustomerLeadService.List(CustomerLeadFilter);
            CustomerLeads = await CustomerLeadService.BulkDelete(CustomerLeads);
            if (CustomerLeads.Any(x => !x.IsValidated))
                return BadRequest(CustomerLeads.Where(x => !x.IsValidated));
            return true;
        }

        [Route(CustomerLeadRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            CustomerLeadLevelFilter CustomerLeadLevelFilter = new CustomerLeadLevelFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CustomerLeadLevelSelect.ALL
            };
            List<CustomerLeadLevel> CustomerLeadLevels = await CustomerLeadLevelService.List(CustomerLeadLevelFilter);
            CustomerLeadSourceFilter CustomerLeadSourceFilter = new CustomerLeadSourceFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CustomerLeadSourceSelect.ALL
            };
            List<CustomerLeadSource> CustomerLeadSources = await CustomerLeadSourceService.List(CustomerLeadSourceFilter);
            CustomerLeadStatusFilter CustomerLeadStatusFilter = new CustomerLeadStatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CustomerLeadStatusSelect.ALL
            };
            List<CustomerLeadStatus> CustomerLeadStatuses = await CustomerLeadStatusService.List(CustomerLeadStatusFilter);
            DistrictFilter DistrictFilter = new DistrictFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = DistrictSelect.ALL
            };
            List<District> Districts = await DistrictService.List(DistrictFilter);
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
            AppUserFilter UserFilter = new AppUserFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = AppUserSelect.ALL
            };
            List<AppUser> Users = await AppUserService.List(UserFilter);
            List<CustomerLead> CustomerLeads = new List<CustomerLead>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(CustomerLeads);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int NameColumn = 1 + StartColumn;
                int TelePhoneColumn = 2 + StartColumn;
                int PhoneColumn = 3 + StartColumn;
                int FaxColumn = 4 + StartColumn;
                int EmailColumn = 5 + StartColumn;
                int SecondEmailColumn = 6 + StartColumn;
                int CompanyColumn = 7 + StartColumn;
                int WebsiteColumn = 8 + StartColumn;
                int CustomerLeadSourceIdColumn = 9 + StartColumn;
                int CustomerLeadLevelIdColumn = 10 + StartColumn;
                int CampaignIdColumn = 11 + StartColumn;
                int ProfessionIdColumn = 12 + StartColumn;
                int RevenueColumn = 13 + StartColumn;
                int EmployeeQuantityColumn = 14 + StartColumn;
                int AddressColumn = 15 + StartColumn;
                int ProvinceIdColumn = 16 + StartColumn;
                int DistrictIdColumn = 17 + StartColumn;
                int UserIdColumn = 18 + StartColumn;
                int CustomerLeadStatusIdColumn = 19 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string TelePhoneValue = worksheet.Cells[i + StartRow, TelePhoneColumn].Value?.ToString();
                    string PhoneValue = worksheet.Cells[i + StartRow, PhoneColumn].Value?.ToString();
                    string FaxValue = worksheet.Cells[i + StartRow, FaxColumn].Value?.ToString();
                    string EmailValue = worksheet.Cells[i + StartRow, EmailColumn].Value?.ToString();
                    string SecondEmailValue = worksheet.Cells[i + StartRow, SecondEmailColumn].Value?.ToString();
                    string CompanyValue = worksheet.Cells[i + StartRow, CompanyColumn].Value?.ToString();
                    string WebsiteValue = worksheet.Cells[i + StartRow, WebsiteColumn].Value?.ToString();
                    string CustomerLeadSourceIdValue = worksheet.Cells[i + StartRow, CustomerLeadSourceIdColumn].Value?.ToString();
                    string CustomerLeadLevelIdValue = worksheet.Cells[i + StartRow, CustomerLeadLevelIdColumn].Value?.ToString();
                    string CampaignIdValue = worksheet.Cells[i + StartRow, CampaignIdColumn].Value?.ToString();
                    string ProfessionIdValue = worksheet.Cells[i + StartRow, ProfessionIdColumn].Value?.ToString();
                    string RevenueValue = worksheet.Cells[i + StartRow, RevenueColumn].Value?.ToString();
                    string EmployeeQuantityValue = worksheet.Cells[i + StartRow, EmployeeQuantityColumn].Value?.ToString();
                    string AddressValue = worksheet.Cells[i + StartRow, AddressColumn].Value?.ToString();
                    string ProvinceIdValue = worksheet.Cells[i + StartRow, ProvinceIdColumn].Value?.ToString();
                    string DistrictIdValue = worksheet.Cells[i + StartRow, DistrictIdColumn].Value?.ToString();
                    string UserIdValue = worksheet.Cells[i + StartRow, UserIdColumn].Value?.ToString();
                    string CustomerLeadStatusIdValue = worksheet.Cells[i + StartRow, CustomerLeadStatusIdColumn].Value?.ToString();

                    CustomerLead CustomerLead = new CustomerLead();
                    CustomerLead.Name = NameValue;
                    CustomerLead.TelePhone = TelePhoneValue;
                    CustomerLead.Phone = PhoneValue;
                    CustomerLead.Fax = FaxValue;
                    CustomerLead.Email = EmailValue;
                    CustomerLead.SecondEmail = SecondEmailValue;
                    CustomerLead.Website = WebsiteValue;
                    CustomerLead.Revenue = decimal.TryParse(RevenueValue, out decimal Revenue) ? Revenue : 0;
                    CustomerLead.EmployeeQuantity = long.TryParse(EmployeeQuantityValue, out long EmployeeQuantity) ? EmployeeQuantity : 0;
                    CustomerLead.Address = AddressValue;
                    CustomerLeadLevel CustomerLeadLevel = CustomerLeadLevels.Where(x => x.Id.ToString() == CustomerLeadLevelIdValue).FirstOrDefault();
                    CustomerLead.CustomerLeadLevelId = CustomerLeadLevel == null ? 0 : CustomerLeadLevel.Id;
                    CustomerLead.CustomerLeadLevel = CustomerLeadLevel;
                    CustomerLeadSource CustomerLeadSource = CustomerLeadSources.Where(x => x.Id.ToString() == CustomerLeadSourceIdValue).FirstOrDefault();
                    CustomerLead.CustomerLeadSourceId = CustomerLeadSource == null ? 0 : CustomerLeadSource.Id;
                    CustomerLead.CustomerLeadSource = CustomerLeadSource;
                    CustomerLeadStatus CustomerLeadStatus = CustomerLeadStatuses.Where(x => x.Id.ToString() == CustomerLeadStatusIdValue).FirstOrDefault();
                    CustomerLead.CustomerLeadStatusId = CustomerLeadStatus == null ? 0 : CustomerLeadStatus.Id;
                    CustomerLead.CustomerLeadStatus = CustomerLeadStatus;
                    //Company Company = Companys.Where(x => x.Id.ToString() == CompanyIdValue).FirstOrDefault();
                    //CustomerLead.CompanyId = Company == null ? 0 : Company.Id;
                    //CustomerLead.Company = Company;
                    District District = Districts.Where(x => x.Id.ToString() == DistrictIdValue).FirstOrDefault();
                    CustomerLead.DistrictId = District == null ? 0 : District.Id;
                    CustomerLead.District = District;
                    Profession Profession = Professions.Where(x => x.Id.ToString() == ProfessionIdValue).FirstOrDefault();
                    CustomerLead.ProfessionId = Profession == null ? 0 : Profession.Id;
                    CustomerLead.Profession = Profession;
                    Province Province = Provinces.Where(x => x.Id.ToString() == ProvinceIdValue).FirstOrDefault();
                    CustomerLead.ProvinceId = Province == null ? 0 : Province.Id;
                    CustomerLead.Province = Province;
                    AppUser User = Users.Where(x => x.Id.ToString() == UserIdValue).FirstOrDefault();
                    CustomerLead.AppUserId = User == null ? 0 : User.Id;
                    CustomerLead.AppUser = User;

                    CustomerLeads.Add(CustomerLead);
                }
            }
            CustomerLeads = await CustomerLeadService.Import(CustomerLeads);
            if (CustomerLeads.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < CustomerLeads.Count; i++)
                {
                    CustomerLead CustomerLead = CustomerLeads[i];
                    if (!CustomerLead.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.Id)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.Id)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.Name)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.Name)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.TelePhone)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.TelePhone)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.Phone)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.Phone)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.Fax)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.Fax)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.Email)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.Email)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.SecondEmail)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.SecondEmail)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.Company)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.Company)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.Website)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.Website)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.CustomerLeadSourceId)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.CustomerLeadSourceId)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.CustomerLeadLevelId)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.CustomerLeadLevelId)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.CampaignId)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.CampaignId)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.ProfessionId)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.ProfessionId)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.Revenue)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.Revenue)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.EmployeeQuantity)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.EmployeeQuantity)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.Address)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.Address)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.ProvinceId)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.ProvinceId)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.DistrictId)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.DistrictId)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.AppUserId)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.AppUserId)];
                        if (CustomerLead.Errors.ContainsKey(nameof(CustomerLead.CustomerLeadStatusId)))
                            Error += CustomerLead.Errors[nameof(CustomerLead.CustomerLeadStatusId)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }

        [Route(CustomerLeadRoute.Export), HttpPost]
        public async Task<FileResult> Export([FromBody] CustomerLead_CustomerLeadFilterDTO CustomerLead_CustomerLeadFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region CustomerLead
                var CustomerLeadFilter = ConvertFilterDTOToFilterEntity(CustomerLead_CustomerLeadFilterDTO);
                CustomerLeadFilter.Skip = 0;
                CustomerLeadFilter.Take = int.MaxValue;
                CustomerLeadFilter = await CustomerLeadService.ToFilter(CustomerLeadFilter);
                List<CustomerLead> CustomerLeads = await CustomerLeadService.List(CustomerLeadFilter);

                var CustomerLeadHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Name",
                        "TelePhone",
                        "Phone",
                        "Fax",
                        "Email",
                        "SecondEmail",
                        "Company",
                        "Website",
                        "CustomerLeadSourceId",
                        "CustomerLeadLevelId",
                        "CampaignId",
                        "ProfessionId",
                        "Revenue",
                        "EmployeeQuantity",
                        "Address",
                        "ProvinceId",
                        "DistrictId",
                        "UserId",
                        "CustomerLeadStatusId",
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
                        CustomerLead.TelePhone,
                        CustomerLead.Phone,
                        CustomerLead.Fax,
                        CustomerLead.Email,
                        CustomerLead.SecondEmail,
                        CustomerLead.Company,
                        CustomerLead.Website,
                        CustomerLead.CustomerLeadSourceId,
                        CustomerLead.CustomerLeadLevelId,
                        CustomerLead.CampaignId,
                        CustomerLead.ProfessionId,
                        CustomerLead.Revenue,
                        CustomerLead.EmployeeQuantity,
                        CustomerLead.Address,
                        CustomerLead.ProvinceId,
                        CustomerLead.DistrictId,
                        CustomerLead.AppUserId,
                        CustomerLead.CustomerLeadStatusId,
                    });
                }
                excel.GenerateWorksheet("CustomerLead", CustomerLeadHeaders, CustomerLeadData);
                #endregion

                #region CustomerLeadLevel
                var CustomerLeadLevelFilter = new CustomerLeadLevelFilter();
                CustomerLeadLevelFilter.Selects = CustomerLeadLevelSelect.ALL;
                CustomerLeadLevelFilter.OrderBy = CustomerLeadLevelOrder.Id;
                CustomerLeadLevelFilter.OrderType = OrderType.ASC;
                CustomerLeadLevelFilter.Skip = 0;
                CustomerLeadLevelFilter.Take = int.MaxValue;
                List<CustomerLeadLevel> CustomerLeadLevels = await CustomerLeadLevelService.List(CustomerLeadLevelFilter);

                var CustomerLeadLevelHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> CustomerLeadLevelData = new List<object[]>();
                for (int i = 0; i < CustomerLeadLevels.Count; i++)
                {
                    var CustomerLeadLevel = CustomerLeadLevels[i];
                    CustomerLeadLevelData.Add(new Object[]
                    {
                        CustomerLeadLevel.Id,
                        CustomerLeadLevel.Code,
                        CustomerLeadLevel.Name,
                    });
                }
                excel.GenerateWorksheet("CustomerLeadLevel", CustomerLeadLevelHeaders, CustomerLeadLevelData);
                #endregion
                #region CustomerLeadSource
                var CustomerLeadSourceFilter = new CustomerLeadSourceFilter();
                CustomerLeadSourceFilter.Selects = CustomerLeadSourceSelect.ALL;
                CustomerLeadSourceFilter.OrderBy = CustomerLeadSourceOrder.Id;
                CustomerLeadSourceFilter.OrderType = OrderType.ASC;
                CustomerLeadSourceFilter.Skip = 0;
                CustomerLeadSourceFilter.Take = int.MaxValue;
                List<CustomerLeadSource> CustomerLeadSources = await CustomerLeadSourceService.List(CustomerLeadSourceFilter);

                var CustomerLeadSourceHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> CustomerLeadSourceData = new List<object[]>();
                for (int i = 0; i < CustomerLeadSources.Count; i++)
                {
                    var CustomerLeadSource = CustomerLeadSources[i];
                    CustomerLeadSourceData.Add(new Object[]
                    {
                        CustomerLeadSource.Id,
                        CustomerLeadSource.Code,
                        CustomerLeadSource.Name,
                    });
                }
                excel.GenerateWorksheet("CustomerLeadSource", CustomerLeadSourceHeaders, CustomerLeadSourceData);
                #endregion
                #region CustomerLeadStatus
                var CustomerLeadStatusFilter = new CustomerLeadStatusFilter();
                CustomerLeadStatusFilter.Selects = CustomerLeadStatusSelect.ALL;
                CustomerLeadStatusFilter.OrderBy = CustomerLeadStatusOrder.Id;
                CustomerLeadStatusFilter.OrderType = OrderType.ASC;
                CustomerLeadStatusFilter.Skip = 0;
                CustomerLeadStatusFilter.Take = int.MaxValue;
                List<CustomerLeadStatus> CustomerLeadStatuses = await CustomerLeadStatusService.List(CustomerLeadStatusFilter);

                var CustomerLeadStatusHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> CustomerLeadStatusData = new List<object[]>();
                for (int i = 0; i < CustomerLeadStatuses.Count; i++)
                {
                    var CustomerLeadStatus = CustomerLeadStatuses[i];
                    CustomerLeadStatusData.Add(new Object[]
                    {
                        CustomerLeadStatus.Id,
                        CustomerLeadStatus.Code,
                        CustomerLeadStatus.Name,
                    });
                }
                excel.GenerateWorksheet("CustomerLeadStatus", CustomerLeadStatusHeaders, CustomerLeadStatusData);
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
                    });
                }
                excel.GenerateWorksheet("District", DistrictHeaders, DistrictData);
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
                    });
                }
                excel.GenerateWorksheet("Province", ProvinceHeaders, ProvinceData);
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
                        "PositionId",
                        "Department",
                        "OrganizationId",
                        "ProvinceId",
                        "Longitude",
                        "Latitude",
                        "StatusId",
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
                        AppUser.PositionId,
                        AppUser.Department,
                        AppUser.OrganizationId,
                        AppUser.ProvinceId,
                        AppUser.Longitude,
                        AppUser.Latitude,
                        AppUser.StatusId,
                    });
                }
                excel.GenerateWorksheet("AppUser", AppUserHeaders, AppUserData);
                #endregion
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "CustomerLead.xlsx");
        }

        [Route(CustomerLeadRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] CustomerLead_CustomerLeadFilterDTO CustomerLead_CustomerLeadFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region CustomerLead
                var CustomerLeadHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Name",
                        "TelePhone",
                        "Phone",
                        "Fax",
                        "Email",
                        "SecondEmail",
                        "Company",
                        "Website",
                        "CustomerLeadSourceId",
                        "CustomerLeadLevelId",
                        "CampaignId",
                        "ProfessionId",
                        "Revenue",
                        "EmployeeQuantity",
                        "Address",
                        "ProvinceId",
                        "DistrictId",
                        "UserId",
                        "CustomerLeadStatusId",
                    }
                };
                List<object[]> CustomerLeadData = new List<object[]>();
                excel.GenerateWorksheet("CustomerLead", CustomerLeadHeaders, CustomerLeadData);
                #endregion

                #region CustomerLeadLevel
                var CustomerLeadLevelFilter = new CustomerLeadLevelFilter();
                CustomerLeadLevelFilter.Selects = CustomerLeadLevelSelect.ALL;
                CustomerLeadLevelFilter.OrderBy = CustomerLeadLevelOrder.Id;
                CustomerLeadLevelFilter.OrderType = OrderType.ASC;
                CustomerLeadLevelFilter.Skip = 0;
                CustomerLeadLevelFilter.Take = int.MaxValue;
                List<CustomerLeadLevel> CustomerLeadLevels = await CustomerLeadLevelService.List(CustomerLeadLevelFilter);

                var CustomerLeadLevelHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> CustomerLeadLevelData = new List<object[]>();
                for (int i = 0; i < CustomerLeadLevels.Count; i++)
                {
                    var CustomerLeadLevel = CustomerLeadLevels[i];
                    CustomerLeadLevelData.Add(new Object[]
                    {
                        CustomerLeadLevel.Id,
                        CustomerLeadLevel.Code,
                        CustomerLeadLevel.Name,
                    });
                }
                excel.GenerateWorksheet("CustomerLeadLevel", CustomerLeadLevelHeaders, CustomerLeadLevelData);
                #endregion
                #region CustomerLeadSource
                var CustomerLeadSourceFilter = new CustomerLeadSourceFilter();
                CustomerLeadSourceFilter.Selects = CustomerLeadSourceSelect.ALL;
                CustomerLeadSourceFilter.OrderBy = CustomerLeadSourceOrder.Id;
                CustomerLeadSourceFilter.OrderType = OrderType.ASC;
                CustomerLeadSourceFilter.Skip = 0;
                CustomerLeadSourceFilter.Take = int.MaxValue;
                List<CustomerLeadSource> CustomerLeadSources = await CustomerLeadSourceService.List(CustomerLeadSourceFilter);

                var CustomerLeadSourceHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> CustomerLeadSourceData = new List<object[]>();
                for (int i = 0; i < CustomerLeadSources.Count; i++)
                {
                    var CustomerLeadSource = CustomerLeadSources[i];
                    CustomerLeadSourceData.Add(new Object[]
                    {
                        CustomerLeadSource.Id,
                        CustomerLeadSource.Code,
                        CustomerLeadSource.Name,
                    });
                }
                excel.GenerateWorksheet("CustomerLeadSource", CustomerLeadSourceHeaders, CustomerLeadSourceData);
                #endregion
                #region CustomerLeadStatus
                var CustomerLeadStatusFilter = new CustomerLeadStatusFilter();
                CustomerLeadStatusFilter.Selects = CustomerLeadStatusSelect.ALL;
                CustomerLeadStatusFilter.OrderBy = CustomerLeadStatusOrder.Id;
                CustomerLeadStatusFilter.OrderType = OrderType.ASC;
                CustomerLeadStatusFilter.Skip = 0;
                CustomerLeadStatusFilter.Take = int.MaxValue;
                List<CustomerLeadStatus> CustomerLeadStatuses = await CustomerLeadStatusService.List(CustomerLeadStatusFilter);

                var CustomerLeadStatusHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> CustomerLeadStatusData = new List<object[]>();
                for (int i = 0; i < CustomerLeadStatuses.Count; i++)
                {
                    var CustomerLeadStatus = CustomerLeadStatuses[i];
                    CustomerLeadStatusData.Add(new Object[]
                    {
                        CustomerLeadStatus.Id,
                        CustomerLeadStatus.Code,
                        CustomerLeadStatus.Name,
                    });
                }
                excel.GenerateWorksheet("CustomerLeadStatus", CustomerLeadStatusHeaders, CustomerLeadStatusData);
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
                    });
                }
                excel.GenerateWorksheet("District", DistrictHeaders, DistrictData);
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
                    });
                }
                excel.GenerateWorksheet("Province", ProvinceHeaders, ProvinceData);
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
                        "PositionId",
                        "Department",
                        "OrganizationId",
                        "ProvinceId",
                        "Longitude",
                        "Latitude",
                        "StatusId",
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
                        AppUser.PositionId,
                        AppUser.Department,
                        AppUser.OrganizationId,
                        AppUser.ProvinceId,
                        AppUser.Longitude,
                        AppUser.Latitude,
                        AppUser.StatusId,
                    });
                }
                excel.GenerateWorksheet("AppUser", AppUserHeaders, AppUserData);
                #endregion
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "CustomerLead.xlsx");
        }

        [HttpPost]
        [Route(CustomerLeadRoute.UploadFile)]
        public async Task<ActionResult<CustomerLead_FileDTO>> UploadFile(IFormFile file)
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
            File = await CustomerLeadService.UploadFile(File);
            if (File == null)
                return BadRequest();
            CustomerLead_FileDTO CustomerLead_FileDTO = new CustomerLead_FileDTO
            {
                Id = File.Id,
                Name = File.Name,
                Url = File.Url,
                AppUserId = File.AppUserId
            };
            return Ok(CustomerLead_FileDTO);
        }

        private async Task<bool> HasPermission(long Id)
        {
            CustomerLeadFilter CustomerLeadFilter = new CustomerLeadFilter();
            CustomerLeadFilter = await CustomerLeadService.ToFilter(CustomerLeadFilter);
            if (Id == 0)
            {

            }
            else
            {
                CustomerLeadFilter.Id = new IdFilter { Equal = Id };
                int count = await CustomerLeadService.Count(CustomerLeadFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private CustomerLead ConvertDTOToEntity(CustomerLead_CustomerLeadDTO CustomerLead_CustomerLeadDTO)
        {
            CustomerLead CustomerLead = new CustomerLead();
            CustomerLead.Id = CustomerLead_CustomerLeadDTO.Id;
            CustomerLead.CompanyName = CustomerLead_CustomerLeadDTO.CompanyName;
            CustomerLead.Name = CustomerLead_CustomerLeadDTO.Name;
            CustomerLead.TelePhone = CustomerLead_CustomerLeadDTO.TelePhone;
            CustomerLead.Phone = CustomerLead_CustomerLeadDTO.Phone;
            CustomerLead.Fax = CustomerLead_CustomerLeadDTO.Fax;
            CustomerLead.Email = CustomerLead_CustomerLeadDTO.Email;
            CustomerLead.SecondEmail = CustomerLead_CustomerLeadDTO.SecondEmail;
            CustomerLead.CompanyId = CustomerLead_CustomerLeadDTO.CompanyId;
            CustomerLead.Website = CustomerLead_CustomerLeadDTO.Website;
            CustomerLead.CustomerLeadSourceId = CustomerLead_CustomerLeadDTO.CustomerLeadSourceId;
            CustomerLead.CustomerLeadLevelId = CustomerLead_CustomerLeadDTO.CustomerLeadLevelId;
            CustomerLead.CampaignId = CustomerLead_CustomerLeadDTO.CampaignId;
            CustomerLead.ProfessionId = CustomerLead_CustomerLeadDTO.ProfessionId;
            CustomerLead.Revenue = CustomerLead_CustomerLeadDTO.Revenue;
            CustomerLead.EmployeeQuantity = CustomerLead_CustomerLeadDTO.EmployeeQuantity;
            CustomerLead.Address = CustomerLead_CustomerLeadDTO.Address;
            CustomerLead.ProvinceId = CustomerLead_CustomerLeadDTO.ProvinceId;
            CustomerLead.DistrictId = CustomerLead_CustomerLeadDTO.DistrictId;
            CustomerLead.AppUserId = CustomerLead_CustomerLeadDTO.AppUserId;
            CustomerLead.CustomerLeadStatusId = CustomerLead_CustomerLeadDTO.CustomerLeadStatusId;
            CustomerLead.BusinessRegistrationCode = CustomerLead_CustomerLeadDTO.BusinessRegistrationCode;
            CustomerLead.SexId = CustomerLead_CustomerLeadDTO.SexId;
            CustomerLead.RefuseReciveSMS = CustomerLead_CustomerLeadDTO.RefuseReciveSMS;
            CustomerLead.NationId = CustomerLead_CustomerLeadDTO.NationId;
            CustomerLead.RefuseReciveEmail = CustomerLead_CustomerLeadDTO.RefuseReciveEmail;
            CustomerLead.Description = CustomerLead_CustomerLeadDTO.Description;
            CustomerLead.CreatorId = CustomerLead_CustomerLeadDTO.CreatorId;
            CustomerLead.ZipCode = CustomerLead_CustomerLeadDTO.ZipCode;
            CustomerLead.CurrencyId = CustomerLead_CustomerLeadDTO.CurrencyId;
            CustomerLead.IsNewCompany = CustomerLead_CustomerLeadDTO.IsNewCompany;
            CustomerLead.IsNewContact = CustomerLead_CustomerLeadDTO.IsNewContact;
            CustomerLead.IsCreateOpportunity = CustomerLead_CustomerLeadDTO.IsCreateOpportunity;
            CustomerLead.IsNewOpportunity = CustomerLead_CustomerLeadDTO.IsNewOpportunity;
            CustomerLead.CustomerLeadLevel = CustomerLead_CustomerLeadDTO.CustomerLeadLevel == null ? null : new CustomerLeadLevel
            {
                Id = CustomerLead_CustomerLeadDTO.CustomerLeadLevel.Id,
                Code = CustomerLead_CustomerLeadDTO.CustomerLeadLevel.Code,
                Name = CustomerLead_CustomerLeadDTO.CustomerLeadLevel.Name,
            };
            CustomerLead.CustomerLeadSource = CustomerLead_CustomerLeadDTO.CustomerLeadSource == null ? null : new CustomerLeadSource
            {
                Id = CustomerLead_CustomerLeadDTO.CustomerLeadSource.Id,
                Code = CustomerLead_CustomerLeadDTO.CustomerLeadSource.Code,
                Name = CustomerLead_CustomerLeadDTO.CustomerLeadSource.Name,
            };
            CustomerLead.CustomerLeadStatus = CustomerLead_CustomerLeadDTO.CustomerLeadStatus == null ? null : new CustomerLeadStatus
            {
                Id = CustomerLead_CustomerLeadDTO.CustomerLeadStatus.Id,
                Code = CustomerLead_CustomerLeadDTO.CustomerLeadStatus.Code,
                Name = CustomerLead_CustomerLeadDTO.CustomerLeadStatus.Name,
            };
            CustomerLead.District = CustomerLead_CustomerLeadDTO.District == null ? null : new District
            {
                Id = CustomerLead_CustomerLeadDTO.District.Id,
                Code = CustomerLead_CustomerLeadDTO.District.Code,
                Name = CustomerLead_CustomerLeadDTO.District.Name,
                Priority = CustomerLead_CustomerLeadDTO.District.Priority,
                ProvinceId = CustomerLead_CustomerLeadDTO.District.ProvinceId,
                StatusId = CustomerLead_CustomerLeadDTO.District.StatusId,
            };
            CustomerLead.Profession = CustomerLead_CustomerLeadDTO.Profession == null ? null : new Profession
            {
                Id = CustomerLead_CustomerLeadDTO.Profession.Id,
                Code = CustomerLead_CustomerLeadDTO.Profession.Code,
                Name = CustomerLead_CustomerLeadDTO.Profession.Name,
            };
            CustomerLead.Currency = CustomerLead_CustomerLeadDTO.Currency == null ? null : new Currency
            {
                Id = CustomerLead_CustomerLeadDTO.Currency.Id,
                Code = CustomerLead_CustomerLeadDTO.Currency.Code,
                Name = CustomerLead_CustomerLeadDTO.Currency.Name,
            };
            CustomerLead.Province = CustomerLead_CustomerLeadDTO.Province == null ? null : new Province
            {
                Id = CustomerLead_CustomerLeadDTO.Province.Id,
                Code = CustomerLead_CustomerLeadDTO.Province.Code,
                Name = CustomerLead_CustomerLeadDTO.Province.Name,
                Priority = CustomerLead_CustomerLeadDTO.Province.Priority,
                StatusId = CustomerLead_CustomerLeadDTO.Province.StatusId,
            };
            CustomerLead.AppUser = CustomerLead_CustomerLeadDTO.AppUser == null ? null : new AppUser
            {
                Id = CustomerLead_CustomerLeadDTO.AppUser.Id,
                Username = CustomerLead_CustomerLeadDTO.AppUser.Username,
                DisplayName = CustomerLead_CustomerLeadDTO.AppUser.DisplayName,
                Address = CustomerLead_CustomerLeadDTO.AppUser.Address,
                Email = CustomerLead_CustomerLeadDTO.AppUser.Email,
                Phone = CustomerLead_CustomerLeadDTO.AppUser.Phone,
                SexId = CustomerLead_CustomerLeadDTO.AppUser.SexId,
                Birthday = CustomerLead_CustomerLeadDTO.AppUser.Birthday,
                Avatar = CustomerLead_CustomerLeadDTO.AppUser.Avatar,
                PositionId = CustomerLead_CustomerLeadDTO.AppUser.PositionId,
                Department = CustomerLead_CustomerLeadDTO.AppUser.Department,
                OrganizationId = CustomerLead_CustomerLeadDTO.AppUser.OrganizationId,
                ProvinceId = CustomerLead_CustomerLeadDTO.AppUser.ProvinceId,
                Longitude = CustomerLead_CustomerLeadDTO.AppUser.Longitude,
                Latitude = CustomerLead_CustomerLeadDTO.AppUser.Latitude,
                StatusId = CustomerLead_CustomerLeadDTO.AppUser.StatusId,
            };
            CustomerLead.Sex = CustomerLead_CustomerLeadDTO.Sex == null ? null : new Sex
            {
                Id = CustomerLead_CustomerLeadDTO.Sex.Id,
                Code = CustomerLead_CustomerLeadDTO.Sex.Code,
                Name = CustomerLead_CustomerLeadDTO.Sex.Name,
            };
            CustomerLead.Nation = CustomerLead_CustomerLeadDTO.Nation == null ? null : new Nation
            {
                Id = CustomerLead_CustomerLeadDTO.Nation.Id,
                Code = CustomerLead_CustomerLeadDTO.Nation.Code,
                Name = CustomerLead_CustomerLeadDTO.Nation.Name,
                Priority = CustomerLead_CustomerLeadDTO.Nation.DisplayOrder,
                StatusId = CustomerLead_CustomerLeadDTO.Nation.StatusId,
            };
            CustomerLead.Company = CustomerLead_CustomerLeadDTO.Company == null ? null : new Company
            {
                Id = CustomerLead_CustomerLeadDTO.Company.Id,
                Name = CustomerLead_CustomerLeadDTO.Company.Name,
                ProfessionId = CustomerLead_CustomerLeadDTO.Company.ProfessionId,
            };
            CustomerLead.Contact = CustomerLead_CustomerLeadDTO.Contact == null ? null : new Contact
            {
                Id = CustomerLead_CustomerLeadDTO.Contact.Id,
                Name = CustomerLead_CustomerLeadDTO.Contact.Name,
                Phone = CustomerLead_CustomerLeadDTO.Contact.Phone,
            };
            CustomerLead.Opportunity = CustomerLead_CustomerLeadDTO.Opportunity == null ? null : new Opportunity
            {
                Id = CustomerLead_CustomerLeadDTO.Opportunity.Id,
                Name = CustomerLead_CustomerLeadDTO.Opportunity.Name,
                Amount = CustomerLead_CustomerLeadDTO.Opportunity.Amount,
                Probability = CustomerLead_CustomerLeadDTO.Opportunity.Probability == null ? null : new Probability
                {
                    Id = CustomerLead_CustomerLeadDTO.Opportunity.Probability.Id,
                    Code = CustomerLead_CustomerLeadDTO.Opportunity.Probability.Code,
                    Name = CustomerLead_CustomerLeadDTO.Opportunity.Probability.Name,
                },
                SaleStage = CustomerLead_CustomerLeadDTO.Opportunity.SaleStage == null ? null : new SaleStage
                {
                    Id = CustomerLead_CustomerLeadDTO.Opportunity.SaleStage.Id,
                    Code = CustomerLead_CustomerLeadDTO.Opportunity.SaleStage.Code,
                    Name = CustomerLead_CustomerLeadDTO.Opportunity.SaleStage.Name,
                },
                ClosingDate = CustomerLead_CustomerLeadDTO.Opportunity.ClosingDate
            };
            CustomerLead.Creator = CustomerLead_CustomerLeadDTO.Creator == null ? null : new AppUser
            {
                Id = CustomerLead_CustomerLeadDTO.Creator.Id,
                Username = CustomerLead_CustomerLeadDTO.Creator.Username,
                DisplayName = CustomerLead_CustomerLeadDTO.Creator.DisplayName,
                Address = CustomerLead_CustomerLeadDTO.Creator.Address,
                Email = CustomerLead_CustomerLeadDTO.Creator.Email,
                Phone = CustomerLead_CustomerLeadDTO.Creator.Phone,
                SexId = CustomerLead_CustomerLeadDTO.Creator.SexId,
                Birthday = CustomerLead_CustomerLeadDTO.Creator.Birthday,
                PositionId = CustomerLead_CustomerLeadDTO.Creator.PositionId,
                Department = CustomerLead_CustomerLeadDTO.Creator.Department,
                OrganizationId = CustomerLead_CustomerLeadDTO.Creator.OrganizationId,
                StatusId = CustomerLead_CustomerLeadDTO.Creator.StatusId,
            };
            CustomerLead.Sex = CustomerLead_CustomerLeadDTO.Sex == null ? null : new Sex
            {
                Id = CustomerLead_CustomerLeadDTO.Sex.Id,
                Name = CustomerLead_CustomerLeadDTO.Sex.Name,
                Code = CustomerLead_CustomerLeadDTO.Sex.Code,
            };
            CustomerLead.Nation = CustomerLead_CustomerLeadDTO.Nation == null ? null : new Nation
            {
                Id = CustomerLead_CustomerLeadDTO.Nation.Id,
                Name = CustomerLead_CustomerLeadDTO.Nation.Name,
                Code = CustomerLead_CustomerLeadDTO.Nation.Code,
            };
            CustomerLead.CustomerLeadEmails = CustomerLead_CustomerLeadDTO.CustomerLeadEmails?.Select(p => new CustomerLeadEmail
            {
                CustomerLeadId = p.CustomerLeadId,
                Id = p.Id,
                Content = p.Content,
                Reciepient = p.Reciepient,
                CreatorId = p.CreatorId,
                EmailStatusId = p.EmailStatusId,
                Title = p.Title,
                CustomerLeadEmailCCMappings = p.CustomerLeadEmailCCMappings?.Select(x => new CustomerLeadEmailCCMapping
                {
                    AppUserId = x.AppUserId,
                    CustomerLeadEmailId = x.CustomerLeadEmailId,
                }).ToList()
            }).ToList();
            CustomerLead.CustomerLeadItemMappings = CustomerLead_CustomerLeadDTO.CustomerLeadItemMappings?.Select(p => new CustomerLeadItemMapping
            {
                ItemId = p.ItemId,
                CustomerLeadId = p.CustomerLeadId,
                UnitOfMeasureId = p.UnitOfMeasureId,
                Quantity = p.Quantity,
                DiscountPercentage = p.DiscountPercentage,
                RequestQuantity = p.RequestQuantity,
                PrimaryPrice = p.PrimaryPrice,
                SalePrice = p.SalePrice,
                Discount = p.Discount,
                VAT = p.VAT,
                VATOther = p.VATOther,
                TotalPrice = p.TotalPrice,
                Factor = p.Factor,
            }).ToList();
            CustomerLead.CustomerLeadFileGroups = CustomerLead_CustomerLeadDTO.CustomerLeadFileGroups?.Select(x => new CustomerLeadFileGroup
            {
                Id = x.Id,
                CreatorId = x.CreatorId,
                CustomerLeadId = x.CustomerLeadId,
                Description = x.Description,
                FileTypeId = x.FileTypeId,
                Title = x.Title,
                CustomerLeadFileMappings = x.CustomerLeadFileMappings?.Select(y => new CustomerLeadFileMapping
                {
                    FileId = y.FileId
                }).ToList()
            }).ToList();

            CustomerLead.BaseLanguage = CurrentContext.Language;
            return CustomerLead;
        }

        private CustomerLeadFilter ConvertFilterDTOToFilterEntity(CustomerLead_CustomerLeadFilterDTO CustomerLead_CustomerLeadFilterDTO)
        {
            CustomerLeadFilter CustomerLeadFilter = new CustomerLeadFilter();
            CustomerLeadFilter.Selects = CustomerLeadSelect.ALL;
            CustomerLeadFilter.Skip = CustomerLead_CustomerLeadFilterDTO.Skip;
            CustomerLeadFilter.Take = CustomerLead_CustomerLeadFilterDTO.Take;
            CustomerLeadFilter.OrderBy = CustomerLead_CustomerLeadFilterDTO.OrderBy;
            CustomerLeadFilter.OrderType = CustomerLead_CustomerLeadFilterDTO.OrderType;

            CustomerLeadFilter.Id = CustomerLead_CustomerLeadFilterDTO.Id;
            CustomerLeadFilter.Name = CustomerLead_CustomerLeadFilterDTO.Name;
            CustomerLeadFilter.TelePhone = CustomerLead_CustomerLeadFilterDTO.TelePhone;
            CustomerLeadFilter.Phone = CustomerLead_CustomerLeadFilterDTO.Phone;
            CustomerLeadFilter.CompanyName = CustomerLead_CustomerLeadFilterDTO.CompanyName;
            CustomerLeadFilter.Fax = CustomerLead_CustomerLeadFilterDTO.Fax;
            CustomerLeadFilter.Email = CustomerLead_CustomerLeadFilterDTO.Email;
            CustomerLeadFilter.SecondEmail = CustomerLead_CustomerLeadFilterDTO.SecondEmail;
            CustomerLeadFilter.Website = CustomerLead_CustomerLeadFilterDTO.Website;
            CustomerLeadFilter.CustomerLeadSourceId = CustomerLead_CustomerLeadFilterDTO.CustomerLeadSourceId;
            CustomerLeadFilter.CustomerLeadLevelId = CustomerLead_CustomerLeadFilterDTO.CustomerLeadLevelId;
            CustomerLeadFilter.CompanyId = CustomerLead_CustomerLeadFilterDTO.CompanyId;
            CustomerLeadFilter.CampaignId = CustomerLead_CustomerLeadFilterDTO.CampaignId;
            CustomerLeadFilter.ProfessionId = CustomerLead_CustomerLeadFilterDTO.ProfessionId;
            CustomerLeadFilter.Revenue = CustomerLead_CustomerLeadFilterDTO.Revenue;
            CustomerLeadFilter.EmployeeQuantity = CustomerLead_CustomerLeadFilterDTO.EmployeeQuantity;
            CustomerLeadFilter.Address = CustomerLead_CustomerLeadFilterDTO.Address;
            CustomerLeadFilter.ProvinceId = CustomerLead_CustomerLeadFilterDTO.ProvinceId;
            CustomerLeadFilter.DistrictId = CustomerLead_CustomerLeadFilterDTO.DistrictId;
            CustomerLeadFilter.AppUserId = CustomerLead_CustomerLeadFilterDTO.AppUserId;
            CustomerLeadFilter.CustomerLeadStatusId = CustomerLead_CustomerLeadFilterDTO.CustomerLeadStatusId;
            CustomerLeadFilter.BusinessRegistrationCode = CustomerLead_CustomerLeadFilterDTO.BusinessRegistrationCode;
            CustomerLeadFilter.SexId = CustomerLead_CustomerLeadFilterDTO.SexId;
            CustomerLeadFilter.NationId = CustomerLead_CustomerLeadFilterDTO.NationId;
            CustomerLeadFilter.Description = CustomerLead_CustomerLeadFilterDTO.Description;
            CustomerLeadFilter.CreatorId = CustomerLead_CustomerLeadFilterDTO.CreatorId;
            CustomerLeadFilter.CreatedAt = CustomerLead_CustomerLeadFilterDTO.CreatedAt;
            CustomerLeadFilter.UpdatedAt = CustomerLead_CustomerLeadFilterDTO.UpdatedAt;
            return CustomerLeadFilter;
        }

        #region activity
        [Route(CustomerLeadRoute.CreateActivity), HttpPost]
        public async Task<ActionResult<CustomerLead_CustomerLeadActivityDTO>> CreateActivity([FromBody] CustomerLead_CustomerLeadActivityDTO CustomerLead_CustomerLeadActivityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadActivity CustomerLeadActivity = ConvertActivity(CustomerLead_CustomerLeadActivityDTO);
            CustomerLeadActivity = await CustomerLeadActivityService.Create(CustomerLeadActivity);
            CustomerLead_CustomerLeadActivityDTO = new CustomerLead_CustomerLeadActivityDTO(CustomerLeadActivity);
            if (CustomerLeadActivity.IsValidated)
                return CustomerLead_CustomerLeadActivityDTO;
            else
                return BadRequest(CustomerLead_CustomerLeadActivityDTO);
        }

        [Route(CustomerLeadRoute.UpdateActivity), HttpPost]
        public async Task<ActionResult<CustomerLead_CustomerLeadActivityDTO>> UpdateActivity([FromBody] CustomerLead_CustomerLeadActivityDTO CustomerLead_CustomerLeadActivityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadActivity CustomerLeadActivity = ConvertActivity(CustomerLead_CustomerLeadActivityDTO);
            CustomerLeadActivity = await CustomerLeadActivityService.Update(CustomerLeadActivity);
            CustomerLead_CustomerLeadActivityDTO = new CustomerLead_CustomerLeadActivityDTO(CustomerLeadActivity);
            if (CustomerLeadActivity.IsValidated)
                return CustomerLead_CustomerLeadActivityDTO;
            else
                return BadRequest(CustomerLead_CustomerLeadActivityDTO);
        }

        [Route(CustomerLeadRoute.DeleteActivity), HttpPost]
        public async Task<ActionResult<CustomerLead_CustomerLeadActivityDTO>> DeleteActivity([FromBody] CustomerLead_CustomerLeadActivityDTO CustomerLead_CustomerLeadActivityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadActivity CustomerLeadActivity = ConvertActivity(CustomerLead_CustomerLeadActivityDTO);
            CustomerLeadActivity = await CustomerLeadActivityService.Delete(CustomerLeadActivity);
            CustomerLead_CustomerLeadActivityDTO = new CustomerLead_CustomerLeadActivityDTO(CustomerLeadActivity);
            if (CustomerLeadActivity.IsValidated)
                return CustomerLead_CustomerLeadActivityDTO;
            else
                return BadRequest(CustomerLead_CustomerLeadActivityDTO);
        }

        [Route(CustomerLeadRoute.BulkDeleteActivity), HttpPost]
        public async Task<ActionResult<bool>> BulkDeleteActivity([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadActivityFilter CustomerLeadActivityFilter = new CustomerLeadActivityFilter();
            CustomerLeadActivityFilter = await CustomerLeadActivityService.ToFilter(CustomerLeadActivityFilter);
            CustomerLeadActivityFilter.Id = new IdFilter { In = Ids };
            CustomerLeadActivityFilter.Selects = CustomerLeadActivitySelect.Id;
            CustomerLeadActivityFilter.Skip = 0;
            CustomerLeadActivityFilter.Take = int.MaxValue;

            List<CustomerLeadActivity> CustomerLeadActivities = await CustomerLeadActivityService.List(CustomerLeadActivityFilter);
            CustomerLeadActivities = await CustomerLeadActivityService.BulkDelete(CustomerLeadActivities);
            if (CustomerLeadActivities.Any(x => !x.IsValidated))
                return BadRequest(CustomerLeadActivities.Where(x => !x.IsValidated));
            return true;
        }

        private CustomerLeadActivity ConvertActivity(CustomerLead_CustomerLeadActivityDTO CustomerLead_CustomerLeadActivityDTO)
        {
            CustomerLeadActivity CustomerLeadActivity = new CustomerLeadActivity();
            CustomerLeadActivity.Id = CustomerLead_CustomerLeadActivityDTO.Id;
            CustomerLeadActivity.Title = CustomerLead_CustomerLeadActivityDTO.Title;
            CustomerLeadActivity.FromDate = CustomerLead_CustomerLeadActivityDTO.FromDate;
            CustomerLeadActivity.ToDate = CustomerLead_CustomerLeadActivityDTO.ToDate;
            CustomerLeadActivity.ActivityTypeId = CustomerLead_CustomerLeadActivityDTO.ActivityTypeId;
            CustomerLeadActivity.ActivityPriorityId = CustomerLead_CustomerLeadActivityDTO.ActivityPriorityId;
            CustomerLeadActivity.Description = CustomerLead_CustomerLeadActivityDTO.Description;
            CustomerLeadActivity.Address = CustomerLead_CustomerLeadActivityDTO.Address;
            CustomerLeadActivity.CustomerLeadId = CustomerLead_CustomerLeadActivityDTO.CustomerLeadId;
            CustomerLeadActivity.AppUserId = CustomerLead_CustomerLeadActivityDTO.AppUserId;
            CustomerLeadActivity.ActivityStatusId = CustomerLead_CustomerLeadActivityDTO.ActivityStatusId;
            CustomerLeadActivity.ActivityPriority = CustomerLead_CustomerLeadActivityDTO.ActivityPriority == null ? null : new ActivityPriority
            {
                Id = CustomerLead_CustomerLeadActivityDTO.ActivityPriority.Id,
                Code = CustomerLead_CustomerLeadActivityDTO.ActivityPriority.Code,
                Name = CustomerLead_CustomerLeadActivityDTO.ActivityPriority.Name,
            };
            CustomerLeadActivity.ActivityStatus = CustomerLead_CustomerLeadActivityDTO.ActivityStatus == null ? null : new ActivityStatus
            {
                Id = CustomerLead_CustomerLeadActivityDTO.ActivityStatus.Id,
                Code = CustomerLead_CustomerLeadActivityDTO.ActivityStatus.Code,
                Name = CustomerLead_CustomerLeadActivityDTO.ActivityStatus.Name,
            };
            CustomerLeadActivity.ActivityType = CustomerLead_CustomerLeadActivityDTO.ActivityType == null ? null : new ActivityType
            {
                Id = CustomerLead_CustomerLeadActivityDTO.ActivityType.Id,
                Code = CustomerLead_CustomerLeadActivityDTO.ActivityType.Code,
                Name = CustomerLead_CustomerLeadActivityDTO.ActivityType.Name,
            };
            CustomerLeadActivity.AppUser = CustomerLead_CustomerLeadActivityDTO.AppUser == null ? null : new AppUser
            {
                Id = CustomerLead_CustomerLeadActivityDTO.AppUser.Id,
                Username = CustomerLead_CustomerLeadActivityDTO.AppUser.Username,
                DisplayName = CustomerLead_CustomerLeadActivityDTO.AppUser.DisplayName,
                Address = CustomerLead_CustomerLeadActivityDTO.AppUser.Address,
                Email = CustomerLead_CustomerLeadActivityDTO.AppUser.Email,
                Phone = CustomerLead_CustomerLeadActivityDTO.AppUser.Phone,
                SexId = CustomerLead_CustomerLeadActivityDTO.AppUser.SexId,
                Birthday = CustomerLead_CustomerLeadActivityDTO.AppUser.Birthday,
                Department = CustomerLead_CustomerLeadActivityDTO.AppUser.Department,
                OrganizationId = CustomerLead_CustomerLeadActivityDTO.AppUser.OrganizationId,
                StatusId = CustomerLead_CustomerLeadActivityDTO.AppUser.StatusId,
            };
            CustomerLeadActivity.BaseLanguage = CurrentContext.Language;
            return CustomerLeadActivity;
        }
        #endregion

        #region CallLog
        [Route(CustomerLeadRoute.DeleteCallLog), HttpPost]
        public async Task<ActionResult<CustomerLead_CallLogDTO>> DeleteCallLog([FromBody] CustomerLead_CallLogDTO CustomerLead_CallLogDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLog CallLog = ConvertCallLog(CustomerLead_CallLogDTO);
            CallLog = await CallLogService.Delete(CallLog);
            CustomerLead_CallLogDTO = new CustomerLead_CallLogDTO(CallLog);
            if (CallLog.IsValidated)
                return CustomerLead_CallLogDTO;
            else
                return BadRequest(CustomerLead_CallLogDTO);
        }

        private CallLog ConvertCallLog(CustomerLead_CallLogDTO CustomerLead_CallLogDTO)
        {
            CallLog CallLog = new CallLog();
            CallLog.Id = CustomerLead_CallLogDTO.Id;
            CallLog.EntityReferenceId = CustomerLead_CallLogDTO.EntityReferenceId;
            CallLog.CallTypeId = CustomerLead_CallLogDTO.CallTypeId;
            CallLog.CallEmotionId = CustomerLead_CallLogDTO.CallEmotionId;
            CallLog.AppUserId = CustomerLead_CallLogDTO.AppUserId;
            CallLog.Title = CustomerLead_CallLogDTO.Title;
            CallLog.Content = CustomerLead_CallLogDTO.Content;
            CallLog.Phone = CustomerLead_CallLogDTO.Phone;
            CallLog.CallTime = CustomerLead_CallLogDTO.CallTime;
            CallLog.AppUser = CustomerLead_CallLogDTO.AppUser == null ? null : new AppUser
            {
                Id = CustomerLead_CallLogDTO.AppUser.Id,
                Username = CustomerLead_CallLogDTO.AppUser.Username,
                DisplayName = CustomerLead_CallLogDTO.AppUser.DisplayName,
                Address = CustomerLead_CallLogDTO.AppUser.Address,
                Email = CustomerLead_CallLogDTO.AppUser.Email,
                Phone = CustomerLead_CallLogDTO.AppUser.Phone,
                SexId = CustomerLead_CallLogDTO.AppUser.SexId,
                Birthday = CustomerLead_CallLogDTO.AppUser.Birthday,
                Avatar = CustomerLead_CallLogDTO.AppUser.Avatar,
                PositionId = CustomerLead_CallLogDTO.AppUser.PositionId,
                Department = CustomerLead_CallLogDTO.AppUser.Department,
                OrganizationId = CustomerLead_CallLogDTO.AppUser.OrganizationId,
                ProvinceId = CustomerLead_CallLogDTO.AppUser.ProvinceId,
                Longitude = CustomerLead_CallLogDTO.AppUser.Longitude,
                Latitude = CustomerLead_CallLogDTO.AppUser.Latitude,
                StatusId = CustomerLead_CallLogDTO.AppUser.StatusId,
            };
            CallLog.EntityReference = CustomerLead_CallLogDTO.EntityReference == null ? null : new EntityReference
            {
                Id = CustomerLead_CallLogDTO.EntityReference.Id,
                Code = CustomerLead_CallLogDTO.EntityReference.Code,
                Name = CustomerLead_CallLogDTO.EntityReference.Name,
            };
            CallLog.CallType = CustomerLead_CallLogDTO.CallType == null ? null : new CallType
            {
                Id = CustomerLead_CallLogDTO.CallType.Id,
                Code = CustomerLead_CallLogDTO.CallType.Code,
                Name = CustomerLead_CallLogDTO.CallType.Name,
                ColorCode = CustomerLead_CallLogDTO.CallType.ColorCode,
                StatusId = CustomerLead_CallLogDTO.CallType.StatusId,
                Used = CustomerLead_CallLogDTO.CallType.Used,
            };
            CallLog.CallEmotion = CustomerLead_CallLogDTO.CallEmotion == null ? null : new CallEmotion
            {
                Id = CustomerLead_CallLogDTO.CallEmotion.Id,
                Name = CustomerLead_CallLogDTO.CallEmotion.Name,
                Code = CustomerLead_CallLogDTO.CallEmotion.Code,
                StatusId = CustomerLead_CallLogDTO.CallEmotion.StatusId,
                Description = CustomerLead_CallLogDTO.CallEmotion.Description,
            };
            CallLog.BaseLanguage = CurrentContext.Language;
            return CallLog;
        }
        #endregion

        [Route(CustomerLeadRoute.SendSms), HttpPost]
        public async Task<ActionResult<bool>> SendSms([FromBody] CustomerLead_SmsQueueDTO CustomerLead_SmsQueueDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            SmsQueue SmsQueue = new SmsQueue();
            SmsQueue.Id = CustomerLead_SmsQueueDTO.Id;
            SmsQueue.Phone = CustomerLead_SmsQueueDTO.Phone;
            SmsQueue.SmsCode = CustomerLead_SmsQueueDTO.SmsCode;
            SmsQueue.SmsTitle = CustomerLead_SmsQueueDTO.SmsTitle;
            SmsQueue.SmsContent = CustomerLead_SmsQueueDTO.SmsContent;
            SmsQueue.SentByAppUserId = CurrentContext.UserId;
            SmsQueue.SmsQueueStatusId = CustomerLead_SmsQueueDTO.SmsQueueStatusId;
            SmsQueue.SmsQueueStatus = CustomerLead_SmsQueueDTO.SmsQueueStatus == null ? null : new SmsQueueStatus
            {
                Id = CustomerLead_SmsQueueDTO.SmsQueueStatus.Id,
                Code = CustomerLead_SmsQueueDTO.SmsQueueStatus.Code,
                Name = CustomerLead_SmsQueueDTO.SmsQueueStatus.Name,
            };
            SmsQueue.BaseLanguage = CurrentContext.Language;

            SmsQueue.EntityReferenceId = CustomerLead_SmsQueueDTO.EntityReferenceId;
            return true;
        }

        #region Email
        [Route(CustomerLeadRoute.CreateEmail), HttpPost]
        public async Task<ActionResult<CustomerLead_CustomerLeadEmailDTO>> CreateEmail([FromBody] CustomerLead_CustomerLeadEmailDTO CustomerLead_CustomerLeadEmailDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerLeadEmail CustomerLeadEmail = ConvertCustomerLeadEmail(CustomerLead_CustomerLeadEmailDTO);
            CustomerLeadEmail = await CustomerLeadEmailService.Create(CustomerLeadEmail);
            CustomerLead_CustomerLeadEmailDTO = new CustomerLead_CustomerLeadEmailDTO(CustomerLeadEmail);
            if (CustomerLeadEmail.IsValidated)
                return CustomerLead_CustomerLeadEmailDTO;
            else
                return BadRequest(CustomerLead_CustomerLeadEmailDTO);
        }

        [Route(CustomerLeadRoute.SendEmail), HttpPost]
        public async Task<ActionResult<bool>> SendEmail([FromBody] CustomerLead_CustomerLeadEmailDTO CustomerLead_CustomerLeadEmailDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            CustomerLeadEmail CustomerLeadEmail = ConvertCustomerLeadEmail(CustomerLead_CustomerLeadEmailDTO);
            CustomerLeadEmail = await CustomerLeadEmailService.Send(CustomerLeadEmail);
            if (CustomerLeadEmail.IsValidated)
                return Ok();
            else
                return BadRequest(CustomerLead_CustomerLeadEmailDTO);
        }

        private CustomerLeadEmail ConvertCustomerLeadEmail(CustomerLead_CustomerLeadEmailDTO CustomerLead_CustomerLeadEmailDTO)
        {
            CustomerLeadEmail CustomerLeadEmail = new CustomerLeadEmail();
            CustomerLeadEmail.Id = CustomerLead_CustomerLeadEmailDTO.Id;
            CustomerLeadEmail.Reciepient = CustomerLead_CustomerLeadEmailDTO.Reciepient;
            CustomerLeadEmail.Title = CustomerLead_CustomerLeadEmailDTO.Title;
            CustomerLeadEmail.Content = CustomerLead_CustomerLeadEmailDTO.Content;
            CustomerLeadEmail.CreatorId = CustomerLead_CustomerLeadEmailDTO.CreatorId;
            CustomerLeadEmail.CustomerLeadId = CustomerLead_CustomerLeadEmailDTO.CustomerLeadId;
            CustomerLeadEmail.EmailStatusId = CustomerLead_CustomerLeadEmailDTO.EmailStatusId;
            CustomerLeadEmail.EmailStatus = CustomerLead_CustomerLeadEmailDTO.EmailStatus == null ? null : new EmailStatus
            {
                Id = CustomerLead_CustomerLeadEmailDTO.EmailStatus.Id,
                Code = CustomerLead_CustomerLeadEmailDTO.EmailStatus.Code,
                Name = CustomerLead_CustomerLeadEmailDTO.EmailStatus.Name,
            };
            CustomerLeadEmail.CustomerLeadEmailCCMappings = CustomerLead_CustomerLeadEmailDTO.CustomerLeadEmailCCMappings?.Select(x => new CustomerLeadEmailCCMapping
            {
                AppUserId = x.AppUserId,
                CustomerLeadEmailId = x.CustomerLeadEmailId,
                AppUser = x.AppUser == null ? null : new AppUser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Email = x.AppUser.Email,
                },
            }).ToList();
            CustomerLeadEmail.BaseLanguage = CurrentContext.Language;
            return CustomerLeadEmail;
        }
        #endregion
    }
}

