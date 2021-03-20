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
using CRM.Enums;
using CRM.Services.MOrganization;
using CRM.Services.MFile;

namespace CRM.Services.MCompany
{
    public interface ICompanyService :  IServiceScoped
    {
        Task<int> Count(CompanyFilter CompanyFilter);
        Task<List<Company>> List(CompanyFilter CompanyFilter);
        Task<Company> Get(long Id);
        Task<Company> Create(Company Company);
        Task<Company> Update(Company Company);
        Task<Company> Delete(Company Company);
        Task<List<Company>> BulkDelete(List<Company> Companys);
        Task<List<Company>> Import(List<Company> Companys);
        Task<Entities.File> UploadFile(Entities.File File);
        Task<CompanyFilter> ToFilter(CompanyFilter CompanyFilter); 
    }

    public class CompanyService : BaseService, ICompanyService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICompanyValidator CompanyValidator;
        private IOrganizationService OrganizationService;
        private IFileService FileService;

        public CompanyService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IOrganizationService OrganizationService,
            ICompanyValidator CompanyValidator,
            IFileService FileService
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.OrganizationService = OrganizationService;
            this.CompanyValidator = CompanyValidator;
            this.FileService = FileService;
        }
        public async Task<int> Count(CompanyFilter CompanyFilter)
        {
            try
            {
                int result = await UOW.CompanyRepository.Count(CompanyFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CompanyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CompanyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Company>> List(CompanyFilter CompanyFilter)
        {
            try
            {
                List<Company> Companys = await UOW.CompanyRepository.List(CompanyFilter);
                return Companys;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CompanyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CompanyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<Company> Get(long Id)
        {
            Company Company = await UOW.CompanyRepository.Get(Id);
            if (Company == null)
                return null;
            return Company;
        }
       
        public async Task<Company> Create(Company Company)
        {
            if (!await CompanyValidator.Create(Company))
                return Company;

            try
            {
                await UOW.Begin();
                var Creator = await UOW.AppUserRepository.Get(CurrentContext.UserId);
                Company.CreatorId = Creator.Id;
                Company.OrganizationId = Creator.OrganizationId;
                Company.CompanyStatusId = CompanyStatusEnum.NEW.Id;
                await UOW.CompanyRepository.Create(Company);
                await UOW.CompanyRepository.Update(Company);
                await UOW.Commit();
                Company = await UOW.CompanyRepository.Get(Company.Id);
                await Logging.CreateAuditLog(Company, new { }, nameof(CompanyService));
                return Company;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CompanyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CompanyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Company> Update(Company Company)
        {
            if (!await CompanyValidator.Update(Company))
                return Company;
            try
            {
                var oldData = await UOW.CompanyRepository.Get(Company.Id);

                await UOW.Begin();
                await UOW.CompanyRepository.Update(Company);
                await UOW.Commit();

                Company = await UOW.CompanyRepository.Get(Company.Id);
                await Logging.CreateAuditLog(Company, oldData, nameof(CompanyService));
                return Company;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CompanyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CompanyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Company> Delete(Company Company)
        {
            if (!await CompanyValidator.Delete(Company))
                return Company;

            try
            {
                await UOW.Begin();
                await UOW.CompanyRepository.Delete(Company);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, Company, nameof(CompanyService));
                return Company;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CompanyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CompanyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Company>> BulkDelete(List<Company> Companys)
        {
            if (!await CompanyValidator.BulkDelete(Companys))
                return Companys;

            try
            {
                await UOW.Begin();
                await UOW.CompanyRepository.BulkDelete(Companys);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, Companys, nameof(CompanyService));
                return Companys;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CompanyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CompanyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<Company>> Import(List<Company> Companys)
        {
            if (!await CompanyValidator.Import(Companys))
                return Companys;
            try
            {
                await UOW.Begin();
                await UOW.CompanyRepository.BulkMerge(Companys);
                await UOW.Commit();

                await Logging.CreateAuditLog(Companys, new { }, nameof(CompanyService));
                return Companys;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CompanyService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CompanyService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Entities.File> UploadFile(Entities.File File)
        {
            FileInfo fileInfo = new FileInfo(File.Name);
            string path = $"/company/{StaticParams.DateTimeNow.ToString("yyyyMMdd")}/{Guid.NewGuid()}{fileInfo.Extension}";
            string thumbnailPath = $"/company/{StaticParams.DateTimeNow.ToString("yyyyMMdd")}/{Guid.NewGuid()}{fileInfo.Extension}";
            File = await FileService.Create(File, path);
            return File;
        }

        public async Task<CompanyFilter> ToFilter(CompanyFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CompanyFilter>();
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
                CompanyFilter subFilter = new CompanyFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterBuilder.Merge(subFilter.AppUserId, FilterPermissionDefinition.IdFilter);
                    //if (FilterPermissionDefinition.Name == nameof(subFilter.OrganizationId))
                    //{
                    //    var organizationIds = FilterOrganization(Organizations, FilterPermissionDefinition.IdFilter);
                    //    IdFilter IdFilter = new IdFilter { In = organizationIds };
                    //    subFilter.OrganizationId = FilterBuilder.Merge(subFilter.OrganizationId, IdFilter);
                    //}
                    //if (FilterPermissionDefinition.Name == nameof(CurrentContext.UserId) && FilterPermissionDefinition.IdFilter != null)
                    //{
                    //    if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.IS.Id)
                    //    {
                    //        if (subFilter.UserId == null) subFilter.UserId = new IdFilter { };
                    //        subFilter.UserId.Equal = CurrentContext.UserId;
                    //    }
                    //    if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.ISNT.Id)
                    //    {
                    //        if (subFilter.UserId == null) subFilter.UserId = new IdFilter { };
                    //        subFilter.UserId.NotEqual = CurrentContext.UserId;
                    //    }
                    //}
                }
            }
            return filter;
        }
    }
}
