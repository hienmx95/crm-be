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
using CRM.Services.MCompanyStatus;
using CRM.Services.MContact;
using CRM.Services.MCurrency;
using CRM.Services.MCustomerLead;
using CRM.Services.MCustomerLeadLevel;
using CRM.Services.MCustomerLeadSource;
using CRM.Services.MCustomerLeadStatus;
using CRM.Services.MDistrict;
using CRM.Services.MEditedPriceStatus;
using CRM.Services.MEmailStatus;
using CRM.Services.MFile;
using CRM.Services.MFileType;
using CRM.Services.MMailTemplate;
using CRM.Services.MNation;
using CRM.Services.MOpportunity;
using CRM.Services.MOpportunityActivity;
using CRM.Services.MOpportunityEmail;
using CRM.Services.MOpportunityResultType;
using CRM.Services.MOrderQuote;
using CRM.Services.MOrderQuoteStatus;
using CRM.Services.MOrganization;
using CRM.Services.MPosition;
using CRM.Services.MPotentialResult;
using CRM.Services.MProbability;
using CRM.Services.MProduct;
using CRM.Services.MProductGrouping;
using CRM.Services.MProductType;
using CRM.Services.MProfession;
using CRM.Services.MProvince;
using CRM.Services.MSaleStage;
using CRM.Services.MSex;
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

namespace CRM.Rpc.opportunity
{
    public partial class OpportunityController : RpcController
    {
        private ICompanyService CompanyService;
        private ICompanyStatusService CompanyStatusService;
        private IAppUserService AppUserService;
        private IContactService ContactService;
        private IFileService FileService;
        private IOrganizationService OrganizationService;
        private ICustomerLeadSourceService CustomerLeadSourceService;
        private IPotentialResultService PotentialResultService;
        private IProbabilityService ProbabilityService;
        private ISaleStageService SaleStageService;
        private IOpportunityService OpportunityService;
        private ISupplierService SupplierService;
        private ICurrencyService CurrencyService;
        private IProductService ProductService;
        private IOpportunityResultTypeService OpportunityResultTypeService;
        private IUnitOfMeasureService UnitOfMeasureService;
        private IItemService ItemService;
        private IActivityStatusService ActivityStatusService;
        private IActivityTypeService ActivityTypeService;
        private IActivityPriorityService ActivityPriorityService;
        private ICustomerLeadLevelService CustomerLeadLevelService;
        private ICustomerLeadStatusService CustomerLeadStatusService;
        private ICallLogService CallLogService;
        private IDistrictService DistrictService;
        private IFileTypeService FileTypeService;
        private IProfessionService ProfessionService;
        private IOrderQuoteService OrderQuoteService;
        private IProvinceService ProvinceService;
        private ICustomerLeadService CustomerLeadService;
        private IProductGroupingService ProductGroupingService;
        private IProductTypeService ProductTypeService;
        private ISexService SexService;
        private INationService NationService;
        private IOrderQuoteStatusService OrderQuoteStatusService;
        private IEditedPriceStatusService EditedPriceStatusService;
        private ITaxTypeService TaxTypeService;
        private IPositionService PositionService;
        private IOpportunityEmailService OpportunityEmailService;
        private IMailTemplateService MailTemplateService;
        private ICurrentContext CurrentContext;
        private DataContext DataContext;
        private IEmailStatusService EmailStatusService;
        private IOpportunityActivityService OpportunityActivityService;


        public OpportunityController(
            ICompanyService CompanyService,
            ICompanyStatusService CompanyStatusService,
            IAppUserService AppUserService,
            IContactService ContactService,
            IFileService FileService,
            IOrganizationService OrganizationService,
            ICustomerLeadSourceService CustomerLeadSourceService,
            IPotentialResultService PotentialResultService,
            IProbabilityService ProbabilityService,
            ISaleStageService SaleStageService,
            IOpportunityService OpportunityService,
            ISupplierService SupplierService,
            ICurrencyService CurrencyService,
            IProductService ProductService,
            IOrderQuoteService OrderQuoteService,
            IOpportunityResultTypeService OpportunityResultTypeService,
            IUnitOfMeasureService UnitOfMeasureService,
            IItemService ItemService,
            IActivityStatusService ActivityStatusService,
            IActivityTypeService ActivityTypeService,
            IActivityPriorityService ActivityPriorityService,
            ICustomerLeadLevelService CustomerLeadLevelService,
            ICustomerLeadStatusService CustomerLeadStatusService,
            ICallLogService CallLogService,
            IDistrictService DistrictService,
            IFileTypeService FileTypeService,
            IProfessionService ProfessionService,
            IProvinceService ProvinceService,
            ICustomerLeadService CustomerLeadService,
            IProductGroupingService ProductGroupingService,
            IProductTypeService ProductTypeService,
            ISexService SexService,
            INationService NationService,
            IOrderQuoteStatusService OrderQuoteStatusService,
            IEditedPriceStatusService EditedPriceStatusService,
            ITaxTypeService TaxTypeService,
            IPositionService PositionService,
            IOpportunityEmailService OpportunityEmailService,
            IMailTemplateService MailTemplateService,
            ICurrentContext CurrentContext,
            DataContext DataContext,
            IEmailStatusService EmailStatusService,
            IOpportunityActivityService OpportunityActivityService
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.CompanyService = CompanyService;
            this.CompanyStatusService = CompanyStatusService;
            this.AppUserService = AppUserService;
            this.ContactService = ContactService;
            this.FileService = FileService;
            this.OrganizationService = OrganizationService;
            this.CustomerLeadSourceService = CustomerLeadSourceService;
            this.PotentialResultService = PotentialResultService;
            this.ProbabilityService = ProbabilityService;
            this.SaleStageService = SaleStageService;
            this.OpportunityService = OpportunityService;
            this.SupplierService = SupplierService;
            this.CurrencyService = CurrencyService;
            this.ProductService = ProductService;
            this.OpportunityResultTypeService = OpportunityResultTypeService;
            this.UnitOfMeasureService = UnitOfMeasureService;
            this.ItemService = ItemService;
            this.ActivityStatusService = ActivityStatusService;
            this.OrderQuoteService = OrderQuoteService;
            this.ActivityTypeService = ActivityTypeService;
            this.ActivityPriorityService = ActivityPriorityService;
            this.CustomerLeadLevelService = CustomerLeadLevelService;
            this.CustomerLeadStatusService = CustomerLeadStatusService;
            this.CallLogService = CallLogService;
            this.DistrictService = DistrictService;
            this.FileTypeService = FileTypeService;
            this.ProfessionService = ProfessionService;
            this.ProvinceService = ProvinceService;
            this.CustomerLeadService = CustomerLeadService;
            this.ProductGroupingService = ProductGroupingService;
            this.ProductTypeService = ProductTypeService;
            this.SexService = SexService;
            this.NationService = NationService;
            this.OrderQuoteStatusService = OrderQuoteStatusService;
            this.EditedPriceStatusService = EditedPriceStatusService;
            this.TaxTypeService = TaxTypeService;
            this.PositionService = PositionService;
            this.OpportunityEmailService = OpportunityEmailService;
            this.MailTemplateService = MailTemplateService;
            this.CurrentContext = CurrentContext;
            this.DataContext = DataContext;
            this.EmailStatusService = EmailStatusService;
            this.OpportunityActivityService = OpportunityActivityService;
        }

        [Route(OpportunityRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] Opportunity_OpportunityFilterDTO Opportunity_OpportunityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityFilter OpportunityFilter = ConvertFilterDTOToFilterEntity(Opportunity_OpportunityFilterDTO);
            OpportunityFilter = await OpportunityService.ToFilter(OpportunityFilter);
            int count = await OpportunityService.Count(OpportunityFilter);
            return count;
        }

        [Route(OpportunityRoute.List), HttpPost]
        public async Task<ActionResult<List<Opportunity_OpportunityDTO>>> List([FromBody] Opportunity_OpportunityFilterDTO Opportunity_OpportunityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityFilter OpportunityFilter = ConvertFilterDTOToFilterEntity(Opportunity_OpportunityFilterDTO);
            OpportunityFilter = await OpportunityService.ToFilter(OpportunityFilter);
            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            List<Opportunity_OpportunityDTO> Opportunity_OpportunityDTOs = Opportunities
                .Select(c => new Opportunity_OpportunityDTO(c)).ToList();
            return Opportunity_OpportunityDTOs;
        }

        [Route(OpportunityRoute.Get), HttpPost]
        public async Task<ActionResult<Opportunity_OpportunityDTO>> Get([FromBody] Opportunity_OpportunityDTO Opportunity_OpportunityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Opportunity_OpportunityDTO.Id))
                return Forbid();

            Opportunity Opportunity = await OpportunityService.Get(Opportunity_OpportunityDTO.Id);
            return new Opportunity_OpportunityDTO(Opportunity);
        }

        [Route(OpportunityRoute.Create), HttpPost]
        public async Task<ActionResult<Opportunity_OpportunityDTO>> Create([FromBody] Opportunity_OpportunityDTO Opportunity_OpportunityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Opportunity_OpportunityDTO.Id))
                return Forbid();

            Opportunity Opportunity = ConvertDTOToEntity(Opportunity_OpportunityDTO);
            Opportunity = await OpportunityService.Create(Opportunity);
            Opportunity_OpportunityDTO = new Opportunity_OpportunityDTO(Opportunity);
            if (Opportunity.IsValidated)
                return Opportunity_OpportunityDTO;
            else
                return BadRequest(Opportunity_OpportunityDTO);
        }

        [Route(OpportunityRoute.Update), HttpPost]
        public async Task<ActionResult<Opportunity_OpportunityDTO>> Update([FromBody] Opportunity_OpportunityDTO Opportunity_OpportunityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Opportunity_OpportunityDTO.Id))
                return Forbid();

            Opportunity Opportunity = ConvertDTOToEntity(Opportunity_OpportunityDTO);
            Opportunity = await OpportunityService.Update(Opportunity);
            Opportunity_OpportunityDTO = new Opportunity_OpportunityDTO(Opportunity);
            if (Opportunity.IsValidated)
                return Opportunity_OpportunityDTO;
            else
                return BadRequest(Opportunity_OpportunityDTO);
        }

        [Route(OpportunityRoute.Delete), HttpPost]
        public async Task<ActionResult<Opportunity_OpportunityDTO>> Delete([FromBody] Opportunity_OpportunityDTO Opportunity_OpportunityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Opportunity_OpportunityDTO.Id))
                return Forbid();

            Opportunity Opportunity = ConvertDTOToEntity(Opportunity_OpportunityDTO);
            Opportunity = await OpportunityService.Delete(Opportunity);
            Opportunity_OpportunityDTO = new Opportunity_OpportunityDTO(Opportunity);
            if (Opportunity.IsValidated)
                return Opportunity_OpportunityDTO;
            else
                return BadRequest(Opportunity_OpportunityDTO);
        }

        [Route(OpportunityRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
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

        [Route(OpportunityRoute.Import), HttpPost]
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
            CustomerLeadSourceFilter LeadSourceFilter = new CustomerLeadSourceFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CustomerLeadSourceSelect.ALL
            };
            List<CustomerLeadSource> LeadSources = await CustomerLeadSourceService.List(LeadSourceFilter);
            OpportunityResultTypeFilter OpportunityResultTypeFilter = new OpportunityResultTypeFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = OpportunityResultTypeSelect.ALL
            };
            List<OpportunityResultType> OpportunityResultTypes = await OpportunityResultTypeService.List(OpportunityResultTypeFilter);
            PotentialResultFilter PotentialResultFilter = new PotentialResultFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = PotentialResultSelect.ALL
            };
            List<PotentialResult> PotentialResults = await PotentialResultService.List(PotentialResultFilter);
            ProbabilityFilter ProbabilityFilter = new ProbabilityFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = ProbabilitySelect.ALL
            };
            List<Probability> Probabilities = await ProbabilityService.List(ProbabilityFilter);
            SaleStageFilter SaleStageFilter = new SaleStageFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = SaleStageSelect.ALL
            };
            List<SaleStage> SaleStages = await SaleStageService.List(SaleStageFilter);
            List<Opportunity> Opportunities = new List<Opportunity>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(Opportunities);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int NameColumn = 1 + StartColumn;
                int CompanyIdColumn = 2 + StartColumn;
                int CustomerLeadIdColumn = 3 + StartColumn;
                int ClosingDateColumn = 4 + StartColumn;
                int SaleStageIdColumn = 5 + StartColumn;
                int ProbabilityIdColumn = 6 + StartColumn;
                int PotentialResultIdColumn = 7 + StartColumn;
                int LeadSourceIdColumn = 8 + StartColumn;
                int AppUserIdColumn = 9 + StartColumn;
                int CurrencyIdColumn = 10 + StartColumn;
                int AmountColumn = 11 + StartColumn;
                int ForecastAmountColumn = 12 + StartColumn;
                int DescriptionColumn = 13 + StartColumn;
                int RefuseReciveSMSColumn = 14 + StartColumn;
                int RefuseReciveEmailColumn = 15 + StartColumn;
                int OpportunityResultTypeIdColumn = 16 + StartColumn;
                int CreatorIdColumn = 17 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string CompanyIdValue = worksheet.Cells[i + StartRow, CompanyIdColumn].Value?.ToString();
                    string CustomerLeadIdValue = worksheet.Cells[i + StartRow, CustomerLeadIdColumn].Value?.ToString();
                    string ClosingDateValue = worksheet.Cells[i + StartRow, ClosingDateColumn].Value?.ToString();
                    string SaleStageIdValue = worksheet.Cells[i + StartRow, SaleStageIdColumn].Value?.ToString();
                    string ProbabilityIdValue = worksheet.Cells[i + StartRow, ProbabilityIdColumn].Value?.ToString();
                    string PotentialResultIdValue = worksheet.Cells[i + StartRow, PotentialResultIdColumn].Value?.ToString();
                    string LeadSourceIdValue = worksheet.Cells[i + StartRow, LeadSourceIdColumn].Value?.ToString();
                    string AppUserIdValue = worksheet.Cells[i + StartRow, AppUserIdColumn].Value?.ToString();
                    string CurrencyIdValue = worksheet.Cells[i + StartRow, CurrencyIdColumn].Value?.ToString();
                    string AmountValue = worksheet.Cells[i + StartRow, AmountColumn].Value?.ToString();
                    string ForecastAmountValue = worksheet.Cells[i + StartRow, ForecastAmountColumn].Value?.ToString();
                    string DescriptionValue = worksheet.Cells[i + StartRow, DescriptionColumn].Value?.ToString();
                    string RefuseReciveSMSValue = worksheet.Cells[i + StartRow, RefuseReciveSMSColumn].Value?.ToString();
                    string RefuseReciveEmailValue = worksheet.Cells[i + StartRow, RefuseReciveEmailColumn].Value?.ToString();
                    string OpportunityResultTypeIdValue = worksheet.Cells[i + StartRow, OpportunityResultTypeIdColumn].Value?.ToString();
                    string CreatorIdValue = worksheet.Cells[i + StartRow, CreatorIdColumn].Value?.ToString();

                    Opportunity Opportunity = new Opportunity();
                    Opportunity.Name = NameValue;
                    Opportunity.ClosingDate = DateTime.TryParse(ClosingDateValue, out DateTime ClosingDate) ? ClosingDate : DateTime.Now;
                    Opportunity.Amount = decimal.TryParse(AmountValue, out decimal Amount) ? Amount : 0;
                    Opportunity.ForecastAmount = decimal.TryParse(ForecastAmountValue, out decimal ForecastAmount) ? ForecastAmount : 0;
                    Opportunity.Description = DescriptionValue;
                    AppUser AppUser = AppUsers.Where(x => x.Id.ToString() == AppUserIdValue).FirstOrDefault();
                    Opportunity.AppUserId = AppUser == null ? 0 : AppUser.Id;
                    Opportunity.AppUser = AppUser;
                    Currency Currency = Currencies.Where(x => x.Id.ToString() == CurrencyIdValue).FirstOrDefault();
                    Opportunity.CurrencyId = Currency == null ? 0 : Currency.Id;
                    Opportunity.Currency = Currency;
                    CustomerLead CustomerLead = CustomerLeads.Where(x => x.Id.ToString() == CustomerLeadIdValue).FirstOrDefault();
                    Opportunity.CustomerLeadId = CustomerLead == null ? 0 : CustomerLead.Id;
                    Opportunity.CustomerLead = CustomerLead;
                    CustomerLeadSource LeadSource = LeadSources.Where(x => x.Id.ToString() == LeadSourceIdValue).FirstOrDefault();
                    Opportunity.LeadSourceId = LeadSource == null ? 0 : LeadSource.Id;
                    Opportunity.LeadSource = LeadSource;
                    OpportunityResultType OpportunityResultType = OpportunityResultTypes.Where(x => x.Id.ToString() == OpportunityResultTypeIdValue).FirstOrDefault();
                    Opportunity.OpportunityResultTypeId = OpportunityResultType == null ? 0 : OpportunityResultType.Id;
                    Opportunity.OpportunityResultType = OpportunityResultType;
                    PotentialResult PotentialResult = PotentialResults.Where(x => x.Id.ToString() == PotentialResultIdValue).FirstOrDefault();
                    Opportunity.PotentialResultId = PotentialResult == null ? 0 : PotentialResult.Id;
                    Opportunity.PotentialResult = PotentialResult;
                    Probability Probability = Probabilities.Where(x => x.Id.ToString() == ProbabilityIdValue).FirstOrDefault();
                    Opportunity.ProbabilityId = Probability == null ? 0 : Probability.Id;
                    Opportunity.Probability = Probability;
                    SaleStage SaleStage = SaleStages.Where(x => x.Id.ToString() == SaleStageIdValue).FirstOrDefault();
                    Opportunity.SaleStageId = SaleStage == null ? 0 : SaleStage.Id;
                    Opportunity.SaleStage = SaleStage;

                    Opportunities.Add(Opportunity);
                }
            }
            Opportunities = await OpportunityService.Import(Opportunities);
            if (Opportunities.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < Opportunities.Count; i++)
                {
                    Opportunity Opportunity = Opportunities[i];
                    if (!Opportunity.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.Id)))
                            Error += Opportunity.Errors[nameof(Opportunity.Id)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.Name)))
                            Error += Opportunity.Errors[nameof(Opportunity.Name)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.CompanyId)))
                            Error += Opportunity.Errors[nameof(Opportunity.CompanyId)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.CustomerLeadId)))
                            Error += Opportunity.Errors[nameof(Opportunity.CustomerLeadId)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.ClosingDate)))
                            Error += Opportunity.Errors[nameof(Opportunity.ClosingDate)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.SaleStageId)))
                            Error += Opportunity.Errors[nameof(Opportunity.SaleStageId)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.ProbabilityId)))
                            Error += Opportunity.Errors[nameof(Opportunity.ProbabilityId)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.PotentialResultId)))
                            Error += Opportunity.Errors[nameof(Opportunity.PotentialResultId)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.LeadSourceId)))
                            Error += Opportunity.Errors[nameof(Opportunity.LeadSourceId)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.AppUserId)))
                            Error += Opportunity.Errors[nameof(Opportunity.AppUserId)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.CurrencyId)))
                            Error += Opportunity.Errors[nameof(Opportunity.CurrencyId)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.Amount)))
                            Error += Opportunity.Errors[nameof(Opportunity.Amount)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.ForecastAmount)))
                            Error += Opportunity.Errors[nameof(Opportunity.ForecastAmount)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.Description)))
                            Error += Opportunity.Errors[nameof(Opportunity.Description)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.RefuseReciveSMS)))
                            Error += Opportunity.Errors[nameof(Opportunity.RefuseReciveSMS)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.RefuseReciveEmail)))
                            Error += Opportunity.Errors[nameof(Opportunity.RefuseReciveEmail)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.OpportunityResultTypeId)))
                            Error += Opportunity.Errors[nameof(Opportunity.OpportunityResultTypeId)];
                        if (Opportunity.Errors.ContainsKey(nameof(Opportunity.CreatorId)))
                            Error += Opportunity.Errors[nameof(Opportunity.CreatorId)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }

        [Route(OpportunityRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] Opportunity_OpportunityFilterDTO Opportunity_OpportunityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region Opportunity
                var OpportunityFilter = ConvertFilterDTOToFilterEntity(Opportunity_OpportunityFilterDTO);
                OpportunityFilter.Skip = 0;
                OpportunityFilter.Take = int.MaxValue;
                OpportunityFilter = await OpportunityService.ToFilter(OpportunityFilter);
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
                        CustomerLead.AppUserId,
                        CustomerLead.CreatorId,
                        CustomerLead.ZipCode,
                        CustomerLead.CurrencyId,
                        CustomerLead.RowId,
                    });
                }
                excel.GenerateWorksheet("CustomerLead", CustomerLeadHeaders, CustomerLeadData);
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
                #region OpportunityResultType
                var OpportunityResultTypeFilter = new OpportunityResultTypeFilter();
                OpportunityResultTypeFilter.Selects = OpportunityResultTypeSelect.ALL;
                OpportunityResultTypeFilter.OrderBy = OpportunityResultTypeOrder.Id;
                OpportunityResultTypeFilter.OrderType = OrderType.ASC;
                OpportunityResultTypeFilter.Skip = 0;
                OpportunityResultTypeFilter.Take = int.MaxValue;
                List<OpportunityResultType> OpportunityResultTypes = await OpportunityResultTypeService.List(OpportunityResultTypeFilter);

                var OpportunityResultTypeHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> OpportunityResultTypeData = new List<object[]>();
                for (int i = 0; i < OpportunityResultTypes.Count; i++)
                {
                    var OpportunityResultType = OpportunityResultTypes[i];
                    OpportunityResultTypeData.Add(new Object[]
                    {
                        OpportunityResultType.Id,
                        OpportunityResultType.Code,
                        OpportunityResultType.Name,
                    });
                }
                excel.GenerateWorksheet("OpportunityResultType", OpportunityResultTypeHeaders, OpportunityResultTypeData);
                #endregion
                #region PotentialResult
                var PotentialResultFilter = new PotentialResultFilter();
                PotentialResultFilter.Selects = PotentialResultSelect.ALL;
                PotentialResultFilter.OrderBy = PotentialResultOrder.Id;
                PotentialResultFilter.OrderType = OrderType.ASC;
                PotentialResultFilter.Skip = 0;
                PotentialResultFilter.Take = int.MaxValue;
                List<PotentialResult> PotentialResults = await PotentialResultService.List(PotentialResultFilter);

                var PotentialResultHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> PotentialResultData = new List<object[]>();
                for (int i = 0; i < PotentialResults.Count; i++)
                {
                    var PotentialResult = PotentialResults[i];
                    PotentialResultData.Add(new Object[]
                    {
                        PotentialResult.Id,
                        PotentialResult.Code,
                        PotentialResult.Name,
                    });
                }
                excel.GenerateWorksheet("PotentialResult", PotentialResultHeaders, PotentialResultData);
                #endregion
                #region Probability
                var ProbabilityFilter = new ProbabilityFilter();
                ProbabilityFilter.Selects = ProbabilitySelect.ALL;
                ProbabilityFilter.OrderBy = ProbabilityOrder.Id;
                ProbabilityFilter.OrderType = OrderType.ASC;
                ProbabilityFilter.Skip = 0;
                ProbabilityFilter.Take = int.MaxValue;
                List<Probability> Probabilities = await ProbabilityService.List(ProbabilityFilter);

                var ProbabilityHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> ProbabilityData = new List<object[]>();
                for (int i = 0; i < Probabilities.Count; i++)
                {
                    var Probability = Probabilities[i];
                    ProbabilityData.Add(new Object[]
                    {
                        Probability.Id,
                        Probability.Code,
                        Probability.Name,
                    });
                }
                excel.GenerateWorksheet("Probability", ProbabilityHeaders, ProbabilityData);
                #endregion
                #region SaleStage
                var SaleStageFilter = new SaleStageFilter();
                SaleStageFilter.Selects = SaleStageSelect.ALL;
                SaleStageFilter.OrderBy = SaleStageOrder.Id;
                SaleStageFilter.OrderType = OrderType.ASC;
                SaleStageFilter.Skip = 0;
                SaleStageFilter.Take = int.MaxValue;
                List<SaleStage> SaleStages = await SaleStageService.List(SaleStageFilter);

                var SaleStageHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> SaleStageData = new List<object[]>();
                for (int i = 0; i < SaleStages.Count; i++)
                {
                    var SaleStage = SaleStages[i];
                    SaleStageData.Add(new Object[]
                    {
                        SaleStage.Id,
                        SaleStage.Code,
                        SaleStage.Name,
                    });
                }
                excel.GenerateWorksheet("SaleStage", SaleStageHeaders, SaleStageData);
                #endregion
                #region OpportunityActivity
                var OpportunityActivityFilter = new OpportunityActivityFilter();
                OpportunityActivityFilter.Selects = OpportunityActivitySelect.ALL;
                OpportunityActivityFilter.OrderBy = OpportunityActivityOrder.Id;
                OpportunityActivityFilter.OrderType = OrderType.ASC;
                OpportunityActivityFilter.Skip = 0;
                OpportunityActivityFilter.Take = int.MaxValue;
                List<OpportunityActivity> OpportunityActivities = await OpportunityActivityService.List(OpportunityActivityFilter);

                var OpportunityActivityHeaders = new List<string[]>()
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
                        "OpportunityId",
                        "AppUserId",
                        "ActivityStatusId",
                    }
                };
                List<object[]> OpportunityActivityData = new List<object[]>();
                for (int i = 0; i < OpportunityActivities.Count; i++)
                {
                    var OpportunityActivity = OpportunityActivities[i];
                    OpportunityActivityData.Add(new Object[]
                    {
                        OpportunityActivity.Id,
                        OpportunityActivity.Title,
                        OpportunityActivity.FromDate,
                        OpportunityActivity.ToDate,
                        OpportunityActivity.ActivityTypeId,
                        OpportunityActivity.ActivityPriorityId,
                        OpportunityActivity.Description,
                        OpportunityActivity.Address,
                        OpportunityActivity.OpportunityId,
                        OpportunityActivity.AppUserId,
                        OpportunityActivity.ActivityStatusId,
                    });
                }
                excel.GenerateWorksheet("OpportunityActivity", OpportunityActivityHeaders, OpportunityActivityData);
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
                        CallLog.EntityId,
                        CallLog.CallTypeId,
                        CallLog.CallEmotionId,
                        CallLog.AppUserId,
                        CallLog.CreatorId,
                    });
                }
                excel.GenerateWorksheet("CallLog", CallLogHeaders, CallLogData);
                #endregion
                #region Contact
                var ContactFilter = new ContactFilter();
                ContactFilter.Selects = ContactSelect.ALL;
                ContactFilter.OrderBy = ContactOrder.Id;
                ContactFilter.OrderType = OrderType.ASC;
                ContactFilter.Skip = 0;
                ContactFilter.Take = int.MaxValue;
                List<Contact> Contacts = await ContactService.List(ContactFilter);

                var ContactHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Name",
                        "ProfessionId",
                        "CompanyId",
                        "ContactStatusId",
                        "Address",
                        "NationId",
                        "ProvinceId",
                        "DistrictId",
                        "CustomerLeadId",
                        "ImageId",
                        "Description",
                        "EmailOther",
                        "DateOfBirth",
                        "Phone",
                        "PhoneHome",
                        "FAX",
                        "Email",
                        "Department",
                        "ZIPCode",
                        "SexId",
                        "AppUserId",
                        "RefuseReciveEmail",
                        "RefuseReciveSMS",
                        "PositionId",
                        "CreatorId",
                    }
                };
                List<object[]> ContactData = new List<object[]>();
                for (int i = 0; i < Contacts.Count; i++)
                {
                    var Contact = Contacts[i];
                    ContactData.Add(new Object[]
                    {
                        Contact.Id,
                        Contact.Name,
                        Contact.ProfessionId,
                        Contact.CompanyId,
                        Contact.ContactStatusId,
                        Contact.Address,
                        Contact.NationId,
                        Contact.ProvinceId,
                        Contact.DistrictId,
                        Contact.CustomerLeadId,
                        Contact.ImageId,
                        Contact.Description,
                        Contact.EmailOther,
                        Contact.DateOfBirth,
                        Contact.Phone,
                        Contact.PhoneHome,
                        Contact.FAX,
                        Contact.Email,
                        Contact.Department,
                        Contact.ZIPCode,
                        Contact.SexId,
                        Contact.AppUserId,
                        Contact.RefuseReciveEmail,
                        Contact.RefuseReciveSMS,
                        Contact.PositionId,
                        Contact.CreatorId,
                    });
                }
                excel.GenerateWorksheet("Contact", ContactHeaders, ContactData);
                #endregion
                #region OpportunityEmail
                var OpportunityEmailFilter = new OpportunityEmailFilter();
                OpportunityEmailFilter.Selects = OpportunityEmailSelect.ALL;
                OpportunityEmailFilter.OrderBy = OpportunityEmailOrder.Id;
                OpportunityEmailFilter.OrderType = OrderType.ASC;
                OpportunityEmailFilter.Skip = 0;
                OpportunityEmailFilter.Take = int.MaxValue;
                List<OpportunityEmail> OpportunityEmails = await OpportunityEmailService.List(OpportunityEmailFilter);

                var OpportunityEmailHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Title",
                        "Content",
                        "Reciepient",
                        "OpportunityId",
                        "CreatorId",
                        "EmailStatusId",
                    }
                };
                List<object[]> OpportunityEmailData = new List<object[]>();
                for (int i = 0; i < OpportunityEmails.Count; i++)
                {
                    var OpportunityEmail = OpportunityEmails[i];
                    OpportunityEmailData.Add(new Object[]
                    {
                        OpportunityEmail.Id,
                        OpportunityEmail.Title,
                        OpportunityEmail.Content,
                        OpportunityEmail.Reciepient,
                        OpportunityEmail.OpportunityId,
                        OpportunityEmail.CreatorId,
                        OpportunityEmail.EmailStatusId,
                    });
                }
                excel.GenerateWorksheet("OpportunityEmail", OpportunityEmailHeaders, OpportunityEmailData);
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
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "Opportunity.xlsx");
        }

        [Route(OpportunityRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] Opportunity_OpportunityFilterDTO Opportunity_OpportunityFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            string path = "Templates/Opportunity_Template.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "Opportunity.xlsx");
        }

        [HttpPost]
        [Route(OpportunityRoute.UploadFile)]
        public async Task<ActionResult<Opportunity_FileDTO>> UploadFile(IFormFile file)
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
            File = await OpportunityService.UploadFile(File);
            if (File == null)
                return BadRequest();
            Opportunity_FileDTO Opportunity_FileDTO = new Opportunity_FileDTO
            {
                Id = File.Id,
                Name = File.Name,
                Url = File.Url,
                AppUserId = File.AppUserId
            };
            return Ok(Opportunity_FileDTO);
        }

        private async Task<bool> HasPermission(long Id)
        {
            OpportunityFilter OpportunityFilter = new OpportunityFilter();
            OpportunityFilter = await OpportunityService.ToFilter(OpportunityFilter);
            if (Id == 0)
            {

            }
            else
            {
                OpportunityFilter.Id = new IdFilter { Equal = Id };
                int count = await OpportunityService.Count(OpportunityFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private Opportunity ConvertDTOToEntity(Opportunity_OpportunityDTO Opportunity_OpportunityDTO)
        {
            Opportunity Opportunity = new Opportunity();
            Opportunity.Id = Opportunity_OpportunityDTO.Id;
            Opportunity.Name = Opportunity_OpportunityDTO.Name;
            Opportunity.CompanyId = Opportunity_OpportunityDTO.CompanyId;
            Opportunity.CustomerLeadId = Opportunity_OpportunityDTO.CustomerLeadId;
            Opportunity.ClosingDate = Opportunity_OpportunityDTO.ClosingDate;
            Opportunity.SaleStageId = Opportunity_OpportunityDTO.SaleStageId;
            Opportunity.ProbabilityId = Opportunity_OpportunityDTO.ProbabilityId;
            Opportunity.PotentialResultId = Opportunity_OpportunityDTO.PotentialResultId;
            Opportunity.LeadSourceId = Opportunity_OpportunityDTO.LeadSourceId;
            Opportunity.AppUserId = Opportunity_OpportunityDTO.AppUserId;
            Opportunity.CurrencyId = Opportunity_OpportunityDTO.CurrencyId;
            Opportunity.Amount = Opportunity_OpportunityDTO.Amount;
            Opportunity.ForecastAmount = Opportunity_OpportunityDTO.ForecastAmount;
            Opportunity.Description = Opportunity_OpportunityDTO.Description;
            Opportunity.RefuseReciveSMS = Opportunity_OpportunityDTO.RefuseReciveSMS;
            Opportunity.RefuseReciveEmail = Opportunity_OpportunityDTO.RefuseReciveEmail;
            Opportunity.OpportunityResultTypeId = Opportunity_OpportunityDTO.OpportunityResultTypeId;
            Opportunity.CreatorId = Opportunity_OpportunityDTO.CreatorId;
            Opportunity.AppUser = Opportunity_OpportunityDTO.AppUser == null ? null : new AppUser
            {
                Id = Opportunity_OpportunityDTO.AppUser.Id,
                Username = Opportunity_OpportunityDTO.AppUser.Username,
                DisplayName = Opportunity_OpportunityDTO.AppUser.DisplayName,
                Address = Opportunity_OpportunityDTO.AppUser.Address,
                Email = Opportunity_OpportunityDTO.AppUser.Email,
                Phone = Opportunity_OpportunityDTO.AppUser.Phone,
                SexId = Opportunity_OpportunityDTO.AppUser.SexId,
                Birthday = Opportunity_OpportunityDTO.AppUser.Birthday,
                Avatar = Opportunity_OpportunityDTO.AppUser.Avatar,
                Department = Opportunity_OpportunityDTO.AppUser.Department,
                OrganizationId = Opportunity_OpportunityDTO.AppUser.OrganizationId,
                Longitude = Opportunity_OpportunityDTO.AppUser.Longitude,
                Latitude = Opportunity_OpportunityDTO.AppUser.Latitude,
                StatusId = Opportunity_OpportunityDTO.AppUser.StatusId,
            };
            Opportunity.Company = Opportunity_OpportunityDTO.Company == null ? null : new Company
            {
                Id = Opportunity_OpportunityDTO.Company.Id,
                Name = Opportunity_OpportunityDTO.Company.Name,
                Phone = Opportunity_OpportunityDTO.Company.Phone,
                FAX = Opportunity_OpportunityDTO.Company.FAX,
                PhoneOther = Opportunity_OpportunityDTO.Company.PhoneOther,
                Email = Opportunity_OpportunityDTO.Company.Email,
                EmailOther = Opportunity_OpportunityDTO.Company.EmailOther,
                ZIPCode = Opportunity_OpportunityDTO.Company.ZIPCode,
                Revenue = Opportunity_OpportunityDTO.Company.Revenue,
                Website = Opportunity_OpportunityDTO.Company.Website,
                Address = Opportunity_OpportunityDTO.Company.Address,
                NationId = Opportunity_OpportunityDTO.Company.NationId,
                ProvinceId = Opportunity_OpportunityDTO.Company.ProvinceId,
                DistrictId = Opportunity_OpportunityDTO.Company.DistrictId,
                NumberOfEmployee = Opportunity_OpportunityDTO.Company.NumberOfEmployee,
                RefuseReciveEmail = Opportunity_OpportunityDTO.Company.RefuseReciveEmail,
                RefuseReciveSMS = Opportunity_OpportunityDTO.Company.RefuseReciveSMS,
                CustomerLeadId = Opportunity_OpportunityDTO.Company.CustomerLeadId,
                ParentId = Opportunity_OpportunityDTO.Company.ParentId,
                Path = Opportunity_OpportunityDTO.Company.Path,
                Level = Opportunity_OpportunityDTO.Company.Level,
                ProfessionId = Opportunity_OpportunityDTO.Company.ProfessionId,
                AppUserId = Opportunity_OpportunityDTO.Company.AppUserId,
                CurrencyId = Opportunity_OpportunityDTO.Company.CurrencyId,
                CompanyStatusId = Opportunity_OpportunityDTO.Company.CompanyStatusId,
                Description = Opportunity_OpportunityDTO.Company.Description,
                RowId = Opportunity_OpportunityDTO.Company.RowId,
            };
            Opportunity.Currency = Opportunity_OpportunityDTO.Currency == null ? null : new Currency
            {
                Id = Opportunity_OpportunityDTO.Currency.Id,
                Code = Opportunity_OpportunityDTO.Currency.Code,
                Name = Opportunity_OpportunityDTO.Currency.Name,
            };
            Opportunity.CustomerLead = Opportunity_OpportunityDTO.CustomerLead == null ? null : new CustomerLead
            {
                Id = Opportunity_OpportunityDTO.CustomerLead.Id,
                Name = Opportunity_OpportunityDTO.CustomerLead.Name,
                TelePhone = Opportunity_OpportunityDTO.CustomerLead.TelePhone,
                Phone = Opportunity_OpportunityDTO.CustomerLead.Phone,
                Fax = Opportunity_OpportunityDTO.CustomerLead.Fax,
                Email = Opportunity_OpportunityDTO.CustomerLead.Email,
                SecondEmail = Opportunity_OpportunityDTO.CustomerLead.SecondEmail,
                Website = Opportunity_OpportunityDTO.CustomerLead.Website,
                CustomerLeadSourceId = Opportunity_OpportunityDTO.CustomerLead.CustomerLeadSourceId,
                CustomerLeadLevelId = Opportunity_OpportunityDTO.CustomerLead.CustomerLeadLevelId,
                CompanyId = Opportunity_OpportunityDTO.CustomerLead.CompanyId,
                CampaignId = Opportunity_OpportunityDTO.CustomerLead.CampaignId,
                ProfessionId = Opportunity_OpportunityDTO.CustomerLead.ProfessionId,
                Revenue = Opportunity_OpportunityDTO.CustomerLead.Revenue,
                EmployeeQuantity = Opportunity_OpportunityDTO.CustomerLead.EmployeeQuantity,
                Address = Opportunity_OpportunityDTO.CustomerLead.Address,
                NationId = Opportunity_OpportunityDTO.CustomerLead.NationId,
                ProvinceId = Opportunity_OpportunityDTO.CustomerLead.ProvinceId,
                DistrictId = Opportunity_OpportunityDTO.CustomerLead.DistrictId,
                CustomerLeadStatusId = Opportunity_OpportunityDTO.CustomerLead.CustomerLeadStatusId,
                BusinessRegistrationCode = Opportunity_OpportunityDTO.CustomerLead.BusinessRegistrationCode,
                SexId = Opportunity_OpportunityDTO.CustomerLead.SexId,
                RefuseReciveSMS = Opportunity_OpportunityDTO.CustomerLead.RefuseReciveSMS,
                RefuseReciveEmail = Opportunity_OpportunityDTO.CustomerLead.RefuseReciveEmail,
                Description = Opportunity_OpportunityDTO.CustomerLead.Description,
                ZipCode = Opportunity_OpportunityDTO.CustomerLead.ZipCode,
            };
            Opportunity.LeadSource = Opportunity_OpportunityDTO.LeadSource == null ? null : new CustomerLeadSource
            {
                Id = Opportunity_OpportunityDTO.LeadSource.Id,
                Code = Opportunity_OpportunityDTO.LeadSource.Code,
                Name = Opportunity_OpportunityDTO.LeadSource.Name,
            };
            Opportunity.OpportunityResultType = Opportunity_OpportunityDTO.OpportunityResultType == null ? null : new OpportunityResultType
            {
                Id = Opportunity_OpportunityDTO.OpportunityResultType.Id,
                Code = Opportunity_OpportunityDTO.OpportunityResultType.Code,
                Name = Opportunity_OpportunityDTO.OpportunityResultType.Name,
            };
            Opportunity.PotentialResult = Opportunity_OpportunityDTO.PotentialResult == null ? null : new PotentialResult
            {
                Id = Opportunity_OpportunityDTO.PotentialResult.Id,
                Code = Opportunity_OpportunityDTO.PotentialResult.Code,
                Name = Opportunity_OpportunityDTO.PotentialResult.Name,
            };
            Opportunity.Probability = Opportunity_OpportunityDTO.Probability == null ? null : new Probability
            {
                Id = Opportunity_OpportunityDTO.Probability.Id,
                Code = Opportunity_OpportunityDTO.Probability.Code,
                Name = Opportunity_OpportunityDTO.Probability.Name,
            };
            Opportunity.SaleStage = Opportunity_OpportunityDTO.SaleStage == null ? null : new SaleStage
            {
                Id = Opportunity_OpportunityDTO.SaleStage.Id,
                Code = Opportunity_OpportunityDTO.SaleStage.Code,
                Name = Opportunity_OpportunityDTO.SaleStage.Name,
            };
            Opportunity.OpportunityActivities = Opportunity_OpportunityDTO.OpportunityActivities?
                .Select(x => new OpportunityActivity
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
                    },
                }).ToList();
            Opportunity.OpportunityCallLogMappings = Opportunity_OpportunityDTO.OpportunityCallLogMappings?
                .Select(x => new OpportunityCallLogMapping
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
                        EntityId = x.CallLog.EntityId,
                        CallTypeId = x.CallLog.CallTypeId,
                        CallEmotionId = x.CallLog.CallEmotionId,
                        AppUserId = x.CallLog.AppUserId,
                    },
                }).ToList();
            Opportunity.OpportunityContactMappings = Opportunity_OpportunityDTO.OpportunityContactMappings?
                .Select(x => new OpportunityContactMapping
                {
                    ContactId = x.ContactId,
                    Contact = x.Contact == null ? null : new Contact
                    {
                        Id = x.Contact.Id,
                        Name = x.Contact.Name,
                        ProfessionId = x.Contact.ProfessionId,
                        CompanyId = x.Contact.CompanyId,
                        ContactStatusId = x.Contact.ContactStatusId,
                        Address = x.Contact.Address,
                        NationId = x.Contact.NationId,
                        ProvinceId = x.Contact.ProvinceId,
                        DistrictId = x.Contact.DistrictId,
                        CustomerLeadId = x.Contact.CustomerLeadId,
                        ImageId = x.Contact.ImageId,
                        Description = x.Contact.Description,
                        EmailOther = x.Contact.EmailOther,
                        DateOfBirth = x.Contact.DateOfBirth,
                        Phone = x.Contact.Phone,
                        PhoneHome = x.Contact.PhoneHome,
                        FAX = x.Contact.FAX,
                        Email = x.Contact.Email,
                        Department = x.Contact.Department,
                        ZIPCode = x.Contact.ZIPCode,
                        SexId = x.Contact.SexId,
                        AppUserId = x.Contact.AppUserId,
                        RefuseReciveEmail = x.Contact.RefuseReciveEmail,
                        RefuseReciveSMS = x.Contact.RefuseReciveSMS,
                        PositionId = x.Contact.PositionId,
                    },
                }).ToList();
            Opportunity.OpportunityEmails = Opportunity_OpportunityDTO.OpportunityEmails?
                .Select(x => new OpportunityEmail
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
                    },
                    EmailStatus = x.EmailStatus == null ? null : new EmailStatus
                    {
                        Id = x.EmailStatus.Id,
                        Code = x.EmailStatus.Code,
                        Name = x.EmailStatus.Name,
                    },
                }).ToList();
            Opportunity.OpportunityFileGroupings = Opportunity_OpportunityDTO.OpportunityFileGroupings?
                .Select(x => new OpportunityFileGrouping
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CreatorId = x.CreatorId,
                    FileTypeId = x.FileTypeId,
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
                    },
                    FileType = x.FileType == null ? null : new FileType
                    {
                        Id = x.FileType.Id,
                        Code = x.FileType.Code,
                        Name = x.FileType.Name,
                    },
                }).ToList();
            Opportunity.OpportunityItemMappings = Opportunity_OpportunityDTO.OpportunityItemMappings?
                .Select(x => new OpportunityItemMapping
                {
                    ItemId = x.ItemId,
                    UnitOfMeasureId = x.UnitOfMeasureId,
                    Quantity = x.Quantity,
                    RequestQuantity = x.RequestQuantity,
                    PrimaryPrice = x.PrimaryPrice,
                    SalePrice = x.SalePrice,
                    DiscountPercentage = x.DiscountPercentage,
                    Discount = x.Discount,
                    VAT = x.VAT,
                    VATOther = x.VATOther,
                    Amount = x.Amount,
                    Factor = x.Factor,
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
                    },
                    UnitOfMeasure = x.UnitOfMeasure == null ? null : new UnitOfMeasure
                    {
                        Id = x.UnitOfMeasure.Id,
                        Code = x.UnitOfMeasure.Code,
                        Name = x.UnitOfMeasure.Name,
                        Description = x.UnitOfMeasure.Description,
                        StatusId = x.UnitOfMeasure.StatusId,
                        Used = x.UnitOfMeasure.Used,
                    },
                }).ToList();
            Opportunity.BaseLanguage = CurrentContext.Language;
            return Opportunity;
        }

        private OpportunityFilter ConvertFilterDTOToFilterEntity(Opportunity_OpportunityFilterDTO Opportunity_OpportunityFilterDTO)
        {
            OpportunityFilter OpportunityFilter = new OpportunityFilter();
            OpportunityFilter.Selects = OpportunitySelect.ALL;
            OpportunityFilter.Skip = Opportunity_OpportunityFilterDTO.Skip;
            OpportunityFilter.Take = Opportunity_OpportunityFilterDTO.Take;
            OpportunityFilter.OrderBy = Opportunity_OpportunityFilterDTO.OrderBy;
            OpportunityFilter.OrderType = Opportunity_OpportunityFilterDTO.OrderType;

            OpportunityFilter.Id = Opportunity_OpportunityFilterDTO.Id;
            OpportunityFilter.Name = Opportunity_OpportunityFilterDTO.Name;
            OpportunityFilter.CompanyId = Opportunity_OpportunityFilterDTO.CompanyId;
            OpportunityFilter.CustomerLeadId = Opportunity_OpportunityFilterDTO.CustomerLeadId;
            OpportunityFilter.ClosingDate = Opportunity_OpportunityFilterDTO.ClosingDate;
            OpportunityFilter.SaleStageId = Opportunity_OpportunityFilterDTO.SaleStageId;
            OpportunityFilter.ProbabilityId = Opportunity_OpportunityFilterDTO.ProbabilityId;
            OpportunityFilter.PotentialResultId = Opportunity_OpportunityFilterDTO.PotentialResultId;
            OpportunityFilter.LeadSourceId = Opportunity_OpportunityFilterDTO.LeadSourceId;
            OpportunityFilter.AppUserId = Opportunity_OpportunityFilterDTO.AppUserId;
            OpportunityFilter.CurrencyId = Opportunity_OpportunityFilterDTO.CurrencyId;
            OpportunityFilter.Amount = Opportunity_OpportunityFilterDTO.Amount;
            OpportunityFilter.ForecastAmount = Opportunity_OpportunityFilterDTO.ForecastAmount;
            OpportunityFilter.Description = Opportunity_OpportunityFilterDTO.Description;
            OpportunityFilter.OpportunityResultTypeId = Opportunity_OpportunityFilterDTO.OpportunityResultTypeId;
            OpportunityFilter.CreatorId = Opportunity_OpportunityFilterDTO.CreatorId;
            OpportunityFilter.CreatedAt = Opportunity_OpportunityFilterDTO.CreatedAt;
            OpportunityFilter.UpdatedAt = Opportunity_OpportunityFilterDTO.UpdatedAt;
            return OpportunityFilter;
        }

        #region activity
        [Route(OpportunityRoute.CreateActivity), HttpPost]
        public async Task<ActionResult<Opportunity_OpportunityActivityDTO>> CreateActivity([FromBody] Opportunity_OpportunityActivityDTO Opportunity_OpportunityActivityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityActivity OpportunityActivity = ConvertDTOToEntity(Opportunity_OpportunityActivityDTO);
            OpportunityActivity = await OpportunityActivityService.Create(OpportunityActivity);
            Opportunity_OpportunityActivityDTO = new Opportunity_OpportunityActivityDTO(OpportunityActivity);
            if (OpportunityActivity.IsValidated)
                return Opportunity_OpportunityActivityDTO;
            else
                return BadRequest(Opportunity_OpportunityActivityDTO);
        }

        [Route(OpportunityRoute.UpdateActivity), HttpPost]
        public async Task<ActionResult<Opportunity_OpportunityActivityDTO>> UpdateActivity([FromBody] Opportunity_OpportunityActivityDTO Opportunity_OpportunityActivityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityActivity OpportunityActivity = ConvertDTOToEntity(Opportunity_OpportunityActivityDTO);
            OpportunityActivity = await OpportunityActivityService.Update(OpportunityActivity);
            Opportunity_OpportunityActivityDTO = new Opportunity_OpportunityActivityDTO(OpportunityActivity);
            if (OpportunityActivity.IsValidated)
                return Opportunity_OpportunityActivityDTO;
            else
                return BadRequest(Opportunity_OpportunityActivityDTO);
        }

        [Route(OpportunityRoute.DeleteActivity), HttpPost]
        public async Task<ActionResult<Opportunity_OpportunityActivityDTO>> DeleteActivity([FromBody] Opportunity_OpportunityActivityDTO Opportunity_OpportunityActivityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityActivity OpportunityActivity = ConvertDTOToEntity(Opportunity_OpportunityActivityDTO);
            OpportunityActivity = await OpportunityActivityService.Delete(OpportunityActivity);
            Opportunity_OpportunityActivityDTO = new Opportunity_OpportunityActivityDTO(OpportunityActivity);
            if (OpportunityActivity.IsValidated)
                return Opportunity_OpportunityActivityDTO;
            else
                return BadRequest(Opportunity_OpportunityActivityDTO);
        }

        [Route(OpportunityRoute.BulkDeleteActivity), HttpPost]
        public async Task<ActionResult<bool>> BulkDeleteActivity([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityActivityFilter OpportunityActivityFilter = new OpportunityActivityFilter();
            OpportunityActivityFilter = await OpportunityActivityService.ToFilter(OpportunityActivityFilter);
            OpportunityActivityFilter.Id = new IdFilter { In = Ids };
            OpportunityActivityFilter.Selects = OpportunityActivitySelect.Id;
            OpportunityActivityFilter.Skip = 0;
            OpportunityActivityFilter.Take = int.MaxValue;

            List<OpportunityActivity> OpportunityActivities = await OpportunityActivityService.List(OpportunityActivityFilter);
            OpportunityActivities = await OpportunityActivityService.BulkDelete(OpportunityActivities);
            if (OpportunityActivities.Any(x => !x.IsValidated))
                return BadRequest(OpportunityActivities.Where(x => !x.IsValidated));
            return true;
        }

        private OpportunityActivity ConvertDTOToEntity(Opportunity_OpportunityActivityDTO Opportunity_OpportunityActivityDTO)
        {
            OpportunityActivity OpportunityActivity = new OpportunityActivity();
            OpportunityActivity.Id = Opportunity_OpportunityActivityDTO.Id;
            OpportunityActivity.Title = Opportunity_OpportunityActivityDTO.Title;
            OpportunityActivity.FromDate = Opportunity_OpportunityActivityDTO.FromDate;
            OpportunityActivity.ToDate = Opportunity_OpportunityActivityDTO.ToDate;
            OpportunityActivity.ActivityTypeId = Opportunity_OpportunityActivityDTO.ActivityTypeId;
            OpportunityActivity.ActivityPriorityId = Opportunity_OpportunityActivityDTO.ActivityPriorityId;
            OpportunityActivity.Description = Opportunity_OpportunityActivityDTO.Description;
            OpportunityActivity.Address = Opportunity_OpportunityActivityDTO.Address;
            OpportunityActivity.OpportunityId = Opportunity_OpportunityActivityDTO.OpportunityId;
            OpportunityActivity.AppUserId = Opportunity_OpportunityActivityDTO.AppUserId;
            OpportunityActivity.ActivityStatusId = Opportunity_OpportunityActivityDTO.ActivityStatusId;
            OpportunityActivity.ActivityPriority = Opportunity_OpportunityActivityDTO.ActivityPriority == null ? null : new ActivityPriority
            {
                Id = Opportunity_OpportunityActivityDTO.ActivityPriority.Id,
                Code = Opportunity_OpportunityActivityDTO.ActivityPriority.Code,
                Name = Opportunity_OpportunityActivityDTO.ActivityPriority.Name,
            };
            OpportunityActivity.ActivityStatus = Opportunity_OpportunityActivityDTO.ActivityStatus == null ? null : new ActivityStatus
            {
                Id = Opportunity_OpportunityActivityDTO.ActivityStatus.Id,
                Code = Opportunity_OpportunityActivityDTO.ActivityStatus.Code,
                Name = Opportunity_OpportunityActivityDTO.ActivityStatus.Name,
            };
            OpportunityActivity.ActivityType = Opportunity_OpportunityActivityDTO.ActivityType == null ? null : new ActivityType
            {
                Id = Opportunity_OpportunityActivityDTO.ActivityType.Id,
                Code = Opportunity_OpportunityActivityDTO.ActivityType.Code,
                Name = Opportunity_OpportunityActivityDTO.ActivityType.Name,
            };
            OpportunityActivity.AppUser = Opportunity_OpportunityActivityDTO.AppUser == null ? null : new AppUser
            {
                Id = Opportunity_OpportunityActivityDTO.AppUser.Id,
                Username = Opportunity_OpportunityActivityDTO.AppUser.Username,
                DisplayName = Opportunity_OpportunityActivityDTO.AppUser.DisplayName,
                Address = Opportunity_OpportunityActivityDTO.AppUser.Address,
                Email = Opportunity_OpportunityActivityDTO.AppUser.Email,
                Phone = Opportunity_OpportunityActivityDTO.AppUser.Phone,
                SexId = Opportunity_OpportunityActivityDTO.AppUser.SexId,
                Birthday = Opportunity_OpportunityActivityDTO.AppUser.Birthday,
                Department = Opportunity_OpportunityActivityDTO.AppUser.Department,
                OrganizationId = Opportunity_OpportunityActivityDTO.AppUser.OrganizationId,
                StatusId = Opportunity_OpportunityActivityDTO.AppUser.StatusId,
            };
            OpportunityActivity.BaseLanguage = CurrentContext.Language;
            return OpportunityActivity;
        }
        #endregion

        #region CallLog
        [Route(OpportunityRoute.DeleteCallLog), HttpPost]
        public async Task<ActionResult<Opportunity_CallLogDTO>> DeleteCallLog([FromBody] Opportunity_CallLogDTO Opportunity_CallLogDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CallLog CallLog = ConvertCallLog(Opportunity_CallLogDTO);
            CallLog = await CallLogService.Delete(CallLog);
            Opportunity_CallLogDTO = new Opportunity_CallLogDTO(CallLog);
            if (CallLog.IsValidated)
                return Opportunity_CallLogDTO;
            else
                return BadRequest(Opportunity_CallLogDTO);
        }

        private CallLog ConvertCallLog(Opportunity_CallLogDTO Opportunity_CallLogDTO)
        {
            CallLog CallLog = new CallLog();
            CallLog.Id = Opportunity_CallLogDTO.Id;
            CallLog.EntityReferenceId = Opportunity_CallLogDTO.EntityReferenceId;
            CallLog.CallTypeId = Opportunity_CallLogDTO.CallTypeId;
            CallLog.CallEmotionId = Opportunity_CallLogDTO.CallEmotionId;
            CallLog.AppUserId = Opportunity_CallLogDTO.AppUserId;
            CallLog.Title = Opportunity_CallLogDTO.Title;
            CallLog.Content = Opportunity_CallLogDTO.Content;
            CallLog.Phone = Opportunity_CallLogDTO.Phone;
            CallLog.CallTime = Opportunity_CallLogDTO.CallTime;
            CallLog.AppUser = Opportunity_CallLogDTO.AppUser == null ? null : new AppUser
            {
                Id = Opportunity_CallLogDTO.AppUser.Id,
                Username = Opportunity_CallLogDTO.AppUser.Username,
                DisplayName = Opportunity_CallLogDTO.AppUser.DisplayName,
                Address = Opportunity_CallLogDTO.AppUser.Address,
                Email = Opportunity_CallLogDTO.AppUser.Email,
                Phone = Opportunity_CallLogDTO.AppUser.Phone,
                SexId = Opportunity_CallLogDTO.AppUser.SexId,
                Birthday = Opportunity_CallLogDTO.AppUser.Birthday,
                Department = Opportunity_CallLogDTO.AppUser.Department,
                OrganizationId = Opportunity_CallLogDTO.AppUser.OrganizationId,
                StatusId = Opportunity_CallLogDTO.AppUser.StatusId,
            };
            CallLog.EntityReference = Opportunity_CallLogDTO.EntityReference == null ? null : new EntityReference
            {
                Id = Opportunity_CallLogDTO.EntityReference.Id,
                Code = Opportunity_CallLogDTO.EntityReference.Code,
                Name = Opportunity_CallLogDTO.EntityReference.Name,
            };
            CallLog.CallType = Opportunity_CallLogDTO.CallType == null ? null : new CallType
            {
                Id = Opportunity_CallLogDTO.CallType.Id,
                Code = Opportunity_CallLogDTO.CallType.Code,
                Name = Opportunity_CallLogDTO.CallType.Name,
                ColorCode = Opportunity_CallLogDTO.CallType.ColorCode,
                StatusId = Opportunity_CallLogDTO.CallType.StatusId,
                Used = Opportunity_CallLogDTO.CallType.Used,
            };
            CallLog.CallEmotion = Opportunity_CallLogDTO.CallEmotion == null ? null : new CallEmotion
            {
                Id = Opportunity_CallLogDTO.CallEmotion.Id,
                Name = Opportunity_CallLogDTO.CallEmotion.Name,
                Code = Opportunity_CallLogDTO.CallEmotion.Code,
                StatusId = Opportunity_CallLogDTO.CallEmotion.StatusId,
                Description = Opportunity_CallLogDTO.CallEmotion.Description,
            };
            CallLog.BaseLanguage = CurrentContext.Language;
            return CallLog;
        }
        #endregion

        #region Contact
        [Route(OpportunityRoute.CreateContact), HttpPost]
        public async Task<ActionResult<Opportunity_ContactDTO>> CreateContact([FromBody] Opportunity_ContactDTO Opportunity_ContactDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Contact Contact = ConvertContact(Opportunity_ContactDTO);
            Contact = await ContactService.Create(Contact);
            Opportunity_ContactDTO = new Opportunity_ContactDTO(Contact);
            if (Contact.IsValidated)
                return Opportunity_ContactDTO;
            else
                return BadRequest(Opportunity_ContactDTO);
        }

        [Route(OpportunityRoute.UpdateContact), HttpPost]
        public async Task<ActionResult<Opportunity_ContactDTO>> UpdateContact([FromBody] Opportunity_ContactDTO Opportunity_ContactDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Contact Contact = ConvertContact(Opportunity_ContactDTO);
            Contact = await ContactService.Update(Contact);
            Opportunity_ContactDTO = new Opportunity_ContactDTO(Contact);
            if (Contact.IsValidated)
                return Opportunity_ContactDTO;
            else
                return BadRequest(Opportunity_ContactDTO);
        }

        [Route(OpportunityRoute.DeleteContact), HttpPost]
        public async Task<ActionResult<Opportunity_ContactDTO>> DeleteContact([FromBody] Opportunity_ContactDTO Opportunity_ContactDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Contact Contact = ConvertContact(Opportunity_ContactDTO);
            Contact = await ContactService.Delete(Contact);
            Opportunity_ContactDTO = new Opportunity_ContactDTO(Contact);
            if (Contact.IsValidated)
                return Opportunity_ContactDTO;
            else
                return BadRequest(Opportunity_ContactDTO);
        }

        [Route(OpportunityRoute.BulkDeleteContact), HttpPost]
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

        private Contact ConvertContact(Opportunity_ContactDTO Opportunity_ContactDTO)
        {
            Contact Contact = new Contact();
            Contact.Id = Opportunity_ContactDTO.Id;
            Contact.Name = Opportunity_ContactDTO.Name;
            Contact.ProfessionId = Opportunity_ContactDTO.ProfessionId;
            Contact.CompanyId = Opportunity_ContactDTO.CompanyId;
            Contact.ProvinceId = Opportunity_ContactDTO.ProvinceId;
            Contact.DistrictId = Opportunity_ContactDTO.DistrictId;
            Contact.NationId = Opportunity_ContactDTO.NationId;
            Contact.CustomerLeadId = Opportunity_ContactDTO.CustomerLeadId;
            Contact.ImageId = Opportunity_ContactDTO.ImageId;
            Contact.Description = Opportunity_ContactDTO.Description;
            Contact.Address = Opportunity_ContactDTO.Address;
            Contact.EmailOther = Opportunity_ContactDTO.EmailOther;
            Contact.DateOfBirth = Opportunity_ContactDTO.DateOfBirth;
            Contact.Phone = Opportunity_ContactDTO.Phone;
            Contact.PhoneHome = Opportunity_ContactDTO.PhoneHome;
            Contact.FAX = Opportunity_ContactDTO.FAX;
            Contact.Email = Opportunity_ContactDTO.Email;
            Contact.RefuseReciveEmail = Opportunity_ContactDTO.RefuseReciveEmail;
            Contact.RefuseReciveSMS = Opportunity_ContactDTO.RefuseReciveSMS;
            Contact.ZIPCode = Opportunity_ContactDTO.ZIPCode;
            Contact.SexId = Opportunity_ContactDTO.SexId;
            Contact.AppUserId = Opportunity_ContactDTO.AppUserId;
            Contact.PositionId = Opportunity_ContactDTO.PositionId;
            Contact.Department = Opportunity_ContactDTO.Description;
            Contact.ContactStatusId = Opportunity_ContactDTO.ContactStatusId;
            Contact.AppUser = Opportunity_ContactDTO.AppUser == null ? null : new AppUser
            {
                Id = Opportunity_ContactDTO.AppUser.Id,
                Username = Opportunity_ContactDTO.AppUser.Username,
                DisplayName = Opportunity_ContactDTO.AppUser.DisplayName,
                Address = Opportunity_ContactDTO.AppUser.Address,
                Email = Opportunity_ContactDTO.AppUser.Email,
                Phone = Opportunity_ContactDTO.AppUser.Phone,
                SexId = Opportunity_ContactDTO.AppUser.SexId,
                Birthday = Opportunity_ContactDTO.AppUser.Birthday,
                Department = Opportunity_ContactDTO.AppUser.Department,
                OrganizationId = Opportunity_ContactDTO.AppUser.OrganizationId,
                StatusId = Opportunity_ContactDTO.AppUser.StatusId,
            };
            Contact.Company = Opportunity_ContactDTO.Company == null ? null : new Company
            {
                Id = Opportunity_ContactDTO.Company.Id,
                Name = Opportunity_ContactDTO.Company.Name,
                Phone = Opportunity_ContactDTO.Company.Phone,
                FAX = Opportunity_ContactDTO.Company.FAX,
                PhoneOther = Opportunity_ContactDTO.Company.PhoneOther,
                Email = Opportunity_ContactDTO.Company.Email,
                EmailOther = Opportunity_ContactDTO.Company.EmailOther,
                ZIPCode = Opportunity_ContactDTO.Company.ZIPCode,
                Revenue = Opportunity_ContactDTO.Company.Revenue,
                Website = Opportunity_ContactDTO.Company.Website,
                NationId = Opportunity_ContactDTO.Company.NationId,
                ProvinceId = Opportunity_ContactDTO.Company.ProvinceId,
                DistrictId = Opportunity_ContactDTO.Company.DistrictId,
                Address = Opportunity_ContactDTO.Company.Address,
                NumberOfEmployee = Opportunity_ContactDTO.Company.NumberOfEmployee,
                RefuseReciveEmail = Opportunity_ContactDTO.Company.RefuseReciveEmail,
                RefuseReciveSMS = Opportunity_ContactDTO.Company.RefuseReciveSMS,
                CustomerLeadId = Opportunity_ContactDTO.Company.CustomerLeadId,
                ParentId = Opportunity_ContactDTO.Company.ParentId,
                Path = Opportunity_ContactDTO.Company.Path,
                Level = Opportunity_ContactDTO.Company.Level,
                ProfessionId = Opportunity_ContactDTO.Company.ProfessionId,
                AppUserId = Opportunity_ContactDTO.Company.AppUserId,
                CurrencyId = Opportunity_ContactDTO.Company.CurrencyId,
                CompanyStatusId = Opportunity_ContactDTO.Company.CompanyStatusId,
                Description = Opportunity_ContactDTO.Company.Description,
                RowId = Opportunity_ContactDTO.Company.RowId,
            };
            Contact.ContactStatus = Opportunity_ContactDTO.ContactStatus == null ? null : new ContactStatus
            {
                Id = Opportunity_ContactDTO.ContactStatus.Id,
                Code = Opportunity_ContactDTO.ContactStatus.Code,
                Name = Opportunity_ContactDTO.ContactStatus.Name,
            };
            Contact.CustomerLead = Opportunity_ContactDTO.CustomerLead == null ? null : new CustomerLead
            {
                Id = Opportunity_ContactDTO.CustomerLead.Id,
                Name = Opportunity_ContactDTO.CustomerLead.Name,
                TelePhone = Opportunity_ContactDTO.CustomerLead.TelePhone,
                Phone = Opportunity_ContactDTO.CustomerLead.Phone,
                Fax = Opportunity_ContactDTO.CustomerLead.Fax,
                Email = Opportunity_ContactDTO.CustomerLead.Email,
                SecondEmail = Opportunity_ContactDTO.CustomerLead.SecondEmail,
                Website = Opportunity_ContactDTO.CustomerLead.Website,
                CustomerLeadSourceId = Opportunity_ContactDTO.CustomerLead.CustomerLeadSourceId,
                CustomerLeadLevelId = Opportunity_ContactDTO.CustomerLead.CustomerLeadLevelId,
                CompanyId = Opportunity_ContactDTO.CustomerLead.CompanyId,
                CampaignId = Opportunity_ContactDTO.CustomerLead.CampaignId,
                ProfessionId = Opportunity_ContactDTO.CustomerLead.ProfessionId,
                Revenue = Opportunity_ContactDTO.CustomerLead.Revenue,
                EmployeeQuantity = Opportunity_ContactDTO.CustomerLead.EmployeeQuantity,
                Address = Opportunity_ContactDTO.CustomerLead.Address,
                ProvinceId = Opportunity_ContactDTO.CustomerLead.ProvinceId,
                DistrictId = Opportunity_ContactDTO.CustomerLead.DistrictId,
                CustomerLeadStatusId = Opportunity_ContactDTO.CustomerLead.CustomerLeadStatusId,
                BusinessRegistrationCode = Opportunity_ContactDTO.CustomerLead.BusinessRegistrationCode,
                SexId = Opportunity_ContactDTO.CustomerLead.SexId,
                RefuseReciveSMS = Opportunity_ContactDTO.CustomerLead.RefuseReciveSMS,
                NationId = Opportunity_ContactDTO.CustomerLead.NationId,
                RefuseReciveEmail = Opportunity_ContactDTO.CustomerLead.RefuseReciveEmail,
                Description = Opportunity_ContactDTO.CustomerLead.Description,
                ZipCode = Opportunity_ContactDTO.CustomerLead.ZipCode,
            };
            Contact.District = Opportunity_ContactDTO.District == null ? null : new District
            {
                Id = Opportunity_ContactDTO.District.Id,
                Code = Opportunity_ContactDTO.District.Code,
                Name = Opportunity_ContactDTO.District.Name,
                Priority = Opportunity_ContactDTO.District.Priority,
                ProvinceId = Opportunity_ContactDTO.District.ProvinceId,
                StatusId = Opportunity_ContactDTO.District.StatusId,
            };
            Contact.Image = Opportunity_ContactDTO.Image == null ? null : new Image
            {
                Id = Opportunity_ContactDTO.Image.Id,
                Name = Opportunity_ContactDTO.Image.Name,
                Url = Opportunity_ContactDTO.Image.Url,
            };
            Contact.Nation = Opportunity_ContactDTO.Nation == null ? null : new Nation
            {
                Id = Opportunity_ContactDTO.Nation.Id,
                Code = Opportunity_ContactDTO.Nation.Code,
                Name = Opportunity_ContactDTO.Nation.Name,
                StatusId = Opportunity_ContactDTO.Nation.StatusId,
            };
            Contact.Position = Opportunity_ContactDTO.Position == null ? null : new Position
            {
                Id = Opportunity_ContactDTO.Position.Id,
                Code = Opportunity_ContactDTO.Position.Code,
                Name = Opportunity_ContactDTO.Position.Name,
                StatusId = Opportunity_ContactDTO.Position.StatusId,
            };
            Contact.Profession = Opportunity_ContactDTO.Profession == null ? null : new Profession
            {
                Id = Opportunity_ContactDTO.Profession.Id,
                Code = Opportunity_ContactDTO.Profession.Code,
                Name = Opportunity_ContactDTO.Profession.Name,
            };
            Contact.Province = Opportunity_ContactDTO.Province == null ? null : new Province
            {
                Id = Opportunity_ContactDTO.Province.Id,
                Code = Opportunity_ContactDTO.Province.Code,
                Name = Opportunity_ContactDTO.Province.Name,
                Priority = Opportunity_ContactDTO.Province.Priority,
                StatusId = Opportunity_ContactDTO.Province.StatusId,
            };
            Contact.Sex = Opportunity_ContactDTO.Sex == null ? null : new Sex
            {
                Id = Opportunity_ContactDTO.Sex.Id,
                Code = Opportunity_ContactDTO.Sex.Code,
                Name = Opportunity_ContactDTO.Sex.Name,
            };
            Contact.BaseLanguage = CurrentContext.Language;
            return Contact;
        }
        #endregion

        #region OrderQuote
        [Route(OpportunityRoute.CreateOrderQuote), HttpPost]
        public async Task<ActionResult<Opportunity_OrderQuoteDTO>> CreateOrderQuote([FromBody] Opportunity_OrderQuoteDTO Opportunity_OrderQuoteDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuote OrderQuote = ConvertOrderQuote(Opportunity_OrderQuoteDTO);

            OrderQuote.CreatorId = CurrentContext.UserId;
            OrderQuote = await OrderQuoteService.Create(OrderQuote);
            Opportunity_OrderQuoteDTO = new Opportunity_OrderQuoteDTO(OrderQuote);
            if (OrderQuote.IsValidated)
                return Opportunity_OrderQuoteDTO;
            else
                return BadRequest(Opportunity_OrderQuoteDTO);
        }

        [Route(OpportunityRoute.UpdateOrderQuote), HttpPost]
        public async Task<ActionResult<Opportunity_OrderQuoteDTO>> UpdateOrderQuote([FromBody] Opportunity_OrderQuoteDTO Opportunity_OrderQuoteDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuote OrderQuote = ConvertOrderQuote(Opportunity_OrderQuoteDTO);
            OrderQuote = await OrderQuoteService.Update(OrderQuote);
            Opportunity_OrderQuoteDTO = new Opportunity_OrderQuoteDTO(OrderQuote);
            if (OrderQuote.IsValidated)
                return Opportunity_OrderQuoteDTO;
            else
                return BadRequest(Opportunity_OrderQuoteDTO);
        }

        [Route(OpportunityRoute.DeleteOrderQuote), HttpPost]
        public async Task<ActionResult<Opportunity_OrderQuoteDTO>> DeleteOrderQuote([FromBody] Opportunity_OrderQuoteDTO Opportunity_OrderQuoteDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuote OrderQuote = ConvertOrderQuote(Opportunity_OrderQuoteDTO);
            OrderQuote = await OrderQuoteService.Delete(OrderQuote);
            Opportunity_OrderQuoteDTO = new Opportunity_OrderQuoteDTO(OrderQuote);
            if (OrderQuote.IsValidated)
                return Opportunity_OrderQuoteDTO;
            else
                return BadRequest(Opportunity_OrderQuoteDTO);
        }

        [Route(OpportunityRoute.BulkDeleteOrderQuote), HttpPost]
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

        private OrderQuote ConvertOrderQuote(Opportunity_OrderQuoteDTO Opportunity_OrderQuoteDTO)
        {
            OrderQuote OrderQuote = new OrderQuote();
            OrderQuote.Id = Opportunity_OrderQuoteDTO.Id;
            OrderQuote.Subject = Opportunity_OrderQuoteDTO.Subject;
            OrderQuote.NationId = Opportunity_OrderQuoteDTO.NationId;
            OrderQuote.ProvinceId = Opportunity_OrderQuoteDTO.ProvinceId;
            OrderQuote.DistrictId = Opportunity_OrderQuoteDTO.DistrictId;
            OrderQuote.Address = Opportunity_OrderQuoteDTO.Address;
            OrderQuote.InvoiceAddress = Opportunity_OrderQuoteDTO.InvoiceAddress;
            OrderQuote.InvoiceProvinceId = Opportunity_OrderQuoteDTO.InvoiceProvinceId;
            OrderQuote.InvoiceDistrictId = Opportunity_OrderQuoteDTO.InvoiceDistrictId;
            OrderQuote.InvoiceNationId = Opportunity_OrderQuoteDTO.InvoiceNationId;
            OrderQuote.EditedPriceStatusId = Opportunity_OrderQuoteDTO.EditedPriceStatusId;
            OrderQuote.ZIPCode = Opportunity_OrderQuoteDTO.ZIPCode;
            OrderQuote.InvoiceZIPCode = Opportunity_OrderQuoteDTO.InvoiceZIPCode;
            OrderQuote.AppUserId = Opportunity_OrderQuoteDTO.UserId;
            OrderQuote.ContactId = Opportunity_OrderQuoteDTO.ContactId;
            OrderQuote.CompanyId = Opportunity_OrderQuoteDTO.CompanyId;
            OrderQuote.OpportunityId = Opportunity_OrderQuoteDTO.OpportunityId;
            OrderQuote.OrderQuoteStatusId = Opportunity_OrderQuoteDTO.OrderQuoteStatusId;
            OrderQuote.SubTotal = Opportunity_OrderQuoteDTO.SubTotal;
            OrderQuote.Total = Opportunity_OrderQuoteDTO.Total;
            OrderQuote.TotalTaxAmount = Opportunity_OrderQuoteDTO.TotalTaxAmount;
            OrderQuote.TotalTaxAmountOther = Opportunity_OrderQuoteDTO.TotalTaxAmountOther;
            OrderQuote.GeneralDiscountPercentage = Opportunity_OrderQuoteDTO.GeneralDiscountPercentage;
            OrderQuote.GeneralDiscountAmount = Opportunity_OrderQuoteDTO.GeneralDiscountAmount;
            OrderQuote.EditedPriceStatus = Opportunity_OrderQuoteDTO.EditedPriceStatus == null ? null : new EditedPriceStatus
            {
                Id = Opportunity_OrderQuoteDTO.EditedPriceStatus.Id,
                Code = Opportunity_OrderQuoteDTO.EditedPriceStatus.Code,
                Name = Opportunity_OrderQuoteDTO.EditedPriceStatus.Name,
            };
            OrderQuote.Company = Opportunity_OrderQuoteDTO.Company == null ? null : new Company
            {
                Id = Opportunity_OrderQuoteDTO.Company.Id,
                Name = Opportunity_OrderQuoteDTO.Company.Name,
                Phone = Opportunity_OrderQuoteDTO.Company.Phone,
                FAX = Opportunity_OrderQuoteDTO.Company.FAX,
                PhoneOther = Opportunity_OrderQuoteDTO.Company.PhoneOther,
                Email = Opportunity_OrderQuoteDTO.Company.Email,
                EmailOther = Opportunity_OrderQuoteDTO.Company.EmailOther,
            };
            OrderQuote.Contact = Opportunity_OrderQuoteDTO.Contact == null ? null : new Contact
            {
                Id = Opportunity_OrderQuoteDTO.Contact.Id,
                Name = Opportunity_OrderQuoteDTO.Contact.Name,
                ProfessionId = Opportunity_OrderQuoteDTO.Contact.ProfessionId,
                CompanyId = Opportunity_OrderQuoteDTO.Contact.CompanyId,
                RefuseReciveEmail = Opportunity_OrderQuoteDTO.Contact.RefuseReciveEmail,
                RefuseReciveSMS = Opportunity_OrderQuoteDTO.Contact.RefuseReciveSMS,
            };
            OrderQuote.District = Opportunity_OrderQuoteDTO.District == null ? null : new District
            {
                Id = Opportunity_OrderQuoteDTO.District.Id,
                Code = Opportunity_OrderQuoteDTO.District.Code,
                Name = Opportunity_OrderQuoteDTO.District.Name,
                Priority = Opportunity_OrderQuoteDTO.District.Priority,
                ProvinceId = Opportunity_OrderQuoteDTO.District.ProvinceId,
                StatusId = Opportunity_OrderQuoteDTO.District.StatusId,
            };
            OrderQuote.InvoiceDistrict = Opportunity_OrderQuoteDTO.InvoiceDistrict == null ? null : new District
            {
                Id = Opportunity_OrderQuoteDTO.InvoiceDistrict.Id,
                Code = Opportunity_OrderQuoteDTO.InvoiceDistrict.Code,
                Name = Opportunity_OrderQuoteDTO.InvoiceDistrict.Name,
                Priority = Opportunity_OrderQuoteDTO.InvoiceDistrict.Priority,
                ProvinceId = Opportunity_OrderQuoteDTO.InvoiceDistrict.ProvinceId,
                StatusId = Opportunity_OrderQuoteDTO.InvoiceDistrict.StatusId,
            };
            OrderQuote.InvoiceNation = Opportunity_OrderQuoteDTO.InvoiceNation == null ? null : new Nation
            {
                Id = Opportunity_OrderQuoteDTO.InvoiceNation.Id,
                Code = Opportunity_OrderQuoteDTO.InvoiceNation.Code,
                Name = Opportunity_OrderQuoteDTO.InvoiceNation.Name,
                Priority = Opportunity_OrderQuoteDTO.InvoiceNation.DisplayOrder,
                StatusId = Opportunity_OrderQuoteDTO.InvoiceNation.StatusId,
            };
            OrderQuote.InvoiceProvince = Opportunity_OrderQuoteDTO.InvoiceProvince == null ? null : new Province
            {
                Id = Opportunity_OrderQuoteDTO.InvoiceProvince.Id,
                Code = Opportunity_OrderQuoteDTO.InvoiceProvince.Code,
                Name = Opportunity_OrderQuoteDTO.InvoiceProvince.Name,
                Priority = Opportunity_OrderQuoteDTO.InvoiceProvince.Priority,
                StatusId = Opportunity_OrderQuoteDTO.InvoiceProvince.StatusId,
            };
            OrderQuote.Nation = Opportunity_OrderQuoteDTO.Nation == null ? null : new Nation
            {
                Id = Opportunity_OrderQuoteDTO.Nation.Id,
                Code = Opportunity_OrderQuoteDTO.Nation.Code,
                Name = Opportunity_OrderQuoteDTO.Nation.Name,
                Priority = Opportunity_OrderQuoteDTO.Nation.DisplayOrder,
                StatusId = Opportunity_OrderQuoteDTO.Nation.StatusId,
            };
            OrderQuote.Opportunity = Opportunity_OrderQuoteDTO.Opportunity == null ? null : new Opportunity
            {
                Id = Opportunity_OrderQuoteDTO.Opportunity.Id,
                Name = Opportunity_OrderQuoteDTO.Opportunity.Name,
                CompanyId = Opportunity_OrderQuoteDTO.Opportunity.CompanyId,
                ClosingDate = Opportunity_OrderQuoteDTO.Opportunity.ClosingDate,
                SaleStageId = Opportunity_OrderQuoteDTO.Opportunity.SaleStageId,
                ProbabilityId = Opportunity_OrderQuoteDTO.Opportunity.ProbabilityId,
                PotentialResultId = Opportunity_OrderQuoteDTO.Opportunity.PotentialResultId,
                LeadSourceId = Opportunity_OrderQuoteDTO.Opportunity.LeadSourceId,
                Amount = Opportunity_OrderQuoteDTO.Opportunity.Amount,
                ForecastAmount = Opportunity_OrderQuoteDTO.Opportunity.ForecastAmount,
                Description = Opportunity_OrderQuoteDTO.Opportunity.Description,
            };
            OrderQuote.OrderQuoteStatus = Opportunity_OrderQuoteDTO.OrderQuoteStatus == null ? null : new OrderQuoteStatus
            {
                Id = Opportunity_OrderQuoteDTO.OrderQuoteStatus.Id,
                Code = Opportunity_OrderQuoteDTO.OrderQuoteStatus.Code,
                Name = Opportunity_OrderQuoteDTO.OrderQuoteStatus.Name,
            };
            OrderQuote.Province = Opportunity_OrderQuoteDTO.Province == null ? null : new Province
            {
                Id = Opportunity_OrderQuoteDTO.Province.Id,
                Code = Opportunity_OrderQuoteDTO.Province.Code,
                Name = Opportunity_OrderQuoteDTO.Province.Name,
                Priority = Opportunity_OrderQuoteDTO.Province.Priority,
                StatusId = Opportunity_OrderQuoteDTO.Province.StatusId,
            };
            OrderQuote.AppUser = Opportunity_OrderQuoteDTO.AppUser == null ? null : new AppUser
            {
                Id = Opportunity_OrderQuoteDTO.AppUser.Id,
                Username = Opportunity_OrderQuoteDTO.AppUser.Username,
                DisplayName = Opportunity_OrderQuoteDTO.AppUser.DisplayName,
                Address = Opportunity_OrderQuoteDTO.AppUser.Address,
                Email = Opportunity_OrderQuoteDTO.AppUser.Email,
                Phone = Opportunity_OrderQuoteDTO.AppUser.Phone,
                SexId = Opportunity_OrderQuoteDTO.AppUser.SexId,
                Birthday = Opportunity_OrderQuoteDTO.AppUser.Birthday,
                Avatar = Opportunity_OrderQuoteDTO.AppUser.Avatar,
                PositionId = Opportunity_OrderQuoteDTO.AppUser.PositionId,
                Department = Opportunity_OrderQuoteDTO.AppUser.Department,
                OrganizationId = Opportunity_OrderQuoteDTO.AppUser.OrganizationId,
                ProvinceId = Opportunity_OrderQuoteDTO.AppUser.ProvinceId,
                Longitude = Opportunity_OrderQuoteDTO.AppUser.Longitude,
                Latitude = Opportunity_OrderQuoteDTO.AppUser.Latitude,
                StatusId = Opportunity_OrderQuoteDTO.AppUser.StatusId,
            };
            OrderQuote.OrderQuoteContents = Opportunity_OrderQuoteDTO.OrderQuoteContents?
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

        #region Email
        [Route(OpportunityRoute.CreateEmail), HttpPost]
        public async Task<ActionResult<Opportunity_OpportunityEmailDTO>> CreateEmail([FromBody] Opportunity_OpportunityEmailDTO Opportunity_OpportunityEmailDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OpportunityEmail OpportunityEmail = ConvertOpportunityEmail(Opportunity_OpportunityEmailDTO);
            OpportunityEmail = await OpportunityEmailService.Create(OpportunityEmail);
            Opportunity_OpportunityEmailDTO = new Opportunity_OpportunityEmailDTO(OpportunityEmail);
            if (OpportunityEmail.IsValidated)
                return Opportunity_OpportunityEmailDTO;
            else
                return BadRequest(Opportunity_OpportunityEmailDTO);
        }

        [Route(OpportunityRoute.SendEmail), HttpPost]
        public async Task<ActionResult<bool>> SendEmail([FromBody] Opportunity_OpportunityEmailDTO Opportunity_OpportunityEmailDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            OpportunityEmail OpportunityEmail = ConvertOpportunityEmail(Opportunity_OpportunityEmailDTO);
            OpportunityEmail = await OpportunityEmailService.Send(OpportunityEmail);
            if (OpportunityEmail.IsValidated)
                return Ok();
            else
                return BadRequest(Opportunity_OpportunityEmailDTO);
        }

        private OpportunityEmail ConvertOpportunityEmail(Opportunity_OpportunityEmailDTO Opportunity_OpportunityEmailDTO)
        {
            OpportunityEmail OpportunityEmail = new OpportunityEmail();
            OpportunityEmail.Id = Opportunity_OpportunityEmailDTO.Id;
            OpportunityEmail.Reciepient = Opportunity_OpportunityEmailDTO.Reciepient;
            OpportunityEmail.Title = Opportunity_OpportunityEmailDTO.Title;
            OpportunityEmail.Content = Opportunity_OpportunityEmailDTO.Content;
            OpportunityEmail.CreatorId = Opportunity_OpportunityEmailDTO.CreatorId;
            OpportunityEmail.OpportunityId = Opportunity_OpportunityEmailDTO.OpportunityId;
            OpportunityEmail.EmailStatusId = Opportunity_OpportunityEmailDTO.EmailStatusId;
            OpportunityEmail.EmailStatus = Opportunity_OpportunityEmailDTO.EmailStatus == null ? null : new EmailStatus
            {
                Id = Opportunity_OpportunityEmailDTO.EmailStatus.Id,
                Code = Opportunity_OpportunityEmailDTO.EmailStatus.Code,
                Name = Opportunity_OpportunityEmailDTO.EmailStatus.Name,
            };
            OpportunityEmail.OpportunityEmailCCMappings = Opportunity_OpportunityEmailDTO.OpportunityEmailCCMappings?.Select(x => new OpportunityEmailCCMapping
            {
                AppUserId = x.AppUserId,
                OpportunityEmailId = x.OpportunityEmailId,
                AppUser = x.AppUser == null ? null : new AppUser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Email = x.AppUser.Email,
                },
            }).ToList();
            OpportunityEmail.BaseLanguage = CurrentContext.Language;
            return OpportunityEmail;
        }
        #endregion
    }
}

