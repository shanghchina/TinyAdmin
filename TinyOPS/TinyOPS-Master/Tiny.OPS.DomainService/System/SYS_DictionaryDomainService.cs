using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Entity;
using Tiny.Common.Dapper.Persistence.UnitOfWork;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain;
using Tiny.OPS.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.DomainService
{
    public class SYS_DictionaryDomainService : RealDomainService<T_SYS_Dictionary>, ISYS_DictionaryDomainService
    {
        public ISYS_DictionaryRepository dictionaryRepository => IoC.Resolve<ISYS_DictionaryRepository>();
        /// <summary>
        /// 获取字典集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetDictionaryResponse GetDictionary(GetDictionaryRequest request)
        {
            GetDictionaryResponse response = new GetDictionaryResponse();
            List<DictionaryItem> list = dictionaryRepository.GetDictionary(request);
            response.dataList = list;
            return response;
        }

        List<DictTreeResponse> responseTreeNodeList = new List<DictTreeResponse>();
        /// <summary>
        /// 获取参数 课程所属年级、课程所属班型、课程所属期段、课程所属类型、课程所属科目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<DictTreeResponse> GetPOCDictParams()
        {
            AddRootData();
            return responseTreeNodeList;
        }

        /// <summary>
        /// 添加根节点
        /// </summary>
        /// <param name="parentData"></param>
        /// <param name="parentGuid"></param>
        /// <returns></returns>
        private void AddRootData()
        {
            //获取根节点
            GetDictionaryRequest requestRoot = new GetDictionaryRequest();
            requestRoot.DictGuid.Add(new Guid("2E9AED60-7EF3-477D-ABE3-0541F7DA3501"));
            requestRoot.DictGuid.Add(new Guid("D7459004-5E26-43C7-B066-7F93F29D9A34"));
            requestRoot.DictGuid.Add(new Guid("1A930526-9F51-41BC-AC35-C890D0B60918"));
            requestRoot.DictGuid.Add(new Guid("0E0E674D-BBDF-4C3E-85AE-362E8FA5F3D5"));
            requestRoot.DictGuid.Add(new Guid("AA98545B-495F-46FE-AA90-25C418E96C8C"));
            List<DictionaryItem> listDictRoot = dictionaryRepository.GetDictionary(requestRoot);//获取根节点
            foreach (var item in listDictRoot)
            {
                DictTreeResponse dtNode = new DictTreeResponse();
                dtNode.value = item.DictGuid.ToString();//字典表key
                dtNode.label = item.SysDictValue;//字典显示名称
                responseTreeNodeList.Add(dtNode);
                AddChildData(dtNode, item.DictGuid);
            }
        }

        /// <summary>
        /// 递归加载获取子节点
        /// </summary>
        /// <param name="list"></param>
        /// <param name="parentGuid"></param>
        /// <returns></returns>
        private void AddChildData(DictTreeResponse parentNode, Guid parentGuid)
        {
            //获取子节点
            GetDictionaryRequest requestRoot = new GetDictionaryRequest();
            requestRoot.ParentGuid.Add(parentGuid);
            List<DictionaryItem> listDictChild = dictionaryRepository.GetDictionary(requestRoot);

            foreach (var item in listDictChild)
            {
                DictTreeResponse dtNode = new DictTreeResponse();
                if (item.ParentGuid == parentGuid)
                {
                    dtNode.value = item.DictGuid.ToString();//字典表key
                    dtNode.label = item.SysDictValue;//字典显示名称
                    parentNode.children.Add(dtNode);
                    AddChildData(dtNode, item.DictGuid);
                }
            }
        }

        /// <summary>
        /// 获取POC自定义参数大类
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetVMDictionaryResponse GetPOCBasicParamsParentList(GetDictionaryRequest request)
        {
            //获取根节点
            request.DictGuid.Add(new Guid("2E9AED60-7EF3-477D-ABE3-0541F7DA3501"));
            request.DictGuid.Add(new Guid("D7459004-5E26-43C7-B066-7F93F29D9A34"));
            request.DictGuid.Add(new Guid("1A930526-9F51-41BC-AC35-C890D0B60918"));
            request.DictGuid.Add(new Guid("0E0E674D-BBDF-4C3E-85AE-362E8FA5F3D5"));
            request.DictGuid.Add(new Guid("AA98545B-495F-46FE-AA90-25C418E96C8C"));

            GetVMDictionaryResponse response = new GetVMDictionaryResponse();
            List<VM_SYS_Dictionary> list = dictionaryRepository.GetParentDictionary(request);
            response.ReusltList = list;
            return response;
        }

        /// <summary>
        /// 获取POC自定义参数明细参宿
        /// </summary>
        /// <param name="parentGuid">父节点guid</param>
        /// <returns></returns>
        public GetVMDictionaryResponse getPOCBasiceChildParamsList(Guid parentGuid)
        {
            GetDictionaryRequest request = new GetDictionaryRequest();
            request.ParentGuid.Add(parentGuid);

            GetVMDictionaryResponse response = new GetVMDictionaryResponse();
            List<VM_SYS_Dictionary> list = dictionaryRepository.GetChildDictionaryList(request);
            response.ReusltList = list;
            return response;
        }

        /// <summary>
        /// 新增修改明细字典内容
        /// </summary>
        /// <param name="viewModel"></param>
        public UnitOfWorkResult SaveOrUpdateChild(SaveVMDictionaryRequest viewModel)
        {
            //var result = UnitOfWorkResult.GetCurrentUow();
            //父类 参数名称：必输，最大字符20
            //参数描述：非必输，最大字符50
            //参数选项：选项名称必输，最大字段20，选项描述非必输，最大字符50
            //排序：必输，正整数

            //校验长度
            //校验是否已经存在映射关系
            StringBuilder errormsg = new StringBuilder();
            if (viewModel != null)
            {
                foreach (var item in viewModel.selectOptions)
                {
                    //校验长度
                    if (string.IsNullOrEmpty(item.SysDictValue))
                    {
                        errormsg.AppendFormat("参数名称必填");
                    }
                    else
                    {
                        if (item.SysDictValue.Length > 20)
                        {
                            errormsg.AppendFormat("参数名称最大字符20");
                        }
                    }
                    if ((!string.IsNullOrEmpty(item.SysDictDesc)) && (item.SysDictDesc.Length > 50))
                    {
                        errormsg.AppendFormat("参数描述最大字符50");
                    }
                }

                List<VM_SYS_Dictionary> updateDictList = new List<VM_SYS_Dictionary>();//记录修改的节点

                //获取当前字典类型的所有子节点，循环判断新增或修改
                //var childDbDictList = this.getPOCBasiceChildParamsList(new Guid(viewModel.parentGuid));
                GetDictionaryRequest request = new GetDictionaryRequest();
                request.ParentGuid.Add(new Guid(viewModel.parentGuid));
                List<T_SYS_Dictionary> childDbDictList = dictionaryRepository.GetDictEntityList(request);

                int iOrderNo = 1;//排序号
                foreach (var itemDb in childDbDictList)
                {
                    iOrderNo++;
                    var findItem = viewModel.selectOptions.Find(t => t.DictGuid == itemDb.DictGuid);
                    if (findItem != null)//修改
                    {
                        //判断新增或修改或删除
                        T_SYS_Dictionary dictnew = new T_SYS_Dictionary();
                        dictnew.Id = findItem.Id;
                        dictnew.DictGuid = findItem.DictGuid;
                        dictnew.ParentGuid = new Guid(viewModel.parentGuid);//父节点Guid
                        dictnew.SysDictKey = dictnew.DictGuid.ToString();
                        dictnew.SysDictValue = findItem.SysDictValue;
                        dictnew.SysDictDesc = findItem.SysDictDesc;
                        dictnew.SysDictSort = iOrderNo;
                        dictnew.SysCatalogCode = viewModel.vmParentDict.SysCatalogCode;
                        dictnew.SysCatalogName = viewModel.vmParentDict.SysCatalogName;
                        dictnew.SysIsCatalog = false;
                        dictnew.SysIsEnabled = true;
                        dictnew.SysIsEdit = true;

                        dictnew.UpdaterUserId = findItem.UpdaterUserId;
                        dictnew.UpdaterUserName = findItem.UpdaterUserName;
                        dictnew.CreatedDate = findItem.CreatedDate;
                        dictnew.UpdateDate = DateTime.Now;
                        dictnew.SaveType = SaveType.Modify;
                        var uowSave = Save(dictnew);

                        updateDictList.Add(findItem);
                    }
                    else
                    {
                        //校验是否已经存在映射关系
                        if (this.CheckHasMappedBasciData(itemDb.DictGuid))
                        {
                            errormsg.AppendFormat($"{itemDb.SysDictValue}已存在映射关系的数据，不能删除！");
                        }
                        //校验是否已经存在映射关系
                        if (this.CheckIsExtractorBasciData(itemDb.DictGuid))
                        {
                            errormsg.AppendFormat($"{itemDb.SysDictValue}提取正在使用，不能删除！");
                        }
                        //删除没在界面上存在
                        var uowDel = Remove(itemDb);
                    }
                }

                if (!string.IsNullOrWhiteSpace(errormsg.ToString()))
                {
                    throw new Exception(errormsg.ToString());
                }

                foreach (var item in viewModel.selectOptions)
                {
                    var findItem = updateDictList.Find(t => t.DictGuid == item.DictGuid);
                    if (findItem == null)//没找到的是要新增的节点
                    {
                        iOrderNo++;
                        T_SYS_Dictionary dictnew = new T_SYS_Dictionary();
                        dictnew.DictGuid = Guid.NewGuid();
                        dictnew.ParentGuid = new Guid(viewModel.parentGuid);//父节点Guid
                        dictnew.SysDictKey = dictnew.DictGuid.ToString();
                        dictnew.SysDictValue = item.SysDictValue;
                        dictnew.SysDictDesc = item.SysDictDesc;
                        dictnew.SysDictSort = iOrderNo;
                        dictnew.SysCatalogCode = viewModel.vmParentDict.SysCatalogCode;
                        dictnew.SysCatalogName = viewModel.vmParentDict.SysCatalogName;
                        dictnew.SysIsCatalog = false;
                        dictnew.SysIsEnabled = true;
                        dictnew.SysIsEdit = true;

                        dictnew.UpdaterUserId = "";
                        dictnew.UpdaterUserName = "";
                        dictnew.CreatedDate = DateTime.Now;
                        dictnew.UpdateDate = DateTime.Now;
                        dictnew.SaveType = SaveType.Add;
                        var uowAdd= Add(dictnew);
                    }
                }
            }

            //父节点赋值
            viewModel.vmParentDict.SysDictValue = viewModel.SysDictValue;
            viewModel.vmParentDict.SysDictDesc = viewModel.SysDictDesc;
            viewModel.vmParentDict.SysDictSort = viewModel.SysDictSort;
            var uowParent = dictionaryRepository.UpdateParentDict(viewModel.vmParentDict);

            var result = UnitOfWorkResult.GetCurrentUow();
            //throw new NotImplementedException();
            return result;
        }

        /// <summary>
        /// 判断该字典已经映射过了，就不能删除了
        /// </summary>
        /// <param name="dictGuid"></param>
        /// <returns></returns>
        public bool CheckHasMappedBasciData(Guid dictGuid)
        {
            return dictionaryRepository.CheckHasMappedBasciData(dictGuid);
        }

        /// <summary>
        /// 已有待提取的提取器在使用此参数，提示：提取器正在使用，不能删除
        /// </summary>
        /// <param name="dictGuid"></param>
        /// <returns></returns>
        public bool CheckIsExtractorBasciData(Guid dictGuid)
        {
            return dictionaryRepository.CheckIsExtractorBasciData(dictGuid);
        }

    }
}

