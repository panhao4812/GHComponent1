using System;
using System.Collections.Generic;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
namespace GHComponent1
{
  public  class Param_Mesh2 : GH_PersistentGeometryParam<GH_Mesh>
    {
        public Param_Mesh2()
            : base(new GH_InstanceDescription("Mesh2", "Mesh2", "Contains a collection of polygon meshes", "Params", "Util"))
{
    this.m_hidden = false;
}

 
    private bool m_hidden;
    public override GH_Exposure Exposure
    {
        get
        {
            return GH_Exposure.secondary;
        }
    }

        protected override GH_GetterResult Prompt_Plural(ref List<GH_Mesh> values)
        {
            values = Grasshopper.Getters.GH_MeshGetter.GetMeshes();
            if ((values != null) && (values.Count != 0))
            {
                return GH_GetterResult.success;
            }
            return GH_GetterResult.cancel;
        }


        protected override GH_GetterResult Prompt_Singular(ref GH_Mesh value)
        {
            value = Grasshopper.Getters.GH_MeshGetter.GetMesh();
            if (value == null)
            {
                return GH_GetterResult.cancel;
            }
            return GH_GetterResult.success;
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
