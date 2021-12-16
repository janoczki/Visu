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
        #region declaration

        public enum columns
        {
            ID, datapointName, datapointDescription, datapointDatatype, datapointSave, datapointCOV, deviceIP, deviceInstance, objectType, objectInstance,
            txt00, txt01, txt02, txt03, txt04, txt05, txt06, txt07, txt08, txt09, txt10, txt11, txt12, txt13, txt14, txt15,
            value
        }

        public static string[] header;
        public static string[] content;

        public static List<string> error = new List<string>();

        #endregion

        #region column check

        public static bool isTableUniquenessCorrect(string[] content)
        {
            var columnsToCheck = new List<int> { (int)columns.ID, (int)columns.datapointName };
            foreach (int columnIndex in columnsToCheck)
            {
                if (!isColumnUnique(content, columnIndex))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool isColumnUnique(string[] content, int index)
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
                    error.Add("Datapoint name and/or ID isn't unique near line " + Array.IndexOf(content, row) + 1);
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region type check

        public static bool isTableTypeCorrect(string[] content)
        {
            foreach (string row in content)
            {
                if (!isDatapointTypeCorrect(row))
                {
                    error.Add("Datapoint type incorrect near line " + Array.IndexOf(content, row) + 1);
                    return false;
                }
            }
            return true;
        }

        public static bool isDatapointTypeCorrect(string row)
        {
            var objectType = row.Split(';')[(int)columns.objectType];
            var datapointDatatype = row.Split(';')[(int)columns.datapointDatatype];
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

        #endregion

        #region stetetext check

        public static bool isTableStateTextCorrect(string[] content)
        {
            foreach (string row in content)
            {
                if (!isDatapointStateTextCorrect(row))
                {
                    error.Add("StateText incorrect near line " + Array.IndexOf(content, row) + 1);
                    return false;
                }
            }
            return true;
        }

        public static bool isDatapointStateTextCorrect(string row)
        {
            var objectType = row.Split(';')[(int)columns.objectType];

            if (objectType != "AI" && objectType != "AO" && objectType != "AV" && objectType != "PIV" && objectType != "SC")
            {
                var stateTexts = string.Join("", new ArraySegment<string>(row.Split(';'), (int)columns.txt00, row.Split(';').Length - (int)columns.txt00));
                if (stateTexts == "")
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        public static void readfile()
        {
            try
            {
                var file = File.ReadAllLines(_global.path + "\\datapoints.bacnetip", Encoding.Default).ToArray();
                content = file.Skip(1).ToArray();
                var head = file[0] + ";Value";
                header = head.Split(';');
            }
            catch (FileNotFoundException)
            {
                content = null;
                header = null;
                MessageBox.Show("The file defining Bacnet IP objects doesn't exist.");
            }
        }

        public static void readHeder()
        {

        }

        public static string[] getTable()
        {
            try
            {
                readfile();
                if (content != null & isTableUniquenessCorrect(content) & isTableTypeCorrect(content) & isTableStateTextCorrect(content))
                {
                    return content;
                }
                throw new ArgumentException(string.Join(Environment.NewLine, error));
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Datapoint definition file error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }


}
