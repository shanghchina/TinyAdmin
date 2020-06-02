using Tiny.Common.Dapper.Persistence.UnitOfWork;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.DomainService
{
    public interface ISYS_DictionaryDomainService : IDomainService
    {
        /// <summary>
        /// 获取字典集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetDictionaryResponse GetDictionary(GetDictionaryRequest request);


        /// <summary>
        /// 获取参数 课程所属年级、课程所属班型、课程所属期段、课程所属类型、课程所属科目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<DictTreeResponse> GetPOCDictParams();

        /// <summary>
        /// 获取POC自定义参数大类
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetVMDictionaryResponse GetPOCBasicParamsParentList(GetDictionaryRequest request);

        /// <summary>
        /// 获取POC自定义参数明细参数
        /// </summary>
        /// <param name="parentGuid">父节点guid</param>
        /// <returns></returns>
        GetVMDictionaryResponse getPOCBasiceChildParamsList(Guid parentGuid);

        /// <summary>
        /// 新增修改明细字典内容
        /// </summary>
        /// <param name="viewModel"></param>
        UnitOfWorkResult SaveOrUpdateChild(SaveVMDictionaryRequest viewModel);

        /// <summary>
        /// 判断该字典已经映射过了，就不能删除了
        /// </summary>
        /// <param name="dictGuid"></param>
        /// <returns></returns>
        bool CheckHasMappedBasciData(Guid dictGuid);

        /// <summary>
        /// 已有待提取的提取器在使用此参数，提示：提取器正在使用，不能删除
        /// </summary>
        /// <param name="dictGuid"></param>
        /// <returns></returns>
        bool CheckIsExtractorBasciData(Guid dictGuid);
    }
}

