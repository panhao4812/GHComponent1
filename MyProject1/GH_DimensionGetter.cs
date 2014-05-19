using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
 using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.Commands;
using Rhino.DocObjects;
using System.Collections.Generic;


namespace GHComponent1
{
    public sealed class GH_DimensionGetter
    {
        // Methods
        private static bool m_reference;
        static GH_DimensionGetter()
{
    m_reference = true;
}


        public static GH_LinearDimension GetLinearDimension1()
        {

            LinearDimension line2;
            if (RhinoGet.GetLinearDimension(out line2) == Result.Success)
            {
                return new GH_LinearDimension(line2);
            }
            return null;


        }
        public static GH_LinearDimension GetLinearDimension2()
        {
            GetObject obj2;
        Label_0000:
            obj2 = new GetObject();
            if (m_reference)
            {
                obj2.SetCommandPrompt("LinearDimension  reference");
                obj2.AddOption("Mode", "Reference");
            }
            else
            {
                obj2.SetCommandPrompt("LinearDimension to copy");
                obj2.AddOption("Mode", "Copy");
            }
            obj2.GeometryFilter = ObjectType.Annotation;
            GetResult result = obj2.Get();
            if (result == GetResult.Option)
            {
                m_reference = !m_reference;
                goto Label_0000;
            }
            if (result != GetResult.Object)
            {
                return null;
            }
            if (!m_reference)
            {
                return new GH_LinearDimension((LinearDimension)obj2.Object(0).Geometry());
            }

            GH_LinearDimension dm =new GH_LinearDimension(obj2.Object(0).ObjectId);
            if ((dm != null) && (!dm.IsGeometryLoaded)) { dm.LoadGeometry(); }
            return dm;
        }
        public static List<GH_LinearDimension> GetLinearDimensions1()
        {
         
            List<GH_LinearDimension> list2 = new List<GH_LinearDimension>();      
               LinearDimension line;
               while (RhinoGet.GetLinearDimension(out line) == Result.Success)
                {
                    list2.Add(new GH_LinearDimension(line));  
                }
               if (list2.Count == 0)
               {
                   return null;
               }
               return list2;     
        }
        public static List<GH_LinearDimension> GetLinearDimensions2()
        {
            GetObject obj2;
        Label_0000:
            obj2 = new GetObject();
            if (m_reference)
            {
                obj2.SetCommandPrompt("LinearDimension  reference");
                obj2.AddOption("Mode", "Reference");
            }
            else
            {
                obj2.SetCommandPrompt("LinearDimension to copy");
                obj2.AddOption("Mode", "Copy");
            }
            obj2.GeometryFilter = ObjectType.Annotation;
            GetResult multiple = obj2.GetMultiple(1, 0);
            if (multiple == GetResult.Option)
            {
                m_reference = !m_reference;
                goto Label_0000;
            }
            if (multiple != GetResult.Object)
            {
                return null;
            }
            List<GH_LinearDimension> list2 = new List<GH_LinearDimension>();
         
            for (int i = 0; i < obj2.ObjectCount; i++)
            {
                if (m_reference)
                {
                    GH_LinearDimension dm = new GH_LinearDimension(obj2.Object(i).ObjectId);
                    if ((dm != null) && (!dm.IsGeometryLoaded)) { dm.LoadGeometry(); }
                    list2.Add( dm);                 
                }
                else
                {
                    list2.Add(new GH_LinearDimension((LinearDimension)obj2.Object(i).Geometry()));
                }
            }
            return list2;
        }
    }

}
