using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace IGU_CalorieTracker2.Utils
{
    class Drawing
    {
        public static Polygon getBar(Point origin, double width, double height, Brush fill)
        {
            Polygon ret = new Polygon();
            PointCollection pc = new PointCollection();
            Point abajoIzquierda, abajoDerecha, arribaIzquierda, arribaDerecha;

            abajoIzquierda = new Point(origin.X - width / 2, origin.Y);
            abajoDerecha = new Point(origin.X + width / 2, origin.Y);
            arribaIzquierda = new Point(origin.X - width / 2, origin.Y + height);
            arribaDerecha = new Point(origin.X + width / 2, origin.Y + height);

            pc.Add(abajoIzquierda);
            pc.Add(abajoDerecha);
            pc.Add(arribaDerecha);
            pc.Add(arribaIzquierda);

            ret.Fill = fill;
            ret.Points = pc;

            return ret;
        }

        public static TextBlock getText(Point origin, String text, int fontSize, Brush color, bool flip = true)
        {
            TextBlock tb = new TextBlock();
            tb.Text = text;
            tb.FontSize = fontSize;
            tb.Foreground = color;
            tb.Margin = new Thickness(origin.X, origin.Y, 0, 0);
            if (flip) {
                tb.LayoutTransform = new ScaleTransform(1, -1);
            }
            return tb;
        }
    }
}
