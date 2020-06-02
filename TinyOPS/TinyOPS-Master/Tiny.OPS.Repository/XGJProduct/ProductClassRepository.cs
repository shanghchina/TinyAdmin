using Dapper;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using Tiny.OPS.Domain.XGJProduct;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    public class ProductClassRepository : RepositoryBase, IProductClassRepository
    {
        public IList<T_EXT_Class> GetInfoByGuids(int from, params string[] guids)
        {
            return GetInfos<T_EXT_Class>(@"select * from [T_EXT_Class] with(nolock) where FromSystem=@from and  ClassID in @ClassIds", new { from = from, ClassIds = guids });
        }

        /// <summary>
        /// 获取分页班级信息列表
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        public VMEXTClassPageInfoResponse GetEXTClassByPage(EXTClassRequest search)
        {
            VMEXTClassPageInfoResponse resultInfo = new VMEXTClassPageInfoResponse();
            string strWhere = string.Empty;
            var _parameters = new DynamicParameters();
            #region 查询条件
            if (search.PocSource?.Count > 0)
            {
                strWhere += " and c.POCSource in @Pocsource";
                _parameters.Add("@Pocsource", search.PocSource.ToArray());
            }
            if (search.LevelOneOrgID?.Count > 0)
            {
                strWhere += " and c.TeachLevelOneOrgID in @OneOrgId";
                _parameters.Add("@OneOrgId", search.LevelOneOrgID.ToArray());
            }
            //班级状态
            if (!string.IsNullOrEmpty(search.ClassStatus))
            {
                if ((search.ClassStatus == ((int)EXTClassStatusEnum.Enabled).ToString()) || (search.ClassStatus.ToString() == ((int)EXTClassStatusEnum.Disabled).ToString()))
                {
                    strWhere += " and c.ClassStatus = @ClassStatus";
                    _parameters.Add("@ClassStatus", search.ClassStatus);
                }
            }

            //班级名称
            if (!string.IsNullOrEmpty(search.ClassName))
            {
                var signs = new List<string> { "'", "[", "%", "_" };
                strWhere += " and c.ClassName like @ClassName";
                _parameters.Add("@ClassName", $"%{search.ClassName.ReplaceBySigns(signs)}%");
            }
            #endregion

            string strSql = @"(SELECT c.* ,(CASE C.POCSource WHEN '100' THEN '校管家' ELSE '' END) POCSourceName,m.CourseName,
                            (SELECT TOP 1 TeacherUserName FROM T_EXT_ClassTeachTime ct WHERE ct.ProductClassID=c.Id) TeacherUserName
                            FROM T_EXT_Class c
                            LEFT JOIN T_EXT_Course m on m.FromSystem= c.FromSystem and m.CourseID=c.CourseID
                            WHERE 1=1 " + strWhere + ") as t";

            string SqlOrder = " ORDER BY t.Id desc";
            if (!string.IsNullOrWhiteSpace(search.SortName))
            {
                SqlOrder = " ORDER BY t." + search.SortName + " " + search.SortOrder;
            }

            string strQuery = "SELECT * FROM " + strSql + " " + SqlOrder;

            resultInfo.RecordCount = GetPageCount<VM_EXT_Class>(EumDBName.POC, strSql, "", _parameters);
            string sql = string.Empty;
            if (search.IsExport)
            {
                sql = strQuery;
            }
            else
            {
                sql = $"{strQuery} OFFSET {(search.PageIndex - 1) * search.PageSize} ROW FETCH NEXT {search.PageSize} ROWS ONLY";
            }
            resultInfo.ReusltList = GetInfos<VM_EXT_Class>(EumDBName.POC, sql, _parameters).AsList();

            return resultInfo;
        }

    }
}
