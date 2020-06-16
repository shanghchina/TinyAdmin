using TinyEdu.Model.Param.SystemManage;
using TinyEdu.Util.Model;

namespace TinyEdu.Model
{
    /// <summary>
    /// webapi请求参数
    /// </summary>
    public class DataDictDetailListRequest : BaseApiToken
    {

        public DataDictDetailListParam param { get; set; }

        public Pagination pagination { get; set; }
    }
}
