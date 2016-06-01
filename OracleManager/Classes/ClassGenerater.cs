using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OracleManager
{
    public class ClassGenerater
    {

        public static string GetCSharpClass(DataTable data, string className, bool withWcfDecorators, bool withCollectionClass, string companyName, string nameSpace, string customClassName)
        {
            if (data == null) return string.Empty;

            var cleanClassName = className.SplitterByUnderscore();

            if (customClassName.IsEmpty())
            {
                customClassName = ControlMod.InputBox("", "Class name", cleanClassName);
            }

            cleanClassName = customClassName;
            
            if (nameSpace.IsEmpty())
            {
                nameSpace = "<ToBeSet>";
            }

            var __s =
            "//------------------------------------------------------------------" + Environment.NewLine +
           "// <copyright file=\"" + cleanClassName + ".cs\" company=\"" + companyName + "\">" + Environment.NewLine +
           "//     Copyright (c) " + companyName + " Ltd.  All rights reserved." + Environment.NewLine +
           "// </copyright>" + Environment.NewLine +
           "//" + Environment.NewLine +
           "// <summary>" + Environment.NewLine +
           "// An object that is used to hold the " + cleanClassName + " info." + Environment.NewLine +
           "// </summary>" + Environment.NewLine +
           "//" + Environment.NewLine +
           "// <remarks/>" + Environment.NewLine +
           "//------------------------------------------------------------------" + Environment.NewLine +
           Environment.NewLine +
           "namespace " + nameSpace + Environment.NewLine +
           "{" + Environment.NewLine +
           "    using System.Runtime.Serialization;" + Environment.NewLine +
           "" + Environment.NewLine +
           "    /// <summary>" + Environment.NewLine +
           "    /// An object that is used to hold the " + cleanClassName + "." + Environment.NewLine +
           "    /// </summary>" + Environment.NewLine +
           (withWcfDecorators ? "    [DataContract]" + Environment.NewLine : "") +
           "    public class " + cleanClassName + Environment.NewLine + "    {" + Environment.NewLine;

            __s += data.Columns.Cast<DataColumn>().Select(o =>
                Environment.NewLine +
                "        /// <summary>" + Environment.NewLine +
                "        /// Represents the " + o.ColumnName.SplitterByUnderscore() + Environment.NewLine +
                "        /// </summary>" + Environment.NewLine +
                (withWcfDecorators ? "        [DataMember]" + Environment.NewLine : "") +
                "        public " + GetTypeString(o.DataType) + " " + o.ColumnName.SplitterByUnderscore() + " { get; set; }" + Environment.NewLine
                ).Aggregate((f1, f2) => f1 + f2);
            __s += Environment.NewLine + "    }";
            __s += Environment.NewLine + "}";


            /*-------------------------------------------------- Collection Class -------------------------------------------------*/

            var __sCollection =
            "//------------------------------------------------------------------" + Environment.NewLine +
           "// <copyright file=\"" + cleanClassName + "Collection.cs\" company=\"" + companyName + "\">" + Environment.NewLine +
           "//     Copyright (c) " + companyName + " Ltd.  All rights reserved." + Environment.NewLine +
           "// </copyright>" + Environment.NewLine +
           "//" + Environment.NewLine +
           "// <summary>" + Environment.NewLine +
           "// An object that is used to hold the " + cleanClassName + "Collection info." + Environment.NewLine +
           "// </summary>" + Environment.NewLine +
           "//" + Environment.NewLine +
           "// <remarks/>" + Environment.NewLine +
           "//------------------------------------------------------------------" + Environment.NewLine +
           Environment.NewLine +
           "namespace " + nameSpace + Environment.NewLine +
           "{" + Environment.NewLine +
           "    using System.Collections.Generic;" + Environment.NewLine +
           "    using System.Runtime.Serialization;" + Environment.NewLine +
           "" + Environment.NewLine +
           "    /// <summary>" + Environment.NewLine +
           "    /// An object that is used to hold the " + cleanClassName + "Collection." + Environment.NewLine +
           "    /// </summary>" + Environment.NewLine +
           (withWcfDecorators ? "    [CollectionDataContract]" + Environment.NewLine : "") +
           "    public class " + cleanClassName + "Collection : List<" + cleanClassName + ">" + Environment.NewLine +
           "    {" + Environment.NewLine;

            __sCollection += Environment.NewLine + "    }";
            __sCollection += Environment.NewLine + "}";

            System.IO.File.WriteAllText(ControlMod.CombinePath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), cleanClassName + ".cs"), __s);
            System.IO.File.WriteAllText(ControlMod.CombinePath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), cleanClassName + "Collection.cs"), __sCollection);

            return __s;
        }

        public static string GetCSharpSelectFunctions(DataTable data, string className, string customClassName)
        {
            if (data == null) return string.Empty;

            var cleanClassName = className.SplitterByUnderscore();

            if (customClassName.IsEmpty())
            {
                customClassName = ControlMod.InputBox("", "Class name", cleanClassName);
            }

            cleanClassName = customClassName;

            var __s =
           @"        /// <summary>
        /// Retrieves " + cleanClassName + @".
        /// </summary>
        public " + cleanClassName + @"Collection Get" + (cleanClassName.EndsWith("y") ? cleanClassName.Substring(0, cleanClassName.Length - 1) + "ie" : cleanClassName) + @"s()
        {
            return DataOperation(ds =>
            {

                var results = ds.ExecuteQuery<" + cleanClassName + @">(
                                    @""SELECT " + Environment.NewLine;

            __s += data.Columns.Cast<DataColumn>().Select(o =>
                "                                     " + GetColumnQuerySlice(o)
                ).Aggregate((f1, f2) => f1 + ", " + Environment.NewLine + f2);

            __s += Environment.NewLine +
                @"                                    FROM " + className + @""");

                var " + (cleanClassName[0].ToString().ToLower() + cleanClassName.Substring(1)) + @"Collection = new " + cleanClassName + @"Collection();

                if (NotEmpty(results))
                {
                    " + (cleanClassName[0].ToString().ToLower() + cleanClassName.Substring(1)) + @"Collection.AddRange(results);
                }

                return " + (cleanClassName[0].ToString().ToLower() + cleanClassName.Substring(1)) + @"Collection;

            });

        }" + Environment.NewLine + Environment.NewLine;

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
                case "System.Decimal":
                    res = "decimal";
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

        public static string GetColumnQuerySlice(DataColumn col)
        {
            var res = "";
            switch (col.DataType.ToString())
            {
                case "System.String":
                case "System.DateTime":
                    res = col.ColumnName + " " + col.ColumnName.SplitterByUnderscore();
                    break;
                case "System.Double":
                    res = "cast(" + col.ColumnName + " as FLOAT) " + col.ColumnName.SplitterByUnderscore();
                    break;
                case "System.Decimal":
                    res = "cast(" + col.ColumnName + " as NUMBER(19)) " + col.ColumnName.SplitterByUnderscore();
                    break;
                case "System.Int32":
                    res = "cast(" + col.ColumnName + " as NUMBER(9)) " + col.ColumnName.SplitterByUnderscore();
                    break;
                case "System.Int64":
                    res = "cast(" + col.ColumnName + " as NUMBER(18)) " + col.ColumnName.SplitterByUnderscore();
                    break;
                default:
                    res = col.ColumnName + " " + col.ColumnName.SplitterByUnderscore();
                    break;
            }
            return res;
        }
    }
}
