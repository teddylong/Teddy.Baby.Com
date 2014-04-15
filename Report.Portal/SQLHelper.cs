using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace Report.Portal
{
    public class SQLHelper
    {
        private static string ConnectionStr = ConfigurationManager.AppSettings["ATDataBaseCI"];
        
        /// <summary>
        /// 执行查询操作
        /// </summary>
        /// <param name="sqlCommand">SQL语句</param>
        /// <param name="param">参数数组</param>
        /// <returns></returns>
        public static DataTable GetDataTableBySql(string sqlCommand, SqlParameter[] param)
        {
            DataTable dtSelected = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionStr))
            {
                using (SqlCommand comm = new SqlCommand(sqlCommand, conn))
                {
                    try
                    {
                        comm.Parameters.AddRange(param);
                        conn.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(comm))
                        {
                            da.Fill(dtSelected);

                        }
                        return dtSelected;
                    }
                    catch (Exception ex)
                    {
                        
                        conn.Close();
                        return dtSelected;
                    }

                }
            }

        }

        public static DataTable GetDataTableBySql(string sqlCommand)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable datatable = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                try
                {
                    conn.Open();
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = sqlCommand;
                    da.SelectCommand = comm;
                    da.Fill(datatable);
                }
                catch (Exception ex)
                {
                    
                    conn.Close();
                }
            }

            return datatable;
        }

        public static string GetValueBySqlAndKey(string sqlCommand, string key)
        {
            DataTable datatable = GetDataTableBySql(sqlCommand);
            if (null == datatable || datatable.Rows.Count == 0)
                return string.Empty;
            return datatable.Rows[0][key].ToString();
        }

        public static bool ExeSQLCmd(string sqlCommand)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStr))
            {
                SqlCommand comm = new SqlCommand(sqlCommand, conn);
                try
                {
                    conn.Open();
                    comm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    
                    conn.Close();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 执行修改数据库操作
        /// </summary>
        /// <param name="sqlCommand">SQL语句</param>
        /// <param name="param">参数数组</param>
        /// <returns>影响的行数(-1:出错;0:无影响;>0:表示有影响，且返回的是影响的行数)</returns>
        public static int ExceSqlCmd(string sqlCommand, SqlParameter[] param)
        {
            int effectline = 0;
            using (SqlConnection conn = new SqlConnection(ConnectionStr))
            {
                using (SqlCommand comm = new SqlCommand(sqlCommand, conn))
                {
                    try
                    {
                        comm.Parameters.AddRange(param);
                        conn.Open();
                        effectline = comm.ExecuteNonQuery();
                        return effectline;
                    }
                    catch (Exception ex)
                    {
                        effectline = -1;
                        
                        conn.Close();
                        return effectline;
                    }

                }
            }

        }
        public static int ExecSQLCmd(string sqlCommand)
        {
            int effectline = 0;
            using (SqlConnection conn = new SqlConnection(ConnectionStr))
            {
                using (SqlCommand comm = new SqlCommand(sqlCommand, conn))
                {
                    try
                    {
                        conn.Open();
                        effectline = comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                       
                        conn.Close();
                    }
                }
            }
            return effectline;
        }

        /// <summary>
        /// 插入SQL，并获得自动生成的ID号
        /// </summary>
        /// <param name="insertSqlCommand"></param>
        /// <returns></returns>
        public static string InsertSQL(string insertSqlCommand)
        {
            string id = string.Empty;
            using (SqlConnection conn = new SqlConnection(ConnectionStr))
            {
                SqlCommand comm = new SqlCommand(insertSqlCommand, conn);
                try
                {
                    conn.Open();
                    comm.ExecuteNonQuery();
                    string sql = "SELECT @@IDENTITY";
                    comm = new SqlCommand(sql, conn);
                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.Read())
                    {
                        id = reader[0].ToString();
                    }
                }
                catch (Exception ex)
                {
                    
                    conn.Close();
                }
            }
            return id;
        }

        /// <summary>
        /// Execute SP Return DataTable with Paras zhaojiang 
        /// </summary>
        /// <param name="SP"></param>
        /// <returns></returns>
        public static DataTable ExecSPWithParaRtnTable1(string SP, SqlParameter[] paras)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionStr))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(SP, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddRange(paras);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds, "table");
                    cmd.Parameters.Clear();
                    return ds.Tables["table"];
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        /// <summary>
        /// Execute SP Return DataTable which not including paras zhaojiang 
        /// </summary>
        /// <param name="SP"></param>
        /// <returns></returns>
        public static DataTable ExecSPWithParaRtnTable1(string SP)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStr))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(SP, conn);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds, "table");
                    cmd.Parameters.Clear();
                    return ds.Tables["table"];
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>    
        /// 执行带参数的SQL语句    
        /// </summary>    
        /// <param name="Sqlstr">SQL语句</param>    
        /// <param name="param">参数对象数组</param>    
        /// <returns></returns>    
        public static int ExecuteSql(String Sqlstr, SqlParameter[] param)
        {
            String ConnStr = ConnectionStr;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = Sqlstr;
                    cmd.Parameters.AddRange(param);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return 1;
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Execute SQL Return table
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public static DataTable ExecSQLRtnTb(string SQL, SqlParameter[] paras)
        {
            String ConnStr = ConnectionStr;
            using (SqlConnection conn = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand(SQL, conn);

                try
                {
                    using (conn)
                    {
                        cmd.Parameters.AddRange(paras);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        sda.Fill(ds, "table");
                        cmd.Parameters.Clear();
                        return ds.Tables["table"];
                    }
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// 只执行SQL命令 无参数
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <returns></returns>
        public static DataTable GetDataTableBySqlNoParm(string sqlCommand)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable datatable = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                try
                {
                    conn.Open();
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = sqlCommand;
                    da.SelectCommand = comm;
                    da.Fill(datatable);
                }
                catch (Exception ex)
                {
                   
                    conn.Close();
                }
            }

            return datatable;
        }

    }

}
