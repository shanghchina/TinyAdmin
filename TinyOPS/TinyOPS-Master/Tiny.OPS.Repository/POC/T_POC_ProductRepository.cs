using Dapper;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using Tiny.OPS.Domain.XGJProduct;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiny.OPS.Repository
{
    /// <summary>
    /// 产品管理-产品信息
    /// </summary>
	public class T_POC_ProductRepository : RepositoryBase, IT_POC_ProductRepository
    {
        //public IList<T_POC_Product> GetAll(string strWhere)
        //{
        //    return GetInfos<T_POC_Product>(@"SELECT * FROM T_POC_Product WHERE 1=1 AND @strWhere", new { strWhere = @strWhere });
        //}

        /// <summary>
        /// 获取分页产品信息列表
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        public POCProductPageInfoResponse GetPOCProductByPage(POCProductRequest search)
        {
            POCProductPageInfoResponse resultInfo = new POCProductPageInfoResponse();
            string strWhere = string.Empty;
            var _parameters = new DynamicParameters();
            #region 查询条件
            if (search.ProductTypeGuid != Guid.Empty)
            {
                strWhere += " AND ProductTypeGuid=@ProductTypeGuid";
                _parameters.Add("@ProductTypeGuid", search.ProductTypeGuid);
            }
            if ((search.CourseStatus == CourseStatusEnum.Deleted) ||
                (search.CourseStatus == CourseStatusEnum.Disabled) ||
                (search.CourseStatus == CourseStatusEnum.Enabled))
            {
                strWhere += " AND CourseStatus=@CourseStatus";
                _parameters.Add("@CourseStatus", search.CourseStatus);
            }
            if (!string.IsNullOrWhiteSpace(search.CourseName))
            {
                var signs = new List<string> { "'", "[", "%", "_" };
                strWhere += " AND CourseName like @CourseName";
                _parameters.Add("@CourseName", $"%{search.CourseName.ReplaceBySigns(signs)}%");
            }
            //if ((search.CheckedTreeNodes != null) && (search.CheckedTreeNodes.Count>0))
            //{
            //    //获取所有选择的treeNode的Guid
            //    GetChildNodeGuid(null, search.CheckedTreeNodes);
            //    if (ChildNodeGuidList.Count>0)
            //    {
            //        strWhere += " AND ((GradeDictGuid in @CheckedTreeNodesGuid) or (CategoryDictGuid in @CheckedTreeNodesGuid)" +
            //            "or (SubjectDictGuid in @CheckedTreeNodesGuid) or (TermDictGuid in @CheckedTreeNodesGuid) or (ClassTypeDictGuid in @CheckedTreeNodesGuid))";
            //        _parameters.Add("@CheckedTreeNodesGuid", ChildNodeGuidList);
            //    }
            //}
            if ((search.CheckedNodeGuids != null) && (search.CheckedNodeGuids.Count > 0))
            {
                List<Guid> listGuid = new List<Guid>();
                foreach(var str in search.CheckedNodeGuids)
                {
                    if ((!string.IsNullOrEmpty(str)) && (str.Length == 36))
                    {
                        listGuid.Add(new Guid(str));
                    }
                }
                if (listGuid.Count > 0)
                {
                    strWhere += " AND ((ProductTypeGuid in @CheckedTreeNodesGuid))";
                    _parameters.Add("@CheckedTreeNodesGuid", listGuid.ToArray());
                }
            }
            #endregion

            string strSql = @"(SELECT * FROM T_POC_Product
                            WHERE 1=1 "+ strWhere + ") as t";

            string SqlOrder = " ORDER BY t.Id desc";
            if (!string.IsNullOrWhiteSpace(search.SortName))
            {
                SqlOrder = " ORDER BY t." + search.SortName + " " + search.SortOrder;
            }

            string strQuery = "SELECT * FROM " + strSql + " " + SqlOrder;

            //resultInfo.allCount = GetPageCount<T_POC_Product>(EumDBName.OSC, strSql, "", new { DSI_Id = search.Id });
            resultInfo.RecordCount = GetPageCount<T_POC_Product>(EumDBName.POC, strSql, "", _parameters);
            var sql = $"{strQuery} OFFSET {(search.PageIndex - 1) * search.PageSize} ROW FETCH NEXT {search.PageSize} ROWS ONLY";

            resultInfo.ReusltList = GetInfos<T_POC_Product>(sql, _parameters).AsList();

            return resultInfo;
        }

        List<Guid> ChildNodeGuidList = new List<Guid>();
        /// <summary>
        /// 遍历获取子节点Guid
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="CheckedTreeNodes"></param>
        /// <returns></returns>
        private void GetChildNodeGuid(DictTreeResponse parentNode, List<DictTreeResponse> CheckedTreeNodes)
        {
            foreach (var item in CheckedTreeNodes)
            {
                if (!string.IsNullOrEmpty(item.value))
                {
                    ChildNodeGuidList.Add(new Guid(item.value));//添加字典唯一Guid
                    if (item.value.ToUpper() == parentNode.value.ToUpper())
                    {
                        GetChildNodeGuid(item, parentNode.children);
                    }
                }
            }
        }

        //public static void FindEvery(TreeView tv, List<DictTreeResponse> CheckedTreeNodes, Guid parentGuid)
        //{
        //    if (CheckedTreeNodes.Count > 0)
        //    {
        //        foreach (var item in CheckedTreeNodes)
        //        {
        //            if (item.value.ToUpper() == parentGuid.ToString().ToUpper())
        //            {
        //                tv.SelectedNode = item;
        //                //tv.SelectedNode.Expand();//展开找到的节点
        //                //tv.SelectedNode.BackColor = System.Drawing.Color.LightGray;//谁知道在Node失去选中状态时，如何取消掉这个BackColor的，请留言评论
        //                //return;//找到一个就返回，没有return则继续查找 直到遍历所有节点
        //            }
        //            FindEvery(tv, item.Nodes, parentGuid);
        //        }
        //    }
        //}


        /// <summary>
        /// 来源系统产品体系--课程信息表
        /// </summary>
        /// <param name="fkProductCourseGuid"></param>
        /// <returns></returns>
        public EXTCoursePageInfoResponse GetEXTCourseByPage(POCEXTCourseRequest search)
        {
            EXTCoursePageInfoResponse resultInfo = new EXTCoursePageInfoResponse();
            string strWhere = string.Empty;
            var _parameters = new DynamicParameters();
            #region 查询条件
            if (search.FKProductCourseGuid != Guid.Empty)
            {
                strWhere += " AND FKProductCourseGuid=@FKProductCourseGuid";
                _parameters.Add("@FKProductCourseGuid", search.FKProductCourseGuid);
            }
            if ((search.ExtractStatus == EXTCourseExtractStatusEnum.HasAssociated) ||
                (search.ExtractStatus == EXTCourseExtractStatusEnum.HasExtract) ||
                (search.ExtractStatus == EXTCourseExtractStatusEnum.WaitExtract))
            {
                strWhere += " AND ExtractStatus=@ExtractStatus";
                _parameters.Add("@ExtractStatus", search.ExtractStatus);
            }

            #endregion

            string strSql = @"(SELECT * FROM T_EXT_Course
                            WHERE 1=1 " + strWhere + ") as t";

            string SqlOrder = " ORDER BY t.Id desc";
            if (!string.IsNullOrWhiteSpace(search.SortName))
            {
                SqlOrder = " ORDER BY t." + search.SortName + " " + search.SortOrder;
            }

            string strQuery = "SELECT * FROM " + strSql + " " + SqlOrder;

            //resultInfo.allCount = GetPageCount<T_POC_Product>(EumDBName.OSC, strSql, "", new { DSI_Id = search.Id });
            resultInfo.RecordCount = GetPageCount<T_EXT_Course>(EumDBName.POC, strSql, "", _parameters);
            var sql = $"{strQuery} OFFSET {(search.PageIndex - 1) * search.PageSize} ROW FETCH NEXT {search.PageSize} ROWS ONLY";

            resultInfo.ReusltList = GetInfos<T_EXT_Course>(sql, _parameters);

            return resultInfo;
        }

        /// <summary>
        /// 来源系统产品体系--课程信息表 ViewModel
        /// </summary>
        /// <param name="fkProductCourseGuid"></param>
        /// <returns></returns>
        public VMEXTCoursePageInfoResponse GetVMEXTCourseByPage(POCEXTCourseRequest search)
        {
            VMEXTCoursePageInfoResponse resultInfo = new VMEXTCoursePageInfoResponse();
            string strWhere = string.Empty;
            var _parameters = new DynamicParameters();
            #region 查询条件
            if (search.FKProductCourseGuid != Guid.Empty)
            {
                strWhere += " AND FKProductCourseGuid=@FKProductCourseGuid";
                _parameters.Add("@FKProductCourseGuid", search.FKProductCourseGuid);
            }
            if ((search.ExtractStatus == EXTCourseExtractStatusEnum.HasAssociated) ||
                (search.ExtractStatus == EXTCourseExtractStatusEnum.HasExtract) ||
                (search.ExtractStatus == EXTCourseExtractStatusEnum.WaitExtract))
            {
                strWhere += " AND ExtractStatus=@ExtractStatus";
                _parameters.Add("@ExtractStatus", search.ExtractStatus);
            }

            #endregion

            string strSql = @"(SELECT i.ProductTypeName,(SELECT COUNT(1) FROM T_EXT_CourseRange r WHERE UPPER(r.CourseID)=UPPER(c.CourseID)) CampusCountName,
                               c.* FROM T_EXT_Course c 
                               LEFT JOIN T_EXT_ItemType i on c.FromSystem = i.FromSystem and UPPER(i.ProductTypeID)=UPPER(c.ProductTypeOneID)
                            WHERE 1=1 " + strWhere + ") as t";

            string SqlOrder = " ORDER BY t.Id desc";
            if (!string.IsNullOrWhiteSpace(search.SortName))
            {
                SqlOrder = " ORDER BY t." + search.SortName + " " + search.SortOrder;
            }

            string strQuery = "SELECT * FROM " + strSql + " " + SqlOrder;

            resultInfo.RecordCount = GetPageCount<VM_EXT_Course>(EumDBName.POC, strSql, "", _parameters);
            var sql = $"{strQuery} OFFSET {(search.PageIndex - 1) * search.PageSize} ROW FETCH NEXT {search.PageSize} ROWS ONLY";
            var queryInfo = GetInfos<VM_EXT_Course>(EumDBName.POC, sql, _parameters);
            resultInfo.ReusltList = queryInfo;

            return resultInfo;
        }

        /// <summary>
        /// 获取产品最大的编号
        /// </summary>
        /// <returns></returns>
        public string GetMaxProductNo()
        {
            string _strSql = "select top 1 [ProductNo] from [dbo].[T_POC_Product] order by Id desc";
            return GetInfos<string>(EumDBName.POC, _strSql.ToString(), null).FirstOrDefault();
        }


    }
}

