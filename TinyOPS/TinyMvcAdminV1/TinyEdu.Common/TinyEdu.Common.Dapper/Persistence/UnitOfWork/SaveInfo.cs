using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TinyEdu.Common.Dapper.Entity;
using TinyEdu.Common.Dapper.Persistence.Data;

namespace TinyEdu.Common.Dapper.Persistence.UnitOfWork
{
    public class SaveInfo
    {
        /// <summary>
        /// 排序
        /// </summary>
        public int Sequence { get; set; }
        /// <summary>
        /// 存储信息
        /// </summary>
        public BaseInfo Info { get; set; }
        /// <summary>
        /// 执行命令
        /// </summary>
        public string CommandText { set; get; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public CommandType CommandType { set; get; }
        /// <summary>
        /// Command参数
        /// </summary>
        public DataParameterInfo[] ParamsInfo { set; get; }
        /// <summary>
        /// 实体参数
        /// </summary>
        public object ParamEntityInfo { set; get; }
        /// <summary>
        /// 回调
        /// </summary>
        public Action<IList<dynamic>> CallBack { set; get; }

    }
}
