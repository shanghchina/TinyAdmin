using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using System;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
	public interface IT_POC_ProductRepository : IRepository
    {

        /// <summary>
        /// 获取分页产品信息列表
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        POCProductPageInfoResponse GetPOCProductByPage(POCProductRequest search);

        /// <summary>
        /// 来源系统产品体系--课程信息表
        /// </summary>
        /// <param name="fkProductCourseGuid"></param>
        /// <returns></returns>
        EXTCoursePageInfoResponse GetEXTCourseByPage(POCEXTCourseRequest search);


        /// <summary>
        /// 来源系统产品体系--课程信息表 ViewModel
        /// </summary>
        /// <param name="fkProductCourseGuid"></param>
        /// <returns></returns>
        VMEXTCoursePageInfoResponse GetVMEXTCourseByPage(POCEXTCourseRequest search);


        /// <summary>
        /// 获取产品的最大的编号
        /// </summary>
        /// <returns></returns>
        string GetMaxProductNo();
        
            
    }
}

