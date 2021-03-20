using CRM.Common;
using CRM.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using CRM.Repositories;
using CRM.Entities;
using CRM.Services.MOrganization;
using CRM.Enums;

namespace CRM.Services.MKnowledgeArticle
{
    public interface IKnowledgeArticleService : IServiceScoped
    {
        Task<int> Count(KnowledgeArticleFilter KnowledgeArticleFilter);
        Task<List<KnowledgeArticle>> List(KnowledgeArticleFilter KnowledgeArticleFilter);
        Task<KnowledgeArticle> Get(long Id);
        Task<KnowledgeArticle> Create(KnowledgeArticle KnowledgeArticle);
        Task<KnowledgeArticle> Update(KnowledgeArticle KnowledgeArticle);
        Task<KnowledgeArticle> Delete(KnowledgeArticle KnowledgeArticle);
        Task<List<KnowledgeArticle>> BulkDelete(List<KnowledgeArticle> KnowledgeArticles);
        Task<List<KnowledgeArticle>> Import(List<KnowledgeArticle> KnowledgeArticles);
        Task<KnowledgeArticleFilter> ToFilter(KnowledgeArticleFilter KnowledgeArticleFilter);
    }

    public class KnowledgeArticleService : BaseService, IKnowledgeArticleService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IKnowledgeArticleValidator KnowledgeArticleValidator;
        private IOrganizationService OrganizationService;

        public KnowledgeArticleService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IOrganizationService OrganizationService,
            IKnowledgeArticleValidator KnowledgeArticleValidator
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.KnowledgeArticleValidator = KnowledgeArticleValidator;
            this.OrganizationService = OrganizationService;
        }
        public async Task<int> Count(KnowledgeArticleFilter KnowledgeArticleFilter)
        {
            try
            {
                int result = await UOW.KnowledgeArticleRepository.Count(KnowledgeArticleFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(KnowledgeArticleService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(KnowledgeArticleService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<KnowledgeArticle>> List(KnowledgeArticleFilter KnowledgeArticleFilter)
        {
            try
            {
                List<KnowledgeArticle> KnowledgeArticles = await UOW.KnowledgeArticleRepository.List(KnowledgeArticleFilter);
                foreach (var KnowledgeArticle in KnowledgeArticles)
                {
                    var Now = StaticParams.DateTimeNow;
                    if (Now >= KnowledgeArticle.FromDate && (KnowledgeArticle.ToDate.HasValue == false || KnowledgeArticle.ToDate >= Now))
                    {
                        KnowledgeArticle.KMSStatusId = KMSStatusEnum.DOING.Id;
                        KnowledgeArticle.KMSStatus = new KMSStatus
                        {
                            Id = KMSStatusEnum.DOING.Id,
                            Code = KMSStatusEnum.DOING.Code,
                            Name = KMSStatusEnum.DOING.Name,
                        };
                    }
                    else if (KnowledgeArticle.ToDate < Now)
                    {
                        KnowledgeArticle.KMSStatusId = KMSStatusEnum.EXPIRED.Id;
                        KnowledgeArticle.KMSStatus = new KMSStatus
                        {
                            Id = KMSStatusEnum.EXPIRED.Id,
                            Code = KMSStatusEnum.EXPIRED.Code,
                            Name = KMSStatusEnum.EXPIRED.Name,
                        };
                    }
                }
                return KnowledgeArticles;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(KnowledgeArticleService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(KnowledgeArticleService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<KnowledgeArticle> Get(long Id)
        {
            KnowledgeArticle KnowledgeArticle = await UOW.KnowledgeArticleRepository.Get(Id);
            if (KnowledgeArticle == null)
                return null;

            var Now = StaticParams.DateTimeNow;
            if (Now >= KnowledgeArticle.FromDate && (KnowledgeArticle.ToDate.HasValue == false || KnowledgeArticle.ToDate >= Now))
            {
                KnowledgeArticle.KMSStatusId = KMSStatusEnum.DOING.Id;
                KnowledgeArticle.KMSStatus = new KMSStatus
                {
                    Id = KMSStatusEnum.DOING.Id,
                    Code = KMSStatusEnum.DOING.Code,
                    Name = KMSStatusEnum.DOING.Name,
                };
            }
            else if (KnowledgeArticle.ToDate < Now)
            {
                KnowledgeArticle.KMSStatusId = KMSStatusEnum.EXPIRED.Id;
                KnowledgeArticle.KMSStatus = new KMSStatus
                {
                    Id = KMSStatusEnum.EXPIRED.Id,
                    Code = KMSStatusEnum.EXPIRED.Code,
                    Name = KMSStatusEnum.EXPIRED.Name,
                };
            }
            return KnowledgeArticle;
        }

        public async Task<KnowledgeArticle> Create(KnowledgeArticle KnowledgeArticle)
        {
            if (!await KnowledgeArticleValidator.Create(KnowledgeArticle))
                return KnowledgeArticle;

            try
            {
                KnowledgeArticle.KMSStatusId = KMSStatusEnum.NEW.Id;
                var Now = StaticParams.DateTimeNow;
                if(Now >= KnowledgeArticle.FromDate && (KnowledgeArticle.ToDate.HasValue == false || KnowledgeArticle.ToDate >= Now))
                {
                    KnowledgeArticle.KMSStatusId = KMSStatusEnum.DOING.Id;
                }
                else if(KnowledgeArticle.ToDate < Now)
                {
                    KnowledgeArticle.KMSStatusId = KMSStatusEnum.EXPIRED.Id;
                }

                await UOW.Begin();
                await UOW.KnowledgeArticleRepository.Create(KnowledgeArticle);
                await UOW.Commit();
                KnowledgeArticle = await UOW.KnowledgeArticleRepository.Get(KnowledgeArticle.Id);
                await Logging.CreateAuditLog(KnowledgeArticle, new { }, nameof(KnowledgeArticleService));
                return KnowledgeArticle;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(KnowledgeArticleService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(KnowledgeArticleService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<KnowledgeArticle> Update(KnowledgeArticle KnowledgeArticle)
        {
            if (!await KnowledgeArticleValidator.Update(KnowledgeArticle))
                return KnowledgeArticle;
            try
            {
                KnowledgeArticle.KMSStatusId = KMSStatusEnum.NEW.Id;
                var Now = StaticParams.DateTimeNow;
                if (Now >= KnowledgeArticle.FromDate && (KnowledgeArticle.ToDate.HasValue == false || KnowledgeArticle.ToDate >= Now))
                {
                    KnowledgeArticle.KMSStatusId = KMSStatusEnum.DOING.Id;
                }
                else if (KnowledgeArticle.ToDate < Now)
                {
                    KnowledgeArticle.KMSStatusId = KMSStatusEnum.EXPIRED.Id;
                }
                var oldData = await UOW.KnowledgeArticleRepository.Get(KnowledgeArticle.Id);

                await UOW.Begin();
                await UOW.KnowledgeArticleRepository.Update(KnowledgeArticle);
                await UOW.Commit();

                KnowledgeArticle = await UOW.KnowledgeArticleRepository.Get(KnowledgeArticle.Id);
                await Logging.CreateAuditLog(KnowledgeArticle, oldData, nameof(KnowledgeArticleService));
                return KnowledgeArticle;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(KnowledgeArticleService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(KnowledgeArticleService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<KnowledgeArticle> Delete(KnowledgeArticle KnowledgeArticle)
        {
            if (!await KnowledgeArticleValidator.Delete(KnowledgeArticle))
                return KnowledgeArticle;

            try
            {
                await UOW.Begin();
                await UOW.KnowledgeArticleRepository.Delete(KnowledgeArticle);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, KnowledgeArticle, nameof(KnowledgeArticleService));
                return KnowledgeArticle;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(KnowledgeArticleService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(KnowledgeArticleService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<KnowledgeArticle>> BulkDelete(List<KnowledgeArticle> KnowledgeArticles)
        {
            if (!await KnowledgeArticleValidator.BulkDelete(KnowledgeArticles))
                return KnowledgeArticles;

            try
            {
                await UOW.Begin();
                await UOW.KnowledgeArticleRepository.BulkDelete(KnowledgeArticles);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, KnowledgeArticles, nameof(KnowledgeArticleService));
                return KnowledgeArticles;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(KnowledgeArticleService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(KnowledgeArticleService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<KnowledgeArticle>> Import(List<KnowledgeArticle> KnowledgeArticles)
        {
            if (!await KnowledgeArticleValidator.Import(KnowledgeArticles))
                return KnowledgeArticles;
            try
            {
                await UOW.Begin();
                await UOW.KnowledgeArticleRepository.BulkMerge(KnowledgeArticles);
                await UOW.Commit();

                await Logging.CreateAuditLog(KnowledgeArticles, new { }, nameof(KnowledgeArticleService));
                return KnowledgeArticles;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(KnowledgeArticleService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(KnowledgeArticleService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<KnowledgeArticleFilter> ToFilter(KnowledgeArticleFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<KnowledgeArticleFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            List<Organization> Organizations = await OrganizationService.List(new OrganizationFilter
            {
                Skip = 0,
                Take = int.MaxValue,
                Selects = OrganizationSelect.ALL,
                OrderBy = OrganizationOrder.Id,
                OrderType = OrderType.ASC
            });
            foreach (var currentFilter in CurrentContext.Filters)
            {
                KnowledgeArticleFilter subFilter = new KnowledgeArticleFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterBuilder.Merge(subFilter.AppUserId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.OrganizationId))
                    {
                        var organizationIds = FilterOrganization(Organizations, FilterPermissionDefinition.IdFilter);
                        IdFilter IdFilter = new IdFilter { In = organizationIds };
                        subFilter.OrganizationId = FilterBuilder.Merge(subFilter.OrganizationId, IdFilter);
                    }
                    if (FilterPermissionDefinition.Name == nameof(CurrentContext.UserId) && FilterPermissionDefinition.IdFilter != null)
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.IS.Id)
                        {
                            if (subFilter.CreatorId == null) subFilter.CreatorId = new IdFilter { };
                            subFilter.CreatorId.Equal = CurrentContext.UserId;
                        }
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.ISNT.Id)
                        {
                            if (subFilter.CreatorId == null) subFilter.CreatorId = new IdFilter { };
                            subFilter.CreatorId.NotEqual = CurrentContext.UserId;
                        }
                    }
                    if (FilterPermissionDefinition.Name == nameof(subFilter.KnowledgeGroupId))
                        subFilter.GroupId = FilterBuilder.Merge(subFilter.KnowledgeGroupId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppliedDepartmentId))
                    {
                        if (subFilter.CurrentUserId == null) subFilter.CurrentUserId = new IdFilter { };
                        subFilter.CurrentUserId.Equal = CurrentContext.UserId;
                        subFilter.AppliedDepartmentId = FilterBuilder.Merge(subFilter.AppliedDepartmentId, FilterPermissionDefinition.IdFilter);
                    }


                }
            }
            return filter;
        }
    }
}
