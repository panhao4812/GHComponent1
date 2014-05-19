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
   public class GH_Mesh2 : GH_GeometricGoo<Mesh>
    {
             private Guid m_ref_guid;        
        public GH_Mesh2()
        {
            this.m_ref_guid = Guid.Empty;
        }
        public GH_Mesh2(GH_Mesh mesh)
        {
            this.m_ref_guid = mesh.ReferenceID;
            this.m_value = mesh.Value;
        }   
        public GH_Mesh2(GH_Mesh2 other)
        {
            this.m_ref_guid = Guid.Empty;
            if (other.m_value != null)
            {
                base.m_value = other.m_value.DuplicateMesh();
            }
            this.m_ref_guid = other.m_ref_guid;
        }       
        public GH_Mesh2(Mesh mesh) : base(mesh)
        {
            this.m_ref_guid = Guid.Empty;
        }      
        public GH_Mesh2(Guid id)
        {
            this.m_ref_guid = Guid.Empty;
            this.m_ref_guid = id;
        }
        public GH_Mesh2 DuplicateMesh()
        {
            return new GH_Mesh2(this);
        }
        public override object ScriptVariable()
        {
            if (base.m_value == null)
            {
                return null;
            }
            return base.m_value.DuplicateShallow();
        }
        public override BoundingBox Boundingbox
        {
            get
            {
                if (base.m_value == null)
                {
                    return BoundingBox.Empty;
                }
                return base.m_value.GetBoundingBox(true);
            }
        }
        public override IGH_GeometricGoo DuplicateGeometry()
        {
            return this.DuplicateMesh();
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
            if (xform.SimilarityType == TransformSimilarityType.OrientationReversing)
            {
                base.m_value.Flip(true, true, false);
            }
            this.ReferenceID = Guid.Empty;
            return this;
        }
        public override string ToString()
        {
            return "Mesh";
        }
        public override string TypeDescription
        {
            get
            {
                return "3D Polygonal mesh";
            }
        }
        public override string TypeName
        {
            get
            {
                return "Mesh";
            }
        }
        public override IGH_GooProxy EmitProxy()
        {
            return new GH_MeshProxy2(this);
        }
        public override bool LoadGeometry(RhinoDoc doc)
        {
            RhinoObject obj2 = doc.Objects.Find((Guid)this.ReferenceID);
            if (obj2 == null)
            {
                return false;
            }
            if (!(obj2.Geometry is Mesh))
            {
                return false;
            }
            base.m_value = (Mesh)obj2.Geometry;
            if (base.m_value != null)
            {
                base.m_value = base.m_value.DuplicateMesh();
            }
            return true;
        }
        public override bool Write(GH_IWriter writer)
        {
            writer.SetGuid("RefID2", (Guid)this.ReferenceID);
            if ((this.ReferenceID == Guid.Empty) && (base.m_value != null))
            {
                byte[] buffer = GH_Convert.CommonObjectToByteArray(base.m_value);
                if (buffer != null)
                {
                    writer.SetByteArray("ON_Data2", buffer);
                }
            }
            return true;
        }
        public override bool Read(GH_IReader reader)
        {
            this.ReferenceID = Guid.Empty;
            this.Value = null;
            this.ClearCaches();
            this.ReferenceID = (Guid)reader.GetGuid("RefID2");
            if (reader.ItemExists("ON_Data2"))
            {
                byte[] byteArray = reader.GetByteArray("ON_Data2");
                base.m_value = GH_Convert.ByteArrayToCommonObject<Mesh>(byteArray);
            }
            return true;
        }
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
                return GH_Format.FormatMeshValidity(base.m_value, "Mesh");
            }
        }
        public override Guid ReferenceID
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
        public override bool CastFrom(object source)
        {
            GH_Mesh target =new GH_Mesh( this.Value);
            target.ReferenceID = this.ReferenceID;
         bool bl= GH_Convert.ToGHMesh(RuntimeHelpers.GetObjectValue(source), GH_Conversion.Both, ref target);
         this.Value = target.Value;
         this.ReferenceID = target.ReferenceID;
         return bl;
        }
        public override bool CastTo<T>(out T target)
        {
            if ((base.m_value != null) && typeof(T).IsAssignableFrom(GH_TypeLib.t_rc_mesh))
            {
                object obj2 = base.m_value.DuplicateShallow();
                target = (T)obj2;
                return true;
            }
            target = default(T);
            return false;
        }
/// <summary>
/// /////
/// </summary>
        public class GH_MeshProxy2 : GH_GooProxy<GH_Mesh2>
        {
            public GH_MeshProxy2(GH_Mesh2 Value)
                : base(Value)
            {
            }

            public override void Construct()
            {
                try
                {
                    Instances.DocumentEditorFadeOut();
                    GH_Mesh mesh = GH_MeshGetter.GetMesh();                  
                    if (mesh != null)
                    {
                        this.Owner.m_value = mesh.Value;
                        this.Owner.m_ref_guid = mesh.ReferenceID;
                        this.Owner.LoadGeometry();
                    }
                }
                finally
                {
                    Instances.DocumentEditorFadeIn();
                }
            }
        }
///////
    }
}
