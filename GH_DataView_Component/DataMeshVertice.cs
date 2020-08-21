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
using Rhino.Geometry;

namespace GH_DataView_Component
{
    public class DataMeshVertice : DataView, IGH_Component
    {
        public override Guid ComponentGuid
        {
            get { return new Guid("20657694-EB49-47E3-AAFC-DCE0D36D5150"); }
        }
    protected override System.Drawing.Bitmap Icon
        {
            get
            {
                Bitmap bitmap2 = new Bitmap(24, 24);
                Graphics g = Graphics.FromImage(bitmap2);
                g.DrawImage(Properties.Resources.p3, new RectangleF(0, 0, 24, 24));
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

        public DataMeshVertice() : base("DataMeshVertice", "DataMeshVertice", "MeshVerticeBox")
        {
            this.BackGroundColor = Color.FromArgb(255, 203, 224);
            this._edit = true;
        }
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "M", "Data", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "M", "Data", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Mesh meshinput = new Mesh();
            if (DA.GetData(0,ref meshinput))
            {
                int Width_ = 3;
                int height_ = meshinput.Vertices.Count;
                if (Width_ < 1) Width_ = 1;
                if (height_ < 1) height_ = 1;
                if (Width_ != table_Width) { table_Width = Width_; newTable = true; }
                if (height_ != table_Height) { table_Height = height_; newTable = true; }
                for (int r = 0; r < table_Height; r++)
                {
                    try
                    {
                        float x_ = 0, y_ = 0, z_ = 0;
                        if (table0[0, r] != "" && table0[0, r] != null) x_ = Convert.ToSingle(table0[0, r]);                
                        if (table0[1, r] != "" && table0[1, r] != null) y_ = Convert.ToSingle(table0[1, r]);
                        if (table0[2, r] != "" && table0[2, r] != null) z_ = Convert.ToSingle(table0[2, r]);
                        x_ += meshinput.Vertices[r].X;
                        y_ += meshinput.Vertices[r].Y;
                        z_ += meshinput.Vertices[r].Z;
                        meshinput.Vertices[r] = new Point3f(x_, y_, z_);
                    }
                    catch { continue; }
                }
                DA.SetData(0, meshinput);
            }
            if (newTable)
            {
                newTable = false;
                table0 = new string[table_Width, table_Height];
            }
           
        }
    }
}
