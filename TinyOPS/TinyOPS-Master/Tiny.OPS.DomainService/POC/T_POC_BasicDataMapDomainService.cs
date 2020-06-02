
using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using Tiny.OPS.Repository;
using System;

namespace Tiny.OPS.DomainService
{
    public class T_POC_BasicDataMapDomainService : RealDomainService<T_POC_BasicDataMap>, IT_POC_BasicDataMapDomainService
    {
        public IT_POC_BasicDataMapRepository pOC_BasicDataMapRepository => IoC.Resolve<IT_POC_BasicDataMapRepository>();
        public void OperateBasicDataMap(OperateBasicDataMapRequest request)
        {
            T_POC_BasicDataMap entity = new T_POC_BasicDataMap();
            entity.Id = request.Id;
            entity.FKBasicDataGuid = request.FKBasicDataGuid;
            entity.FKDictGuid = request.FKDictGuid;
            entity.SysCatalogTitle = request.SysCatalogTitle;
            entity.UpdaterUserId = request.UpdaterUserId;
            entity.UpdaterUserName = request.UpdaterUserName;
            entity.SysIsCatalog = request.SysIsCatalog;
            if (request.BasicDataMapGuid == Guid.Parse("00000000-0000-0000-0000-000000000000"))//新增
            {
                entity.CreatedDate = DateTime.Now;
                entity.UpdateDate = DateTime.Now;
                entity.BasicDataMapGuid = Guid.NewGuid();
                pOC_BasicDataMapRepository.AddPOC_BasicDataMap(entity);
            }
            else//修改
            {
                entity.UpdateDate = DateTime.Now;
                entity.BasicDataMapGuid = request.BasicDataMapGuid;
                pOC_BasicDataMapRepository.UpdatePOC_BasicDataMap(entity);
            }
        }
    }
}

