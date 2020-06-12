using System.Linq;
using System.Collections.Generic;
using TinyEdu.Cache.Factory;
using TinyEdu.Entity.SystemManage;
using TinyEdu.Service.SystemManage;
using System.Threading.Tasks;

namespace TinyEdu.Business.Cache
{
    public class AreaCache
    {
        private string cacheKey = typeof(AreaCache).Name;

        private AreaService areaService = new AreaService();

        public async Task<List<AreaEntity>> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<List<AreaEntity>>(cacheKey);
            if (cacheList == null)
            {
                var result = await areaService.GetList(null);
                var data = result.ToList();
                CacheFactory.Cache().AddCache(cacheKey, data);
                return data;
            }
            else
            {
                return cacheList;
            }
        }

        public void UpdateRow(long id)
        {
            //var cacheList = CacheFactory.Cache().GetCache<IEnumerable<AreaEntity>>(cacheKey);
            //if (cacheList != null)
            //{
            //    cacheList.Except(cacheList.Where(p => p.Id == id));
            //}
        }

        public void RemoveRow(long id)
        {

        }

    }
}
