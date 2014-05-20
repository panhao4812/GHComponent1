using System;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;


namespace GHComponent1
{
    class Param_DimentionAttributes : GH_Attributes<Param_Dimention>
    {
        public Param_DimentionAttributes(Param_Dimention owner): base(owner)
        {}
        protected override void Layout()
        {
            int num = GH_FontServer.StringWidth(this.Owner.NickName, GH_FontServer.Standard);
            RectangleF ef2 = new RectangleF(this.Pivot.X, this.Pivot.Y, 60f, 30f);    
            this.Bounds = GH_Convert.ToRectangle(ef2);
        }
  protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
{
           if (channel == GH_CanvasChannel.Wires)
            {
                RenderIncomingWires(canvas.Painter, Owner.Sources, Owner.WireDisplay);
                return;
            }
            if (channel == GH_CanvasChannel.Objects)
            {              
                //修改显示电池主体的代码
                SolidBrush brush=new SolidBrush(Color.White);
                SolidBrush brush2 = new SolidBrush(Color.White);
                Pen pen = new Pen(Color.Black); 
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
                if (this.Owner.Locked) pen.Color = Color.Gray;
                else pen.Color = Color.Black;

                    graphics.FillEllipse(brush, this.Bounds.X - 4f, this.Bounds.Y+this.Bounds.Height/2 - 4f, 8f, 8f);
                    graphics.DrawEllipse(pen, this.Bounds.X - 4f, this.Bounds.Y+this.Bounds.Height/2 - 4f, 8f, 8f);      
                    graphics.FillEllipse(brush, this.Bounds.X+this.Bounds.Width- 4f, this.Bounds.Y+this.Bounds.Height/2 - 4f, 8f, 8f);
                    graphics.DrawEllipse(pen, this.Bounds.X+this.Bounds.Width- 4f, this.Bounds.Y+this.Bounds.Height/2 - 4f, 8f, 8f);        
                      
                    graphics.FillRectangle(brush2, Rectangle.Round(this.Bounds));
                    graphics.DrawRectangle(pen, Rectangle.Round(this.Bounds));
                }
}

 
    }
}

 



