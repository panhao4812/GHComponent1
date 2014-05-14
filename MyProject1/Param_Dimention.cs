using System;
using System.Collections.Generic;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
namespace GHComponent1
{
public class Param_Dimention :  GH_PersistentGeometryParam<GH_LinearDimension>
    {
        protected override Grasshopper.Kernel.GH_GetterResult Prompt_Singular(ref GH_LinearDimension value)
  {     
Rhino.Input.Custom.GetObject go = new Rhino.Input.Custom.GetObject();
go.SetCommandPrompt("GH_LinearDimension");
go.AcceptNothing(true);
 Rhino.DocObjects. ObjectType geometryFilter =Rhino.DocObjects. ObjectType.Annotation;         
      go.GeometryFilter = geometryFilter;
    switch (go.Get())
    {
        case Rhino.Input.GetResult.Object:
            if (go.ObjectCount == 0) return GH_GetterResult.cancel;
            Rhino.Geometry.LinearDimension ab = (Rhino.Geometry.LinearDimension ) go.Object(0).Geometry();
            if (ab != null) value = new GH_LinearDimension(ab);
            return GH_GetterResult.success;
        case Rhino.Input.GetResult.Nothing:
            return GH_GetterResult.accept;
        default:
            return GH_GetterResult.cancel;
    }
  }
        protected override Grasshopper.Kernel.GH_GetterResult Prompt_Plural(ref List<GH_LinearDimension> values)
  {
      Rhino.Input.Custom.GetObject go = new Rhino.Input.Custom.GetObject();
      go.SetCommandPrompt("GH_LinearDimension");
      go.AcceptNothing(true);
      Rhino.DocObjects.ObjectType geometryFilter = Rhino.DocObjects.ObjectType.Annotation;
      go.GeometryFilter = geometryFilter;
     Rhino.Input.GetResult result=go.GetMultiple(0,1000);
     if (result==Rhino.Input.GetResult.Nothing)return GH_GetterResult.accept;

              if (go.ObjectCount!=0){
              for (int i = 0; i < go.ObjectCount; i++)
              {
                  Rhino.Geometry.LinearDimension ab = (Rhino.Geometry.LinearDimension)go.Objects()[i].Geometry();
                  if (ab != null)  values.Add(new GH_LinearDimension(ab));
              }
          if( values.Count>0)return GH_GetterResult.success;
              }
              return GH_GetterResult.cancel;
      
  }
        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.secondary;
            }
        }
        public Param_Dimention() : base(new GH_InstanceDescription("Data2D", "D2", "Data2D", "Params", "Util")) { }
        public override Guid ComponentGuid
        {
            get { return new Guid("{835F558F-3CCC-4882-A05C-791439E2445E}"); }
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                Bitmap bitmap2 = new Bitmap(24, 24);
                Graphics g = Graphics.FromImage(bitmap2);
                g.DrawImage(GHComponent1.Properties.Resources.pic2,
                    new RectangleF(0, 0, 24, 24));
                return bitmap2;
            }
        }
//////////////
}
}