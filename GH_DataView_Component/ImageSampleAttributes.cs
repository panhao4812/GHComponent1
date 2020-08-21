using System;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;

namespace GH_DataView_Component
{
    public class ImageSampleAttributes : GH_ComponentAttributes
    {     
        public ImageSampleAttributes(ImageSample owner)
            : base(owner)
        {
        }
        
        public override void ExpireLayout()
        {
            base.ExpireLayout();
            //手动释放layout 和layout写在一起
        }
        protected override void Layout()
        {
            //初始化
            this.m_innerBounds = new RectangleF(this.Pivot, new SizeF(100f, 100f));
            LayoutInputParams(this.Owner, this.m_innerBounds);
            LayoutOutputParams(this.Owner, this.m_innerBounds);
            this.Bounds = LayoutBounds(this.Owner, this.m_innerBounds);
        }
        
        //决定电池显示内容的。可以在这里修改决定是否显示物体
        protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        {      
            if (channel == GH_CanvasChannel.Wires)
            {        
                base.Render(canvas, graphics, channel);
                // 显示连线的，可以修改,默认设置如上
            }
            if (channel == GH_CanvasChannel.Objects)
            {              
                //修改显示电池主体的代码
                SolidBrush brush=new SolidBrush(Color.White);
                SolidBrush brush2 = new SolidBrush(Color.White);
                Pen pen = new Pen(Color.Black); Pen pen2 = new Pen(Color.Black); 
                pen.Width = 2;
                switch (Owner.RuntimeMessageLevel)
                {
                    case GH_RuntimeMessageLevel.Warning:
                        brush2.Color = Color.Orange;
                        break;
                    case GH_RuntimeMessageLevel.Error:
                        brush2.Color = Color.Red;
                        break;
                    case GH_RuntimeMessageLevel.Blank:
                        brush2.Color = Color.White;
                        break;
                }
                 //graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                GH_Structure<GH_String> volatileData = (GH_Structure<GH_String>)base.Owner.Params.Input[0].VolatileData;
                foreach (IGH_Param param in this.Owner.Params.Input)
                {
                    graphics.FillEllipse(brush, param.Attributes.InputGrip.X - 6.2f, param.Attributes.InputGrip.Y - 4f, 8f, 8f);
                    graphics.DrawEllipse(pen, param.Attributes.InputGrip.X - 6.2f, param.Attributes.InputGrip.Y - 4f, 8f, 8f);            
                }
                if (!volatileData.IsEmpty)
                {
                    Bitmap bitmap;
                    string myPath = volatileData.get_DataItem(0).Value;
                    try
                    {
                        bitmap = new Bitmap(myPath);
                    }
                    catch
                    {
                        base.Owner.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Path must be a valid location");
                        brush2.Color = Color.Red;                             
                        graphics.FillRectangle(brush2, Rectangle.Round(this.Bounds));
                        graphics.DrawRectangle(pen2, Rectangle.Round(this.Bounds));     
                        return;
                    }
                    this.Bounds = new RectangleF(new PointF(this.Bounds.Location.X, this.Bounds.Location.Y), new SizeF(bitmap.Size.Width-1f, bitmap.Size.Height-1f));              
                    graphics.DrawImage(bitmap, new RectangleF(new PointF(this.Bounds.Location.X, this.Bounds.Location.Y), new SizeF(bitmap.Size.Width, bitmap.Size.Height)));
                    graphics.DrawRectangle(pen2, Rectangle.Round(this.Bounds));
                    bitmap.Dispose();
                }
                else
                {                
                    graphics.FillRectangle(brush2, Rectangle.Round(this.Bounds));
                    graphics.DrawRectangle(pen2, Rectangle.Round(this.Bounds));
                }
            }
        }
/////////////
    }
}