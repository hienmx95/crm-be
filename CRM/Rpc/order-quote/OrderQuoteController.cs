using CRM.Common;
using CRM.Entities;
using CRM.Services.MCompany;
using CRM.Services.MAppUser;
using CRM.Services.MContact;
using CRM.Services.MDistrict;
using CRM.Services.MEditedPriceStatus;
using CRM.Services.MNation;
using CRM.Services.MOpportunity;
using CRM.Services.MOrderQuote;
using CRM.Services.MOrderQuoteStatus;
using CRM.Services.MOrganization;
using CRM.Services.MProduct;
using CRM.Services.MProductGrouping;
using CRM.Services.MProductType;
using CRM.Services.MProvince;
using CRM.Services.MSupplier;
using CRM.Services.MTaxType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using CRM.Helpers;
using CRM.Services.MUnitOfMeasure;
using CRM.Models;

namespace CRM.Rpc.order_quote
{
    public partial class OrderQuoteController : RpcController
    {
        private ICompanyService CompanyService;
        private IContactService ContactService;
        private IDistrictService DistrictService;
        private INationService NationService;
        private IProvinceService ProvinceService;
        private IOpportunityService OpportunityService;
        private IOrderQuoteStatusService OrderQuoteStatusService;
        private IAppUserService AppUserService;
        private IOrderQuoteService OrderQuoteService;
        private IOrganizationService OrganizationService;
        private IItemService ItemService;
        private IProductService ProductService;
        private IProductGroupingService ProductGroupingService;
        private IProductTypeService ProductTypeService;
        private ISupplierService SupplierService;
        private ITaxTypeService TaxTypeService;
        private IEditedPriceStatusService EditedPriceStatusService;
        private IUnitOfMeasureService UnitOfMeasureService;
        private ICurrentContext CurrentContext;
        public OrderQuoteController(
            ICompanyService CompanyService,
            IContactService ContactService,
            IDistrictService DistrictService,
            INationService NationService,
            IProvinceService ProvinceService,
            IOpportunityService OpportunityService,
            IOrderQuoteStatusService OrderQuoteStatusService,
            IOrganizationService OrganizationService,
            IAppUserService AppUserService,
            IOrderQuoteService OrderQuoteService,
            IItemService ItemService,
            IProductService ProductService,
            IProductGroupingService ProductGroupingService,
            IProductTypeService ProductTypeService,
            ISupplierService SupplierService,
            ITaxTypeService TaxTypeService,
            IEditedPriceStatusService EditedPriceStatusService,
            IUnitOfMeasureService UnitOfMeasureService,
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.CompanyService = CompanyService;
            this.ContactService = ContactService;
            this.DistrictService = DistrictService;
            this.NationService = NationService;
            this.ProvinceService = ProvinceService;
            this.OpportunityService = OpportunityService;
            this.OrderQuoteStatusService = OrderQuoteStatusService;
            this.OrganizationService = OrganizationService;
            this.AppUserService = AppUserService;
            this.OrderQuoteService = OrderQuoteService;
            this.ItemService = ItemService;
            this.ProductService = ProductService;
            this.ProductGroupingService = ProductGroupingService;
            this.ProductTypeService = ProductTypeService;
            this.SupplierService = SupplierService;
            this.TaxTypeService = TaxTypeService;
            this.EditedPriceStatusService = EditedPriceStatusService;
            this.UnitOfMeasureService = UnitOfMeasureService;
            this.CurrentContext = CurrentContext;
        }

        [Route(OrderQuoteRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] OrderQuote_OrderQuoteFilterDTO OrderQuote_OrderQuoteFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuoteFilter OrderQuoteFilter = ConvertFilterDTOToFilterEntity(OrderQuote_OrderQuoteFilterDTO);
            OrderQuoteFilter = await OrderQuoteService.ToFilter(OrderQuoteFilter);
            int count = await OrderQuoteService.Count(OrderQuoteFilter);
            return count;
        }

        [Route(OrderQuoteRoute.List), HttpPost]
        public async Task<ActionResult<List<OrderQuote_OrderQuoteDTO>>> List([FromBody] OrderQuote_OrderQuoteFilterDTO OrderQuote_OrderQuoteFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            OrderQuoteFilter OrderQuoteFilter = ConvertFilterDTOToFilterEntity(OrderQuote_OrderQuoteFilterDTO);
            OrderQuoteFilter = await OrderQuoteService.ToFilter(OrderQuoteFilter);
            List<OrderQuote> OrderQuotes = await OrderQuoteService.List(OrderQuoteFilter);
            List<OrderQuote_OrderQuoteDTO> OrderQuote_OrderQuoteDTOs = OrderQuotes
                .Select(c => new OrderQuote_OrderQuoteDTO(c)).ToList();
            return OrderQuote_OrderQuoteDTOs;
        }

        [Route(OrderQuoteRoute.Get), HttpPost]
        public async Task<ActionResult<OrderQuote_OrderQuoteDTO>> Get([FromBody] OrderQuote_OrderQuoteDTO OrderQuote_OrderQuoteDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(OrderQuote_OrderQuoteDTO.Id))
                return Forbid();

            OrderQuote OrderQuote = await OrderQuoteService.Get(OrderQuote_OrderQuoteDTO.Id);
            return new OrderQuote_OrderQuoteDTO(OrderQuote);
        }

        [Route(OrderQuoteRoute.Create), HttpPost]
        public async Task<ActionResult<OrderQuote_OrderQuoteDTO>> Create([FromBody] OrderQuote_OrderQuoteDTO OrderQuote_OrderQuoteDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(OrderQuote_OrderQuoteDTO.Id))
                return Forbid();

            OrderQuote OrderQuote = ConvertDTOToEntity(OrderQuote_OrderQuoteDTO);
            OrderQuote = await OrderQuoteService.Create(OrderQuote);
            OrderQuote_OrderQuoteDTO = new OrderQuote_OrderQuoteDTO(OrderQuote);
            if (OrderQuote.IsValidated)
                return OrderQuote_OrderQuoteDTO;
            else
                return BadRequest(OrderQuote_OrderQuoteDTO);
        }

        [Route(OrderQuoteRoute.Update), HttpPost]
        public async Task<ActionResult<OrderQuote_OrderQuoteDTO>> Update([FromBody] OrderQuote_OrderQuoteDTO OrderQuote_OrderQuoteDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(OrderQuote_OrderQuoteDTO.Id))
                return Forbid();

            OrderQuote OrderQuote = ConvertDTOToEntity(OrderQuote_OrderQuoteDTO);
            OrderQuote = await OrderQuoteService.Update(OrderQuote);
            OrderQuote_OrderQuoteDTO = new OrderQuote_OrderQuoteDTO(OrderQuote);
            if (OrderQuote.IsValidated)
                return OrderQuote_OrderQuoteDTO;
            else
                return BadRequest(OrderQuote_OrderQuoteDTO);
        }

        [Route(OrderQuoteRoute.Delete), HttpPost]
        public async Task<ActionResult<OrderQuote_OrderQuoteDTO>> Delete([FromBody] OrderQuote_OrderQuoteDTO OrderQuote_OrderQuoteDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(OrderQuote_OrderQuoteDTO.Id))
                return Forbid();

            OrderQuote OrderQuote = ConvertDTOToEntity(OrderQuote_OrderQuoteDTO);
            OrderQuote = await OrderQuoteService.Delete(OrderQuote);
            OrderQuote_OrderQuoteDTO = new OrderQuote_OrderQuoteDTO(OrderQuote);
            if (OrderQuote.IsValidated)
                return OrderQuote_OrderQuoteDTO;
            else
                return BadRequest(OrderQuote_OrderQuoteDTO);
        }

        [Route(OrderQuoteRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
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

        [Route(OrderQuoteRoute.Import), HttpPost]
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
            ContactFilter ContactFilter = new ContactFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = ContactSelect.ALL
            };
            List<Contact> Contacts = await ContactService.List(ContactFilter);
            AppUserFilter CreatorFilter = new AppUserFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = AppUserSelect.ALL
            };
            List<AppUser> Creators = await AppUserService.List(CreatorFilter);
            DistrictFilter DistrictFilter = new DistrictFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = DistrictSelect.ALL
            };
            List<District> Districts = await DistrictService.List(DistrictFilter);
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
            NationFilter NationFilter = new NationFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = NationSelect.ALL
            };
            List<Nation> Nations = await NationService.List(NationFilter);
            OpportunityFilter OpportunityFilter = new OpportunityFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = OpportunitySelect.ALL
            };
            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            OrderQuoteStatusFilter OrderQuoteStatusFilter = new OrderQuoteStatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = OrderQuoteStatusSelect.ALL
            };
            List<OrderQuoteStatus> OrderQuoteStatuses = await OrderQuoteStatusService.List(OrderQuoteStatusFilter);
            ProvinceFilter ProvinceFilter = new ProvinceFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = ProvinceSelect.ALL
            };
            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<OrderQuote> OrderQuotes = new List<OrderQuote>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(OrderQuotes);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int SubjectColumn = 1 + StartColumn;
                int CompanyIdColumn = 2 + StartColumn;
                int ContactIdColumn = 3 + StartColumn;
                int OpportunityIdColumn = 4 + StartColumn;
                int EditedPriceStatusIdColumn = 5 + StartColumn;
                int EndAtColumn = 6 + StartColumn;
                int AppUserIdColumn = 7 + StartColumn;
                int OrderQuoteStatusIdColumn = 8 + StartColumn;
                int NoteColumn = 9 + StartColumn;
                int InvoiceAddressColumn = 10 + StartColumn;
                int InvoiceNationIdColumn = 11 + StartColumn;
                int InvoiceProvinceIdColumn = 12 + StartColumn;
                int InvoiceDistrictIdColumn = 13 + StartColumn;
                int InvoiceZIPCodeColumn = 14 + StartColumn;
                int AddressColumn = 15 + StartColumn;
                int NationIdColumn = 16 + StartColumn;
                int ProvinceIdColumn = 17 + StartColumn;
                int DistrictIdColumn = 18 + StartColumn;
                int ZIPCodeColumn = 19 + StartColumn;
                int SubTotalColumn = 20 + StartColumn;
                int GeneralDiscountPercentageColumn = 21 + StartColumn;
                int GeneralDiscountAmountColumn = 22 + StartColumn;
                int TotalTaxAmountOtherColumn = 23 + StartColumn;
                int TotalTaxAmountColumn = 24 + StartColumn;
                int TotalColumn = 25 + StartColumn;
                int CreatorIdColumn = 26 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string SubjectValue = worksheet.Cells[i + StartRow, SubjectColumn].Value?.ToString();
                    string CompanyIdValue = worksheet.Cells[i + StartRow, CompanyIdColumn].Value?.ToString();
                    string ContactIdValue = worksheet.Cells[i + StartRow, ContactIdColumn].Value?.ToString();
                    string OpportunityIdValue = worksheet.Cells[i + StartRow, OpportunityIdColumn].Value?.ToString();
                    string EditedPriceStatusIdValue = worksheet.Cells[i + StartRow, EditedPriceStatusIdColumn].Value?.ToString();
                    string EndAtValue = worksheet.Cells[i + StartRow, EndAtColumn].Value?.ToString();
                    string AppUserIdValue = worksheet.Cells[i + StartRow, AppUserIdColumn].Value?.ToString();
                    string OrderQuoteStatusIdValue = worksheet.Cells[i + StartRow, OrderQuoteStatusIdColumn].Value?.ToString();
                    string NoteValue = worksheet.Cells[i + StartRow, NoteColumn].Value?.ToString();
                    string InvoiceAddressValue = worksheet.Cells[i + StartRow, InvoiceAddressColumn].Value?.ToString();
                    string InvoiceNationIdValue = worksheet.Cells[i + StartRow, InvoiceNationIdColumn].Value?.ToString();
                    string InvoiceProvinceIdValue = worksheet.Cells[i + StartRow, InvoiceProvinceIdColumn].Value?.ToString();
                    string InvoiceDistrictIdValue = worksheet.Cells[i + StartRow, InvoiceDistrictIdColumn].Value?.ToString();
                    string InvoiceZIPCodeValue = worksheet.Cells[i + StartRow, InvoiceZIPCodeColumn].Value?.ToString();
                    string AddressValue = worksheet.Cells[i + StartRow, AddressColumn].Value?.ToString();
                    string NationIdValue = worksheet.Cells[i + StartRow, NationIdColumn].Value?.ToString();
                    string ProvinceIdValue = worksheet.Cells[i + StartRow, ProvinceIdColumn].Value?.ToString();
                    string DistrictIdValue = worksheet.Cells[i + StartRow, DistrictIdColumn].Value?.ToString();
                    string ZIPCodeValue = worksheet.Cells[i + StartRow, ZIPCodeColumn].Value?.ToString();
                    string SubTotalValue = worksheet.Cells[i + StartRow, SubTotalColumn].Value?.ToString();
                    string GeneralDiscountPercentageValue = worksheet.Cells[i + StartRow, GeneralDiscountPercentageColumn].Value?.ToString();
                    string GeneralDiscountAmountValue = worksheet.Cells[i + StartRow, GeneralDiscountAmountColumn].Value?.ToString();
                    string TotalTaxAmountOtherValue = worksheet.Cells[i + StartRow, TotalTaxAmountOtherColumn].Value?.ToString();
                    string TotalTaxAmountValue = worksheet.Cells[i + StartRow, TotalTaxAmountColumn].Value?.ToString();
                    string TotalValue = worksheet.Cells[i + StartRow, TotalColumn].Value?.ToString();
                    string CreatorIdValue = worksheet.Cells[i + StartRow, CreatorIdColumn].Value?.ToString();

                    OrderQuote OrderQuote = new OrderQuote();
                    OrderQuote.Subject = SubjectValue;
                    OrderQuote.EndAt = DateTime.TryParse(EndAtValue, out DateTime EndAt) ? EndAt : DateTime.Now;
                    OrderQuote.Note = NoteValue;
                    OrderQuote.InvoiceAddress = InvoiceAddressValue;
                    OrderQuote.InvoiceZIPCode = InvoiceZIPCodeValue;
                    OrderQuote.Address = AddressValue;
                    OrderQuote.ZIPCode = ZIPCodeValue;
                    OrderQuote.SubTotal = decimal.TryParse(SubTotalValue, out decimal SubTotal) ? SubTotal : 0;
                    OrderQuote.GeneralDiscountPercentage = decimal.TryParse(GeneralDiscountPercentageValue, out decimal GeneralDiscountPercentage) ? GeneralDiscountPercentage : 0;
                    OrderQuote.GeneralDiscountAmount = decimal.TryParse(GeneralDiscountAmountValue, out decimal GeneralDiscountAmount) ? GeneralDiscountAmount : 0;
                    OrderQuote.TotalTaxAmountOther = decimal.TryParse(TotalTaxAmountOtherValue, out decimal TotalTaxAmountOther) ? TotalTaxAmountOther : 0;
                    OrderQuote.TotalTaxAmount = decimal.TryParse(TotalTaxAmountValue, out decimal TotalTaxAmount) ? TotalTaxAmount : 0;
                    OrderQuote.Total = decimal.TryParse(TotalValue, out decimal Total) ? Total : 0;
                    AppUser AppUser = AppUsers.Where(x => x.Id.ToString() == AppUserIdValue).FirstOrDefault();
                    OrderQuote.AppUserId = AppUser == null ? 0 : AppUser.Id;
                    OrderQuote.AppUser = AppUser;
                    Contact Contact = Contacts.Where(x => x.Id.ToString() == ContactIdValue).FirstOrDefault();
                    OrderQuote.ContactId = Contact == null ? 0 : Contact.Id;
                    OrderQuote.Contact = Contact;
                    AppUser Creator = Creators.Where(x => x.Id.ToString() == CreatorIdValue).FirstOrDefault();
                    OrderQuote.CreatorId = Creator == null ? 0 : Creator.Id;
                    OrderQuote.Creator = Creator;
                    District District = Districts.Where(x => x.Id.ToString() == DistrictIdValue).FirstOrDefault();
                    OrderQuote.DistrictId = District == null ? 0 : District.Id;
                    OrderQuote.District = District;
                    EditedPriceStatus EditedPriceStatus = EditedPriceStatuses.Where(x => x.Id.ToString() == EditedPriceStatusIdValue).FirstOrDefault();
                    OrderQuote.EditedPriceStatusId = EditedPriceStatus == null ? 0 : EditedPriceStatus.Id;
                    OrderQuote.EditedPriceStatus = EditedPriceStatus;
                    District InvoiceDistrict = InvoiceDistricts.Where(x => x.Id.ToString() == InvoiceDistrictIdValue).FirstOrDefault();
                    OrderQuote.InvoiceDistrictId = InvoiceDistrict == null ? 0 : InvoiceDistrict.Id;
                    OrderQuote.InvoiceDistrict = InvoiceDistrict;
                    Nation InvoiceNation = InvoiceNations.Where(x => x.Id.ToString() == InvoiceNationIdValue).FirstOrDefault();
                    OrderQuote.InvoiceNationId = InvoiceNation == null ? 0 : InvoiceNation.Id;
                    OrderQuote.InvoiceNation = InvoiceNation;
                    Province InvoiceProvince = InvoiceProvinces.Where(x => x.Id.ToString() == InvoiceProvinceIdValue).FirstOrDefault();
                    OrderQuote.InvoiceProvinceId = InvoiceProvince == null ? 0 : InvoiceProvince.Id;
                    OrderQuote.InvoiceProvince = InvoiceProvince;
                    Nation Nation = Nations.Where(x => x.Id.ToString() == NationIdValue).FirstOrDefault();
                    OrderQuote.NationId = Nation == null ? 0 : Nation.Id;
                    OrderQuote.Nation = Nation;
                    Opportunity Opportunity = Opportunities.Where(x => x.Id.ToString() == OpportunityIdValue).FirstOrDefault();
                    OrderQuote.OpportunityId = Opportunity == null ? 0 : Opportunity.Id;
                    OrderQuote.Opportunity = Opportunity;
                    OrderQuoteStatus OrderQuoteStatus = OrderQuoteStatuses.Where(x => x.Id.ToString() == OrderQuoteStatusIdValue).FirstOrDefault();
                    OrderQuote.OrderQuoteStatusId = OrderQuoteStatus == null ? 0 : OrderQuoteStatus.Id;
                    OrderQuote.OrderQuoteStatus = OrderQuoteStatus;
                    Province Province = Provinces.Where(x => x.Id.ToString() == ProvinceIdValue).FirstOrDefault();
                    OrderQuote.ProvinceId = Province == null ? 0 : Province.Id;
                    OrderQuote.Province = Province;

                    OrderQuotes.Add(OrderQuote);
                }
            }
            OrderQuotes = await OrderQuoteService.Import(OrderQuotes);
            if (OrderQuotes.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < OrderQuotes.Count; i++)
                {
                    OrderQuote OrderQuote = OrderQuotes[i];
                    if (!OrderQuote.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.Id)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.Id)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.Subject)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.Subject)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.CompanyId)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.CompanyId)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.ContactId)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.ContactId)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.OpportunityId)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.OpportunityId)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.EditedPriceStatusId)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.EditedPriceStatusId)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.EndAt)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.EndAt)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.AppUserId)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.AppUserId)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.OrderQuoteStatusId)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.OrderQuoteStatusId)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.Note)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.Note)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.InvoiceAddress)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.InvoiceAddress)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.InvoiceNationId)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.InvoiceNationId)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.InvoiceProvinceId)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.InvoiceProvinceId)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.InvoiceDistrictId)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.InvoiceDistrictId)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.InvoiceZIPCode)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.InvoiceZIPCode)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.Address)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.Address)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.NationId)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.NationId)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.ProvinceId)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.ProvinceId)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.DistrictId)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.DistrictId)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.ZIPCode)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.ZIPCode)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.SubTotal)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.SubTotal)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.GeneralDiscountPercentage)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.GeneralDiscountPercentage)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.GeneralDiscountAmount)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.GeneralDiscountAmount)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.TotalTaxAmountOther)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.TotalTaxAmountOther)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.TotalTaxAmount)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.TotalTaxAmount)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.Total)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.Total)];
                        if (OrderQuote.Errors.ContainsKey(nameof(OrderQuote.CreatorId)))
                            Error += OrderQuote.Errors[nameof(OrderQuote.CreatorId)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }

        [Route(OrderQuoteRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] OrderQuote_OrderQuoteFilterDTO OrderQuote_OrderQuoteFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region OrderQuote
                var OrderQuoteFilter = ConvertFilterDTOToFilterEntity(OrderQuote_OrderQuoteFilterDTO);
                OrderQuoteFilter.Skip = 0;
                OrderQuoteFilter.Take = int.MaxValue;
                OrderQuoteFilter = await OrderQuoteService.ToFilter(OrderQuoteFilter);
                List<OrderQuote> OrderQuotes = await OrderQuoteService.List(OrderQuoteFilter);

                var OrderQuoteHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Subject",
                        "CompanyId",
                        "ContactId",
                        "OpportunityId",
                        "EditedPriceStatusId",
                        "EndAt",
                        "AppUserId",
                        "OrderQuoteStatusId",
                        "Note",
                        "InvoiceAddress",
                        "InvoiceNationId",
                        "InvoiceProvinceId",
                        "InvoiceDistrictId",
                        "InvoiceZIPCode",
                        "Address",
                        "NationId",
                        "ProvinceId",
                        "DistrictId",
                        "ZIPCode",
                        "SubTotal",
                        "GeneralDiscountPercentage",
                        "GeneralDiscountAmount",
                        "TotalTaxAmountOther",
                        "TotalTaxAmount",
                        "Total",
                        "CreatorId",
                    }
                };
                List<object[]> OrderQuoteData = new List<object[]>();
                for (int i = 0; i < OrderQuotes.Count; i++)
                {
                    var OrderQuote = OrderQuotes[i];
                    OrderQuoteData.Add(new Object[]
                    {
                        OrderQuote.Id,
                        OrderQuote.Subject,
                        OrderQuote.CompanyId,
                        OrderQuote.ContactId,
                        OrderQuote.OpportunityId,
                        OrderQuote.EditedPriceStatusId,
                        OrderQuote.EndAt,
                        OrderQuote.AppUserId,
                        OrderQuote.OrderQuoteStatusId,
                        OrderQuote.Note,
                        OrderQuote.InvoiceAddress,
                        OrderQuote.InvoiceNationId,
                        OrderQuote.InvoiceProvinceId,
                        OrderQuote.InvoiceDistrictId,
                        OrderQuote.InvoiceZIPCode,
                        OrderQuote.Address,
                        OrderQuote.NationId,
                        OrderQuote.ProvinceId,
                        OrderQuote.DistrictId,
                        OrderQuote.ZIPCode,
                        OrderQuote.SubTotal,
                        OrderQuote.GeneralDiscountPercentage,
                        OrderQuote.GeneralDiscountAmount,
                        OrderQuote.TotalTaxAmountOther,
                        OrderQuote.TotalTaxAmount,
                        OrderQuote.Total,
                        OrderQuote.CreatorId,
                    });
                }
                excel.GenerateWorksheet("OrderQuote", OrderQuoteHeaders, OrderQuoteData);
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
                #region OrderQuoteStatus
                var OrderQuoteStatusFilter = new OrderQuoteStatusFilter();
                OrderQuoteStatusFilter.Selects = OrderQuoteStatusSelect.ALL;
                OrderQuoteStatusFilter.OrderBy = OrderQuoteStatusOrder.Id;
                OrderQuoteStatusFilter.OrderType = OrderType.ASC;
                OrderQuoteStatusFilter.Skip = 0;
                OrderQuoteStatusFilter.Take = int.MaxValue;
                List<OrderQuoteStatus> OrderQuoteStatuses = await OrderQuoteStatusService.List(OrderQuoteStatusFilter);

                var OrderQuoteStatusHeaders = new List<string[]>()
                {
                    new string[] {
                        "Id",
                        "Code",
                        "Name",
                    }
                };
                List<object[]> OrderQuoteStatusData = new List<object[]>();
                for (int i = 0; i < OrderQuoteStatuses.Count; i++)
                {
                    var OrderQuoteStatus = OrderQuoteStatuses[i];
                    OrderQuoteStatusData.Add(new Object[]
                    {
                        OrderQuoteStatus.Id,
                        OrderQuoteStatus.Code,
                        OrderQuoteStatus.Name,
                    });
                }
                excel.GenerateWorksheet("OrderQuoteStatus", OrderQuoteStatusHeaders, OrderQuoteStatusData);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "OrderQuote.xlsx");
        }

        [Route(OrderQuoteRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] OrderQuote_OrderQuoteFilterDTO OrderQuote_OrderQuoteFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            string path = "Templates/OrderQuote_Template.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "OrderQuote.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            OrderQuoteFilter OrderQuoteFilter = new OrderQuoteFilter();
            OrderQuoteFilter = await OrderQuoteService.ToFilter(OrderQuoteFilter);
            if (Id == 0)
            {

            }
            else
            {
                OrderQuoteFilter.Id = new IdFilter { Equal = Id };
                int count = await OrderQuoteService.Count(OrderQuoteFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private OrderQuote ConvertDTOToEntity(OrderQuote_OrderQuoteDTO OrderQuote_OrderQuoteDTO)
        {
            OrderQuote OrderQuote = new OrderQuote();
            OrderQuote.Id = OrderQuote_OrderQuoteDTO.Id;
            OrderQuote.Subject = OrderQuote_OrderQuoteDTO.Subject;
            OrderQuote.CompanyId = OrderQuote_OrderQuoteDTO.CompanyId;
            OrderQuote.ContactId = OrderQuote_OrderQuoteDTO.ContactId;
            OrderQuote.OpportunityId = OrderQuote_OrderQuoteDTO.OpportunityId;
            OrderQuote.EditedPriceStatusId = OrderQuote_OrderQuoteDTO.EditedPriceStatusId;
            OrderQuote.EndAt = OrderQuote_OrderQuoteDTO.EndAt;
            OrderQuote.AppUserId = OrderQuote_OrderQuoteDTO.AppUserId;
            OrderQuote.OrderQuoteStatusId = OrderQuote_OrderQuoteDTO.OrderQuoteStatusId;
            OrderQuote.Note = OrderQuote_OrderQuoteDTO.Note;
            OrderQuote.InvoiceAddress = OrderQuote_OrderQuoteDTO.InvoiceAddress;
            OrderQuote.InvoiceNationId = OrderQuote_OrderQuoteDTO.InvoiceNationId;
            OrderQuote.InvoiceProvinceId = OrderQuote_OrderQuoteDTO.InvoiceProvinceId;
            OrderQuote.InvoiceDistrictId = OrderQuote_OrderQuoteDTO.InvoiceDistrictId;
            OrderQuote.InvoiceZIPCode = OrderQuote_OrderQuoteDTO.InvoiceZIPCode;
            OrderQuote.Address = OrderQuote_OrderQuoteDTO.Address;
            OrderQuote.NationId = OrderQuote_OrderQuoteDTO.NationId;
            OrderQuote.ProvinceId = OrderQuote_OrderQuoteDTO.ProvinceId;
            OrderQuote.DistrictId = OrderQuote_OrderQuoteDTO.DistrictId;
            OrderQuote.ZIPCode = OrderQuote_OrderQuoteDTO.ZIPCode;
            OrderQuote.SubTotal = OrderQuote_OrderQuoteDTO.SubTotal;
            OrderQuote.GeneralDiscountPercentage = OrderQuote_OrderQuoteDTO.GeneralDiscountPercentage;
            OrderQuote.GeneralDiscountAmount = OrderQuote_OrderQuoteDTO.GeneralDiscountAmount;
            OrderQuote.TotalTaxAmountOther = OrderQuote_OrderQuoteDTO.TotalTaxAmountOther;
            OrderQuote.TotalTaxAmount = OrderQuote_OrderQuoteDTO.TotalTaxAmount;
            OrderQuote.Total = OrderQuote_OrderQuoteDTO.Total;
            OrderQuote.CreatorId = OrderQuote_OrderQuoteDTO.CreatorId;
            OrderQuote.AppUser = OrderQuote_OrderQuoteDTO.AppUser == null ? null : new AppUser
            {
                Id = OrderQuote_OrderQuoteDTO.AppUser.Id,
                Username = OrderQuote_OrderQuoteDTO.AppUser.Username,
                DisplayName = OrderQuote_OrderQuoteDTO.AppUser.DisplayName,
                Address = OrderQuote_OrderQuoteDTO.AppUser.Address,
                Email = OrderQuote_OrderQuoteDTO.AppUser.Email,
                Phone = OrderQuote_OrderQuoteDTO.AppUser.Phone,
                SexId = OrderQuote_OrderQuoteDTO.AppUser.SexId,
                Birthday = OrderQuote_OrderQuoteDTO.AppUser.Birthday,
                Avatar = OrderQuote_OrderQuoteDTO.AppUser.Avatar,
                Department = OrderQuote_OrderQuoteDTO.AppUser.Department,
                OrganizationId = OrderQuote_OrderQuoteDTO.AppUser.OrganizationId,
                Longitude = OrderQuote_OrderQuoteDTO.AppUser.Longitude,
                Latitude = OrderQuote_OrderQuoteDTO.AppUser.Latitude,
                StatusId = OrderQuote_OrderQuoteDTO.AppUser.StatusId,
            };
            OrderQuote.Company = OrderQuote_OrderQuoteDTO.Company == null ? null : new Company
            {
                Id = OrderQuote_OrderQuoteDTO.Company.Id,
                Name = OrderQuote_OrderQuoteDTO.Company.Name,
                Phone = OrderQuote_OrderQuoteDTO.Company.Phone,
                FAX = OrderQuote_OrderQuoteDTO.Company.FAX,
                PhoneOther = OrderQuote_OrderQuoteDTO.Company.PhoneOther,
                Email = OrderQuote_OrderQuoteDTO.Company.Email,
                EmailOther = OrderQuote_OrderQuoteDTO.Company.EmailOther,
                ZIPCode = OrderQuote_OrderQuoteDTO.Company.ZIPCode,
                Revenue = OrderQuote_OrderQuoteDTO.Company.Revenue,
                Website = OrderQuote_OrderQuoteDTO.Company.Website,
                Address = OrderQuote_OrderQuoteDTO.Company.Address,
                NationId = OrderQuote_OrderQuoteDTO.Company.NationId,
                ProvinceId = OrderQuote_OrderQuoteDTO.Company.ProvinceId,
                DistrictId = OrderQuote_OrderQuoteDTO.Company.DistrictId,
                NumberOfEmployee = OrderQuote_OrderQuoteDTO.Company.NumberOfEmployee,
                RefuseReciveEmail = OrderQuote_OrderQuoteDTO.Company.RefuseReciveEmail,
                RefuseReciveSMS = OrderQuote_OrderQuoteDTO.Company.RefuseReciveSMS,
                CustomerLeadId = OrderQuote_OrderQuoteDTO.Company.CustomerLeadId,
                ParentId = OrderQuote_OrderQuoteDTO.Company.ParentId,
                Path = OrderQuote_OrderQuoteDTO.Company.Path,
                Level = OrderQuote_OrderQuoteDTO.Company.Level,
                ProfessionId = OrderQuote_OrderQuoteDTO.Company.ProfessionId,
                AppUserId = OrderQuote_OrderQuoteDTO.Company.AppUserId,
                CreatorId = OrderQuote_OrderQuoteDTO.Company.CreatorId,
                CurrencyId = OrderQuote_OrderQuoteDTO.Company.CurrencyId,
                CompanyStatusId = OrderQuote_OrderQuoteDTO.Company.CompanyStatusId,
                Description = OrderQuote_OrderQuoteDTO.Company.Description,
                RowId = OrderQuote_OrderQuoteDTO.Company.RowId,
            };
            OrderQuote.Contact = OrderQuote_OrderQuoteDTO.Contact == null ? null : new Contact
            {
                Id = OrderQuote_OrderQuoteDTO.Contact.Id,
                Name = OrderQuote_OrderQuoteDTO.Contact.Name,
                ProfessionId = OrderQuote_OrderQuoteDTO.Contact.ProfessionId,
                CompanyId = OrderQuote_OrderQuoteDTO.Contact.CompanyId,
                ContactStatusId = OrderQuote_OrderQuoteDTO.Contact.ContactStatusId,
                Address = OrderQuote_OrderQuoteDTO.Contact.Address,
                NationId = OrderQuote_OrderQuoteDTO.Contact.NationId,
                ProvinceId = OrderQuote_OrderQuoteDTO.Contact.ProvinceId,
                DistrictId = OrderQuote_OrderQuoteDTO.Contact.DistrictId,
                CustomerLeadId = OrderQuote_OrderQuoteDTO.Contact.CustomerLeadId,
                ImageId = OrderQuote_OrderQuoteDTO.Contact.ImageId,
                Description = OrderQuote_OrderQuoteDTO.Contact.Description,
                EmailOther = OrderQuote_OrderQuoteDTO.Contact.EmailOther,
                DateOfBirth = OrderQuote_OrderQuoteDTO.Contact.DateOfBirth,
                Phone = OrderQuote_OrderQuoteDTO.Contact.Phone,
                PhoneHome = OrderQuote_OrderQuoteDTO.Contact.PhoneHome,
                FAX = OrderQuote_OrderQuoteDTO.Contact.FAX,
                Email = OrderQuote_OrderQuoteDTO.Contact.Email,
                Department = OrderQuote_OrderQuoteDTO.Contact.Department,
                ZIPCode = OrderQuote_OrderQuoteDTO.Contact.ZIPCode,
                SexId = OrderQuote_OrderQuoteDTO.Contact.SexId,
                AppUserId = OrderQuote_OrderQuoteDTO.Contact.AppUserId,
                RefuseReciveEmail = OrderQuote_OrderQuoteDTO.Contact.RefuseReciveEmail,
                RefuseReciveSMS = OrderQuote_OrderQuoteDTO.Contact.RefuseReciveSMS,
                PositionId = OrderQuote_OrderQuoteDTO.Contact.PositionId,
                CreatorId = OrderQuote_OrderQuoteDTO.Contact.CreatorId,
            };
            OrderQuote.Creator = OrderQuote_OrderQuoteDTO.Creator == null ? null : new AppUser
            {
                Id = OrderQuote_OrderQuoteDTO.Creator.Id,
                Username = OrderQuote_OrderQuoteDTO.Creator.Username,
                DisplayName = OrderQuote_OrderQuoteDTO.Creator.DisplayName,
                Address = OrderQuote_OrderQuoteDTO.Creator.Address,
                Email = OrderQuote_OrderQuoteDTO.Creator.Email,
                Phone = OrderQuote_OrderQuoteDTO.Creator.Phone,
                SexId = OrderQuote_OrderQuoteDTO.Creator.SexId,
                Birthday = OrderQuote_OrderQuoteDTO.Creator.Birthday,
                Avatar = OrderQuote_OrderQuoteDTO.Creator.Avatar,
                Department = OrderQuote_OrderQuoteDTO.Creator.Department,
                OrganizationId = OrderQuote_OrderQuoteDTO.Creator.OrganizationId,
                Longitude = OrderQuote_OrderQuoteDTO.Creator.Longitude,
                Latitude = OrderQuote_OrderQuoteDTO.Creator.Latitude,
                StatusId = OrderQuote_OrderQuoteDTO.Creator.StatusId,
            };
            OrderQuote.District = OrderQuote_OrderQuoteDTO.District == null ? null : new District
            {
                Id = OrderQuote_OrderQuoteDTO.District.Id,
                Code = OrderQuote_OrderQuoteDTO.District.Code,
                Name = OrderQuote_OrderQuoteDTO.District.Name,
                Priority = OrderQuote_OrderQuoteDTO.District.Priority,
                ProvinceId = OrderQuote_OrderQuoteDTO.District.ProvinceId,
                StatusId = OrderQuote_OrderQuoteDTO.District.StatusId,
            };
            OrderQuote.EditedPriceStatus = OrderQuote_OrderQuoteDTO.EditedPriceStatus == null ? null : new EditedPriceStatus
            {
                Id = OrderQuote_OrderQuoteDTO.EditedPriceStatus.Id,
                Code = OrderQuote_OrderQuoteDTO.EditedPriceStatus.Code,
                Name = OrderQuote_OrderQuoteDTO.EditedPriceStatus.Name,
            };
            OrderQuote.InvoiceDistrict = OrderQuote_OrderQuoteDTO.InvoiceDistrict == null ? null : new District
            {
                Id = OrderQuote_OrderQuoteDTO.InvoiceDistrict.Id,
                Code = OrderQuote_OrderQuoteDTO.InvoiceDistrict.Code,
                Name = OrderQuote_OrderQuoteDTO.InvoiceDistrict.Name,
                Priority = OrderQuote_OrderQuoteDTO.InvoiceDistrict.Priority,
                ProvinceId = OrderQuote_OrderQuoteDTO.InvoiceDistrict.ProvinceId,
                StatusId = OrderQuote_OrderQuoteDTO.InvoiceDistrict.StatusId,
            };
            OrderQuote.InvoiceNation = OrderQuote_OrderQuoteDTO.InvoiceNation == null ? null : new Nation
            {
                Id = OrderQuote_OrderQuoteDTO.InvoiceNation.Id,
                Code = OrderQuote_OrderQuoteDTO.InvoiceNation.Code,
                Name = OrderQuote_OrderQuoteDTO.InvoiceNation.Name,
                StatusId = OrderQuote_OrderQuoteDTO.InvoiceNation.StatusId,
            };
            OrderQuote.InvoiceProvince = OrderQuote_OrderQuoteDTO.InvoiceProvince == null ? null : new Province
            {
                Id = OrderQuote_OrderQuoteDTO.InvoiceProvince.Id,
                Code = OrderQuote_OrderQuoteDTO.InvoiceProvince.Code,
                Name = OrderQuote_OrderQuoteDTO.InvoiceProvince.Name,
                Priority = OrderQuote_OrderQuoteDTO.InvoiceProvince.Priority,
                StatusId = OrderQuote_OrderQuoteDTO.InvoiceProvince.StatusId,
            };
            OrderQuote.Nation = OrderQuote_OrderQuoteDTO.Nation == null ? null : new Nation
            {
                Id = OrderQuote_OrderQuoteDTO.Nation.Id,
                Code = OrderQuote_OrderQuoteDTO.Nation.Code,
                Name = OrderQuote_OrderQuoteDTO.Nation.Name,
                StatusId = OrderQuote_OrderQuoteDTO.Nation.StatusId,
            };
            OrderQuote.Opportunity = OrderQuote_OrderQuoteDTO.Opportunity == null ? null : new Opportunity
            {
                Id = OrderQuote_OrderQuoteDTO.Opportunity.Id,
                Name = OrderQuote_OrderQuoteDTO.Opportunity.Name,
                CompanyId = OrderQuote_OrderQuoteDTO.Opportunity.CompanyId,
                CustomerLeadId = OrderQuote_OrderQuoteDTO.Opportunity.CustomerLeadId,
                ClosingDate = OrderQuote_OrderQuoteDTO.Opportunity.ClosingDate,
                SaleStageId = OrderQuote_OrderQuoteDTO.Opportunity.SaleStageId,
                ProbabilityId = OrderQuote_OrderQuoteDTO.Opportunity.ProbabilityId,
                PotentialResultId = OrderQuote_OrderQuoteDTO.Opportunity.PotentialResultId,
                LeadSourceId = OrderQuote_OrderQuoteDTO.Opportunity.LeadSourceId,
                AppUserId = OrderQuote_OrderQuoteDTO.Opportunity.AppUserId,
                CurrencyId = OrderQuote_OrderQuoteDTO.Opportunity.CurrencyId,
                Amount = OrderQuote_OrderQuoteDTO.Opportunity.Amount,
                ForecastAmount = OrderQuote_OrderQuoteDTO.Opportunity.ForecastAmount,
                Description = OrderQuote_OrderQuoteDTO.Opportunity.Description,
                RefuseReciveSMS = OrderQuote_OrderQuoteDTO.Opportunity.RefuseReciveSMS,
                RefuseReciveEmail = OrderQuote_OrderQuoteDTO.Opportunity.RefuseReciveEmail,
                OpportunityResultTypeId = OrderQuote_OrderQuoteDTO.Opportunity.OpportunityResultTypeId,
                CreatorId = OrderQuote_OrderQuoteDTO.Opportunity.CreatorId,
            };
            OrderQuote.OrderQuoteStatus = OrderQuote_OrderQuoteDTO.OrderQuoteStatus == null ? null : new OrderQuoteStatus
            {
                Id = OrderQuote_OrderQuoteDTO.OrderQuoteStatus.Id,
                Code = OrderQuote_OrderQuoteDTO.OrderQuoteStatus.Code,
                Name = OrderQuote_OrderQuoteDTO.OrderQuoteStatus.Name,
            };
            OrderQuote.Province = OrderQuote_OrderQuoteDTO.Province == null ? null : new Province
            {
                Id = OrderQuote_OrderQuoteDTO.Province.Id,
                Code = OrderQuote_OrderQuoteDTO.Province.Code,
                Name = OrderQuote_OrderQuoteDTO.Province.Name,
                Priority = OrderQuote_OrderQuoteDTO.Province.Priority,
                StatusId = OrderQuote_OrderQuoteDTO.Province.StatusId,
            };
            OrderQuote.OrderQuoteContents = OrderQuote_OrderQuoteDTO.OrderQuoteContents?
                .Select(x => new OrderQuoteContent
                {
                    Id = x.Id,
                    ItemId = x.ItemId,
                    UnitOfMeasureId = x.UnitOfMeasureId,
                    Quantity = x.Quantity,
                    RequestedQuantity = x.RequestedQuantity,
                    PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                    PrimaryPrice = x.PrimaryPrice,
                    SalePrice = x.SalePrice,
                    DiscountPercentage = x.DiscountPercentage,
                    DiscountAmount = x.DiscountAmount,
                    GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                    GeneralDiscountAmount = x.GeneralDiscountAmount,
                    TaxPercentage = x.TaxPercentage,
                    TaxAmount = x.TaxAmount,
                    TaxAmountOther = x.TaxAmountOther,
                    TaxPercentageOther = x.TaxPercentageOther,
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
                    },
                    PrimaryUnitOfMeasure = x.PrimaryUnitOfMeasure == null ? null : new UnitOfMeasure
                    {
                        Id = x.PrimaryUnitOfMeasure.Id,
                        Code = x.PrimaryUnitOfMeasure.Code,
                        Name = x.PrimaryUnitOfMeasure.Name,
                        Description = x.PrimaryUnitOfMeasure.Description,
                        StatusId = x.PrimaryUnitOfMeasure.StatusId,
                        Used = x.PrimaryUnitOfMeasure.Used,
                    },
                    TaxType = x.TaxType == null ? null : new TaxType
                    {
                        Id = x.TaxType.Id,
                        Code = x.TaxType.Code,
                        Name = x.TaxType.Name,
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
            OrderQuote.BaseLanguage = CurrentContext.Language;
            return OrderQuote;
        }

        private OrderQuoteFilter ConvertFilterDTOToFilterEntity(OrderQuote_OrderQuoteFilterDTO OrderQuote_OrderQuoteFilterDTO)
        {
            OrderQuoteFilter OrderQuoteFilter = new OrderQuoteFilter();
            OrderQuoteFilter.Selects = OrderQuoteSelect.ALL;
            OrderQuoteFilter.Skip = OrderQuote_OrderQuoteFilterDTO.Skip;
            OrderQuoteFilter.Take = OrderQuote_OrderQuoteFilterDTO.Take;
            OrderQuoteFilter.OrderBy = OrderQuote_OrderQuoteFilterDTO.OrderBy;
            OrderQuoteFilter.OrderType = OrderQuote_OrderQuoteFilterDTO.OrderType;

            OrderQuoteFilter.Id = OrderQuote_OrderQuoteFilterDTO.Id;
            OrderQuoteFilter.Subject = OrderQuote_OrderQuoteFilterDTO.Subject;
            OrderQuoteFilter.CompanyId = OrderQuote_OrderQuoteFilterDTO.CompanyId;
            OrderQuoteFilter.ContactId = OrderQuote_OrderQuoteFilterDTO.ContactId;
            OrderQuoteFilter.OpportunityId = OrderQuote_OrderQuoteFilterDTO.OpportunityId;
            OrderQuoteFilter.EditedPriceStatusId = OrderQuote_OrderQuoteFilterDTO.EditedPriceStatusId;
            OrderQuoteFilter.EndAt = OrderQuote_OrderQuoteFilterDTO.EndAt;
            OrderQuoteFilter.AppUserId = OrderQuote_OrderQuoteFilterDTO.AppUserId;
            OrderQuoteFilter.OrderQuoteStatusId = OrderQuote_OrderQuoteFilterDTO.OrderQuoteStatusId;
            OrderQuoteFilter.Note = OrderQuote_OrderQuoteFilterDTO.Note;
            OrderQuoteFilter.InvoiceAddress = OrderQuote_OrderQuoteFilterDTO.InvoiceAddress;
            OrderQuoteFilter.InvoiceNationId = OrderQuote_OrderQuoteFilterDTO.InvoiceNationId;
            OrderQuoteFilter.InvoiceProvinceId = OrderQuote_OrderQuoteFilterDTO.InvoiceProvinceId;
            OrderQuoteFilter.InvoiceDistrictId = OrderQuote_OrderQuoteFilterDTO.InvoiceDistrictId;
            OrderQuoteFilter.InvoiceZIPCode = OrderQuote_OrderQuoteFilterDTO.InvoiceZIPCode;
            OrderQuoteFilter.Address = OrderQuote_OrderQuoteFilterDTO.Address;
            OrderQuoteFilter.NationId = OrderQuote_OrderQuoteFilterDTO.NationId;
            OrderQuoteFilter.ProvinceId = OrderQuote_OrderQuoteFilterDTO.ProvinceId;
            OrderQuoteFilter.DistrictId = OrderQuote_OrderQuoteFilterDTO.DistrictId;
            OrderQuoteFilter.ZIPCode = OrderQuote_OrderQuoteFilterDTO.ZIPCode;
            OrderQuoteFilter.SubTotal = OrderQuote_OrderQuoteFilterDTO.SubTotal;
            OrderQuoteFilter.GeneralDiscountPercentage = OrderQuote_OrderQuoteFilterDTO.GeneralDiscountPercentage;
            OrderQuoteFilter.GeneralDiscountAmount = OrderQuote_OrderQuoteFilterDTO.GeneralDiscountAmount;
            OrderQuoteFilter.TotalTaxAmountOther = OrderQuote_OrderQuoteFilterDTO.TotalTaxAmountOther;
            OrderQuoteFilter.TotalTaxAmount = OrderQuote_OrderQuoteFilterDTO.TotalTaxAmount;
            OrderQuoteFilter.Total = OrderQuote_OrderQuoteFilterDTO.Total;
            OrderQuoteFilter.CreatorId = OrderQuote_OrderQuoteFilterDTO.CreatorId;
            OrderQuoteFilter.CreatedAt = OrderQuote_OrderQuoteFilterDTO.CreatedAt;
            OrderQuoteFilter.UpdatedAt = OrderQuote_OrderQuoteFilterDTO.UpdatedAt;
            return OrderQuoteFilter;
        }
    }
}

