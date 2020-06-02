using Dapper;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiny.OPS.Repository
{
    public class T_POC_ExtractorDetailRepository : RepositoryBase, IT_POC_ExtractorDetailRepository
    {
        /// <summary>
        /// 获取提取器列表
        /// </summary>
        /// <returns></returns>
        public GetExtractorDetailListResponse GetExtractorDetaiList(GetExtractorDetaiListRequest request)
        {
            GetExtractorDetailListResponse response = new GetExtractorDetailListResponse();

            string sqlStr = @"	select tb1.*, ( select (stuff((select ',' + SelectFieldTypeName from [T_POC_ExtractorQuery] where FKExtractorGuid = 
    tt.FKExtractorGuid and SelectFieldTypeCode='ExtFromSystem' for xml path('')),1,1,'')) from [T_POC_ExtractorQuery] tt where tt.SelectFieldTypeCode='ExtFromSystem' and  tt.FKExtractorGuid=tb1.FKExtractorGuid  group by FKExtractorGuid)	 as 'ExtFromSystem',   
                             ( select (stuff((select ',' + SelectFieldTypeName from [T_POC_ExtractorQuery] where FKExtractorGuid = 
    tt.FKExtractorGuid and SelectFieldTypeCode='OneOrg' for xml path('')),1,1,'')) from [T_POC_ExtractorQuery] tt where tt.SelectFieldTypeCode='OneOrg' and  tt.FKExtractorGuid=tb1.FKExtractorGuid  group by FKExtractorGuid)	 as 'OneOrg'
                                       from [dbo].[T_POC_ExtractorDetail] tb1  inner join  [dbo].[T_POC_Extractor] tb2 on
                                     tb1.FKExtractorGuid = tb2.ExtractorGuid ";

            //查询条件
            StringBuilder _where = new StringBuilder(" WHERE  1=1 ");
            var _parameters = new DynamicParameters();
            StringBuilder _innerJoin = new StringBuilder("");
            //来源
            if (request.ExtFromSystem != null && request.ExtFromSystem.Count > 0)
            {
                _innerJoin.AppendFormat(@" inner join ( select FKExtractorGuid from [T_POC_ExtractorQuery] tb4 where SelectFieldTypeCode='ExtFromSystem'  AND  tb4.SelectFieldTypeId in @ExtFromSystem GROUP BY FKExtractorGuid ) tb3
                     on tb3.FKExtractorGuid =tb1.FKExtractorGuid ");
                _parameters.Add("@ExtFromSystem", request.ExtFromSystem.ToArray());
            }
            //事业部
            if (request.LevelOneGuid != null && request.LevelOneGuid.Count > 0)
            {
                _innerJoin.AppendFormat(@" inner join ( select FKExtractorGuid from [T_POC_ExtractorQuery] tb6 where SelectFieldTypeCode='OneOrg' AND  tb6.SelectFieldTypeId in @LevelOneGuid GROUP BY FKExtractorGuid ) tb5
                     on tb5.FKExtractorGuid =tb1.FKExtractorGuid ");
                _parameters.Add("@LevelOneGuid", request.LevelOneGuid.ToArray());
            }

            //提取器编号
            if (!string.IsNullOrEmpty(request.ExtractorNo))
            {
                _where.AppendFormat(" AND  tb1.ExtractorNo = @ExtractorNo");
                _parameters.Add("@ExtractorNo", request.ExtractorNo);

            }
            //提取器状态
            if (!string.IsNullOrEmpty(request.ExtractorStatus) && request.ExtractorStatus != "0")
            {
                _where.AppendFormat(" AND  tb1.ExtractorStatus = @ExtractorStatus");
                _parameters.Add("@ExtractorStatus", request.ExtractorStatus);
            }

            //课程所属年级
            if (request.GradeID != null && request.GradeID.Count > 0)
            {
                _where.AppendFormat(" AND  tb1.GradeID in @GradeID");
                _parameters.Add("@GradeID", request.GradeID.ToArray());
            }
            //课程所属类型
            if (request.CategoryID != null && request.CategoryID.Count > 0)
            {
                _where.AppendFormat(" AND  tb1.CategoryID in @CategoryID");
                _parameters.Add("@CategoryID", request.CategoryID.ToArray());
            }
            //课程所属科目
            if (request.SubjectID != null && request.SubjectID.Count > 0)
            {
                _where.AppendFormat(" AND  tb1.SubjectID in @SubjectID");
                _parameters.Add("@SubjectID", request.SubjectID.ToArray());
            }
            //课程所属期望值
            if (request.TermID != null && request.TermID.Count > 0)
            {
                _where.AppendFormat(" AND  tb1.TermID in @TermID");
                _parameters.Add("@TermID", request.TermID.ToArray());
            }
            //课程所属版型
            if (request.ClassTypeID != null && request.ClassTypeID.Count > 0)
            {
                _where.AppendFormat(" AND  tb1.ClassTypeID in @ClassTypeID");
                _parameters.Add("@ClassTypeID", request.ClassTypeID.ToArray());
            }
            //总数
            string countsql = @" select Count(1)
                                       from [dbo].[T_POC_ExtractorDetail] tb1  inner join  [dbo].[T_POC_Extractor] tb2 on
                                     tb1.FKExtractorGuid = tb2.ExtractorGuid " + _innerJoin.ToString() + _where.ToString();

            response.totalCount = GetInfos<int>(EumDBName.POC, countsql, _parameters).First();

            //排序
            string _orderBy = " order by tb1.Id desc";
            //分页
            var sql = sqlStr.ToString() + _innerJoin.ToString() + _where.ToString() + _orderBy;
            var pageIndex = request.PageIndex;
            var pageSize = request.PageSize;
            var _queryPage = $"{sql} offset {(pageIndex - 1) * pageSize} row fetch next {pageSize} rows only";
            response.dataList = GetInfos<ExtractorDetailItem>(EumDBName.POC, _queryPage.ToString(), _parameters).ToList();
            return response;
        }

        /// <summary>
        /// 获取单个提取器
        /// </summary>
        /// <param name="tiem"></param>
        /// <returns></returns>
        public ExtractorItem GetSingleExtractor(ExtractorItem item)
        {
            string sqlStr = @"select  top 1 tb1.*  from [dbo].[T_POC_ExtractorDetail] tb1  inner join  [dbo].[T_POC_Extractor] tb2 on  tb1.FKExtractorGuid = tb2.ExtractorGuid ";
            var _parameters = new DynamicParameters();
            //连表
            StringBuilder _innerJoin = new StringBuilder("");
            //查询条件
            StringBuilder _where = new StringBuilder(" WHERE  1=1 ");
            //来源系统
            if (item.fromSystem != null && item.fromSystem.Count() > 0)
            {
                _innerJoin.AppendFormat(@" inner join ( select  tt1.FKExtractorGuid  from (");
                foreach (var item1 in item.fromSystem)
                {
                    _innerJoin.AppendFormat("  select FKExtractorGuid  from T_POC_ExtractorQuery where  SelectFieldTypeCode='ExtFromSystem' and  SelectFieldTypeId='{0}'  union  all ", item1);
                }
                _innerJoin.AppendFormat(" select '00000000-0000-0000-0000-000000000000'  as 'FKExtractorGuid' ");
                _innerJoin.AppendFormat(@" )  tt1 group by FKExtractorGuid having count(1)={0}  ) tb3 on tb3.FKExtractorGuid =tb1.FKExtractorGuid", item.fromSystem.Count);

            }
            //事业部
            if (item.oneOrgId != null && item.oneOrgId.Count() > 0)
            {
                _innerJoin.AppendFormat(@" inner join ( select  tt1.FKExtractorGuid  from (");
                foreach (var item1 in item.oneOrgId)
                {
                    _innerJoin.AppendFormat("  select FKExtractorGuid  from T_POC_ExtractorQuery where  SelectFieldTypeCode='OneOrg' and  SelectFieldTypeId='{0}'  union  all ", item1);
                }
                _innerJoin.AppendFormat("  select '00000000-0000-0000-0000-000000000000'  as 'FKExtractorGuid' ");
                _innerJoin.AppendFormat(@" ) tt1 group by FKExtractorGuid having count(1)={0}  ) tb4 on tb4.FKExtractorGuid =tb1.FKExtractorGuid", item.oneOrgId.Count);

            }

            //产品状态
            if (item.proStatue != 3000)
            {
                _innerJoin.AppendFormat(@" inner join ( ");
                _innerJoin.AppendFormat(" select FKExtractorGuid  from T_POC_ExtractorQuery where  SelectFieldTypeCode='ProductStatus' and  SelectFieldTypeId='{0}'  ", item.proStatue);
                _innerJoin.AppendFormat(@" group by FKExtractorGuid ) tb5 on tb5.FKExtractorGuid =tb1.FKExtractorGuid");

            }

            //产品动销阈值
            if (item.thresholdValue != 1000)
            {
                _innerJoin.AppendFormat(@" inner join ( ");
                _innerJoin.AppendFormat("  select FKExtractorGuid  from T_POC_ExtractorQuery where  SelectFieldTypeCode='ThresholdValue' and  SelectFieldTypeId='{0}'  ", item.thresholdValue);
                _innerJoin.AppendFormat(@"  group by FKExtractorGuid ) tb6 on tb6.FKExtractorGuid =tb1.FKExtractorGuid", item.fromSystem.Count);

            }
            //年
            if (item.year > 0)
            {
                _where.AppendFormat(" AND  tb1.Year = @Year");
                _parameters.Add("@Year", item.year);
            }
            //年级
            if (!string.IsNullOrEmpty(item.gradeId))
            {
                _where.AppendFormat(" AND  tb1.GradeID = @GradeID");
                _parameters.Add("@GradeID", item.gradeId);
            }

            //类型
            if (!string.IsNullOrEmpty(item.categoryId))
            {
                _where.AppendFormat(" AND  tb1.CategoryID = @CategoryID");
                _parameters.Add("@CategoryID", item.categoryId);
            }

            //科目
            if (!string.IsNullOrEmpty(item.subjectId))
            {
                _where.AppendFormat(" AND  tb1.SubjectID = @SubjectID");
                _parameters.Add("@SubjectID", item.subjectId);
            }

            //期望值
            if (!string.IsNullOrEmpty(item.termId))
            {
                _where.AppendFormat(" AND  tb1.TermID = @TermID");
                _parameters.Add("@TermID", item.termId);
            }

            //班型
            if (!string.IsNullOrEmpty(item.classTypeId))
            {
                _where.AppendFormat(" AND  tb1.ClassTypeID = @ClassTypeID");
                _parameters.Add("@ClassTypeID", item.classTypeId);
            }
            ExtractorItem model = GetInfos<ExtractorItem>(EumDBName.POC, sqlStr.ToString() + _innerJoin.ToString() + _where.ToString() + " order By tb1.ID desc", _parameters).FirstOrDefault();
            return model;
        }

        /// <summary>
        /// 获取提取器最大的编号
        /// </summary>
        /// <returns></returns>
        public string GetMaxExtractorNo()
        {
            string _strSql = "select top 1 [ExtractorNo] from [dbo].[T_POC_ExtractorDetail] order by Id desc";
            return GetInfos<string>(EumDBName.POC, _strSql.ToString(), null).FirstOrDefault();
        }

        /// <summary>
        ///取消提取器
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public bool CancleExtractor(CancleExtractorRequest request)
        {
            var result = ExecuteCommand<T_POC_ExtractorDetail>(EumDBWay.Writer, EumDBName.POC.GetDisplayName(), " update [dbo].[T_POC_ExtractorDetail] set ExtractorStatus=2400,UpdateDate=getdate() where ExtractorStatus=2100 and [ExtractorDetailGuid] in @ExtractorDetailGuid", new { ExtractorDetailGuid = request.ExtractorGuids }).Commit();
            return result;
        }

        /// <summary>
        /// 获取提取器信息
        /// </summary>
        /// <param name="ExtractorDetailGuid"></param>
        /// <returns></returns>
        public T_POC_ExtractorDetail GetSingleByGuid(Guid extractorDetailGuid)
        {

            string _strSql = "select * from [dbo].[T_POC_ExtractorDetail] where ExtractorDetailGuid=@ExtractorDetailGuid";
            return GetInfos<T_POC_ExtractorDetail>(_strSql.ToString(), new { ExtractorDetailGuid = extractorDetailGuid }).FirstOrDefault();

        }
    }
}

