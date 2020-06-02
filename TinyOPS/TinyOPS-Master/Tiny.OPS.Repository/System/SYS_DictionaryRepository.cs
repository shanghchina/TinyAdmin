using Dapper;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.UnitOfWork;
using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiny.OPS.Repository
{
    public class SYS_DictionaryRepository : RepositoryBase, ISYS_DictionaryRepository
    {
        /// <summary>
        /// 获取字典集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<DictionaryItem> GetDictionary(GetDictionaryRequest request)
        {
            try
            {
                //sql
                StringBuilder _sql = new StringBuilder(@"SELECT * FROM [dbo].[T_SYS_Dictionary] WHERE 1=1 ");
                //查询条件
                StringBuilder _condition = new StringBuilder("");
                var _parameters = new DynamicParameters();

                //guid
                if (request.DictGuid!=null && request.DictGuid.Count>0)
                {
                    _condition.AppendFormat(" AND DictGuid in @DictGuid  ");
                    _parameters.Add("@DictGuid", request.DictGuid.ToArray());

                }
                //父级Id
                if (request.ParentGuid != null && request.ParentGuid.Count > 0)
                {
                    _condition.AppendFormat(" AND ParentGuid in @ParentGuid  ");
                    _parameters.Add("@ParentGuid", request.ParentGuid.ToArray());
                }
                _condition.Append(" ORDER BY SysCatalogCode,SysDictSort");
                return GetInfos<DictionaryItem>(EumDBName.POC, _sql.ToString() + _condition, _parameters).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取字典集合包含子节点内容的父节点集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<VM_SYS_Dictionary> GetParentDictionary(GetDictionaryRequest request)
        {
            try
            {
                //sql
                string strSql = @"SELECT 
                    (SELECT y.ChinldSysDictValueLable FROM 
                    (SELECT ChinldSysDictValueLable =(
                    SELECT STUFF((SELECT '、' + SysDictValue FROM (SELECT * FROM [T_SYS_Dictionary] WHERE ParentGuid=p.DictGuid) x
                    ORDER BY Id FOR XML PATH('')),1,1,''))
                    ) y) ChinldSysDictValueLable,
                    p.* FROM [dbo].[T_SYS_Dictionary] p WHERE 1=1";
                StringBuilder _sql = new StringBuilder(strSql);
                //查询条件
                StringBuilder _condition = new StringBuilder("");
                var _parameters = new DynamicParameters();

                //guid
                if (request.DictGuid != null && request.DictGuid.Count > 0)
                {
                    _condition.AppendFormat(" AND p.DictGuid in @DictGuid  ");
                    _parameters.Add("@DictGuid", request.DictGuid.ToArray());

                }
                //父级Id
                if (request.ParentGuid != null && request.ParentGuid.Count > 0)
                {
                    _condition.AppendFormat(" AND p.ParentGuid in @ParentGuid  ");
                    _parameters.Add("@ParentGuid", request.ParentGuid.ToArray());
                }
                _condition.Append(" ORDER BY SysDictSort");
                return GetInfos<VM_SYS_Dictionary>(EumDBName.POC, _sql.ToString() + _condition, _parameters).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取字典集合包含子节点
        /// </summary>
        /// <param name="parentGuid"></param>
        /// <returns></returns>
        public List<VM_SYS_Dictionary> GetChildDictionaryList(GetDictionaryRequest request)
        {
            try
            {
                //sql
                StringBuilder _sql = new StringBuilder(@"SELECT * FROM [dbo].[T_SYS_Dictionary] WHERE 1=1 ");
                //查询条件
                StringBuilder _condition = new StringBuilder("");
                var _parameters = new DynamicParameters();

                //guid
                if (request.DictGuid != null && request.DictGuid.Count > 0)
                {
                    _condition.AppendFormat(" AND DictGuid in @DictGuid  ");
                    _parameters.Add("@DictGuid", request.DictGuid.ToArray());

                }
                //父级Id
                if (request.ParentGuid != null && request.ParentGuid.Count > 0)
                {
                    _condition.AppendFormat(" AND (ParentGuid<>'"+Guid.Empty.ToString()+"') AND ParentGuid in @ParentGuid  ");
                    _parameters.Add("@ParentGuid", request.ParentGuid.ToArray());
                }
                _condition.Append(" ORDER BY SysCatalogCode,SysDictSort");
                return GetInfos<VM_SYS_Dictionary>(EumDBName.POC, _sql.ToString() + _condition, _parameters).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取字典 实体类集合 
        /// </summary>
        /// <param name="parentGuid"></param>
        /// <returns></returns>
        public List<T_SYS_Dictionary> GetDictEntityList(GetDictionaryRequest request)
        {
            try
            {
                //sql
                StringBuilder _sql = new StringBuilder(@"SELECT * FROM [dbo].[T_SYS_Dictionary] WHERE 1=1 ");
                //查询条件
                StringBuilder _condition = new StringBuilder("");
                var _parameters = new DynamicParameters();

                //guid
                if (request.DictGuid != null && request.DictGuid.Count > 0)
                {
                    _condition.AppendFormat(" AND DictGuid in @DictGuid  ");
                    _parameters.Add("@DictGuid", request.DictGuid.ToArray());

                }
                //父级Id
                if (request.ParentGuid != null && request.ParentGuid.Count > 0)
                {
                    _condition.AppendFormat(" AND (ParentGuid<>'" + Guid.Empty.ToString() + "') AND ParentGuid in @ParentGuid  ");
                    _parameters.Add("@ParentGuid", request.ParentGuid.ToArray());
                }
                _condition.Append(" ORDER BY SysCatalogCode,SysDictSort");
                return GetInfos<T_SYS_Dictionary>(EumDBName.POC, _sql.ToString() + _condition, _parameters).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 判断该字典已经映射过了，就不能删除了
        /// </summary>
        /// <param name="dictGuid"></param>
        /// <returns></returns>
        public bool CheckHasMappedBasciData(Guid dictGuid)
        {
            try
            {
                //sql
                StringBuilder _sql = new StringBuilder(@"SELECT * FROM [dbo].[T_POC_BasicDataMap] WHERE 1=1 ");
                //查询条件
                StringBuilder _condition = new StringBuilder("");
                var _parameters = new DynamicParameters();
                _condition.AppendFormat(" AND FKDictGuid=@FKDictGuid AND (FKDictGuid<>'" + GlobalConstConfig.GUID_EMPTY+ "')");
                _parameters.Add("@FKDictGuid", dictGuid);
                var dictEntity = GetInfos<VM_SYS_Dictionary>(EumDBName.POC, _sql.ToString() + _condition, _parameters).FirstOrDefault();
                if (dictEntity != null)
                {
                    _Log4Net.Info("已存在映射字典Guid：" + dictEntity.DictGuid);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 已有待提取的提取器在使用此参数，提示：提取器正在使用，不能删除
        /// </summary>
        /// <param name="dictGuid"></param>
        /// <returns></returns>
        public bool CheckIsExtractorBasciData(Guid dictGuid)
        {
            try
            {
                string strSql = @"SELECT * FROM T_POC_ExtractorDetail WHERE ExtractorStatus=2100 
                    AND (UPPER(GradeID)=@DictGuid OR UPPER(CategoryID)=@DictGuid OR UPPER(SubjectID)=@DictGuid OR UPPER(TermID)=@DictGuid OR UPPER(ClassTypeID)=@DictGuid)";
                var _paramExtractor = new DynamicParameters();
                _paramExtractor.Add("@DictGuid", dictGuid);
                var dictExtractor = GetInfos<T_POC_ExtractorDetail>(EumDBName.POC, strSql, _paramExtractor).FirstOrDefault();
                if (dictExtractor != null)
                {
                    _Log4Net.Info("提取器正在使用dictGuid：" + dictGuid + "提取器guid：" + dictExtractor.ExtractorDetailGuid);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新父参数节点内容
        /// </summary>
        /// <param name="vmParentDict"></param>
        /// <returns></returns>
        public UnitOfWorkResult UpdateParentDict(VM_SYS_Dictionary vmParentDict)
        {
            string strSql = "UPDATE T_SYS_Dictionary SET SysDictValue=@SysDictValue,SysDictDesc=@SysDictDesc,SysDictSort=@SysDictSort WHERE DictGuid=@DictGuid";
            var _parameters = new DynamicParameters();
            _parameters.Add("@SysDictValue", vmParentDict.SysDictValue);
            _parameters.Add("@SysDictDesc", vmParentDict.SysDictDesc);
            _parameters.Add("@SysDictSort", vmParentDict.SysDictSort);
            _parameters.Add("@DictGuid", vmParentDict.DictGuid);
            return ExecuteCommand<T_SYS_Dictionary>(EumDBWay.Writer, EumDBName.POC.GetDisplayName(), strSql, _parameters);
        }
    }
}

