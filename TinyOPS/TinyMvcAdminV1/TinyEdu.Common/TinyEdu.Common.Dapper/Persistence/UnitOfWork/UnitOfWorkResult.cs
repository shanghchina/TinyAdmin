using System;
using System.Collections.Generic;
using System.Text;
using TinyEdu.Common.Dapper.DI;
using TinyEdu.Common.Dapper.Persistence.Context;

namespace TinyEdu.Common.Dapper.Persistence.UnitOfWork
{
    public class UnitOfWorkResult
    {
        /// <summary>
        /// 信息列表
        /// </summary>
        protected StringBuilder MessageBuilder = new StringBuilder();
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="message"></param>
        public void AddMessage(string message)
        {
            MessageBuilder.Append(message);
        }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message => MessageBuilder.ToString();

        /// <summary>
        /// UnitOfWorks
        /// </summary>
        public IList<IUnitOfWork> UnitOfWorks { set; get; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { set; get; }
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            var res = true;
            if (!IsSuccess) return false;
            if (UnitOfWorks == null) return true;
            try
            {
                ExecuteTrains(UnitOfWorks);
                CommitTrains(UnitOfWorks);
            }
            catch (Exception ex)
            {
                res = false;
                RollbackTrains(UnitOfWorks);
                throw ex;
            }
            finally
            {
                ResetStatus(UnitOfWorks);
            }
            return res;
        }

        protected virtual void ResetStatus(IList<IUnitOfWork> unitOfWorks)
        {
            foreach (var work in unitOfWorks)
            {
                if (work != null)
                {
                    work.IsDispose = false;
                    work.IsExcute = false;
                    work.Close();
                }
            }
            unitOfWorks.Clear();
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="unitOfWorks"></param>
        protected virtual void ExecuteTrains(IList<IUnitOfWork> unitOfWorks)
        {
            foreach (var work in unitOfWorks)
            {
                if (work != null && !work.IsExcute && !work.IsDispose)
                {
                    work.Execute();
                    work.IsExcute = true;
                }
            }
        }

        protected virtual void CommitTrains(IList<IUnitOfWork> unitOfWorks)
        {
            foreach (var work in unitOfWorks)
            {
                if (work != null && work.IsExcute && !work.IsDispose)
                {
                    work.Commit();
                    work.IsDispose = true;
                }
            }
        }

        protected virtual void RollbackTrains(IList<IUnitOfWork> unitOfWorks)
        {
            foreach (var work in unitOfWorks)
            {
                if (work != null && work.IsExcute && !work.IsDispose)
                {
                    work.Rollback();
                    work.IsDispose = true;
                }
            }
        }

        /// <summary>
        /// 仅当无需执行持久化操作时可以调用
        /// </summary>
        /// <returns></returns>
        public static UnitOfWorkResult SuccessResult(string message = "")
        {
            var res = new UnitOfWorkResult
            {
                IsSuccess = true,
                UnitOfWorks = IoC.Resolve<IContext>().Local.UnitOfWorks
            };
            if (!string.IsNullOrEmpty(message))
                res.AddMessage(message);
            return res;
        }

        public static UnitOfWorkResult ErrResult(string message = "")
        {
            var res = new UnitOfWorkResult
            {
                IsSuccess = false,
            };
            if (!string.IsNullOrEmpty(message))
                res.AddMessage(message);
            return res;
        }

        public static UnitOfWorkResult GetCurrentUow()
        {
            return new UnitOfWorkResult
            {
                IsSuccess = true,
                UnitOfWorks = IoC.Resolve<IContext>().Local.UnitOfWorks,
            };
        }
    }
}
