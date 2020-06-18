using System;
using System.Collections.Generic;
using System.Text;
using TinyEdu.Common.Dapper.Entity;
using TinyEdu.Common.Dapper.Persistence.UnitOfWork;

namespace TinyEdu.Common.Dapper.Service
{
    public class RealDomainService<T> : BaseDomainService where T : BaseInfo
    {
        //public bool IsAutoThrowError { set; get; } = ConfigurationSetting.Get<bool>("AutoThrowError");
        #region 重写底层方法
        public override UnitOfWorkResult Add<T1>(T1 info, Func<T1, UnitOfWorkResult> funRep = null)
        {
            return Add(info as T, funRep as Func<T, UnitOfWorkResult>);
        }
        public override UnitOfWorkResult Save<T1>(T1 info, Func<T1, UnitOfWorkResult> funRep = null)
        {
            return Save(info as T, funRep as Func<T, UnitOfWorkResult>);
        }
        public override UnitOfWorkResult Remove<T1>(T1 info, Func<T1, UnitOfWorkResult> funRep = null)
        {
            return Remove(info as T, funRep as Func<T, UnitOfWorkResult>);
        }
        #endregion

        public virtual T GetOriginalInfo(object key)
        {
            return GetOriginalInfo<T>(key);
        }

        public virtual T GetInfo(object key)
        {
            return GetInfo<T>(key);
        }

        public virtual UnitOfWorkResult Save(T info, Func<T, UnitOfWorkResult> funRep = null)
        {
            info.SaveType = SaveType.Modify;
            if (!SaveDataValidate(info) || !SaveBusinessValidate(info))
            {
                //if (IsAutoThrowError)
                //    throw new LogicException((int)MessageBase.M1001, info.GetErrors(), info.GetType());
                //else
                return UnitOfWorkResult.ErrResult(info.GetErrors());
            }
            var res = base.Save<T>(info, funRep);
            if (res.IsSuccess)
            {
                if (!SetSaveBusiness(info))
                {
                    //if (IsAutoThrowError)
                    //    throw new LogicException((int)MessageBase.M1003, info.GetErrors(), info.GetType());
                    //else
                    return UnitOfWorkResult.ErrResult(info.GetErrors());
                }
            }
            return res;
        }

        public virtual UnitOfWorkResult Remove(T info, Func<T, UnitOfWorkResult> funRep = null)
        {
            info.SaveType = SaveType.Remove;
            if (!RemoveDataValidate(info) || !RemoveBusinessValidate(info))
            {
                //if (IsAutoThrowError)
                //    throw new LogicException((int)MessageBase.M1001, info.GetErrors(), info.GetType());
                //else
                   return UnitOfWorkResult.ErrResult(info.GetErrors());
            }
            var res = base.Remove<T>(info, funRep);
            if (res.IsSuccess)
            {
                if (!SetRemoveBusiness(info))
                {
                    //if (IsAutoThrowError)
                    //    throw new LogicException((int)MessageBase.M1003, info.GetErrors(), info.GetType());
                    //else
                       return UnitOfWorkResult.ErrResult(info.GetErrors());
                }
            }
            return res;
        }

        public virtual UnitOfWorkResult Add(T info, Func<T, UnitOfWorkResult> funRep = null)
        {
            info.SaveType = SaveType.Add;
            if (!AddDataValidate(info) || !AddBusinessValidate(info))
            {
                //if (IsAutoThrowError)
                //    throw new LogicException((int)MessageBase.M1001, info.GetErrors(), info.GetType());
                //else
                  return UnitOfWorkResult.ErrResult(info.GetErrors());
            }
            var res = base.Add<T>(info, funRep);
            if (res.IsSuccess)
            {
                if (!SetAddBusiness(info))
                {
                    //if (IsAutoThrowError)
                    //    throw new LogicException((int)MessageBase.M1003, info.GetErrors(), info.GetType());
                    //else
                      return UnitOfWorkResult.ErrResult(info.GetErrors());
                }
            }
            return res;
        }

        /// <summary>
        /// 添加数据验证
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual bool AddDataValidate(T info)
        {
            if (info == null)
            {
                //if (IsAutoThrowError)
                //    throw new LogicException((int)MessageBase.M1001, "无效的数据!");
                //else
                    return false;
            }
            return info.Validate();
        }

        /// <summary>
        /// 添加业务逻辑
        /// </summary>
        /// <param name="info"></param>
        protected virtual bool SetAddBusiness(T info)
        {
            if (info == null)
            {
                //if (IsAutoThrowError)
                //    throw new LogicException((int)MessageBase.M1001, "无效的数据!");
                //else
                    return false;
            }
            //info.CreatedDate = DateTime.Now;
            //info.UpdateDate = DateTime.Now;
            return true;
        }
        /// <summary>
        /// 修改数据验证
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual bool SaveDataValidate(T info)
        {
            if (info == null)
            {
                //if (IsAutoThrowError)
                //    throw new LogicException((int)MessageBase.M1001, "无效的数据!");
                //else
                    return false;
            }
            return info.Validate();
        }

        /// <summary>
        /// 修改业务逻辑
        /// </summary>
        protected virtual bool SetSaveBusiness(T info)
        {
            if (info == null)
            {
                //if (IsAutoThrowError)
                //    throw new LogicException((int)MessageBase.M1001, "无效的数据!");
                //else
                    return false;
            }
            //info.UpdateDate = DateTime.Now;
            return true;
        }
        /// <summary>
        /// 删除数据验证
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual bool RemoveDataValidate(T info)
        {
            if (info == null)
            {
                //if (IsAutoThrowError)
                //    throw new LogicException((int)MessageBase.M1001, "无效的数据!");
                //else
                    return false;
            }
            return info.Validate();
        }
        /// <summary>
        /// 删除业务逻辑
        /// </summary>
        /// <param name="info"></param>
        protected virtual bool SetRemoveBusiness(T info)
        {
            if (info == null)
            {
                //if (IsAutoThrowError)
                //    throw new LogicException((int)MessageBase.M1001, "无效的数据!");
                //else
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 修改业务逻辑验证
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual bool SaveBusinessValidate(T info)
        {
            return true;
        }
        /// <summary>
        /// 添加业务逻辑验证
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual bool AddBusinessValidate(T info)
        {
            return true;
        }
        /// <summary>
        /// 删除业务逻辑验证
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected virtual bool RemoveBusinessValidate(T info)
        {
            return true;
        }
    }
}
