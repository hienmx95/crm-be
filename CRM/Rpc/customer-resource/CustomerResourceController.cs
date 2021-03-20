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
using CRM.Services.MCustomerResource;
using CRM.Services.MStatus;
using CRM.Models;

namespace CRM.Rpc.customer_resource
{
    public partial class CustomerResourceController : RpcController
    {
        private IStatusService StatusService;
        private ICustomerResourceService CustomerResourceService;
        private ICurrentContext CurrentContext;
        public CustomerResourceController(
            IStatusService StatusService,
            ICustomerResourceService CustomerResourceService,
            ICurrentContext CurrentContext
        ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.StatusService = StatusService;
            this.CustomerResourceService = CustomerResourceService;
            this.CurrentContext = CurrentContext;
        }

        [Route(CustomerResourceRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] CustomerResource_CustomerResourceFilterDTO CustomerResource_CustomerResourceFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerResourceFilter CustomerResourceFilter = ConvertFilterDTOToFilterEntity(CustomerResource_CustomerResourceFilterDTO);
            CustomerResourceFilter = await CustomerResourceService.ToFilter(CustomerResourceFilter);
            int count = await CustomerResourceService.Count(CustomerResourceFilter);
            return count;
        }

        [Route(CustomerResourceRoute.List), HttpPost]
        public async Task<ActionResult<List<CustomerResource_CustomerResourceDTO>>> List([FromBody] CustomerResource_CustomerResourceFilterDTO CustomerResource_CustomerResourceFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerResourceFilter CustomerResourceFilter = ConvertFilterDTOToFilterEntity(CustomerResource_CustomerResourceFilterDTO);
            CustomerResourceFilter = await CustomerResourceService.ToFilter(CustomerResourceFilter);
            List<CustomerResource> CustomerResources = await CustomerResourceService.List(CustomerResourceFilter);
            List<CustomerResource_CustomerResourceDTO> CustomerResource_CustomerResourceDTOs = CustomerResources
                .Select(c => new CustomerResource_CustomerResourceDTO(c)).ToList();
            return CustomerResource_CustomerResourceDTOs;
        }

        [Route(CustomerResourceRoute.Get), HttpPost]
        public async Task<ActionResult<CustomerResource_CustomerResourceDTO>> Get([FromBody]CustomerResource_CustomerResourceDTO CustomerResource_CustomerResourceDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CustomerResource_CustomerResourceDTO.Id))
                return Forbid();

            CustomerResource CustomerResource = await CustomerResourceService.Get(CustomerResource_CustomerResourceDTO.Id);
            return new CustomerResource_CustomerResourceDTO(CustomerResource);
        }

        [Route(CustomerResourceRoute.Create), HttpPost]
        public async Task<ActionResult<CustomerResource_CustomerResourceDTO>> Create([FromBody] CustomerResource_CustomerResourceDTO CustomerResource_CustomerResourceDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(CustomerResource_CustomerResourceDTO.Id))
                return Forbid();

            CustomerResource CustomerResource = ConvertDTOToEntity(CustomerResource_CustomerResourceDTO);
            CustomerResource = await CustomerResourceService.Create(CustomerResource);
            CustomerResource_CustomerResourceDTO = new CustomerResource_CustomerResourceDTO(CustomerResource);
            if (CustomerResource.IsValidated)
                return CustomerResource_CustomerResourceDTO;
            else
                return BadRequest(CustomerResource_CustomerResourceDTO);
        }

        [Route(CustomerResourceRoute.Update), HttpPost]
        public async Task<ActionResult<CustomerResource_CustomerResourceDTO>> Update([FromBody] CustomerResource_CustomerResourceDTO CustomerResource_CustomerResourceDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(CustomerResource_CustomerResourceDTO.Id))
                return Forbid();

            CustomerResource CustomerResource = ConvertDTOToEntity(CustomerResource_CustomerResourceDTO);
            CustomerResource = await CustomerResourceService.Update(CustomerResource);
            CustomerResource_CustomerResourceDTO = new CustomerResource_CustomerResourceDTO(CustomerResource);
            if (CustomerResource.IsValidated)
                return CustomerResource_CustomerResourceDTO;
            else
                return BadRequest(CustomerResource_CustomerResourceDTO);
        }

        [Route(CustomerResourceRoute.Delete), HttpPost]
        public async Task<ActionResult<CustomerResource_CustomerResourceDTO>> Delete([FromBody] CustomerResource_CustomerResourceDTO CustomerResource_CustomerResourceDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CustomerResource_CustomerResourceDTO.Id))
                return Forbid();

            CustomerResource CustomerResource = ConvertDTOToEntity(CustomerResource_CustomerResourceDTO);
            CustomerResource = await CustomerResourceService.Delete(CustomerResource);
            CustomerResource_CustomerResourceDTO = new CustomerResource_CustomerResourceDTO(CustomerResource);
            if (CustomerResource.IsValidated)
                return CustomerResource_CustomerResourceDTO;
            else
                return BadRequest(CustomerResource_CustomerResourceDTO);
        }
        
        [Route(CustomerResourceRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerResourceFilter CustomerResourceFilter = new CustomerResourceFilter();
            CustomerResourceFilter = await CustomerResourceService.ToFilter(CustomerResourceFilter);
            CustomerResourceFilter.Id = new IdFilter { In = Ids };
            CustomerResourceFilter.Selects = CustomerResourceSelect.Id;
            CustomerResourceFilter.Skip = 0;
            CustomerResourceFilter.Take = int.MaxValue;

            List<CustomerResource> CustomerResources = await CustomerResourceService.List(CustomerResourceFilter);
            CustomerResources = await CustomerResourceService.BulkDelete(CustomerResources);
            if (CustomerResources.Any(x => !x.IsValidated))
                return BadRequest(CustomerResources.Where(x => !x.IsValidated));
            return true;
        }
        
        [Route(CustomerResourceRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            StatusFilter StatusFilter = new StatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = StatusSelect.ALL
            };
            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<CustomerResource> CustomerResources = new List<CustomerResource>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(CustomerResources);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int CodeColumn = 1 + StartColumn;
                int NameColumn = 2 + StartColumn;
                int StatusIdColumn = 3 + StartColumn;
                int DescriptionColumn = 4 + StartColumn;
                int UsedColumn = 8 + StartColumn;
                int RowIdColumn = 9 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string CodeValue = worksheet.Cells[i + StartRow, CodeColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    string DescriptionValue = worksheet.Cells[i + StartRow, DescriptionColumn].Value?.ToString();
                    string UsedValue = worksheet.Cells[i + StartRow, UsedColumn].Value?.ToString();
                    string RowIdValue = worksheet.Cells[i + StartRow, RowIdColumn].Value?.ToString();
                    
                    CustomerResource CustomerResource = new CustomerResource();
                    CustomerResource.Code = CodeValue;
                    CustomerResource.Name = NameValue;
                    CustomerResource.Description = DescriptionValue;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    CustomerResource.StatusId = Status == null ? 0 : Status.Id;
                    CustomerResource.Status = Status;
                    
                    CustomerResources.Add(CustomerResource);
                }
            }
            CustomerResources = await CustomerResourceService.Import(CustomerResources);
            if (CustomerResources.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < CustomerResources.Count; i++)
                {
                    CustomerResource CustomerResource = CustomerResources[i];
                    if (!CustomerResource.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (CustomerResource.Errors.ContainsKey(nameof(CustomerResource.Id)))
                            Error += CustomerResource.Errors[nameof(CustomerResource.Id)];
                        if (CustomerResource.Errors.ContainsKey(nameof(CustomerResource.Code)))
                            Error += CustomerResource.Errors[nameof(CustomerResource.Code)];
                        if (CustomerResource.Errors.ContainsKey(nameof(CustomerResource.Name)))
                            Error += CustomerResource.Errors[nameof(CustomerResource.Name)];
                        if (CustomerResource.Errors.ContainsKey(nameof(CustomerResource.StatusId)))
                            Error += CustomerResource.Errors[nameof(CustomerResource.StatusId)];
                        if (CustomerResource.Errors.ContainsKey(nameof(CustomerResource.Description)))
                            Error += CustomerResource.Errors[nameof(CustomerResource.Description)];
                        if (CustomerResource.Errors.ContainsKey(nameof(CustomerResource.Used)))
                            Error += CustomerResource.Errors[nameof(CustomerResource.Used)];
                        if (CustomerResource.Errors.ContainsKey(nameof(CustomerResource.RowId)))
                            Error += CustomerResource.Errors[nameof(CustomerResource.RowId)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(CustomerResourceRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] CustomerResource_CustomerResourceFilterDTO CustomerResource_CustomerResourceFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region CustomerResource
                var CustomerResourceFilter = ConvertFilterDTOToFilterEntity(CustomerResource_CustomerResourceFilterDTO);
                CustomerResourceFilter.Skip = 0;
                CustomerResourceFilter.Take = int.MaxValue;
                CustomerResourceFilter = await CustomerResourceService.ToFilter(CustomerResourceFilter);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "CustomerResource.xlsx");
        }

        [Route(CustomerResourceRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] CustomerResource_CustomerResourceFilterDTO CustomerResource_CustomerResourceFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            string path = "Templates/CustomerResource_Template.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "CustomerResource.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            CustomerResourceFilter CustomerResourceFilter = new CustomerResourceFilter();
            CustomerResourceFilter = await CustomerResourceService.ToFilter(CustomerResourceFilter);
            if (Id == 0)
            {

            }
            else
            {
                CustomerResourceFilter.Id = new IdFilter { Equal = Id };
                int count = await CustomerResourceService.Count(CustomerResourceFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private CustomerResource ConvertDTOToEntity(CustomerResource_CustomerResourceDTO CustomerResource_CustomerResourceDTO)
        {
            CustomerResource CustomerResource = new CustomerResource();
            CustomerResource.Id = CustomerResource_CustomerResourceDTO.Id;
            CustomerResource.Code = CustomerResource_CustomerResourceDTO.Code;
            CustomerResource.Name = CustomerResource_CustomerResourceDTO.Name;
            CustomerResource.StatusId = CustomerResource_CustomerResourceDTO.StatusId;
            CustomerResource.Description = CustomerResource_CustomerResourceDTO.Description;
            CustomerResource.Used = CustomerResource_CustomerResourceDTO.Used;
            CustomerResource.RowId = CustomerResource_CustomerResourceDTO.RowId;
            CustomerResource.Status = CustomerResource_CustomerResourceDTO.Status == null ? null : new Status
            {
                Id = CustomerResource_CustomerResourceDTO.Status.Id,
                Code = CustomerResource_CustomerResourceDTO.Status.Code,
                Name = CustomerResource_CustomerResourceDTO.Status.Name,
            };
            CustomerResource.BaseLanguage = CurrentContext.Language;
            return CustomerResource;
        }

        private CustomerResourceFilter ConvertFilterDTOToFilterEntity(CustomerResource_CustomerResourceFilterDTO CustomerResource_CustomerResourceFilterDTO)
        {
            CustomerResourceFilter CustomerResourceFilter = new CustomerResourceFilter();
            CustomerResourceFilter.Selects = CustomerResourceSelect.ALL;
            CustomerResourceFilter.Skip = CustomerResource_CustomerResourceFilterDTO.Skip;
            CustomerResourceFilter.Take = CustomerResource_CustomerResourceFilterDTO.Take;
            CustomerResourceFilter.OrderBy = CustomerResource_CustomerResourceFilterDTO.OrderBy;
            CustomerResourceFilter.OrderType = CustomerResource_CustomerResourceFilterDTO.OrderType;

            CustomerResourceFilter.Id = CustomerResource_CustomerResourceFilterDTO.Id;
            CustomerResourceFilter.Code = CustomerResource_CustomerResourceFilterDTO.Code;
            CustomerResourceFilter.Name = CustomerResource_CustomerResourceFilterDTO.Name;
            CustomerResourceFilter.StatusId = CustomerResource_CustomerResourceFilterDTO.StatusId;
            CustomerResourceFilter.Description = CustomerResource_CustomerResourceFilterDTO.Description;
            CustomerResourceFilter.RowId = CustomerResource_CustomerResourceFilterDTO.RowId;
            CustomerResourceFilter.CreatedAt = CustomerResource_CustomerResourceFilterDTO.CreatedAt;
            CustomerResourceFilter.UpdatedAt = CustomerResource_CustomerResourceFilterDTO.UpdatedAt;
            return CustomerResourceFilter;
        }
    }
}

