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
using CRM.Services.MKnowledgeGroup;
using CRM.Services.MStatus;

namespace CRM.Rpc.knowledge_group
{
    public partial class KnowledgeGroupController : RpcController
    {
        [Route(KnowledgeGroupRoute.FilterListStatus), HttpPost]
        public async Task<List<KnowledgeGroup_StatusDTO>> FilterListStatus([FromBody] KnowledgeGroup_StatusFilterDTO KnowledgeGroup_StatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;
            StatusFilter.Id = KnowledgeGroup_StatusFilterDTO.Id;
            StatusFilter.Code = KnowledgeGroup_StatusFilterDTO.Code;
            StatusFilter.Name = KnowledgeGroup_StatusFilterDTO.Name;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<KnowledgeGroup_StatusDTO> KnowledgeGroup_StatusDTOs = Statuses
                .Select(x => new KnowledgeGroup_StatusDTO(x)).ToList();
            return KnowledgeGroup_StatusDTOs;
        }

        [Route(KnowledgeGroupRoute.SingleListStatus), HttpPost]
        public async Task<List<KnowledgeGroup_StatusDTO>> SingleListStatus([FromBody] KnowledgeGroup_StatusFilterDTO KnowledgeGroup_StatusFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;
            StatusFilter.Id = KnowledgeGroup_StatusFilterDTO.Id;
            StatusFilter.Code = KnowledgeGroup_StatusFilterDTO.Code;
            StatusFilter.Name = KnowledgeGroup_StatusFilterDTO.Name;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<KnowledgeGroup_StatusDTO> KnowledgeGroup_StatusDTOs = Statuses
                .Select(x => new KnowledgeGroup_StatusDTO(x)).ToList();
            return KnowledgeGroup_StatusDTOs;
        }

    }
}

