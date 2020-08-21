using GH_IO.Serialization;
using Grasshopper;
using Grasshopper.Getters;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino;
using Rhino.Display;
using Rhino.DocObjects;
using Rhino.Geometry;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace GH_DataView_Component
{
   
    public class GH_LinearDimension : GH_GeometricGoo<LinearDimension>, IGH_BakeAwareData, IGH_PreviewData
    {
        private System.Guid m_ref_guid;
        public Line L_;
        public Text3d Text_;
        public void initGeo(LinearDimension ld)
        {
            Point3d pa = new Point3d(ld.Arrowhead1End.X, ld.Arrowhead1End.Y, 0);
            Point3d pb = new Point3d(ld.Arrowhead2End.X, ld.Arrowhead2End.Y, 0);
            Point3d pt = (pa+pb)/2;
            pa.Transform(Rhino.Geometry.Transform.PlaneToPlane(Plane.WorldXY, ld.Plane));
            pb.Transform(Rhino.Geometry.Transform.PlaneToPlane(Plane.WorldXY, ld.Plane));
            pt.Transform(Rhino.Geometry.Transform.PlaneToPlane(Plane.WorldXY, ld.Plane));
            L_ = new Line(pa, pb);
            Text_ = new Text3d(ld.Text,new Plane(pt, ld.Plane.XAxis,ld.Plane.YAxis),pa.DistanceTo(pb)/10);        
        }
       public GH_LinearDimension()
        {
            this.m_ref_guid = Guid.Empty;
        }
       public GH_LinearDimension(GH_LinearDimension other)
        {
            this.m_ref_guid = Guid.Empty;
            if (other.m_value != null)
            {
                base.m_value = (LinearDimension)other.m_value.Duplicate();            
            }
            this.m_ref_guid = other.m_ref_guid;          
        }
       public GH_LinearDimension(LinearDimension dimension) : base(dimension)
        {
            this.m_ref_guid = Guid.Empty;
        }
       public GH_LinearDimension(Guid id)
        {
            this.m_ref_guid = Guid.Empty;
            this.m_ref_guid = id;
        }
       public GH_LinearDimension DuplicateLinearDimension()
        {
            return new GH_LinearDimension(this);
        }
        public override string ToString()
        {
            return this.Value.ToString();
        }
        public override bool Write(GH_IO.Serialization.GH_IWriter writer)
        {
            writer.SetGuid("RefLinearDimensionID", this.m_ref_guid);

            double[] eq = this.Value.Plane.GetPlaneEquation();
            double[] data ={eq[0],eq[1],eq[2],eq[3],
this.Value.ExtensionLine1End.X,this.Value.ExtensionLine1End.Y,
this.Value.ExtensionLine2End.X,this.Value.ExtensionLine2End.Y,
this.Value.TextPosition.X,this.Value.TextPosition.Y,};
            writer.SetDoubleArray("GH_LinearDimension_data", data);
            return true;
        }
        public override bool Read(GH_IO.Serialization.GH_IReader reader)
        {
            this.m_ref_guid = System.Guid.Empty;
            this.m_ref_guid = reader.GetGuid("RefLinearDimensionID");

            double[] data = reader.GetDoubleArray("GH_LinearDimension_data");
            this.Value = new LinearDimension(new Plane(data[0], data[1], data[2], data[3]),
                new Point2d(data[4], data[5]),
                 new Point2d(data[6], data[7]),
                  new Point2d(data[8], data[9]));
            return true;
        }
        public override IGH_GeometricGoo DuplicateGeometry()
        {
            return this.DuplicateLinearDimension();
        }
        public override BoundingBox GetBoundingBox(Transform xform)
        {
            if (base.m_value == null)
            {
                return BoundingBox.Empty;
            }
            return base.m_value.GetBoundingBox(xform);
        }
        public override IGH_GeometricGoo Morph(SpaceMorph xmorph)
        {
            if (!this.IsValid)
            {
                return null;
            }
            xmorph.Morph(base.m_value);
            this.ReferenceID = Guid.Empty;
            return this;
        }
        public override IGH_GeometricGoo Transform(Transform xform)
        {
            if (!this.IsValid)
            {
                return null;
            }
            base.m_value.Transform(xform);
            this.ReferenceID = Guid.Empty;
            return this;
        }      
        public override void ClearCaches()
        {
            if (this.IsReferencedGeometry)
            {
                base.m_value = null;
            }
        }
        public override bool LoadGeometry(RhinoDoc doc)
        {
            RhinoObject obj2 = doc.Objects.Find((Guid)this.ReferenceID);
            if (obj2 == null)
            {
                return false;
            }
            if (!(obj2.Geometry is LinearDimension))
            {
                return false;
            }
            base.m_value = (LinearDimension)obj2.Geometry;
            if (base.m_value != null)
            {
                base.m_value = (LinearDimension)base.m_value.Duplicate();
            }
            return true;
        }
        public override object ScriptVariable()
        {
            if (base.m_value == null)
            {
                return null;
            }
            return base.m_value.DuplicateShallow();
        }
        public override IGH_GooProxy EmitProxy()
        {
            return new GH_LinearDimensionProxy(this);
        }
        public override bool CastTo<T>(out T target)
        {
            if ((base.m_value != null) && typeof(T).IsAssignableFrom(GH_TypeLib2.t_rc_lineardimension))
            {
                object obj2 = base.m_value.DuplicateShallow();
                target = (T)obj2;
                return true;
            }
            target = default(T);
            return false;
        }
        public override bool CastFrom(object source)
        {
            GH_LinearDimension target = this;

            return Convert2.ToGHLinearDimension(RuntimeHelpers.GetObjectValue(source), ref target);
                 
        }     
        //*************************************
        public override bool IsValid
        {
            get
            {
                if (base.m_value == null)
                {
                    return false;
                }
                return base.m_value.IsValid;
               
            }
        }
        public override string IsValidWhyNot
        {
            get
            {
                return "I don't know what this method means";
            }
        }
        public override string TypeName
        {
            get { return "LinearDimension"; }
        }
        public override string TypeDescription
        {
            get { return "A LinearDimension Value "; }
        }
        public override bool IsGeometryLoaded
        {
            get
            {
                return (base.m_value != null);
            }
        }
        public override BoundingBox Boundingbox
        {
            get { return this.Value.GetBoundingBox(true); }
        }
        public override LinearDimension Value
        {
            get { return base.Value; }
            set { base.Value = value;  }
        }
        public override System.Guid ReferenceID
        {
            get
            {
                return this.m_ref_guid;
            }
            set
            {
               this.m_ref_guid = value;
            }
        }
        ////////////////////
    public class GH_LinearDimensionProxy : GH_GooProxy<GH_LinearDimension>
    {
        // Methods
        public GH_LinearDimensionProxy(GH_LinearDimension owner) : base(owner) { }
        public override void Construct()
        {
            try
            {
               Grasshopper.Instances.DocumentEditorFadeOut();
               GH_LinearDimension dimension = GH_DimensionGetter.GetLinearDimension2();
               if (dimension != null)
                {
                    this.Owner.m_value = dimension.m_value;
                    this.Owner.m_ref_guid = dimension.m_ref_guid;
                    this.Owner.LoadGeometry();
                }
            }
            finally
            {
                Grasshopper.Instances.DocumentEditorFadeIn();
            }
        }
      }
        ///////////////           
    public BoundingBox ClippingBox
    {
        get
        {
            return this.Boundingbox;
        }
    }
    public void DrawViewportMeshes(GH_PreviewMeshArgs args)
    {
      //////
    }
    public void DrawViewportWires(GH_PreviewWireArgs args)
    {
            if(this.Value!= null) {
            initGeo(Value);
            args.Pipeline.Draw3dText(this.Text_, Color.MediumPurple);
            args.Pipeline.DrawLine(this.L_, Color.MediumPurple, args.Thickness);
            }
        }
    public bool BakeGeometry(RhinoDoc doc, Rhino.DocObjects.ObjectAttributes att, out System.Guid obj_guid)
    {
        if (!this.IsValid)
        {
            obj_guid = System.Guid.Empty;
            return false;
        }
        obj_guid = (System.Guid)doc.Objects.AddLinearDimension(this.Value, att);
        return true;
    }
///////////////
    }
}