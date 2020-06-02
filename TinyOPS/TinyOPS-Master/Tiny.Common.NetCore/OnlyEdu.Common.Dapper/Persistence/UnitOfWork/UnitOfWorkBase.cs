using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using OnlyEdu.Common.Dapper.DI;
using OnlyEdu.Common.Dapper.Entity;
using OnlyEdu.Common.Dapper.Persistence.Mapper;

namespace OnlyEdu.Common.Dapper.Persistence.UnitOfWork
{
    public class UnitOfWorkBase : IUnitOfWork
    {
        public IDbConnection Connection { get; set; }
        /// <summary>
        /// 事务对象集合
        /// </summary>
        public IDbTransaction Transaction { get; set; }

        public IList<SaveInfo> SaveInfos { set; get; } = new List<SaveInfo>();

        public string ConnectionStr { get; set; }


        #region 接口的定义

        public bool IsExcute { get; set; }

        public bool IsDispose { get; set; }

        public UnitOfWorkBase(string connectionStr)
        {
            ConnectionStr = connectionStr;
        }

        public virtual void Execute()
        {
            try
            {
                Begin();
                SaveInfos = SaveInfos.ToList().OrderBy(it => it.Sequence).ToList();
                foreach (var saveInfo in SaveInfos)
                {
                    switch (saveInfo.Info.SaveType)
                    {
                        case SaveType.Add:
                            PersistNewItem(saveInfo);
                            break;
                        case SaveType.Modify:
                            PersistUpdatedItem(saveInfo);
                            break;
                        case SaveType.Remove:
                            PersistDeletedItem(saveInfo);
                            break;
                        case SaveType.Excute:
                            PersistExcuteItem(saveInfo);
                            break;
                        case SaveType.ExcuteT:
                            PersistExcuteItemT(saveInfo);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Close();
                throw ex;
            }
        }

        #region 执行
        public void PersistNewItem(SaveInfo item)
        {
            int newId = 0;

            var objnewId = Connection.Query<object>(IoC.Resolve<IMapper>().GetNewCommand(item.Info.GetType()), item.Info, Transaction).FirstOrDefault();
            if (objnewId is IDictionary<string, object> dicts)
            {
                var id = dicts.Values.FirstOrDefault();
                if (id != null)
                    int.TryParse(id.ToString(), out newId);
            }

            item.Info.AutoIncrementId = newId;
        }

        public void PersistUpdatedItem(SaveInfo item)
        {
            Connection.Execute(IoC.Resolve<IMapper>().GetUpdateCommand(item.Info.GetType()), item.Info, Transaction);
        }

        public void PersistDeletedItem(SaveInfo item)
        {
            Connection.Execute(IoC.Resolve<IMapper>().GetRemoveCommand(item.Info.GetType()), item.Info, Transaction);
        }

        public void PersistExcuteItem(SaveInfo item)
        {
            var cmd = Connection.CreateCommand();
            cmd.Transaction = Transaction;
            cmd.CommandText = item.CommandText;
            cmd.CommandType = item.CommandType;
            cmd = IoC.Resolve<IMapper>().CreateCommandParams(cmd, item.ParamsInfo);
            cmd.ExecuteNonQuery();
        }

        public void PersistExcuteItemT(SaveInfo item)
        {
            if (item.CallBack == null)
                Connection.Execute(item.CommandText, item.ParamEntityInfo, Transaction);
            else
            {
                var res = Connection.Query(item.CommandText, item.ParamEntityInfo, Transaction).ToList();
                item.CallBack(res);
            }
        }
        #endregion

        /// <summary>
        /// 开启事务
        /// </summary>
        private void Begin()
        {
            SetConnnection();
            Transaction = Connection.BeginTransaction();
        }

        /// <summary>
        /// 得到链接
        /// </summary>
        /// <returns></returns>
        private void SetConnnection()
        {
            Connection = IoC.Resolve<IMapper>().GetConnection(ConnectionStr);
            if (Connection != null && Connection.State != ConnectionState.Open)
                Connection.Open();
        }

        /// <summary>
        /// 提交
        /// </summary>
        public void Commit()
        {
            try
            {
                Transaction.Commit();
            }
            finally
            {
                Close();
            }
        }
        /// <summary>
        /// 回滚
        /// </summary>
        public void Rollback()
        {
            try
            {
                Transaction?.Rollback();
            }
            finally
            {
                Close();
            }
        }

        public virtual string GetKey()
        {
            return ConnectionStr;
        }
        #endregion
        public int GetSequence()
        {
            return SaveInfos?.Count ?? 0;
        }

        public void Add(object info)
        {
            SaveInfos.Add(info as SaveInfo);
        }

        public void Close()
        {
            if (Connection != null && Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
    }
}
