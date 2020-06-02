using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using Tiny.OPS.Repository;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.DomainService
{
    public class T_POC_EcsThresholdDomainService : RealDomainService<T_EXT_ThresholdValue>, IT_POC_EcsThresholdDomainService
    {
        public IT_POC_EcsThresholdRepository repository => IoC.Resolve<IT_POC_EcsThresholdRepository>();

        /// <summary>
        /// 更新或新增阈值数据
        /// </summary>
        /// <param name="thresholds"></param>
        /// <returns></returns>
        public BaseResponse AddOrUpdateThresholdValue(List<ThresholdList> thresholds)
        {
            BaseResponse response = new BaseResponse() { Code = "0000", Message = "成功" };
            try
            {
                foreach (var t in thresholds)
                {
                    var thresholdValue = repository.GetThreshold(Convert.ToInt32(t.FromSystem), t.ProductID);
                    if (thresholdValue == null)
                    {
                        //新增
                        T_EXT_ThresholdValue newThresholdValue = new T_EXT_ThresholdValue
                        {
                            FromSystem = Convert.ToInt32(t.FromSystem),
                            ChargeLevelOneOrgID = t.ChargeLevelOneOrgID,
                            ProductID = t.ProductID,
                            LastSaleDate = t.LastSaleDate,
                            CreatedDate = DateTime.Now,
                            UpdateDate = DateTime.Now
                        };
                        Add(newThresholdValue).Commit();
                    }
                    else
                    {
                        //更新
                        thresholdValue.FromSystem = Convert.ToInt32(t.FromSystem);
                        thresholdValue.ChargeLevelOneOrgID = t.ChargeLevelOneOrgID;
                        thresholdValue.ProductID = t.ProductID;
                        thresholdValue.LastSaleDate = t.LastSaleDate;
                        thresholdValue.UpdateDate = DateTime.Now;
                        Save(thresholdValue).Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                _Log4Net.Error($"更新或新增阈值数据失败：Message:{ex.Message} Source:{ex.Source} StackTrace:{ex.StackTrace} TargetSite:{ex.TargetSite}");
                response.Code = "9999";
                response.Message = ex.Message;
            }
            return response;
        }

    }
}
