using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPGUI
{
    public class Placeholder
    {
        //Egy kis placeholder szórakozásképpen
        public static Image MakePlaceholderImage()
        {
            var bmp = new Bitmap(200, 200);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightGray);
                using (var pen = new Pen(Color.Gray))
                {
                    g.DrawRectangle(pen, 0, 0, bmp.Width - 1, bmp.Height - 1);
                    g.DrawLine(pen, 0, 0, bmp.Width - 1, bmp.Height - 1);
                    g.DrawLine(pen, bmp.Width - 1, 0, 0, bmp.Height - 1);
                }
                using (var f = new Font("Segoe UI", 10))
                using (var b = new SolidBrush(Color.DimGray))
                {
                    var text = "No image";
                    var size = g.MeasureString(text, f);
                    g.DrawString(text, f, b, (bmp.Width - size.Width) / 2, (bmp.Height - size.Height) / 2);
                }
            }
            return bmp;
        }
    }
}
