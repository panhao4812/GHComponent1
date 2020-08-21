using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using System.Drawing;
using Grasshopper.GUI.Canvas;
using System.Windows.Forms;
using Grasshopper.GUI;

namespace GH_DataView_Component
{
    public class DataViewAttributes : GH_ComponentAttributes
    {
        public float CellWidth;
        public float CellHeight;
        public int table_Width;
        public int table_Height;
        public DataView parent;
        SolidBrush brush3 = new SolidBrush(Color.FromArgb(90, Color.Black));
        Pen pen3 = new Pen(Color.FromArgb(30, Color.Black));
        StringFormat Fontformat = StringFormat.GenericDefault;
        Color BackGroundColor = Color.WhiteSmoke;
        public override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            if (!parent._edit) return GH_ObjectResponse.Ignore;
            if (e.Button != MouseButtons.Left)
            {
                return GH_ObjectResponse.Ignore;
            }
            if (this.parent.isEdit)
            {
                return GH_ObjectResponse.Ignore;
            }
            this.parent.ShowEditDialog();
            return GH_ObjectResponse.Handled;
        }
        private static Font titleFont;
        static DataViewAttributes()
        {
            titleFont = GH_FontServer.NewFont(FontFamily.GenericSerif, 10f, FontStyle.Italic | FontStyle.Bold);

        }
        public DataViewAttributes(DataView owner) : base(owner)
        {
            this.parent = owner;
            titleFont = GH_FontServer.NewFont(FontFamily.GenericSerif, 10f, FontStyle.Italic | FontStyle.Bold);
            CellWidth = parent.CellWidth;
            CellHeight = parent.CellHeight;
            table_Width = parent.table_Width;
            table_Height = parent.table_Height;
            Fontformat.Alignment = StringAlignment.Center;
        }

        public override void ExpireLayout()
        {
            base.ExpireLayout();
            //手动释放layout 和layout写在一起
        }
        protected override void Layout()
        {
            //初始化 因为LayoutBounds会延长bounds 所以绘制表格需要重算。
            this.m_innerBounds = new RectangleF(this.Pivot,
                 new SizeF(parent.table_Width * parent.CellWidth, parent.table_Height * parent.CellHeight));
            if (this.parent.Collapse)
            {
                m_innerBounds.Width = 100; m_innerBounds.Height = 60;
            }
            LayoutInputParams(this.Owner, this.m_innerBounds);
            LayoutOutputParams(this.Owner, this.m_innerBounds);
            this.Bounds = LayoutBounds(this.Owner, this.m_innerBounds);
            this.table_Width = parent.table_Width;
            this.table_Height = parent.table_Height;
            this.CellWidth = this.Bounds.Width / table_Width;
            this.CellHeight = this.Bounds.Height / table_Height;
        }
        //决定电池显示内容的。可以在这里修改决定是否显示物体
        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {  //graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            ////////////////////////////////////////////////////////////////////////////////////
            if (channel == GH_CanvasChannel.Wires)
            {
                base.Render(canvas, graphics, channel);
                // 显示连线的，可以修改,默认设置如上
            }
            if (channel == GH_CanvasChannel.Objects)
            {
                int apha = parent.ForeColor.A - 60;
                if (apha < 10) apha = 10;
                pen3.Color = Color.FromArgb(apha, parent.ForeColor);
                //修改显示电池主体的代码
                switch (parent.RuntimeMessageLevel)
                {
                    case GH_RuntimeMessageLevel.Warning:
                        BackGroundColor = Color.Orange;
                        break;
                    case GH_RuntimeMessageLevel.Error:
                        BackGroundColor = Color.Red;
                        break;
                    case GH_RuntimeMessageLevel.Blank:
                        BackGroundColor = parent.BackGroundColor;
                        break;
                }
                ///////////////////////////////////////////////////////////
                GH_Capsule capsule = GH_Capsule.CreateCapsule(this.Bounds, GH_Palette.Normal, 3, 20);
                foreach (IGH_Param param in this.parent.Params.Input)
                {
                    capsule.AddInputGrip(param.Attributes.InputGrip.Y);
                }
                foreach (IGH_Param param in this.parent.Params.Output)
                {
                    capsule.AddOutputGrip(param.Attributes.OutputGrip.Y);
                }
                capsule.RenderEngine.RenderGrips_Alternative(graphics);
                capsule.RenderEngine.RenderBackground_Alternative(graphics, BackGroundColor, false);
                ////////////////////////////////////////////////////////////////////
                if ( parent.Collapse )
                {
                    string str =this.parent.NickName;
                    graphics.DrawString(str, titleFont, brush3, this.Bounds, Fontformat);
                }
                else if (parent.table0 != null && parent.Collapse == false)
                {
                    for (int i = 1; i < table_Width; i++)
                    {
                        graphics.DrawLine(pen3, this.Bounds.Left + CellWidth * i, this.Bounds.Top, this.Bounds.Left + CellWidth * i, this.Bounds.Bottom);
                    }
                    for (int i = 1; i < table_Height; i++)
                    {
                        graphics.DrawLine(pen3, this.Bounds.Left, this.Bounds.Top + CellHeight * i, this.Bounds.Right, this.Bounds.Top + CellHeight * i);
                    }

                    for (int i = 0; i < table_Height; i++)
                    {
                        for (int j = 0; j < table_Width; j++)
                        {
                            string str = parent.table0[j, i];
                            RectangleF FontBox = new RectangleF(j * CellWidth + this.Bounds.Location.X,
                                i * CellHeight + this.Bounds.Location.Y, CellWidth, CellHeight);
                            graphics.DrawString(str, titleFont, brush3, FontBox, Fontformat);
                        }
                    }
                }
                //////////////////////////////////////////////////////////////////////////////////
                capsule.RenderEngine.RenderOutlines(graphics, canvas.Viewport.Zoom, GH_Skin.palette_hidden_standard);
                capsule.Dispose();
            }
        }
    }
}