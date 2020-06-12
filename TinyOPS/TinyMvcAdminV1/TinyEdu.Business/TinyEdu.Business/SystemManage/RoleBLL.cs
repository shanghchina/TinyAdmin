using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyEdu.Entity;
using TinyEdu.Service;
using TinyEdu.Service.SystemManage;
using TinyEdu.Entity.SystemManage;
using TinyEdu.Util;
using TinyEdu.Util.Extension;
using TinyEdu.Model;
using TinyEdu.Util.Model;
using TinyEdu.Model.Param.SystemManage;
using TinyEdu.Enum.SystemManage;
using TinyEdu.Business.Cache;

namespace TinyEdu.Business.SystemManage
{
    public class RoleBLL
    {
        private RoleService sysRoleService = new RoleService();
        private MenuAuthorizeService menuAuthorizeService = new MenuAuthorizeService();

        private MenuAuthorizeCache menuAuthorizeCache = new MenuAuthorizeCache();

        #region 获取数据
        public async Task<TData<List<RoleEntity>>> GetList(RoleListParam param)
        {
            TData<List<RoleEntity>> obj = new TData<List<RoleEntity>>();
            obj.Result = await sysRoleService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<RoleEntity>>> GetPageList(RoleListParam param, Pagination pagination)
        {
            TData<List<RoleEntity>> obj = new TData<List<RoleEntity>>();
            obj.Result = await sysRoleService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<RoleEntity>> GetEntity(long id)
        {
            TData<RoleEntity> obj = new TData<RoleEntity>();
            RoleEntity roleEntity = await sysRoleService.GetEntity(id);
            List<MenuAuthorizeEntity> menuAuthorizeList = await menuAuthorizeService.GetList(new MenuAuthorizeEntity
            {
                AuthorizeId = id,
                AuthorizeType = AuthorizeTypeEnum.Role.ParseToInt()
            });
            // 获取角色对应的权限
            roleEntity.MenuIds = string.Join(",", menuAuthorizeList.Select(p => p.MenuId));

            obj.Result = roleEntity;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Result = await sysRoleService.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(RoleEntity entity)
        {
            TData<string> obj = new TData<string>();

            if (sysRoleService.ExistRoleName(entity))
            {
                obj.Message = "角色名称已经存在！";
                return obj;
            }

            await sysRoleService.SaveForm(entity);

            // 清除缓存里面的权限数据
            menuAuthorizeCache.Remove();

            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;

            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();

            await sysRoleService.DeleteForm(ids);

            // 清除缓存里面的权限数据
            menuAuthorizeCache.Remove();

            obj.Tag = 1;
            return obj;
        }
        #endregion

    }
}
