using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Common.Web.XGJTools;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain.Enum;
using Tiny.OPS.Domain.XGJProduct;
using Tiny.OPS.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Tiny.OPS.DomainService
{
    public class InterfaceConfigDomainService : RealDomainService<T_SYS_UrlConfigure>, IInterfaceConfigDomainService
    {
        private readonly IXGJInterfaceConfigureRepository repository = IoC.Resolve<IXGJInterfaceConfigureRepository>();

        /// <summary>
        /// 获取配置数据
        /// </summary>
        /// <param name="category">类别(XGJInSideInterface - 内部接口类型，XGJOutSideInterface - 外部接口类型，string.Empty - 所有接口数据)</param>
        /// <param name="jobData">系统来源+事业部</param>
        /// <returns></returns>
        public PushModel GetInterfaceConfigure(XGJInterfaceConfigureStatus status, string category = XGJInterfaceConfigureCategory.XGJInSideInterface, JobDataRequest jobData=null)
        {
            var pushSystems = new List<PushSystem>();
            IList<T_SYS_UrlConfigure> lists = null;
            if (jobData != null)
                lists = repository.GetListForPocSourceAndLevelOneOrgID(status, jobData);
            else
                lists = repository.GetList(status);
            var PushSystem = new PushSystem();
            string flagCategory = string.Empty;
            for (int i = 0; i < lists.Count; i++)
            {
                if (flagCategory != lists[i].Category)
                {
                    PushSystem = DataCopyPushFailure(lists[i]);
                    PushSystem.Configs.Add(DataCopyPushFailureConfig(lists[i]));
                    flagCategory = lists[i].Category;
                }
                else
                {
                    PushSystem.Configs.Add(DataCopyPushFailureConfig(lists[i]));
                }
                if (i == lists.Count - 1 || flagCategory != lists[i + 1].Category)
                {
                    pushSystems.Add(PushSystem);
                }
            }
            return new PushModel
            {
                Name = "异常推送接口配置",
                Root = string.Empty,
                Systems = category == string.Empty ? pushSystems : pushSystems.Where(v => v.Value == category).ToList()
            };
        }

        private PushSystem DataCopyPushFailure(T_SYS_UrlConfigure data)
        {
            return new PushSystem
            {
                Name = "校官家",
                Url = string.Empty,
                Value = data.Category
            };
        }

        private PushConfig DataCopyPushFailureConfig(T_SYS_UrlConfigure data)
        {
            return new PushConfig
            {
                FromSystem = data.FromSystem,
                ChargeLevelOneOrgId = data.ChargeLevelOneOrgId.ToString(),
                ChargeLevelOneOrgName = data.ChargeLevelOneOrgName,
                Name = data.ChargeLevelOneOrgName,
                Value = data.ChargeLevelOneOrgId,
                AppId = data.AppId,
                AppSecret = data.AppSecret,
                TokenUrl = data.TokenUrl,
                ErrorUrl = data.ErrorUrl,
                POCSource = data.POCSource,
                EnableTokenCache = data.EnableTokenCache ? "true" : "false"
            };
        }
    }
}
