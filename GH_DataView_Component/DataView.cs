using System;
using System.Drawing;
using System.Windows.Forms;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.GUI;
using GH_IO.Serialization;
using System.IO;
using Grasshopper.GUI.Canvas;
using Grasshopper.GUI.Base;

namespace GH_DataView_Component
{
    public class DataView : GH_Component
    {      
        public int table_Width = 5;
        public int table_Height = 5;
        public int CellWidth = 70;
        public int CellHeight = 25;
        public string[,] table0 = new string[5, 5];
        public Color BackGroundColor = Color.WhiteSmoke;
        public Color ForeColor = Color.FromArgb(90, Color.Black);
        public bool _edit = true;
        public bool _save = true;
        public bool Collapse = false;
        public bool ShowGridHead = true;
        public bool newTable = false;
        public bool isEdit = false;
        public void ShowEditDialog()
        {
            if (isEdit) return;
            if (this.RuntimeMessageLevel == GH_RuntimeMessageLevel.Error) return;
            DataForm1 f = new DataForm1(this);
            GH_WindowsFormUtil.CenterFormOnCursor(f, true);
            if (f.ShowDialog(Instances.DocumentEditor) == DialogResult.OK)
            {
                isEdit = true;
            }
        }
        private void Menu_EditNotesClick(object sender, EventArgs e)
        {
            ShowEditDialog();
        }
        private void Menu_SaveData(object sender, EventArgs e)
        {
            DataGridView2Excel_cvs();
        }
        private void Menu_ShowHead(object sender, EventArgs e)
        {
            ShowGridHead = !ShowGridHead;
        }
        private void Menu_CollapseTable(object sender, EventArgs e)
        {
            Collapse = !Collapse;
            this.Attributes.ExpireLayout();
            Instances.RedrawCanvas();
        }
        private void ColourPicker_BackGroundColourChanged(GH_ColourPicker sender, GH_ColourPickerEventArgs e)
        {
            this.BackGroundColor = e.Colour;
            Instances.RedrawCanvas();
        }
        private void ColourPicker_ForeColourChanged(GH_ColourPicker sender, GH_ColourPickerEventArgs e)
        {
            this.ForeColor = e.Colour;
            Instances.RedrawCanvas();
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("0B6E0FAB-C07A-4071-851B-2F8246532FDE"); }
        }
        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            //this.Menu_AppendObjectName(menu);
            if (_edit)
            {
                GH_DocumentObject.Menu_AppendSeparator(menu);         
            ToolStripMenuItem item1 =
                GH_DocumentObject.Menu_AppendItem(menu, "EditForm", new EventHandler(this.Menu_EditNotesClick));
            //item1.Font = GH_FontServer.NewFont(item1.Font, FontStyle.Bold);                                    
            ToolStripMenuItem item2 =
                GH_DocumentObject.Menu_AppendItem(menu, "Show Head", new EventHandler(this.Menu_ShowHead));
                if (ShowGridHead) item2.Text = "Hide Head";
            }
            GH_DocumentObject.Menu_AppendSeparator(menu);
            ToolStripMenuItem item3 =
                GH_DocumentObject.Menu_AppendItem(menu, "Collapse Table", new EventHandler(this.Menu_CollapseTable));
            if (Collapse) item3.Text = "Expand Table";
            GH_DocumentObject.Menu_AppendColourPicker(Menu_AppendItem(menu, "ForeColor").DropDown, this.ForeColor, new ColourEventHandler(this.ColourPicker_ForeColourChanged));
            GH_DocumentObject.Menu_AppendColourPicker(Menu_AppendItem(menu, "BackgroundColor").DropDown, this.BackGroundColor, new ColourEventHandler(this.ColourPicker_BackGroundColourChanged));
            GH_DocumentObject.Menu_AppendSeparator(menu);
            if (_save)
            {
                ToolStripMenuItem itemsave =
                GH_DocumentObject.Menu_AppendItem(menu, "SaveData", new EventHandler(this.Menu_SaveData));
            }
            // GH_DocumentObject.Menu_AppendSeparator(menu);
            //this.Menu_AppendEnableItem(menu);
            // GH_DocumentObject.Menu_AppendSeparator(menu);
            //this.Menu_AppendRuntimeMessages(menu);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                Bitmap bitmap2 = new Bitmap(24, 24);
                Graphics g = Graphics.FromImage(bitmap2);
                g.DrawImage(Properties.Resources.p2, new RectangleF(0, 0, 24, 24));
                return bitmap2;
            }
        }
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
            // 电池在shell的位置 
        }
        public override void CreateAttributes()
        {
            base.m_attributes = new DataViewAttributes(this);
        }
        public DataView() : base("DataView", "DataView", "DataViewBox", "Params", "Zian") { }
        public DataView(string str1,string str2,string str3) : base(str1, str2, str3, "Params", "Zian") { }
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Integer", "W", "Width", GH_ParamAccess.item, 5);
            pManager.AddIntegerParameter("Integer", "H", "Height", GH_ParamAccess.item, 5);
        }
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("String", "D", "Data", GH_ParamAccess.tree);
        }     
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int Width_ = 0;
            int height_ = 0;
            DA.GetData<int>(0, ref Width_);
            DA.GetData<int>(1, ref height_);
            if (Width_ < 1) Width_ = 1;
            if (height_ < 1) height_ = 1;
            if (Width_ != table_Width) { table_Width = Width_; newTable = true; }
            if (height_ != table_Height) { table_Height = height_; newTable = true; }
            if (newTable)
            {
                newTable = false;
                table0 = new string[table_Width, table_Height];
            }
            else {
                DataTree<string> output = new DataTree<string>();
                for (int i = 0; i < table_Height; i++)
                {
                    GH_Path path_ = new GH_Path(i);
                    for (int j = 0; j < table_Width; j++)
                    {
                        output.Add(table0[j, i], path_);
                    }
                }
                DA.SetDataTree(0, output);
            }
        }
        public override bool Write(GH_IWriter writer)
        {
            bool flag = base.Write(writer);
            writer.SetInt32("table_Width", table_Width);
            writer.SetInt32("table_Height", table_Height);
            writer.SetBoolean ("table_Collapse", Collapse);
            for (int i = 0; i < table_Height; i++)
            {
                for (int j = 0; j < table_Width; j++)
                {
                    writer.SetString("Table" + i.ToString(), j, table0[j, i]);
                }
            }
            return flag;
        }       
        public override bool Read(GH_IReader reader)
        {
            if (!base.Read(reader)) return false;         
            reader.TryGetInt32("table_Width", ref table_Width);
            reader.TryGetInt32("table_Height", ref table_Height);          
            reader.TryGetBoolean("table_Collapse", ref Collapse);
            if (table_Width < 1) table_Width = 1;
            if (table_Height < 1) table_Height = 1;
            table0 = new string[table_Width, table_Height];
            for (int i = 0; i < table_Height; i++)
            {
                for (int j = 0; j < table_Width; j++)
                {
                    string str = "";
                    reader.TryGetString("Table" + i.ToString(), j, ref str);
                    table0[j, i] = str;
                }
            }       
            return true;
        }
        private void DataGridView2Excel_cvs()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Excel files (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.CreatePrompt = true;
            dlg.Title = "DataForm_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Stream myStream;
                myStream = dlg.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.Default);
                try
                {
                    for (int j = 0; j < table_Height; j++)
                    {
                        string columnValue = "";
                        for (int k = 0; k < table_Width; k++)
                        {
                            if (k > 0)
                            {
                                columnValue += "\t";
                            }
                            if (table0[k, j] == null)
                            {
                                columnValue += "";
                            }
                            else
                            {
                                columnValue += table0[k, j];
                            }
                        }
                        sw.WriteLine(columnValue);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
            }
        }
    }
}
///////////////////////////

