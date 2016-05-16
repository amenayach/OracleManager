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

        private static string companyName = string.Empty;

        public static string GetCSharpClass(DataTable data, string className, bool withWCFDecorators, bool withCollectionClass, string companyName, string nameSpace)
        {
            if (data == null) return string.Empty;
            
            var cleanClassName = className.SplitterByUnderscore();

            var customClassName = ControlMod.InputBox("", "Class name", cleanClassName);
            if (customClassName.NotEmpty())
            {
                cleanClassName = customClassName;
            }

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
           "namespace "+ nameSpace + Environment.NewLine +
           "{" + Environment.NewLine +
           "    using System.Runtime.Serialization;" + Environment.NewLine +
           "" + Environment.NewLine +
           "    /// <summary>" + Environment.NewLine +
           "    /// An object that is used to hold the " + cleanClassName + "." + Environment.NewLine +
           "    /// </summary>" + Environment.NewLine +
           (withWCFDecorators ? "    [DataContract]" + Environment.NewLine : "") +
           "    public class " + cleanClassName + Environment.NewLine + "    {" + Environment.NewLine;

            __s += data.Columns.Cast<DataColumn>().Select(o =>
                Environment.NewLine +
                "        /// <summary>" + Environment.NewLine +
                "        /// Represents the " + o.ColumnName.SplitterByUnderscore() + Environment.NewLine +
                "        /// </summary>" + Environment.NewLine +
                (withWCFDecorators ? "        [DataMember]" + Environment.NewLine : "") +
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
           "namespace "+ nameSpace + Environment.NewLine +
           "{" + Environment.NewLine +
           "    using System.Collections.Generic;" + Environment.NewLine +
           "    using System.Runtime.Serialization;" + Environment.NewLine +
           "" + Environment.NewLine +
           "    /// <summary>" + Environment.NewLine +
           "    /// An object that is used to hold the " + cleanClassName + "Collection." + Environment.NewLine +
           "    /// </summary>" + Environment.NewLine +
           (withWCFDecorators ? "    [CollectionDataContract]" + Environment.NewLine : "") +
           "    public class " + cleanClassName + "Collection : List<" + cleanClassName + ">" + Environment.NewLine +
           "    {" + Environment.NewLine;

            __sCollection += Environment.NewLine + "    }";
            __sCollection += Environment.NewLine + "}";

            System.IO.File.WriteAllText(ControlMod.CombinePath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), cleanClassName + ".cs"), __s);
            System.IO.File.WriteAllText(ControlMod.CombinePath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), cleanClassName + "Collection.cs"), __sCollection);

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
