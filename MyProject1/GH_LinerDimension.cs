using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using Rhino;
using System.ComponentModel;
namespace GHComponent1
{
    public class GH_LinearDimension : GH_GeometricGoo<LinearDimension>
    {
        private System.Guid m_ref_guid;
        public GH_LinearDimension()
        {
            this.m_ref_guid = System.Guid.Empty;
        }
        public GH_LinearDimension(LinearDimension Dimension)
        {
            this.m_ref_guid = System.Guid.Empty;
            this.Value = (LinearDimension)Dimension.Duplicate();
        }
        public GH_LinearDimension(GH_LinearDimension gh_LinearDimension)
        {
            LinearDimension Dimension = gh_LinearDimension.Value;
            this.Value = (LinearDimension)Dimension.Duplicate();
            this.m_ref_guid = System.Guid.Empty;
            this.m_ref_guid = gh_LinearDimension.m_ref_guid;
        }
        public GH_LinearDimension(System.Guid ref_guid)
        {
            this.m_ref_guid = System.Guid.Empty;
            this.m_ref_guid = ref_guid;
        }
        public override string ToString()
        {
            return this.Value.ToString();
        }
        public override bool Write(GH_IO.Serialization.GH_IWriter writer)
        {
            writer.SetGuid("RefID", this.m_ref_guid);

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
            this.m_ref_guid = reader.GetGuid("RefID");

            double[] data = reader.GetDoubleArray("GH_LinearDimension_data");
            this.Value = new LinearDimension(new Plane(data[0], data[1], data[2], data[3]),
                new Point2d(data[4], data[5]),
                 new Point2d(data[6], data[7]),
                  new Point2d(data[8], data[9]));
            return true;
        }
        public override bool CastTo<Q>(ref Q target)
        {
            if (this.Value == null) return false;
            //First, see if Q is similar to the Integer primitive.
            if (typeof(Q).IsAssignableFrom(typeof(LinearDimension)))
            {
                object ptr = this.Value;
                target = (Q)ptr;
                return true;
            }
            //Then, see if Q is similar to the GH_Integer type.
            if (typeof(Q).IsAssignableFrom(typeof(GH_LinearDimension)))
            {
                object ptr = new GH_LinearDimension(this.Value);
                target = (Q)ptr;
                return true;
            }
            return false;
        }
        public override bool CastFrom(object source)
        {
            if (source == null) { return false; }
            if (source is LinearDimension)
            {
                LinearDimension dm = (LinearDimension)source;
                this.Value = dm;
                return true;
            }

            if (source is GH_LinearDimension)
            {
                GH_LinearDimension dm2 = (GH_LinearDimension)source;
                this.Value = dm2.Value;
                return true;
            }
            return false;
        }
        public override IGH_GeometricGoo DuplicateGeometry()
        {
            return new GH_LinearDimension(this.Value);
        }
        public override IGH_Goo Duplicate()
        {
            return new GH_LinearDimension(this.Value);
        }
        public override BoundingBox GetBoundingBox(Transform xform)
        {
            LinearDimension dm = (LinearDimension)this.Value.Duplicate();
            dm.Transform(xform);
            BoundingBox box = dm.GetBoundingBox(true);
            box.MakeValid();
            return box;
        }
        public override IGH_GeometricGoo Morph(SpaceMorph xmorph)
        {
            if (!this.IsValid)
            {
                return null;
            }
            xmorph.Morph(base.m_value);
            this.ReferenceID = System.Guid.Empty;
            return this;
        }
        public override IGH_GeometricGoo Transform(Transform xform)
        {
            if (!this.IsValid)
            {
                return null;
            }
            this.Value.Transform(xform);
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
            Rhino.DocObjects.RhinoObject obj2 = doc.Objects.Find(this.ReferenceID);
            if (obj2 != null)
            {
                if (obj2.Geometry is LinearDimension)
                {
                    LinearDimension dm = (LinearDimension)(obj2.Geometry);        
                        base.m_value = (LinearDimension)dm.Duplicate();
                    return true;
                }
            }
            return false;
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
                return this.Value.Text.ToString() + "/" + this.Value.NumericValue.ToString();
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
            set { base.Value = value; }
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
               GH_LinearDimension curve = GH_DimensionGetter.GetLinearDimension2();
                if (curve != null)
                {
                    this.Owner.m_value = curve.m_value;
                    this.Owner.m_ref_guid = curve.m_ref_guid;
                    this.Owner.LoadGeometry();
                }
            }
            finally
            {
                Grasshopper.Instances.DocumentEditorFadeIn();
            }
        }
        [Category("Reference"), RefreshProperties(RefreshProperties.All), Description("Object ID of referenced LinearDimension"), DisplayName("Object ID")]
        public string ObjectID
        {
            get
            {
                if (this.Owner.IsReferencedGeometry)
                {
                    return string.Format("{0}", this.Owner.ReferenceID);
                }
                return "none";
            }
            set
            {
                if (this.Owner.IsReferencedGeometry)
                {               
                        System.Guid guid = new System.Guid(value);
                        this.Owner.ReferenceID = guid;
                        this.Owner.ClearCaches();
                        this.Owner.LoadGeometry();    
                }
            }
        }   
    }                   
}
}