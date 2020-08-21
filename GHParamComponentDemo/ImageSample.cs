using System;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace GHComponent1
{
    public class ImageSample : GH_Component
    {
        public ImageSample() : base("ImageSample", "Embryo","ImageSample","Params", "Util") {}
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
                // 电池在shell的位置 
        }
        public override void CreateAttributes()
        {
            base.m_attributes = new ImageSampleAttributes(this);
        }     
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Path", "P", "File Path", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                Bitmap bitmap2 = new Bitmap(24, 24);
                Graphics g = Graphics.FromImage(bitmap2);
                g.DrawImage(GHParamComponentDemo.Properties.Resources.pic1,
                    new RectangleF(0, 0, 24, 24));
                return bitmap2;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("{F6CB254E-70ED-4347-B6A0-9D081C463753}"); }
        }
    }
}
