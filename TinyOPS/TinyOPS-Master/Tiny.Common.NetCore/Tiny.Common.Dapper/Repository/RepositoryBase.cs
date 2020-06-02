using Dapper;
using Tiny.Common.Dapper.DI;
using Tiny.Common.Dapper.Entity;
using Tiny.Common.Dapper.Enumeration;
using Tiny.Common.Dapper.Persistence.Context;
using Tiny.Common.Dapper.Persistence.Data;
using Tiny.Common.Dapper.Persistence.Mapper;
using Tiny.Common.Dapper.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Tiny.Common.Dapper.Repository
{
    public class RepositoryBase : IRepository
    {
        public IContext Context { set; get; }

        public RepositoryBase()
        {
            Context = IoC.Resolve<IContext>();
        }



        public UnitOfWorkResult Add<T>(T info) where T : BaseInfo
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            info.SaveType = SaveType.Add;
            return RegisterCUDUnitOfWork(EumDBWay.Writer, dbAtt.DBName.GetDisplayName(), info);
        }

        public UnitOfWorkResult Save<T>(T info) where T : BaseInfo
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            info.SaveType = SaveType.Modify;
            return RegisterCUDUnitOfWork(EumDBWay.Writer, dbAtt.DBName.GetDisplayName(), info);
        }

        public UnitOfWorkResult Remove<T>(T info) where T : BaseInfo
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            info.SaveType = SaveType.Remove;
            return RegisterCUDUnitOfWork(EumDBWay.Writer, dbAtt.DBName.GetDisplayName(), info);
        }

        public UnitOfWorkResult RemoveList<T>(IList<T> infos) where T : BaseInfo
        {
            UnitOfWorkResult res = new UnitOfWorkResult();
            if (infos != null && infos.Count > 0)
            {
                infos.ToList().ForEach(item =>
                {
                    res = Remove(item);
                    if (!res.IsSuccess)
                    {
                        return;
                    }
                });
            }
            return res;
        }

        public UnitOfWorkResult ExecuteCommand<T>(EumDBWay dbWay, string dbName, string commandText, CommandType commandType, params DataParameterInfo[] parameters) where T : BaseInfo
        {
            return RegisterExecuteUnitOfWork(dbWay, dbName, typeof(T).FullName, commandText, commandType, parameters);
        }

        public UnitOfWorkResult ExecuteCommand<T>(EumDBWay dbWay, EumDBName dbName, string commandText, CommandType commandType, params DataParameterInfo[] parameters) where T : BaseInfo
        {
            return RegisterExecuteUnitOfWork(dbWay, dbName.GetDisplayName(), typeof(T).FullName, commandText, commandType, parameters);
        }



        public UnitOfWorkResult ExecuteCommand<T>(EumDBWay dbWay, string dbName, string commandText, object paramEntity, Action<IList<dynamic>> callback = null) where T : BaseInfo
        {
            return RegisterExecuteTUnitOfWork(dbWay, dbName, typeof(T).FullName, commandText, paramEntity, callback);
        }

        public object ExecuteScalar<T>(string commandText, object paramEntity)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Writer, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                return Connection.ExecuteScalar(commandText, paramEntity);
            }

        }


        public object ExecuteScalar(EumDBName dBName, string commandText, object paramEntity)
        {
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Writer, dBName.GetDisplayName())))
            {
                return Connection.ExecuteScalar(commandText, paramEntity);
            }
        }


        public T GetInfo<T>(object key, bool fromCache = true) where T : BaseInfo
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            T info = Context.Get<T>(key);
            if (fromCache && info != null)
            {
                return info;
            }

            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                IEnumerable<T> infos = Connection.Query<T>(IoC.Resolve<IMapper>().GetKeyCommand(typeof(T)), new { Key = key });
                return infos.FirstOrDefault();
            }
        }

        public IList<T> GetInfos<T>(string query, object parameter)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                IEnumerable<T> infos = Connection.Query<T>(query, parameter);
                return infos.ToList();
            }
        }


        public IList<T> GetInfos<T>(EumDBName dBName, string query, object parameter)
        {
            Type type = typeof(T);
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dBName.GetDisplayName(), typeof(T).FullName)))
            {
                IEnumerable<T> infos = Connection.Query<T>(query, parameter);
                return infos.ToList();
            }
        }

        public IList<TResult> GetInfos<T, T1, TResult>(string query, Func<T, T1, TResult> map, object parameter = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = default(int?))
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                IEnumerable<TResult> infos = Connection.Query(query, map, parameter, buffered: buffered, commandTimeout: commandTimeout, splitOn: splitOn);
                return infos.ToList();
            }
        }

        public IList<TResult> GetInfos<T, T1, T2, TResult>(string query, Func<T, T1, T2, TResult> map, object parameter = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = default(int?))
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                IEnumerable<TResult> infos = Connection.Query(query, map, parameter, buffered: buffered, commandTimeout: commandTimeout, splitOn: splitOn);
                return infos.ToList();
            }
        }

        public IList<TResult> GetInfos<T, T1, T2, T3, TResult>(string query, Func<T, T1, T2, T3, TResult> map, object parameter = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = default(int?))
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                IEnumerable<TResult> infos = Connection.Query(query, map, parameter, buffered: buffered, commandTimeout: commandTimeout, splitOn: splitOn);
                return infos.ToList();
            }
        }

        public IList<TResult> GetInfos<T, T1, T2, T3, T4, TResult>(string query, Func<T, T1, T2, T3, T4, TResult> map, object parameter = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = default(int?))
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                IEnumerable<TResult> infos = Connection.Query(query, map, parameter, buffered: buffered, commandTimeout: commandTimeout, splitOn: splitOn);
                return infos.ToList();
            }
        }

        public IList<TResult> GetInfos<T, T1, T2, T3, T4, T5, TResult>(string query, Func<T, T1, T2, T3, T4, T5, TResult> map, object parameter, bool buffered = true, string splitOn = "Id", int? commandTimeout = default(int?))
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                IEnumerable<TResult> infos = Connection.Query(query, map, parameter, buffered: buffered, commandTimeout: commandTimeout, splitOn: splitOn);
                return infos.ToList();
            }
        }

        public IList<TResult> GetInfos<T, T1, T2, T3, T4, T5, T6, TResult>(string query, Func<T, T1, T2, T3, T4, T5, T6, TResult> map, object parameter = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = default(int?))
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                IEnumerable<TResult> infos = Connection.Query(query, map, parameter, buffered: buffered, commandTimeout: commandTimeout, splitOn: splitOn);
                return infos.ToList();
            }
        }

        public IList<T> GetPageInfos<T>(EumDBName dBName, string sql, string order, int pageIndex, int pageSize, object parameter = null)
        {
            Type type = typeof(T);
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dBName.GetDisplayName(), typeof(T).FullName)))
            {
                var sqlstr = sql.ToString() + " order by " + order;
                var queryPage = $"{sqlstr} offset {(pageIndex - 1) * pageSize} row fetch next {pageSize} rows only";
                return Connection.Query<T>(queryPage.ToString(), parameter).ToList();
            }
        }

        public IList<T> GetPageInfos<T>(string filter, string order, int pageIndex, int pageSize, out int count, object parameter = null)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                count = GetPageCount<T>(filter, parameter);
                IEnumerable<T> infos = Connection.Query<T>(IoC.Resolve<IMapper>().GetPageCommand(typeof(T), filter, order, pageIndex, pageSize), parameter);
                return infos.ToList();
            }
        }
        public IList<TResult> GetPageInfos<T, T1, TResult>(string querySql, int pageIndex, int pageSize, Func<T, T1, TResult> map, object parameter = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = default(int?))
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                string sql = IoC.Resolve<IMapper>().GetPageCommandByMultilist(querySql, pageIndex, pageSize);
                IEnumerable<TResult> infos = Connection.Query(sql, map, parameter, buffered: buffered, commandTimeout: commandTimeout, splitOn: splitOn);
                return infos.ToList();
            }
        }

        public IList<TResult> GetPageInfos<T, T1, T2, TResult>(string querySql, int pageIndex, int pageSize, Func<T, T1, T2, TResult> map, object parameter = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = default(int?))
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                string sql = IoC.Resolve<IMapper>().GetPageCommandByMultilist(querySql, pageIndex, pageSize);
                IEnumerable<TResult> infos = Connection.Query(sql, map, parameter, buffered: buffered, commandTimeout: commandTimeout, splitOn: splitOn);
                return infos.ToList();
            }
        }
        public IList<TResult> GetPageInfos<T, T1, T2, T3, TResult>(string querySql, int pageIndex, int pageSize, Func<T, T1, T2, T3, TResult> map, object parameter = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = default(int?))
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                string sql = IoC.Resolve<IMapper>().GetPageCommandByMultilist(querySql, pageIndex, pageSize);
                IEnumerable<TResult> infos = Connection.Query(sql, map, parameter, buffered: buffered, commandTimeout: commandTimeout, splitOn: splitOn);
                return infos.ToList();
            }
        }
        public IList<TResult> GetPageInfos<T, T1, T2, T3, T4, TResult>(string querySql, int pageIndex, int pageSize, Func<T, T1, T2, T3, T4, TResult> map, object parameter = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = default(int?))
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                string sql = IoC.Resolve<IMapper>().GetPageCommandByMultilist(querySql, pageIndex, pageSize);
                IEnumerable<TResult> infos = Connection.Query(sql, map, parameter, buffered: buffered, commandTimeout: commandTimeout, splitOn: splitOn);
                return infos.ToList();
            }
        }

        public IList<TResult> GetPageInfos<T, T1, T2, T3, T4, T5, TResult>(string querySql, int pageIndex, int pageSize, Func<T, T1, T2, T3, T4, T5, TResult> map, object parameter = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = default(int?))
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                string sql = IoC.Resolve<IMapper>().GetPageCommandByMultilist(querySql, pageIndex, pageSize);
                IEnumerable<TResult> infos = Connection.Query(sql, map, parameter, buffered: buffered, commandTimeout: commandTimeout, splitOn: splitOn);
                return infos.ToList();
            }
        }
        public IList<TResult> GetPageInfos<T, T1, T2, T3, T4, T5, T6, TResult>(string querySql, int pageIndex, int pageSize, Func<T, T1, T2, T3, T4, T5, T6, TResult> map, object parameter = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = default(int?))
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                string sql = IoC.Resolve<IMapper>().GetPageCommandByMultilist(querySql, pageIndex, pageSize);
                IEnumerable<TResult> infos = Connection.Query(sql, map, parameter, buffered: buffered, commandTimeout: commandTimeout, splitOn: splitOn);
                return infos.ToList();
            }
        }


        public int GetPageCount<T>(string filter, object parameter = null)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                return Connection.Query<int>(IoC.Resolve<IMapper>().GetPageCount(typeof(T), filter), parameter).FirstOrDefault();
            }
        }
        public int GetPageCount<T>(string tables, string filter, object parameter = null)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName(), typeof(T).FullName)))
            {
                string str = IoC.Resolve<IMapper>().GetPageCountByMultilist(tables, filter);
                return Connection.Query<int>(IoC.Resolve<IMapper>().GetPageCountByMultilist(tables, filter), parameter).FirstOrDefault();
            }
        }
        public int GetPageCount<T>(EumDBName dBName, string tables, string filter, object parameter = null)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection Connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dBName.GetDisplayName(), typeof(T).FullName)))
            {
                string str = IoC.Resolve<IMapper>().GetPageCountByMultilist(tables, filter);
                return Connection.Query<int>(IoC.Resolve<IMapper>().GetPageCountByMultilist(tables, filter), parameter).FirstOrDefault();
            }
        }

        protected UnitOfWorkResult RegisterExecuteTUnitOfWork(EumDBWay dbWay, string dbName, string typeInfo, string commandText, object paramEntity, Action<IList<dynamic>> callback)
        {
            SaveInfo info = RegisterUnitOfWork(dbWay, dbName, typeInfo);
            info.CommandText = commandText;
            info.ParamEntityInfo = paramEntity;
            info.CallBack = callback;
            info.Info = new BaseInfo { SaveType = SaveType.ExcuteT };
            return UnitOfWorkResult.GetCurrentUow();
        }

        protected UnitOfWorkResult RegisterExecuteUnitOfWork(EumDBWay dbWay, string dbName, string typeInfo, string commandText, CommandType commandType, DataParameterInfo[] paramsInfo)
        {
            SaveInfo info = RegisterUnitOfWork(dbWay, dbName, typeInfo);
            info.CommandType = commandType;
            info.CommandText = commandText;
            info.ParamsInfo = paramsInfo;
            info.Info = new BaseInfo { SaveType = SaveType.Excute };
            return UnitOfWorkResult.GetCurrentUow();
        }

        private SaveInfo RegisterUnitOfWork(EumDBWay dbWay, string dbName, string typeInfo)
        {
            string setDataBaseConnect = DataBaseHelper.GetConnectionStrings(dbWay, dbName, typeInfo);
            IList<IUnitOfWork> unitofworks = IoC.Resolve<IContext>().Local.UnitOfWorks ?? new List<IUnitOfWork>();
            IUnitOfWork findwork = unitofworks.FirstOrDefault(it => it.GetKey() == setDataBaseConnect);
            SaveInfo info = new SaveInfo { Sequence = findwork?.GetSequence() + 1 ?? 1 };
            if (findwork != null)
            {
                findwork.Add(info);
            }
            else
            {
                findwork = new UnitOfWorkBase(setDataBaseConnect);
                findwork.Add(info);
                unitofworks.Add(findwork);
            }
            return info;
        }

        protected UnitOfWorkResult RegisterCUDUnitOfWork<T>(EumDBWay dbWay, string dbName, T item) where T : BaseInfo
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            SaveInfo info = RegisterUnitOfWork(dbWay, dbName, item.GetType().FullName);
            info.Info = item;
            return UnitOfWorkResult.GetCurrentUow();
        }

        public Tuple<List<T>, List<T1>> GetInfoMultiple<T, T1>(CommandDefinition command)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName())))
            {
                using (SqlMapper.GridReader multiple = connection.QueryMultiple(command))
                {
                    List<T> item1 = multiple.Read<T>().ToList();
                    List<T1> item2 = multiple.Read<T1>().ToList();
                    return new Tuple<List<T>, List<T1>>(item1, item2);
                }
            }
        }

        public Tuple<List<T>, List<T1>, List<T2>> GetInfoMultiple<T, T1, T2>(CommandDefinition command)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName())))
            {
                using (SqlMapper.GridReader multiple = connection.QueryMultiple(command))
                {
                    List<T> item1 = multiple.Read<T>().ToList();
                    List<T1> item2 = multiple.Read<T1>().ToList();
                    List<T2> item3 = multiple.Read<T2>().ToList();
                    return new Tuple<List<T>, List<T1>, List<T2>>(item1, item2, item3);
                }
            }
        }

        public Tuple<List<T>, List<T1>, List<T2>, List<T3>> GetInfoMultiple<T, T1, T2, T3>(CommandDefinition command)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName())))
            {
                using (SqlMapper.GridReader multiple = connection.QueryMultiple(command))
                {
                    List<T> item1 = multiple.Read<T>().ToList();
                    List<T1> item2 = multiple.Read<T1>().ToList();
                    List<T2> item3 = multiple.Read<T2>().ToList();
                    List<T3> item4 = multiple.Read<T3>().ToList();
                    return new Tuple<List<T>, List<T1>, List<T2>, List<T3>>(item1, item2, item3, item4);
                }
            }
        }

        public Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>> GetInfoMultiple<T, T1, T2, T3, T4>(CommandDefinition command)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName())))
            {
                using (SqlMapper.GridReader multiple = connection.QueryMultiple(command))
                {
                    List<T> item1 = multiple.Read<T>().ToList();
                    List<T1> item2 = multiple.Read<T1>().ToList();
                    List<T2> item3 = multiple.Read<T2>().ToList();
                    List<T3> item4 = multiple.Read<T3>().ToList();
                    List<T4> item5 = multiple.Read<T4>().ToList();
                    return new Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>>(item1, item2, item3, item4,
                        item5);
                }
            }
        }
        public Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>, List<T5>> GetInfoMultiple<T, T1, T2, T3, T4, T5>(CommandDefinition command)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName())))
            {
                using (SqlMapper.GridReader multiple = connection.QueryMultiple(command))
                {
                    List<T> item1 = multiple.Read<T>().ToList();
                    List<T1> item2 = multiple.Read<T1>().ToList();
                    List<T2> item3 = multiple.Read<T2>().ToList();
                    List<T3> item4 = multiple.Read<T3>().ToList();
                    List<T4> item5 = multiple.Read<T4>().ToList();
                    List<T5> item6 = multiple.Read<T5>().ToList();
                    return new Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>, List<T5>>(item1, item2, item3, item4,
                        item5, item6);
                }
            }
        }
        public Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>, List<T5>, List<T6>> GetInfoMultiple<T, T1, T2, T3, T4, T5, T6>(CommandDefinition command)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName())))
            {
                using (SqlMapper.GridReader multiple = connection.QueryMultiple(command))
                {
                    List<T> item1 = multiple.Read<T>().ToList();
                    List<T1> item2 = multiple.Read<T1>().ToList();
                    List<T2> item3 = multiple.Read<T2>().ToList();
                    List<T3> item4 = multiple.Read<T3>().ToList();
                    List<T4> item5 = multiple.Read<T4>().ToList();
                    List<T5> item6 = multiple.Read<T5>().ToList();
                    List<T6> item7 = multiple.Read<T6>().ToList();
                    return new Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>, List<T5>, List<T6>>(item1, item2, item3, item4,
                        item5, item6, item7);
                }
            }
        }

        public Tuple<List<T>, List<T1>> GetInfoMultiple<T, T1>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName())))
            {
                using (SqlMapper.GridReader multiple = connection.QueryMultiple(sql, param, null, commandTimeout, commandType))
                {
                    List<T> item1 = multiple.Read<T>().ToList();
                    List<T1> item2 = multiple.Read<T1>().ToList();
                    return new Tuple<List<T>, List<T1>>(item1, item2);
                }
            }
        }

        public Tuple<List<T>, List<T1>, List<T2>> GetInfoMultiple<T, T1, T2>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName())))
            {
                using (SqlMapper.GridReader multiple = connection.QueryMultiple(sql, param, null, commandTimeout, commandType))
                {
                    List<T> item1 = multiple.Read<T>().ToList();
                    List<T1> item2 = multiple.Read<T1>().ToList();
                    List<T2> item3 = multiple.Read<T2>().ToList();
                    return new Tuple<List<T>, List<T1>, List<T2>>(item1, item2, item3);
                }
            }
        }

        public Tuple<List<T>, List<T1>, List<T2>, List<T3>> GetInfoMultiple<T, T1, T2, T3>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName())))
            {
                using (SqlMapper.GridReader multiple = connection.QueryMultiple(sql, param, null, commandTimeout, commandType))
                {
                    List<T> item1 = multiple.Read<T>().ToList();
                    List<T1> item2 = multiple.Read<T1>().ToList();
                    List<T2> item3 = multiple.Read<T2>().ToList();
                    List<T3> item4 = multiple.Read<T3>().ToList();
                    return new Tuple<List<T>, List<T1>, List<T2>, List<T3>>(item1, item2, item3, item4);
                }
            }
        }

        public Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>> GetInfoMultiple<T, T1, T2, T3, T4>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName())))
            {
                using (SqlMapper.GridReader multiple = connection.QueryMultiple(sql, param, null, commandTimeout, commandType))
                {
                    List<T> item1 = multiple.Read<T>().ToList();
                    List<T1> item2 = multiple.Read<T1>().ToList();
                    List<T2> item3 = multiple.Read<T2>().ToList();
                    List<T3> item4 = multiple.Read<T3>().ToList();
                    List<T4> item5 = multiple.Read<T4>().ToList();
                    return new Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>>(item1, item2, item3, item4,
                        item5);
                }
            }
        }
        public Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>, List<T5>> GetInfoMultiple<T, T1, T2, T3, T4, T5>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName())))
            {
                using (SqlMapper.GridReader multiple = connection.QueryMultiple(sql, param, null, commandTimeout, commandType))
                {
                    List<T> item1 = multiple.Read<T>().ToList();
                    List<T1> item2 = multiple.Read<T1>().ToList();
                    List<T2> item3 = multiple.Read<T2>().ToList();
                    List<T3> item4 = multiple.Read<T3>().ToList();
                    List<T4> item5 = multiple.Read<T4>().ToList();
                    List<T5> item6 = multiple.Read<T5>().ToList();
                    return new Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>, List<T5>>(item1, item2, item3, item4,
                        item5, item6);
                }
            }
        }
        public Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>, List<T5>, List<T6>> GetInfoMultiple<T, T1, T2, T3, T4, T5, T6>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Type type = typeof(T);
            TableAttribute dbAtt = type.GetCustomAttribute(typeof(TableAttribute)) as TableAttribute;
            using (IDbConnection connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(EumDBWay.Reader, dbAtt.DBName.GetDisplayName())))
            {
                using (SqlMapper.GridReader multiple = connection.QueryMultiple(sql, param, null, commandTimeout, commandType))
                {
                    List<T> item1 = multiple.Read<T>().ToList();
                    List<T1> item2 = multiple.Read<T1>().ToList();
                    List<T2> item3 = multiple.Read<T2>().ToList();
                    List<T3> item4 = multiple.Read<T3>().ToList();
                    List<T4> item5 = multiple.Read<T4>().ToList();
                    List<T5> item6 = multiple.Read<T5>().ToList();
                    List<T6> item7 = multiple.Read<T6>().ToList();
                    return new Tuple<List<T>, List<T1>, List<T2>, List<T3>, List<T4>, List<T5>, List<T6>>(item1, item2, item3, item4,
                        item5, item6, item7);
                }
            }
        }

        public bool BatchInsert(EumDBWay dbWay, string dbName, DataTable table)
        {
            bool isSuccess = false;
            if (string.IsNullOrEmpty(table.TableName))
            {
                throw new ArgumentNullException($"The DataTable TableName cannot be empty");
            }

            if (table.Columns.Count <= 0)
            {
                throw new ArgumentNullException($"The DataTable Columns cannot be empty");
            }

            if (table.Rows.Count <= 0)
            {
                throw new ArgumentNullException($"The DataTable Rows cannot be empty");
            }

            using (IDbConnection connection = IoC.Resolve<IMapper>().GetConnection(DataBaseHelper.GetConnectionStrings(dbWay, dbWay.GetDisplayName())))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (System.Data.SqlClient.SqlBulkCopy blukCopy = new System.Data.SqlClient.SqlBulkCopy((System.Data.SqlClient.SqlConnection)connection))
                {
                    try
                    {
                        blukCopy.DestinationTableName = table.TableName;
                        foreach (DataColumn col in table.Columns)
                        {
                            blukCopy.ColumnMappings.Add(new System.Data.SqlClient.SqlBulkCopyColumnMapping(col.ColumnName, col.ColumnName));
                        }

                        //复制数据
                        blukCopy.WriteToServer(table);
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }

            return isSuccess;
        }

        public bool BatchInsert<T>(List<T> entitys) where T : BaseInfo
        {
            Type type = typeof(T);
            TableAttribute tableAtt = type.GetCustomAttribute(typeof(TableAttribute), true) as TableAttribute;
            if (tableAtt == null)
            {
                throw new Exception("UnValid BaseInfo, Not Set TableName Attribute");
            }

            DataTable dt = new DataTable(tableAtt.TableName);
            List<PropertyInfo> propertyInfoList = new List<PropertyInfo>(type.GetProperties());
            foreach (PropertyInfo prop in propertyInfoList)
            {
                object[] keys = prop.GetCustomAttributes(typeof(NoMapperAttribute), true);
                if (keys.Length > 0)
                {
                    continue;
                }

                bool keyIsIgnore = prop.GetCustomAttributes(typeof(KeyAttribute), true)
                    .Any(t => (t as KeyAttribute).IsIgnore);
                if (keyIsIgnore)
                {
                    continue;
                }

                Type columnType = prop.PropertyType;
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    columnType = prop.PropertyType.GetGenericArguments()[0];
                }

                dt.Columns.Add(new DataColumn(prop.Name, columnType));
            }

            foreach (T item in entitys)
            {
                DataRow row = dt.NewRow();
                foreach (DataColumn column in dt.Columns)
                {
                    PropertyInfo property = item.GetType().GetProperty(column.ColumnName);
                    row[property.Name] = property.GetValue(item);
                }

                dt.Rows.Add(row);
            }
            return BatchInsert(EumDBWay.Writer, tableAtt.DBName.GetDisplayName(), dt);
        }
    }
}
