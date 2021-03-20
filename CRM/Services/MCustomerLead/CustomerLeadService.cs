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
using CRM.Services.MContact;
using CRM.Enums;
using CRM.Services.MOrganization;
using CRM.Services.MFile;

namespace CRM.Services.MCustomerLead
{
    public interface ICustomerLeadService : IServiceScoped
    {
        Task<int> Count(CustomerLeadFilter CustomerLeadFilter);
        Task<List<CustomerLead>> List(CustomerLeadFilter CustomerLeadFilter);
        Task<CustomerLead> Get(long Id);
        Task<CustomerLead> Create(CustomerLead CustomerLead);
        Task<CustomerLead> Convert(CustomerLead CustomerLead);
        Task<CustomerLead> Update(CustomerLead CustomerLead);
        Task<CustomerLead> Delete(CustomerLead CustomerLead);
        Task<List<CustomerLead>> BulkDelete(List<CustomerLead> CustomerLeads);
        Task<List<CustomerLead>> Import(List<CustomerLead> CustomerLeads);
        Task<CustomerLeadFilter> ToFilter(CustomerLeadFilter CustomerLeadFilter);
        Task<Entities.File> UploadFile(Entities.File File);
    }

    public class CustomerLeadService : BaseService, ICustomerLeadService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private ICustomerLeadValidator CustomerLeadValidator;
        private IOrganizationService OrganizationService;
        private IFileService FileService;

        public CustomerLeadService(
            IUOW UOW,
            ILogging Logging,
            ICurrentContext CurrentContext,
            IOrganizationService OrganizationService,
            ICustomerLeadValidator CustomerLeadValidator,
            IFileService FileService
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.OrganizationService = OrganizationService;
            this.CustomerLeadValidator = CustomerLeadValidator;
            this.FileService = FileService;
        }
        public async Task<int> Count(CustomerLeadFilter CustomerLeadFilter)
        {
            try
            {
                int result = await UOW.CustomerLeadRepository.Count(CustomerLeadFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CustomerLeadService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CustomerLeadService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<CustomerLead>> List(CustomerLeadFilter CustomerLeadFilter)
        {
            try
            {
                List<CustomerLead> CustomerLeads = await UOW.CustomerLeadRepository.List(CustomerLeadFilter);
                return CustomerLeads;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CustomerLeadService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CustomerLeadService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        public async Task<CustomerLead> Get(long Id)
        {
            CustomerLead CustomerLead = await UOW.CustomerLeadRepository.Get(Id);
            if (CustomerLead == null)
                return null;
            return CustomerLead;
        }

        public async Task<CustomerLead> Create(CustomerLead CustomerLead)
        {
            if (!await CustomerLeadValidator.Create(CustomerLead))
                return CustomerLead;

            try
            {
                await UOW.Begin();
                var Creator = await UOW.AppUserRepository.Get(CurrentContext.UserId);
                CustomerLead.CreatorId = Creator.Id;
                CustomerLead.OrganizationId = Creator.OrganizationId;
                CustomerLead.CustomerLeadStatusId = CustomerLeadStatusEnum.NEW.Id;
                await UOW.CustomerLeadRepository.Create(CustomerLead);
                await UOW.CustomerLeadRepository.Update(CustomerLead);
                await UOW.Commit();
                CustomerLead = await UOW.CustomerLeadRepository.Get(CustomerLead.Id);
                await Logging.CreateAuditLog(CustomerLead, new { }, nameof(CustomerLeadService));
                return CustomerLead;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CustomerLeadService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CustomerLeadService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<CustomerLead> Convert(CustomerLead CustomerLead)
        {
            if (!await CustomerLeadValidator.Convert(CustomerLead))
                return CustomerLead;

            try
            {
                CustomerLead oldData = await UOW.CustomerLeadRepository.Get(CustomerLead.Id);

                await UOW.Begin();
                var Creator = await UOW.AppUserRepository.Get(CurrentContext.UserId);
                if (CustomerLead.IsNewCompany)
                {
                    Company Company = new Company
                    {
                        CustomerLeadId = CustomerLead.Id,
                        Name = CustomerLead.Company?.Name,
                        Phone = CustomerLead.Company?.Phone,
                        FAX = CustomerLead.Fax,
                        PhoneOther = CustomerLead.TelePhone,
                        Email = CustomerLead.Email,
                        EmailOther = CustomerLead.SecondEmail,
                        ZIPCode = CustomerLead.ZipCode,
                        Revenue = CustomerLead.Revenue,
                        Website = CustomerLead.Website,
                        NationId = CustomerLead.NationId,
                        ProvinceId = CustomerLead.ProvinceId,
                        DistrictId = CustomerLead.DistrictId,
                        Address = CustomerLead.Address,
                        NumberOfEmployee = CustomerLead.EmployeeQuantity,
                        RefuseReciveEmail = CustomerLead.RefuseReciveEmail,
                        RefuseReciveSMS = CustomerLead.RefuseReciveSMS,
                        ProfessionId = CustomerLead.ProfessionId,
                        Description = CustomerLead.Description,
                        CompanyStatusId = CompanyStatusEnum.NEW.Id,
                        AppUserId = CurrentContext.UserId,
                        CreatorId = Creator.Id,
                        OrganizationId = Creator.OrganizationId,
                    };
                    await UOW.CompanyRepository.Create(Company);
                    CustomerLead.CompanyId = Company.Id;
                }
                else
                {
                    Company Company = await UOW.CompanyRepository.Get(CustomerLead.Company.Id);
                    Company.CustomerLeadId = CustomerLead.Id;
                    await UOW.CompanyRepository.Update(Company);
                    CustomerLead.CompanyId = Company.Id;
                }
                if (CustomerLead.IsNewContact)
                {
                    Contact Contact = new Contact
                    {
                        CustomerLeadId = CustomerLead.Id,
                        CompanyId = CustomerLead.CompanyId,
                        Name = CustomerLead.Contact?.Name,
                        Phone = CustomerLead.Contact?.Phone,
                        FAX = CustomerLead.Fax,
                        Email = CustomerLead.Email,
                        EmailOther = CustomerLead.SecondEmail,
                        ZIPCode = CustomerLead.ZipCode,
                        NationId = CustomerLead.NationId,
                        ProvinceId = CustomerLead.ProvinceId,
                        DistrictId = CustomerLead.DistrictId,
                        Address = CustomerLead.Address,
                        RefuseReciveEmail = CustomerLead.RefuseReciveEmail,
                        RefuseReciveSMS = CustomerLead.RefuseReciveSMS,
                        ProfessionId = CustomerLead.ProfessionId,
                        Description = CustomerLead.Description,
                        ContactStatusId = ContactStatusEnum.NEW.Id,
                        AppUserId = CurrentContext.UserId,
                        CreatorId = Creator.Id,
                        OrganizationId = Creator.OrganizationId,
                    };
                    await UOW.ContactRepository.Create(Contact);
                }
                else
                {
                    Contact Contact = await UOW.ContactRepository.Get(CustomerLead.Contact.Id);
                    Contact.CustomerLeadId = CustomerLead.Id;
                    await UOW.ContactRepository.Update(Contact);
                }
                if (CustomerLead.IsCreateOpportunity)
                {
                    if (CustomerLead.IsNewOpportunity)
                    {
                        var ClosingDate = new DateTime(StaticParams.DateTimeNow.Year, StaticParams.DateTimeNow.Month, 1);
                        ClosingDate = ClosingDate.AddMonths(2).AddSeconds(-1);
                        Opportunity Opportunity = new Opportunity
                        {
                            CustomerLeadId = CustomerLead.Id,
                            CompanyId = CustomerLead.CompanyId,
                            Name = CustomerLead.Opportunity?.Name,
                            RefuseReciveEmail = CustomerLead.RefuseReciveEmail,
                            RefuseReciveSMS = CustomerLead.RefuseReciveSMS,
                            Description = CustomerLead.Description,
                            ClosingDate = ClosingDate,
                            ProbabilityId = ProbabilityEnum.TEN_PERCENT.Id,
                            SaleStageId = SaleStageEnum.NEW.Id,
                            AppUserId = CurrentContext.UserId,
                            CreatorId = Creator.Id,
                            OrganizationId = Creator.OrganizationId,
                        };
                        await UOW.OpportunityRepository.Create(Opportunity);
                    }
                    else
                    {
                        Opportunity Opportunity = await UOW.OpportunityRepository.Get(CustomerLead.Opportunity.Id);
                        Opportunity.CustomerLeadId = CustomerLead.Id;
                        await UOW.OpportunityRepository.Update(Opportunity);
                    }
                }
                await UOW.CustomerLeadRepository.UpdateState(CustomerLead);
                await UOW.Commit();
                CustomerLead = await UOW.CustomerLeadRepository.Get(CustomerLead.Id);
                await Logging.CreateAuditLog(CustomerLead, oldData, nameof(CustomerLeadService));
                return CustomerLead;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CustomerLeadService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CustomerLeadService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<CustomerLead> Update(CustomerLead CustomerLead)
        {
            if (!await CustomerLeadValidator.Update(CustomerLead))
                return CustomerLead;
            try
            {
                var oldData = await UOW.CustomerLeadRepository.Get(CustomerLead.Id);

                foreach (var CustomerLeadFileGroup in CustomerLead.CustomerLeadFileGroups)
                {
                    if (CustomerLeadFileGroup.Id == 0)
                        CustomerLeadFileGroup.CreatorId = CurrentContext.UserId;
                }

                await UOW.Begin();
                await UOW.CustomerLeadRepository.Update(CustomerLead);
                await UOW.Commit();

                CustomerLead = await UOW.CustomerLeadRepository.Get(CustomerLead.Id);
                await Logging.CreateAuditLog(CustomerLead, oldData, nameof(CustomerLeadService));
                return CustomerLead;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CustomerLeadService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CustomerLeadService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<CustomerLead> Delete(CustomerLead CustomerLead)
        {
            if (!await CustomerLeadValidator.Delete(CustomerLead))
                return CustomerLead;

            try
            {
                await UOW.Begin();
                await UOW.CustomerLeadRepository.Delete(CustomerLead);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, CustomerLead, nameof(CustomerLeadService));
                return CustomerLead;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CustomerLeadService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CustomerLeadService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<CustomerLead>> BulkDelete(List<CustomerLead> CustomerLeads)
        {
            if (!await CustomerLeadValidator.BulkDelete(CustomerLeads))
                return CustomerLeads;

            try
            {
                await UOW.Begin();
                await UOW.CustomerLeadRepository.BulkDelete(CustomerLeads);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, CustomerLeads, nameof(CustomerLeadService));
                return CustomerLeads;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CustomerLeadService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CustomerLeadService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<CustomerLead>> Import(List<CustomerLead> CustomerLeads)
        {
            if (!await CustomerLeadValidator.Import(CustomerLeads))
                return CustomerLeads;
            try
            {
                await UOW.Begin();
                await UOW.CustomerLeadRepository.BulkMerge(CustomerLeads);
                await UOW.Commit();

                await Logging.CreateAuditLog(CustomerLeads, new { }, nameof(CustomerLeadService));
                return CustomerLeads;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(CustomerLeadService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(CustomerLeadService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<CustomerLeadFilter> ToFilter(CustomerLeadFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<CustomerLeadFilter>();
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
                CustomerLeadFilter subFilter = new CustomerLeadFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.AppUserId))
                        subFilter.AppUserId = FilterBuilder.Merge(subFilter.AppUserId, FilterPermissionDefinition.IdFilter);
                }
            }
            return filter;
        }

        public async Task<Entities.File> UploadFile(Entities.File File)
        {
            FileInfo fileInfo = new FileInfo(File.Name);
            string path = $"/customer-lead/{StaticParams.DateTimeNow.ToString("yyyyMMdd")}/{Guid.NewGuid()}{fileInfo.Extension}";
            string thumbnailPath = $"/customer-lead/{StaticParams.DateTimeNow.ToString("yyyyMMdd")}/{Guid.NewGuid()}{fileInfo.Extension}";
            File = await FileService.Create(File, path);
            return File;
        }
    }
}
