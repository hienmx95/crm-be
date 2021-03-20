using CRM.Common;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;
using CRM.Enums;
using CRM.Rpc.ticket;

namespace CRM.Repositories
{
    public interface ITicketRepository
    {
        Task<int> Count(TicketFilter TicketFilter);
        Task<List<Ticket>> List(TicketFilter TicketFilter);
        Task<Ticket> Get(long Id);
        Task<bool> Create(Ticket Ticket);
        Task<bool> Update(Ticket Ticket);
        Task<bool> Delete(Ticket Ticket);
        Task<bool> BulkMerge(List<Ticket> Tickets);
        Task<bool> BulkDelete(List<Ticket> Tickets);
    }
    public class TicketRepository : ITicketRepository
    {
        private DataContext DataContext;
        public TicketRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<TicketDAO> DynamicFilter(IQueryable<TicketDAO> query, TicketFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            if (filter.CreatedAt != null)
                query = query.Where(q => q.CreatedAt, filter.CreatedAt);
            if (filter.UpdatedAt != null)
                query = query.Where(q => q.UpdatedAt, filter.UpdatedAt);
            if (filter.Id != null)
                query = query.Where(q => q.Id, filter.Id);
            if (filter.Name != null)
                query = query.Where(q => q.Name, filter.Name);
            if (filter.Phone != null)
                query = query.Where(q => q.Phone, filter.Phone);
            if (filter.CustomerId != null)
                query = query.Where(q => q.CustomerId, filter.CustomerId);
            if (filter.UserId != null)
                query = query.Where(q => q.UserId, filter.UserId);
            if (filter.CustomerTypeId != null)
                query = query.Where(q => q.CustomerTypeId, filter.CustomerTypeId);
            if (filter.CreatorId != null)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            if (filter.ProductId != null)
                query = query.Where(q => q.ProductId, filter.ProductId);
            if (filter.ReceiveDate != null)
                query = query.Where(q => q.ReceiveDate, filter.ReceiveDate);
            if (filter.ProcessDate != null)
                query = query.Where(q => q.ProcessDate == null).Union(query.Where(q => q.ProcessDate, filter.ProcessDate));
            if (filter.FinishDate != null)
                query = query.Where(q => q.FinishDate == null).Union(query.Where(q => q.FinishDate, filter.FinishDate));
            if (filter.Subject != null)
                query = query.Where(q => q.Subject, filter.Subject);
            if (filter.Content != null)
                query = query.Where(q => q.Content, filter.Content);
            if (filter.TicketIssueLevelId != null)
                query = query.Where(q => q.TicketIssueLevelId, filter.TicketIssueLevelId);
            if (filter.TicketPriorityId != null)
                query = query.Where(q => q.TicketPriorityId, filter.TicketPriorityId);
            if (filter.TicketStatusId != null)
                query = query.Where(q => q.TicketStatusId, filter.TicketStatusId);
            if (filter.TicketSourceId != null)
                query = query.Where(q => q.TicketSourceId, filter.TicketSourceId);
            if (filter.TicketTypeId != null)
                query = query.Where(q => q.TicketIssueLevel.TicketGroup.TicketTypeId, filter.TicketTypeId);
            if (filter.TicketNumber != null)
                query = query.Where(q => q.TicketNumber, filter.TicketNumber);
            if (filter.DepartmentId != null)
                query = query.Where(q => q.DepartmentId, filter.DepartmentId);
            if (filter.RelatedTicketId != null)
                query = query.Where(q => q.RelatedTicketId, filter.RelatedTicketId);
            if (filter.SLA != null)
                query = query.Where(q => q.SLA, filter.SLA);
            if (filter.RelatedCallLogId != null)
                query = query.Where(q => q.RelatedCallLogId, filter.RelatedCallLogId);
            if (filter.ResponseMethodId != null)
                query = query.Where(q => q.ResponseMethodId, filter.ResponseMethodId);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.TicketResolveTypeId != null)
                query = query.Where(q => q.TicketResolveTypeId, filter.TicketResolveTypeId);
            if (filter.ResolveContent != null)
                query = query.Where(q => q.ResolveContent, filter.ResolveContent);
            if (filter.closedAt != null)
                query = query.Where(q => q.closedAt == null).Union(query.Where(q => q.closedAt, filter.closedAt));
            if (filter.AppUserClosedId != null)
                query = query.Where(q => q.AppUserClosedId, filter.AppUserClosedId);
            if (filter.FirstResponseAt != null)
                query = query.Where(q => q.FirstResponseAt == null).Union(query.Where(q => q.FirstResponseAt, filter.FirstResponseAt));
            if (filter.LastResponseAt != null)
                query = query.Where(q => q.LastResponseAt == null).Union(query.Where(q => q.LastResponseAt, filter.LastResponseAt));
            if (filter.LastHoldingAt != null)
                query = query.Where(q => q.LastHoldingAt == null).Union(query.Where(q => q.LastHoldingAt, filter.LastHoldingAt));
            if (filter.ReraisedTimes != null)
                query = query.Where(q => q.ReraisedTimes, filter.ReraisedTimes);
            if (filter.ResolvedAt != null)
                query = query.Where(q => q.ResolvedAt == null).Union(query.Where(q => q.ResolvedAt, filter.ResolvedAt));
            if (filter.AppUserResolvedId != null)
                query = query.Where(q => q.AppUserResolvedId, filter.AppUserResolvedId);
            if (filter.SLAPolicyId != null)
                query = query.Where(q => q.SLAPolicyId, filter.SLAPolicyId);
            if (filter.HoldingTime != null)
                query = query.Where(q => q.HoldingTime, filter.HoldingTime);
            if (filter.ResolveTime != null)
                query = query.Where(q => q.ResolveTime, filter.ResolveTime);


            if (filter.SLAStatusId != null)
                query = query.Where(q => q.SLAStatusId, filter.SLAStatusId);
            if (filter.TicketGroupId != null && filter.TicketGroupId.Equal.HasValue)
                query = query.Where(p => p.TicketIssueLevel != null).Where(q => q.TicketIssueLevel.TicketGroupId, filter.TicketGroupId);

            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<TicketDAO> OrFilter(IQueryable<TicketDAO> query, TicketFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<TicketDAO> initQuery = query.Where(q => false);
            foreach (TicketFilter TicketFilter in filter.OrFilter)
            {
                IQueryable<TicketDAO> queryable = query;
                if (TicketFilter.UserId != null)
                {
                    if (TicketFilter.UserId.Equal != null)
                    {
                        AppUserDAO AppUserDAO = DataContext.AppUser.Where(p => p.Id == TicketFilter.UserId.Equal).FirstOrDefault();
                        OrganizationDAO OrganizationDAO = DataContext.Organization
                            .Where(o => o.Id == AppUserDAO.OrganizationId).FirstOrDefault();
                        queryable = queryable.Where(q => q.Creator.Organization.Path.StartsWith(OrganizationDAO.Path));
                    }
                    if (TicketFilter.UserId.NotEqual != null)
                    {
                        AppUserDAO AppUserDAO = DataContext.AppUser.Where(p => p.Id == TicketFilter.UserId.NotEqual).FirstOrDefault();
                        OrganizationDAO OrganizationDAO = DataContext.Organization
                            .Where(o => o.Id != AppUserDAO.OrganizationId).FirstOrDefault();
                        queryable = queryable.Where(q => !q.Creator.Organization.Path.StartsWith(OrganizationDAO.Path));
                    }
                }
                if (TicketFilter.OrganizationId != null)
                {
                    if (TicketFilter.OrganizationId.Equal != null)
                    {
                        OrganizationDAO OrganizationDAO = DataContext.Organization
                            .Where(o => o.Id == TicketFilter.OrganizationId.Equal.Value).FirstOrDefault();
                        queryable = queryable.Where(q => q.Creator.Organization.Path.StartsWith(OrganizationDAO.Path)
                        );
                    }
                    if (TicketFilter.OrganizationId.NotEqual != null)
                    {
                        OrganizationDAO OrganizationDAO = DataContext.Organization
                            .Where(o => o.Id == TicketFilter.OrganizationId.NotEqual.Value).FirstOrDefault();
                        queryable = queryable.Where(q => !q.Creator.Organization.Path.StartsWith(OrganizationDAO.Path)
                        );
                    }
                    if (TicketFilter.OrganizationId.In != null)
                    {
                        List<OrganizationDAO> OrganizationDAOs = DataContext.Organization
                            .Where(o => o.DeletedAt == null && o.StatusId == 1).ToList();
                        List<OrganizationDAO> Parents = OrganizationDAOs.Where(o => TicketFilter.OrganizationId.In.Contains(o.Id)).ToList();
                        List<OrganizationDAO> Branches = OrganizationDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
                        List<long> Ids = Branches.Select(o => o.Id).ToList();
                        queryable = queryable.Where(q => Ids.Contains(q.Creator.OrganizationId) );
                    }
                    if (TicketFilter.OrganizationId.NotIn != null)
                    {
                        List<OrganizationDAO> OrganizationDAOs = DataContext.Organization
                            .Where(o => o.DeletedAt == null && o.StatusId == 1).ToList();
                        List<OrganizationDAO> Parents = OrganizationDAOs.Where(o => TicketFilter.OrganizationId.NotIn.Contains(o.Id)).ToList();
                        List<OrganizationDAO> Branches = OrganizationDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
                        List<long> Ids = Branches.Select(o => o.Id).ToList();
                        queryable = queryable.Where(q => !Ids.Contains(q.Creator.OrganizationId));
                    }
                }
                if (TicketFilter.AppUserId != null)
                    queryable = queryable.Where(q => q.CreatorId, TicketFilter.AppUserId);

                if (TicketFilter.TicketTypeId != null)
                {
                    if (TicketFilter.TicketTypeId.Equal != null)
                    {
                        queryable = from t1 in queryable
                                    join t2 in DataContext.TicketIssueLevel on t1.TicketIssueLevelId equals t2.Id
                                    join t3 in DataContext.TicketGroup on t2.TicketGroupId equals t3.Id
                                    join t4 in DataContext.TicketType on t3.TicketTypeId equals t4.Id
                                    where t4.Id == TicketFilter.TicketTypeId.Equal
                                    select t1;
                    }
                    if (TicketFilter.TicketTypeId.NotEqual != null)
                    {
                        queryable = from t1 in queryable
                                    join t2 in DataContext.TicketIssueLevel on t1.TicketIssueLevelId equals t2.Id
                                    join t3 in DataContext.TicketGroup on t2.TicketGroupId equals t3.Id
                                    join t4 in DataContext.TicketType on t3.TicketTypeId equals t4.Id
                                    where t4.Id != TicketFilter.TicketTypeId.NotEqual
                                    select t1;
                    }
                    if (TicketFilter.TicketTypeId.In != null)
                    {
                        queryable = from t1 in queryable
                                    join t2 in DataContext.TicketIssueLevel on t1.TicketIssueLevelId equals t2.Id
                                    join t3 in DataContext.TicketGroup on t2.TicketGroupId equals t3.Id
                                    join t4 in DataContext.TicketType on t3.TicketTypeId equals t4.Id
                                    where TicketFilter.TicketTypeId.In.Contains(t4.Id)
                                    select t1;
                    }
                    if (TicketFilter.TicketTypeId.NotIn != null)
                    {
                        queryable = from t1 in queryable
                                    join t2 in DataContext.TicketIssueLevel on t1.TicketIssueLevelId equals t2.Id
                                    join t3 in DataContext.TicketGroup on t2.TicketGroupId equals t3.Id
                                    join t4 in DataContext.TicketType on t3.TicketTypeId equals t4.Id
                                    where !TicketFilter.TicketTypeId.NotIn.Contains(t4.Id)
                                    select t1;
                    }
                }
                if (TicketFilter.DepartmentId != null)
                    queryable = queryable.Where(q => q.DepartmentId, TicketFilter.DepartmentId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<TicketDAO> DynamicOrder(IQueryable<TicketDAO> query, TicketFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case TicketOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case TicketOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case TicketOrder.Phone:
                            query = query.OrderBy(q => q.Phone);
                            break;
                        case TicketOrder.Customer:
                            query = query.OrderBy(q => q.CustomerId);
                            break;
                        case TicketOrder.User:
                            query = query.OrderBy(q => q.UserId);
                            break;
                        case TicketOrder.CustomerType:
                            query = query.OrderBy(q => q.CustomerTypeId);
                            break;
                        case TicketOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case TicketOrder.Product:
                            query = query.OrderBy(q => q.ProductId);
                            break;
                        case TicketOrder.ReceiveDate:
                            query = query.OrderBy(q => q.ReceiveDate);
                            break;
                        case TicketOrder.ProcessDate:
                            query = query.OrderBy(q => q.ProcessDate);
                            break;
                        case TicketOrder.FinishDate:
                            query = query.OrderBy(q => q.FinishDate);
                            break;
                        case TicketOrder.Subject:
                            query = query.OrderBy(q => q.Subject);
                            break;
                        case TicketOrder.Content:
                            query = query.OrderBy(q => q.Content);
                            break;
                        case TicketOrder.TicketIssueLevel:
                            query = query.OrderBy(q => q.TicketIssueLevelId);
                            break;
                        case TicketOrder.TicketPriority:
                            query = query.OrderBy(q => q.TicketPriorityId);
                            break;
                        case TicketOrder.TicketStatus:
                            query = query.OrderBy(q => q.TicketStatusId);
                            break;
                        case TicketOrder.TicketSource:
                            query = query.OrderBy(q => q.TicketSourceId);
                            break;
                        case TicketOrder.TicketNumber:
                            query = query.OrderBy(q => q.TicketNumber);
                            break;
                        case TicketOrder.Department:
                            query = query.OrderBy(q => q.DepartmentId);
                            break;
                        case TicketOrder.RelatedTicket:
                            query = query.OrderBy(q => q.RelatedTicketId);
                            break;
                        case TicketOrder.SLA:
                            query = query.OrderBy(q => q.SLA);
                            break;
                        case TicketOrder.RelatedCallLog:
                            query = query.OrderBy(q => q.RelatedCallLogId);
                            break;
                        case TicketOrder.ResponseMethod:
                            query = query.OrderBy(q => q.ResponseMethodId);
                            break;
                        case TicketOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case TicketOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                        case TicketOrder.IsAlerted:
                            query = query.OrderBy(q => q.IsAlerted);
                            break;
                        case TicketOrder.IsAlertedFRT:
                            query = query.OrderBy(q => q.IsAlertedFRT);
                            break;
                        case TicketOrder.IsEscalated:
                            query = query.OrderBy(q => q.IsEscalated);
                            break;
                        case TicketOrder.IsEscalatedFRT:
                            query = query.OrderBy(q => q.IsEscalatedFRT);
                            break;
                        case TicketOrder.TicketResolveType:
                            query = query.OrderBy(q => q.TicketResolveTypeId);
                            break;
                        case TicketOrder.ResolveContent:
                            query = query.OrderBy(q => q.ResolveContent);
                            break;
                        case TicketOrder.closedAt:
                            query = query.OrderBy(q => q.closedAt);
                            break;
                        case TicketOrder.AppUserClosed:
                            query = query.OrderBy(q => q.AppUserClosedId);
                            break;
                        case TicketOrder.FirstResponseAt:
                            query = query.OrderBy(q => q.FirstResponseAt);
                            break;
                        case TicketOrder.LastResponseAt:
                            query = query.OrderBy(q => q.LastResponseAt);
                            break;
                        case TicketOrder.LastHoldingAt:
                            query = query.OrderBy(q => q.LastHoldingAt);
                            break;
                        case TicketOrder.ReraisedTimes:
                            query = query.OrderBy(q => q.ReraisedTimes);
                            break;
                        case TicketOrder.ResolvedAt:
                            query = query.OrderBy(q => q.ResolvedAt);
                            break;
                        case TicketOrder.AppUserResolved:
                            query = query.OrderBy(q => q.AppUserResolvedId);
                            break;
                        case TicketOrder.IsClose:
                            query = query.OrderBy(q => q.IsClose);
                            break;
                        case TicketOrder.IsOpen:
                            query = query.OrderBy(q => q.IsOpen);
                            break;
                        case TicketOrder.IsWait:
                            query = query.OrderBy(q => q.IsWait);
                            break;
                        case TicketOrder.IsWork:
                            query = query.OrderBy(q => q.IsWork);
                            break;
                        case TicketOrder.SLAPolicy:
                            query = query.OrderBy(q => q.SLAPolicyId);
                            break;
                        case TicketOrder.HoldingTime:
                            query = query.OrderBy(q => q.HoldingTime);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case TicketOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case TicketOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case TicketOrder.Phone:
                            query = query.OrderByDescending(q => q.Phone);
                            break;
                        case TicketOrder.Customer:
                            query = query.OrderByDescending(q => q.CustomerId);
                            break;
                        case TicketOrder.User:
                            query = query.OrderByDescending(q => q.UserId);
                            break;
                        case TicketOrder.CustomerType:
                            query = query.OrderByDescending(q => q.CustomerTypeId);
                            break;
                        case TicketOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case TicketOrder.Product:
                            query = query.OrderByDescending(q => q.ProductId);
                            break;
                        case TicketOrder.ReceiveDate:
                            query = query.OrderByDescending(q => q.ReceiveDate);
                            break;
                        case TicketOrder.ProcessDate:
                            query = query.OrderByDescending(q => q.ProcessDate);
                            break;
                        case TicketOrder.FinishDate:
                            query = query.OrderByDescending(q => q.FinishDate);
                            break;
                        case TicketOrder.Subject:
                            query = query.OrderByDescending(q => q.Subject);
                            break;
                        case TicketOrder.Content:
                            query = query.OrderByDescending(q => q.Content);
                            break;
                        case TicketOrder.TicketIssueLevel:
                            query = query.OrderByDescending(q => q.TicketIssueLevelId);
                            break;
                        case TicketOrder.TicketPriority:
                            query = query.OrderByDescending(q => q.TicketPriorityId);
                            break;
                        case TicketOrder.TicketStatus:
                            query = query.OrderByDescending(q => q.TicketStatusId);
                            break;
                        case TicketOrder.TicketSource:
                            query = query.OrderByDescending(q => q.TicketSourceId);
                            break;
                        case TicketOrder.TicketNumber:
                            query = query.OrderByDescending(q => q.TicketNumber);
                            break;
                        case TicketOrder.Department:
                            query = query.OrderByDescending(q => q.DepartmentId);
                            break;
                        case TicketOrder.RelatedTicket:
                            query = query.OrderByDescending(q => q.RelatedTicketId);
                            break;
                        case TicketOrder.SLA:
                            query = query.OrderByDescending(q => q.SLA);
                            break;
                        case TicketOrder.RelatedCallLog:
                            query = query.OrderByDescending(q => q.RelatedCallLogId);
                            break;
                        case TicketOrder.ResponseMethod:
                            query = query.OrderByDescending(q => q.ResponseMethodId);
                            break;
                        case TicketOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case TicketOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                        case TicketOrder.IsAlerted:
                            query = query.OrderByDescending(q => q.IsAlerted);
                            break;
                        case TicketOrder.IsAlertedFRT:
                            query = query.OrderByDescending(q => q.IsAlertedFRT);
                            break;
                        case TicketOrder.IsEscalated:
                            query = query.OrderByDescending(q => q.IsEscalated);
                            break;
                        case TicketOrder.IsEscalatedFRT:
                            query = query.OrderByDescending(q => q.IsEscalatedFRT);
                            break;
                        case TicketOrder.TicketResolveType:
                            query = query.OrderByDescending(q => q.TicketResolveTypeId);
                            break;
                        case TicketOrder.ResolveContent:
                            query = query.OrderByDescending(q => q.ResolveContent);
                            break;
                        case TicketOrder.closedAt:
                            query = query.OrderByDescending(q => q.closedAt);
                            break;
                        case TicketOrder.AppUserClosed:
                            query = query.OrderByDescending(q => q.AppUserClosedId);
                            break;
                        case TicketOrder.FirstResponseAt:
                            query = query.OrderByDescending(q => q.FirstResponseAt);
                            break;
                        case TicketOrder.LastResponseAt:
                            query = query.OrderByDescending(q => q.LastResponseAt);
                            break;
                        case TicketOrder.LastHoldingAt:
                            query = query.OrderByDescending(q => q.LastHoldingAt);
                            break;
                        case TicketOrder.ReraisedTimes:
                            query = query.OrderByDescending(q => q.ReraisedTimes);
                            break;
                        case TicketOrder.ResolvedAt:
                            query = query.OrderByDescending(q => q.ResolvedAt);
                            break;
                        case TicketOrder.AppUserResolved:
                            query = query.OrderByDescending(q => q.AppUserResolvedId);
                            break;
                        case TicketOrder.IsClose:
                            query = query.OrderByDescending(q => q.IsClose);
                            break;
                        case TicketOrder.IsOpen:
                            query = query.OrderByDescending(q => q.IsOpen);
                            break;
                        case TicketOrder.IsWait:
                            query = query.OrderByDescending(q => q.IsWait);
                            break;
                        case TicketOrder.IsWork:
                            query = query.OrderByDescending(q => q.IsWork);
                            break;
                        case TicketOrder.SLAPolicy:
                            query = query.OrderByDescending(q => q.SLAPolicyId);
                            break;
                        case TicketOrder.HoldingTime:
                            query = query.OrderByDescending(q => q.HoldingTime);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Ticket>> DynamicSelect(IQueryable<TicketDAO> query, TicketFilter filter)
        {
            List<Ticket> Tickets = await query.Select(q => new Ticket()
            {
                Id = filter.Selects.Contains(TicketSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(TicketSelect.Name) ? q.Name : default(string),
                Phone = filter.Selects.Contains(TicketSelect.Phone) ? q.Phone : default(string),
                CustomerId = filter.Selects.Contains(TicketSelect.Customer) ? q.CustomerId : default(long),
                UserId = filter.Selects.Contains(TicketSelect.User) ? q.UserId : default(long),
                CustomerTypeId = filter.Selects.Contains(TicketSelect.CustomerType) ? q.CustomerTypeId : default(long),
                CreatorId = filter.Selects.Contains(TicketSelect.Creator) ? q.CreatorId : default(long),
                ProductId = filter.Selects.Contains(TicketSelect.Product) ? q.ProductId : default(long?),
                ReceiveDate = filter.Selects.Contains(TicketSelect.ReceiveDate) ? q.ReceiveDate : default(DateTime),
                ProcessDate = filter.Selects.Contains(TicketSelect.ProcessDate) ? q.ProcessDate : default(DateTime?),
                FinishDate = filter.Selects.Contains(TicketSelect.FinishDate) ? q.FinishDate : default(DateTime?),
                Subject = filter.Selects.Contains(TicketSelect.Subject) ? q.Subject : default(string),
                Content = filter.Selects.Contains(TicketSelect.Content) ? q.Content : default(string),
                TicketIssueLevelId = filter.Selects.Contains(TicketSelect.TicketIssueLevel) ? q.TicketIssueLevelId : default(long),
                TicketPriorityId = filter.Selects.Contains(TicketSelect.TicketPriority) ? q.TicketPriorityId : default(long),
                TicketStatusId = filter.Selects.Contains(TicketSelect.TicketStatus) ? q.TicketStatusId : default(long),
                TicketSourceId = filter.Selects.Contains(TicketSelect.TicketSource) ? q.TicketSourceId : default(long),
                TicketNumber = filter.Selects.Contains(TicketSelect.TicketNumber) ? q.TicketNumber : default(string),
                DepartmentId = filter.Selects.Contains(TicketSelect.Department) ? q.DepartmentId : default(long?),
                RelatedTicketId = filter.Selects.Contains(TicketSelect.RelatedTicket) ? q.RelatedTicketId : default(long?),
                SLA = filter.Selects.Contains(TicketSelect.SLA) ? q.SLA : default(long),
                RelatedCallLogId = filter.Selects.Contains(TicketSelect.RelatedCallLog) ? q.RelatedCallLogId : default(long?),
                ResponseMethodId = filter.Selects.Contains(TicketSelect.ResponseMethod) ? q.ResponseMethodId : default(long?),
                StatusId = filter.Selects.Contains(TicketSelect.Status) ? q.StatusId : default(long),
                IsAlerted = filter.Selects.Contains(TicketSelect.IsAlerted) ? q.IsAlerted : default(bool?),
                IsAlertedFRT = filter.Selects.Contains(TicketSelect.IsAlertedFRT) ? q.IsAlertedFRT : default(bool?),
                IsEscalated = filter.Selects.Contains(TicketSelect.IsEscalated) ? q.IsEscalated : default(bool?),
                IsEscalatedFRT = filter.Selects.Contains(TicketSelect.IsEscalatedFRT) ? q.IsEscalatedFRT : default(bool?),
                Used = filter.Selects.Contains(TicketSelect.Used) ? q.Used : default(bool),
                TicketResolveTypeId = filter.Selects.Contains(TicketSelect.TicketResolveType) ? q.TicketResolveTypeId : default(long?),
                ResolveContent = filter.Selects.Contains(TicketSelect.ResolveContent) ? q.ResolveContent : default(string?),
                closedAt = filter.Selects.Contains(TicketSelect.closedAt) ? q.closedAt : default(DateTime?),
                AppUserClosedId = filter.Selects.Contains(TicketSelect.AppUserClosed) ? q.AppUserClosedId : default(long?),
                FirstResponseAt = filter.Selects.Contains(TicketSelect.FirstResponseAt) ? q.FirstResponseAt : default(DateTime?),
                LastResponseAt = filter.Selects.Contains(TicketSelect.LastResponseAt) ? q.LastResponseAt : default(DateTime?),
                LastHoldingAt = filter.Selects.Contains(TicketSelect.LastHoldingAt) ? q.LastHoldingAt : default(DateTime?),
                ReraisedTimes = filter.Selects.Contains(TicketSelect.ReraisedTimes) ? q.ReraisedTimes : default(long?),
                ResolvedAt = filter.Selects.Contains(TicketSelect.ResolvedAt) ? q.ResolvedAt : default(DateTime?),
                ResolveTime = filter.Selects.Contains(TicketSelect.ResolveTime) ? q.ResolveTime : default(DateTime?),
                AppUserResolvedId = filter.Selects.Contains(TicketSelect.AppUserResolved) ? q.AppUserResolvedId : default(long?),
                IsClose = filter.Selects.Contains(TicketSelect.IsClose) ? q.IsClose : default(bool?),
                IsOpen = filter.Selects.Contains(TicketSelect.IsOpen) ? q.IsOpen : default(bool?),
                IsWait = filter.Selects.Contains(TicketSelect.IsWait) ? q.IsWait : default(bool?),
                IsWork = filter.Selects.Contains(TicketSelect.IsWork) ? q.IsWork : default(bool?),
                SLAPolicyId = filter.Selects.Contains(TicketSelect.SLAPolicy) ? q.SLAPolicyId : default(long?),
                HoldingTime = filter.Selects.Contains(TicketSelect.HoldingTime) ? q.HoldingTime : default(long?),
                SLAStatusId = filter.Selects.Contains(TicketSelect.SLAStatusId) ? q.SLAStatusId : default(long?),
                AppUserClosed = filter.Selects.Contains(TicketSelect.AppUserClosed) && q.AppUserClosed != null ? new AppUser
                {
                    Id = q.AppUserClosed.Id,
                    Username = q.AppUserClosed.Username,
                    DisplayName = q.AppUserClosed.DisplayName,
                    Address = q.AppUserClosed.Address,
                    Email = q.AppUserClosed.Email,
                    Phone = q.AppUserClosed.Phone,
                    SexId = q.AppUserClosed.SexId,
                    Birthday = q.AppUserClosed.Birthday,
                    Avatar = q.AppUserClosed.Avatar,
                    Department = q.AppUserClosed.Department,
                    OrganizationId = q.AppUserClosed.OrganizationId,
                    Longitude = q.AppUserClosed.Longitude,
                    Latitude = q.AppUserClosed.Latitude,
                    StatusId = q.AppUserClosed.StatusId,
                } : null,
                AppUserResolved = filter.Selects.Contains(TicketSelect.AppUserResolved) && q.AppUserResolved != null ? new AppUser
                {
                    Id = q.AppUserResolved.Id,
                    Username = q.AppUserResolved.Username,
                    DisplayName = q.AppUserResolved.DisplayName,
                    Address = q.AppUserResolved.Address,
                    Email = q.AppUserResolved.Email,
                    Phone = q.AppUserResolved.Phone,
                    SexId = q.AppUserResolved.SexId,
                    Birthday = q.AppUserResolved.Birthday,
                    Avatar = q.AppUserResolved.Avatar,
                    Department = q.AppUserResolved.Department,
                    OrganizationId = q.AppUserResolved.OrganizationId,
                    Longitude = q.AppUserResolved.Longitude,
                    Latitude = q.AppUserResolved.Latitude,
                    StatusId = q.AppUserResolved.StatusId,
                } : null,
                SLAPolicy = filter.Selects.Contains(TicketSelect.SLAPolicy) && q.SLAPolicy != null ? new SLAPolicy
                {
                    Id = q.SLAPolicy.Id,
                    TicketIssueLevelId = q.SLAPolicy.TicketIssueLevelId,
                    TicketPriorityId = q.SLAPolicy.TicketPriorityId,
                    FirstResponseTime = q.SLAPolicy.FirstResponseTime,
                    FirstResponseUnitId = q.SLAPolicy.FirstResponseUnitId,
                    ResolveTime = q.SLAPolicy.ResolveTime,
                    ResolveUnitId = q.SLAPolicy.ResolveUnitId,
                    IsAlert = q.SLAPolicy.IsAlert,
                    IsAlertFRT = q.SLAPolicy.IsAlertFRT,
                    IsEscalation = q.SLAPolicy.IsEscalation,
                    IsEscalationFRT = q.SLAPolicy.IsEscalationFRT,
                } : null,
                Customer = filter.Selects.Contains(TicketSelect.Customer) && q.Customer != null ? new Customer
                {
                    Id = q.Customer.Id,
                    Code = q.Customer.Code,
                    Name = q.Customer.Name,
                    Phone = q.Customer.Phone,
                } : null,
                CustomerType = filter.Selects.Contains(TicketSelect.CustomerType) && q.CustomerType != null ? new CustomerType
                {
                    Id = q.CustomerType.Id,
                    Name = q.CustomerType.Name,
                    Code = q.CustomerType.Code,
                } : null,
                Department = filter.Selects.Contains(TicketSelect.Department) && q.Department != null ? new Organization
                {
                    Id = q.Department.Id,
                    Name = q.Department.Name,
                } : null,
                Product = filter.Selects.Contains(TicketSelect.Product) && q.Product != null ? new Product
                {
                    Id = q.Product.Id,
                    Name = q.Product.Name,
                } : null,
                RelatedCallLog = filter.Selects.Contains(TicketSelect.RelatedCallLog) && q.RelatedCallLog != null ? new CallLog
                {
                    Id = q.RelatedCallLog.Id,
                    EntityReferenceId = q.RelatedCallLog.EntityReferenceId,
                    CallTypeId = q.RelatedCallLog.CallTypeId,
                    CallEmotionId = q.RelatedCallLog.CallEmotionId,
                    AppUserId = q.RelatedCallLog.AppUserId,
                    Title = q.RelatedCallLog.Title,
                    Content = q.RelatedCallLog.Content,
                    Phone = q.RelatedCallLog.Phone,
                    CallTime = q.RelatedCallLog.CallTime,
                } : null,
                RelatedTicket = filter.Selects.Contains(TicketSelect.RelatedTicket) && q.RelatedTicket != null ? new Ticket
                {
                    Id = q.RelatedTicket.Id,
                    Name = q.RelatedTicket.Name,
                    Phone = q.RelatedTicket.Phone,
                    CustomerId = q.RelatedTicket.CustomerId,
                    UserId = q.RelatedTicket.UserId,
                    ProductId = q.RelatedTicket.ProductId,
                    ReceiveDate = q.RelatedTicket.ReceiveDate,
                    ProcessDate = q.RelatedTicket.ProcessDate,
                    FinishDate = q.RelatedTicket.FinishDate,
                    Subject = q.RelatedTicket.Subject,
                    Content = q.RelatedTicket.Content,
                    TicketIssueLevelId = q.RelatedTicket.TicketIssueLevelId,
                    TicketPriorityId = q.RelatedTicket.TicketPriorityId,
                    TicketStatusId = q.RelatedTicket.TicketStatusId,
                    TicketSourceId = q.RelatedTicket.TicketSourceId,
                    TicketNumber = q.RelatedTicket.TicketNumber,
                    DepartmentId = q.RelatedTicket.DepartmentId,
                    RelatedTicketId = q.RelatedTicket.RelatedTicketId,
                    SLA = q.RelatedTicket.SLA,
                    RelatedCallLogId = q.RelatedTicket.RelatedCallLogId,
                    ResponseMethodId = q.RelatedTicket.ResponseMethodId,
                    StatusId = q.RelatedTicket.StatusId,
                    Used = q.RelatedTicket.Used,
                } : null,
                Status = filter.Selects.Contains(TicketSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                SLAStatus = filter.Selects.Contains(TicketSelect.SLAStatus) && q.SLAStatus != null ? new SLAStatus
                {
                    Id = q.SLAStatus.Id,
                    Code = q.SLAStatus.Code,
                    Name = q.SLAStatus.Name,
                    ColorCode = q.SLAStatus.ColorCode,
                } : null,
                TicketIssueLevel = filter.Selects.Contains(TicketSelect.TicketIssueLevel) && q.TicketIssueLevel != null ? new TicketIssueLevel
                {
                    Id = q.TicketIssueLevel.Id,
                    Name = q.TicketIssueLevel.Name,
                    OrderNumber = q.TicketIssueLevel.OrderNumber,
                    TicketGroupId = q.TicketIssueLevel.TicketGroupId,
                    StatusId = q.TicketIssueLevel.StatusId,
                    TicketGroup = filter.Selects.Contains(TicketIssueLevelSelect.TicketGroup) && q.TicketIssueLevel.TicketGroup != null ? new TicketGroup
                    {
                        Id = q.TicketIssueLevel.TicketGroup.Id,
                        Name = q.TicketIssueLevel.TicketGroup.Name,
                        OrderNumber = q.TicketIssueLevel.TicketGroup.OrderNumber,
                        StatusId = q.TicketIssueLevel.TicketGroup.StatusId,
                        TicketTypeId = q.TicketIssueLevel.TicketGroup.TicketTypeId,
                        TicketType = q.TicketIssueLevel.TicketGroup.TicketType == null ? null : new TicketType
                        {
                            Id = q.TicketIssueLevel.TicketGroup.TicketType.Id,
                            Code = q.TicketIssueLevel.TicketGroup.TicketType.Code,
                            Name = q.TicketIssueLevel.TicketGroup.TicketType.Name,
                            StatusId = q.TicketIssueLevel.TicketGroup.TicketType.StatusId,
                            Used = q.TicketIssueLevel.TicketGroup.TicketType.Used,
                        },
                        Used = q.TicketIssueLevel.TicketGroup.Used,
                    } : null,
                    SLA = q.TicketIssueLevel.SLA,
                    Used = q.TicketIssueLevel.Used,
                } : null,
                TicketPriority = filter.Selects.Contains(TicketSelect.TicketPriority) && q.TicketPriority != null ? new TicketPriority
                {
                    Id = q.TicketPriority.Id,
                    Name = q.TicketPriority.Name,
                    OrderNumber = q.TicketPriority.OrderNumber,
                    ColorCode = q.TicketPriority.ColorCode,
                    StatusId = q.TicketPriority.StatusId,
                    Used = q.TicketPriority.Used,
                } : null,
                TicketSource = filter.Selects.Contains(TicketSelect.TicketSource) && q.TicketSource != null ? new TicketSource
                {
                    Id = q.TicketSource.Id,
                    Name = q.TicketSource.Name,
                    OrderNumber = q.TicketSource.OrderNumber,
                    StatusId = q.TicketSource.StatusId,
                    Used = q.TicketSource.Used,
                } : null,
                TicketStatus = filter.Selects.Contains(TicketSelect.TicketStatus) && q.TicketStatus != null ? new TicketStatus
                {
                    Id = q.TicketStatus.Id,
                    Name = q.TicketStatus.Name,
                    OrderNumber = q.TicketStatus.OrderNumber,
                    ColorCode = q.TicketStatus.ColorCode,
                    StatusId = q.TicketStatus.StatusId,
                    Used = q.TicketStatus.Used,
                } : null,
                User = filter.Selects.Contains(TicketSelect.User) && q.User != null ? new AppUser
                {
                    Id = q.User.Id,
                    Username = q.User.Username,
                    DisplayName = q.User.DisplayName,
                } : null,
                Creator = filter.Selects.Contains(TicketSelect.Creator) && q.Creator != null ? new AppUser
                {
                    Id = q.Creator.Id,
                    Username = q.Creator.Username,
                } : null,
                TicketResolveType = filter.Selects.Contains(TicketSelect.TicketResolveType) && q.TicketResolveType != null ? new TicketResolveType
                {
                    Id = q.TicketResolveType.Id,
                    Code = q.TicketResolveType.Code,
                    Name = q.TicketResolveType.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
                SLADatetime = q.CreatedAt.AddMinutes(q.SLA) <= StaticParams.DateTimeNow ? "Trễ hạn" : ""
            }).ToListAsync();
            return Tickets;
        }

        public async Task<int> Count(TicketFilter filter)
        {
            IQueryable<TicketDAO> Tickets = DataContext.Ticket.AsNoTracking();
            Tickets = DynamicFilter(Tickets, filter);
            return await Tickets.CountAsync();
        }

        public async Task<List<Ticket>> List(TicketFilter filter)
        {
            if (filter == null) return new List<Ticket>();
            IQueryable<TicketDAO> TicketDAOs = DataContext.Ticket.AsNoTracking();
            TicketDAOs = DynamicFilter(TicketDAOs, filter);
            TicketDAOs = DynamicOrder(TicketDAOs, filter);
            List<Ticket> Tickets = await DynamicSelect(TicketDAOs, filter);
            return Tickets;
        }

        public async Task<Ticket> Get(long Id)
        {
            Ticket Ticket = await DataContext.Ticket.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new Ticket()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
                CustomerId = x.CustomerId,
                UserId = x.UserId,
                CustomerTypeId = x.CustomerTypeId,
                CreatorId = x.CreatorId,
                ProductId = x.ProductId,
                ReceiveDate = x.ReceiveDate,
                ProcessDate = x.ProcessDate,
                FinishDate = x.FinishDate,
                Subject = x.Subject,
                Content = x.Content,
                TicketIssueLevelId = x.TicketIssueLevelId,
                TicketPriorityId = x.TicketPriorityId,
                TicketStatusId = x.TicketStatusId,
                TicketSourceId = x.TicketSourceId,
                TicketNumber = x.TicketNumber,
                DepartmentId = x.DepartmentId,
                RelatedTicketId = x.RelatedTicketId,
                SLA = x.SLA,
                RelatedCallLogId = x.RelatedCallLogId,
                ResponseMethodId = x.ResponseMethodId,
                StatusId = x.StatusId,
                IsAlerted = x.IsAlerted,
                IsAlertedFRT = x.IsAlertedFRT,
                IsEscalated = x.IsEscalated,
                IsEscalatedFRT = x.IsEscalatedFRT,
                Used = x.Used,
                closedAt = x.closedAt,
                AppUserClosedId = x.AppUserClosedId,
                FirstResponseAt = x.FirstResponseAt,
                LastResponseAt = x.LastResponseAt,
                LastHoldingAt = x.LastHoldingAt,
                ReraisedTimes = x.ReraisedTimes,
                ResolvedAt = x.ResolvedAt,
                AppUserResolvedId = x.AppUserResolvedId,
                IsClose = x.IsClose,
                IsOpen = x.IsOpen,
                IsWait = x.IsWait,
                IsWork = x.IsWork,
                SLAPolicyId = x.SLAPolicyId,
                HoldingTime = x.HoldingTime,
                FirstResponeTime = x.FirstResponeTime,
                ResolveTime = x.ResolveTime,
                FirstRespTimeRemaining = x.FirstRespTimeRemaining,
                ResolveTimeRemaining = x.ResolveTimeRemaining,
                SLAStatusId = x.SLAStatusId,
                ResolveMinute = x.ResolveMinute,
                SLAOverTime = x.SLAOverTime,
                SLAStatus = x.SLAStatus == null ? null : new SLAStatus
                {
                    Id = x.SLAStatus.Id,
                    Code = x.SLAStatus.Code,
                    Name = x.SLAStatus.Name,
                    ColorCode = x.SLAStatus.ColorCode,
                },
                AppUserClosed = x.AppUserClosed == null ? null : new AppUser
                {
                    Id = x.AppUserClosed.Id,
                    Username = x.AppUserClosed.Username,
                    DisplayName = x.AppUserClosed.DisplayName,
                    Address = x.AppUserClosed.Address,
                    Email = x.AppUserClosed.Email,
                    Phone = x.AppUserClosed.Phone,
                    SexId = x.AppUserClosed.SexId,
                    Birthday = x.AppUserClosed.Birthday,
                    Avatar = x.AppUserClosed.Avatar,
                    Department = x.AppUserClosed.Department,
                    OrganizationId = x.AppUserClosed.OrganizationId,
                    Longitude = x.AppUserClosed.Longitude,
                    Latitude = x.AppUserClosed.Latitude,
                    StatusId = x.AppUserClosed.StatusId,
                },
                AppUserResolved = x.AppUserResolved == null ? null : new AppUser
                {
                    Id = x.AppUserResolved.Id,
                    Username = x.AppUserResolved.Username,
                    DisplayName = x.AppUserResolved.DisplayName,
                    Address = x.AppUserResolved.Address,
                    Email = x.AppUserResolved.Email,
                    Phone = x.AppUserResolved.Phone,
                    SexId = x.AppUserResolved.SexId,
                    Birthday = x.AppUserResolved.Birthday,
                    Avatar = x.AppUserResolved.Avatar,
                    Department = x.AppUserResolved.Department,
                    OrganizationId = x.AppUserResolved.OrganizationId,
                    Longitude = x.AppUserResolved.Longitude,
                    Latitude = x.AppUserResolved.Latitude,
                    StatusId = x.AppUserResolved.StatusId,
                },
                SLAPolicy = x.SLAPolicy == null ? null : new SLAPolicy
                {
                    Id = x.SLAPolicy.Id,
                    TicketIssueLevelId = x.SLAPolicy.TicketIssueLevelId,
                    TicketPriorityId = x.SLAPolicy.TicketPriorityId,
                    FirstResponseTime = x.SLAPolicy.FirstResponseTime,
                    FirstResponseUnitId = x.SLAPolicy.FirstResponseUnitId,
                    ResolveTime = x.SLAPolicy.ResolveTime,
                    ResolveUnitId = x.SLAPolicy.ResolveUnitId,
                    IsAlert = x.SLAPolicy.IsAlert,
                    IsAlertFRT = x.SLAPolicy.IsAlertFRT,
                    IsEscalation = x.SLAPolicy.IsEscalation,
                    IsEscalationFRT = x.SLAPolicy.IsEscalationFRT,
                },
                Customer = x.Customer == null ? null : new Customer
                {
                    Id = x.Customer.Id,
                    Code = x.Customer.Code,
                    Name = x.Customer.Name,
                    Phone = x.Customer.Phone,
                    Email = x.Customer.Email,
                },
                CustomerType = x.CustomerType == null ? null : new CustomerType
                {
                    Id = x.CustomerType.Id,
                    Name = x.CustomerType.Name,
                    Code = x.CustomerType.Code,
                },
                Department = x.Department == null ? null : new Organization
                {
                    Id = x.Department.Id,
                    Name = x.Department.Name,
                },
                Product = x.Product == null ? null : new Product
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                },
                RelatedCallLog = x.RelatedCallLog == null ? null : new CallLog
                {
                    Id = x.RelatedCallLog.Id,
                    EntityReferenceId = x.RelatedCallLog.EntityReferenceId,
                    CallTypeId = x.RelatedCallLog.CallTypeId,
                    CallEmotionId = x.RelatedCallLog.CallEmotionId,
                    AppUserId = x.RelatedCallLog.AppUserId,
                    Title = x.RelatedCallLog.Title,
                    Content = x.RelatedCallLog.Content,
                    Phone = x.RelatedCallLog.Phone,
                    CallTime = x.RelatedCallLog.CallTime,
                },
                RelatedTicket = x.RelatedTicket == null ? null : new Ticket
                {
                    Id = x.RelatedTicket.Id,
                    Name = x.RelatedTicket.Name,
                    Phone = x.RelatedTicket.Phone,
                    CustomerId = x.RelatedTicket.CustomerId,
                    UserId = x.RelatedTicket.UserId,
                    ProductId = x.RelatedTicket.ProductId,
                    ReceiveDate = x.RelatedTicket.ReceiveDate,
                    ProcessDate = x.RelatedTicket.ProcessDate,
                    FinishDate = x.RelatedTicket.FinishDate,
                    Subject = x.RelatedTicket.Subject,
                    Content = x.RelatedTicket.Content,
                    TicketIssueLevelId = x.RelatedTicket.TicketIssueLevelId,
                    TicketPriorityId = x.RelatedTicket.TicketPriorityId,
                    TicketStatusId = x.RelatedTicket.TicketStatusId,
                    TicketSourceId = x.RelatedTicket.TicketSourceId,
                    TicketNumber = x.RelatedTicket.TicketNumber,
                    DepartmentId = x.RelatedTicket.DepartmentId,
                    RelatedTicketId = x.RelatedTicket.RelatedTicketId,
                    SLA = x.RelatedTicket.SLA,
                    RelatedCallLogId = x.RelatedTicket.RelatedCallLogId,
                    ResponseMethodId = x.RelatedTicket.ResponseMethodId,
                    StatusId = x.RelatedTicket.StatusId,
                    Used = x.RelatedTicket.Used,
                },
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
                TicketIssueLevel = x.TicketIssueLevel == null ? null : new TicketIssueLevel
                {
                    Id = x.TicketIssueLevel.Id,
                    Name = x.TicketIssueLevel.Name,
                    OrderNumber = x.TicketIssueLevel.OrderNumber,
                    TicketGroupId = x.TicketIssueLevel.TicketGroupId,
                    TicketGroup = x.TicketIssueLevel.TicketGroup == null ? null : new TicketGroup
                    {
                        Id = x.TicketIssueLevel.TicketGroup.Id,
                        Name = x.TicketIssueLevel.TicketGroup.Name,
                        TicketTypeId = x.TicketIssueLevel.TicketGroup.TicketTypeId,
                        TicketType = x.TicketIssueLevel.TicketGroup.TicketType == null ? null : new TicketType
                        {
                            Id = x.TicketIssueLevel.TicketGroup.TicketType.Id,
                            Name = x.TicketIssueLevel.TicketGroup.TicketType.Name,
                        },
                    },
                    StatusId = x.TicketIssueLevel.StatusId,
                    SLA = x.TicketIssueLevel.SLA,
                    Used = x.TicketIssueLevel.Used,
                },
                TicketPriority = x.TicketPriority == null ? null : new TicketPriority
                {
                    Id = x.TicketPriority.Id,
                    Name = x.TicketPriority.Name,
                    OrderNumber = x.TicketPriority.OrderNumber,
                    ColorCode = x.TicketPriority.ColorCode,
                    StatusId = x.TicketPriority.StatusId,
                    Used = x.TicketPriority.Used,
                },
                TicketSource = x.TicketSource == null ? null : new TicketSource
                {
                    Id = x.TicketSource.Id,
                    Name = x.TicketSource.Name,
                    OrderNumber = x.TicketSource.OrderNumber,
                    StatusId = x.TicketSource.StatusId,
                    Used = x.TicketSource.Used,
                },
                TicketStatus = x.TicketStatus == null ? null : new TicketStatus
                {
                    Id = x.TicketStatus.Id,
                    Name = x.TicketStatus.Name,
                    OrderNumber = x.TicketStatus.OrderNumber,
                    ColorCode = x.TicketStatus.ColorCode,
                    StatusId = x.TicketStatus.StatusId,
                    Used = x.TicketStatus.Used,
                },
                User = x.User == null ? null : new AppUser
                {
                    Id = x.User.Id,
                    Username = x.User.Username,
                    DisplayName = x.User.DisplayName
                },
                Creator = x.Creator == null ? null : new AppUser
                {
                    Id = x.Creator.Id,
                    Username = x.Creator.Username,
                    DisplayName = x.Creator.DisplayName
                },
                TicketResolveTypeId = x.TicketResolveTypeId,
                ResolveContent = x.ResolveContent,
                TicketResolveType = x.TicketResolveType == null ? null : new TicketResolveType
                {
                    Id = x.TicketResolveType.Id,
                    Code = x.TicketResolveType.Code,
                    Name = x.TicketResolveType.Name,
                },
            }).FirstOrDefaultAsync();

            if (Ticket == null)
                return null;

            return Ticket;
        }
        public async Task<bool> Create(Ticket Ticket)
        {
            TicketDAO TicketDAO = new TicketDAO();
            TicketDAO.Id = Ticket.Id;
            TicketDAO.Name = Ticket.Name;
            TicketDAO.Phone = Ticket.Phone;
            TicketDAO.CustomerId = Ticket.CustomerId;
            TicketDAO.UserId = Ticket.UserId;
            TicketDAO.CustomerTypeId = Ticket.CustomerTypeId;
            TicketDAO.CreatorId = Ticket.CreatorId;
            TicketDAO.ProductId = Ticket.ProductId;
            TicketDAO.ReceiveDate = StaticParams.DateTimeNow;
            TicketDAO.ProcessDate = Ticket.ProcessDate;
            TicketDAO.FinishDate = Ticket.FinishDate;
            TicketDAO.Subject = Ticket.Subject;
            TicketDAO.Content = Ticket.Content;
            TicketDAO.TicketIssueLevelId = Ticket.TicketIssueLevelId;
            TicketDAO.TicketPriorityId = Ticket.TicketPriorityId;
            TicketDAO.TicketStatusId = Ticket.TicketStatusId;
            TicketDAO.TicketSourceId = Ticket.TicketSourceId;
            TicketDAO.TicketNumber = Ticket.TicketNumber;
            TicketDAO.DepartmentId = Ticket.DepartmentId;
            TicketDAO.RelatedTicketId = Ticket.RelatedTicketId;
            TicketDAO.SLA = Ticket.SLA;
            TicketDAO.RelatedCallLogId = Ticket.RelatedCallLogId;
            TicketDAO.ResponseMethodId = Ticket.ResponseMethodId;
            TicketDAO.StatusId = Ticket.StatusId;
            TicketDAO.IsAlerted = Ticket.IsAlerted;
            TicketDAO.IsAlertedFRT = Ticket.IsAlertedFRT;
            TicketDAO.IsEscalated = Ticket.IsEscalated;
            TicketDAO.IsEscalatedFRT = Ticket.IsEscalatedFRT;
            TicketDAO.Used = Ticket.Used;
            TicketDAO.TicketResolveTypeId = Ticket.TicketResolveTypeId;
            TicketDAO.ResolveContent = Ticket.ResolveContent;
            TicketDAO.closedAt = Ticket.closedAt;
            TicketDAO.AppUserClosedId = Ticket.AppUserClosedId;
            TicketDAO.FirstResponseAt = Ticket.FirstResponseAt;
            TicketDAO.LastResponseAt = Ticket.LastResponseAt;
            TicketDAO.LastHoldingAt = Ticket.LastHoldingAt;
            TicketDAO.ReraisedTimes = Ticket.ReraisedTimes;
            TicketDAO.ResolvedAt = Ticket.ResolvedAt;
            TicketDAO.AppUserResolvedId = Ticket.AppUserResolvedId;
            TicketDAO.IsClose = Ticket.IsClose;
            TicketDAO.IsOpen = Ticket.IsOpen;
            TicketDAO.IsWait = Ticket.IsWait;
            TicketDAO.IsWork = Ticket.IsWork;
            TicketDAO.SLAPolicyId = Ticket.SLAPolicyId;
            TicketDAO.HoldingTime = Ticket.HoldingTime;
            TicketDAO.FirstResponeTime = Ticket.FirstResponeTime;
            TicketDAO.ResolveTime = Ticket.ResolveTime;
            TicketDAO.FirstRespTimeRemaining = Ticket.FirstRespTimeRemaining;
            TicketDAO.ResolveTimeRemaining = Ticket.ResolveTimeRemaining;
            TicketDAO.SLAStatusId = Ticket.SLAStatusId;
            TicketDAO.ResolveMinute = Ticket.ResolveMinute;
            TicketDAO.SLAOverTime = Ticket.SLAOverTime;
            TicketDAO.CreatedAt = StaticParams.DateTimeNow;
            TicketDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.Ticket.Add(TicketDAO);
            await DataContext.SaveChangesAsync();
            Ticket.Id = TicketDAO.Id;
            await SaveReference(Ticket);
            return true;
        }

        public async Task<bool> Update(Ticket Ticket)
        {
            TicketDAO TicketDAO = DataContext.Ticket.Where(x => x.Id == Ticket.Id).FirstOrDefault();
            if (TicketDAO == null)
                return false;
            TicketDAO.Name = Ticket.Name;
            TicketDAO.ReceiveDate = Ticket.ReceiveDate;
            TicketDAO.ProcessDate = Ticket.ProcessDate;
            TicketDAO.FinishDate = Ticket.FinishDate;
            TicketDAO.TicketPriorityId = Ticket.TicketPriorityId;
            TicketDAO.TicketStatusId = Ticket.TicketStatusId;
            TicketDAO.RelatedTicketId = Ticket.RelatedTicketId;
            TicketDAO.UserId = Ticket.UserId;
            TicketDAO.IsAlerted = Ticket.IsAlerted;
            TicketDAO.IsAlertedFRT = Ticket.IsAlertedFRT;
            TicketDAO.IsEscalated = Ticket.IsEscalated;
            TicketDAO.IsEscalatedFRT = Ticket.IsEscalatedFRT;
            TicketDAO.Used = Ticket.Used;
            TicketDAO.TicketResolveTypeId = Ticket.TicketResolveTypeId;
            TicketDAO.ResolveContent = Ticket.ResolveContent;
            TicketDAO.closedAt = Ticket.closedAt;
            TicketDAO.AppUserClosedId = Ticket.AppUserClosedId;
            TicketDAO.FirstResponseAt = Ticket.FirstResponseAt;
            TicketDAO.LastResponseAt = Ticket.LastResponseAt;
            TicketDAO.LastHoldingAt = Ticket.LastHoldingAt;
            TicketDAO.ReraisedTimes = Ticket.ReraisedTimes;
            TicketDAO.ResolvedAt = Ticket.ResolvedAt;
            TicketDAO.AppUserResolvedId = Ticket.AppUserResolvedId;
            TicketDAO.IsClose = Ticket.IsClose;
            TicketDAO.IsOpen = Ticket.IsOpen;
            TicketDAO.IsWait = Ticket.IsWait;
            TicketDAO.IsWork = Ticket.IsWork;
            TicketDAO.SLAPolicyId = Ticket.SLAPolicyId;
            TicketDAO.HoldingTime = Ticket.HoldingTime;
            TicketDAO.FirstResponeTime = Ticket.FirstResponeTime;
            TicketDAO.ResolveTime = Ticket.ResolveTime;
            TicketDAO.FirstRespTimeRemaining = Ticket.FirstRespTimeRemaining;
            TicketDAO.ResolveTimeRemaining = Ticket.ResolveTimeRemaining;
            TicketDAO.SLAStatusId = Ticket.SLAStatusId;
            TicketDAO.ResolveMinute = Ticket.ResolveMinute;
            TicketDAO.SLAOverTime = Ticket.SLAOverTime;
            TicketDAO.DepartmentId = Ticket.DepartmentId;
            TicketDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(Ticket);
            return true;
        }

        public async Task<bool> Delete(Ticket Ticket)
        {
            await DataContext.Ticket.Where(x => x.Id == Ticket.Id).UpdateFromQueryAsync(x => new TicketDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<Ticket> Tickets)
        {
            List<TicketDAO> TicketDAOs = new List<TicketDAO>();
            foreach (Ticket Ticket in Tickets)
            {
                TicketDAO TicketDAO = new TicketDAO();
                TicketDAO.Id = Ticket.Id;
                TicketDAO.Name = Ticket.Name;
                TicketDAO.Phone = Ticket.Phone;
                TicketDAO.CustomerId = Ticket.CustomerId;
                TicketDAO.UserId = Ticket.UserId;
                TicketDAO.CustomerTypeId = Ticket.CustomerTypeId;
                TicketDAO.ProductId = Ticket.ProductId;
                TicketDAO.ReceiveDate = Ticket.ReceiveDate;
                TicketDAO.ProcessDate = Ticket.ProcessDate;
                TicketDAO.FinishDate = Ticket.FinishDate;
                TicketDAO.Subject = Ticket.Subject;
                TicketDAO.Content = Ticket.Content;
                TicketDAO.TicketIssueLevelId = Ticket.TicketIssueLevelId;
                TicketDAO.TicketPriorityId = Ticket.TicketPriorityId;
                TicketDAO.TicketStatusId = Ticket.TicketStatusId;
                TicketDAO.TicketSourceId = Ticket.TicketSourceId;
                TicketDAO.TicketNumber = Ticket.TicketNumber;
                TicketDAO.DepartmentId = Ticket.DepartmentId;
                TicketDAO.RelatedTicketId = Ticket.RelatedTicketId;
                TicketDAO.SLA = Ticket.SLA;
                TicketDAO.RelatedCallLogId = Ticket.RelatedCallLogId;
                TicketDAO.ResponseMethodId = Ticket.ResponseMethodId;
                TicketDAO.StatusId = Ticket.StatusId;
                TicketDAO.IsAlerted = Ticket.IsAlerted;
                TicketDAO.IsAlertedFRT = Ticket.IsAlertedFRT;
                TicketDAO.IsEscalated = Ticket.IsEscalated;
                TicketDAO.IsEscalatedFRT = Ticket.IsEscalatedFRT;
                TicketDAO.Used = Ticket.Used;
                TicketDAO.TicketResolveTypeId = Ticket.TicketResolveTypeId;
                TicketDAO.ResolveContent = Ticket.ResolveContent;
                TicketDAO.closedAt = Ticket.closedAt;
                TicketDAO.AppUserClosedId = Ticket.AppUserClosedId;
                TicketDAO.FirstResponseAt = Ticket.FirstResponseAt;
                TicketDAO.LastResponseAt = Ticket.LastResponseAt;
                TicketDAO.LastHoldingAt = Ticket.LastHoldingAt;
                TicketDAO.ReraisedTimes = Ticket.ReraisedTimes;
                TicketDAO.ResolvedAt = Ticket.ResolvedAt;
                TicketDAO.AppUserResolvedId = Ticket.AppUserResolvedId;
                TicketDAO.IsClose = Ticket.IsClose;
                TicketDAO.IsOpen = Ticket.IsOpen;
                TicketDAO.IsWait = Ticket.IsWait;
                TicketDAO.IsWork = Ticket.IsWork;
                TicketDAO.SLAPolicyId = Ticket.SLAPolicyId;
                TicketDAO.HoldingTime = Ticket.HoldingTime;
                TicketDAO.FirstResponeTime = Ticket.FirstResponeTime;
                TicketDAO.ResolveTime = Ticket.ResolveTime;
                TicketDAO.FirstRespTimeRemaining = Ticket.FirstRespTimeRemaining;
                TicketDAO.ResolveTimeRemaining = Ticket.ResolveTimeRemaining;
                TicketDAO.SLAStatusId = Ticket.SLAStatusId;
                TicketDAO.ResolveMinute = Ticket.ResolveMinute;
                TicketDAO.SLAOverTime = Ticket.SLAOverTime;
                TicketDAO.CreatedAt = StaticParams.DateTimeNow;
                TicketDAO.UpdatedAt = StaticParams.DateTimeNow;
                TicketDAOs.Add(TicketDAO);
            }
            await DataContext.BulkMergeAsync(TicketDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<Ticket> Tickets)
        {
            List<long> Ids = Tickets.Select(x => x.Id).ToList();
            await DataContext.Ticket
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new TicketDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(Ticket Ticket)
        {
            List<TicketOfUserDAO> TicketOfUserDAOs = await DataContext.TicketOfUser
                .Where(x => x.TicketId == Ticket.Id).ToListAsync();
            if (Ticket.TicketOfUsers != null)
            {
                foreach (TicketOfUser TicketOfUser in Ticket.TicketOfUsers)
                {
                    TicketOfUserDAO TicketOfUserDAO = TicketOfUserDAOs
                        .Where(x => x.Id == TicketOfUser.Id && x.Id != 0).FirstOrDefault();
                    if (TicketOfUserDAO == null)
                    {

                        TicketOfUserDAO = new TicketOfUserDAO();
                        TicketOfUserDAO.Id = TicketOfUser.Id;
                        TicketOfUserDAO.TicketId = Ticket.Id;
                        TicketOfUserDAO.TicketStatusId = TicketOfUser.TicketStatusId;
                        TicketOfUserDAO.UserId = TicketOfUser.UserId;
                        TicketOfUserDAO.Notes = TicketOfUser.Notes;
                        TicketOfUserDAOs.Add(TicketOfUserDAO);
                        TicketOfUserDAO.CreatedAt = StaticParams.DateTimeNow;
                        TicketOfUserDAO.UpdatedAt = StaticParams.DateTimeNow;
                        TicketOfUserDAO.DeletedAt = null;

                    }
                }
                await DataContext.TicketOfUser.BulkMergeAsync(TicketOfUserDAOs);
            }
        }

    }
}
