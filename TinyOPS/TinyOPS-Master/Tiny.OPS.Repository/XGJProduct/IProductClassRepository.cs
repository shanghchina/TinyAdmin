using Tiny.Common.Dapper.Repository;
using Tiny.OPS.Contract;
using Tiny.OPS.Domain.XGJProduct;
using System.Collections.Generic;

namespace Tiny.OPS.Repository
{
    public interface IProductClassRepository : IRepository
    {
        IList<T_EXT_Class> GetInfoByGuids(int from, params string[] guids);

        /// <summary>
        /// 获取分页班级信息列表
        /// </summary>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        VMEXTClassPageInfoResponse GetEXTClassByPage(EXTClassRequest search);
    }
}
