using CrowdDJ.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdDJ.DAL
{
    public class DataBase: IDataBase
    {

      private string connectionString;

      public DataBase(string connectionString)
      {
        this.connectionString = connectionString;
      }

      private DbConnection CreateOpenConnection()
      {
        DbConnection conn = new SqlConnection(connectionString);
        conn.Open();
        return conn;

      }

      private DbConnection GetOpenConnection()
      {
        return CreateOpenConnection();

      }

      private void ReleaseConnection(DbConnection conn)
      {
        if (conn != null)
          conn.Close();
      }

      private bool IsSharedConnection()
      {
        return false;
      }


      public DbCommand CreateCommand(string sql)
      {
        return new SqlCommand(sql);
      }

      public int DeclareParameter(DbCommand command, string name, System.Data.DbType dbType)
      {
        if (!command.Parameters.Contains(name))
        {
          return command.Parameters.Add(new SqlParameter(name, dbType));
        }
        else
          throw new ArgumentException("Parameter already declared");
       
      }



      public void DefineParameter(DbCommand command, string name, System.Data.DbType dbType, object value)
      {
        int paramIdx = DeclareParameter(command, name, dbType);
        command.Parameters[paramIdx].Value = value;
      }

      public void SetParameter(DbCommand command, string name, object value)
      {
        if (command.Parameters.Contains(name))
        {
          command.Parameters[name].Value = value;
        }
        else
          throw new ArgumentException("Parameter already declared");
      }

      public System.Data.IDataReader ExecuteReader(DbCommand command)
      {
        DbConnection conn = null;
        try
        {
          conn = GetOpenConnection();
          command.Connection = conn;
          CommandBehavior cmdBehavior = IsSharedConnection() ? CommandBehavior.Default: CommandBehavior.CloseConnection;
          return command.ExecuteReader(cmdBehavior);
        }
        catch
        {
          ReleaseConnection(conn);
          throw;
        }
      }

      public int ExecuteNonQuery(DbCommand command)
      {
        DbConnection conn = null;
        try
        {
          conn = GetOpenConnection();
          command.Connection = conn;
          return command.ExecuteNonQuery();
        }
        finally
        {
          ReleaseConnection(conn);
        }
      }


      public void LoadDataTable(DataSet ds, string tableName, DbCommand selectCommand)
      {
        DbConnection conn= null;
        DbDataAdapter adapter = null;
        try
        {
          conn = GetOpenConnection();
          adapter = CreateDataAdapter(conn, selectCommand);

          adapter.Fill(ds, tableName);

        }
        finally
        {
          if (adapter != null)
          {
            adapter.Dispose();
          }
          ReleaseConnection(conn);
        }
      }

      public void UpdateDataTable(DataSet ds, string tableName, DbCommand selectCommand)
      {
        DbConnection conn = null;
        DbDataAdapter adapter = null;
        try
        {
          conn = GetOpenConnection();
          adapter = CreateDataAdapter(conn, selectCommand);

          adapter.Update(ds, tableName);

        }
        finally
        {
          if (adapter != null)
          {
            adapter.Dispose();
          }
          ReleaseConnection(conn);
        }
      }

      private DbDataAdapter CreateDataAdapter(DbConnection conn,
                                          DbCommand selectCommand)
      {
        DbDataAdapter dataAdapter = new SqlDataAdapter();
        selectCommand.Connection = conn;
        dataAdapter.SelectCommand = selectCommand;
        DbCommandBuilder cmdBuilder = new SqlCommandBuilder();
        cmdBuilder.DataAdapter = dataAdapter;

        return dataAdapter;
      }
    }
}
