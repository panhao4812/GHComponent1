using GH_IO.Serialization;
using Grasshopper.Getters;
using Grasshopper.GUI.HTML;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Input.Custom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GHComponent1
{
  public  class Param_Line2 : GH_PersistentGeometryParam<GH_Curve>
    {
      public Param_Line2()
            : base(new GH_InstanceDescription("Line2", "Line2", "Test", "Params", "Util"))
{
                        
}
    public override GH_Exposure Exposure
    {
        get
        {
            return GH_Exposure.secondary;
        }
    }
    protected override GH_Curve InstantiateT()
    {
        return new GH_Curve();
    }

    protected override GH_GetterResult Prompt_Singular(ref GH_Curve value)
  
        {
            Rhino.Input.Custom.GetObject obj2= new GetObject();
                obj2.SetCommandPrompt("LinearDimension  reference");
                obj2.GeometryFilter = ObjectType.Curve;
            Rhino.Input.GetResult result = obj2.Get();
           
            if (result !=  Rhino.Input. GetResult.Object)
            {
                return GH_GetterResult.accept;
            }

            GH_Curve dm = new GH_Curve(obj2.Object(0).ObjectId);
            if (dm != null)
            {
                value = dm;
                return GH_GetterResult.success;
            }
            else
            {
                return GH_GetterResult.cancel;
            }
        }
    protected override GH_GetterResult Prompt_Plural(ref List<GH_Curve> values)
        {
            values = GH_CurveGetter.GetCurves();
            if (values != null)
            {
                return GH_GetterResult.success;
            }
            return GH_GetterResult.cancel;

        }

        public override Guid ComponentGuid
        {
            get { return new Guid("{EEECFD05-F87B-49D7-ACC0-FEE1C7AA6697}"); }
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                Bitmap bitmap2 = new Bitmap(24, 24);
                Graphics g = Graphics.FromImage(bitmap2);
                g.DrawImage(GHComponent1.Properties.Resources.pic3,
                    new RectangleF(0, 0, 24, 24));
                return bitmap2;
            }
        }
    }
}
