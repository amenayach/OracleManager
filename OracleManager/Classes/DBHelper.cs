using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

public static class DBHelper
{

    public static DataTable GetDatatable(OracleConnection _con, string sQuery, params OracleParameter[] OracleParams)
    {
        DataTable res = new DataTable();
        try
        {
            using (var da = new OracleDataAdapter(sQuery,_con))
            {
                if (_con.State != ConnectionState.Open) _con.Open();
                da.SelectCommand.Parameters.AddRange(OracleParams);
                da.Fill(res);
            }
        }
        catch (Exception ex)
        {
            ControlMod.PromptMsg(ex);
        }
        return res;
    }

    public static object GetScalar(OracleConnection _con, string sQuery, params OracleParameter[] OracleParams)
    {
        object res = null;
        try
        {
            using (var cmd = new OracleCommand(sQuery, _con))
            {
                if (_con.State != ConnectionState.Open) _con.Open();
                cmd.Parameters.AddRange(OracleParams);
                res = cmd.ExecuteScalar();
            }
        }
        catch (Exception ex)
        {
            ControlMod.PromptMsg(ex);
        }
        return res;
    }

    public static bool ExecCommand(OracleConnection _con, string sQuery, params OracleParameter[] OracleParams)
    {
        bool res = false;
        try
        {
            using (var cmd = new OracleCommand(sQuery, _con))
            {
                if (_con.State != ConnectionState.Open) _con.Open();
                cmd.Parameters.AddRange(OracleParams);
                cmd.ExecuteNonQuery();
                res = true;
            }
        }
        catch (Exception ex)
        {
            ControlMod.PromptMsg(ex);
        }
        return res;
    }

    public static List<T> ExecuteQuery<T>(string s, OracleConnection condb, params OracleParameter[] Params)
    {
        List<T> res = new List<T>();
        string er = "";
        OracleDataReader r = null;
        try
        {
            if (condb == null)
                throw new Exception("Connection is NULL");
            if (string.IsNullOrEmpty(s))
                throw new Exception("The query string is empty");
            using (OracleCommand cm = new OracleCommand(s, condb))
            {
                if (Params.Length > 0)
                {
                    cm.Parameters.AddRange(Params);
                }
                if (cm.Connection.State != ConnectionState.Open)
                    cm.Connection.Open();
                r = cm.ExecuteReader();

                var prps = typeof(T).GetProperties();
                var prpNames = prps.Select(f => f.Name).ToList();

                if (r.HasRows)
                {
                    while (r.Read())
                    {
                        if (typeof(T).FullName.Contains("System."))
                        {
                            res.Add((T)r[0]);
                            // Classes
                        }
                        else
                        {
                            var c = (T)Activator.CreateInstance(typeof(T));
                            for (int j = 0; j <= r.FieldCount - 1; j++)
                            {
                                var jj = j;
                                //er = dt.Rows(jj)("ColumnName").ToLower
                                var fname = r.GetName(j).ToString();
                                er = fname;
                                var fType = r.GetProviderSpecificFieldType(j).ToString().ToLower();
                                var p = prps.Where(f => f.Name.Trim().ToLower() == fname.ToLower()).ToList();
                                if (p.Count > 0)
                                {
                                    //Date or DateTime
                                    if (fType.Contains("date"))
                                    {
                                        if (!p[0].PropertyType.FullName.ToLower().Contains("system.nullable") && (r[fname] == null || r[fname].Equals(System.DBNull.Value)))
                                        {
                                            p[0].SetValue(c, DateTime.Now, null);
                                        }
                                        else
                                        {
                                            if (!(p[0].PropertyType.FullName.ToLower().Contains("system.nullable") && (r[fname] == null || r[fname].Equals(System.DBNull.Value))))
                                            {
                                                p[0].SetValue(c, r[fname], null);
                                            }
                                        }
                                        //String
                                    }
                                    else if (fType.Contains("string"))
                                    {
                                        if (r[fname] == null || r[fname].Equals(System.DBNull.Value))
                                        {
                                            p[0].SetValue(c, "", null);
                                        }
                                        else
                                        {
                                            p[0].SetValue(c, r[fname], null);
                                        }
                                    }
                                    else
                                    {
                                        if (!(p[0].PropertyType.FullName.ToLower().Contains("system.nullable") && (r[fname] == null || r[fname].Equals(System.DBNull.Value))))
                                        {
                                            p[0].SetValue(c, r[fname], null);
                                        }
                                    }
                                }
                            }
                            res.Add(c);
                        }
                    }
                }
                r.Close();

            }
            //If cm IsNot Nothing Then
            //    'cm.Connection.Close()
            //    cm.Dispose()
            //End If

        }
        catch (Exception ex)
        {
            if (r != null && r.IsClosed == false)
                r.Close();
            throw ex;
        }
        return res;
    }

    //public static string OracleDate(string FieldName)
    //{
    //    var s = " DATEADD(HOUR, - DATEPART(HOUR, " + FieldName + "), " +
    //                         "DATEADD(MINUTE, - DATEPART(MINUTE, " + FieldName + ")," +
    //                         " DATEADD(SECOND, - DATEPART(SECOND, " + FieldName + ")," +
    //                         " DATEADD(MILLISECOND, " +
    //                         " - DATEPART(MILLISECOND, " + FieldName + "), " + FieldName + ")))) ";
    //    return s;
    //}

    public static string ToOracle(this DateTime dt)
    {
        return "'" + dt.ToString("yyyy-MM-dd HH:mm:ss:ttt") + "'";
    }
}
