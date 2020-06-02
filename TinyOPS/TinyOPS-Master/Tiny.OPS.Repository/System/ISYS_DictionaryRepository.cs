using Tiny.Common.Dapper.Persistence.UnitOfWork;
using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    public interface ISYS_DictionaryRepository : IRepository
    {
        /// <summary>
        /// 获取字典集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<DictionaryItem> GetDictionary(GetDictionaryRequest request);


        /// <summary>
        /// 获取字典集合包含子节点内容的父节点集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        List<VM_SYS_Dictionary> GetParentDictionary(GetDictionaryRequest request);

        /// <summary>
        /// 获取字典集合包含子节点
        /// </summary>
        /// <param name="parentGuid"></param>
        /// <returns></returns>
        List<VM_SYS_Dictionary> GetChildDictionaryList(GetDictionaryRequest request);

        /// <summary>
        /// 获取字典 实体类集合 
        /// </summary>
        /// <param name="parentGuid"></param>
        /// <returns></returns>
        List<T_SYS_Dictionary> GetDictEntityList(GetDictionaryRequest request);

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

        /// <summary>
        /// 更新父参数节点内容
        /// </summary>
        /// <param name="vmParentDict"></param>
        /// <returns></returns>
        UnitOfWorkResult UpdateParentDict(VM_SYS_Dictionary vmParentDict);
    }
}

