using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TinyEdu.Util.Model;

namespace TinyEdu.Business.AutoJob
{
    public interface IJobTask
    {
        Task<TData> Start();
    }
}
