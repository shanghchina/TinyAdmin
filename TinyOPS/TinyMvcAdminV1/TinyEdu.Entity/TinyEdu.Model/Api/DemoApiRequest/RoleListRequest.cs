using TinyEdu.Model.Param.SystemManage;
using TinyEdu.Util.Model;

namespace TinyEdu.Model
{
    /// <summary>
    /// role请求参数
    /// </summary>
    public class RoleListRequest
    {
        public RoleListParam param { get; set; }
        public Pagination pagination { get; set; }
    }

}
