using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TinyEdu.Common.Dapper.Persistence.Data;

namespace TinyEdu.Common.Dapper.Persistence.Mapper
{
    public interface IMapper
    {
        IDbConnection GetConnection(string connectionStr);
        string GetNewCommand(Type type);
        string GetUpdateCommand(Type type);
        string GetRemoveCommand(Type type);
        string GetKeyCommand(Type type);
        string GetPageCommand(Type type, string filter, string order, int pageIndex, int pageSize);
        string GetPageCount(Type type, string filter);

        IDbCommand CreateCommandParams(IDbCommand command, DataParameterInfo[] paramsInfo);
        string GetPageCommandByMultilist(string sql, int pageIndex, int pageSize);
        string GetPageCommandByMultilist(string sql, string order, int pageIndex, int pageSize);
        string GetPageCountByMultilist(string froms, string filter);
    }
}
