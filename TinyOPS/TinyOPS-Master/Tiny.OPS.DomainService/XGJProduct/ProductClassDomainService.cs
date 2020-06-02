using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Service;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain.XGJProduct;
using Tiny.OPS.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiny.OPS.DomainService
{
    public class ProductClassDomainService : RealDomainService<T_EXT_Class>, IProductClassDomainService
    {
        private IProductClassRepository ProductClassRepository => IoC.Resolve<IProductClassRepository>();

        public IList<T_EXT_Class> GetInfoByGuids(int from, params string[] guids)
        {
            return ProductClassRepository.GetInfoByGuids(from, guids);
        }

        /// <summary>
        /// 获取分页班级信息列表
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        public VMEXTClassPageInfoResponse GetEXTClassByPage(EXTClassRequest search)
        {
            VMEXTClassPageInfoResponse response = new VMEXTClassPageInfoResponse();
            response = ProductClassRepository.GetEXTClassByPage(search);
            return response;
        }
    }
}
