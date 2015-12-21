using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleManager
{
    public class ClassGenerater
    {
        public static string GetCSharpClass(DataTable data, string ClassName)
        {
            var __s = @"    public class " + ClassName + " {" + Environment.NewLine;
            __s += data.Columns.Cast<DataColumn>().Select(o => "        public " + GetTypeString(o.DataType) + " " + o.ColumnName + " { get; set; }" + Environment.NewLine).Aggregate((f1, f2) => f1 + f2);
            __s += "    }";
            return __s;
        }

        public static string GetTypeString(Type type)
        {
            var res = "string";
            switch (type.ToString())
            {
                case "System.String":
                    res = "string";
                    break;
                case "System.DateTime":
                    res = "DateTime";
                    break;
                case "System.Double":
                    res = "double";
                    break;
                case "System.Int16":
                    res = "short";
                    break;
                case "System.Int64":
                    res = "long";
                    break;
                default:
                    res = type.ToString();
                    break;
            }
            return res;
        }
    }
}
