using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using Rhino;
namespace GHComponent1
{
    public class GH_LinearDimension : GH_GeometricGoo<LinearDimension>
    {

        private System.Guid m_ref_guid;

public GH_LinearDimension()
{
  this.Value = new LinearDimension();
}
public GH_LinearDimension(LinearDimension Dimension)
{
  // this.Value=new LinearDimension(Dimension.Plane, Dimension.ExtensionLine1End,Dimension.ExtensionLine2End, Dimension.TextPosition);
   this.Value = (LinearDimension)Dimension.Duplicate();
}
public GH_LinearDimension(GH_LinearDimension gh_LinearDimension)
{
    LinearDimension Dimension = gh_LinearDimension.Value;
    this.Value = (LinearDimension)Dimension.Duplicate();
}

public override LinearDimension Value
{
    get { return base.Value; }
    set{ base.Value = value;}
}
public override bool IsValid
{
    get { return true; }
}
public override string IsValidWhyNot
{
    get
    {
        return this.Value.Text.ToString ()+ "/" + this.Value.NumericValue.ToString(); 
    }
}
public override string TypeName
{
    get { return "GH_LinearDimension"; }
}
public override string TypeDescription
{
    get { return "A GH_LinearDimension Value "; }
}
public override string ToString()
{
    return this.Value.ToString();
}
public override bool Write(GH_IO.Serialization.GH_IWriter writer)
{
    double[] eq=this.Value.Plane.GetPlaneEquation();
    double[] data ={eq[0],eq[1],eq[2],eq[3],
this.Value.ExtensionLine1End.X,this.Value.ExtensionLine1End.Y,
this.Value.ExtensionLine2End.X,this.Value.ExtensionLine2End.Y,
this.Value.TextPosition.X,this.Value.TextPosition.Y,};
    writer.SetDoubleArray("GH_LinearDimension_data",data);
    return true;
}
public override bool Read(GH_IO.Serialization.GH_IReader reader)
{
    double[] data = reader.GetDoubleArray("GH_LinearDimension_data");
    this.Value=new LinearDimension(new Plane(data[0],data[1],data[2],data[3]),
        new Point2d(data[4],data[5]),
         new Point2d(data[6],data[7]),
          new Point2d(data[8],data[9]));
    return true;
}
public override bool CastTo<Q>(ref Q target)
{
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
    object ptr =new GH_LinearDimension(this.Value);
    target = (Q)ptr;
    return true;
  }
  //We could choose to also handle casts to Boolean, GH_Boolean, 
  //Double and GH_Number, but this is left as an exercise for the reader.
  return false;
}
public override bool CastFrom(object source)
{
  //Abort immediately on bogus data.
  if (source == null) { return false; }
  //Use the Grasshopper Integer converter. By specifying GH_Conversion.Both 
  //we will get both exact and fuzzy results. You should always try to use the
  //methods available through GH_Convert as they are extensive and consistent.
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
public override BoundingBox Boundingbox
{
    get { return this.Value.GetBoundingBox(true); }
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
    return null;
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
    if (obj2 == null)
    {
        return false;
    }
    if (obj2.Geometry.ObjectType != Rhino.DocObjects.ObjectType.Annotation)
    {
        return false;
    }
    base.m_value = (LinearDimension)obj2.Geometry;
    if (base.m_value != null)
    {
        base.m_value =(LinearDimension) base.m_value.Duplicate();
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

 


////////////////////
    }
}
