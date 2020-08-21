using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using Grasshopper.Kernel.Types;

namespace GH_DataView_Component
{
    public class DimensionAnalysis : GH_Component
    {
        public DimensionAnalysis() : base("DimensionAnalysis", "DA", "DimensionAnalysis", "Params", "Zian") { }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("", "", "", GH_ParamAccess.item);
            IGH_Param param = this.Params.Input[0];
            param.MutableNickName = false;
            param.NickName = "Dimension";
            param.Name = "Di";
            param.Description = "Dimension To Input";
            param.Optional = true;
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPlaneParameter("Plane", "P", "Plane", GH_ParamAccess.item);
            pManager.AddPointParameter("PointA", "A", "Point3d", GH_ParamAccess.item);
            pManager.AddPointParameter("PointB", "B", "Point3d", GH_ParamAccess.item);
            pManager.AddTextParameter("Text", "T", "String", GH_ParamAccess.item);
            pManager.AddPointParameter("TextPosition", "L", "Point3d", GH_ParamAccess.item);
            pManager.AddNumberParameter("Number", "N", "Double", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            GH_LinearDimension destination = null;
            DA.GetData<GH_LinearDimension>(0, ref destination);
            if (destination != null)
            {
                
                LinearDimension ld = destination.Value;
                Point3d pa = new Point3d(ld.Arrowhead1End.X, ld.Arrowhead1End.Y, 0);
                Point3d pb = new Point3d(ld.Arrowhead2End.X, ld.Arrowhead2End.Y, 0);
                Point3d pt = new Point3d(ld.TextPosition.X, ld.TextPosition.Y, 0);
                pa.Transform(Transform.PlaneToPlane(Plane.WorldXY, ld.Plane));
                pb.Transform(Transform.PlaneToPlane(Plane.WorldXY, ld.Plane));
                pt.Transform(Transform.PlaneToPlane(Plane.WorldXY, ld.Plane));
                DA.SetData(0, ld.Plane);
                DA.SetData(1, pa);
                DA.SetData(2, pb);
                DA.SetData(3, ld.Text);
                DA.SetData(4, pt);
                DA.SetData(5, ld.NumericValue);
               
                return;
            }
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                Bitmap bitmap2 = new Bitmap(24, 24);
                Graphics g = Graphics.FromImage(bitmap2);
                g.DrawImage(Properties.Resources.p6, new RectangleF(0, 0, 24, 24));
                return bitmap2;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("c120a6f5-f392-4b97-9b23-fc305a439c78"); }
        }
    }
}