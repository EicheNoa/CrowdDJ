using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdDJ.Interfaces
{
    public interface IDataBase
    {
        DbCommand CreateCommand(string sql);
        int DeclareParameter(DbCommand command, string name, DbType dbType);
        void DefineParameter(DbCommand command, string name, DbType dbType, object value);
        void SetParameter(DbCommand command, string name, object value);
        IDataReader ExecuteReader(DbCommand command);
        int ExecuteNonQuery(DbCommand command);
        //Dataset specific methods
        void LoadDataTable(DataSet ds, string tableName, DbCommand selectCommand);
        void UpdateDataTable(DataSet ds, string tableName, DbCommand selectCommand);
    }
}
