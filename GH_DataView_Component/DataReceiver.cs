using System;
using System.Drawing;
using System.Windows.Forms;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.GUI;
using GH_IO.Serialization;
using System.IO;
using Grasshopper.Kernel.Types;

namespace GH_DataView_Component
{
    public class DataReceiver : DataView, IGH_Component
    {
        public override Guid ComponentGuid
        {
            get { return new Guid("C2426B5A-DC4E-4E88-ACAF-35BECE373EEE"); }
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                Bitmap bitmap2 = new Bitmap(24, 24);
                Graphics g = Graphics.FromImage(bitmap2);
                g.DrawImage(Properties.Resources.p1, new RectangleF(0, 0, 24, 24));
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

        public DataReceiver() : base("DataReceiver", "DataReceiver", "ReceiverBox") {
            this.BackGroundColor = Color.FromArgb(255, 253, 224);
            this._edit = false;
        }
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("string", "S", "Data", GH_ParamAccess.tree);       
        }
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("String", "D", "Data", GH_ParamAccess.tree);
        }       
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            GH_Structure<GH_String> input = new GH_Structure<GH_String>();     
           if( DA.GetDataTree<GH_String>(0, out input)) {
            int Width_ = input.Branches[0].Count;
            int height_ = input.Branches.Count;
            if (Width_ < 1) Width_ = 1;
            if (height_ < 1) height_ = 1;
            if (Width_ != table_Width) { table_Width = Width_; }
            if (height_ != table_Height) { table_Height = height_; }                    
                table0 = new string[table_Width, table_Height];
                for (int r = 0; r < table_Height; r++)
                {
                    GH_Path path = new GH_Path(r);
                    for (int i = 0; i < table_Width; i++)
                    {    
                        if(input.Branches[r][i].Value!=null)
                       table0[i, r] = input.Branches[r][i].Value;
                    }
                }
                DA.SetDataTree(0, input);
            }
        }    
    }
}
