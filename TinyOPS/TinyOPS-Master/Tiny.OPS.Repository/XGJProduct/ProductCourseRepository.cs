using Dapper;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using Tiny.OPS.Domain.XGJProduct;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiny.OPS.Repository
{
    public class ProductCourseRepository : RepositoryBase, IProductCourseRepository
    {
        public T_EXT_Course GetInfoByGuid(int from, string guid)
        {
            return GetInfos<T_EXT_Course>(@"select * from [T_EXT_Course] where CourseID=@guid and FromSystem=@from", new { guid = @guid, from = @from }).FirstOrDefault();
        }
        /// <summary>
        /// 根据提取器信息获取课程信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetCouseListByExtractorResponse GetCouseListByExtractor(GetCouseListByExtractorRequest request)
        {
            GetCouseListByExtractorResponse response = new GetCouseListByExtractorResponse();
            string _sqlStr = @"SELECT tb1.*, ProductTypeName =(select  top 1 ProductTypeName  from T_EXT_ItemType  where  ProductTypeID = tb1.ProductTypeTwoID order  by NodeFlag ) , CampusCountName=(convert(varchar(50),(SELECT Count(1) FROM [dbo].[T_EXT_CourseRange]  where CourseID =tb1.CourseID))+'')  FROM [dbo].[T_EXT_Course] tb1  ";

            //查询条件
            StringBuilder _where = new StringBuilder(" WHERE  1=1 ");
            var _parameters = new DynamicParameters();
            StringBuilder _join = new StringBuilder("");
            //根据提取器Id查询
            if (!string.IsNullOrEmpty(request.extractorDetailGuid))
            {
                _join.AppendFormat(" inner  join [dbo].[T_POC_Product] tb3 on tb1.FKProductCourseGuid=tb3.ProductGuid");
                _where.AppendFormat(" and tb1.ExtractStatus  = @ExtractStatus ");
                _where.AppendFormat(" and tb3.FKDetailGuid  = @FKDetailGuid ");
                _parameters.Add("@ExtractStatus", request.extractStatus);
                _parameters.Add("@FKDetailGuid", request.extractorDetailGuid);
            }
            //根据条件查询
            else
            {
                //来源
                if (request.fromSystems != null && request.fromSystems.Count > 0)
                {
                    _where.AppendFormat(" and tb1.POCSource  in @POCSource ");
                    _parameters.Add("@POCSource", request.fromSystems.ToArray());
                }
                //事业部
                if (request.oneOrgIds != null && request.oneOrgIds.Count > 0)
                {
                    _where.AppendFormat(" and tb1.TeachLevelOneOrgID  in @TeachLevelOneOrgID ");
                    _parameters.Add("@TeachLevelOneOrgID", request.oneOrgIds.ToArray());
                }

                //产品状态
                if (request.courseStatus == 0 || request.courseStatus == 1)
                {
                    _where.AppendFormat(" and tb1.CourseStatus  = @CourseStatus ");
                    _parameters.Add("@CourseStatus", request.courseStatus);
                }
                //动销阈值
                if (request.lastSaleTime != null)
                {

                    _join.AppendFormat(" left join [dbo].[T_EXT_ThresholdValue] tb2 on tb1.CourseID=tb2.ProductID and tb1.FromSystem = tb2.FromSystem ");
                    _where.AppendFormat(" and tb2.LastSaleDate  >= @LastSaleDate ");
                    _parameters.Add("@LastSaleDate", request.lastSaleTime);

                }
                //课程所属年份
                if (request.courseYear > 0)
                {
                    _where.AppendFormat(" and tb1.CourseYear  = @CourseYear ");
                    _parameters.Add("@CourseYear", request.courseYear);
                }

                //课程所属年级
                if (request.gradeId != null && request.gradeId.Count > 0)
                {
                    _where.AppendFormat(" and tb1.GradeID  in  @GradeID ");
                    _parameters.Add("@GradeID", request.gradeId);
                }
                //课程所属类型
                if (request.categoryId != null && request.categoryId.Count > 0)
                {
                    _where.AppendFormat(" and tb1.CategoryID  in  @CategoryID ");
                    _parameters.Add("@CategoryID", request.categoryId);
                }
                //课程所属科目
                if (request.subjectId != null && request.subjectId.Count > 0)
                {
                    _where.AppendFormat(" and tb1.SubjectID  in  @SubjectID ");
                    _parameters.Add("@SubjectID", request.subjectId);
                }

                //课程所属期段
                if (request.termId != null && request.termId.Count > 0)
                {
                    _where.AppendFormat(" and tb1.TermID  in  @TermID ");
                    _parameters.Add("@TermID", request.termId);
                }
                //课程所属班型
                if (request.classTypeId != null && request.classTypeId.Count > 0)
                {
                    _where.AppendFormat(" and tb1.ClassTypeID  in  @ClassTypeID ");
                    _parameters.Add("@ClassTypeID", request.classTypeId);
                }
                //排除的课程Id
                if (request.notInCourseGuid != null && request.notInCourseGuid.Count > 0)
                {
                    _where.AppendFormat(" and tb1.CourseID not  in  @notInCourseGuid ");
                    _parameters.Add("@notInCourseGuid", request.notInCourseGuid);
                }
                _where.AppendFormat(" and tb1.ExtractStatus  = @ExtractStatus ");
                _parameters.Add("@ExtractStatus", (int)ExtractState.Unextracted);
            }
            //排除已删除的
            _where.AppendFormat(" and tb1.CourseStatus  != -1 ");

            //总数
            string countsql = @"SELECT Count(1) FROM [dbo].[T_EXT_Course] tb1  " + _join.ToString() + _where.ToString();
            response.totalCount = GetInfos<int>(EumDBName.POC, countsql, _parameters).First();
            if (!request.IsCount)
            {
                //排序
                string _orderBy = " order by tb1.Id desc";
                //分页
                var sql = _sqlStr.ToString() + _join.ToString() + _where.ToString() + _orderBy;
                var pageIndex = request.PageIndex;
                var pageSize = request.PageSize;
                var _queryPage = $"{sql} offset {(pageIndex - 1) * pageSize} row fetch next {pageSize} rows only";
                response.dataList = GetInfos<CourseItem>(EumDBName.POC, _queryPage.ToString(), _parameters).ToList();
            }
            return response;

        }

        public List<ProductCoursePageInfo> GetProductCoursePageInfo(ProductCoursePageRequest request, out int total)
        {
            StringBuilder _query = new StringBuilder();
            _query.Append(@"select c.SysDictValue PocSource,a.TeachLevelOneOrgName LevelOneOrgName,a.CourseName,b.ProductTypeName,a.CourseStatus,a.ExtractStatus,
                            a.FeeUnitPrice,a.FeeUnitPriceName,a.TotalClassHour,a.CourseYear,a.GradeName,a.CategoryName,a.SubjectName,a.TermName,a.ClassTypeName,
                            count(d.Id) AuthorizeNum,a.ProductCreatedDate as CreatedDate,a.ProductUpdateDate as UpdateDate from T_EXT_Course a
                            left join T_EXT_ItemType b on a.ProductTypeTwoID  = b.ProductTypeID
                            left join T_EXT_CourseRange d on d.CourseID = a.CourseID
                            left join T_SYS_Dictionary c on c.SysDictKey = cast(a.POCSource as nvarchar)");

            var _groupby = @" group by a.Id,c.SysDictValue,a.TeachLevelOneOrgName,a.CourseName,b.ProductTypeName,a.CourseStatus,a.ExtractStatus,
                            a.FeeUnitPrice,a.FeeUnitPriceName,a.TotalClassHour,a.CourseYear,a.GradeName,a.CategoryName,a.SubjectName,
                            a.TermName,a.ClassTypeName,a.ProductCreatedDate,a.ProductUpdateDate";

            var _parameters = new DynamicParameters();
            var _where = new StringBuilder(" where 1 = 1");

            if (request.PocSource?.Count > 0)
            {
                _where.Append(" and a.POCSource in @Pocsource");
                _parameters.Add("@Pocsource", request.PocSource.ToArray());
            }
            if (request.LevelOneOrgID?.Count > 0)
            {
                _where.Append(" and a.TeachLevelOneOrgID in @OneOrgId");
                _parameters.Add("@OneOrgId", request.LevelOneOrgID.ToArray());
            }
            if (request.CourseStatus != null && request.CourseStatus != CourseStatusEnum.All)
            {
                _where.Append(" and a.CourseStatus = @CourseStatus");
                _parameters.Add("@CourseStatus", (int)request.CourseStatus);
            }
            if (request.ExtractStatus != null)
            {
                _where.Append(" and a.ExtractStatus = @ExtractStatus");
                _parameters.Add("@ExtractStatus", (int)request.ExtractStatus);
            }
            if (!string.IsNullOrWhiteSpace(request.CourseName))
            {
                var signs = new List<string> { "'", "[", "%", "_" };
                _where.Append(" and a.CourseName like @CourseName");
                _parameters.Add("@CourseName", $"%{request.CourseName.ReplaceBySigns(signs)}%");
            }

            var countsql = $@"with tab as(
                             select count(1) num from T_EXT_Course a
                             left join T_EXT_ItemType b on a.ProductTypeTwoID  = b.ProductTypeID
                             left join T_EXT_CourseRange d on d.CourseID = a.CourseID
                             left join T_SYS_Dictionary c on c.SysDictKey = cast(a.POCSource as nvarchar)
                             {_where.ToString()}
                             {_groupby}
                             )
                             select count(1) from tab";

            total = GetInfos<int>(EumDBName.POC, countsql, _parameters).First();

            if (request.IsExport)
                return GetInfos<ProductCoursePageInfo>(EumDBName.POC, _query.ToString() + _where.ToString() + _groupby, _parameters).ToList();

            return GetPageInfos<ProductCoursePageInfo>(EumDBName.POC, _query.ToString() + _where.ToString() + _groupby, "a.Id ASC", request.PageIndex, request.PageSize, _parameters).ToList();
        }
    }
}
