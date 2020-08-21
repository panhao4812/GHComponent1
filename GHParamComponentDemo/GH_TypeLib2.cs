using GH_IO.Serialization;
using Grasshopper;
using Grasshopper.Getters;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino;
using Rhino.DocObjects;
using Rhino.Geometry;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace GHComponent1
{
    public sealed class GH_TypeLib2
    {
        public static Guid id_gh_lineardimension;
        public static Guid id_rc_lineardimension;
        public static Type t_gh_lineardimension;
        public static Type t_rc_lineardimension;
        public GH_TypeLib2()
        {
            t_gh_lineardimension = typeof(GH_LinearDimension);
            t_rc_lineardimension = typeof(LinearDimension);
            id_rc_lineardimension = t_rc_lineardimension.GUID;
            id_gh_lineardimension = t_gh_lineardimension.GUID;
        }
    }
}