using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Visu_dataviewer
{
    class Datapoint
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Format { get; set; }
        public bool Record { get; set; }
        public bool COV { get; set; }

        public ushort NetworkNumber { get; set; }
        public string DeviceIP { get; set; }
        public uint DeviceInstance { get; set; }
        public string BacnetObjectType { get; set; }
        public uint BacnetObjectInstance { get; set; }
        public List<string> StateTexts { get; set; }
        public string BacnetObjectDataFormat { get; set; }

        public Datapoint(System.Windows.Forms.ListViewItem parameters)
        {
            this.ID = int.Parse(parameters.SubItems[(int)DatapointDefinition.Columns.Id].Text);
            this.Name = parameters.SubItems[(int)DatapointDefinition.Columns.DatapointName].Text;
            this.Description =parameters.SubItems[(int)DatapointDefinition.Columns.DatapointDescription].Text;
            this.Format = parameters.SubItems[(int)DatapointDefinition.Columns.DatapointDatatype].Text;
            this.Record = bool.Parse(parameters.SubItems[(int)DatapointDefinition.Columns.DatapointSave].Text);
            this.COV = bool.Parse(parameters.SubItems[(int)DatapointDefinition.Columns.DatapointCov].Text);
            this.NetworkNumber = 1;
            this.DeviceIP= parameters.SubItems[(int)DatapointDefinition.Columns.DeviceIp].Text;
            this.DeviceInstance=uint.Parse(parameters.SubItems[(int)DatapointDefinition.Columns.DeviceInstance].Text);

            this.BacnetObjectType = parameters.SubItems[(int)DatapointDefinition.Columns.ObjectType].Text;
            this.BacnetObjectInstance = uint.Parse(parameters.SubItems[(int)DatapointDefinition.Columns.ObjectInstance].Text);
            this.BacnetObjectDataFormat = parameters.SubItems[(int)DatapointDefinition.Columns.Txt01].Text == "" ? "value" : "enum";

            var isThereAnyContent = false;
            var stateTextList = new List<string>();
            for (int i = (int)DatapointDefinition.Columns.Txt00; i <= (int)DatapointDefinition.Columns.Txt15; i++)
            {
                stateTextList.Add(parameters.SubItems[i].Text);
                if (parameters.SubItems[i].Text != "") isThereAnyContent = true;
            }
            this.StateTexts = isThereAnyContent ? stateTextList : null;
        }
    }
}
