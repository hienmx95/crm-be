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
using CRM.Services.MTicket;
using CRM.Services.MCustomer;
using CRM.Services.MOrganization;
using CRM.Services.MProduct;
using CRM.Services.MCallLog;
using CRM.Services.MStatus;
using CRM.Services.MTicketIssueLevel;
using CRM.Services.MTicketPriority;
using CRM.Services.MTicketSource;
using CRM.Services.MTicketStatus;
using CRM.Services.MTicketGroup;
using CRM.Services.MTicketType;
using CRM.Services.MTicketResolveType;
using CRM.Services.MAppUser;
using CRM.Services.MCustomerType;
using CRM.Models;

namespace CRM.Rpc.ticket
{
    public partial class TicketController : RpcController
    {
        private ICustomerService CustomerService;
        private IOrganizationService OrganizationService;
        private IProductService ProductService;
        private ICallLogService CallLogService;
        private IStatusService StatusService;
        private ITicketIssueLevelService TicketIssueLevelService;
        private ITicketPriorityService TicketPriorityService;
        private ITicketSourceService TicketSourceService;
        private ITicketStatusService TicketStatusService;
        private ITicketGroupService TicketGroupService;
        private ITicketTypeService TicketTypeService;
        private ITicketResolveTypeService TicketResolveTypeService;
        private IAppUserService AppUserService;
        private ITicketService TicketService;
        private ICustomerTypeService CustomerTypeService;
        private ICurrentContext CurrentContext; 
        public TicketController(
            ICustomerService CustomerService,
            IOrganizationService OrganizationService,
            IProductService ProductService,
            ICallLogService CallLogService,
            IStatusService StatusService,
            ITicketIssueLevelService TicketIssueLevelService,
            ITicketPriorityService TicketPriorityService,
            ITicketSourceService TicketSourceService,
            ITicketStatusService TicketStatusService,
            ITicketGroupService TicketGroupService,
            ITicketTypeService TicketTypeService,
            ITicketResolveTypeService TicketResolveTypeService,
            IAppUserService AppUserService,
            ITicketService TicketService,
            ICustomerTypeService CustomerTypeService,
            ICurrentContext CurrentContext
        ,IHttpContextAccessor httpContextAccessor, DataContext _DataContext
        ):base(httpContextAccessor,_DataContext)
        {
            this.CustomerService = CustomerService;
            this.OrganizationService = OrganizationService;
            this.ProductService = ProductService;
            this.CallLogService = CallLogService;
            this.StatusService = StatusService;
            this.TicketIssueLevelService = TicketIssueLevelService;
            this.TicketPriorityService = TicketPriorityService;
            this.TicketSourceService = TicketSourceService;
            this.TicketStatusService = TicketStatusService;
            this.TicketGroupService = TicketGroupService;
            this.TicketTypeService = TicketTypeService;
            this.TicketResolveTypeService = TicketResolveTypeService;
            this.AppUserService = AppUserService;
            this.TicketService = TicketService;
            this.CustomerTypeService = CustomerTypeService;
            this.CurrentContext = CurrentContext;
        }

        [Route(TicketRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] Ticket_TicketFilterDTO Ticket_TicketFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketFilter TicketFilter = ConvertFilterDTOToFilterEntity(Ticket_TicketFilterDTO);
            //TicketFilter =  TicketService.ToFilter(TicketFilter);
            int count = await TicketService.Count(TicketFilter);
            return count;
        }

        [Route(TicketRoute.List), HttpPost]
        public async Task<ActionResult<List<Ticket_TicketDTO>>> List([FromBody] Ticket_TicketFilterDTO Ticket_TicketFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketFilter TicketFilter = ConvertFilterDTOToFilterEntity(Ticket_TicketFilterDTO);
            TicketFilter = await TicketService.ToFilter(TicketFilter);
            List<Ticket> Tickets = await TicketService.List(TicketFilter);
            List<Ticket_TicketDTO> Ticket_TicketDTOs = Tickets
                .Select(c => new Ticket_TicketDTO(c)).ToList();
            return Ticket_TicketDTOs;
        }

        [Route(TicketRoute.Get), HttpPost]
        public async Task<ActionResult<Ticket_TicketDTO>> Get([FromBody]Ticket_TicketDTO Ticket_TicketDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Ticket_TicketDTO.Id))
                return Forbid();

            Ticket Ticket = await TicketService.Get(Ticket_TicketDTO.Id);
            return new Ticket_TicketDTO(Ticket);
        }

        [Route(TicketRoute.Create), HttpPost]
        public async Task<ActionResult<Ticket_TicketDTO>> Create([FromBody] Ticket_TicketDTO Ticket_TicketDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(Ticket_TicketDTO.Id))
                return Forbid();

            Ticket Ticket = ConvertDTOToEntity(Ticket_TicketDTO);
            Ticket = await TicketService.Create(Ticket);
            Ticket_TicketDTO = new Ticket_TicketDTO(Ticket);
            if (Ticket.IsValidated)
                return Ticket_TicketDTO;
            else
                return BadRequest(Ticket_TicketDTO);
        }

        [Route(TicketRoute.Update), HttpPost]
        public async Task<ActionResult<Ticket_TicketDTO>> Update([FromBody] Ticket_TicketDTO Ticket_TicketDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            if (!await HasPermission(Ticket_TicketDTO.Id))
                return Forbid();

            Ticket Ticket = ConvertDTOToEntity(Ticket_TicketDTO);
            Ticket = await TicketService.Update(Ticket);
            Ticket_TicketDTO = new Ticket_TicketDTO(Ticket);
            if (Ticket.IsValidated)
                return Ticket_TicketDTO;
            else
                return BadRequest(Ticket_TicketDTO);
        }

        [Route(TicketRoute.Delete), HttpPost]
        public async Task<ActionResult<Ticket_TicketDTO>> Delete([FromBody] Ticket_TicketDTO Ticket_TicketDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Ticket_TicketDTO.Id))
                return Forbid();

            Ticket Ticket = ConvertDTOToEntity(Ticket_TicketDTO);
            Ticket = await TicketService.Delete(Ticket);
            Ticket_TicketDTO = new Ticket_TicketDTO(Ticket);
            if (Ticket.IsValidated)
                return Ticket_TicketDTO;
            else
                return BadRequest(Ticket_TicketDTO);
        }
        
        [Route(TicketRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            TicketFilter TicketFilter = new TicketFilter();
            TicketFilter = await TicketService.ToFilter(TicketFilter);
            TicketFilter.Id = new IdFilter { In = Ids };
            TicketFilter.Selects = TicketSelect.Id;
            TicketFilter.Skip = 0;
            TicketFilter.Take = int.MaxValue;

            List<Ticket> Tickets = await TicketService.List(TicketFilter);
            Tickets = await TicketService.BulkDelete(Tickets);
            if (Tickets.Any(x => !x.IsValidated))
                return BadRequest(Tickets.Where(x => !x.IsValidated));
            return true;
        } 
        private async Task<bool> HasPermission(long Id)
        {
            TicketFilter TicketFilter = new TicketFilter();
            TicketFilter =  await TicketService.ToFilter(TicketFilter);
            if (Id == 0)
            {

            }
            else
            {
                TicketFilter.Id = new IdFilter { Equal = Id };
                int count = await TicketService.Count(TicketFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private Ticket ConvertDTOToEntity(Ticket_TicketDTO Ticket_TicketDTO)
        {
            Ticket Ticket = new Ticket();
            Ticket.Id = Ticket_TicketDTO.Id;
            Ticket.Name = Ticket_TicketDTO.Name;
            Ticket.Phone = Ticket_TicketDTO.Phone;
            Ticket.CustomerId = Ticket_TicketDTO.CustomerId;
            Ticket.UserId = Ticket_TicketDTO.UserId;
            Ticket.CustomerTypeId = Ticket_TicketDTO.CustomerTypeId;
            Ticket.CreatorId = Ticket_TicketDTO.CreatorId;
            Ticket.ProductId = Ticket_TicketDTO.ProductId;
            Ticket.ReceiveDate = Ticket_TicketDTO.ReceiveDate;
            Ticket.ProcessDate = Ticket_TicketDTO.ProcessDate;
            Ticket.FinishDate = Ticket_TicketDTO.FinishDate;
            Ticket.Subject = Ticket_TicketDTO.Subject;
            Ticket.Content = Ticket_TicketDTO.Content;
            Ticket.TicketIssueLevelId = Ticket_TicketDTO.TicketIssueLevelId;
            Ticket.TicketPriorityId = Ticket_TicketDTO.TicketPriorityId;
            Ticket.TicketStatusId = Ticket_TicketDTO.TicketStatusId;
            Ticket.TicketSourceId = Ticket_TicketDTO.TicketSourceId;
            Ticket.TicketNumber = Ticket_TicketDTO.TicketNumber;
            Ticket.DepartmentId = Ticket_TicketDTO.DepartmentId;
            Ticket.RelatedTicketId = Ticket_TicketDTO.RelatedTicketId;
            Ticket.SLA = Ticket_TicketDTO.SLA;
            Ticket.RelatedCallLogId = Ticket_TicketDTO.RelatedCallLogId;
            Ticket.ResponseMethodId = Ticket_TicketDTO.ResponseMethodId;
            Ticket.StatusId = Ticket_TicketDTO.StatusId;
            Ticket.Used = Ticket_TicketDTO.Used;
            Ticket.Notes = Ticket_TicketDTO.Notes;
            Ticket.IsAlerted = Ticket_TicketDTO.IsAlerted;
            Ticket.IsAlertedFRT = Ticket_TicketDTO.IsAlertedFRT;
            Ticket.IsEscalated = Ticket_TicketDTO.IsEscalated;
            Ticket.IsEscalatedFRT = Ticket_TicketDTO.IsEscalatedFRT;
            Ticket.TicketResolveTypeId = Ticket_TicketDTO.TicketResolveTypeId;
            Ticket.ResolveContent = Ticket_TicketDTO.ResolveContent;
            Ticket.closedAt = Ticket_TicketDTO.closedAt;
            Ticket.AppUserClosedId = Ticket_TicketDTO.AppUserClosedId;
            Ticket.FirstResponseAt = Ticket_TicketDTO.FirstResponseAt;
            Ticket.LastResponseAt = Ticket_TicketDTO.LastResponseAt;
            Ticket.LastHoldingAt = Ticket_TicketDTO.LastHoldingAt;
            Ticket.ReraisedTimes = Ticket_TicketDTO.ReraisedTimes;
            Ticket.ResolvedAt = Ticket_TicketDTO.ResolvedAt;
            Ticket.AppUserResolvedId = Ticket_TicketDTO.AppUserResolvedId;
            Ticket.IsClose = Ticket_TicketDTO.IsClose;
            Ticket.IsOpen = Ticket_TicketDTO.IsOpen;
            Ticket.IsWait = Ticket_TicketDTO.IsWait;
            Ticket.IsWork = Ticket_TicketDTO.IsWork;
            Ticket.SLAPolicyId = Ticket_TicketDTO.SLAPolicyId;
            Ticket.HoldingTime = Ticket_TicketDTO.HoldingTime;
            Ticket.FirstResponeTime = Ticket_TicketDTO.FirstResponeTime;
            Ticket.ResolveTime = Ticket_TicketDTO.ResolveTime;
            Ticket.FirstRespTimeRemaining = Ticket_TicketDTO.FirstRespTimeRemaining;
            Ticket.ResolveTimeRemaining = Ticket_TicketDTO.ResolveTimeRemaining;
            Ticket.SLAStatusId = Ticket_TicketDTO.SLAStatusId;
            Ticket.SLAOverTime = Ticket_TicketDTO.SLAOverTime;
            Ticket.AppUserClosed = Ticket_TicketDTO.AppUserClosed == null ? null : new AppUser
            {
                Id = Ticket_TicketDTO.AppUserClosed.Id,
                Username = Ticket_TicketDTO.AppUserClosed.Username,
                DisplayName = Ticket_TicketDTO.AppUserClosed.DisplayName,
                Address = Ticket_TicketDTO.AppUserClosed.Address,
                Email = Ticket_TicketDTO.AppUserClosed.Email,
                Phone = Ticket_TicketDTO.AppUserClosed.Phone,
                SexId = Ticket_TicketDTO.AppUserClosed.SexId,
                Birthday = Ticket_TicketDTO.AppUserClosed.Birthday,
                Avatar = Ticket_TicketDTO.AppUserClosed.Avatar,
                PositionId = Ticket_TicketDTO.AppUserClosed.PositionId,
                Department = Ticket_TicketDTO.AppUserClosed.Department,
                OrganizationId = Ticket_TicketDTO.AppUserClosed.OrganizationId,
                ProvinceId = Ticket_TicketDTO.AppUserClosed.ProvinceId,
                Longitude = Ticket_TicketDTO.AppUserClosed.Longitude,
                Latitude = Ticket_TicketDTO.AppUserClosed.Latitude,
                StatusId = Ticket_TicketDTO.AppUserClosed.StatusId,
            };
            Ticket.AppUserResolved = Ticket_TicketDTO.AppUserResolved == null ? null : new AppUser
            {
                Id = Ticket_TicketDTO.AppUserResolved.Id,
                Username = Ticket_TicketDTO.AppUserResolved.Username,
                DisplayName = Ticket_TicketDTO.AppUserResolved.DisplayName,
                Address = Ticket_TicketDTO.AppUserResolved.Address,
                Email = Ticket_TicketDTO.AppUserResolved.Email,
                Phone = Ticket_TicketDTO.AppUserResolved.Phone,
                SexId = Ticket_TicketDTO.AppUserResolved.SexId,
                Birthday = Ticket_TicketDTO.AppUserResolved.Birthday,
                Avatar = Ticket_TicketDTO.AppUserResolved.Avatar,
                PositionId = Ticket_TicketDTO.AppUserResolved.PositionId,
                Department = Ticket_TicketDTO.AppUserResolved.Department,
                OrganizationId = Ticket_TicketDTO.AppUserResolved.OrganizationId,
                ProvinceId = Ticket_TicketDTO.AppUserResolved.ProvinceId,
                Longitude = Ticket_TicketDTO.AppUserResolved.Longitude,
                Latitude = Ticket_TicketDTO.AppUserResolved.Latitude,
                StatusId = Ticket_TicketDTO.AppUserResolved.StatusId,
            };
            Ticket.SLAPolicy = Ticket_TicketDTO.SLAPolicy == null ? null : new SLAPolicy
            {
                Id = Ticket_TicketDTO.SLAPolicy.Id,
                TicketIssueLevelId = Ticket_TicketDTO.SLAPolicy.TicketIssueLevelId,
                TicketPriorityId = Ticket_TicketDTO.SLAPolicy.TicketPriorityId,
                FirstResponseTime = Ticket_TicketDTO.SLAPolicy.FirstResponseTime,
                FirstResponseUnitId = Ticket_TicketDTO.SLAPolicy.FirstResponseUnitId,
                ResolveTime = Ticket_TicketDTO.SLAPolicy.ResolveTime,
                ResolveUnitId = Ticket_TicketDTO.SLAPolicy.ResolveUnitId,
                IsAlert = Ticket_TicketDTO.SLAPolicy.IsAlert,
                IsAlertFRT = Ticket_TicketDTO.SLAPolicy.IsAlertFRT,
                IsEscalation = Ticket_TicketDTO.SLAPolicy.IsEscalation,
                IsEscalationFRT = Ticket_TicketDTO.SLAPolicy.IsEscalationFRT,
            };
            Ticket.TicketResolveType = Ticket_TicketDTO.TicketResolveType == null ? null : new TicketResolveType
            {
                Id = Ticket_TicketDTO.TicketResolveType.Id,
                Code = Ticket_TicketDTO.TicketResolveType.Code,
                Name = Ticket_TicketDTO.TicketResolveType.Name,
            };
            Ticket.Customer = Ticket_TicketDTO.Customer == null ? null : new Customer
            {
                Id = Ticket_TicketDTO.Customer.Id,
                Code = Ticket_TicketDTO.Customer.Code,
            };
            Ticket.CustomerType = Ticket_TicketDTO.CustomerType == null ? null : new CustomerType
            {
                Id = Ticket_TicketDTO.CustomerType.Id,
                Name = Ticket_TicketDTO.CustomerType.Name,
                Code = Ticket_TicketDTO.CustomerType.Code,
            };
            Ticket.Department = Ticket_TicketDTO.Department == null ? null : new Organization
            {
                Id = Ticket_TicketDTO.Department.Id,
                Name = Ticket_TicketDTO.Department.Name,
            };
            Ticket.Product = Ticket_TicketDTO.Product == null ? null : new Product
            {
                Id = Ticket_TicketDTO.Product.Id,
                Name = Ticket_TicketDTO.Product.Name,
            };
            Ticket.RelatedCallLog = Ticket_TicketDTO.RelatedCallLog == null ? null : new CallLog
            {
                Id = Ticket_TicketDTO.RelatedCallLog.Id,
                EntityReferenceId = Ticket_TicketDTO.RelatedCallLog.EntityReferenceId,
                CallTypeId = Ticket_TicketDTO.RelatedCallLog.CallTypeId,
                CallEmotionId = Ticket_TicketDTO.RelatedCallLog.CallEmotionId,
                AppUserId = Ticket_TicketDTO.RelatedCallLog.AppUserId,
                Title = Ticket_TicketDTO.RelatedCallLog.Title,
                Content = Ticket_TicketDTO.RelatedCallLog.Content,
                Phone = Ticket_TicketDTO.RelatedCallLog.Phone,
                CallTime = Ticket_TicketDTO.RelatedCallLog.CallTime,
            };
            Ticket.RelatedTicket = Ticket_TicketDTO.RelatedTicket == null ? null : new Ticket
            {
                Id = Ticket_TicketDTO.RelatedTicket.Id,
                Name = Ticket_TicketDTO.RelatedTicket.Name,
                Phone = Ticket_TicketDTO.RelatedTicket.Phone,
                CustomerId = Ticket_TicketDTO.RelatedTicket.CustomerId,
                UserId = Ticket_TicketDTO.RelatedTicket.UserId,
                ProductId = Ticket_TicketDTO.RelatedTicket.ProductId,
                ReceiveDate = Ticket_TicketDTO.RelatedTicket.ReceiveDate,
                ProcessDate = Ticket_TicketDTO.RelatedTicket.ProcessDate,
                FinishDate = Ticket_TicketDTO.RelatedTicket.FinishDate,
                Subject = Ticket_TicketDTO.RelatedTicket.Subject,
                Content = Ticket_TicketDTO.RelatedTicket.Content,
                TicketIssueLevelId = Ticket_TicketDTO.RelatedTicket.TicketIssueLevelId,
                TicketPriorityId = Ticket_TicketDTO.RelatedTicket.TicketPriorityId,
                TicketStatusId = Ticket_TicketDTO.RelatedTicket.TicketStatusId,
                TicketSourceId = Ticket_TicketDTO.RelatedTicket.TicketSourceId,
                TicketNumber = Ticket_TicketDTO.RelatedTicket.TicketNumber,
                DepartmentId = Ticket_TicketDTO.RelatedTicket.DepartmentId,
                RelatedTicketId = Ticket_TicketDTO.RelatedTicket.RelatedTicketId,
                SLA = Ticket_TicketDTO.RelatedTicket.SLA,
                RelatedCallLogId = Ticket_TicketDTO.RelatedTicket.RelatedCallLogId,
                ResponseMethodId = Ticket_TicketDTO.RelatedTicket.ResponseMethodId,
                StatusId = Ticket_TicketDTO.RelatedTicket.StatusId,
                Used = Ticket_TicketDTO.RelatedTicket.Used,
            };
            Ticket.Status = Ticket_TicketDTO.Status == null ? null : new Status
            {
                Id = Ticket_TicketDTO.Status.Id,
                Code = Ticket_TicketDTO.Status.Code,
                Name = Ticket_TicketDTO.Status.Name,
            };
            Ticket.TicketIssueLevel = Ticket_TicketDTO.TicketIssueLevel == null ? null : new TicketIssueLevel
            {
                Id = Ticket_TicketDTO.TicketIssueLevel.Id,
                Name = Ticket_TicketDTO.TicketIssueLevel.Name,
                OrderNumber = Ticket_TicketDTO.TicketIssueLevel.OrderNumber,
                TicketGroupId = Ticket_TicketDTO.TicketIssueLevel.TicketGroupId,
                TicketGroup = Ticket_TicketDTO.TicketIssueLevel.TicketGroup == null ? null : new TicketGroup
                { 
                    Id = Ticket_TicketDTO.TicketIssueLevel.TicketGroup.Id,
                    Name = Ticket_TicketDTO.TicketIssueLevel.TicketGroup.Name,
                    TicketType = Ticket_TicketDTO.TicketIssueLevel.TicketGroup.TicketType == null ? null : new TicketType { 
                        Id = Ticket_TicketDTO.TicketIssueLevel.TicketGroup.TicketType.Id,
                        Name = Ticket_TicketDTO.TicketIssueLevel.TicketGroup.TicketType.Name
                    }
                },
                StatusId = Ticket_TicketDTO.TicketIssueLevel.StatusId,
                SLA = Ticket_TicketDTO.TicketIssueLevel.SLA,
                Used = Ticket_TicketDTO.TicketIssueLevel.Used,
            };
            Ticket.TicketPriority = Ticket_TicketDTO.TicketPriority == null ? null : new TicketPriority
            {
                Id = Ticket_TicketDTO.TicketPriority.Id,
                Name = Ticket_TicketDTO.TicketPriority.Name,
                OrderNumber = Ticket_TicketDTO.TicketPriority.OrderNumber,
                ColorCode = Ticket_TicketDTO.TicketPriority.ColorCode,
                StatusId = Ticket_TicketDTO.TicketPriority.StatusId,
                Used = Ticket_TicketDTO.TicketPriority.Used,
            };
            Ticket.TicketSource = Ticket_TicketDTO.TicketSource == null ? null : new TicketSource
            {
                Id = Ticket_TicketDTO.TicketSource.Id,
                Name = Ticket_TicketDTO.TicketSource.Name,
                OrderNumber = Ticket_TicketDTO.TicketSource.OrderNumber,
                StatusId = Ticket_TicketDTO.TicketSource.StatusId,
                Used = Ticket_TicketDTO.TicketSource.Used,
            };
            Ticket.TicketStatus = Ticket_TicketDTO.TicketStatus == null ? null : new TicketStatus
            {
                Id = Ticket_TicketDTO.TicketStatus.Id,
                Name = Ticket_TicketDTO.TicketStatus.Name,
                OrderNumber = Ticket_TicketDTO.TicketStatus.OrderNumber,
                ColorCode = Ticket_TicketDTO.TicketStatus.ColorCode,
                StatusId = Ticket_TicketDTO.TicketStatus.StatusId,
                Used = Ticket_TicketDTO.TicketStatus.Used,
            };
            Ticket.User = Ticket_TicketDTO.User == null ? null : new AppUser
            {
                Id = Ticket_TicketDTO.User.Id,
                Username = Ticket_TicketDTO.User.Username,
            };
            Ticket.Creator = Ticket_TicketDTO.Creator == null ? null : new AppUser
            {
                Id = Ticket_TicketDTO.Creator.Id,
                Username = Ticket_TicketDTO.Creator.Username,
            };
            Ticket.SLAStatus = Ticket_TicketDTO.SLAStatus == null ? null : new SLAStatus
            {
                Id = Ticket_TicketDTO.SLAStatus.Id,
                Code = Ticket_TicketDTO.SLAStatus.Code,
                Name = Ticket_TicketDTO.SLAStatus.Name,
            };
            Ticket.BaseLanguage = CurrentContext.Language;
            return Ticket;
        }

        private TicketFilter ConvertFilterDTOToFilterEntity(Ticket_TicketFilterDTO Ticket_TicketFilterDTO)
        {
            TicketFilter TicketFilter = new TicketFilter();
            TicketFilter.Selects = TicketSelect.ALL;
            TicketFilter.Skip = Ticket_TicketFilterDTO.Skip;
            TicketFilter.Take = Ticket_TicketFilterDTO.Take;
            TicketFilter.OrderBy = Ticket_TicketFilterDTO.OrderBy;
            TicketFilter.OrderType = Ticket_TicketFilterDTO.OrderType;
            TicketFilter.Id = Ticket_TicketFilterDTO.Id;
            TicketFilter.Name = Ticket_TicketFilterDTO.Name;
            TicketFilter.Phone = Ticket_TicketFilterDTO.Phone;
            TicketFilter.CustomerId = Ticket_TicketFilterDTO.CustomerId;
            TicketFilter.UserId = Ticket_TicketFilterDTO.UserId;
            TicketFilter.CustomerTypeId = Ticket_TicketFilterDTO.CustomerTypeId;
            TicketFilter.ProductId = Ticket_TicketFilterDTO.ProductId;
            TicketFilter.ReceiveDate = Ticket_TicketFilterDTO.ReceiveDate;
            TicketFilter.ProcessDate = Ticket_TicketFilterDTO.ProcessDate;
            TicketFilter.FinishDate = Ticket_TicketFilterDTO.FinishDate;
            TicketFilter.Subject = Ticket_TicketFilterDTO.Subject;
            TicketFilter.Content = Ticket_TicketFilterDTO.Content;
            TicketFilter.TicketIssueLevelId = Ticket_TicketFilterDTO.TicketIssueLevelId;
            TicketFilter.TicketPriorityId = Ticket_TicketFilterDTO.TicketPriorityId;
            TicketFilter.TicketStatusId = Ticket_TicketFilterDTO.TicketStatusId;
            TicketFilter.TicketSourceId = Ticket_TicketFilterDTO.TicketSourceId;
            TicketFilter.TicketTypeId = Ticket_TicketFilterDTO.TicketTypeId;
            TicketFilter.TicketNumber = Ticket_TicketFilterDTO.TicketNumber;
            TicketFilter.DepartmentId = Ticket_TicketFilterDTO.DepartmentId;
            TicketFilter.RelatedTicketId = Ticket_TicketFilterDTO.RelatedTicketId;
            TicketFilter.SLA = Ticket_TicketFilterDTO.SLA;
            TicketFilter.RelatedCallLogId = Ticket_TicketFilterDTO.RelatedCallLogId;
            TicketFilter.ResponseMethodId = Ticket_TicketFilterDTO.ResponseMethodId;
            TicketFilter.StatusId = Ticket_TicketFilterDTO.StatusId;
            TicketFilter.CreatedAt = Ticket_TicketFilterDTO.CreatedAt;
            TicketFilter.UpdatedAt = Ticket_TicketFilterDTO.UpdatedAt;
            TicketFilter.CustomerAgentId = Ticket_TicketFilterDTO.CustomerAgentId;
            TicketFilter.CustomerRetailId = Ticket_TicketFilterDTO.CustomerRetailId;
            TicketFilter.TicketResolveTypeId = Ticket_TicketFilterDTO.TicketResolveTypeId;
            TicketFilter.ResolveContent = Ticket_TicketFilterDTO.ResolveContent;
            TicketFilter.closedAt = Ticket_TicketFilterDTO.closedAt;
            TicketFilter.AppUserClosedId = Ticket_TicketFilterDTO.AppUserClosedId;
            TicketFilter.FirstResponseAt = Ticket_TicketFilterDTO.FirstResponseAt;
            TicketFilter.LastResponseAt = Ticket_TicketFilterDTO.LastResponseAt;
            TicketFilter.LastHoldingAt = Ticket_TicketFilterDTO.LastHoldingAt;
            TicketFilter.ReraisedTimes = Ticket_TicketFilterDTO.ReraisedTimes;
            TicketFilter.ResolvedAt = Ticket_TicketFilterDTO.ResolvedAt;
            TicketFilter.AppUserResolvedId = Ticket_TicketFilterDTO.AppUserResolvedId;
            TicketFilter.SLAPolicyId = Ticket_TicketFilterDTO.SLAPolicyId;
            TicketFilter.HoldingTime = Ticket_TicketFilterDTO.HoldingTime;
            TicketFilter.ResolveTime = Ticket_TicketFilterDTO.ResolveTime;
            return TicketFilter;
        }
    }
}

