using CRM.Common;
using CRM.Entities;
using CRM.Enums;
using CRM.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Services.MAppUser
{
    public interface IAppUserValidator : IServiceScoped
    {
        Task<bool> Update(AppUser AppUser);
    }

    public class AppUserValidator : IAppUserValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            ERouteScopeNotExisted
        }

        private IUOW UOW;
        private ICurrentContext CurrentContext;

        public AppUserValidator(IUOW UOW, ICurrentContext CurrentContext)
        {
            this.UOW = UOW;
            this.CurrentContext = CurrentContext;
        }

        private async Task<bool> ValidateId(AppUser AppUser)
        {
            AppUserFilter AppUserFilter = new AppUserFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = AppUser.Id },
                Selects = AppUserSelect.Id
            };

            int count = await UOW.AppUserRepository.Count(AppUserFilter);
            if (count == 0)
                AppUser.AddError(nameof(AppUserValidator), nameof(AppUser.Id), ErrorCode.IdNotExisted);
            return AppUser.IsValidated;
        }

        public async Task<bool> ValidateOrganization(AppUser AppUser)
        {
            if (AppUser.ERouteScopeId.HasValue)
            {
                OrganizationFilter OrganizationFilter = new OrganizationFilter
                {
                    Id = new IdFilter { Equal = AppUser.ERouteScopeId }
                };

                var count = await UOW.OrganizationRepository.Count(OrganizationFilter);
                if(count == 0)
                    AppUser.AddError(nameof(AppUserValidator), nameof(AppUser.ERouteScope), ErrorCode.ERouteScopeNotExisted);
            }
            return AppUser.IsValidated;
        }

        public async Task<bool> Update(AppUser AppUser)
        {
            if (await ValidateId(AppUser))
            {
                await ValidateOrganization(AppUser);
            }
            return AppUser.IsValidated;
        }

    }
}
