
using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Persistence.UnitOfWork;
using Tiny.Common.Dapper.Service;
using Tiny.Common.Extensions;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using Tiny.OPS.Domain.XGJProduct;
using Tiny.OPS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiny.OPS.DomainService
{
    public class T_POC_ExtractorDetailDomainService : RealDomainService<T_POC_ExtractorDetail>, IT_POC_ExtractorDetailDomainService
    {
        IT_POC_ExtractorDetailRepository pocExtractorDetailRepository = IoC.Resolve<IT_POC_ExtractorDetailRepository>();
        IT_POC_ExtractorDomainService pocExtractorDomainService = IoC.Resolve<IT_POC_ExtractorDomainService>();
        IT_POC_ExtractorQueryDomainService pocExtractorQueryDomainService = IoC.Resolve<IT_POC_ExtractorQueryDomainService>();
        IT_POC_ExtractorQueryRepository pocExtractorQueryRepository = IoC.Resolve<IT_POC_ExtractorQueryRepository>();
        IT_POC_ProductRepository pocProductRepository = IoC.Resolve<IT_POC_ProductRepository>();
        IT_POC_ProductDomainService pocProductDomainService = IoC.Resolve<IT_POC_ProductDomainService>();
        IT_POC_ProductTypeMapRepository pocProductTypeMapRepository = IoC.Resolve<IT_POC_ProductTypeMapRepository>();
        IProductCourseDomainService productCourseDomainService = IoC.Resolve<IProductCourseDomainService>();
        ISYS_DictionaryDomainService dictionaryDomainService => IoC.Resolve<ISYS_DictionaryDomainService>();
        IT_POC_ProductTypeRepository pocProductTypeRepository => IoC.Resolve<IT_POC_ProductTypeRepository>();



        private static object objlock = new object();
        private static object objlock1 = new object();

        /// <summary>
        /// 获取提取器列表
        /// </summary>
        /// <returns></returns>
        public GetExtractorDetailListResponse GetExtractorDetaiList(GetExtractorDetaiListRequest request)
        {
            //获取字典
            GetDictionaryRequest req = new GetDictionaryRequest();
            req.ParentGuid = new List<Guid>() { new Guid("2E9AED60-7EF3-477D-ABE3-0541F7DA3501"), new Guid("D7459004-5E26-43C7-B066-7F93F29D9A34"), new Guid("1A930526-9F51-41BC-AC35-C890D0B60918"), new Guid("0E0E674D-BBDF-4C3E-85AE-362E8FA5F3D5"), new Guid("AA98545B-495F-46FE-AA90-25C418E96C8C") };
            List<DictionaryItem> dicList = dictionaryDomainService.GetDictionary(req).dataList;
            //获取提取器详细
            GetExtractorDetailListResponse response = pocExtractorDetailRepository.GetExtractorDetaiList(request);
            if (response != null && response.dataList != null && response.dataList.Count > 0)
            {
                foreach (var item in response.dataList)
                {
                    item.ExtractorStatusName = EnumExtension.ToEnumTypeDescriptionByID(typeof(ExtractorStatusEnum), item.ExtractorStatus.ToString());
                    if (dicList != null && dicList.Count > 0)
                    {
                        item.GradeName = dicList.Where(t => t.DictGuid.ToString().ToLower() == item.GradeID.ToLower()).FirstOrDefault().SysDictValue;
                        item.SubjectName = dicList.Where(t => t.DictGuid.ToString().ToLower() == item.SubjectID.ToLower()).FirstOrDefault().SysDictValue;
                        item.CategoryName = dicList.Where(t => t.DictGuid.ToString().ToLower() == item.CategoryID.ToLower()).FirstOrDefault().SysDictValue;
                        item.TermName = dicList.Where(t => t.DictGuid.ToString().ToLower() == item.TermID.ToLower()).FirstOrDefault().SysDictValue;
                        item.ClassTypeName = dicList.Where(t => t.DictGuid.ToString().ToLower() == item.ClassTypeID.ToLower()).FirstOrDefault().SysDictValue;
                    }
                }

            }
            return response;

        }

        /// <summary>
        /// 提取器预览判断
        /// </summary>
        /// <param name="request"></param>
        public PreviewJudgeExtractorResponse PreviewJudgeExtractor(PreviewJudgeExtractorRequest request)
        {
            PreviewJudgeExtractorResponse response = new PreviewJudgeExtractorResponse();
            response.dataList = new List<ExtractorItem>();
            if (request != null && request.dataList != null && request.dataList.Count > 0)
            {
                foreach (var item in request.dataList)
                {
                    response.dataList.Add(item);
                    var model = pocExtractorDetailRepository.GetSingleExtractor(item);
                    if (model != null)
                    {
                        item.extractorNo = model.extractorNo;
                        item.extractorStatus = model.extractorStatus;
                        item.extractorStatusName = EnumExtension.ToEnumTypeDescriptionByID(typeof(ExtractorStatusEnum), model.extractorStatus.ToString());

                    }
                    else
                    {
                        item.extractorNo = "";
                        item.extractorStatus = (int)ExtractorStatusEnum.WaitExtract;
                        item.extractorStatusName = "待提取";

                    }
                }
            }
            return response;
        }

        /// <summary>
        /// 创建提取器
        /// </summary>
        /// <param name="request"></param>
        public bool CreateExtractor(CreateExtractorRequest request)
        {
            lock (objlock)
            {
                if (request != null && request.dataList != null && request.dataList.Count > 0)
                {
                    //提取器主表
                    T_POC_Extractor pocExtractor = new T_POC_Extractor();
                    pocExtractor.ExtractorGuid = Guid.NewGuid();
                    pocExtractor.UpdaterUserId = request.dataList[0].UpdateUserId;
                    pocExtractor.UpdaterUserName = request.dataList[0].UpdateUserName;
                    pocExtractor.UpdateDate = DateTime.Now;
                    pocExtractor.CreatedDate = DateTime.Now;
                    //获取当前最大的提取器编号
                    string maxExtractorNo = pocExtractorDetailRepository.GetMaxExtractorNo();
                    string extractorNo = "000000";
                    if (!string.IsNullOrEmpty(maxExtractorNo))
                    {
                        extractorNo = maxExtractorNo.Substring(2);
                    }

                    //提取器头部查询条件
                    List<T_POC_ExtractorQuery> pocExtractorQueryList = new List<T_POC_ExtractorQuery>();
                    //提取器明细
                    List<T_POC_ExtractorDetail> pocExtractorDetailList = new List<T_POC_ExtractorDetail>();

                    foreach (var item in request.dataList)
                    {
                        pocExtractorQueryList = new List<T_POC_ExtractorQuery>();
                        foreach (var item1 in item.fromSystem)
                        {
                            T_POC_ExtractorQuery queryModel = new T_POC_ExtractorQuery();
                            queryModel.FKExtractorGuid = pocExtractor.ExtractorGuid;
                            queryModel.ExtractorQueryGuid = Guid.NewGuid();
                            queryModel.SelectFieldTypeCode = "ExtFromSystem";
                            queryModel.SelectFieldTypeId = item1.ToString();
                            queryModel.SelectFieldTypeName = item.fromSystemName.Split(',')[item.fromSystem.IndexOf(item1)];
                            queryModel.UpdaterUserId = item.UpdateUserId;
                            queryModel.UpdaterUserName = item.UpdateUserName;
                            queryModel.UpdateDate = DateTime.Now;
                            queryModel.CreatedDate = DateTime.Now;
                            pocExtractorQueryList.Add(queryModel);
                        }
                        //事业部
                        foreach (var item1 in item.oneOrgId)
                        {
                            T_POC_ExtractorQuery queryModel = new T_POC_ExtractorQuery();
                            queryModel.FKExtractorGuid = pocExtractor.ExtractorGuid;
                            queryModel.ExtractorQueryGuid = Guid.NewGuid();
                            queryModel.SelectFieldTypeCode = "OneOrg";
                            queryModel.SelectFieldTypeId = item1.ToString();
                            queryModel.SelectFieldTypeName = item.oneOrgName.Split(',')[item.oneOrgId.IndexOf(item1)];
                            queryModel.UpdaterUserId = item.UpdateUserId;
                            queryModel.UpdaterUserName = item.UpdateUserName;
                            queryModel.UpdateDate = DateTime.Now;
                            queryModel.CreatedDate = DateTime.Now;
                            pocExtractorQueryList.Add(queryModel);
                        }
                        //产品状态
                        if (item.proStatue >= -2)
                        {
                            T_POC_ExtractorQuery queryModel = new T_POC_ExtractorQuery();
                            queryModel.FKExtractorGuid = pocExtractor.ExtractorGuid;
                            queryModel.ExtractorQueryGuid = Guid.NewGuid();
                            queryModel.SelectFieldTypeCode = "ProductStatus";
                            queryModel.SelectFieldTypeId = item.proStatue.ToString();
                            queryModel.SelectFieldTypeName = item.proStatueName;
                            queryModel.UpdaterUserId = item.UpdateUserId;
                            queryModel.UpdaterUserName = item.UpdateUserName;
                            queryModel.UpdateDate = DateTime.Now;
                            queryModel.CreatedDate = DateTime.Now;
                            pocExtractorQueryList.Add(queryModel);
                        }

                        //产品动销阈值
                        if (item.thresholdValue > 0)
                        {
                            T_POC_ExtractorQuery queryModel = new T_POC_ExtractorQuery();
                            queryModel.FKExtractorGuid = pocExtractor.ExtractorGuid;
                            queryModel.ExtractorQueryGuid = Guid.NewGuid();
                            queryModel.SelectFieldTypeCode = "ThresholdValue";
                            queryModel.SelectFieldTypeId = item.thresholdValue.ToString();
                            queryModel.SelectFieldTypeName = item.thresholdName;
                            queryModel.UpdaterUserId = item.UpdateUserId;
                            queryModel.UpdaterUserName = item.UpdateUserName;
                            queryModel.UpdateDate = DateTime.Now;
                            queryModel.CreatedDate = DateTime.Now;
                            pocExtractorQueryList.Add(queryModel);
                        }

                        var model = pocExtractorDetailRepository.GetSingleExtractor(item);
                        //创建提取器明细
                        if (model == null || model.extractorStatus == (int)ExtractorStatusEnum.Cancelled || model.extractorStatus == (int)ExtractorStatusEnum.Completed)
                        {
                            T_POC_ExtractorDetail pocExtractorDetail = new T_POC_ExtractorDetail();
                            pocExtractorDetail.FKExtractorGuid = pocExtractor.ExtractorGuid;
                            pocExtractorDetail.ExtractorDetailGuid = Guid.NewGuid();
                            pocExtractorDetail.ExtractorStatus = item.extractorStatus;
                            extractorNo = (Convert.ToInt32(extractorNo) + 1).ToString("000000");
                            pocExtractorDetail.ExtractorNo = "TQ" + extractorNo;
                            pocExtractorDetail.ProductCount = item.productCount;
                            pocExtractorDetail.ExtractCount = 0;
                            pocExtractorDetail.AssociatedCount = 0;
                            pocExtractorDetail.NotExtractCount = 0;
                            pocExtractorDetail.Year = item.year;
                            pocExtractorDetail.GradeID = item.gradeId;
                            pocExtractorDetail.GradeName = item.gradeName;
                            pocExtractorDetail.CategoryID = item.categoryId;
                            pocExtractorDetail.CategoryName = item.categoryName;
                            pocExtractorDetail.FlagID = "";
                            pocExtractorDetail.FlagName = "";
                            pocExtractorDetail.SubjectID = item.subjectId;
                            pocExtractorDetail.SubjectName = item.subjectName;
                            pocExtractorDetail.TermID = item.termId;
                            pocExtractorDetail.TermName = item.termName;
                            pocExtractorDetail.ClassTypeID = item.classTypeId;
                            pocExtractorDetail.ClassTypeName = item.classTypeName;
                            pocExtractorDetail.UpdaterUserId = item.UpdateUserId;
                            pocExtractorDetail.UpdaterUserName = item.UpdateUserName;
                            pocExtractorDetail.UpdateDate = DateTime.Now;
                            pocExtractorDetail.CreatedDate = DateTime.Now;
                            pocExtractorDetailList.Add(pocExtractorDetail);

                        }
                    }
                    //事务提交
                    UnitOfWorkResult result = new UnitOfWorkResult();
                    if (pocExtractorDetailList != null && pocExtractorDetailList.Count > 0)
                    {
                        result = pocExtractorDomainService.Add(pocExtractor);
                        foreach (var item in pocExtractorQueryList)
                        {
                            result = pocExtractorQueryDomainService.Add(item);
                        }
                        foreach (var item in pocExtractorDetailList)
                        {
                            result = Add(item);
                        }
                        return result.Commit();
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        ///取消提取器
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public bool CancleExtractor(CancleExtractorRequest request)
        {
            if (request.ExtractorGuids != null && request.ExtractorGuids.Count > 0)
            {
                return pocExtractorDetailRepository.CancleExtractor(request);
            }
            else
            {
                return false;

            }

        }

        /// <summary>
        /// 获取提取器信息
        /// </summary>
        /// <param name="ExtractorDetailGuid"></param>
        /// <returns></returns>
        public GetExtractorInfoResponse GetExtractorInfo(GetExtractorInfoRequest request)
        {
            GetExtractorInfoResponse response = new GetExtractorInfoResponse();
            //提取器详细信息
            T_POC_ExtractorDetail detail = pocExtractorDetailRepository.GetSingleByGuid(request.extractorDetailGuid);
            if (detail != null)
            {
                List<T_POC_ExtractorQuery> list = pocExtractorQueryRepository.GetListByExtractorGuid(detail.FKExtractorGuid);
                response.extractorDetail = detail;
                response.queryList = list;
            }
            return response;
        }

        /// <summary>
        /// 提取产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool ExtractProducts(ExtractProductRequest request)
        {
            lock (objlock1)
            {
                //获取当前最大的产品编号
                string maxProductNo = pocProductRepository.GetMaxProductNo();
                string productNo = "0000000";
                if (!string.IsNullOrEmpty(maxProductNo))
                {
                    productNo = maxProductNo.Substring(2);
                }
                //提取器提取的产品数量
                int extractCount = 0;
                //提取器提取的关联产品数量
                int associatedCount = 0;
                if (request != null && request.productList != null && request.productList.Count > 0)
                {

                    var productTypeList = pocProductTypeRepository.GetProductType();
                    UnitOfWorkResult result = new UnitOfWorkResult();
                    foreach (var item in request.productList)
                    {
                        item.ProductGuid = Guid.NewGuid();
                        productNo = (Convert.ToInt32(productNo) + 1).ToString("0000000");
                        item.ProductNo = "KC" + productNo;
                        //根据产品池产品类型获取产品中心产品类型
                        T_POC_ProductTypeMap map = pocProductTypeMapRepository.GetProductTypeMapByProductTypeID(item.ProductTypeTwoID);
                        if (map != null)
                        {
                            var fatherList = GetFatherList(map.FKProductTypeGuid, productTypeList);
                            if (fatherList != null && fatherList.Count > 0)
                            {
                                fatherList = fatherList.OrderBy(t => t.ProductTypeLevelNo).ToList();
                                item.ProductTypeGuid = map.FKProductTypeGuid;
                                item.ProductTypeName = fatherList.Last().ProductTypeName;
                                string lable = "";
                                foreach (var obj in fatherList)
                                {
                                    lable += obj.ProductTypeName + ">";
                                }
                                item.ProductTypeLable = lable.TrimEnd('>');
                            }
                        }
                        item.CreatedDate = DateTime.Now;
                        item.UpdateDate = DateTime.Now;
                        //添加到产品中心
                        extractCount += 1;
                        result = pocProductDomainService.Add(item);
                        //修改产品池的提取的产品状态
                        T_EXT_Course course = productCourseDomainService.GetInfoByGuid(item.FromSystem, item.CourseID);
                        if (course != null)
                        {

                            course.ExtractStatus = (int)ExtractState.Extracted;
                            course.FKProductCourseGuid = item.ProductGuid;
                            result = productCourseDomainService.Save(course);
                        }
                        //修改关联产品的状态
                        foreach (var id in item.RelationIds)
                        {
                            T_EXT_Course course1 = productCourseDomainService.GetInfoByGuid(item.FromSystem, id.ToString());
                            if (course1 != null)
                            {
                                associatedCount += 1;
                                course1.ExtractStatus = (int)ExtractState.Associated;
                                course1.FKProductCourseGuid = item.ProductGuid;
                                result = productCourseDomainService.Save(course1);
                            }
                        }
                    }
                    //修改提取器的状态
                    T_POC_ExtractorDetail detail = pocExtractorDetailRepository.GetSingleByGuid(request.productList[0].FKDetailGuid);
                    if (detail != null)
                    {
                        detail.ExtractorStatus = (int)ExtractorStatusEnum.Completed;
                        detail.ProductCount = request.totalCount;
                        detail.AssociatedCount = associatedCount;
                        detail.ExtractCount = extractCount;
                        detail.NotExtractCount = request.totalCount - associatedCount - extractCount;
                        detail.UpdateDate = DateTime.Now;
                        result = Save(detail);
                    }

                    return result.Commit();

                }
                else
                {
                    return false;
                }
            }

        }

        /// <summary>
        /// 根据产品类型Id获取所有上级信息
        /// </summary>
        /// <param name="list"></param>
        /// <param name="branchGuid"></param>
        /// <returns></returns>
        private List<T_POC_ProductType> GetFatherList(Guid productTypeGuid, List<T_POC_ProductType> productTypeList)
        {
            List<T_POC_ProductType> list = productTypeList;
            var query = list.Where(p => p.ProductTypeGuid == productTypeGuid).ToList();
            return query.ToList().Concat(query.ToList().SelectMany(t => GetFatherList(t.ParentGuid, list))).ToList();
        }

    }
}

