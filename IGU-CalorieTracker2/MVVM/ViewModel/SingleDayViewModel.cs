using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using IGU_CalorieTracker2.Utils;
using System.Diagnostics;

namespace IGU_CalorieTracker2.MVVM.ViewModel
{
    public class SingleDayViewModel
    {
        public static void Draw(Canvas graphCanvas)
        {
            if (Model.Instance.diaSeleccionado == null) return;
            Debug.Write("Llamado Draw");
            Dia d = Model.Instance.diaSeleccionado;
            double xMin, xMax, xDesayuno, xComida, xCena, xSnacks;
            double yMin, yMax, yDesayuno, yComida, yCena, ySnacks;
            double margin = 15;
            double caloriasMaximas = new[] { d.caloriasDesayuno, d.caloriasComida, d.caloriasCena, d.caloriasSnacks }.Max();
            xMax = graphCanvas.ActualWidth;
            yMax = graphCanvas.ActualHeight;
            double ratio = yMax / caloriasMaximas;

            yDesayuno = d.caloriasDesayuno * ratio - margin;
            yComida = d.caloriasComida * ratio - margin;
            yCena = d.caloriasCena * ratio - margin;
            ySnacks = d.caloriasSnacks * ratio - margin;
            xDesayuno = xMax / 8;
            xComida = 3 * xMax / 8;
            xCena = 5*xMax / 8;
            xSnacks = 7*xMax / 8;

            Polygon pDesayuno, pComida, pCena, pSnacks;
            TextBlock tDesayuno, tComida, tCena, tSnacks;
            pDesayuno = Utils.Drawing.getBar(new Point(xDesayuno, 0), 30, yDesayuno, Brushes.Red);
            pComida = Utils.Drawing.getBar(new Point(xComida, 0), 30, yComida, Brushes.Blue);
            pCena = Utils.Drawing.getBar(new Point(xCena, 0), 30, yCena, Brushes.Green);
            pSnacks = Utils.Drawing.getBar(new Point(xSnacks, 0), 30, ySnacks, Brushes.Black);

            tDesayuno = Utils.Drawing.getText(new Point(xDesayuno, yDesayuno), d.caloriasDesayuno.ToString(), 14, Brushes.Black);
            tComida = Utils.Drawing.getText(new Point(xComida, yComida), d.caloriasComida.ToString(), 14, Brushes.Black);
            tCena = Utils.Drawing.getText(new Point(xCena, yCena), d.caloriasCena.ToString(), 14, Brushes.Black);
            tSnacks = Utils.Drawing.getText(new Point(xSnacks, ySnacks), d.caloriasSnacks.ToString(), 14, Brushes.Black);

            graphCanvas.Children.Add(pDesayuno);
            graphCanvas.Children.Add(pComida);
            graphCanvas.Children.Add(pCena);
            graphCanvas.Children.Add(pSnacks);

            graphCanvas.Children.Add(tDesayuno);
            graphCanvas.Children.Add(tComida);
            graphCanvas.Children.Add(tCena);
            graphCanvas.Children.Add(tSnacks);
        }
    }
}
