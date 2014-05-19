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
  public  class Param_Line2 : GH_PersistentGeometryParam<GH_Mesh2>
    {
      public Param_Line2() : base(new GH_InstanceDescription("Mesh2", "M2", "Test", "Params", "Util")) { }
    public override GH_Exposure Exposure
    {
        get
        {
            return GH_Exposure.secondary;
        }
    }
    protected override GH_Mesh2 InstantiateT()
    {
        return new GH_Mesh2();
    }
    protected override GH_GetterResult Prompt_Singular(ref GH_Mesh2 value)  
        {
            Rhino.Input.Custom.GetObject obj2= new GetObject();
                obj2.SetCommandPrompt("Mesh  reference");
                obj2.GeometryFilter = ObjectType.Mesh;
            Rhino.Input.GetResult result = obj2.Get();
           
            if (result !=  Rhino.Input. GetResult.Object)
            {
                return GH_GetterResult.accept;
            }

            GH_Mesh2 dm = new GH_Mesh2(obj2.Object(0).ObjectId);
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
    protected override GH_GetterResult Prompt_Plural(ref List<GH_Mesh2> values)
        {
            List<GH_Mesh> meshes =GH_MeshGetter.GetMeshes();
            values = new List<GH_Mesh2>();
            for (int i = 0; i < meshes.Count; i++)
            {
                values.Add(new GH_Mesh2(meshes[i]));
            }
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
                g.DrawImage(GHComponent1.Properties.Resources.pic3,new RectangleF(0, 0, 24, 24));
                return bitmap2;
            }
        }
      ////////////
    }
}
