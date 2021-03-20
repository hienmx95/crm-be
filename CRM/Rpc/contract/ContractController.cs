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
using CRM.Services.MContract;
using CRM.Services.MCompany;
using CRM.Services.MAppUser;
using CRM.Services.MContact;
using CRM.Services.MContractStatus;
using CRM.Services.MContractType;
using CRM.Services.MCurrency;
using CRM.Services.MCustomer;
using CRM.Services.MDistrict;
using CRM.Services.MNation;
using CRM.Services.MProvince;
using CRM.Services.MOpportunity;
using CRM.Services.MOrganization;
using CRM.Services.MPaymentStatus;
using CRM.Services.MProduct;
using CRM.Services.MProductType;
using CRM.Services.MSupplier;
using CRM.Services.MUnitOfMeasure;
using CRM.Services.MTaxType;
using CRM.Services.MProductGrouping;
using CRM.Services.MFileType;
using CRM.Models;

namespace CRM.Rpc.contract
{
    public partial class ContractController : RpcController
    {
        private ICompanyService CompanyService;
        private IAppUserService AppUserService;
        private IContactService ContactService;
        private IContractStatusService ContractStatusService;
        private IContractTypeService ContractTypeService;
        private ICurrencyService CurrencyService;
        private ICustomerService CustomerService;
        private IDistrictService DistrictService;
        private INationService NationService;
        private IProvinceService ProvinceService;
        private IOpportunityService OpportunityService;
        private IOrganizationService OrganizationService;
        private IPaymentStatusService PaymentStatusService;
        private IContractService ContractService;
        private IProductService ProductService;
        private IProductGroupingService ProductGroupingService;
        private IProductTypeService ProductTypeService;
        private ISupplierService SupplierService;
        private IUnitOfMeasureService UnitOfMeasureService;
        private IItemService ItemService;
        private ITaxTypeService TaxTypeService;
        private IFileTypeService FileTypeService;
        private ICurrentContext CurrentContext;
        public ContractController(
            ICompanyService CompanyService,
            IAppUserService AppUserService,
            IContactService ContactService,
            IContractStatusService ContractStatusService,
            IContractTypeService ContractTypeService,
            ICurrencyService CurrencyService,
            ICustomerService CustomerService,
            IDistrictService DistrictService,
            INationService NationService,
            IProvinceService ProvinceService,
            IOpportunityService OpportunityService,
            IOrganizationService OrganizationService,
            IPaymentStatusService PaymentStatusService,
            IContractService ContractService,
            IProductService ProductService,
            IProductGroupingService ProductGroupingService,
            IProductTypeService ProductTypeService,
            ISupplierService SupplierService,
            IUnitOfMeasureService UnitOfMeasureService,
            IItemService ItemService,
            ITaxTypeService TaxTypeService,
            IFileTypeService FileTypeService,
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.CompanyService = CompanyService;
            this.AppUserService = AppUserService;
            this.ContactService = ContactService;
            this.ContractStatusService = ContractStatusService;
            this.ContractTypeService = ContractTypeService;
            this.CurrencyService = CurrencyService;
            this.CustomerService = CustomerService;
            this.DistrictService = DistrictService;
            this.NationService = NationService;
            this.ProvinceService = ProvinceService;
            this.OpportunityService = OpportunityService;
            this.OrganizationService = OrganizationService;
            this.PaymentStatusService = PaymentStatusService;
            this.ContractService = ContractService;
            this.ProductService = ProductService;
            this.ProductGroupingService = ProductGroupingService;
            this.ProductTypeService = ProductTypeService;
            this.SupplierService = SupplierService;
            this.UnitOfMeasureService = UnitOfMeasureService;
            this.ItemService = ItemService;
            this.TaxTypeService = TaxTypeService;
            this.FileTypeService = FileTypeService;
            this.CurrentContext = CurrentContext;
        }

        [Route(ContractRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] Contract_ContractFilterDTO Contract_ContractFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContractFilter ContractFilter = ConvertFilterDTOToFilterEntity(Contract_ContractFilterDTO);
            ContractFilter = await ContractService.ToFilter(ContractFilter);
            int count = await ContractService.Count(ContractFilter);
            return count;
        }

        [Route(ContractRoute.List), HttpPost]
        public async Task<ActionResult<List<Contract_ContractDTO>>> List([FromBody] Contract_ContractFilterDTO Contract_ContractFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ContractFilter ContractFilter = ConvertFilterDTOToFilterEntity(Contract_ContractFilterDTO);
            ContractFilter = await ContractService.ToFilter(ContractFilter);
            List<Contract> Contracts = await ContractService.List(ContractFilter);
            List<Contract_ContractDTO> Contract_ContractDTOs = Contracts
                .Select(c => new Contract_ContractDTO(c)).ToList();
            return Contract_ContractDTOs;
        }

        [Route(ContractRoute.Get), HttpPost]
        public async Task<ActionResult<Contract_ContractDTO>> Get([FromBody] Contract_ContractDTO Contract_ContractDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Contract_ContractDTO.Id))
                return Forbid();

            Contract Contract = await ContractService.Get(Contract_ContractDTO.Id);
            return new Contract_ContractDTO(Contract);
        }

        [Route(ContractRoute.Create), HttpPost]
        public async Task<ActionResult<Contract_ContractDTO>> Create([FromBody] Contract_ContractDTO Contract_ContractDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Contract_ContractDTO.Id))
                return Forbid();

            Contract Contract = ConvertDTOToEntity(Contract_ContractDTO);
            Contract = await ContractService.Create(Contract);
            Contract_ContractDTO = new Contract_ContractDTO(Contract);
            if (Contract.IsValidated)
                return Contract_ContractDTO;
            else
                return BadRequest(Contract_ContractDTO);
        }

        [Route(ContractRoute.Update), HttpPost]
        public async Task<ActionResult<Contract_ContractDTO>> Update([FromBody] Contract_ContractDTO Contract_ContractDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Contract_ContractDTO.Id))
                return Forbid();

            Contract Contract = ConvertDTOToEntity(Contract_ContractDTO);
            Contract = await ContractService.Update(Contract);
            Contract_ContractDTO = new Contract_ContractDTO(Contract);
            if (Contract.IsValidated)
                return Contract_ContractDTO;
            else
                return BadRequest(Contract_ContractDTO);
        }

        [Route(ContractRoute.Delete), HttpPost]
        public async Task<ActionResult<Contract_ContractDTO>> Delete([FromBody] Contract_ContractDTO Contract_ContractDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Contract_ContractDTO.Id))
                return Forbid();

            Contract Contract = ConvertDTOToEntity(Contract_ContractDTO);
            Contract = await ContractService.Delete(Contract);
            Contract_ContractDTO = new Contract_ContractDTO(Contract);
            if (Contract.IsValidated)
                return Contract_ContractDTO;
            else
                return BadRequest(Contract_ContractDTO);
        }

        [Route(ContractRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
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

        [Route(ContractRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            AppUserFilter AssignedToAppUerFilter = new AppUserFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = AppUserSelect.ALL
            };
            List<AppUser> AssignedToAppUers = await AppUserService.List(AssignedToAppUerFilter);
            ContactFilter ContactFilter = new ContactFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = ContactSelect.ALL
            };
            List<Contact> Contacts = await ContactService.List(ContactFilter);
            ContractStatusFilter ContractStatusFilter = new ContractStatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = ContractStatusSelect.ALL
            };
            List<ContractStatus> ContractStatuses = await ContractStatusService.List(ContractStatusFilter);
            ContractTypeFilter ContractTypeFilter = new ContractTypeFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = ContractTypeSelect.ALL
            };
            List<ContractType> ContractTypes = await ContractTypeService.List(ContractTypeFilter);
            AppUserFilter CreatorFilter = new AppUserFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = AppUserSelect.ALL
            };
            List<AppUser> CreatedBies = await AppUserService.List(CreatorFilter);
            CurrencyFilter CurrencyFilter = new CurrencyFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CurrencySelect.ALL
            };
            List<Currency> Currencies = await CurrencyService.List(CurrencyFilter);
            CustomerFilter CustomerFilter = new CustomerFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CustomerSelect.ALL
            };
            List<Customer> Customers = await CustomerService.List(CustomerFilter);
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
            OpportunityFilter OpportunityFilter = new OpportunityFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = OpportunitySelect.ALL
            };
            List<Opportunity> Opportunities = await OpportunityService.List(OpportunityFilter);
            PaymentStatusFilter PaymentStatusFilter = new PaymentStatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = PaymentStatusSelect.ALL
            };
            List<PaymentStatus> PaymentStatuses = await PaymentStatusService.List(PaymentStatusFilter);
            DistrictFilter ReceiveDistrictFilter = new DistrictFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = DistrictSelect.ALL
            };
            List<District> ReceiveDistricts = await DistrictService.List(ReceiveDistrictFilter);
            NationFilter ReceiveNationFilter = new NationFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = NationSelect.ALL
            };
            List<Nation> ReceiveNations = await NationService.List(ReceiveNationFilter);
            ProvinceFilter ReceiveProvinceFilter = new ProvinceFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = ProvinceSelect.ALL
            };
            List<Province> ReceiveProvinces = await ProvinceService.List(ReceiveProvinceFilter);
            List<Contract> Contracts = new List<Contract>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(Contracts);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int CodeColumn = 1 + StartColumn;
                int NameColumn = 2 + StartColumn;
                int TotalValueColumn = 3 + StartColumn;
                int ValidityDateColumn = 4 + StartColumn;
                int ExpirationDateColumn = 5 + StartColumn;
                int DeliveryUnitColumn = 6 + StartColumn;
                int InvoiceAddressColumn = 7 + StartColumn;
                int InvoiceZipCodeColumn = 8 + StartColumn;
                int ReceiveAddressColumn = 9 + StartColumn;
                int ReceiveZipCodeColumn = 10 + StartColumn;
                int TermAndConditionColumn = 11 + StartColumn;
                int InvoiceNationIdColumn = 12 + StartColumn;
                int InvoiceProvinceIdColumn = 13 + StartColumn;
                int InvoiceDistrictIdColumn = 14 + StartColumn;
                int ReceiveNationIdColumn = 15 + StartColumn;
                int ReceiveProvinceIdColumn = 16 + StartColumn;
                int ReceiveDistrictIdColumn = 17 + StartColumn;
                int ContractTypeIdColumn = 18 + StartColumn;
                int ContactIdColumn = 19 + StartColumn;
                int CompanyIdColumn = 20 + StartColumn;
                int OpportunityIdColumn = 21 + StartColumn;
                int OrganizationIdColumn = 22 + StartColumn;
                int AssignedToAppUerIdColumn = 23 + StartColumn;
                int ContractStatusIdColumn = 24 + StartColumn;
                int CreatorIdColumn = 25 + StartColumn;
                int CustomerIdColumn = 26 + StartColumn;
                int CurrencyIdColumn = 27 + StartColumn;
                int PaymentStatusIdColumn = 28 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string CodeValue = worksheet.Cells[i + StartRow, CodeColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string TotalValueValue = worksheet.Cells[i + StartRow, TotalValueColumn].Value?.ToString();
                    string ValidityDateValue = worksheet.Cells[i + StartRow, ValidityDateColumn].Value?.ToString();
                    string ExpirationDateValue = worksheet.Cells[i + StartRow, ExpirationDateColumn].Value?.ToString();
                    string DeliveryUnitValue = worksheet.Cells[i + StartRow, DeliveryUnitColumn].Value?.ToString();
                    string InvoiceAddressValue = worksheet.Cells[i + StartRow, InvoiceAddressColumn].Value?.ToString();
                    string InvoiceZipCodeValue = worksheet.Cells[i + StartRow, InvoiceZipCodeColumn].Value?.ToString();
                    string ReceiveAddressValue = worksheet.Cells[i + StartRow, ReceiveAddressColumn].Value?.ToString();
                    string ReceiveZipCodeValue = worksheet.Cells[i + StartRow, ReceiveZipCodeColumn].Value?.ToString();
                    string TermAndConditionValue = worksheet.Cells[i + StartRow, TermAndConditionColumn].Value?.ToString();
                    string InvoiceNationIdValue = worksheet.Cells[i + StartRow, InvoiceNationIdColumn].Value?.ToString();
                    string InvoiceProvinceIdValue = worksheet.Cells[i + StartRow, InvoiceProvinceIdColumn].Value?.ToString();
                    string InvoiceDistrictIdValue = worksheet.Cells[i + StartRow, InvoiceDistrictIdColumn].Value?.ToString();
                    string ReceiveNationIdValue = worksheet.Cells[i + StartRow, ReceiveNationIdColumn].Value?.ToString();
                    string ReceiveProvinceIdValue = worksheet.Cells[i + StartRow, ReceiveProvinceIdColumn].Value?.ToString();
                    string ReceiveDistrictIdValue = worksheet.Cells[i + StartRow, ReceiveDistrictIdColumn].Value?.ToString();
                    string ContractTypeIdValue = worksheet.Cells[i + StartRow, ContractTypeIdColumn].Value?.ToString();
                    string ContactIdValue = worksheet.Cells[i + StartRow, ContactIdColumn].Value?.ToString();
                    string CompanyIdValue = worksheet.Cells[i + StartRow, CompanyIdColumn].Value?.ToString();
                    string OpportunityIdValue = worksheet.Cells[i + StartRow, OpportunityIdColumn].Value?.ToString();
                    string OrganizationIdValue = worksheet.Cells[i + StartRow, OrganizationIdColumn].Value?.ToString();
                    string ContractStatusIdValue = worksheet.Cells[i + StartRow, ContractStatusIdColumn].Value?.ToString();
                    string CreatorIdValue = worksheet.Cells[i + StartRow, CreatorIdColumn].Value?.ToString();
                    string CustomerIdValue = worksheet.Cells[i + StartRow, CustomerIdColumn].Value?.ToString();
                    string CurrencyIdValue = worksheet.Cells[i + StartRow, CurrencyIdColumn].Value?.ToString();
                    string PaymentStatusIdValue = worksheet.Cells[i + StartRow, PaymentStatusIdColumn].Value?.ToString();

                    Contract Contract = new Contract();
                    Contract.Code = CodeValue;
                    Contract.Name = NameValue;
                    Contract.TotalValue = decimal.TryParse(TotalValueValue, out decimal TotalValue) ? TotalValue : 0;
                    Contract.ValidityDate = DateTime.TryParse(ValidityDateValue, out DateTime ValidityDate) ? ValidityDate : DateTime.Now;
                    Contract.ExpirationDate = DateTime.TryParse(ExpirationDateValue, out DateTime ExpirationDate) ? ExpirationDate : DateTime.Now;
                    Contract.DeliveryUnit = DeliveryUnitValue;
                    Contract.InvoiceAddress = InvoiceAddressValue;
                    Contract.InvoiceZipCode = InvoiceZipCodeValue;
                    Contract.ReceiveAddress = ReceiveAddressValue;
                    Contract.ReceiveZipCode = ReceiveZipCodeValue;
                    Contract.TermAndCondition = TermAndConditionValue;
                    Contact Contact = Contacts.Where(x => x.Id.ToString() == ContactIdValue).FirstOrDefault();
                    ContractStatus ContractStatus = ContractStatuses.Where(x => x.Id.ToString() == ContractStatusIdValue).FirstOrDefault();
                    Contract.ContractStatusId = ContractStatus == null ? 0 : ContractStatus.Id;
                    Contract.ContractStatus = ContractStatus;
                    ContractType ContractType = ContractTypes.Where(x => x.Id.ToString() == ContractTypeIdValue).FirstOrDefault();
                    Contract.ContractTypeId = ContractType == null ? 0 : ContractType.Id;
                    Contract.ContractType = ContractType;
                    AppUser Creator = CreatedBies.Where(x => x.Id.ToString() == CreatorIdValue).FirstOrDefault();
                    Contract.CreatorId = Creator == null ? 0 : Creator.Id;
                    Contract.Creator = Creator;
                    Currency Currency = Currencies.Where(x => x.Id.ToString() == CurrencyIdValue).FirstOrDefault();
                    Contract.CurrencyId = Currency == null ? 0 : Currency.Id;
                    Contract.Currency = Currency;
                    Customer Customer = Customers.Where(x => x.Id.ToString() == CustomerIdValue).FirstOrDefault();
                    Contract.CustomerId = Customer == null ? 0 : Customer.Id;
                    Contract.Customer = Customer;
                    District InvoiceDistrict = InvoiceDistricts.Where(x => x.Id.ToString() == InvoiceDistrictIdValue).FirstOrDefault();
                    Contract.InvoiceDistrictId = InvoiceDistrict == null ? 0 : InvoiceDistrict.Id;
                    Contract.InvoiceDistrict = InvoiceDistrict;
                    Nation InvoiceNation = InvoiceNations.Where(x => x.Id.ToString() == InvoiceNationIdValue).FirstOrDefault();
                    Contract.InvoiceNationId = InvoiceNation == null ? 0 : InvoiceNation.Id;
                    Contract.InvoiceNation = InvoiceNation;
                    Province InvoiceProvince = InvoiceProvinces.Where(x => x.Id.ToString() == InvoiceProvinceIdValue).FirstOrDefault();
                    Contract.InvoiceProvinceId = InvoiceProvince == null ? 0 : InvoiceProvince.Id;
                    Contract.InvoiceProvince = InvoiceProvince;
                    Opportunity Opportunity = Opportunities.Where(x => x.Id.ToString() == OpportunityIdValue).FirstOrDefault();
                    Contract.OpportunityId = Opportunity == null ? 0 : Opportunity.Id;
                    Contract.Opportunity = Opportunity;
                    PaymentStatus PaymentStatus = PaymentStatuses.Where(x => x.Id.ToString() == PaymentStatusIdValue).FirstOrDefault();
                    Contract.PaymentStatusId = PaymentStatus == null ? 0 : PaymentStatus.Id;
                    Contract.PaymentStatus = PaymentStatus;
                    District ReceiveDistrict = ReceiveDistricts.Where(x => x.Id.ToString() == ReceiveDistrictIdValue).FirstOrDefault();
                    Contract.ReceiveDistrictId = ReceiveDistrict == null ? 0 : ReceiveDistrict.Id;
                    Contract.ReceiveDistrict = ReceiveDistrict;
                    Nation ReceiveNation = ReceiveNations.Where(x => x.Id.ToString() == ReceiveNationIdValue).FirstOrDefault();
                    Contract.ReceiveNationId = ReceiveNation == null ? 0 : ReceiveNation.Id;
                    Contract.ReceiveNation = ReceiveNation;
                    Province ReceiveProvince = ReceiveProvinces.Where(x => x.Id.ToString() == ReceiveProvinceIdValue).FirstOrDefault();
                    Contract.ReceiveProvinceId = ReceiveProvince == null ? 0 : ReceiveProvince.Id;
                    Contract.ReceiveProvince = ReceiveProvince;

                    Contracts.Add(Contract);
                }
            }
            Contracts = await ContractService.Import(Contracts);
            if (Contracts.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < Contracts.Count; i++)
                {
                    Contract Contract = Contracts[i];
                    if (!Contract.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (Contract.Errors.ContainsKey(nameof(Contract.Id)))
                            Error += Contract.Errors[nameof(Contract.Id)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.Code)))
                            Error += Contract.Errors[nameof(Contract.Code)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.Name)))
                            Error += Contract.Errors[nameof(Contract.Name)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.TotalValue)))
                            Error += Contract.Errors[nameof(Contract.TotalValue)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.ValidityDate)))
                            Error += Contract.Errors[nameof(Contract.ValidityDate)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.ExpirationDate)))
                            Error += Contract.Errors[nameof(Contract.ExpirationDate)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.DeliveryUnit)))
                            Error += Contract.Errors[nameof(Contract.DeliveryUnit)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.InvoiceAddress)))
                            Error += Contract.Errors[nameof(Contract.InvoiceAddress)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.InvoiceZipCode)))
                            Error += Contract.Errors[nameof(Contract.InvoiceZipCode)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.ReceiveAddress)))
                            Error += Contract.Errors[nameof(Contract.ReceiveAddress)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.ReceiveZipCode)))
                            Error += Contract.Errors[nameof(Contract.ReceiveZipCode)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.TermAndCondition)))
                            Error += Contract.Errors[nameof(Contract.TermAndCondition)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.InvoiceNationId)))
                            Error += Contract.Errors[nameof(Contract.InvoiceNationId)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.InvoiceProvinceId)))
                            Error += Contract.Errors[nameof(Contract.InvoiceProvinceId)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.InvoiceDistrictId)))
                            Error += Contract.Errors[nameof(Contract.InvoiceDistrictId)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.ReceiveNationId)))
                            Error += Contract.Errors[nameof(Contract.ReceiveNationId)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.ReceiveProvinceId)))
                            Error += Contract.Errors[nameof(Contract.ReceiveProvinceId)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.ReceiveDistrictId)))
                            Error += Contract.Errors[nameof(Contract.ReceiveDistrictId)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.ContractTypeId)))
                            Error += Contract.Errors[nameof(Contract.ContractTypeId)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.CompanyId)))
                            Error += Contract.Errors[nameof(Contract.CompanyId)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.OpportunityId)))
                            Error += Contract.Errors[nameof(Contract.OpportunityId)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.OrganizationId)))
                            Error += Contract.Errors[nameof(Contract.OrganizationId)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.ContractStatusId)))
                            Error += Contract.Errors[nameof(Contract.ContractStatusId)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.CreatorId)))
                            Error += Contract.Errors[nameof(Contract.CreatorId)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.CustomerId)))
                            Error += Contract.Errors[nameof(Contract.CustomerId)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.CurrencyId)))
                            Error += Contract.Errors[nameof(Contract.CurrencyId)];
                        if (Contract.Errors.ContainsKey(nameof(Contract.PaymentStatusId)))
                            Error += Contract.Errors[nameof(Contract.PaymentStatusId)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }

        [HttpPost]
        [Route(ContractRoute.UploadFile)]
        public async Task<ActionResult<Contract_FileDTO>> UploadFile(IFormFile file)
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
            File = await ContractService.UploadFile(File);
            if (File == null)
                return BadRequest();
            Contract_FileDTO Contract_FileDTO = new Contract_FileDTO
            {
                Id = File.Id,
                Name = File.Name,
                Url = File.Url,
                AppUserId = File.AppUserId
            };
            return Ok(Contract_FileDTO);
        }

        private async Task<bool> HasPermission(long Id)
        {
            ContractFilter ContractFilter = new ContractFilter();
            ContractFilter = await ContractService.ToFilter(ContractFilter);
            if (Id == 0)
            {

            }
            else
            {
                ContractFilter.Id = new IdFilter { Equal = Id };
                int count = await ContractService.Count(ContractFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private Contract ConvertDTOToEntity(Contract_ContractDTO Contract_ContractDTO)
        {
            Contract Contract = new Contract();
            Contract.Id = Contract_ContractDTO.Id;
            Contract.Code = Contract_ContractDTO.Code;
            Contract.Name = Contract_ContractDTO.Name;
            Contract.CompanyId = Contract_ContractDTO.CompanyId;
            Contract.OpportunityId = Contract_ContractDTO.OpportunityId;
            Contract.CustomerId = Contract_ContractDTO.CustomerId;
            Contract.ContractTypeId = Contract_ContractDTO.ContractTypeId;
            Contract.TotalValue = Contract_ContractDTO.TotalValue;
            Contract.CurrencyId = Contract_ContractDTO.CurrencyId;
            Contract.ValidityDate = Contract_ContractDTO.ValidityDate;
            Contract.ExpirationDate = Contract_ContractDTO.ExpirationDate;
            Contract.AppUserId = Contract_ContractDTO.AppUserId;
            Contract.DeliveryUnit = Contract_ContractDTO.DeliveryUnit;
            Contract.ContractStatusId = Contract_ContractDTO.ContractStatusId;
            Contract.PaymentStatusId = Contract_ContractDTO.PaymentStatusId;
            Contract.InvoiceAddress = Contract_ContractDTO.InvoiceAddress;
            Contract.InvoiceNationId = Contract_ContractDTO.InvoiceNationId;
            Contract.InvoiceProvinceId = Contract_ContractDTO.InvoiceProvinceId;
            Contract.InvoiceDistrictId = Contract_ContractDTO.InvoiceDistrictId;
            Contract.InvoiceZipCode = Contract_ContractDTO.InvoiceZipCode;
            Contract.ReceiveAddress = Contract_ContractDTO.ReceiveAddress;
            Contract.ReceiveNationId = Contract_ContractDTO.ReceiveNationId;
            Contract.ReceiveProvinceId = Contract_ContractDTO.ReceiveProvinceId;
            Contract.ReceiveDistrictId = Contract_ContractDTO.ReceiveDistrictId;
            Contract.ReceiveZipCode = Contract_ContractDTO.ReceiveZipCode;
            Contract.SubTotal = Contract_ContractDTO.SubTotal;
            Contract.GeneralDiscountPercentage = Contract_ContractDTO.GeneralDiscountPercentage;
            Contract.GeneralDiscountAmount = Contract_ContractDTO.GeneralDiscountAmount;
            Contract.TotalTaxAmountOther = Contract_ContractDTO.TotalTaxAmountOther;
            Contract.TotalTaxAmount = Contract_ContractDTO.TotalTaxAmount;
            Contract.Total = Contract_ContractDTO.Total;
            Contract.TermAndCondition = Contract_ContractDTO.TermAndCondition;
            Contract.CreatorId = Contract_ContractDTO.CreatorId;
            Contract.OrganizationId = Contract_ContractDTO.OrganizationId;
            Contract.AppUser = Contract_ContractDTO.AppUser == null ? null : new AppUser
            {
                Id = Contract_ContractDTO.AppUser.Id,
                Username = Contract_ContractDTO.AppUser.Username,
                DisplayName = Contract_ContractDTO.AppUser.DisplayName,
                Address = Contract_ContractDTO.AppUser.Address,
                Email = Contract_ContractDTO.AppUser.Email,
                Phone = Contract_ContractDTO.AppUser.Phone,
                SexId = Contract_ContractDTO.AppUser.SexId,
                Birthday = Contract_ContractDTO.AppUser.Birthday,
                Avatar = Contract_ContractDTO.AppUser.Avatar,
                Department = Contract_ContractDTO.AppUser.Department,
                OrganizationId = Contract_ContractDTO.AppUser.OrganizationId,
                Longitude = Contract_ContractDTO.AppUser.Longitude,
                Latitude = Contract_ContractDTO.AppUser.Latitude,
                StatusId = Contract_ContractDTO.AppUser.StatusId,
            };
            Contract.Company = Contract_ContractDTO.Company == null ? null : new Company
            {
                Id = Contract_ContractDTO.Company.Id,
                Name = Contract_ContractDTO.Company.Name,
                Phone = Contract_ContractDTO.Company.Phone,
                FAX = Contract_ContractDTO.Company.FAX,
                PhoneOther = Contract_ContractDTO.Company.PhoneOther,
                Email = Contract_ContractDTO.Company.Email,
                EmailOther = Contract_ContractDTO.Company.EmailOther,
                ZIPCode = Contract_ContractDTO.Company.ZIPCode,
                Revenue = Contract_ContractDTO.Company.Revenue,
                Website = Contract_ContractDTO.Company.Website,
                Address = Contract_ContractDTO.Company.Address,
                NationId = Contract_ContractDTO.Company.NationId,
                ProvinceId = Contract_ContractDTO.Company.ProvinceId,
                DistrictId = Contract_ContractDTO.Company.DistrictId,
                NumberOfEmployee = Contract_ContractDTO.Company.NumberOfEmployee,
                RefuseReciveEmail = Contract_ContractDTO.Company.RefuseReciveEmail,
                RefuseReciveSMS = Contract_ContractDTO.Company.RefuseReciveSMS,
                CustomerLeadId = Contract_ContractDTO.Company.CustomerLeadId,
                ParentId = Contract_ContractDTO.Company.ParentId,
                Path = Contract_ContractDTO.Company.Path,
                Level = Contract_ContractDTO.Company.Level,
                ProfessionId = Contract_ContractDTO.Company.ProfessionId,
                AppUserId = Contract_ContractDTO.Company.AppUserId,
                CurrencyId = Contract_ContractDTO.Company.CurrencyId,
                CompanyStatusId = Contract_ContractDTO.Company.CompanyStatusId,
                Description = Contract_ContractDTO.Company.Description,
                RowId = Contract_ContractDTO.Company.RowId,
            };
            Contract.ContractStatus = Contract_ContractDTO.ContractStatus == null ? null : new ContractStatus
            {
                Id = Contract_ContractDTO.ContractStatus.Id,
                Name = Contract_ContractDTO.ContractStatus.Name,
                Code = Contract_ContractDTO.ContractStatus.Code,
            };
            Contract.ContractType = Contract_ContractDTO.ContractType == null ? null : new ContractType
            {
                Id = Contract_ContractDTO.ContractType.Id,
                Name = Contract_ContractDTO.ContractType.Name,
                Code = Contract_ContractDTO.ContractType.Code,
            };
            Contract.Creator = Contract_ContractDTO.Creator == null ? null : new AppUser
            {
                Id = Contract_ContractDTO.Creator.Id,
                Username = Contract_ContractDTO.Creator.Username,
                DisplayName = Contract_ContractDTO.Creator.DisplayName,
                Address = Contract_ContractDTO.Creator.Address,
                Email = Contract_ContractDTO.Creator.Email,
                Phone = Contract_ContractDTO.Creator.Phone,
                SexId = Contract_ContractDTO.Creator.SexId,
                Birthday = Contract_ContractDTO.Creator.Birthday,
                Avatar = Contract_ContractDTO.Creator.Avatar,
                Department = Contract_ContractDTO.Creator.Department,
                OrganizationId = Contract_ContractDTO.Creator.OrganizationId,
                Longitude = Contract_ContractDTO.Creator.Longitude,
                Latitude = Contract_ContractDTO.Creator.Latitude,
                StatusId = Contract_ContractDTO.Creator.StatusId,
            };
            Contract.Currency = Contract_ContractDTO.Currency == null ? null : new Currency
            {
                Id = Contract_ContractDTO.Currency.Id,
                Code = Contract_ContractDTO.Currency.Code,
                Name = Contract_ContractDTO.Currency.Name,
            };
            Contract.Customer = Contract_ContractDTO.Customer == null ? null : new Customer
            {
                Id = Contract_ContractDTO.Customer.Id,
                Code = Contract_ContractDTO.Customer.Code,
                Name = Contract_ContractDTO.Customer.Name,
                Phone = Contract_ContractDTO.Customer.Phone,
                Address = Contract_ContractDTO.Customer.Address,
                NationId = Contract_ContractDTO.Customer.NationId,
                ProvinceId = Contract_ContractDTO.Customer.ProvinceId,
                DistrictId = Contract_ContractDTO.Customer.DistrictId,
                WardId = Contract_ContractDTO.Customer.WardId,
                CustomerTypeId = Contract_ContractDTO.Customer.CustomerTypeId,
                Birthday = Contract_ContractDTO.Customer.Birthday,
                Email = Contract_ContractDTO.Customer.Email,
                ProfessionId = Contract_ContractDTO.Customer.ProfessionId,
                CustomerResourceId = Contract_ContractDTO.Customer.CustomerResourceId,
                SexId = Contract_ContractDTO.Customer.SexId,
                StatusId = Contract_ContractDTO.Customer.StatusId,
                CompanyId = Contract_ContractDTO.Customer.CompanyId,
                ParentCompanyId = Contract_ContractDTO.Customer.ParentCompanyId,
                TaxCode = Contract_ContractDTO.Customer.TaxCode,
                Fax = Contract_ContractDTO.Customer.Fax,
                Website = Contract_ContractDTO.Customer.Website,
                NumberOfEmployee = Contract_ContractDTO.Customer.NumberOfEmployee,
                BusinessTypeId = Contract_ContractDTO.Customer.BusinessTypeId,
                Investment = Contract_ContractDTO.Customer.Investment,
                RevenueAnnual = Contract_ContractDTO.Customer.RevenueAnnual,
                IsSupplier = Contract_ContractDTO.Customer.IsSupplier,
                Descreption = Contract_ContractDTO.Customer.Descreption,
                CreatorId = Contract_ContractDTO.Customer.CreatorId,
                Used = Contract_ContractDTO.Customer.Used,
                RowId = Contract_ContractDTO.Customer.RowId,
            };
            Contract.InvoiceDistrict = Contract_ContractDTO.InvoiceDistrict == null ? null : new District
            {
                Id = Contract_ContractDTO.InvoiceDistrict.Id,
                Code = Contract_ContractDTO.InvoiceDistrict.Code,
                Name = Contract_ContractDTO.InvoiceDistrict.Name,
                Priority = Contract_ContractDTO.InvoiceDistrict.Priority,
                ProvinceId = Contract_ContractDTO.InvoiceDistrict.ProvinceId,
                StatusId = Contract_ContractDTO.InvoiceDistrict.StatusId,
            };
            Contract.InvoiceNation = Contract_ContractDTO.InvoiceNation == null ? null : new Nation
            {
                Id = Contract_ContractDTO.InvoiceNation.Id,
                Code = Contract_ContractDTO.InvoiceNation.Code,
                Name = Contract_ContractDTO.InvoiceNation.Name,
                StatusId = Contract_ContractDTO.InvoiceNation.StatusId,
            };
            Contract.InvoiceProvince = Contract_ContractDTO.InvoiceProvince == null ? null : new Province
            {
                Id = Contract_ContractDTO.InvoiceProvince.Id,
                Code = Contract_ContractDTO.InvoiceProvince.Code,
                Name = Contract_ContractDTO.InvoiceProvince.Name,
                Priority = Contract_ContractDTO.InvoiceProvince.Priority,
                StatusId = Contract_ContractDTO.InvoiceProvince.StatusId,
            };
            Contract.Opportunity = Contract_ContractDTO.Opportunity == null ? null : new Opportunity
            {
                Id = Contract_ContractDTO.Opportunity.Id,
                Name = Contract_ContractDTO.Opportunity.Name,
                CompanyId = Contract_ContractDTO.Opportunity.CompanyId,
                CustomerLeadId = Contract_ContractDTO.Opportunity.CustomerLeadId,
                ClosingDate = Contract_ContractDTO.Opportunity.ClosingDate,
                SaleStageId = Contract_ContractDTO.Opportunity.SaleStageId,
                ProbabilityId = Contract_ContractDTO.Opportunity.ProbabilityId,
                PotentialResultId = Contract_ContractDTO.Opportunity.PotentialResultId,
                LeadSourceId = Contract_ContractDTO.Opportunity.LeadSourceId,
                AppUserId = Contract_ContractDTO.Opportunity.AppUserId,
                CurrencyId = Contract_ContractDTO.Opportunity.CurrencyId,
                Amount = Contract_ContractDTO.Opportunity.Amount,
                ForecastAmount = Contract_ContractDTO.Opportunity.ForecastAmount,
                Description = Contract_ContractDTO.Opportunity.Description,
                RefuseReciveSMS = Contract_ContractDTO.Opportunity.RefuseReciveSMS,
                RefuseReciveEmail = Contract_ContractDTO.Opportunity.RefuseReciveEmail,
                OpportunityResultTypeId = Contract_ContractDTO.Opportunity.OpportunityResultTypeId,
                CreatorId = Contract_ContractDTO.Opportunity.CreatorId,
            };
            Contract.Organization = Contract_ContractDTO.Organization == null ? null : new Organization
            {
                Id = Contract_ContractDTO.Organization.Id,
                Code = Contract_ContractDTO.Organization.Code,
                Name = Contract_ContractDTO.Organization.Name,
                ParentId = Contract_ContractDTO.Organization.ParentId,
                Path = Contract_ContractDTO.Organization.Path,
                Level = Contract_ContractDTO.Organization.Level,
                StatusId = Contract_ContractDTO.Organization.StatusId,
                Phone = Contract_ContractDTO.Organization.Phone,
                Email = Contract_ContractDTO.Organization.Email,
                Address = Contract_ContractDTO.Organization.Address,
            };
            Contract.PaymentStatus = Contract_ContractDTO.PaymentStatus == null ? null : new PaymentStatus
            {
                Id = Contract_ContractDTO.PaymentStatus.Id,
                Code = Contract_ContractDTO.PaymentStatus.Code,
                Name = Contract_ContractDTO.PaymentStatus.Name,
            };
            Contract.ReceiveDistrict = Contract_ContractDTO.ReceiveDistrict == null ? null : new District
            {
                Id = Contract_ContractDTO.ReceiveDistrict.Id,
                Code = Contract_ContractDTO.ReceiveDistrict.Code,
                Name = Contract_ContractDTO.ReceiveDistrict.Name,
                Priority = Contract_ContractDTO.ReceiveDistrict.Priority,
                ProvinceId = Contract_ContractDTO.ReceiveDistrict.ProvinceId,
                StatusId = Contract_ContractDTO.ReceiveDistrict.StatusId,
            };
            Contract.ReceiveNation = Contract_ContractDTO.ReceiveNation == null ? null : new Nation
            {
                Id = Contract_ContractDTO.ReceiveNation.Id,
                Code = Contract_ContractDTO.ReceiveNation.Code,
                Name = Contract_ContractDTO.ReceiveNation.Name,
                StatusId = Contract_ContractDTO.ReceiveNation.StatusId,
            };
            Contract.ReceiveProvince = Contract_ContractDTO.ReceiveProvince == null ? null : new Province
            {
                Id = Contract_ContractDTO.ReceiveProvince.Id,
                Code = Contract_ContractDTO.ReceiveProvince.Code,
                Name = Contract_ContractDTO.ReceiveProvince.Name,
                Priority = Contract_ContractDTO.ReceiveProvince.Priority,
                StatusId = Contract_ContractDTO.ReceiveProvince.StatusId,
            };
            Contract.ContractContactMappings = Contract_ContractDTO.ContractContactMappings?
                .Select(x => new ContractContactMapping
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
                        CreatorId = x.Contact.CreatorId,
                    },
                }).ToList();
            Contract.ContractFileGroupings = Contract_ContractDTO.ContractFileGroupings?
                .Select(x => new ContractFileGrouping
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
                    },
                    FileType = x.FileType == null ? null : new FileType
                    {
                        Id = x.FileType.Id,
                        Code = x.FileType.Code,
                        Name = x.FileType.Name,
                    },
                }).ToList();
            Contract.ContractItemDetails = Contract_ContractDTO.ContractItemDetails?
                .Select(x => new ContractItemDetail
                {
                    Id = x.Id,
                    ContractId = x.ContractId,
                    ItemId = x.ItemId,
                    UnitOfMeasureId = x.UnitOfMeasureId,
                    PrimaryUnitOfMeasureId = x.PrimaryUnitOfMeasureId,
                    Quantity = x.Quantity,
                    RequestQuantity = x.RequestQuantity,
                    PrimaryPrice = x.PrimaryPrice,
                    SalePrice = x.SalePrice,
                    DiscountPercentage = x.DiscountPercentage,
                    DiscountAmount = x.DiscountAmount,
                    GeneralDiscountPercentage = x.GeneralDiscountPercentage,
                    GeneralDiscountAmount = x.GeneralDiscountAmount,
                    TaxPercentage = x.TaxPercentage,
                    TaxAmount = x.TaxAmount,
                    TaxPercentageOther = x.TaxPercentageOther,
                    TaxAmountOther = x.TaxAmountOther,
                    TotalPrice = x.TotalPrice,
                    Factor = x.Factor,
                    TaxTypeId = x.TaxTypeId,
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
            Contract.ContractPaymentHistories = Contract_ContractDTO.ContractPaymentHistories?
                .Select(x => new ContractPaymentHistory
                {
                    Id = x.Id,
                    PaymentMilestone = x.PaymentMilestone,
                    PaymentPercentage = x.PaymentPercentage,
                    PaymentAmount = x.PaymentAmount,
                    Description = x.Description,
                    IsPaid = x.IsPaid,
                }).ToList();
            Contract.BaseLanguage = CurrentContext.Language;
            return Contract;
        }

        private ContractFilter ConvertFilterDTOToFilterEntity(Contract_ContractFilterDTO Contract_ContractFilterDTO)
        {
            ContractFilter ContractFilter = new ContractFilter();
            ContractFilter.Selects = ContractSelect.ALL;
            ContractFilter.Skip = Contract_ContractFilterDTO.Skip;
            ContractFilter.Take = Contract_ContractFilterDTO.Take;
            ContractFilter.OrderBy = Contract_ContractFilterDTO.OrderBy;
            ContractFilter.OrderType = Contract_ContractFilterDTO.OrderType;

            ContractFilter.Id = Contract_ContractFilterDTO.Id;
            ContractFilter.Code = Contract_ContractFilterDTO.Code;
            ContractFilter.Name = Contract_ContractFilterDTO.Name;
            ContractFilter.CompanyId = Contract_ContractFilterDTO.CompanyId;
            ContractFilter.OpportunityId = Contract_ContractFilterDTO.OpportunityId;
            ContractFilter.CustomerId = Contract_ContractFilterDTO.CustomerId;
            ContractFilter.ContractTypeId = Contract_ContractFilterDTO.ContractTypeId;
            ContractFilter.TotalValue = Contract_ContractFilterDTO.TotalValue;
            ContractFilter.CurrencyId = Contract_ContractFilterDTO.CurrencyId;
            ContractFilter.ValidityDate = Contract_ContractFilterDTO.ValidityDate;
            ContractFilter.ExpirationDate = Contract_ContractFilterDTO.ExpirationDate;
            ContractFilter.AppUserId = Contract_ContractFilterDTO.AppUserId;
            ContractFilter.DeliveryUnit = Contract_ContractFilterDTO.DeliveryUnit;
            ContractFilter.ContractStatusId = Contract_ContractFilterDTO.ContractStatusId;
            ContractFilter.PaymentStatusId = Contract_ContractFilterDTO.PaymentStatusId;
            ContractFilter.InvoiceAddress = Contract_ContractFilterDTO.InvoiceAddress;
            ContractFilter.InvoiceNationId = Contract_ContractFilterDTO.InvoiceNationId;
            ContractFilter.InvoiceProvinceId = Contract_ContractFilterDTO.InvoiceProvinceId;
            ContractFilter.InvoiceDistrictId = Contract_ContractFilterDTO.InvoiceDistrictId;
            ContractFilter.InvoiceZipCode = Contract_ContractFilterDTO.InvoiceZipCode;
            ContractFilter.ReceiveAddress = Contract_ContractFilterDTO.ReceiveAddress;
            ContractFilter.ReceiveNationId = Contract_ContractFilterDTO.ReceiveNationId;
            ContractFilter.ReceiveProvinceId = Contract_ContractFilterDTO.ReceiveProvinceId;
            ContractFilter.ReceiveDistrictId = Contract_ContractFilterDTO.ReceiveDistrictId;
            ContractFilter.ReceiveZipCode = Contract_ContractFilterDTO.ReceiveZipCode;
            ContractFilter.SubTotal = Contract_ContractFilterDTO.SubTotal;
            ContractFilter.GeneralDiscountPercentage = Contract_ContractFilterDTO.GeneralDiscountPercentage;
            ContractFilter.GeneralDiscountAmount = Contract_ContractFilterDTO.GeneralDiscountAmount;
            ContractFilter.TotalTaxAmountOther = Contract_ContractFilterDTO.TotalTaxAmountOther;
            ContractFilter.TotalTaxAmount = Contract_ContractFilterDTO.TotalTaxAmount;
            ContractFilter.Total = Contract_ContractFilterDTO.Total;
            ContractFilter.TermAndCondition = Contract_ContractFilterDTO.TermAndCondition;
            ContractFilter.CreatorId = Contract_ContractFilterDTO.CreatorId;
            ContractFilter.OrganizationId = Contract_ContractFilterDTO.OrganizationId;
            ContractFilter.CreatedAt = Contract_ContractFilterDTO.CreatedAt;
            ContractFilter.UpdatedAt = Contract_ContractFilterDTO.UpdatedAt;
            return ContractFilter;
        }
    }
}

