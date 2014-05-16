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
      value = GH_DimensionGetter.GetLinearDimension2();
      if (value == null)
      {
          return GH_GetterResult.cancel;
      }
      return GH_GetterResult.success;
  }
        protected override Grasshopper.Kernel.GH_GetterResult Prompt_Plural(ref List<GH_LinearDimension> values)
  {
      values = GH_DimensionGetter.GetLinearDimensions2();
      if ((values != null) && (values.Count != 0))
      {
          return GH_GetterResult.success;
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