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
        public static string GetCSharpClass(DataTable data, string className, bool withWCFDecorators)
        {
            if (data == null) return string.Empty;

            var __s =
            "    /// <summary>" + Environment.NewLine +
            "    /// An object that is used to hold the " + className.SplitterByUnderscore() + "." + Environment.NewLine +
            "    /// </summary>" + Environment.NewLine +
            (withWCFDecorators ? "    [DataContract]" + Environment.NewLine : "") +
            "    public class " + className.SplitterByUnderscore() + " {" + Environment.NewLine;

            __s += data.Columns.Cast<DataColumn>().Select(o =>
                Environment.NewLine +
                "        /// <summary>" + Environment.NewLine +
                "        /// Represents the " + o.ColumnName.SplitterByUnderscore() + Environment.NewLine +
                "        /// </summary>" + Environment.NewLine +
                (withWCFDecorators ? "        [DataMember]" + Environment.NewLine : "") +
                "        public " + GetTypeString(o.DataType) + " " + o.ColumnName.SplitterByUnderscore() + " { get; set; }" + Environment.NewLine
                ).Aggregate((f1, f2) => f1 + f2);
            __s += Environment.NewLine + "    }";
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
