using CRM.Common;
using CRM.Entities;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Helpers;

namespace CRM.Repositories
{
    public interface IKnowledgeArticleRepository
    {
        Task<int> Count(KnowledgeArticleFilter KnowledgeArticleFilter);
        Task<List<KnowledgeArticle>> List(KnowledgeArticleFilter KnowledgeArticleFilter);
        Task<KnowledgeArticle> Get(long Id);
        Task<bool> Create(KnowledgeArticle KnowledgeArticle);
        Task<bool> Update(KnowledgeArticle KnowledgeArticle);
        Task<bool> Delete(KnowledgeArticle KnowledgeArticle);
        Task<bool> BulkMerge(List<KnowledgeArticle> KnowledgeArticles);
        Task<bool> BulkDelete(List<KnowledgeArticle> KnowledgeArticles);
    }
    public class KnowledgeArticleRepository : IKnowledgeArticleRepository
    {
        private DataContext DataContext;
        public KnowledgeArticleRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }
        private IQueryable<KnowledgeArticleDAO> DynamicFilter(IQueryable<KnowledgeArticleDAO> query, KnowledgeArticleFilter filter)
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
            if (filter.Title != null)
                query = query.Where(q => q.Title, filter.Title);
            if (filter.Detail != null)
                query = query.Where(q => q.Detail, filter.Detail);
            if (filter.StatusId != null)
                query = query.Where(q => q.StatusId, filter.StatusId);
            if (filter.GroupId != null)
                query = query.Where(q => q.GroupId, filter.GroupId);
            if (filter.CreatorId != null)
                query = query.Where(q => q.CreatorId, filter.CreatorId);
            if (filter.DisplayOrder != null)
                query = query.Where(q => q.DisplayOrder, filter.DisplayOrder);
            if (filter.ItemId != null)
                query = query.Where(q => q.ItemId, filter.ItemId);
            if (filter.FromDate != null)
                query = query.Where(q => q.FromDate, filter.FromDate);
            if (filter.ToDate != null)
                query = query.Where(q => q.ToDate, filter.ToDate);
            if (filter.KMSStatusId != null)
                query = query.Where(q => q.KMSStatusId, filter.KMSStatusId);

            //Tìm kiếm theo Organization
            if (filter.OrganizationId != null)
            {
                if (filter.OrganizationId.Equal.HasValue)
                {
                    query = from q in query
                            join ar in DataContext.KnowledgeArticleOrganizationMapping on q.Id equals ar.KnowledgeArticleId
                            where ar.OrganizationId == filter.OrganizationId.Equal.Value
                            select q;
                }
            }
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<KnowledgeArticleDAO> OrFilter(IQueryable<KnowledgeArticleDAO> query, KnowledgeArticleFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<KnowledgeArticleDAO> initQuery = query.Where(q => false);
            foreach (KnowledgeArticleFilter KnowledgeArticleFilter in filter.OrFilter)
            {
                IQueryable<KnowledgeArticleDAO> queryable = query;
                if (KnowledgeArticleFilter.AppUserId != null)
                    queryable = queryable.Where(q => q.CreatorId, KnowledgeArticleFilter.AppUserId);
                //if (KnowledgeArticleFilter.OrganizationId != null)
                //{
                //    if (KnowledgeArticleFilter.OrganizationId.Equal != null)
                //    {
                //        OrganizationDAO OrganizationDAO = DataContext.Organization
                //            .Where(o => o.Id == KnowledgeArticleFilter.OrganizationId.Equal.Value).FirstOrDefault();
                //        queryable = queryable.Where(q => q.KnowledgeArticleOrganizationMappings.Any(p => p.Organization.Path.StartsWith(OrganizationDAO.Path)));
                //    }
                //    if (KnowledgeArticleFilter.OrganizationId.NotEqual != null)
                //    {
                //        OrganizationDAO OrganizationDAO = DataContext.Organization
                //            .Where(o => o.Id == KnowledgeArticleFilter.OrganizationId.NotEqual.Value).FirstOrDefault();
                //        queryable = queryable.Where(q => !q.KnowledgeArticleOrganizationMappings.Any(p => p.Organization.Path.StartsWith(OrganizationDAO.Path)));
                //    }
                //    if (KnowledgeArticleFilter.OrganizationId.In != null)
                //    {
                //        List<OrganizationDAO> OrganizationDAOs = DataContext.Organization
                //            .Where(o => o.DeletedAt == null && o.StatusId == 1).ToList();
                //        List<OrganizationDAO> Parents = OrganizationDAOs.Where(o => KnowledgeArticleFilter.OrganizationId.In.Contains(o.Id)).ToList();
                //        List<OrganizationDAO> Branches = OrganizationDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
                //        List<long> Ids = Branches.Select(o => o.Id).ToList();
                //        foreach (var id in Ids)
                //        {
                //            queryable = queryable.Where(q => q.KnowledgeArticleOrganizationMappings.Any(p => Ids.Contains(p.OrganizationId)));
                //        }
                //    }
                //    if (KnowledgeArticleFilter.OrganizationId.NotIn != null)
                //    {
                //        List<OrganizationDAO> OrganizationDAOs = DataContext.Organization
                //            .Where(o => o.DeletedAt == null && o.StatusId == 1).ToList();
                //        List<OrganizationDAO> Parents = OrganizationDAOs.Where(o => KnowledgeArticleFilter.OrganizationId.NotIn.Contains(o.Id)).ToList();
                //        List<OrganizationDAO> Branches = OrganizationDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
                //        List<long> Ids = Branches.Select(o => o.Id).ToList();
                //        foreach (var id in Ids)
                //        {
                //            queryable = queryable.Where(q => q.KnowledgeArticleOrganizationMappings.Any(p => !Ids.Contains(p.OrganizationId)));
                //        }
                //    }
                //}
                if (KnowledgeArticleFilter.OrganizationId != null)
                {
                    if (KnowledgeArticleFilter.OrganizationId.Equal != null)
                    {
                        OrganizationDAO OrganizationDAO = DataContext.Organization
                            .Where(o => o.Id == KnowledgeArticleFilter.OrganizationId.Equal.Value).FirstOrDefault();
                        queryable = queryable.Where(q => q.Creator.Organization.Path.StartsWith(OrganizationDAO.Path));
                    }
                    if (KnowledgeArticleFilter.OrganizationId.NotEqual != null)
                    {
                        OrganizationDAO OrganizationDAO = DataContext.Organization
                            .Where(o => o.Id == KnowledgeArticleFilter.OrganizationId.NotEqual.Value).FirstOrDefault();
                        queryable = queryable.Where(q => !q.Creator.Organization.Path.StartsWith(OrganizationDAO.Path));
                    }
                    if (KnowledgeArticleFilter.OrganizationId.In != null)
                    {
                        List<OrganizationDAO> OrganizationDAOs = DataContext.Organization
                            .Where(o => o.DeletedAt == null && o.StatusId == 1).ToList();
                        List<OrganizationDAO> Parents = OrganizationDAOs.Where(o => KnowledgeArticleFilter.OrganizationId.In.Contains(o.Id)).ToList();
                        List<OrganizationDAO> Branches = OrganizationDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
                        List<long> Ids = Branches.Select(o => o.Id).ToList();
                        queryable = queryable.Where(q => Ids.Contains(q.Creator.OrganizationId));
                    }
                    if (KnowledgeArticleFilter.OrganizationId.NotIn != null)
                    {
                        List<OrganizationDAO> OrganizationDAOs = DataContext.Organization
                            .Where(o => o.DeletedAt == null && o.StatusId == 1).ToList();
                        List<OrganizationDAO> Parents = OrganizationDAOs.Where(o => KnowledgeArticleFilter.OrganizationId.NotIn.Contains(o.Id)).ToList();
                        List<OrganizationDAO> Branches = OrganizationDAOs.Where(o => Parents.Any(p => o.Path.StartsWith(p.Path))).ToList();
                        List<long> Ids = Branches.Select(o => o.Id).ToList();
                        queryable = queryable.Where(q => !Ids.Contains(q.Creator.OrganizationId));
                    }
                }
                if (KnowledgeArticleFilter.UserId != null)
                {
                    if (KnowledgeArticleFilter.UserId.Equal != null)
                    {
                        AppUserDAO AppUserDAO = DataContext.AppUser.Where(p => p.Id == KnowledgeArticleFilter.UserId.Equal).FirstOrDefault();
                        OrganizationDAO OrganizationDAO = DataContext.Organization
                            .Where(o => o.Id == AppUserDAO.OrganizationId).FirstOrDefault();
                        queryable = queryable.Where(q => q.Creator.Organization.Path.StartsWith(OrganizationDAO.Path));
                    }
                    if (KnowledgeArticleFilter.UserId.NotEqual != null)
                    {
                        AppUserDAO AppUserDAO = DataContext.AppUser.Where(p => p.Id == KnowledgeArticleFilter.UserId.NotEqual).FirstOrDefault();
                        OrganizationDAO OrganizationDAO = DataContext.Organization
                            .Where(o => o.Id != AppUserDAO.OrganizationId).FirstOrDefault();
                        queryable = queryable.Where(q => !q.Creator.Organization.Path.StartsWith(OrganizationDAO.Path));
                    }
                }
                if (KnowledgeArticleFilter.KnowledgeGroupId != null)
                    queryable = queryable.Where(q => q.GroupId, KnowledgeArticleFilter.KnowledgeGroupId);

                // Hiển thị ra phòng ban của người dùng hiện tại
                if (KnowledgeArticleFilter.AppliedDepartmentId != null)
                {
                    if (KnowledgeArticleFilter.AppliedDepartmentId.Equal != null)
                    {
                        AppUserDAO AppUserDAO = DataContext.AppUser.Where(p => p.Id == KnowledgeArticleFilter.CurrentUserId.Equal).FirstOrDefault();
                        OrganizationDAO OrganizationDAO = DataContext.Organization
                                                            .Where(o => o.Id == AppUserDAO.OrganizationId).FirstOrDefault();
                        queryable = queryable.Where(q => q.KnowledgeArticleOrganizationMappings.Select(p => p.OrganizationId).Contains(OrganizationDAO.Id));
                    }
                    if (KnowledgeArticleFilter.AppliedDepartmentId.NotEqual != null)
                    {
                        AppUserDAO AppUserDAO = DataContext.AppUser.Where(p => p.Id == KnowledgeArticleFilter.CurrentUserId.Equal).FirstOrDefault();
                        OrganizationDAO OrganizationDAO = DataContext.Organization
                                                            .Where(o => o.Id != AppUserDAO.OrganizationId).FirstOrDefault();
                        queryable = queryable.Where(q => q.KnowledgeArticleOrganizationMappings.Select(p => p.OrganizationId).Contains(OrganizationDAO.Id));
                    }
                }
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<KnowledgeArticleDAO> DynamicOrder(IQueryable<KnowledgeArticleDAO> query, KnowledgeArticleFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case KnowledgeArticleOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case KnowledgeArticleOrder.Title:
                            query = query.OrderBy(q => q.Title);
                            break;
                        case KnowledgeArticleOrder.Detail:
                            query = query.OrderBy(q => q.Detail);
                            break;
                        case KnowledgeArticleOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case KnowledgeArticleOrder.Group:
                            query = query.OrderBy(q => q.GroupId);
                            break;
                        case KnowledgeArticleOrder.Creator:
                            query = query.OrderBy(q => q.CreatorId);
                            break;
                        case KnowledgeArticleOrder.DisplayOrder:
                            query = query.OrderBy(q => q.DisplayOrder);
                            break;
                        case KnowledgeArticleOrder.Item:
                            query = query.OrderBy(q => q.ItemId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case KnowledgeArticleOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case KnowledgeArticleOrder.Title:
                            query = query.OrderByDescending(q => q.Title);
                            break;
                        case KnowledgeArticleOrder.Detail:
                            query = query.OrderByDescending(q => q.Detail);
                            break;
                        case KnowledgeArticleOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case KnowledgeArticleOrder.Group:
                            query = query.OrderByDescending(q => q.GroupId);
                            break;
                        case KnowledgeArticleOrder.Creator:
                            query = query.OrderByDescending(q => q.CreatorId);
                            break;
                        case KnowledgeArticleOrder.DisplayOrder:
                            query = query.OrderByDescending(q => q.DisplayOrder);
                            break;
                        case KnowledgeArticleOrder.Item:
                            query = query.OrderByDescending(q => q.ItemId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<KnowledgeArticle>> DynamicSelect(IQueryable<KnowledgeArticleDAO> query, KnowledgeArticleFilter filter)
        {
            List<KnowledgeArticle> KnowledgeArticles = await query.Select(q => new KnowledgeArticle()
            {
                Id = filter.Selects.Contains(KnowledgeArticleSelect.Id) ? q.Id : default(long),
                Title = filter.Selects.Contains(KnowledgeArticleSelect.Title) ? q.Title : default(string),
                Detail = filter.Selects.Contains(KnowledgeArticleSelect.Detail) ? q.Detail : default(string),
                StatusId = filter.Selects.Contains(KnowledgeArticleSelect.Status) ? q.StatusId : default(long),
                GroupId = filter.Selects.Contains(KnowledgeArticleSelect.Group) ? q.GroupId : default(long),
                CreatorId = filter.Selects.Contains(KnowledgeArticleSelect.Creator) ? q.CreatorId : default(long),
                DisplayOrder = filter.Selects.Contains(KnowledgeArticleSelect.DisplayOrder) ? q.DisplayOrder : default(long),
                FromDate = filter.Selects.Contains(KnowledgeArticleSelect.FromDate) ? q.FromDate : default(DateTime?),
                ToDate = filter.Selects.Contains(KnowledgeArticleSelect.ToDate) ? q.ToDate : default(DateTime?),
                ItemId = filter.Selects.Contains(KnowledgeArticleSelect.Item) ? q.ItemId : default(long?),
                KMSStatusId = filter.Selects.Contains(KnowledgeArticleSelect.KMSStatus) ? q.KMSStatusId : default(long?),
                //OrganizationId = filter.Selects.Contains(KnowledgeArticleSelect.Organization) ? q.OrganizationId : default(long),
                Creator = filter.Selects.Contains(KnowledgeArticleSelect.Creator) && q.Creator != null ? new AppUser
                {
                    Id = q.Creator.Id,
                    Username = q.Creator.Username,
                    DisplayName = q.Creator.DisplayName,
                    Address = q.Creator.Address,
                    Email = q.Creator.Email,
                    Phone = q.Creator.Phone,
                    SexId = q.Creator.SexId,
                    Birthday = q.Creator.Birthday,
                    Avatar = q.Creator.Avatar,
                    Department = q.Creator.Department,
                    OrganizationId = q.Creator.OrganizationId,
                    Longitude = q.Creator.Longitude,
                    Latitude = q.Creator.Latitude,
                    StatusId = q.Creator.StatusId,
                } : null,
                Group = filter.Selects.Contains(KnowledgeArticleSelect.Group) && q.Group != null ? new KnowledgeGroup
                {
                    Id = q.Group.Id,
                    Name = q.Group.Name,
                    Code = q.Group.Code,
                    StatusId = q.Group.StatusId,
                    DisplayOrder = q.Group.DisplayOrder,
                    Description = q.Group.Description,
                } : null,
                //Organization = filter.Selects.Contains(KnowledgeArticleSelect.Organization) && q.Organization != null ? new Organization
                //{
                //    Id = q.Organization.Id,
                //    Code = q.Organization.Code,
                //    Name = q.Organization.Name,
                //    Address = q.Organization.Address,
                //    Phone = q.Organization.Phone,
                //    Path = q.Organization.Path,
                //    ParentId = q.Organization.ParentId,
                //    Email = q.Organization.Email,
                //    StatusId = q.Organization.StatusId,
                //    Level = q.Organization.Level
                //} : null,
                Status = filter.Selects.Contains(KnowledgeArticleSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                KMSStatus = filter.Selects.Contains(KnowledgeArticleSelect.KMSStatus) && q.KMSStatus != null ? new KMSStatus
                {
                    Id = q.KMSStatus.Id,
                    Code = q.KMSStatus.Code,
                    Name = q.KMSStatus.Name,
                } : null,
                Item = filter.Selects.Contains(KnowledgeArticleSelect.Item) && q.Status != null ? new Item
                {
                    Id = q.Item.Id,
                    Code = q.Item.Code,
                    Name = q.Item.Name,
                } : null,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
                KnowledgeArticleOrganizationMappings = q.KnowledgeArticleOrganizationMappings != null ? q.KnowledgeArticleOrganizationMappings.Select(p => new KnowledgeArticleOrganizationMapping
                {
                    KnowledgeArticleId = p.KnowledgeArticleId,
                    OrganizationId = p.OrganizationId,
                    Organization = p.Organization != null ? new Organization
                    {
                        Id = p.Organization.Id,
                        Code = p.Organization.Code,
                        Name = p.Organization.Name,
                        ParentId = p.Organization.ParentId,
                        Path = p.Organization.Path,
                        Level = p.Organization.Level,
                        StatusId = p.Organization.StatusId,
                        Phone = p.Organization.Phone,
                        Email = p.Organization.Email,
                        Address = p.Organization.Address,
                    } : null,
                }).ToList() : null,
                KnowledgeArticleKeywords = q.KnowledgeArticleKeywords != null ? q.KnowledgeArticleKeywords.Select(p => new KnowledgeArticleKeyword
                {
                    Id = p.Id,
                    KnowledgeArticleId = p.KnowledgeArticleId,
                    Name = p.Name,
                }).ToList() : null,
            }).ToListAsync();

            var Ids = KnowledgeArticles.Select(x => x.Id).ToList();
            var KnowledgeArticleOrganizationMappings = await DataContext.KnowledgeArticleOrganizationMapping
                .Where(x => Ids.Contains(x.KnowledgeArticleId))
                .Select(x => new KnowledgeArticleOrganizationMapping
                {
                    KnowledgeArticleId = x.KnowledgeArticleId,
                    OrganizationId = x.OrganizationId,
                    Organization = x.Organization != null ? new Organization
                    {
                        Id = x.Organization.Id,
                        Code = x.Organization.Code,
                        Name = x.Organization.Name,
                        ParentId = x.Organization.ParentId,
                        Path = x.Organization.Path,
                        Level = x.Organization.Level,
                        StatusId = x.Organization.StatusId,
                        Phone = x.Organization.Phone,
                        Email = x.Organization.Email,
                        Address = x.Organization.Address,
                    } : null,
                }).ToListAsync();

            var KnowledgeArticleKeywords = await DataContext.KnowledgeArticleKeyword
                .Where(x => Ids.Contains(x.KnowledgeArticleId))
                .Select(x => new KnowledgeArticleKeyword
                {
                    Id = x.Id,
                    KnowledgeArticleId = x.KnowledgeArticleId,
                    Name = x.Name,
                }).ToListAsync();

            foreach (var KnowledgeArticle in KnowledgeArticles)
            {
                KnowledgeArticle.KnowledgeArticleOrganizationMappings = KnowledgeArticleOrganizationMappings.Where(x => x.KnowledgeArticleId == KnowledgeArticle.Id).ToList();
                KnowledgeArticle.KnowledgeArticleKeywords = KnowledgeArticleKeywords.Where(x => x.KnowledgeArticleId == KnowledgeArticle.Id).ToList();
            }
            return KnowledgeArticles;
        }

        public async Task<int> Count(KnowledgeArticleFilter filter)
        {
            IQueryable<KnowledgeArticleDAO> KnowledgeArticles = DataContext.KnowledgeArticle.AsNoTracking();
            KnowledgeArticles = DynamicFilter(KnowledgeArticles, filter);
            return await KnowledgeArticles.CountAsync();
        }

        public async Task<List<KnowledgeArticle>> List(KnowledgeArticleFilter filter)
        {
            if (filter == null) return new List<KnowledgeArticle>();
            IQueryable<KnowledgeArticleDAO> KnowledgeArticleDAOs = DataContext.KnowledgeArticle.AsNoTracking();
            KnowledgeArticleDAOs = DynamicFilter(KnowledgeArticleDAOs, filter);
            KnowledgeArticleDAOs = DynamicOrder(KnowledgeArticleDAOs, filter);
            List<KnowledgeArticle> KnowledgeArticles = await DynamicSelect(KnowledgeArticleDAOs, filter);
            return KnowledgeArticles;
        }

        public async Task<KnowledgeArticle> Get(long Id)
        {
            KnowledgeArticle KnowledgeArticle = await DataContext.KnowledgeArticle.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new KnowledgeArticle()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Id = x.Id,
                Title = x.Title,
                Detail = x.Detail,
                StatusId = x.StatusId,
                GroupId = x.GroupId,
                CreatorId = x.CreatorId,
                DisplayOrder = x.DisplayOrder,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                ItemId = x.ItemId,
                KMSStatusId = x.KMSStatusId,
                //OrganizationId = x.OrganizationId,
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
                Group = x.Group == null ? null : new KnowledgeGroup
                {
                    Id = x.Group.Id,
                    Name = x.Group.Name,
                    Code = x.Group.Code,
                    StatusId = x.Group.StatusId,
                    DisplayOrder = x.Group.DisplayOrder,
                    Description = x.Group.Description,
                },
                //Organization = x.Organization == null ? null : new Organization
                //{
                //    Id = x.Organization.Id,
                //    Code = x.Organization.Code,
                //    Name = x.Organization.Name,
                //    Address = x.Organization.Address,
                //    Phone = x.Organization.Phone,
                //    Path = x.Organization.Path,
                //    ParentId = x.Organization.ParentId,
                //    Email = x.Organization.Email,
                //    StatusId = x.Organization.StatusId,
                //    Level = x.Organization.Level
                //},
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
                Item = x.Item == null ? null : new Item
                {
                    Id = x.Item.Id,
                    Code = x.Item.Code,
                    Name = x.Item.Name,
                },
                KMSStatus = x.KMSStatus == null ? null : new KMSStatus
                {
                    Id = x.KMSStatus.Id,
                    Code = x.KMSStatus.Code,
                    Name = x.KMSStatus.Name,
                },
                KnowledgeArticleOrganizationMappings = x.KnowledgeArticleOrganizationMappings != null ? x.KnowledgeArticleOrganizationMappings.Select(p => new KnowledgeArticleOrganizationMapping
                {
                    KnowledgeArticleId = p.KnowledgeArticleId,
                    OrganizationId = p.OrganizationId,
                    Organization = p.Organization != null ? new Organization
                    {
                        Id = p.Organization.Id,
                        Code = p.Organization.Code,
                        Name = p.Organization.Name,
                        ParentId = p.Organization.ParentId,
                        Path = p.Organization.Path,
                        Level = p.Organization.Level,
                        StatusId = p.Organization.StatusId,
                        Phone = p.Organization.Phone,
                        Email = p.Organization.Email,
                        Address = p.Organization.Address,
                    } : null,
                }).ToList() : null,
                KnowledgeArticleKeywords = x.KnowledgeArticleKeywords != null ? x.KnowledgeArticleKeywords.Select(p => new KnowledgeArticleKeyword
                {
                    Id = p.Id,
                    KnowledgeArticleId = p.KnowledgeArticleId,
                    Name = p.Name,
                }).ToList() : null,

            }).FirstOrDefaultAsync();

            if (KnowledgeArticle == null)
                return null;

            return KnowledgeArticle;
        }
        public async Task<bool> Create(KnowledgeArticle KnowledgeArticle)
        {
            KnowledgeArticleDAO KnowledgeArticleDAO = new KnowledgeArticleDAO();
            KnowledgeArticleDAO.Id = KnowledgeArticle.Id;
            KnowledgeArticleDAO.Title = KnowledgeArticle.Title;
            KnowledgeArticleDAO.Detail = KnowledgeArticle.Detail;
            KnowledgeArticleDAO.StatusId = KnowledgeArticle.StatusId;
            KnowledgeArticleDAO.GroupId = KnowledgeArticle.GroupId;
            KnowledgeArticleDAO.CreatorId = KnowledgeArticle.CreatorId;
            KnowledgeArticleDAO.DisplayOrder = KnowledgeArticle.DisplayOrder;
            KnowledgeArticleDAO.FromDate = KnowledgeArticle.FromDate;
            KnowledgeArticleDAO.ToDate = KnowledgeArticle.ToDate;
            KnowledgeArticleDAO.ItemId = KnowledgeArticle.ItemId == 0 ? null : KnowledgeArticle.ItemId;
            KnowledgeArticleDAO.KMSStatusId = KnowledgeArticle.KMSStatusId;
            //KnowledgeArticleDAO.OrganizationId = KnowledgeArticle.OrganizationId;
            KnowledgeArticleDAO.CreatedAt = StaticParams.DateTimeNow;
            KnowledgeArticleDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.KnowledgeArticle.Add(KnowledgeArticleDAO);
            await DataContext.SaveChangesAsync();
            KnowledgeArticle.Id = KnowledgeArticleDAO.Id;
            await SaveReference(KnowledgeArticle);
            return true;
        }

        public async Task<bool> Update(KnowledgeArticle KnowledgeArticle)
        {
            KnowledgeArticleDAO KnowledgeArticleDAO = DataContext.KnowledgeArticle.Where(x => x.Id == KnowledgeArticle.Id).FirstOrDefault();
            if (KnowledgeArticleDAO == null)
                return false;
            KnowledgeArticleDAO.Id = KnowledgeArticle.Id;
            KnowledgeArticleDAO.Title = KnowledgeArticle.Title;
            KnowledgeArticleDAO.Detail = KnowledgeArticle.Detail;
            KnowledgeArticleDAO.StatusId = KnowledgeArticle.StatusId;
            KnowledgeArticleDAO.GroupId = KnowledgeArticle.GroupId;
            KnowledgeArticleDAO.CreatorId = KnowledgeArticle.CreatorId;
            KnowledgeArticleDAO.DisplayOrder = KnowledgeArticle.DisplayOrder;
            KnowledgeArticleDAO.FromDate = KnowledgeArticle.FromDate;
            KnowledgeArticleDAO.ToDate = KnowledgeArticle.ToDate;
            KnowledgeArticleDAO.ItemId = KnowledgeArticle.ItemId == 0 ? null : KnowledgeArticle.ItemId;
            KnowledgeArticleDAO.KMSStatusId = KnowledgeArticle.KMSStatusId;
            KnowledgeArticleDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            await SaveReference(KnowledgeArticle);
            return true;
        }

        public async Task<bool> Delete(KnowledgeArticle KnowledgeArticle)
        {
            await DataContext.KnowledgeArticle.Where(x => x.Id == KnowledgeArticle.Id).UpdateFromQueryAsync(x => new KnowledgeArticleDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> BulkMerge(List<KnowledgeArticle> KnowledgeArticles)
        {

            List<KnowledgeArticleDAO> KnowledgeArticleDAOs = new List<KnowledgeArticleDAO>();
            foreach (KnowledgeArticle KnowledgeArticle in KnowledgeArticles)
            {
                KnowledgeArticleDAO KnowledgeArticleDAO = new KnowledgeArticleDAO();
                KnowledgeArticleDAO.Id = KnowledgeArticle.Id;
                KnowledgeArticleDAO.Title = KnowledgeArticle.Title;
                KnowledgeArticleDAO.Detail = KnowledgeArticle.Detail;
                KnowledgeArticleDAO.StatusId = KnowledgeArticle.StatusId;
                KnowledgeArticleDAO.GroupId = KnowledgeArticle.GroupId;
                KnowledgeArticleDAO.CreatorId = KnowledgeArticle.CreatorId;
                KnowledgeArticleDAO.DisplayOrder = KnowledgeArticle.DisplayOrder;
                KnowledgeArticleDAO.FromDate = KnowledgeArticle.FromDate;
                KnowledgeArticleDAO.ToDate = KnowledgeArticle.ToDate;
                KnowledgeArticleDAO.ItemId = KnowledgeArticle.ItemId;
                KnowledgeArticleDAO.KMSStatusId = KnowledgeArticle.KMSStatusId;
                //KnowledgeArticleDAO.OrganizationId = KnowledgeArticle.OrganizationId;
                KnowledgeArticleDAO.CreatedAt = StaticParams.DateTimeNow;
                KnowledgeArticleDAO.UpdatedAt = StaticParams.DateTimeNow;
                KnowledgeArticleDAOs.Add(KnowledgeArticleDAO);
            }
            await DataContext.BulkMergeAsync(KnowledgeArticleDAOs);
            return true;
        }

        public async Task<bool> BulkDelete(List<KnowledgeArticle> KnowledgeArticles)
        {
            List<long> Ids = KnowledgeArticles.Select(x => x.Id).ToList();
            await DataContext.KnowledgeArticle
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new KnowledgeArticleDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        private async Task SaveReference(KnowledgeArticle KnowledgeArticle)
        {
            #region Gán Organization cho knowledgeArticle
            if (KnowledgeArticle.KnowledgeArticleOrganizationMappings != null)
            {
                await DataContext.KnowledgeArticleOrganizationMapping.Where(p => p.KnowledgeArticleId == KnowledgeArticle.Id).DeleteFromQueryAsync();
                List<KnowledgeArticleOrganizationMappingDAO> KnowledgeArticleOrganizationMappings = new List<KnowledgeArticleOrganizationMappingDAO>();
                foreach (var item in KnowledgeArticle.KnowledgeArticleOrganizationMappings)
                {
                    KnowledgeArticleOrganizationMappingDAO KnowledgeArticleOrganizationMappingDAO = new KnowledgeArticleOrganizationMappingDAO();
                    KnowledgeArticleOrganizationMappingDAO.KnowledgeArticleId = KnowledgeArticle.Id;
                    KnowledgeArticleOrganizationMappingDAO.OrganizationId = item.OrganizationId;
                    KnowledgeArticleOrganizationMappings.Add(KnowledgeArticleOrganizationMappingDAO);
                }
                await DataContext.KnowledgeArticleOrganizationMapping.BulkMergeAsync(KnowledgeArticleOrganizationMappings);
            }
            #endregion

            #region Gán keyword
            if (KnowledgeArticle.KnowledgeArticleKeywords != null)
            {
                await DataContext.KnowledgeArticleKeyword.Where(p => p.KnowledgeArticleId == KnowledgeArticle.Id).DeleteFromQueryAsync();
                List<KnowledgeArticleKeywordDAO> KnowledgeArticleKeywordDAOs = KnowledgeArticle.KnowledgeArticleKeywords.Select(p => new KnowledgeArticleKeywordDAO
                {
                    KnowledgeArticleId = KnowledgeArticle.Id,
                    Name = p.Name
                }).ToList();
                await DataContext.KnowledgeArticleKeyword.BulkMergeAsync(KnowledgeArticleKeywordDAOs);
            }
            #endregion
        }

    }
}
