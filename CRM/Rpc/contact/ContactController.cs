using CRM.Common;
using CRM.Entities;
using CRM.Helpers;
using CRM.Services.MActivityPriority;
using CRM.Services.MActivityStatus;
using CRM.Services.MActivityType;
using CRM.Services.MAppUser;
using CRM.Services.MCallLog;
using CRM.Services.MCompany;
using CRM.Services.MContact;
using CRM.Services.MContactActivity;
using CRM.Services.MContactEmail;
using CRM.Services.MContactStatus;
using CRM.Services.MContract;
using CRM.Services.MCustomerLead;
using CRM.Services.MCustomerLeadSource;
using CRM.Services.MCustomerSalesOrder;
using CRM.Services.MDirectSalesOrder;
using CRM.Services.MDistrict;
using CRM.Services.MEditedPriceStatus;
using CRM.Services.MEmailStatus;
using CRM.Services.MFileType;
using CRM.Services.MImage;
using CRM.Services.MMailTemplate;
using CRM.Services.MNation;
using CRM.Services.MOpportunity;
using CRM.Services.MOrderQuote;
using CRM.Services.MOrderQuoteStatus;
using CRM.Services.MOrganization;
using CRM.Services.MPosition;
using CRM.Services.MProduct;
using CRM.Services.MProductGrouping;
using CRM.Services.MProductType;
using CRM.Services.MProfession;
using CRM.Services.MProvince;
using CRM.Services.MSex;
using CRM.Services.MSmsTemplate;
using CRM.Services.MStore;
using CRM.Services.MSupplier;
using CRM.Services.MTaxType;
using CRM.Services.MSaleStage;
using CRM.Services.MOrderPaymentStatus;
using CRM.Services.MRequestState;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CRM.Services.MCompanyStatus;
using CRM.Services.MProbability;
using CRM.Models;

namespace CRM.Rpc.contact
{
    public partial class ContactController : RpcController
    {
        private IDistrictService DistrictService;
        private IImageService ImageService;
        private IProfessionService ProfessionService;
        private ICustomerLeadSourceService CustomerLeadSourceService;
        private INationService NationService;
        private IProvinceService ProvinceService;
        private IAppUserService AppUserService;
        private IContactStatusService ContactStatusService;
        private IContactService ContactService;
        private ICurrentContext CurrentContext;
        private ICompanyService CompanyService;
        private IPositionService PositionService;
        private IOrganizationService OrganizationService;
        private ISexService SexService;
        private IActivityStatusService ActivityStatusService;
        private IActivityTypeService ActivityTypeService;
        private ICallLogService CallLogService;
        private ISmsTemplateService SmsTemplateService;
        private IMailTemplateService MailTemplateService;
        private IOpportunityService OpportunityService;
        private IOrderQuoteService OrderQuoteService;
        private IOrderQuoteStatusService OrderQuoteStatusService;
        private IProductService ProductService;
        private ITaxTypeService TaxTypeService;
        private ISupplierService SupplierService;
        private IProductTypeService ProductTypeService;
        private IProductGroupingService ProductGroupingService;
        private IDirectSalesOrderService DirectSalesOrderService;
        private IEditedPriceStatusService EditedPriceStatusService;
        private IStoreService StoreService;
        private IContractService ContractService;
        private IContactEmailService ContactEmailService;
        private IEmailStatusService EmailStatusService;
        private IContactActivityService ContactActivityService;
        private ICustomerLeadService CustomerLeadService;
        private IActivityPriorityService ActivityPriorityService;
        private IFileTypeService FileTypeService;
        private ICustomerSalesOrderService CustomerSalesOrderService;
        private ISaleStageService SaleStageService;
        private IOrderPaymentStatusService OrderPaymentStatusService;
        private IRequestStateService RequestStateService;
        private ICompanyStatusService CompanyStatusService;
        private IProbabilityService ProbabilityService;

        public ContactController(
            IDistrictService DistrictService,
            IImageService ImageService,
            IProfessionService ProfessionService,
            ICustomerLeadSourceService CustomerLeadSourceService,
            INationService NationService,
            IProvinceService ProvinceService,
            IAppUserService AppUserService,
            IContactStatusService ContactStatusService,
            IContactService ContactService,
            ICurrentContext CurrentContext,
            ICompanyService CompanyService,
            IPositionService PositionService,
            IOrganizationService OrganizationService,
            ISexService SexService,
            IActivityStatusService ActivityStatusService,
            IActivityTypeService ActivityTypeService,
            ICallLogService CallLogService,
            ISmsTemplateService SmsTemplateService,
            IMailTemplateService MailTemplateService,
            IOpportunityService OpportunityService,
            IOrderQuoteService OrderQuoteService,
            IOrderQuoteStatusService OrderQuoteStatusService,
            IProductService ProductService,
            ITaxTypeService TaxTypeService,
            ISupplierService SupplierService,
            IProductTypeService ProductTypeService,
            IProductGroupingService ProductGroupingService,
            IDirectSalesOrderService DirectSalesOrderService,
            IEditedPriceStatusService EditedPriceStatusService,
            IStoreService StoreService,
            IContractService ContractService,
            IContactEmailService ContactEmailService,
            IEmailStatusService EmailStatusService,
            IContactActivityService ContactActivityService,
            ICustomerLeadService CustomerLeadService,
            IActivityPriorityService ActivityPriorityService,
            IFileTypeService FileTypeService,
            ICustomerSalesOrderService CustomerSalesOrderService,
            ISaleStageService SaleStageService,
            IOrderPaymentStatusService OrderPaymentStatusService,
            IRequestStateService RequestStateService,
            ICompanyStatusService CompanyStatusService,
            IProbabilityService ProbabilityService
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.DistrictService = DistrictService;
            this.ImageService = ImageService;
            this.ProfessionService = ProfessionService;
            this.CustomerLeadSourceService = CustomerLeadSourceService;
            this.NationService = NationService;
            this.ProvinceService = ProvinceService;
            this.AppUserService = AppUserService;
            this.ContactStatusService = ContactStatusService;
            this.ContactService = ContactService;
            this.CurrentContext = CurrentContext;
            this.CompanyService = CompanyService;
            this.PositionService = PositionService;
            this.OrganizationService = OrganizationService;
            this.SexService = SexService;
            this.ActivityStatusService = ActivityStatusService;
            this.ActivityTypeService = ActivityTypeService;
            this.CallLogService = CallLogService;
            this.SmsTemplateService = SmsTemplateService;
            this.MailTemplateService = MailTemplateService;
            this.OpportunityService = OpportunityService;
            this.OrderQuoteService = OrderQuoteService;
            this.OrderQuoteStatusService = OrderQuoteStatusService;
            this.ProductService = ProductService;
            this.TaxTypeService = TaxTypeService;
            this.SupplierService = SupplierService;
            this.ProductTypeService = ProductTypeService;
            this.ProductGroupingService = ProductGroupingService;
            this.DirectSalesOrderService = DirectSalesOrderService;
            this.EditedPriceStatusService = EditedPriceStatusService;
            this.StoreService = StoreService;
            this.ContractService = ContractService;
            this.ContactEmailService = ContactEmailService;
            this.EmailStatusService = EmailStatusService;
            this.ContactActivityService = ContactActivityService;
            this.CustomerLeadService = CustomerLeadService;
            this.ActivityPriorityService = ActivityPriorityService;
            this.FileTypeService = FileTypeService;
            this.CustomerSalesOrderService = CustomerSalesOrderService;
            this.SaleStageService = SaleStageService;
            this.OrderPaymentStatusService = OrderPaymentStatusService;
            this.RequestStateService = RequestStateService;
            this.CompanyStatusService = CompanyStatusService;
            this.ProbabilityService = ProbabilityService;
        }

        [Route(ContactRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] Contact_ContactFilterDTO Contact_ContactFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactFilter ContactFilter = ConvertFilterDTOToFilterEntity(Contact_ContactFilterDTO);
            ContactFilter = await  ContactService.ToFilter(ContactFilter);
            int count = await ContactService.Count(ContactFilter);
            return count;
        }       

        [Route(ContactRoute.List), HttpPost]
        public async Task<ActionResult<List<Contact_ContactDTO>>> List([FromBody] Contact_ContactFilterDTO Contact_ContactFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactFilter ContactFilter = ConvertFilterDTOToFilterEntity(Contact_ContactFilterDTO);
            ContactFilter = await ContactService.ToFilter(ContactFilter);
            List<Contact> Contacts = await ContactService.List(ContactFilter);
            List<Contact_ContactDTO> Contact_ContactDTOs = Contacts
                .Select(c => new Contact_ContactDTO(c)).ToList();
            return Contact_ContactDTOs;
        }

        [Route(ContactRoute.Get), HttpPost]
        public async Task<ActionResult<Contact_ContactDTO>> Get([FromBody]Contact_ContactDTO Contact_ContactDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Contact_ContactDTO.Id))
                return Forbid();

            Contact Contact = await ContactService.Get(Contact_ContactDTO.Id);
            return new Contact_ContactDTO(Contact);
        }

        [Route(ContactRoute.Create), HttpPost]
        public async Task<ActionResult<Contact_ContactDTO>> Create([FromBody] Contact_ContactDTO Contact_ContactDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(Contact_ContactDTO.Id))
                return Forbid();
            
            Contact Contact = ConvertDTOToEntity(Contact_ContactDTO);
            Contact = await ContactService.Create(Contact);
            Contact_ContactDTO = new Contact_ContactDTO(Contact);
            if (Contact.IsValidated)
                return Contact_ContactDTO;
            else
                return BadRequest(Contact_ContactDTO);
        }

        [Route(ContactRoute.Update), HttpPost]
        public async Task<ActionResult<Contact_ContactDTO>> Update([FromBody] Contact_ContactDTO Contact_ContactDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(Contact_ContactDTO.Id))
                return Forbid();

            Contact Contact = ConvertDTOToEntity(Contact_ContactDTO);
            Contact = await ContactService.Update(Contact);
            Contact_ContactDTO = new Contact_ContactDTO(Contact);
            if (Contact.IsValidated)
                return Contact_ContactDTO;
            else
                return BadRequest(Contact_ContactDTO);
        }

        [Route(ContactRoute.Delete), HttpPost]
        public async Task<ActionResult<Contact_ContactDTO>> Delete([FromBody] Contact_ContactDTO Contact_ContactDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Contact_ContactDTO.Id))
                return Forbid();

            Contact Contact = ConvertDTOToEntity(Contact_ContactDTO);
            Contact = await ContactService.Delete(Contact);
            Contact_ContactDTO = new Contact_ContactDTO(Contact);
            if (Contact.IsValidated)
                return Contact_ContactDTO;
            else
                return BadRequest(Contact_ContactDTO);
        }
        
        [Route(ContactRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
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


        [Route(ContactRoute.Import), HttpPost]
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
            ContactStatusFilter ContactStatusFilter = new ContactStatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = ContactStatusSelect.ALL
            };
            List<ContactStatus> ContactStatuses = await ContactStatusService.List(ContactStatusFilter);
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
            ImageFilter ImageFilter = new ImageFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = ImageSelect.ALL
            };
            List<Image> Images = await ImageService.List(ImageFilter);
            NationFilter NationFilter = new NationFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = NationSelect.ALL
            };
            List<Nation> Nations = await NationService.List(NationFilter);
            PositionFilter PositionFilter = new PositionFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = PositionSelect.ALL
            };
            List<Position> Positions = await PositionService.List(PositionFilter);
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
            List<Contact> Contacts = new List<Contact>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(Contacts);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int CodeColumn = 1 + StartColumn;
                int NameColumn = 2 + StartColumn;
                int ProfessionIdColumn = 3 + StartColumn;
                int CompanyIdColumn = 4 + StartColumn;
                int ProvinceIdColumn = 5 + StartColumn;
                int DistrictIdColumn = 6 + StartColumn;
                int NationIdColumn = 7 + StartColumn;
                int CustomerLeadIdColumn = 8 + StartColumn;
                int ImageIdColumn = 9 + StartColumn;
                int DescriptionColumn = 10 + StartColumn;
                int AddressColumn = 11 + StartColumn;
                int EmailOtherColumn = 12 + StartColumn;
                int DateOfBirthColumn = 13 + StartColumn;
                int PhoneColumn = 14 + StartColumn;
                int PhoneHomeColumn = 15 + StartColumn;
                int PhoneOtherColumn = 16 + StartColumn;
                int FAXColumn = 17 + StartColumn;
                int EmailColumn = 18 + StartColumn;
                int RefuseReciveEmailColumn = 19 + StartColumn;
                int RefuseReciveSMSColumn = 20 + StartColumn;
                int ZIPCodeColumn = 21 + StartColumn;
                int SexIdColumn = 22 + StartColumn;
                int AppUserIdColumn = 23 + StartColumn;
                int PositionIdColumn = 24 + StartColumn;
                int OrganizationIdColumn = 25 + StartColumn;
                int ContactStatusIdColumn = 26 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string CodeValue = worksheet.Cells[i + StartRow, CodeColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string ProfessionIdValue = worksheet.Cells[i + StartRow, ProfessionIdColumn].Value?.ToString();
                    string CompanyIdValue = worksheet.Cells[i + StartRow, CompanyIdColumn].Value?.ToString();
                    string ProvinceIdValue = worksheet.Cells[i + StartRow, ProvinceIdColumn].Value?.ToString();
                    string DistrictIdValue = worksheet.Cells[i + StartRow, DistrictIdColumn].Value?.ToString();
                    string NationIdValue = worksheet.Cells[i + StartRow, NationIdColumn].Value?.ToString();
                    string CustomerLeadIdValue = worksheet.Cells[i + StartRow, CustomerLeadIdColumn].Value?.ToString();
                    string ImageIdValue = worksheet.Cells[i + StartRow, ImageIdColumn].Value?.ToString();
                    string DescriptionValue = worksheet.Cells[i + StartRow, DescriptionColumn].Value?.ToString();
                    string AddressValue = worksheet.Cells[i + StartRow, AddressColumn].Value?.ToString();
                    string EmailOtherValue = worksheet.Cells[i + StartRow, EmailOtherColumn].Value?.ToString();
                    string DateOfBirthValue = worksheet.Cells[i + StartRow, DateOfBirthColumn].Value?.ToString();
                    string PhoneValue = worksheet.Cells[i + StartRow, PhoneColumn].Value?.ToString();
                    string PhoneHomeValue = worksheet.Cells[i + StartRow, PhoneHomeColumn].Value?.ToString();
                    string PhoneOtherValue = worksheet.Cells[i + StartRow, PhoneOtherColumn].Value?.ToString();
                    string FAXValue = worksheet.Cells[i + StartRow, FAXColumn].Value?.ToString();
                    string EmailValue = worksheet.Cells[i + StartRow, EmailColumn].Value?.ToString();
                    string RefuseReciveEmailValue = worksheet.Cells[i + StartRow, RefuseReciveEmailColumn].Value?.ToString();
                    string RefuseReciveSMSValue = worksheet.Cells[i + StartRow, RefuseReciveSMSColumn].Value?.ToString();
                    string ZIPCodeValue = worksheet.Cells[i + StartRow, ZIPCodeColumn].Value?.ToString();
                    string SexIdValue = worksheet.Cells[i + StartRow, SexIdColumn].Value?.ToString();
                    string AppUserIdValue = worksheet.Cells[i + StartRow, AppUserIdColumn].Value?.ToString();
                    string PositionIdValue = worksheet.Cells[i + StartRow, PositionIdColumn].Value?.ToString();
                    string OrganizationIdValue = worksheet.Cells[i + StartRow, OrganizationIdColumn].Value?.ToString();
                    string ContactStatusIdValue = worksheet.Cells[i + StartRow, ContactStatusIdColumn].Value?.ToString();

                    Contact Contact = new Contact();
                    Contact.Name = NameValue;
                    Contact.Description = DescriptionValue;
                    Contact.Address = AddressValue;
                    Contact.EmailOther = EmailOtherValue;
                    Contact.DateOfBirth = DateTime.TryParse(DateOfBirthValue, out DateTime DateOfBirth) ? DateOfBirth : DateTime.Now;
                    Contact.Phone = PhoneValue;
                    Contact.PhoneHome = PhoneHomeValue;
                    Contact.FAX = FAXValue;
                    Contact.Email = EmailValue;
                    Contact.ZIPCode = ZIPCodeValue;
                    AppUser AppUser = AppUsers.Where(x => x.Id.ToString() == AppUserIdValue).FirstOrDefault();
                    Contact.AppUserId = AppUser == null ? 0 : AppUser.Id;
                    Contact.AppUser = AppUser;
                    ContactStatus ContactStatus = ContactStatuses.Where(x => x.Id.ToString() == ContactStatusIdValue).FirstOrDefault();
                    Contact.ContactStatusId = ContactStatus == null ? 0 : ContactStatus.Id;
                    Contact.ContactStatus = ContactStatus;
                    CustomerLead CustomerLead = CustomerLeads.Where(x => x.Id.ToString() == CustomerLeadIdValue).FirstOrDefault();
                    Contact.CustomerLeadId = CustomerLead == null ? 0 : CustomerLead.Id;
                    Contact.CustomerLead = CustomerLead;
                    District District = Districts.Where(x => x.Id.ToString() == DistrictIdValue).FirstOrDefault();
                    Contact.DistrictId = District == null ? 0 : District.Id;
                    Contact.District = District;
                    Image Image = Images.Where(x => x.Id.ToString() == ImageIdValue).FirstOrDefault();
                    Contact.ImageId = Image == null ? 0 : Image.Id;
                    Contact.Image = Image;
                    Nation Nation = Nations.Where(x => x.Id.ToString() == NationIdValue).FirstOrDefault();
                    Contact.NationId = Nation == null ? 0 : Nation.Id;
                    Contact.Nation = Nation;
                    Position Position = Positions.Where(x => x.Id.ToString() == PositionIdValue).FirstOrDefault();
                    Contact.PositionId = Position == null ? 0 : Position.Id;
                    Contact.Position = Position;
                    Profession Profession = Professions.Where(x => x.Id.ToString() == ProfessionIdValue).FirstOrDefault();
                    Contact.ProfessionId = Profession == null ? 0 : Profession.Id;
                    Contact.Profession = Profession;
                    Province Province = Provinces.Where(x => x.Id.ToString() == ProvinceIdValue).FirstOrDefault();
                    Contact.ProvinceId = Province == null ? 0 : Province.Id;
                    Contact.Province = Province;
                    Sex Sex = Sexes.Where(x => x.Id.ToString() == SexIdValue).FirstOrDefault();
                    Contact.SexId = Sex == null ? 0 : Sex.Id;
                    Contact.Sex = Sex;

                    Contacts.Add(Contact);
                }
            }
            Contacts = await ContactService.Import(Contacts);
            if (Contacts.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < Contacts.Count; i++)
                {
                    Contact Contact = Contacts[i];
                    if (!Contact.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (Contact.Errors.ContainsKey(nameof(Contact.Id)))
                            Error += Contact.Errors[nameof(Contact.Id)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.Name)))
                            Error += Contact.Errors[nameof(Contact.Name)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.ProfessionId)))
                            Error += Contact.Errors[nameof(Contact.ProfessionId)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.CompanyId)))
                            Error += Contact.Errors[nameof(Contact.CompanyId)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.ProvinceId)))
                            Error += Contact.Errors[nameof(Contact.ProvinceId)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.DistrictId)))
                            Error += Contact.Errors[nameof(Contact.DistrictId)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.NationId)))
                            Error += Contact.Errors[nameof(Contact.NationId)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.CustomerLeadId)))
                            Error += Contact.Errors[nameof(Contact.CustomerLeadId)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.ImageId)))
                            Error += Contact.Errors[nameof(Contact.ImageId)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.Description)))
                            Error += Contact.Errors[nameof(Contact.Description)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.Address)))
                            Error += Contact.Errors[nameof(Contact.Address)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.EmailOther)))
                            Error += Contact.Errors[nameof(Contact.EmailOther)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.DateOfBirth)))
                            Error += Contact.Errors[nameof(Contact.DateOfBirth)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.Phone)))
                            Error += Contact.Errors[nameof(Contact.Phone)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.PhoneHome)))
                            Error += Contact.Errors[nameof(Contact.PhoneHome)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.FAX)))
                            Error += Contact.Errors[nameof(Contact.FAX)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.Email)))
                            Error += Contact.Errors[nameof(Contact.Email)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.RefuseReciveEmail)))
                            Error += Contact.Errors[nameof(Contact.RefuseReciveEmail)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.RefuseReciveSMS)))
                            Error += Contact.Errors[nameof(Contact.RefuseReciveSMS)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.ZIPCode)))
                            Error += Contact.Errors[nameof(Contact.ZIPCode)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.SexId)))
                            Error += Contact.Errors[nameof(Contact.SexId)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.AppUserId)))
                            Error += Contact.Errors[nameof(Contact.AppUserId)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.PositionId)))
                            Error += Contact.Errors[nameof(Contact.PositionId)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.Department)))
                            Error += Contact.Errors[nameof(Contact.Department)];
                        if (Contact.Errors.ContainsKey(nameof(Contact.ContactStatusId)))
                            Error += Contact.Errors[nameof(Contact.ContactStatusId)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }

        [Route(ContactRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] Contact_ContactFilterDTO Contact_ContactFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region Contact
                var ContactFilter = ConvertFilterDTOToFilterEntity(Contact_ContactFilterDTO);
                ContactFilter.Skip = 0;
                ContactFilter.Take = int.MaxValue;
                ContactFilter = await ContactService.ToFilter(ContactFilter);
                List<Contact> Contacts = await ContactService.List(ContactFilter);

                var ContactHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                        "ProfessionId",
                        "CompanyId",
                        "ProvinceId",
                        "DistrictId",
                        "NationId",
                        "CustomerLeadId",
                        "ImageId",
                        "Description",
                        "Address",
                        "EmailOther",
                        "DateOfBirth",
                        "Phone",
                        "PhoneHome",
                        "PhoneOther",
                        "FAX",
                        "Email",
                        "RefuseReciveEmail",
                        "RefuseReciveSMS",
                        "ZIPCode",
                        "SexId",
                        "AppUserId",
                        "PositionId",
                        "OrganizationId",
                        "ContactStatusId",
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
                        Contact.ProvinceId,
                        Contact.DistrictId,
                        Contact.NationId,
                        Contact.CustomerLeadId,
                        Contact.ImageId,
                        Contact.Description,
                        Contact.Address,
                        Contact.EmailOther,
                        Contact.DateOfBirth,
                        Contact.Phone,
                        Contact.PhoneHome,
                        Contact.FAX,
                        Contact.Email,
                        Contact.RefuseReciveEmail,
                        Contact.RefuseReciveSMS,
                        Contact.ZIPCode,
                        Contact.SexId,
                        Contact.AppUserId,
                        Contact.PositionId,
                        Contact.Department,
                        Contact.ContactStatusId,
                    });
                }
                excel.GenerateWorksheet("Contact", ContactHeaders, ContactData);
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
                        "Code",
                        "Name",
                        "Phone",
                        "FAX",
                        "PhoneOther",
                        "Email",
                        "EmailOther",
                        "ZIPCode",
                        "Revenue",
                        "Website",
                        "NationId",
                        "ProvinceId",
                        "DistrictId",
                        "Address",
                        "NumberOfEmployee",
                        "RefuseReciveEmail",
                        "RefuseReciveSMS",
                        "CustomerLeadId",
                        "ParentId",
                        "Path",
                        "Level",
                        "ProfessionId",
                        "AppUserId",
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
                        Company.NationId,
                        Company.ProvinceId,
                        Company.DistrictId,
                        Company.Address,
                        Company.NumberOfEmployee,
                        Company.RefuseReciveEmail,
                        Company.RefuseReciveSMS,
                        Company.CustomerLeadId,
                        Company.ParentId,
                        Company.Path,
                        Company.Level,
                        Company.ProfessionId,
                        Company.AppUserId,
                        Company.CurrencyId,
                        Company.CompanyStatusId,
                        Company.Description,
                        Company.RowId,
                    });
                }
                excel.GenerateWorksheet("Company", CompanyHeaders, CompanyData);
                #endregion
                #region ContactStatus
                var ContactStatusFilter = new ContactStatusFilter();
                ContactStatusFilter.Selects = ContactStatusSelect.ALL;
                ContactStatusFilter.OrderBy = ContactStatusOrder.Id;
                ContactStatusFilter.OrderType = OrderType.ASC;
                ContactStatusFilter.Skip = 0;
                ContactStatusFilter.Take = int.MaxValue;
                List<ContactStatus> ContactStatuses = await ContactStatusService.List(ContactStatusFilter);

                var ContactStatusHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> ContactStatusData = new List<object[]>();
                for (int i = 0; i < ContactStatuses.Count; i++)
                {
                    var ContactStatus = ContactStatuses[i];
                    ContactStatusData.Add(new Object[]
                    {
                        ContactStatus.Id,
                        ContactStatus.Code,
                        ContactStatus.Name,
                    });
                }
                excel.GenerateWorksheet("ContactStatus", ContactStatusHeaders, ContactStatusData);
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
                        "Code",
                        "Name",
                        "TelePhone",
                        "Phone",
                        "CompanyName",
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
                        "ProvinceId",
                        "DistrictId",
                        "UserId",
                        "CustomerLeadStatusId",
                        "BusinessRegistrationCode",
                        "SexId",
                        "RefuseReciveSMS",
                        "NationId",
                        "RefuseReciveEmail",
                        "Description",
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
                        CustomerLead.TelePhone,
                        CustomerLead.Phone,
                        CustomerLead.CompanyName,
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
                        CustomerLead.ProvinceId,
                        CustomerLead.DistrictId,
                        CustomerLead.AppUserId,
                        CustomerLead.CustomerLeadStatusId,
                        CustomerLead.BusinessRegistrationCode,
                        CustomerLead.SexId,
                        CustomerLead.RefuseReciveSMS,
                        CustomerLead.NationId,
                        CustomerLead.RefuseReciveEmail,
                        CustomerLead.Description,
                        CustomerLead.CreatorId,
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
                #region Image
                var ImageFilter = new ImageFilter();
                ImageFilter.Selects = ImageSelect.ALL;
                ImageFilter.OrderBy = ImageOrder.Id;
                ImageFilter.OrderType = OrderType.ASC;
                ImageFilter.Skip = 0;
                ImageFilter.Take = int.MaxValue;
                List<Image> Images = await ImageService.List(ImageFilter);

                var ImageHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Name",
                        "Url",
                        "ThumbnailUrl",
                        "RowId",
                    }
                };
                List<object[]> ImageData = new List<object[]>();
                for (int i = 0; i < Images.Count; i++)
                {
                    var Image = Images[i];
                    ImageData.Add(new Object[]
                    {
                        Image.Id,
                        Image.Name,
                        Image.Url,
                        Image.ThumbnailUrl,
                        Image.RowId,
                    });
                }
                excel.GenerateWorksheet("Image", ImageHeaders, ImageData);
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
                #region Position
                var PositionFilter = new PositionFilter();
                PositionFilter.Selects = PositionSelect.ALL;
                PositionFilter.OrderBy = PositionOrder.Id;
                PositionFilter.OrderType = OrderType.ASC;
                PositionFilter.Skip = 0;
                PositionFilter.Take = int.MaxValue;
                List<Position> Positions = await PositionService.List(PositionFilter);

                var PositionHeaders = new List<string[]>()
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
                List<object[]> PositionData = new List<object[]>();
                for (int i = 0; i < Positions.Count; i++)
                {
                    var Position = Positions[i];
                    PositionData.Add(new Object[]
                    {
                        Position.Id,
                        Position.Code,
                        Position.Name,
                        Position.StatusId,
                        Position.RowId,
                        Position.Used,
                    });
                }
                excel.GenerateWorksheet("Position", PositionHeaders, PositionData);
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
                #region ContactActivity
                var ContactActivityFilter = new ContactActivityFilter();
                ContactActivityFilter.Selects = ContactActivitySelect.ALL;
                ContactActivityFilter.OrderBy = ContactActivityOrder.Id;
                ContactActivityFilter.OrderType = OrderType.ASC;
                ContactActivityFilter.Skip = 0;
                ContactActivityFilter.Take = int.MaxValue;
                List<ContactActivity> ContactActivities = await ContactActivityService.List(ContactActivityFilter);

                var ContactActivityHeaders = new List<string[]>()
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
                        "ContactId",
                        "AppUserId",
                        "ActivityStatusId",
                    }
                };
                List<object[]> ContactActivityData = new List<object[]>();
                for (int i = 0; i < ContactActivities.Count; i++)
                {
                    var ContactActivity = ContactActivities[i];
                    ContactActivityData.Add(new Object[]
                    {
                        ContactActivity.Id,
                        ContactActivity.Title,
                        ContactActivity.FromDate,
                        ContactActivity.ToDate,
                        ContactActivity.ActivityTypeId,
                        ContactActivity.ActivityPriorityId,
                        ContactActivity.Description,
                        ContactActivity.Address,
                        ContactActivity.ContactId,
                        ContactActivity.AppUserId,
                        ContactActivity.ActivityStatusId,
                    });
                }
                excel.GenerateWorksheet("ContactActivity", ContactActivityHeaders, ContactActivityData);
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
                        CallLog.CallEmotionId,
                        CallLog.AppUserId,
                        CallLog.CreatorId,
                    });
                }
                excel.GenerateWorksheet("CallLog", CallLogHeaders, CallLogData);
                #endregion
                #region ContactEmail
                var ContactEmailFilter = new ContactEmailFilter();
                ContactEmailFilter.Selects = ContactEmailSelect.ALL;
                ContactEmailFilter.OrderBy = ContactEmailOrder.Id;
                ContactEmailFilter.OrderType = OrderType.ASC;
                ContactEmailFilter.Skip = 0;
                ContactEmailFilter.Take = int.MaxValue;
                List<ContactEmail> ContactEmails = await ContactEmailService.List(ContactEmailFilter);

                var ContactEmailHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Title",
                        "Content",
                        "Reciepient",
                        "ContactId",
                        "CreatorId",
                        "EmailStatusId",
                    }
                };
                List<object[]> ContactEmailData = new List<object[]>();
                for (int i = 0; i < ContactEmails.Count; i++)
                {
                    var ContactEmail = ContactEmails[i];
                    ContactEmailData.Add(new Object[]
                    {
                        ContactEmail.Id,
                        ContactEmail.Title,
                        ContactEmail.Content,
                        ContactEmail.Reciepient,
                        ContactEmail.ContactId,
                        ContactEmail.CreatorId,
                        ContactEmail.EmailStatusId,
                    });
                }
                excel.GenerateWorksheet("ContactEmail", ContactEmailHeaders, ContactEmailData);
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
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "Contact.xlsx");
        }

        [Route(ContactRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] Contact_ContactFilterDTO Contact_ContactFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            string path = "Templates/Contact_Template.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "Contact.xlsx");
        }

        [Route(ContactRoute.UploadFile), HttpPost]
        public async Task<ActionResult<List<Contact_FileDTO>>> UploadFile(IFormFile file)
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
            File = await ContactService.UploadFile(File);
            if (File == null)
                return BadRequest();
            Contact_FileDTO Contact_FileDTO = new Contact_FileDTO
            {
                Id = File.Id,
                Name = File.Name,
                Url = File.Url,
                AppUserId = File.AppUserId
            };
            return Ok(Contact_FileDTO);
        }

        private async Task<bool> HasPermission(long Id)
        {
            ContactFilter ContactFilter = new ContactFilter();
            ContactFilter = await ContactService.ToFilter(ContactFilter);
            if (Id == 0)
            {

            }
            else
            {
                ContactFilter.Id = new IdFilter { Equal = Id };
                int count = await ContactService.Count(ContactFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private Contact ConvertDTOToEntity(Contact_ContactDTO Contact_ContactDTO)
        {
            Contact Contact = new Contact();
            Contact.Id = Contact_ContactDTO.Id;
            Contact.Name = Contact_ContactDTO.Name;
            Contact.ProfessionId = Contact_ContactDTO.ProfessionId;
            Contact.CompanyId = Contact_ContactDTO.CompanyId;
            Contact.ContactStatusId = Contact_ContactDTO.ContactStatusId;
            Contact.Address = Contact_ContactDTO.Address;
            Contact.NationId = Contact_ContactDTO.NationId;
            Contact.ProvinceId = Contact_ContactDTO.ProvinceId;
            Contact.DistrictId = Contact_ContactDTO.DistrictId;
            Contact.CustomerLeadId = Contact_ContactDTO.CustomerLeadId;
            Contact.ImageId = Contact_ContactDTO.ImageId;
            Contact.Description = Contact_ContactDTO.Description;
            Contact.EmailOther = Contact_ContactDTO.EmailOther;
            Contact.DateOfBirth = Contact_ContactDTO.DateOfBirth;
            Contact.Phone = Contact_ContactDTO.Phone;
            Contact.PhoneHome = Contact_ContactDTO.PhoneHome;
            Contact.FAX = Contact_ContactDTO.FAX;
            Contact.Email = Contact_ContactDTO.Email;
            Contact.Department = Contact_ContactDTO.Department;
            Contact.ZIPCode = Contact_ContactDTO.ZIPCode;
            Contact.SexId = Contact_ContactDTO.SexId;
            Contact.AppUserId = Contact_ContactDTO.AppUserId;
            Contact.RefuseReciveEmail = Contact_ContactDTO.RefuseReciveEmail;
            Contact.RefuseReciveSMS = Contact_ContactDTO.RefuseReciveSMS;
            Contact.PositionId = Contact_ContactDTO.PositionId;
            Contact.CreatorId = Contact_ContactDTO.CreatorId;
            Contact.AppUser = Contact_ContactDTO.AppUser == null ? null : new AppUser
            {
                Id = Contact_ContactDTO.AppUser.Id,
                Username = Contact_ContactDTO.AppUser.Username,
                DisplayName = Contact_ContactDTO.AppUser.DisplayName,
                Address = Contact_ContactDTO.AppUser.Address,
                Email = Contact_ContactDTO.AppUser.Email,
                Phone = Contact_ContactDTO.AppUser.Phone,
                SexId = Contact_ContactDTO.AppUser.SexId,
                Birthday = Contact_ContactDTO.AppUser.Birthday,
                Avatar = Contact_ContactDTO.AppUser.Avatar,
                Department = Contact_ContactDTO.AppUser.Department,
                OrganizationId = Contact_ContactDTO.AppUser.OrganizationId,
                Longitude = Contact_ContactDTO.AppUser.Longitude,
                Latitude = Contact_ContactDTO.AppUser.Latitude,
                StatusId = Contact_ContactDTO.AppUser.StatusId,
            };
            Contact.Company = Contact_ContactDTO.Company == null ? null : new Company
            {
                Id = Contact_ContactDTO.Company.Id,
                Name = Contact_ContactDTO.Company.Name,
                Phone = Contact_ContactDTO.Company.Phone,
                FAX = Contact_ContactDTO.Company.FAX,
                PhoneOther = Contact_ContactDTO.Company.PhoneOther,
                Email = Contact_ContactDTO.Company.Email,
                EmailOther = Contact_ContactDTO.Company.EmailOther,
                ZIPCode = Contact_ContactDTO.Company.ZIPCode,
                Revenue = Contact_ContactDTO.Company.Revenue,
                Website = Contact_ContactDTO.Company.Website,
                Address = Contact_ContactDTO.Company.Address,
                NationId = Contact_ContactDTO.Company.NationId,
                ProvinceId = Contact_ContactDTO.Company.ProvinceId,
                DistrictId = Contact_ContactDTO.Company.DistrictId,
                NumberOfEmployee = Contact_ContactDTO.Company.NumberOfEmployee,
                RefuseReciveEmail = Contact_ContactDTO.Company.RefuseReciveEmail,
                RefuseReciveSMS = Contact_ContactDTO.Company.RefuseReciveSMS,
                CustomerLeadId = Contact_ContactDTO.Company.CustomerLeadId,
                ParentId = Contact_ContactDTO.Company.ParentId,
                Path = Contact_ContactDTO.Company.Path,
                Level = Contact_ContactDTO.Company.Level,
                ProfessionId = Contact_ContactDTO.Company.ProfessionId,
                AppUserId = Contact_ContactDTO.Company.AppUserId,
                CreatorId = Contact_ContactDTO.Company.CreatorId,
                CurrencyId = Contact_ContactDTO.Company.CurrencyId,
                CompanyStatusId = Contact_ContactDTO.Company.CompanyStatusId,
                Description = Contact_ContactDTO.Company.Description,
                RowId = Contact_ContactDTO.Company.RowId,
            };
            Contact.ContactStatus = Contact_ContactDTO.ContactStatus == null ? null : new ContactStatus
            {
                Id = Contact_ContactDTO.ContactStatus.Id,
                Code = Contact_ContactDTO.ContactStatus.Code,
                Name = Contact_ContactDTO.ContactStatus.Name,
            };
            Contact.Creator = Contact_ContactDTO.Creator == null ? null : new AppUser
            {
                Id = Contact_ContactDTO.Creator.Id,
                Username = Contact_ContactDTO.Creator.Username,
                DisplayName = Contact_ContactDTO.Creator.DisplayName,
                Address = Contact_ContactDTO.Creator.Address,
                Email = Contact_ContactDTO.Creator.Email,
                Phone = Contact_ContactDTO.Creator.Phone,
                SexId = Contact_ContactDTO.Creator.SexId,
                Birthday = Contact_ContactDTO.Creator.Birthday,
                Avatar = Contact_ContactDTO.Creator.Avatar,
                Department = Contact_ContactDTO.Creator.Department,
                OrganizationId = Contact_ContactDTO.Creator.OrganizationId,
                Longitude = Contact_ContactDTO.Creator.Longitude,
                Latitude = Contact_ContactDTO.Creator.Latitude,
                StatusId = Contact_ContactDTO.Creator.StatusId,
            };
            Contact.CustomerLead = Contact_ContactDTO.CustomerLead == null ? null : new CustomerLead
            {
                Id = Contact_ContactDTO.CustomerLead.Id,
                Name = Contact_ContactDTO.CustomerLead.Name,
                CompanyName = Contact_ContactDTO.CustomerLead.CompanyName,
                TelePhone = Contact_ContactDTO.CustomerLead.TelePhone,
                Phone = Contact_ContactDTO.CustomerLead.Phone,
                Fax = Contact_ContactDTO.CustomerLead.Fax,
                Email = Contact_ContactDTO.CustomerLead.Email,
                SecondEmail = Contact_ContactDTO.CustomerLead.SecondEmail,
                Website = Contact_ContactDTO.CustomerLead.Website,
                CustomerLeadSourceId = Contact_ContactDTO.CustomerLead.CustomerLeadSourceId,
                CustomerLeadLevelId = Contact_ContactDTO.CustomerLead.CustomerLeadLevelId,
                CompanyId = Contact_ContactDTO.CustomerLead.CompanyId,
                CampaignId = Contact_ContactDTO.CustomerLead.CampaignId,
                ProfessionId = Contact_ContactDTO.CustomerLead.ProfessionId,
                Revenue = Contact_ContactDTO.CustomerLead.Revenue,
                EmployeeQuantity = Contact_ContactDTO.CustomerLead.EmployeeQuantity,
                Address = Contact_ContactDTO.CustomerLead.Address,
                NationId = Contact_ContactDTO.CustomerLead.NationId,
                ProvinceId = Contact_ContactDTO.CustomerLead.ProvinceId,
                DistrictId = Contact_ContactDTO.CustomerLead.DistrictId,
                CustomerLeadStatusId = Contact_ContactDTO.CustomerLead.CustomerLeadStatusId,
                BusinessRegistrationCode = Contact_ContactDTO.CustomerLead.BusinessRegistrationCode,
                SexId = Contact_ContactDTO.CustomerLead.SexId,
                RefuseReciveSMS = Contact_ContactDTO.CustomerLead.RefuseReciveSMS,
                RefuseReciveEmail = Contact_ContactDTO.CustomerLead.RefuseReciveEmail,
                Description = Contact_ContactDTO.CustomerLead.Description,
                AppUserId = Contact_ContactDTO.CustomerLead.AppUserId,
                CreatorId = Contact_ContactDTO.CustomerLead.CreatorId,
                ZipCode = Contact_ContactDTO.CustomerLead.ZipCode,
                CurrencyId = Contact_ContactDTO.CustomerLead.CurrencyId,
                RowId = Contact_ContactDTO.CustomerLead.RowId,
            };
            Contact.District = Contact_ContactDTO.District == null ? null : new District
            {
                Id = Contact_ContactDTO.District.Id,
                Code = Contact_ContactDTO.District.Code,
                Name = Contact_ContactDTO.District.Name,
                Priority = Contact_ContactDTO.District.Priority,
                ProvinceId = Contact_ContactDTO.District.ProvinceId,
                StatusId = Contact_ContactDTO.District.StatusId,
            };
            Contact.Image = Contact_ContactDTO.Image == null ? null : new Image
            {
                Id = Contact_ContactDTO.Image.Id,
                Name = Contact_ContactDTO.Image.Name,
                Url = Contact_ContactDTO.Image.Url,
            };
            Contact.Nation = Contact_ContactDTO.Nation == null ? null : new Nation
            {
                Id = Contact_ContactDTO.Nation.Id,
                Code = Contact_ContactDTO.Nation.Code,
                Name = Contact_ContactDTO.Nation.Name,
                StatusId = Contact_ContactDTO.Nation.StatusId,
            };
            Contact.Position = Contact_ContactDTO.Position == null ? null : new Position
            {
                Id = Contact_ContactDTO.Position.Id,
                Code = Contact_ContactDTO.Position.Code,
                Name = Contact_ContactDTO.Position.Name,
                StatusId = Contact_ContactDTO.Position.StatusId,
            };
            Contact.Profession = Contact_ContactDTO.Profession == null ? null : new Profession
            {
                Id = Contact_ContactDTO.Profession.Id,
                Code = Contact_ContactDTO.Profession.Code,
                Name = Contact_ContactDTO.Profession.Name,
            };
            Contact.Province = Contact_ContactDTO.Province == null ? null : new Province
            {
                Id = Contact_ContactDTO.Province.Id,
                Code = Contact_ContactDTO.Province.Code,
                Name = Contact_ContactDTO.Province.Name,
                Priority = Contact_ContactDTO.Province.Priority,
                StatusId = Contact_ContactDTO.Province.StatusId,
            };
            Contact.Sex = Contact_ContactDTO.Sex == null ? null : new Sex
            {
                Id = Contact_ContactDTO.Sex.Id,
                Code = Contact_ContactDTO.Sex.Code,
                Name = Contact_ContactDTO.Sex.Name,
            };
            Contact.ContactActivities = Contact_ContactDTO.ContactActivities?
                .Select(x => new ContactActivity
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
            Contact.ContactEmails = Contact_ContactDTO.ContactEmails?
                .Select(x => new ContactEmail
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
            Contact.ContactFileGroupings = Contact_ContactDTO.ContactFileGroupings?
                .Select(x => new ContactFileGrouping
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
            Contact.BaseLanguage = CurrentContext.Language;
            return Contact;
        }

        private ContactFilter ConvertFilterDTOToFilterEntity(Contact_ContactFilterDTO Contact_ContactFilterDTO)
        {
            ContactFilter ContactFilter = new ContactFilter();
            ContactFilter.Selects = ContactSelect.ALL;
            ContactFilter.Skip = Contact_ContactFilterDTO.Skip;
            ContactFilter.Take = Contact_ContactFilterDTO.Take;
            ContactFilter.OrderBy = Contact_ContactFilterDTO.OrderBy;
            ContactFilter.OrderType = Contact_ContactFilterDTO.OrderType;

            ContactFilter.Id = Contact_ContactFilterDTO.Id;
            ContactFilter.Name = Contact_ContactFilterDTO.Name;
            ContactFilter.ProfessionId = Contact_ContactFilterDTO.ProfessionId;
            ContactFilter.CompanyId = Contact_ContactFilterDTO.CompanyId;
            ContactFilter.ContactStatusId = Contact_ContactFilterDTO.ContactStatusId;
            ContactFilter.Address = Contact_ContactFilterDTO.Address;
            ContactFilter.NationId = Contact_ContactFilterDTO.NationId;
            ContactFilter.ProvinceId = Contact_ContactFilterDTO.ProvinceId;
            ContactFilter.DistrictId = Contact_ContactFilterDTO.DistrictId;
            ContactFilter.CustomerLeadId = Contact_ContactFilterDTO.CustomerLeadId;
            ContactFilter.ImageId = Contact_ContactFilterDTO.ImageId;
            ContactFilter.Description = Contact_ContactFilterDTO.Description;
            ContactFilter.EmailOther = Contact_ContactFilterDTO.EmailOther;
            ContactFilter.DateOfBirth = Contact_ContactFilterDTO.DateOfBirth;
            ContactFilter.Phone = Contact_ContactFilterDTO.Phone;
            ContactFilter.PhoneHome = Contact_ContactFilterDTO.PhoneHome;
            ContactFilter.FAX = Contact_ContactFilterDTO.FAX;
            ContactFilter.Email = Contact_ContactFilterDTO.Email;
            ContactFilter.Department = Contact_ContactFilterDTO.Department;
            ContactFilter.ZIPCode = Contact_ContactFilterDTO.ZIPCode;
            ContactFilter.SexId = Contact_ContactFilterDTO.SexId;
            ContactFilter.AppUserId = Contact_ContactFilterDTO.AppUserId;
            ContactFilter.PositionId = Contact_ContactFilterDTO.PositionId;
            ContactFilter.CreatorId = Contact_ContactFilterDTO.CreatorId;
            ContactFilter.CreatedAt = Contact_ContactFilterDTO.CreatedAt;
            ContactFilter.UpdatedAt = Contact_ContactFilterDTO.UpdatedAt;
            return ContactFilter;
        }

        #region activity
        [Route(ContactRoute.CreateActivity), HttpPost]
        public async Task<ActionResult<Contact_ContactActivityDTO>> CreateActivity([FromBody] Contact_ContactActivityDTO Contact_ContactActivityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactActivity ContactActivity = ConvertDTOToEntity(Contact_ContactActivityDTO);
            ContactActivity = await ContactActivityService.Create(ContactActivity);
            Contact_ContactActivityDTO = new Contact_ContactActivityDTO(ContactActivity);
            if (ContactActivity.IsValidated)
                return Contact_ContactActivityDTO;
            else
                return BadRequest(Contact_ContactActivityDTO);
        }

        [Route(ContactRoute.UpdateActivity), HttpPost]
        public async Task<ActionResult<Contact_ContactActivityDTO>> UpdateActivity([FromBody] Contact_ContactActivityDTO Contact_ContactActivityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactActivity ContactActivity = ConvertDTOToEntity(Contact_ContactActivityDTO);
            ContactActivity = await ContactActivityService.Update(ContactActivity);
            Contact_ContactActivityDTO = new Contact_ContactActivityDTO(ContactActivity);
            if (ContactActivity.IsValidated)
                return Contact_ContactActivityDTO;
            else
                return BadRequest(Contact_ContactActivityDTO);
        }

        [Route(ContactRoute.DeleteActivity), HttpPost]
        public async Task<ActionResult<Contact_ContactActivityDTO>> DeleteActivity([FromBody] Contact_ContactActivityDTO Contact_ContactActivityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactActivity ContactActivity = ConvertDTOToEntity(Contact_ContactActivityDTO);
            ContactActivity = await ContactActivityService.Delete(ContactActivity);
            Contact_ContactActivityDTO = new Contact_ContactActivityDTO(ContactActivity);
            if (ContactActivity.IsValidated)
                return Contact_ContactActivityDTO;
            else
                return BadRequest(Contact_ContactActivityDTO);
        }

        [Route(ContactRoute.BulkDeleteActivity), HttpPost]
        public async Task<ActionResult<bool>> BulkDeleteActivity([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactActivityFilter ContactActivityFilter = new ContactActivityFilter();
            ContactActivityFilter = await ContactActivityService.ToFilter(ContactActivityFilter);
            ContactActivityFilter.Id = new IdFilter { In = Ids };
            ContactActivityFilter.Selects = ContactActivitySelect.Id;
            ContactActivityFilter.Skip = 0;
            ContactActivityFilter.Take = int.MaxValue;

            List<ContactActivity> ContactActivities = await ContactActivityService.List(ContactActivityFilter);
            ContactActivities = await ContactActivityService.BulkDelete(ContactActivities);
            if (ContactActivities.Any(x => !x.IsValidated))
                return BadRequest(ContactActivities.Where(x => !x.IsValidated));
            return true;
        }

        private ContactActivity ConvertDTOToEntity(Contact_ContactActivityDTO Contact_ContactActivityDTO)
        {
            ContactActivity ContactActivity = new ContactActivity();
            ContactActivity.Id = Contact_ContactActivityDTO.Id;
            ContactActivity.Title = Contact_ContactActivityDTO.Title;
            ContactActivity.FromDate = Contact_ContactActivityDTO.FromDate;
            ContactActivity.ToDate = Contact_ContactActivityDTO.ToDate;
            ContactActivity.ActivityTypeId = Contact_ContactActivityDTO.ActivityTypeId;
            ContactActivity.ActivityPriorityId = Contact_ContactActivityDTO.ActivityPriorityId;
            ContactActivity.Description = Contact_ContactActivityDTO.Description;
            ContactActivity.Address = Contact_ContactActivityDTO.Address;
            ContactActivity.ContactId = Contact_ContactActivityDTO.ContactId;
            ContactActivity.AppUserId = Contact_ContactActivityDTO.AppUserId;
            ContactActivity.ActivityStatusId = Contact_ContactActivityDTO.ActivityStatusId;
            ContactActivity.ActivityPriority = Contact_ContactActivityDTO.ActivityPriority == null ? null : new ActivityPriority
            {
                Id = Contact_ContactActivityDTO.ActivityPriority.Id,
                Code = Contact_ContactActivityDTO.ActivityPriority.Code,
                Name = Contact_ContactActivityDTO.ActivityPriority.Name,
            };
            ContactActivity.ActivityStatus = Contact_ContactActivityDTO.ActivityStatus == null ? null : new ActivityStatus
            {
                Id = Contact_ContactActivityDTO.ActivityStatus.Id,
                Code = Contact_ContactActivityDTO.ActivityStatus.Code,
                Name = Contact_ContactActivityDTO.ActivityStatus.Name,
            };
            ContactActivity.ActivityType = Contact_ContactActivityDTO.ActivityType == null ? null : new ActivityType
            {
                Id = Contact_ContactActivityDTO.ActivityType.Id,
                Code = Contact_ContactActivityDTO.ActivityType.Code,
                Name = Contact_ContactActivityDTO.ActivityType.Name,
            };
            ContactActivity.AppUser = Contact_ContactActivityDTO.AppUser == null ? null : new AppUser
            {
                Id = Contact_ContactActivityDTO.AppUser.Id,
                Username = Contact_ContactActivityDTO.AppUser.Username,
                DisplayName = Contact_ContactActivityDTO.AppUser.DisplayName,
                Address = Contact_ContactActivityDTO.AppUser.Address,
                Email = Contact_ContactActivityDTO.AppUser.Email,
                Phone = Contact_ContactActivityDTO.AppUser.Phone,
                SexId = Contact_ContactActivityDTO.AppUser.SexId,
                Birthday = Contact_ContactActivityDTO.AppUser.Birthday,
                Department = Contact_ContactActivityDTO.AppUser.Department,
                OrganizationId = Contact_ContactActivityDTO.AppUser.OrganizationId,
                StatusId = Contact_ContactActivityDTO.AppUser.StatusId,
            };
            ContactActivity.BaseLanguage = CurrentContext.Language;
            return ContactActivity;
        }
        #endregion

        #region callLog
        [Route(ContactRoute.DeleteCallLog), HttpPost]
        public async Task<ActionResult<Contact_CallLogDTO>> DeleteCallLog([FromBody] Contact_CallLogDTO Contact_CallLogDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Contact_CallLogDTO.Id))
                return Forbid();

            CallLog CallLog = ConvertCallLog(Contact_CallLogDTO);
            CallLog = await CallLogService.Delete(CallLog);
            Contact_CallLogDTO = new Contact_CallLogDTO(CallLog);
            if (CallLog.IsValidated)
                return Contact_CallLogDTO;
            else
                return BadRequest(Contact_CallLogDTO);
        }

        private CallLog ConvertCallLog(Contact_CallLogDTO Contact_CallLogDTO)
        {
            CallLog CallLog = new CallLog();
            CallLog.Id = Contact_CallLogDTO.Id;
            CallLog.EntityReferenceId = Contact_CallLogDTO.EntityReferenceId;
            CallLog.CallTypeId = Contact_CallLogDTO.CallTypeId;
            CallLog.CallEmotionId = Contact_CallLogDTO.CallEmotionId;
            CallLog.AppUserId = Contact_CallLogDTO.AppUserId;
            CallLog.Title = Contact_CallLogDTO.Title;
            CallLog.Content = Contact_CallLogDTO.Content;
            CallLog.Phone = Contact_CallLogDTO.Phone;
            CallLog.CallTime = Contact_CallLogDTO.CallTime;
            CallLog.AppUser = Contact_CallLogDTO.AppUser == null ? null : new AppUser
            {
                Id = Contact_CallLogDTO.AppUser.Id,
                Username = Contact_CallLogDTO.AppUser.Username,
                DisplayName = Contact_CallLogDTO.AppUser.DisplayName,
                Address = Contact_CallLogDTO.AppUser.Address,
                Email = Contact_CallLogDTO.AppUser.Email,
                Phone = Contact_CallLogDTO.AppUser.Phone,
                SexId = Contact_CallLogDTO.AppUser.SexId,
                Birthday = Contact_CallLogDTO.AppUser.Birthday,
                Department = Contact_CallLogDTO.AppUser.Department,
                OrganizationId = Contact_CallLogDTO.AppUser.OrganizationId,
                StatusId = Contact_CallLogDTO.AppUser.StatusId,
            };
            CallLog.EntityReference = Contact_CallLogDTO.EntityReference == null ? null : new EntityReference
            {
                Id = Contact_CallLogDTO.EntityReference.Id,
                Code = Contact_CallLogDTO.EntityReference.Code,
                Name = Contact_CallLogDTO.EntityReference.Name,
            };
            CallLog.CallType = Contact_CallLogDTO.CallType == null ? null : new CallType
            {
                Id = Contact_CallLogDTO.CallType.Id,
                Code = Contact_CallLogDTO.CallType.Code,
                Name = Contact_CallLogDTO.CallType.Name,
                ColorCode = Contact_CallLogDTO.CallType.ColorCode,
                StatusId = Contact_CallLogDTO.CallType.StatusId,
                Used = Contact_CallLogDTO.CallType.Used,
            };
            CallLog.CallEmotion = Contact_CallLogDTO.CallEmotion == null ? null : new CallEmotion
            {
                Id = Contact_CallLogDTO.CallEmotion.Id,
                Name = Contact_CallLogDTO.CallEmotion.Name,
                Code = Contact_CallLogDTO.CallEmotion.Code,
                StatusId = Contact_CallLogDTO.CallEmotion.StatusId,
                Description = Contact_CallLogDTO.CallEmotion.Description,
            };
            CallLog.BaseLanguage = CurrentContext.Language;
            return CallLog;
        }
        #endregion

        #region Email
        [Route(ContactRoute.CreateEmail), HttpPost]
        public async Task<ActionResult<Contact_ContactEmailDTO>> CreateEmail([FromBody] Contact_ContactEmailDTO Contact_ContactEmailDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContactEmail ContactEmail = ConvertContactEmail(Contact_ContactEmailDTO);
            ContactEmail = await ContactEmailService.Create(ContactEmail);
            Contact_ContactEmailDTO = new Contact_ContactEmailDTO(ContactEmail);
            if (ContactEmail.IsValidated)
                return Contact_ContactEmailDTO;
            else
                return BadRequest(Contact_ContactEmailDTO);
        }

        [Route(ContactRoute.SendEmail), HttpPost]
        public async Task<ActionResult<bool>> SendEmail([FromBody] Contact_ContactEmailDTO Contact_ContactEmailDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            ContactEmail ContactEmail = ConvertContactEmail(Contact_ContactEmailDTO);
            ContactEmail = await ContactEmailService.Send(ContactEmail);
            if (ContactEmail.IsValidated)
                return Ok();
            else
                return BadRequest(Contact_ContactEmailDTO);
        }

        private ContactEmail ConvertContactEmail(Contact_ContactEmailDTO Contact_ContactEmailDTO)
        {
            ContactEmail ContactEmail = new ContactEmail();
            ContactEmail.Id = Contact_ContactEmailDTO.Id;
            ContactEmail.Reciepient = Contact_ContactEmailDTO.Reciepient;
            ContactEmail.Title = Contact_ContactEmailDTO.Title;
            ContactEmail.Content = Contact_ContactEmailDTO.Content;
            ContactEmail.CreatorId = Contact_ContactEmailDTO.CreatorId;
            ContactEmail.ContactId = Contact_ContactEmailDTO.ContactId;
            ContactEmail.EmailStatusId = Contact_ContactEmailDTO.EmailStatusId;
            ContactEmail.EmailStatus = Contact_ContactEmailDTO.EmailStatus == null ? null : new EmailStatus
            {
                Id = Contact_ContactEmailDTO.EmailStatus.Id,
                Code = Contact_ContactEmailDTO.EmailStatus.Code,
                Name = Contact_ContactEmailDTO.EmailStatus.Name,
            };
            ContactEmail.ContactEmailCCMappings = Contact_ContactEmailDTO.ContactEmailCCMappings?.Select(x => new ContactEmailCCMapping
            {
                AppUserId = x.AppUserId,
                ContactEmailId = x.ContactEmailId,
                AppUser = x.AppUser == null ? null : new AppUser
                {
                    Id = x.AppUser.Id,
                    Username = x.AppUser.Username,
                    DisplayName = x.AppUser.DisplayName,
                    Email = x.AppUser.Email,
                },
            }).ToList();
            ContactEmail.BaseLanguage = CurrentContext.Language;
            return ContactEmail;
        }
        #endregion

        #region opportunity
        [Route(ContactRoute.CreateOpportunity), HttpPost]
        public async Task<ActionResult<Contact_OpportunityDTO>> CreateOpportunity([FromBody] Contact_OpportunityDTO Contact_OpportunityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Opportunity Opportunity = ConvertOpportunity(Contact_OpportunityDTO);
            Opportunity = await OpportunityService.Create(Opportunity);
            Contact_OpportunityDTO = new Contact_OpportunityDTO(Opportunity);
            if (Opportunity.IsValidated)
                return Contact_OpportunityDTO;
            else
                return BadRequest(Contact_OpportunityDTO);
        }

        [Route(ContactRoute.UpdateOpportunity), HttpPost]
        public async Task<ActionResult<Contact_OpportunityDTO>> UpdateOpportunity([FromBody] Contact_OpportunityDTO Contact_OpportunityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Contact_OpportunityDTO.Id))
                return Forbid();

            Opportunity Opportunity = ConvertOpportunity(Contact_OpportunityDTO);
            Opportunity = await OpportunityService.Update(Opportunity);
            Contact_OpportunityDTO = new Contact_OpportunityDTO(Opportunity);
            if (Opportunity.IsValidated)
                return Contact_OpportunityDTO;
            else
                return BadRequest(Contact_OpportunityDTO);
        }

        [Route(ContactRoute.BulkDeleteOpportunity), HttpPost]
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

        [Route(ContactRoute.DeleteOpportunity), HttpPost]
        public async Task<ActionResult<Contact_OpportunityDTO>> DeleteOpportunity([FromBody] Contact_OpportunityDTO Contact_OpportunityDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Contact_OpportunityDTO.Id))
                return Forbid();

            Opportunity Opportunity = ConvertOpportunity(Contact_OpportunityDTO);
            Opportunity = await OpportunityService.Delete(Opportunity);
            Contact_OpportunityDTO = new Contact_OpportunityDTO(Opportunity);
            if (Opportunity.IsValidated)
                return Contact_OpportunityDTO;
            else
                return BadRequest(Contact_OpportunityDTO);
        }

        private Opportunity ConvertOpportunity(Contact_OpportunityDTO Contact_OpportunityDTO)
        {
            Opportunity Opportunity = new Opportunity();
            Opportunity.Id = Contact_OpportunityDTO.Id;
            Opportunity.Name = Contact_OpportunityDTO.Name;
            Opportunity.CompanyId = Contact_OpportunityDTO.CompanyId;
            Opportunity.CustomerLeadId = Contact_OpportunityDTO.CustomerLeadId;
            Opportunity.ClosingDate = Contact_OpportunityDTO.ClosingDate;
            Opportunity.SaleStageId = Contact_OpportunityDTO.SaleStageId;
            Opportunity.ProbabilityId = Contact_OpportunityDTO.ProbabilityId;
            Opportunity.PotentialResultId = Contact_OpportunityDTO.PotentialResultId;
            Opportunity.LeadSourceId = Contact_OpportunityDTO.LeadSourceId;
            Opportunity.AppUserId = Contact_OpportunityDTO.AppUserId;
            Opportunity.CurrencyId = Contact_OpportunityDTO.CurrencyId;
            Opportunity.Amount = Contact_OpportunityDTO.Amount;
            Opportunity.ForecastAmount = Contact_OpportunityDTO.ForecastAmount;
            Opportunity.Description = Contact_OpportunityDTO.Description;
            Opportunity.RefuseReciveSMS = Contact_OpportunityDTO.RefuseReciveSMS;
            Opportunity.RefuseReciveEmail = Contact_OpportunityDTO.RefuseReciveEmail;
            Opportunity.OpportunityResultTypeId = Contact_OpportunityDTO.OpportunityResultTypeId;
            Opportunity.CreatorId = Contact_OpportunityDTO.CreatorId;
            Opportunity.AppUser = Contact_OpportunityDTO.AppUser == null ? null : new AppUser
            {
                Id = Contact_OpportunityDTO.AppUser.Id,
                Username = Contact_OpportunityDTO.AppUser.Username,
                DisplayName = Contact_OpportunityDTO.AppUser.DisplayName,
                Address = Contact_OpportunityDTO.AppUser.Address,
                Email = Contact_OpportunityDTO.AppUser.Email,
                Phone = Contact_OpportunityDTO.AppUser.Phone,
                SexId = Contact_OpportunityDTO.AppUser.SexId,
                Birthday = Contact_OpportunityDTO.AppUser.Birthday,
                Avatar = Contact_OpportunityDTO.AppUser.Avatar,
                Department = Contact_OpportunityDTO.AppUser.Department,
                OrganizationId = Contact_OpportunityDTO.AppUser.OrganizationId,
                Longitude = Contact_OpportunityDTO.AppUser.Longitude,
                Latitude = Contact_OpportunityDTO.AppUser.Latitude,
                StatusId = Contact_OpportunityDTO.AppUser.StatusId,
            };
            Opportunity.Company = Contact_OpportunityDTO.Company == null ? null : new Company
            {
                Id = Contact_OpportunityDTO.Company.Id,
                Name = Contact_OpportunityDTO.Company.Name,
                Phone = Contact_OpportunityDTO.Company.Phone,
                FAX = Contact_OpportunityDTO.Company.FAX,
                PhoneOther = Contact_OpportunityDTO.Company.PhoneOther,
                Email = Contact_OpportunityDTO.Company.Email,
                EmailOther = Contact_OpportunityDTO.Company.EmailOther,
                ZIPCode = Contact_OpportunityDTO.Company.ZIPCode,
                Revenue = Contact_OpportunityDTO.Company.Revenue,
                Website = Contact_OpportunityDTO.Company.Website,
                Address = Contact_OpportunityDTO.Company.Address,
                NationId = Contact_OpportunityDTO.Company.NationId,
                ProvinceId = Contact_OpportunityDTO.Company.ProvinceId,
                DistrictId = Contact_OpportunityDTO.Company.DistrictId,
                NumberOfEmployee = Contact_OpportunityDTO.Company.NumberOfEmployee,
                RefuseReciveEmail = Contact_OpportunityDTO.Company.RefuseReciveEmail,
                RefuseReciveSMS = Contact_OpportunityDTO.Company.RefuseReciveSMS,
                CustomerLeadId = Contact_OpportunityDTO.Company.CustomerLeadId,
                ParentId = Contact_OpportunityDTO.Company.ParentId,
                Path = Contact_OpportunityDTO.Company.Path,
                Level = Contact_OpportunityDTO.Company.Level,
                ProfessionId = Contact_OpportunityDTO.Company.ProfessionId,
                AppUserId = Contact_OpportunityDTO.Company.AppUserId,
                CreatorId = Contact_OpportunityDTO.Company.CreatorId,
                CurrencyId = Contact_OpportunityDTO.Company.CurrencyId,
                CompanyStatusId = Contact_OpportunityDTO.Company.CompanyStatusId,
                Description = Contact_OpportunityDTO.Company.Description,
                RowId = Contact_OpportunityDTO.Company.RowId,
            };
            Opportunity.Currency = Contact_OpportunityDTO.Currency == null ? null : new Currency
            {
                Id = Contact_OpportunityDTO.Currency.Id,
                Code = Contact_OpportunityDTO.Currency.Code,
                Name = Contact_OpportunityDTO.Currency.Name,
            };
            Opportunity.CustomerLead = Contact_OpportunityDTO.CustomerLead == null ? null : new CustomerLead
            {
                Id = Contact_OpportunityDTO.CustomerLead.Id,
                Name = Contact_OpportunityDTO.CustomerLead.Name,
                CompanyName = Contact_OpportunityDTO.CustomerLead.CompanyName,
                TelePhone = Contact_OpportunityDTO.CustomerLead.TelePhone,
                Phone = Contact_OpportunityDTO.CustomerLead.Phone,
                Fax = Contact_OpportunityDTO.CustomerLead.Fax,
                Email = Contact_OpportunityDTO.CustomerLead.Email,
                SecondEmail = Contact_OpportunityDTO.CustomerLead.SecondEmail,
                Website = Contact_OpportunityDTO.CustomerLead.Website,
                CustomerLeadSourceId = Contact_OpportunityDTO.CustomerLead.CustomerLeadSourceId,
                CustomerLeadLevelId = Contact_OpportunityDTO.CustomerLead.CustomerLeadLevelId,
                CompanyId = Contact_OpportunityDTO.CustomerLead.CompanyId,
                CampaignId = Contact_OpportunityDTO.CustomerLead.CampaignId,
                ProfessionId = Contact_OpportunityDTO.CustomerLead.ProfessionId,
                Revenue = Contact_OpportunityDTO.CustomerLead.Revenue,
                EmployeeQuantity = Contact_OpportunityDTO.CustomerLead.EmployeeQuantity,
                Address = Contact_OpportunityDTO.CustomerLead.Address,
                NationId = Contact_OpportunityDTO.CustomerLead.NationId,
                ProvinceId = Contact_OpportunityDTO.CustomerLead.ProvinceId,
                DistrictId = Contact_OpportunityDTO.CustomerLead.DistrictId,
                CustomerLeadStatusId = Contact_OpportunityDTO.CustomerLead.CustomerLeadStatusId,
                BusinessRegistrationCode = Contact_OpportunityDTO.CustomerLead.BusinessRegistrationCode,
                SexId = Contact_OpportunityDTO.CustomerLead.SexId,
                RefuseReciveSMS = Contact_OpportunityDTO.CustomerLead.RefuseReciveSMS,
                RefuseReciveEmail = Contact_OpportunityDTO.CustomerLead.RefuseReciveEmail,
                Description = Contact_OpportunityDTO.CustomerLead.Description,
                AppUserId = Contact_OpportunityDTO.CustomerLead.AppUserId,
                CreatorId = Contact_OpportunityDTO.CustomerLead.CreatorId,
                ZipCode = Contact_OpportunityDTO.CustomerLead.ZipCode,
                CurrencyId = Contact_OpportunityDTO.CustomerLead.CurrencyId,
                RowId = Contact_OpportunityDTO.CustomerLead.RowId,
            };
            Opportunity.LeadSource = Contact_OpportunityDTO.LeadSource == null ? null : new CustomerLeadSource
            {
                Id = Contact_OpportunityDTO.LeadSource.Id,
                Code = Contact_OpportunityDTO.LeadSource.Code,
                Name = Contact_OpportunityDTO.LeadSource.Name,
            };
            Opportunity.OpportunityResultType = Contact_OpportunityDTO.OpportunityResultType == null ? null : new OpportunityResultType
            {
                Id = Contact_OpportunityDTO.OpportunityResultType.Id,
                Code = Contact_OpportunityDTO.OpportunityResultType.Code,
                Name = Contact_OpportunityDTO.OpportunityResultType.Name,
            };
            Opportunity.PotentialResult = Contact_OpportunityDTO.PotentialResult == null ? null : new PotentialResult
            {
                Id = Contact_OpportunityDTO.PotentialResult.Id,
                Code = Contact_OpportunityDTO.PotentialResult.Code,
                Name = Contact_OpportunityDTO.PotentialResult.Name,
            };
            Opportunity.Probability = Contact_OpportunityDTO.Probability == null ? null : new Probability
            {
                Id = Contact_OpportunityDTO.Probability.Id,
                Code = Contact_OpportunityDTO.Probability.Code,
                Name = Contact_OpportunityDTO.Probability.Name,
            };
            Opportunity.SaleStage = Contact_OpportunityDTO.SaleStage == null ? null : new SaleStage
            {
                Id = Contact_OpportunityDTO.SaleStage.Id,
                Code = Contact_OpportunityDTO.SaleStage.Code,
                Name = Contact_OpportunityDTO.SaleStage.Name,
            };

            Opportunity.BaseLanguage = CurrentContext.Language;
            return Opportunity;
        }
        #endregion

        #region orderQuote
        [Route(ContactRoute.CreateOrderQuote), HttpPost]
        public async Task<ActionResult<Contact_OrderQuoteDTO>> CreateOrderQuote([FromBody] Contact_OrderQuoteDTO Contact_OrderQuoteDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuote OrderQuote = ConvertOrderQuote(Contact_OrderQuoteDTO);
            OrderQuote = await OrderQuoteService.Create(OrderQuote);
            Contact_OrderQuoteDTO = new Contact_OrderQuoteDTO(OrderQuote);
            if (OrderQuote.IsValidated)
                return Contact_OrderQuoteDTO;
            else
                return BadRequest(Contact_OrderQuoteDTO);
        }

        private OrderQuote ConvertOrderQuote(Contact_OrderQuoteDTO Contact_OrderQuoteDTO)
        {
            OrderQuote OrderQuote = new OrderQuote();
            OrderQuote.Id = Contact_OrderQuoteDTO.Id;
            OrderQuote.Subject = Contact_OrderQuoteDTO.Subject;
            OrderQuote.NationId = Contact_OrderQuoteDTO.NationId;
            OrderQuote.ProvinceId = Contact_OrderQuoteDTO.ProvinceId;
            OrderQuote.DistrictId = Contact_OrderQuoteDTO.DistrictId;
            OrderQuote.Address = Contact_OrderQuoteDTO.Address;
            OrderQuote.InvoiceAddress = Contact_OrderQuoteDTO.InvoiceAddress;
            OrderQuote.InvoiceProvinceId = Contact_OrderQuoteDTO.InvoiceProvinceId;
            OrderQuote.InvoiceDistrictId = Contact_OrderQuoteDTO.InvoiceDistrictId;
            OrderQuote.InvoiceNationId = Contact_OrderQuoteDTO.InvoiceNationId;
            OrderQuote.EditedPriceStatusId = Contact_OrderQuoteDTO.EditedPriceStatusId;
            OrderQuote.ZIPCode = Contact_OrderQuoteDTO.ZIPCode;
            OrderQuote.InvoiceZIPCode = Contact_OrderQuoteDTO.InvoiceZIPCode;
            OrderQuote.AppUserId = Contact_OrderQuoteDTO.AppUserId;
            OrderQuote.ContactId = Contact_OrderQuoteDTO.ContactId;
            OrderQuote.CompanyId = Contact_OrderQuoteDTO.CompanyId;
            OrderQuote.OpportunityId = Contact_OrderQuoteDTO.OpportunityId;
            OrderQuote.OrderQuoteStatusId = Contact_OrderQuoteDTO.OrderQuoteStatusId;
            OrderQuote.SubTotal = Contact_OrderQuoteDTO.SubTotal;
            OrderQuote.Total = Contact_OrderQuoteDTO.Total;
            OrderQuote.TotalTaxAmount = Contact_OrderQuoteDTO.TotalTaxAmount;
            OrderQuote.TotalTaxAmountOther = Contact_OrderQuoteDTO.TotalTaxAmountOther;
            OrderQuote.GeneralDiscountPercentage = Contact_OrderQuoteDTO.GeneralDiscountPercentage;
            OrderQuote.GeneralDiscountAmount = Contact_OrderQuoteDTO.GeneralDiscountAmount;
            OrderQuote.EditedPriceStatus = Contact_OrderQuoteDTO.EditedPriceStatus == null ? null : new EditedPriceStatus
            {
                Id = Contact_OrderQuoteDTO.EditedPriceStatus.Id,
                Code = Contact_OrderQuoteDTO.EditedPriceStatus.Code,
                Name = Contact_OrderQuoteDTO.EditedPriceStatus.Name,
            };
            OrderQuote.Company = Contact_OrderQuoteDTO.Company == null ? null : new Company
            {
                Id = Contact_OrderQuoteDTO.Company.Id,
                Name = Contact_OrderQuoteDTO.Company.Name,
                Phone = Contact_OrderQuoteDTO.Company.Phone,
                FAX = Contact_OrderQuoteDTO.Company.FAX,
                PhoneOther = Contact_OrderQuoteDTO.Company.PhoneOther,
                Email = Contact_OrderQuoteDTO.Company.Email,
                EmailOther = Contact_OrderQuoteDTO.Company.EmailOther,
                Description = Contact_OrderQuoteDTO.Company.Description,
            };
            OrderQuote.Contact = Contact_OrderQuoteDTO.Contact == null ? null : new Contact
            {
                Id = Contact_OrderQuoteDTO.Contact.Id,
                Name = Contact_OrderQuoteDTO.Contact.Name,
                ProfessionId = Contact_OrderQuoteDTO.Contact.ProfessionId,
                CompanyId = Contact_OrderQuoteDTO.Contact.CompanyId,
                RefuseReciveEmail = Contact_OrderQuoteDTO.Contact.RefuseReciveEmail,
                RefuseReciveSMS = Contact_OrderQuoteDTO.Contact.RefuseReciveSMS,
            };
            OrderQuote.District = Contact_OrderQuoteDTO.District == null ? null : new District
            {
                Id = Contact_OrderQuoteDTO.District.Id,
                Code = Contact_OrderQuoteDTO.District.Code,
                Name = Contact_OrderQuoteDTO.District.Name,
                Priority = Contact_OrderQuoteDTO.District.Priority,
                ProvinceId = Contact_OrderQuoteDTO.District.ProvinceId,
                StatusId = Contact_OrderQuoteDTO.District.StatusId,
            };
            OrderQuote.InvoiceDistrict = Contact_OrderQuoteDTO.InvoiceDistrict == null ? null : new District
            {
                Id = Contact_OrderQuoteDTO.InvoiceDistrict.Id,
                Code = Contact_OrderQuoteDTO.InvoiceDistrict.Code,
                Name = Contact_OrderQuoteDTO.InvoiceDistrict.Name,
                Priority = Contact_OrderQuoteDTO.InvoiceDistrict.Priority,
                ProvinceId = Contact_OrderQuoteDTO.InvoiceDistrict.ProvinceId,
                StatusId = Contact_OrderQuoteDTO.InvoiceDistrict.StatusId,
            };
            OrderQuote.InvoiceNation = Contact_OrderQuoteDTO.InvoiceNation == null ? null : new Nation
            {
                Id = Contact_OrderQuoteDTO.InvoiceNation.Id,
                Code = Contact_OrderQuoteDTO.InvoiceNation.Code,
                Name = Contact_OrderQuoteDTO.InvoiceNation.Name,
                Priority = Contact_OrderQuoteDTO.InvoiceNation.DisplayOrder,
                StatusId = Contact_OrderQuoteDTO.InvoiceNation.StatusId,
            };
            OrderQuote.InvoiceProvince = Contact_OrderQuoteDTO.InvoiceProvince == null ? null : new Province
            {
                Id = Contact_OrderQuoteDTO.InvoiceProvince.Id,
                Code = Contact_OrderQuoteDTO.InvoiceProvince.Code,
                Name = Contact_OrderQuoteDTO.InvoiceProvince.Name,
                Priority = Contact_OrderQuoteDTO.InvoiceProvince.Priority,
                StatusId = Contact_OrderQuoteDTO.InvoiceProvince.StatusId,
            };
            OrderQuote.Nation = Contact_OrderQuoteDTO.Nation == null ? null : new Nation
            {
                Id = Contact_OrderQuoteDTO.Nation.Id,
                Code = Contact_OrderQuoteDTO.Nation.Code,
                Name = Contact_OrderQuoteDTO.Nation.Name,
                Priority = Contact_OrderQuoteDTO.Nation.DisplayOrder,
                StatusId = Contact_OrderQuoteDTO.Nation.StatusId,
            };
            OrderQuote.Opportunity = Contact_OrderQuoteDTO.Opportunity == null ? null : new Opportunity
            {
                Id = Contact_OrderQuoteDTO.Opportunity.Id,
                Name = Contact_OrderQuoteDTO.Opportunity.Name,
                CompanyId = Contact_OrderQuoteDTO.Opportunity.CompanyId,
                CustomerLeadId = Contact_OrderQuoteDTO.Opportunity.CustomerLeadId,
                ClosingDate = Contact_OrderQuoteDTO.Opportunity.ClosingDate,
                SaleStageId = Contact_OrderQuoteDTO.Opportunity.SaleStageId,
                ProbabilityId = Contact_OrderQuoteDTO.Opportunity.ProbabilityId,
                PotentialResultId = Contact_OrderQuoteDTO.Opportunity.PotentialResultId,
                LeadSourceId = Contact_OrderQuoteDTO.Opportunity.LeadSourceId,
                AppUserId = Contact_OrderQuoteDTO.Opportunity.AppUserId,
                CurrencyId = Contact_OrderQuoteDTO.Opportunity.CurrencyId,
                Amount = Contact_OrderQuoteDTO.Opportunity.Amount,
                ForecastAmount = Contact_OrderQuoteDTO.Opportunity.ForecastAmount,
                Description = Contact_OrderQuoteDTO.Opportunity.Description,
                RefuseReciveSMS = Contact_OrderQuoteDTO.Opportunity.RefuseReciveSMS,
                RefuseReciveEmail = Contact_OrderQuoteDTO.Opportunity.RefuseReciveEmail,
                OpportunityResultTypeId = Contact_OrderQuoteDTO.Opportunity.OpportunityResultTypeId,
                CreatorId = Contact_OrderQuoteDTO.Opportunity.CreatorId,
            };
            OrderQuote.OrderQuoteStatus = Contact_OrderQuoteDTO.OrderQuoteStatus == null ? null : new OrderQuoteStatus
            {
                Id = Contact_OrderQuoteDTO.OrderQuoteStatus.Id,
                Code = Contact_OrderQuoteDTO.OrderQuoteStatus.Code,
                Name = Contact_OrderQuoteDTO.OrderQuoteStatus.Name,
            };
            OrderQuote.Province = Contact_OrderQuoteDTO.Province == null ? null : new Province
            {
                Id = Contact_OrderQuoteDTO.Province.Id,
                Code = Contact_OrderQuoteDTO.Province.Code,
                Name = Contact_OrderQuoteDTO.Province.Name,
                Priority = Contact_OrderQuoteDTO.Province.Priority,
                StatusId = Contact_OrderQuoteDTO.Province.StatusId,
            };
            OrderQuote.AppUser = Contact_OrderQuoteDTO.AppUser == null ? null : new AppUser
            {
                Id = Contact_OrderQuoteDTO.AppUser.Id,
                Username = Contact_OrderQuoteDTO.AppUser.Username,
                DisplayName = Contact_OrderQuoteDTO.AppUser.DisplayName,
                Address = Contact_OrderQuoteDTO.AppUser.Address,
                Email = Contact_OrderQuoteDTO.AppUser.Email,
                Phone = Contact_OrderQuoteDTO.AppUser.Phone,
                SexId = Contact_OrderQuoteDTO.AppUser.SexId,
                Birthday = Contact_OrderQuoteDTO.AppUser.Birthday,
                Avatar = Contact_OrderQuoteDTO.AppUser.Avatar,
                PositionId = Contact_OrderQuoteDTO.AppUser.PositionId,
                Department = Contact_OrderQuoteDTO.AppUser.Department,
                OrganizationId = Contact_OrderQuoteDTO.AppUser.OrganizationId,
                ProvinceId = Contact_OrderQuoteDTO.AppUser.ProvinceId,
                Longitude = Contact_OrderQuoteDTO.AppUser.Longitude,
                Latitude = Contact_OrderQuoteDTO.AppUser.Latitude,
                StatusId = Contact_OrderQuoteDTO.AppUser.StatusId,
            };
            OrderQuote.BaseLanguage = CurrentContext.Language;
            return OrderQuote;
        }
        #endregion

        #region sms
        [Route(ContactRoute.SendSms), HttpPost]
        public async Task<ActionResult<bool>> SendSms([FromBody] Contact_SmsQueueDTO Contact_SmsQueueDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            SmsQueue SmsQueue = new SmsQueue();
            SmsQueue.Id = Contact_SmsQueueDTO.Id;
            SmsQueue.Phone = Contact_SmsQueueDTO.Phone;
            SmsQueue.SmsCode = Contact_SmsQueueDTO.SmsCode;
            SmsQueue.SmsTitle = Contact_SmsQueueDTO.SmsTitle;
            SmsQueue.SmsContent = Contact_SmsQueueDTO.SmsContent;
            SmsQueue.SentByAppUserId = CurrentContext.UserId;
            SmsQueue.SmsQueueStatusId = Contact_SmsQueueDTO.SmsQueueStatusId;
            SmsQueue.SmsQueueStatus = Contact_SmsQueueDTO.SmsQueueStatus == null ? null : new SmsQueueStatus
            {
                Id = Contact_SmsQueueDTO.SmsQueueStatus.Id,
                Code = Contact_SmsQueueDTO.SmsQueueStatus.Code,
                Name = Contact_SmsQueueDTO.SmsQueueStatus.Name,
            };
            SmsQueue.BaseLanguage = CurrentContext.Language;

            SmsQueue.EntityReferenceId = Contact_SmsQueueDTO.EntityReferenceId;
            return true;
        }
        #endregion
    }
}

