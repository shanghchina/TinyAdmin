using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Tiny.Common.Dapper.DI;
using Tiny.OPS.Common;
using Tiny.OPS.Contract;
using Tiny.OPS.Contract.POC;
using Tiny.OPS.Domain;
using Tiny.OPS.DomainService;
using Tiny.OPS.WebApi.Filter;


namespace Tiny.OPS.WebApi.Controllers
{    /// <summary>
     /// POC产品类别
     /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Description("POC产品类别")]
    public class ProductTypeController : BaseController
    {
        private IT_POC_ProductTypeDomainService PocProductDomainService => IoC.Resolve<IT_POC_ProductTypeDomainService>();
        private IProductItemTypeDomainService ProductItemTypeDomainService => IoC.Resolve<IProductItemTypeDomainService>();
        private IT_POC_ProductTypeMapDomainService ProductTypeMapDomainService => IoC.Resolve<IT_POC_ProductTypeMapDomainService>();

        /// <summary>
        /// 获取子级产品类别下拉框选项
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetLevelOneProductType()
        {
            try
            {
                List<T_POC_ProductType> response = PocProductDomainService.GetLevelOneProductType();
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 获取父级产品类别下拉框选项
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetChildProductType(ProductTypeSelectListRequest request)
        {
            try
            {
                List<T_POC_ProductType> response = PocProductDomainService.GetChildProductType(request.ProductTypeGuid);
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 获取产品分类映射分页列表
        /// </summary>
        [HttpPost]
        public BaseResponse GetPageInfoList(ProductTypeMapRequest request)
        {
            try
            {
                var response = ProductItemTypeDomainService.GetPageInfoList(request);
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 获取产品分类层级
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetProductTypelevel()
        {
            try
            {
                List<ProductTypelevelResponse> response = PocProductDomainService.GetProductTypelevel();
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("GetProductTypelevel--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 新增或修改产品分类映射
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse AddOrUpdateProductTypeMap(AddProductTypeMapRequest request)
        {
            try
            {
                var isSuccess = ProductTypeMapDomainService.AddOrUpdateProductTypeMap(request);
                if (isSuccess)
                    return ApiSuccessResult(isSuccess);
                else
                    return ApiErrorResult("保存失败");
            }
            catch (Exception ex)
            {
                _Log4Net.Error("AddOrUpdateProductTypeMap--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }


        /// <summary>
        /// 获取产品分类列表(递归不分页)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetProductTypeList()
        {
            try
            {
                List<ProductTypeLevelAllInfoResponse> response = PocProductDomainService.GetProductTypeList();
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("GetProductTypeList--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 新增或修改产品分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse OperateProductType(T_POC_ProductType request)
        {
            try
            {
                PocProductDomainService.OperateProductType(request);
                return ApiSuccessResult("");
            }
            catch (Exception ex)
            {
                _Log4Net.Error("OperateProductType--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }

        /// <summary>
        /// 产品分类启用开关
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public BaseResponse OperateSwitch(OperateSwitchRequest request)
        {
            try
            {
                PocProductDomainService.OperateSwitch(request);
                return ApiSuccessResult("");
            }
            catch (Exception ex)
            {
                _Log4Net.Error("OperateSwitch--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }
        /// <summary>
        /// 删除产品分类
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public BaseResponse DeleteProductType(DeleteProductTypeRequest request)
        {
            try
            {
                bool effect= PocProductDomainService.DeleteProductType(request.ProductTypeGuid);
                if (effect)
                {
                    return ApiSuccessResult("");
                }
                else
                {
                    return ApiErrorResult("删除失败,该产品分类已被使用");
                }
             
            }
            catch (Exception ex)
            {
                _Log4Net.Error("DeleteProductType--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }


        /// <summary>
        /// 获取产品分类映射列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse GetProductTypeMappingList(ProductTypeMappingListRequest request)
        {
            try
            {
                ProductTypeMappingListResponse response = PocProductDomainService.SearchProductTypeList(request);
                return ApiSuccessResult(response);
            }
            catch (Exception ex)
            {
                _Log4Net.Error("GetProductTypeMappingList--异常信息", ex);
                return ApiErrorResult(ex.Message);
            }
        }
    }
}
