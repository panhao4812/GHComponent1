using System;
using System.Collections.Generic;

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
                return Properties.Resources.Reimu ;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("{0078207c-bc94-4684-802e-b57142698a22}"); }
        }
    }
}
