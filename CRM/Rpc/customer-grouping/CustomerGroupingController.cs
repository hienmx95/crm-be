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
using CRM.Services.MCustomerGrouping;
using CRM.Services.MCustomerType;
using CRM.Services.MStatus;
using CRM.Models;

namespace CRM.Rpc.customer_grouping
{
    public partial class CustomerGroupingController : RpcController
    {
        private ICustomerTypeService CustomerTypeService;
        private IStatusService StatusService;
        private ICustomerGroupingService CustomerGroupingService;
        private ICurrentContext CurrentContext;
        public CustomerGroupingController(
            ICustomerTypeService CustomerTypeService,
            IStatusService StatusService,
            ICustomerGroupingService CustomerGroupingService,
            ICurrentContext CurrentContext
      ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.CustomerTypeService = CustomerTypeService;
            this.StatusService = StatusService;
            this.CustomerGroupingService = CustomerGroupingService;
            this.CurrentContext = CurrentContext;
        }

        [Route(CustomerGroupingRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] CustomerGrouping_CustomerGroupingFilterDTO CustomerGrouping_CustomerGroupingFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerGroupingFilter CustomerGroupingFilter = ConvertFilterDTOToFilterEntity(CustomerGrouping_CustomerGroupingFilterDTO);
            CustomerGroupingFilter = await CustomerGroupingService.ToFilter(CustomerGroupingFilter);
            int count = await CustomerGroupingService.Count(CustomerGroupingFilter);
            return count;
        }

        [Route(CustomerGroupingRoute.List), HttpPost]
        public async Task<ActionResult<List<CustomerGrouping_CustomerGroupingDTO>>> List([FromBody] CustomerGrouping_CustomerGroupingFilterDTO CustomerGrouping_CustomerGroupingFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerGroupingFilter CustomerGroupingFilter = ConvertFilterDTOToFilterEntity(CustomerGrouping_CustomerGroupingFilterDTO);
            CustomerGroupingFilter = await CustomerGroupingService.ToFilter(CustomerGroupingFilter);
            List<CustomerGrouping> CustomerGroupings = await CustomerGroupingService.List(CustomerGroupingFilter);
            List<CustomerGrouping_CustomerGroupingDTO> CustomerGrouping_CustomerGroupingDTOs = CustomerGroupings
                .Select(c => new CustomerGrouping_CustomerGroupingDTO(c)).ToList();
            return CustomerGrouping_CustomerGroupingDTOs;
        }

        [Route(CustomerGroupingRoute.Get), HttpPost]
        public async Task<ActionResult<CustomerGrouping_CustomerGroupingDTO>> Get([FromBody]CustomerGrouping_CustomerGroupingDTO CustomerGrouping_CustomerGroupingDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CustomerGrouping_CustomerGroupingDTO.Id))
                return Forbid();

            CustomerGrouping CustomerGrouping = await CustomerGroupingService.Get(CustomerGrouping_CustomerGroupingDTO.Id);
            return new CustomerGrouping_CustomerGroupingDTO(CustomerGrouping);
        }

        [Route(CustomerGroupingRoute.Create), HttpPost]
        public async Task<ActionResult<CustomerGrouping_CustomerGroupingDTO>> Create([FromBody] CustomerGrouping_CustomerGroupingDTO CustomerGrouping_CustomerGroupingDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(CustomerGrouping_CustomerGroupingDTO.Id))
                return Forbid();

            CustomerGrouping CustomerGrouping = ConvertDTOToEntity(CustomerGrouping_CustomerGroupingDTO);
            CustomerGrouping = await CustomerGroupingService.Create(CustomerGrouping);
            CustomerGrouping_CustomerGroupingDTO = new CustomerGrouping_CustomerGroupingDTO(CustomerGrouping);
            if (CustomerGrouping.IsValidated)
                return CustomerGrouping_CustomerGroupingDTO;
            else
                return BadRequest(CustomerGrouping_CustomerGroupingDTO);
        }

        [Route(CustomerGroupingRoute.Update), HttpPost]
        public async Task<ActionResult<CustomerGrouping_CustomerGroupingDTO>> Update([FromBody] CustomerGrouping_CustomerGroupingDTO CustomerGrouping_CustomerGroupingDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(CustomerGrouping_CustomerGroupingDTO.Id))
                return Forbid();

            CustomerGrouping CustomerGrouping = ConvertDTOToEntity(CustomerGrouping_CustomerGroupingDTO);
            CustomerGrouping = await CustomerGroupingService.Update(CustomerGrouping);
            CustomerGrouping_CustomerGroupingDTO = new CustomerGrouping_CustomerGroupingDTO(CustomerGrouping);
            if (CustomerGrouping.IsValidated)
                return CustomerGrouping_CustomerGroupingDTO;
            else
                return BadRequest(CustomerGrouping_CustomerGroupingDTO);
        }

        [Route(CustomerGroupingRoute.Delete), HttpPost]
        public async Task<ActionResult<CustomerGrouping_CustomerGroupingDTO>> Delete([FromBody] CustomerGrouping_CustomerGroupingDTO CustomerGrouping_CustomerGroupingDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(CustomerGrouping_CustomerGroupingDTO.Id))
                return Forbid();

            CustomerGrouping CustomerGrouping = ConvertDTOToEntity(CustomerGrouping_CustomerGroupingDTO);
            CustomerGrouping = await CustomerGroupingService.Delete(CustomerGrouping);
            CustomerGrouping_CustomerGroupingDTO = new CustomerGrouping_CustomerGroupingDTO(CustomerGrouping);
            if (CustomerGrouping.IsValidated)
                return CustomerGrouping_CustomerGroupingDTO;
            else
                return BadRequest(CustomerGrouping_CustomerGroupingDTO);
        }
        
        [Route(CustomerGroupingRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            CustomerGroupingFilter CustomerGroupingFilter = new CustomerGroupingFilter();
            CustomerGroupingFilter = await CustomerGroupingService.ToFilter(CustomerGroupingFilter);
            CustomerGroupingFilter.Id = new IdFilter { In = Ids };
            CustomerGroupingFilter.Selects = CustomerGroupingSelect.Id;
            CustomerGroupingFilter.Skip = 0;
            CustomerGroupingFilter.Take = int.MaxValue;

            List<CustomerGrouping> CustomerGroupings = await CustomerGroupingService.List(CustomerGroupingFilter);
            CustomerGroupings = await CustomerGroupingService.BulkDelete(CustomerGroupings);
            if (CustomerGroupings.Any(x => !x.IsValidated))
                return BadRequest(CustomerGroupings.Where(x => !x.IsValidated));
            return true;
        }
        
        [Route(CustomerGroupingRoute.Import), HttpPost]
        public async Task<ActionResult> Import(IFormFile file)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            CustomerTypeFilter CustomerTypeFilter = new CustomerTypeFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = CustomerTypeSelect.ALL
            };
            List<CustomerType> CustomerTypes = await CustomerTypeService.List(CustomerTypeFilter);
            StatusFilter StatusFilter = new StatusFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = StatusSelect.ALL
            };
            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<CustomerGrouping> CustomerGroupings = new List<CustomerGrouping>();
            using (ExcelPackage excelPackage = new ExcelPackage(file.OpenReadStream()))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                    return Ok(CustomerGroupings);
                int StartColumn = 1;
                int StartRow = 1;
                int IdColumn = 0 + StartColumn;
                int CodeColumn = 1 + StartColumn;
                int NameColumn = 2 + StartColumn;
                int CustomerTypeIdColumn = 3 + StartColumn;
                int ParentIdColumn = 4 + StartColumn;
                int PathColumn = 5 + StartColumn;
                int LevelColumn = 6 + StartColumn;
                int StatusIdColumn = 7 + StartColumn;
                int DescriptionColumn = 8 + StartColumn;

                for (int i = StartRow; i <= worksheet.Dimension.End.Row; i++)
                {
                    if (string.IsNullOrEmpty(worksheet.Cells[i + StartRow, StartColumn].Value?.ToString()))
                        break;
                    string IdValue = worksheet.Cells[i + StartRow, IdColumn].Value?.ToString();
                    string CodeValue = worksheet.Cells[i + StartRow, CodeColumn].Value?.ToString();
                    string NameValue = worksheet.Cells[i + StartRow, NameColumn].Value?.ToString();
                    string CustomerTypeIdValue = worksheet.Cells[i + StartRow, CustomerTypeIdColumn].Value?.ToString();
                    string ParentIdValue = worksheet.Cells[i + StartRow, ParentIdColumn].Value?.ToString();
                    string PathValue = worksheet.Cells[i + StartRow, PathColumn].Value?.ToString();
                    string LevelValue = worksheet.Cells[i + StartRow, LevelColumn].Value?.ToString();
                    string StatusIdValue = worksheet.Cells[i + StartRow, StatusIdColumn].Value?.ToString();
                    string DescriptionValue = worksheet.Cells[i + StartRow, DescriptionColumn].Value?.ToString();
                    
                    CustomerGrouping CustomerGrouping = new CustomerGrouping();
                    CustomerGrouping.Code = CodeValue;
                    CustomerGrouping.Name = NameValue;
                    CustomerGrouping.Path = PathValue;
                    CustomerGrouping.Level = long.TryParse(LevelValue, out long Level) ? Level : 0;
                    CustomerGrouping.Description = DescriptionValue;
                    CustomerType CustomerType = CustomerTypes.Where(x => x.Id.ToString() == CustomerTypeIdValue).FirstOrDefault();
                    CustomerGrouping.CustomerTypeId = CustomerType == null ? 0 : CustomerType.Id;
                    CustomerGrouping.CustomerType = CustomerType;
                    Status Status = Statuses.Where(x => x.Id.ToString() == StatusIdValue).FirstOrDefault();
                    CustomerGrouping.StatusId = Status == null ? 0 : Status.Id;
                    CustomerGrouping.Status = Status;
                    
                    CustomerGroupings.Add(CustomerGrouping);
                }
            }
            CustomerGroupings = await CustomerGroupingService.Import(CustomerGroupings);
            if (CustomerGroupings.All(x => x.IsValidated))
                return Ok(true);
            else
            {
                List<string> Errors = new List<string>();
                for (int i = 0; i < CustomerGroupings.Count; i++)
                {
                    CustomerGrouping CustomerGrouping = CustomerGroupings[i];
                    if (!CustomerGrouping.IsValidated)
                    {
                        string Error = $"Dòng {i + 2} có lỗi:";
                        if (CustomerGrouping.Errors.ContainsKey(nameof(CustomerGrouping.Id)))
                            Error += CustomerGrouping.Errors[nameof(CustomerGrouping.Id)];
                        if (CustomerGrouping.Errors.ContainsKey(nameof(CustomerGrouping.Code)))
                            Error += CustomerGrouping.Errors[nameof(CustomerGrouping.Code)];
                        if (CustomerGrouping.Errors.ContainsKey(nameof(CustomerGrouping.Name)))
                            Error += CustomerGrouping.Errors[nameof(CustomerGrouping.Name)];
                        if (CustomerGrouping.Errors.ContainsKey(nameof(CustomerGrouping.CustomerTypeId)))
                            Error += CustomerGrouping.Errors[nameof(CustomerGrouping.CustomerTypeId)];
                        if (CustomerGrouping.Errors.ContainsKey(nameof(CustomerGrouping.ParentId)))
                            Error += CustomerGrouping.Errors[nameof(CustomerGrouping.ParentId)];
                        if (CustomerGrouping.Errors.ContainsKey(nameof(CustomerGrouping.Path)))
                            Error += CustomerGrouping.Errors[nameof(CustomerGrouping.Path)];
                        if (CustomerGrouping.Errors.ContainsKey(nameof(CustomerGrouping.Level)))
                            Error += CustomerGrouping.Errors[nameof(CustomerGrouping.Level)];
                        if (CustomerGrouping.Errors.ContainsKey(nameof(CustomerGrouping.StatusId)))
                            Error += CustomerGrouping.Errors[nameof(CustomerGrouping.StatusId)];
                        if (CustomerGrouping.Errors.ContainsKey(nameof(CustomerGrouping.Description)))
                            Error += CustomerGrouping.Errors[nameof(CustomerGrouping.Description)];
                        Errors.Add(Error);
                    }
                }
                return BadRequest(Errors);
            }
        }
        
        [Route(CustomerGroupingRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] CustomerGrouping_CustomerGroupingFilterDTO CustomerGrouping_CustomerGroupingFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                #region CustomerGrouping
                var CustomerGroupingFilter = ConvertFilterDTOToFilterEntity(CustomerGrouping_CustomerGroupingFilterDTO);
                CustomerGroupingFilter.Skip = 0;
                CustomerGroupingFilter.Take = int.MaxValue;
                CustomerGroupingFilter = await CustomerGroupingService.ToFilter(CustomerGroupingFilter);
                List<CustomerGrouping> CustomerGroupings = await CustomerGroupingService.List(CustomerGroupingFilter);

                var CustomerGroupingHeaders = new List<string[]>()
                {
                    new string[] { 
                        "Id",
                        "Code",
                        "Name",
                        "CustomerTypeId",
                        "ParentId",
                        "Path",
                        "Level",
                        "StatusId",
                        "Description",
                    }
                };
                List<object[]> CustomerGroupingData = new List<object[]>();
                for (int i = 0; i < CustomerGroupings.Count; i++)
                {
                    var CustomerGrouping = CustomerGroupings[i];
                    CustomerGroupingData.Add(new Object[]
                    {
                        CustomerGrouping.Id,
                        CustomerGrouping.Code,
                        CustomerGrouping.Name,
                        CustomerGrouping.CustomerTypeId,
                        CustomerGrouping.ParentId,
                        CustomerGrouping.Path,
                        CustomerGrouping.Level,
                        CustomerGrouping.StatusId,
                        CustomerGrouping.Description,
                    });
                }
                excel.GenerateWorksheet("CustomerGrouping", CustomerGroupingHeaders, CustomerGroupingData);
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
            return File(memoryStream.ToArray(), "application/octet-stream", "CustomerGrouping.xlsx");
        }

        [Route(CustomerGroupingRoute.ExportTemplate), HttpPost]
        public async Task<ActionResult> ExportTemplate([FromBody] CustomerGrouping_CustomerGroupingFilterDTO CustomerGrouping_CustomerGroupingFilterDTO)
        {
            if (UnAuthorization) return Forbid();
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            string path = "Templates/CustomerGrouping_Template.xlsx";
            byte[] arr = System.IO.File.ReadAllBytes(path);
            MemoryStream input = new MemoryStream(arr);
            MemoryStream output = new MemoryStream();
            dynamic Data = new ExpandoObject();
            using (var document = StaticParams.DocumentFactory.Open(input, output, "xlsx"))
            {
                document.Process(Data);
            };
            return File(output.ToArray(), "application/octet-stream", "CustomerGrouping.xlsx");
        }

        private async Task<bool> HasPermission(long Id)
        {
            CustomerGroupingFilter CustomerGroupingFilter = new CustomerGroupingFilter();
            CustomerGroupingFilter = await CustomerGroupingService.ToFilter(CustomerGroupingFilter);
            if (Id == 0)
            {

            }
            else
            {
                CustomerGroupingFilter.Id = new IdFilter { Equal = Id };
                int count = await CustomerGroupingService.Count(CustomerGroupingFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private CustomerGrouping ConvertDTOToEntity(CustomerGrouping_CustomerGroupingDTO CustomerGrouping_CustomerGroupingDTO)
        {
            CustomerGrouping CustomerGrouping = new CustomerGrouping();
            CustomerGrouping.Id = CustomerGrouping_CustomerGroupingDTO.Id;
            CustomerGrouping.Code = CustomerGrouping_CustomerGroupingDTO.Code;
            CustomerGrouping.Name = CustomerGrouping_CustomerGroupingDTO.Name;
            CustomerGrouping.CustomerTypeId = CustomerGrouping_CustomerGroupingDTO.CustomerTypeId;
            CustomerGrouping.ParentId = CustomerGrouping_CustomerGroupingDTO.ParentId;
            CustomerGrouping.Path = CustomerGrouping_CustomerGroupingDTO.Path;
            CustomerGrouping.Level = CustomerGrouping_CustomerGroupingDTO.Level;
            CustomerGrouping.StatusId = CustomerGrouping_CustomerGroupingDTO.StatusId;
            CustomerGrouping.Description = CustomerGrouping_CustomerGroupingDTO.Description;
            CustomerGrouping.CustomerType = CustomerGrouping_CustomerGroupingDTO.CustomerType == null ? null : new CustomerType
            {
                Id = CustomerGrouping_CustomerGroupingDTO.CustomerType.Id,
                Code = CustomerGrouping_CustomerGroupingDTO.CustomerType.Code,
                Name = CustomerGrouping_CustomerGroupingDTO.CustomerType.Name,
            };
            CustomerGrouping.Parent = CustomerGrouping_CustomerGroupingDTO.Parent == null ? null : new CustomerGrouping
            {
                Id = CustomerGrouping_CustomerGroupingDTO.Parent.Id,
                Code = CustomerGrouping_CustomerGroupingDTO.Parent.Code,
                Name = CustomerGrouping_CustomerGroupingDTO.Parent.Name,
                CustomerTypeId = CustomerGrouping_CustomerGroupingDTO.Parent.CustomerTypeId,
                ParentId = CustomerGrouping_CustomerGroupingDTO.Parent.ParentId,
                Path = CustomerGrouping_CustomerGroupingDTO.Parent.Path,
                Level = CustomerGrouping_CustomerGroupingDTO.Parent.Level,
                StatusId = CustomerGrouping_CustomerGroupingDTO.Parent.StatusId,
                Description = CustomerGrouping_CustomerGroupingDTO.Parent.Description,
            };
            CustomerGrouping.Status = CustomerGrouping_CustomerGroupingDTO.Status == null ? null : new Status
            {
                Id = CustomerGrouping_CustomerGroupingDTO.Status.Id,
                Code = CustomerGrouping_CustomerGroupingDTO.Status.Code,
                Name = CustomerGrouping_CustomerGroupingDTO.Status.Name,
            };
            CustomerGrouping.BaseLanguage = CurrentContext.Language;
            return CustomerGrouping;
        }

        private CustomerGroupingFilter ConvertFilterDTOToFilterEntity(CustomerGrouping_CustomerGroupingFilterDTO CustomerGrouping_CustomerGroupingFilterDTO)
        {
            CustomerGroupingFilter CustomerGroupingFilter = new CustomerGroupingFilter();
            CustomerGroupingFilter.Selects = CustomerGroupingSelect.ALL;
            CustomerGroupingFilter.Skip = 0;
            CustomerGroupingFilter.Take = 99999;
            CustomerGroupingFilter.OrderBy = CustomerGrouping_CustomerGroupingFilterDTO.OrderBy;
            CustomerGroupingFilter.OrderType = CustomerGrouping_CustomerGroupingFilterDTO.OrderType;

            CustomerGroupingFilter.Id = CustomerGrouping_CustomerGroupingFilterDTO.Id;
            CustomerGroupingFilter.Code = CustomerGrouping_CustomerGroupingFilterDTO.Code;
            CustomerGroupingFilter.Name = CustomerGrouping_CustomerGroupingFilterDTO.Name;
            CustomerGroupingFilter.CustomerTypeId = CustomerGrouping_CustomerGroupingFilterDTO.CustomerTypeId;
            CustomerGroupingFilter.ParentId = CustomerGrouping_CustomerGroupingFilterDTO.ParentId;
            CustomerGroupingFilter.Path = CustomerGrouping_CustomerGroupingFilterDTO.Path;
            CustomerGroupingFilter.Level = CustomerGrouping_CustomerGroupingFilterDTO.Level;
            CustomerGroupingFilter.StatusId = CustomerGrouping_CustomerGroupingFilterDTO.StatusId;
            CustomerGroupingFilter.Description = CustomerGrouping_CustomerGroupingFilterDTO.Description;
            CustomerGroupingFilter.CreatedAt = CustomerGrouping_CustomerGroupingFilterDTO.CreatedAt;
            CustomerGroupingFilter.UpdatedAt = CustomerGrouping_CustomerGroupingFilterDTO.UpdatedAt;
            return CustomerGroupingFilter;
        }
    }
}

