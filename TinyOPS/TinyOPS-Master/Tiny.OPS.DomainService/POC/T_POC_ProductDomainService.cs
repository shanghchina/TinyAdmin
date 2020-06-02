
using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using Tiny.OPS.Repository;
using System.Collections.Generic;

namespace Tiny.OPS.DomainService
{
    /// <summary>
    /// 
    /// </summary>
	public class T_POC_ProductDomainService : RealDomainService<T_POC_Product>, IT_POC_ProductDomainService
    {

        public IT_POC_ProductRepository pocProductRepository => IoC.Resolve<IT_POC_ProductRepository>();

        /// <summary>
        /// 获取分页产品信息列表
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        public POCProductPageInfoResponse GetPOCProductByPage(POCProductRequest search)
        {
            POCProductPageInfoResponse response = new POCProductPageInfoResponse();
            response = pocProductRepository.GetPOCProductByPage(search);
            return response;
        }

        /// <summary>
        /// 来源系统产品体系--课程信息表
        /// </summary>
        /// <param name="fkProductCourseGuid"></param>
        /// <returns></returns>
        public EXTCoursePageInfoResponse GetEXTCourseByPage(POCEXTCourseRequest search)
        {
            EXTCoursePageInfoResponse response = new EXTCoursePageInfoResponse();
            response = pocProductRepository.GetEXTCourseByPage(search);
            return response;
        }


        /// <summary>
        /// 获取分页产品信息列表 ViewModel
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        public VMPOCProductPageInfoResponse GetVMPOCProductByPage(POCProductRequest search)
        {
            VMPOCProductPageInfoResponse responseVM = new VMPOCProductPageInfoResponse();
            POCProductPageInfoResponse response = new POCProductPageInfoResponse();
            response = pocProductRepository.GetPOCProductByPage(search);
            var responseList = response.ReusltList;

            var listVM = responseList.MapTo<VM_POC_Product>();
            responseVM.ReusltList = listVM;
            return responseVM;
        }


        /// <summary>
        /// 来源系统产品体系--课程信息表 ViewModel
        /// </summary>
        /// <param name="fkProductCourseGuid"></param>
        /// <returns></returns>
        public VMEXTCoursePageInfoResponse GetVMEXTCourseByPage(POCEXTCourseRequest search)
        {
            VMEXTCoursePageInfoResponse response = new VMEXTCoursePageInfoResponse();
            response = pocProductRepository.GetVMEXTCourseByPage(search);
            return response;
        }
    }
}

