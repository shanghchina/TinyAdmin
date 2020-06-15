using TinyEdu.Model.Param.SystemManage;
using TinyEdu.Util.Model;

namespace TinyEdu.Model
{
    /// <summary>
    /// webapi请求参数
    /// </summary>
    public class DataDictListRequest : BaseApiToken
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        public string DictType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public DataDictListParam param { get; set; }

        public Pagination pagination { get; set; }
    }
}
