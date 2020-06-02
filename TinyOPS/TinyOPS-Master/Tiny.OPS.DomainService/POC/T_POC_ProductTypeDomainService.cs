using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using Tiny.OPS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiny.OPS.DomainService
{
    /// <summary>
    /// 产品分类
    /// </summary>
	public class T_POC_ProductTypeDomainService : RealDomainService<T_POC_ProductType>, IT_POC_ProductTypeDomainService
    {
        public IT_POC_ProductTypeRepository pocProductTypeRepository => IoC.Resolve<IT_POC_ProductTypeRepository>();

        public IProductItemTypeRepository pocProductItemTypeRepository => IoC.Resolve<IProductItemTypeRepository>();


        public IT_POC_ProductTypeMapRepository pocProductTypeMapRepository => IoC.Resolve<IT_POC_ProductTypeMapRepository>();



        /// <summary>
        /// 获取父级产品类别下拉框选项
        /// </summary>
        /// <returns></returns>
        public List<T_POC_ProductType> GetChildProductType(Guid productTypeGuid)
        {
            return pocProductTypeRepository.GetChildProductType(productTypeGuid);
        }

        /// <summary>
        /// 获取子级产品类别下拉框选项
        /// </summary>
        /// <param name="productTypeGuid"></param>
        /// <returns></returns>
        public List<T_POC_ProductType> GetLevelOneProductType()
        {
            return pocProductTypeRepository.GetLevelOneProductType();
        }
        /// <summary>
        /// 获取产品分类级联列表
        /// </summary>
        /// <returns></returns>
        public List<ProductTypelevelResponse> GetProductTypelevel()
        {
            var productTypeList = pocProductTypeRepository.GetProductType();
            var fatherTypeList = productTypeList.Where(x => x.ParentGuid == Guid.Parse("00000000-0000-0000-0000-000000000000"));
            List<ProductTypelevelResponse> list = new List<ProductTypelevelResponse>();
            foreach (var data in fatherTypeList)
            {
                ProductTypelevelResponse model = new ProductTypelevelResponse();
                model.value = data.ProductTypeGuid.ToString();
                model.label = data.ProductTypeName;
                model.children = GetChild(data.ProductTypeGuid, productTypeList);
                list.Add(model);
            }
            return list;
        }
        /// <summary>
        /// 获取递归产品分类部分数据
        /// </summary>
        /// <returns></returns>

        public List<ProductTypelevelResponse> GetChild(Guid id, List<T_POC_ProductType> productTypeList)
        {
            List<ProductTypelevelResponse> childrenList = new List<ProductTypelevelResponse>();
            var childs = productTypeList.Where(x => x.ParentGuid == id);
            foreach (var item in childs)
            {
                ProductTypelevelResponse father = new ProductTypelevelResponse();
                father.label = item.ProductTypeName;
                father.value = item.ProductTypeGuid.ToString();

                father.children = GetChild(item.ProductTypeGuid, productTypeList);
                childrenList.Add(father);

            }
            if (childs.Count() <= 0)
                return null;
            return childrenList;
        }

        /// <summary>
        /// 获取产品分类列表(递归不分页)
        /// </summary>
        /// <returns></returns>
        public List<ProductTypeLevelAllInfoResponse> GetProductTypeList()
        {
            var productTypeList = pocProductTypeRepository.GetProductType();
            var fatherTypeList = productTypeList.Where(x => x.ParentGuid == Guid.Parse("00000000-0000-0000-0000-000000000000"));
            List<ProductTypeLevelAllInfoResponse> list = new List<ProductTypeLevelAllInfoResponse>();
            foreach (var data in fatherTypeList)
            {
                ProductTypeLevelAllInfoResponse model = new ProductTypeLevelAllInfoResponse();
                List<string> li = new List<string>();
                li.Add(data.ProductTypeName);
                model.id = data.Id;
                model.isStart = data.IsEnabled;
                model.level = data.ProductTypeLevelName;
                model.levelSort = data.ProductTypeLevelNo;
                model.productTypeGuid = data.ProductTypeGuid;
                model.showLevel = li;
                model.remark = data.Remark;
                model.className = data.ProductTypeName;
                model.createdDate = data.CreatedDate;
                model.parentGuid = data.ParentGuid;
                model.children = GetChildAllIno(data.ProductTypeGuid, productTypeList);
                list.Add(model);
            }
            return list;

        }

        /// <summary>
        /// 获取递归产品分类全部数据
        /// </summary>
        /// <returns></returns>
        public List<ProductTypeLevelAllInfoResponse> GetChildAllIno(Guid id, List<T_POC_ProductType> productTypeList)
        {
            List<ProductTypeLevelAllInfoResponse> childrenList = new List<ProductTypeLevelAllInfoResponse>();
            var childs = productTypeList.Where(x => x.ParentGuid == id);
            foreach (var item in childs)
            {
                ProductTypeLevelAllInfoResponse father = new ProductTypeLevelAllInfoResponse();
                List<string> li = new List<string>();
                li.Add(item.ProductTypeName);
                father.id = item.Id;
                father.isStart = item.IsEnabled;
                father.level = item.ProductTypeLevelName;
                father.levelSort = item.ProductTypeLevelNo;
                father.productTypeGuid = item.ProductTypeGuid;
                father.remark = item.Remark;
                father.className = item.ProductTypeName;
                father.createdDate = item.CreatedDate;
                father.parentGuid = item.ParentGuid;
                father.parentName = productTypeList.Where(x => x.ProductTypeGuid == item.ParentGuid).FirstOrDefault().ProductTypeName;
                ShowLevel(item.ParentGuid, productTypeList, li);
                father.showLevel = li;
                father.children = GetChildAllIno(item.ProductTypeGuid, productTypeList);
                childrenList.Add(father);

            }
            if (childs.Count() <= 0)
                return null;
            return childrenList;
        }
        public List<string> ShowLevel(Guid id, List<T_POC_ProductType> productTypeList, List<string> levelInfo)
        {

            var node = productTypeList.Where(x => x.ProductTypeGuid == id).FirstOrDefault();
            if (node != null)
            {
                levelInfo.Add(node.ProductTypeName);
                ShowLevel(node.ParentGuid, productTypeList, levelInfo);
               
            }
            return levelInfo;
        }
        /// <summary>
        /// 操作产品分类
        /// </summary>
        /// <param name="request"></param>
        public void OperateProductType(T_POC_ProductType request)
        {
            request.UpdateDate = DateTime.Now;
            if (request.ProductTypeGuid == Guid.Parse("00000000-0000-0000-0000-000000000000"))//新增
            {
                request.ProductTypeGuid = Guid.NewGuid();
                pocProductTypeRepository.AddProductType(request);
            }
            else//修改
            {
                pocProductTypeRepository.UpdateProductType(request);
            }
        }
        /// <summary>
        /// 删除产品分类
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool DeleteProductType(Guid productTypeId)
        {
            bool hasProductType = pocProductTypeRepository.HasProductTypeChildren(productTypeId);
            bool hasMappingProductType = pocProductTypeMapRepository.HasMappingProductType(productTypeId);
            if (hasProductType || hasMappingProductType)
            {

                return false;
            }
            else
            {
                pocProductTypeRepository.DeleteProductType(productTypeId);
                return true;
            }

        }
        /// <summary>
        /// 查看产品类别映射关系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ProductTypeMappingListResponse SearchProductTypeList(ProductTypeMappingListRequest request)
        {
            int total = 0;
            ProductTypeMappingListResponse response = new ProductTypeMappingListResponse();
            response.DataList = pocProductItemTypeRepository.GetProductTypeList(request, out total);
            response.Total = total;
            return response;
        }

        /// <summary>
        /// 产品分类启用开关
        /// </summary>
        /// <param name="request"></param>
        public void OperateSwitch(OperateSwitchRequest request)
        {
            pocProductTypeRepository.OperateSwitch(request);
        }

    }
}

