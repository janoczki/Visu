using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Visu_dataviewer
{
    public static class DatapointDefinition
    {
        public static string[] Header;
        public static string[] Content;
        public static List<string> Error = new List<string>();
        public enum Columns
        {
            Id, DatapointName, DatapointDescription, DatapointDatatype, DatapointSave, DatapointCov, DeviceIp, DeviceInstance, ObjectType, ObjectInstance,
            Txt00, Txt01, Txt02, Txt03, Txt04, Txt05, Txt06, Txt07, Txt08, Txt09, Txt10, Txt11, Txt12, Txt13, Txt14, Txt15,
            Value
        }

        public static bool IsTableUniquenessCorrect(string[] content)
        {
            var columnsToCheck = new List<int> { (int)Columns.Id, (int)Columns.DatapointName };
            foreach (int columnIndex in columnsToCheck)
            {
                if (!IsColumnUnique(content, columnIndex))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsColumnUnique(string[] content, int index)
        {
            var propertyList = new List<string>();
            foreach (string row in content)
            {
                var propertyToCheck = row.Split(';')[index];
                if (!propertyList.Contains(propertyToCheck))
                {
                    propertyList.Add(propertyToCheck);
                }
                else
                {
                    Error.Add("Datapoint name and/or ID isn't unique near line " + Array.IndexOf(content, row) + 1);
                    return false;
                }
            }
            return true;
        }

        public static bool IsTableTypeCorrect(string[] content)
        {
            foreach (string row in content)
            {
                if (!IsDatapointTypeCorrect(row))
                {
                    Error.Add("Datapoint type incorrect near line " + Array.IndexOf(content, row) + 1);
                    return false;
                }
            }
            return true;
        }

        public static bool IsDatapointTypeCorrect(string row)
        {
            var objectType = row.Split(';')[(int)Columns.ObjectType];
            var datapointDatatype = row.Split(';')[(int)Columns.DatapointDatatype];
            switch (datapointDatatype)
            {
                case "binary":
                    if (objectType != "BI" && objectType != "BO" && objectType != "BV" && objectType != "SC") return false;
                    break;
                case "int":
                    if (objectType != "MI" && objectType != "MO" && objectType != "MV" && objectType != "PIV" && objectType != "SC") return false;
                    break;
                case "float":
                    if (objectType != "AI" && objectType != "AO" && objectType != "AV" && objectType != "SC") return false;
                    break;
            }
            return true;
        }

        public static bool IsTableStateTextCorrect(string[] content)
        {
            foreach (string row in content)
            {
                if (!IsDatapointStateTextCorrect(row))
                {
                    Error.Add("StateText incorrect near line " + Array.IndexOf(content, row) + 1);
                    return false;
                }
            }
            return true;
        }

        public static bool IsDatapointStateTextCorrect(string row)
        {
            var objectType = row.Split(';')[(int)Columns.ObjectType];

            if (objectType != "AI" && objectType != "AO" && objectType != "AV" && objectType != "PIV" && objectType != "SC")
            {
                var stateTexts = string.Join("", new ArraySegment<string>(row.Split(';'), (int)Columns.Txt00, row.Split(';').Length - (int)Columns.Txt00));
                if (stateTexts == "")
                {
                    return false;
                }
            }
            return true;
        }

        public static void Readfile()
        {
            try
            {
                var file = File.ReadAllLines(global.Path + "\\datapoints.bacnetip", Encoding.Default).ToArray();
                Content = file.Skip(1).ToArray();
                var head = file[0] + ";Value";
                Header = head.Split(';');
            }
            catch (FileNotFoundException)
            {
                Content = null;
                Header = null;
                MessageBox.Show("The file defining Bacnet IP objects doesn't exist.");
            }
        }

        public static string[] GetTable()
        {
            try
            {
                Readfile();
                if (Content != null & IsTableUniquenessCorrect(Content) & IsTableTypeCorrect(Content) & IsTableStateTextCorrect(Content))
                {
                    return Content;
                }
                throw new ArgumentException(string.Join(Environment.NewLine, Error));
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Datapoint definition file error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }


}
