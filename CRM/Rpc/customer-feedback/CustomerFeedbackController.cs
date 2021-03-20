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
using CRM.Services.MCustomerFeedback;
using CRM.Services.MCustomer;
using CRM.Services.MCustomerFeedbackType;
using CRM.Services.MStatus;
using CRM.Models;

namespace CRM.Rpc.customer_feedback
{
    public partial class CustomerFeedbackController : RpcController
    {
        private ICustomerService CustomerService;
        private ICustomerFeedbackTypeService CustomerFeedbackTypeService;
        private IStatusService StatusService;
        private ICustomerFeedbackService CustomerFeedbackService;
        private ICurrentContext CurrentContext;
        public CustomerFeedbackController(
            ICustomerService CustomerService,
            ICustomerFeedbackTypeService CustomerFeedbackTypeService,
            IStatusService StatusService,
            ICustomerFeedbackService CustomerFeedbackService,
            ICurrentContext CurrentContext
       ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.CustomerService = CustomerService;
            this.CustomerFeedbackTypeService = CustomerFeedbackTypeService;
            this.StatusService = StatusService;
            this.CustomerFeedbackService = CustomerFeedbackService;
            this.CurrentContext = CurrentContext;
        }

        [Route(CustomerFeedbackRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] CustomerFeedback_CustomerFeedbackFilterDTO CustomerFeedback_CustomerFeedbackFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFeedbackFilter CustomerFeedbackFilter = ConvertFilterDTOToFilterEntity(CustomerFeedback_CustomerFeedbackFilterDTO);
            CustomerFeedbackFilter = await CustomerFeedbackService.ToFilter(CustomerFeedbackFilter);
            int count = await CustomerFeedbackService.Count(CustomerFeedbackFilter);
            return count;
        }

        [Route(CustomerFeedbackRoute.List), HttpPost]
        public async Task<ActionResult<List<CustomerFeedback_CustomerFeedbackDTO>>> List([FromBody] CustomerFeedback_CustomerFeedbackFilterDTO CustomerFeedback_CustomerFeedbackFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFeedbackFilter CustomerFeedbackFilter = ConvertFilterDTOToFilterEntity(CustomerFeedback_CustomerFeedbackFilterDTO);
            CustomerFeedbackFilter = await CustomerFeedbackService.ToFilter(CustomerFeedbackFilter);
            List<CustomerFeedback> CustomerFeedbacks = await CustomerFeedbackService.List(CustomerFeedbackFilter);
            List<CustomerFeedback_CustomerFeedbackDTO> CustomerFeedback_CustomerFeedbackDTOs = CustomerFeedbacks
                .Select(c => new CustomerFeedback_CustomerFeedbackDTO(c)).ToList();
            return CustomerFeedback_CustomerFeedbackDTOs;
        }

        [Route(CustomerFeedbackRoute.Get), HttpPost]
        public async Task<ActionResult<CustomerFeedback_CustomerFeedbackDTO>> Get([FromBody]CustomerFeedback_CustomerFeedbackDTO CustomerFeedback_CustomerFeedbackDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CustomerFeedback_CustomerFeedbackDTO.Id))
                return Forbid();

            CustomerFeedback CustomerFeedback = await CustomerFeedbackService.Get(CustomerFeedback_CustomerFeedbackDTO.Id);
            return new CustomerFeedback_CustomerFeedbackDTO(CustomerFeedback);
        }

        [Route(CustomerFeedbackRoute.Create), HttpPost]
        public async Task<ActionResult<CustomerFeedback_CustomerFeedbackDTO>> Create([FromBody] CustomerFeedback_CustomerFeedbackDTO CustomerFeedback_CustomerFeedbackDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(CustomerFeedback_CustomerFeedbackDTO.Id))
                return Forbid();

            CustomerFeedback CustomerFeedback = ConvertDTOToEntity(CustomerFeedback_CustomerFeedbackDTO);
            CustomerFeedback = await CustomerFeedbackService.Create(CustomerFeedback);
            CustomerFeedback_CustomerFeedbackDTO = new CustomerFeedback_CustomerFeedbackDTO(CustomerFeedback);
            if (CustomerFeedback.IsValidated)
                return CustomerFeedback_CustomerFeedbackDTO;
            else
                return BadRequest(CustomerFeedback_CustomerFeedbackDTO);
        }

        [Route(CustomerFeedbackRoute.Update), HttpPost]
        public async Task<ActionResult<CustomerFeedback_CustomerFeedbackDTO>> Update([FromBody] CustomerFeedback_CustomerFeedbackDTO CustomerFeedback_CustomerFeedbackDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(CustomerFeedback_CustomerFeedbackDTO.Id))
                return Forbid();

            CustomerFeedback CustomerFeedback = ConvertDTOToEntity(CustomerFeedback_CustomerFeedbackDTO);
            CustomerFeedback = await CustomerFeedbackService.Update(CustomerFeedback);
            CustomerFeedback_CustomerFeedbackDTO = new CustomerFeedback_CustomerFeedbackDTO(CustomerFeedback);
            if (CustomerFeedback.IsValidated)
                return CustomerFeedback_CustomerFeedbackDTO;
            else
                return BadRequest(CustomerFeedback_CustomerFeedbackDTO);
        }

        [Route(CustomerFeedbackRoute.Delete), HttpPost]
        public async Task<ActionResult<CustomerFeedback_CustomerFeedbackDTO>> Delete([FromBody] CustomerFeedback_CustomerFeedbackDTO CustomerFeedback_CustomerFeedbackDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CustomerFeedback_CustomerFeedbackDTO.Id))
                return Forbid();

            CustomerFeedback CustomerFeedback = ConvertDTOToEntity(CustomerFeedback_CustomerFeedbackDTO);
            CustomerFeedback = await CustomerFeedbackService.Delete(CustomerFeedback);
            CustomerFeedback_CustomerFeedbackDTO = new CustomerFeedback_CustomerFeedbackDTO(CustomerFeedback);
            if (CustomerFeedback.IsValidated)
                return CustomerFeedback_CustomerFeedbackDTO;
            else
                return BadRequest(CustomerFeedback_CustomerFeedbackDTO);
        }
        
        [Route(CustomerFeedbackRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerFeedbackFilter CustomerFeedbackFilter = new CustomerFeedbackFilter();
            CustomerFeedbackFilter = await CustomerFeedbackService.ToFilter(CustomerFeedbackFilter);
            CustomerFeedbackFilter.Id = new IdFilter { In = Ids };
            CustomerFeedbackFilter.Selects = CustomerFeedbackSelect.Id;
            CustomerFeedbackFilter.Skip = 0;
            CustomerFeedbackFilter.Take = int.MaxValue;

            List<CustomerFeedback> CustomerFeedbacks = await CustomerFeedbackService.List(CustomerFeedbackFilter);
            CustomerFeedbacks = await CustomerFeedbackService.BulkDelete(CustomerFeedbacks);
            if (CustomerFeedbacks.Any(x => !x.IsValidated))
                return BadRequest(CustomerFeedbacks.Where(x => !x.IsValidated));
            return true;
        }
        
        [Route(CustomerFeedbackRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            CustomerFilter CustomerFilter = new CustomerFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CustomerSelect.ALL
            };
            List<Customer> Customers = await CustomerService.List(CustomerFilter);
            CustomerFeedbackTypeFilter CustomerFeedbackTypeFilter = new CustomerFeedbackTypeFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CustomerFeedbackTypeSelect.ALL
            };
            List<CustomerFeedbackType> CustomerFeedbackTypes = await CustomerFeedbackTypeService.List(CustomerFeedbackTypeFilter);
            StatusFilter StatusFilter = new StatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = StatusSelect.ALL
            };
            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<CustomerFeedback> CustomerFeedbacks = new List<CustomerFeedback>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(CustomerFeedbacks);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int IsSystemCustomerColumn = 1 + StartColumn;
                int CustomerIdColumn = 2 + StartColumn;
                int FullNameColumn = 3 + StartColumn;
                int EmailColumn = 4 + StartColumn;
                int PhoneNumberColumn = 5 + StartColumn;
                int CustomerFeedbackTypeIdColumn = 6 + StartColumn;
                int TitleColumn = 7 + StartColumn;
                int SendDateColumn = 8 + StartColumn;
                int ContentColumn = 9 + StartColumn;
                int StatusIdColumn = 10 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string IsSystemCustomerValue = worksheet.Cells[i + StartRow, IsSystemCustomerColumn].Value?.ToString();
                    string CustomerIdValue = worksheet.Cells[i + StartRow, CustomerIdColumn].Value?.ToString();
                    string FullNameValue = worksheet.Cells[i + StartRow, FullNameColumn].Value?.ToString();
                    string EmailValue = worksheet.Cells[i + StartRow, EmailColumn].Value?.ToString();
                    string PhoneNumberValue = worksheet.Cells[i + StartRow, PhoneNumberColumn].Value?.ToString();
                    string CustomerFeedbackTypeIdValue = worksheet.Cells[i + StartRow, CustomerFeedbackTypeIdColumn].Value?.ToString();
                    string TitleValue = worksheet.Cells[i + StartRow, TitleColumn].Value?.ToString();
                    string SendDateValue = worksheet.Cells[i + StartRow, SendDateColumn].Value?.ToString();
                    string ContentValue = worksheet.Cells[i + StartRow, ContentColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    
                    CustomerFeedback CustomerFeedback = new CustomerFeedback();
                    CustomerFeedback.FullName = FullNameValue;
                    CustomerFeedback.Email = EmailValue;
                    CustomerFeedback.PhoneNumber = PhoneNumberValue;
                    CustomerFeedback.Title = TitleValue;
                    CustomerFeedback.SendDate = DateTime.TryParse(SendDateValue, out DateTime SendDate) ? SendDate : DateTime.Now;
                    CustomerFeedback.Content = ContentValue;
                    Customer Customer = Customers.Where(x => x.Id.ToString() == CustomerIdValue).FirstOrDefault();
                    CustomerFeedback.CustomerId = Customer == null ? 0 : Customer.Id;
                    CustomerFeedback.Customer = Customer;
                    CustomerFeedbackType CustomerFeedbackType = CustomerFeedbackTypes.Where(x => x.Id.ToString() == CustomerFeedbackTypeIdValue).FirstOrDefault();
                    CustomerFeedback.CustomerFeedbackTypeId = CustomerFeedbackType == null ? 0 : CustomerFeedbackType.Id;
                    CustomerFeedback.CustomerFeedbackType = CustomerFeedbackType;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    CustomerFeedback.StatusId = Status == null ? 0 : Status.Id;
                    CustomerFeedback.Status = Status;
                    
                    CustomerFeedbacks.Add(CustomerFeedback);
                }
            }
            CustomerFeedbacks = await CustomerFeedbackService.Import(CustomerFeedbacks);
            if (CustomerFeedbacks.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < CustomerFeedbacks.Count; i++)
                {
                    CustomerFeedback CustomerFeedback = CustomerFeedbacks[i];
                    if (!CustomerFeedback.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (CustomerFeedback.Errors.ContainsKey(nameof(CustomerFeedback.Id)))
                            Error += CustomerFeedback.Errors[nameof(CustomerFeedback.Id)];
                        if (CustomerFeedback.Errors.ContainsKey(nameof(CustomerFeedback.IsSystemCustomer)))
                            Error += CustomerFeedback.Errors[nameof(CustomerFeedback.IsSystemCustomer)];
                        if (CustomerFeedback.Errors.ContainsKey(nameof(CustomerFeedback.CustomerId)))
                            Error += CustomerFeedback.Errors[nameof(CustomerFeedback.CustomerId)];
                        if (CustomerFeedback.Errors.ContainsKey(nameof(CustomerFeedback.FullName)))
                            Error += CustomerFeedback.Errors[nameof(CustomerFeedback.FullName)];
                        if (CustomerFeedback.Errors.ContainsKey(nameof(CustomerFeedback.Email)))
                            Error += CustomerFeedback.Errors[nameof(CustomerFeedback.Email)];
                        if (CustomerFeedback.Errors.ContainsKey(nameof(CustomerFeedback.PhoneNumber)))
                            Error += CustomerFeedback.Errors[nameof(CustomerFeedback.PhoneNumber)];
                        if (CustomerFeedback.Errors.ContainsKey(nameof(CustomerFeedback.CustomerFeedbackTypeId)))
                            Error += CustomerFeedback.Errors[nameof(CustomerFeedback.CustomerFeedbackTypeId)];
                        if (CustomerFeedback.Errors.ContainsKey(nameof(CustomerFeedback.Title)))
                            Error += CustomerFeedback.Errors[nameof(CustomerFeedback.Title)];
                        if (CustomerFeedback.Errors.ContainsKey(nameof(CustomerFeedback.SendDate)))
                            Error += CustomerFeedback.Errors[nameof(CustomerFeedback.SendDate)];
                        if (CustomerFeedback.Errors.ContainsKey(nameof(CustomerFeedback.Content)))
                            Error += CustomerFeedback.Errors[nameof(CustomerFeedback.Content)];
                        if (CustomerFeedback.Errors.ContainsKey(nameof(CustomerFeedback.StatusId)))
                            Error += CustomerFeedback.Errors[nameof(CustomerFeedback.StatusId)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(CustomerFeedbackRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] CustomerFeedback_CustomerFeedbackFilterDTO CustomerFeedback_CustomerFeedbackFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region CustomerFeedback
                var CustomerFeedbackFilter = ConvertFilterDTOToFilterEntity(CustomerFeedback_CustomerFeedbackFilterDTO);
                CustomerFeedbackFilter.Skip = 0;
                CustomerFeedbackFilter.Take = int.MaxValue;
                CustomerFeedbackFilter = await CustomerFeedbackService.ToFilter(CustomerFeedbackFilter);
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
                excel.Save();
            }
            return File(memoryStream.ToArray(), "application/octet-stream", "CustomerFeedback.xlsx");
        }

        [Route(CustomerFeedbackRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] CustomerFeedback_CustomerFeedbackFilterDTO CustomerFeedback_CustomerFeedbackFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            string path = "Templates/CustomerFeedback_Template.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "CustomerFeedback.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            CustomerFeedbackFilter CustomerFeedbackFilter = new CustomerFeedbackFilter();
            CustomerFeedbackFilter = await CustomerFeedbackService.ToFilter(CustomerFeedbackFilter);
            if (Id == 0)
            {

            }
            else
            {
                CustomerFeedbackFilter.Id = new IdFilter { Equal = Id };
                int count = await CustomerFeedbackService.Count(CustomerFeedbackFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private CustomerFeedback ConvertDTOToEntity(CustomerFeedback_CustomerFeedbackDTO CustomerFeedback_CustomerFeedbackDTO)
        {
            CustomerFeedback CustomerFeedback = new CustomerFeedback();
            CustomerFeedback.Id = CustomerFeedback_CustomerFeedbackDTO.Id;
            CustomerFeedback.IsSystemCustomer = CustomerFeedback_CustomerFeedbackDTO.IsSystemCustomer;
            CustomerFeedback.CustomerId = CustomerFeedback_CustomerFeedbackDTO.CustomerId;
            CustomerFeedback.FullName = CustomerFeedback_CustomerFeedbackDTO.FullName;
            CustomerFeedback.Email = CustomerFeedback_CustomerFeedbackDTO.Email;
            CustomerFeedback.PhoneNumber = CustomerFeedback_CustomerFeedbackDTO.PhoneNumber;
            CustomerFeedback.CustomerFeedbackTypeId = CustomerFeedback_CustomerFeedbackDTO.CustomerFeedbackTypeId;
            CustomerFeedback.Title = CustomerFeedback_CustomerFeedbackDTO.Title;
            CustomerFeedback.SendDate = CustomerFeedback_CustomerFeedbackDTO.SendDate;
            CustomerFeedback.Content = CustomerFeedback_CustomerFeedbackDTO.Content;
            CustomerFeedback.StatusId = CustomerFeedback_CustomerFeedbackDTO.StatusId;
            CustomerFeedback.Customer = CustomerFeedback_CustomerFeedbackDTO.Customer == null ? null : new Customer
            {
                Id = CustomerFeedback_CustomerFeedbackDTO.Customer.Id,
                Code = CustomerFeedback_CustomerFeedbackDTO.Customer.Code,
                Name = CustomerFeedback_CustomerFeedbackDTO.Customer.Name,
                Phone = CustomerFeedback_CustomerFeedbackDTO.Customer.Phone,
                Address = CustomerFeedback_CustomerFeedbackDTO.Customer.Address,
                NationId = CustomerFeedback_CustomerFeedbackDTO.Customer.NationId,
                ProvinceId = CustomerFeedback_CustomerFeedbackDTO.Customer.ProvinceId,
                DistrictId = CustomerFeedback_CustomerFeedbackDTO.Customer.DistrictId,
                WardId = CustomerFeedback_CustomerFeedbackDTO.Customer.WardId,
                CustomerTypeId = CustomerFeedback_CustomerFeedbackDTO.Customer.CustomerTypeId,
                Birthday = CustomerFeedback_CustomerFeedbackDTO.Customer.Birthday,
                Email = CustomerFeedback_CustomerFeedbackDTO.Customer.Email,
                ProfessionId = CustomerFeedback_CustomerFeedbackDTO.Customer.ProfessionId,
                CustomerResourceId = CustomerFeedback_CustomerFeedbackDTO.Customer.CustomerResourceId,
                SexId = CustomerFeedback_CustomerFeedbackDTO.Customer.SexId,
                StatusId = CustomerFeedback_CustomerFeedbackDTO.Customer.StatusId,
                CompanyId = CustomerFeedback_CustomerFeedbackDTO.Customer.CompanyId,
                ParentCompanyId = CustomerFeedback_CustomerFeedbackDTO.Customer.ParentCompanyId,
                TaxCode = CustomerFeedback_CustomerFeedbackDTO.Customer.TaxCode,
                Fax = CustomerFeedback_CustomerFeedbackDTO.Customer.Fax,
                Website = CustomerFeedback_CustomerFeedbackDTO.Customer.Website,
                NumberOfEmployee = CustomerFeedback_CustomerFeedbackDTO.Customer.NumberOfEmployee,
                BusinessTypeId = CustomerFeedback_CustomerFeedbackDTO.Customer.BusinessTypeId,
                Investment = CustomerFeedback_CustomerFeedbackDTO.Customer.Investment,
                RevenueAnnual = CustomerFeedback_CustomerFeedbackDTO.Customer.RevenueAnnual,
                IsSupplier = CustomerFeedback_CustomerFeedbackDTO.Customer.IsSupplier,
                Descreption = CustomerFeedback_CustomerFeedbackDTO.Customer.Descreption,
                Used = CustomerFeedback_CustomerFeedbackDTO.Customer.Used,
                RowId = CustomerFeedback_CustomerFeedbackDTO.Customer.RowId,
            };
            CustomerFeedback.CustomerFeedbackType = CustomerFeedback_CustomerFeedbackDTO.CustomerFeedbackType == null ? null : new CustomerFeedbackType
            {
                Id = CustomerFeedback_CustomerFeedbackDTO.CustomerFeedbackType.Id,
                Code = CustomerFeedback_CustomerFeedbackDTO.CustomerFeedbackType.Code,
                Name = CustomerFeedback_CustomerFeedbackDTO.CustomerFeedbackType.Name,
            };
            CustomerFeedback.Status = CustomerFeedback_CustomerFeedbackDTO.Status == null ? null : new Status
            {
                Id = CustomerFeedback_CustomerFeedbackDTO.Status.Id,
                Code = CustomerFeedback_CustomerFeedbackDTO.Status.Code,
                Name = CustomerFeedback_CustomerFeedbackDTO.Status.Name,
            };
            CustomerFeedback.BaseLanguage = CurrentContext.Language;
            return CustomerFeedback;
        }

        private CustomerFeedbackFilter ConvertFilterDTOToFilterEntity(CustomerFeedback_CustomerFeedbackFilterDTO CustomerFeedback_CustomerFeedbackFilterDTO)
        {
            CustomerFeedbackFilter CustomerFeedbackFilter = new CustomerFeedbackFilter();
            CustomerFeedbackFilter.Selects = CustomerFeedbackSelect.ALL;
            CustomerFeedbackFilter.Skip = CustomerFeedback_CustomerFeedbackFilterDTO.Skip;
            CustomerFeedbackFilter.Take = CustomerFeedback_CustomerFeedbackFilterDTO.Take;
            CustomerFeedbackFilter.OrderBy = CustomerFeedback_CustomerFeedbackFilterDTO.OrderBy;
            CustomerFeedbackFilter.OrderType = CustomerFeedback_CustomerFeedbackFilterDTO.OrderType;

            CustomerFeedbackFilter.Id = CustomerFeedback_CustomerFeedbackFilterDTO.Id;
            CustomerFeedbackFilter.CustomerId = CustomerFeedback_CustomerFeedbackFilterDTO.CustomerId;
            CustomerFeedbackFilter.FullName = CustomerFeedback_CustomerFeedbackFilterDTO.FullName;
            CustomerFeedbackFilter.Email = CustomerFeedback_CustomerFeedbackFilterDTO.Email;
            CustomerFeedbackFilter.PhoneNumber = CustomerFeedback_CustomerFeedbackFilterDTO.PhoneNumber;
            CustomerFeedbackFilter.CustomerFeedbackTypeId = CustomerFeedback_CustomerFeedbackFilterDTO.CustomerFeedbackTypeId;
            CustomerFeedbackFilter.Title = CustomerFeedback_CustomerFeedbackFilterDTO.Title;
            CustomerFeedbackFilter.SendDate = CustomerFeedback_CustomerFeedbackFilterDTO.SendDate;
            CustomerFeedbackFilter.Content = CustomerFeedback_CustomerFeedbackFilterDTO.Content;
            CustomerFeedbackFilter.StatusId = CustomerFeedback_CustomerFeedbackFilterDTO.StatusId;
            CustomerFeedbackFilter.CreatedAt = CustomerFeedback_CustomerFeedbackFilterDTO.CreatedAt;
            CustomerFeedbackFilter.UpdatedAt = CustomerFeedback_CustomerFeedbackFilterDTO.UpdatedAt;
            return CustomerFeedbackFilter;
        }
    }
}

