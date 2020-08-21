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
namespace GH_DataView_Component
{
    public sealed class Convert2
    {
        public static bool ToGHLinearDimension(object data, ref GH_LinearDimension target)
        {
            if (!ToGHLinearDimension_Primary(RuntimeHelpers.GetObjectValue(data), ref target))
            {
                return ToGHLinearDimension_Secondary(RuntimeHelpers.GetObjectValue(data), ref target);
            }
            return true;
        }
        public static bool ToGHLinearDimension_Primary(object data, ref GH_LinearDimension target)
        {
            if (data == null)
            {
                return false;
            }
            Guid gUID = data.GetType().GUID;
            if (gUID == GH_TypeLib2.id_rc_lineardimension)
            {
                if (target == null)
                {
                    target = new GH_LinearDimension((LinearDimension)data);
                }
                else
                {
                    target.Value = (LinearDimension)data;
                }
                return true;
            }
            if (!(gUID == GH_TypeLib2.id_gh_lineardimension))
            {
                return false;
            }
            if (target == null)
            {
                target = (GH_LinearDimension)data;
            }
            else
            {
                target.Value = ((GH_LinearDimension)data).Value;
                target.ReferenceID = ((GH_LinearDimension)data).ReferenceID;
            }
            return true;
        }
        public static bool ToGHLinearDimension_Secondary(object data, ref GH_LinearDimension target)
        {
            Guid guid = Guid.Empty;
            if (data == null)
            {
                return false;
            }
            if (GH_Convert.ToGUID_Primary(RuntimeHelpers.GetObjectValue(data), ref guid))
            {
                if (target == null)
                {
                    target = new GH_LinearDimension(guid);
                }
                else
                {
                    target.ReferenceID = guid;
                }
                target.ClearCaches();
                target.LoadGeometry();
                return target.IsValid;
            }
            string destination = null;
            if (GH_Convert.ToString_Primary(RuntimeHelpers.GetObjectValue(data), ref destination))
            {
                RhinoObject obj2 = GH_Convert.FindRhinoObjectByNameAndType(destination, ObjectType.Annotation);
                if (obj2 != null)
                {
                    if (target == null)
                    {
                        target = new GH_LinearDimension();
                    }
                    target.ReferenceID = (Guid)obj2.Id;
                    target.ClearCaches();
                    target.LoadGeometry();
                    return target.IsValid;
                }
            }
            LinearDimension rc = null;
            if (!ToLinearDimension_Secondary(RuntimeHelpers.GetObjectValue(data), ref rc))
            {
                return false;
            }
            if (target == null)
            {
                target = new GH_LinearDimension(rc);
            }
            else
            {
                target.Value = rc;
                target.ReferenceID = Guid.Empty;
            }
            return true;
        }
        public static bool ToLinearDimension_Secondary(object data, ref LinearDimension rc)
        {
            if (data != null)
            {
                Guid gUID = data.GetType().GUID;
                Guid guid3 = gUID;
                if ((guid3 == GH_TypeLib.id_guid) || (guid3 == GH_TypeLib.id_gh_guid))
                {
                    Guid guid2;
                    if (gUID == GH_TypeLib.id_guid)
                    {
                        guid2 = (Guid)data;
                    }
                    else
                    {
                        guid2 = ((GH_Guid)data).Value;
                    }
                    Rhino.DocObjects.ObjRef refer = new ObjRef((Guid)guid2);
                    LinearDimension dimension = (LinearDimension)refer.Geometry();
                    if (dimension != null)
                    {
                        rc = (LinearDimension)dimension.Duplicate();
                        return true;
                    }
                }
                return false;
            }
            return false;
        }
    }
}