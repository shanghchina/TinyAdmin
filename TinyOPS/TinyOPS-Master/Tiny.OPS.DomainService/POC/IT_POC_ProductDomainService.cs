using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;


namespace Tiny.OPS.DomainService
{
	public interface IT_POC_ProductDomainService : IDomainService
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
        /// 获取分页产品信息列表ViewModel
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        VMPOCProductPageInfoResponse GetVMPOCProductByPage(POCProductRequest search);

        /// <summary>
        /// 来源系统产品体系--课程信息表 ViewModel
        /// </summary>
        /// <param name="fkProductCourseGuid"></param>
        /// <returns></returns>
        VMEXTCoursePageInfoResponse GetVMEXTCourseByPage(POCEXTCourseRequest search);
    }
}

