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

namespace CRM.Services.MPosition
{
    public interface IPositionService :  IServiceScoped
    {
        Task<int> Count(PositionFilter PositionFilter);
        Task<List<Position>> List(PositionFilter PositionFilter);
        Task<Position> Get(long Id);
        Task<Position> Create(Position Position);
        Task<Position> Update(Position Position);
        Task<Position> Delete(Position Position);
        Task<List<Position>> BulkDelete(List<Position> Positions);
        Task<List<Position>> Import(List<Position> Positions);
        Task<PositionFilter> ToFilter(PositionFilter PositionFilter);
    }

    public class PositionService : BaseService, IPositionService
    {
        private IUOW UOW;
        private ILogging Logging;
        private ICurrentContext CurrentContext;
        private IPositionValidator PositionValidator;

        public PositionService(
            IUOW UOW,
            ICurrentContext CurrentContext,
            IPositionValidator PositionValidator,
            ILogging Logging
        )
        {
            this.UOW = UOW;
            this.Logging = Logging;
            this.CurrentContext = CurrentContext;
            this.PositionValidator = PositionValidator;
        }
        public async Task<int> Count(PositionFilter PositionFilter)
        {
            try
            {
                int result = await UOW.PositionRepository.Count(PositionFilter);
                return result;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(PositionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(PositionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Position>> List(PositionFilter PositionFilter)
        {
            try
            {
                List<Position> Positions = await UOW.PositionRepository.List(PositionFilter);
                return Positions;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(PositionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(PositionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<Position> Get(long Id)
        {
            Position Position = await UOW.PositionRepository.Get(Id);
            if (Position == null)
                return null;
            return Position;
        }
        public async Task<Position> Create(Position Position)
        {
            if (!await PositionValidator.Create(Position))
                return Position;

            try
            {
                await UOW.Begin();
                await UOW.PositionRepository.Create(Position);
                await UOW.Commit();
                Position = await UOW.PositionRepository.Get(Position.Id);
                await Logging.CreateAuditLog(Position, new { }, nameof(PositionService));
                return Position;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(PositionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(PositionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Position> Update(Position Position)
        {
            if (!await PositionValidator.Update(Position))
                return Position;
            try
            {
                var oldData = await UOW.PositionRepository.Get(Position.Id);

                await UOW.Begin();
                await UOW.PositionRepository.Update(Position);
                await UOW.Commit();

                Position = await UOW.PositionRepository.Get(Position.Id);
                await Logging.CreateAuditLog(Position, oldData, nameof(PositionService));
                return Position;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(PositionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(PositionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<Position> Delete(Position Position)
        {
            if (!await PositionValidator.Delete(Position))
                return Position;

            try
            {
                await UOW.Begin();
                await UOW.PositionRepository.Delete(Position);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, Position, nameof(PositionService));
                return Position;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(PositionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(PositionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }

        public async Task<List<Position>> BulkDelete(List<Position> Positions)
        {
            if (!await PositionValidator.BulkDelete(Positions))
                return Positions;

            try
            {
                await UOW.Begin();
                await UOW.PositionRepository.BulkDelete(Positions);
                await UOW.Commit();
                await Logging.CreateAuditLog(new { }, Positions, nameof(PositionService));
                return Positions;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(PositionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(PositionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }
        
        public async Task<List<Position>> Import(List<Position> Positions)
        {
            if (!await PositionValidator.Import(Positions))
                return Positions;
            try
            {
                await UOW.Begin();
                await UOW.PositionRepository.BulkMerge(Positions);
                await UOW.Commit();

                await Logging.CreateAuditLog(Positions, new { }, nameof(PositionService));
                return Positions;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                if (ex.InnerException == null)
                {
                    await Logging.CreateSystemLog(ex, nameof(PositionService));
                    throw new MessageException(ex);
                }
                else
                {
                    await Logging.CreateSystemLog(ex.InnerException, nameof(PositionService));
                    throw new MessageException(ex.InnerException);
                }
            }
        }     
        
        public async Task<PositionFilter> ToFilter(PositionFilter filter)
        {
            if (filter.OrFilter == null) filter.OrFilter = new List<PositionFilter>();
            if (CurrentContext.Filters == null || CurrentContext.Filters.Count == 0) return filter;
            foreach (var currentFilter in CurrentContext.Filters)
            {
                PositionFilter subFilter = new PositionFilter();
                filter.OrFilter.Add(subFilter);
                List<FilterPermissionDefinition> FilterPermissionDefinitions = currentFilter.Value;
                foreach (FilterPermissionDefinition FilterPermissionDefinition in FilterPermissionDefinitions)
                {
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Id))
                        subFilter.Id = FilterBuilder.Merge(subFilter.Id, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Code))
                        subFilter.Code = FilterBuilder.Merge(subFilter.Code, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.Name))
                        subFilter.Name = FilterBuilder.Merge(subFilter.Name, FilterPermissionDefinition.StringFilter);
                    if (FilterPermissionDefinition.Name == nameof(subFilter.StatusId))
                        subFilter.StatusId = FilterBuilder.Merge(subFilter.StatusId, FilterPermissionDefinition.IdFilter);
                    if (FilterPermissionDefinition.Name == nameof(CurrentContext.UserId) && FilterPermissionDefinition.IdFilter != null)
                    {
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.IS.Id)
                        {
                        }
                        if (FilterPermissionDefinition.IdFilter.Equal.HasValue && FilterPermissionDefinition.IdFilter.Equal.Value == CurrentUserEnum.ISNT.Id)
                        {
                        }
                    }
                }
            }
            return filter;
        }
    }
}
