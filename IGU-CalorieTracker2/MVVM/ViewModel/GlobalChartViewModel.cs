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
    public class GlobalChartViewModel
    {
        public static void Draw(Canvas graphCanvas)
        {
            if (Model.Instance.Dias.Count == 0)
            {
                // Si no hay datos para dibujar pero se ha forzado el comando, ponemos un texto informativo y paramos la función.
                TextBlock tb = new TextBlock();
                tb.Text = "No hay datos para dibujar.";
                tb.Foreground = Brushes.White;
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.VerticalAlignment = VerticalAlignment.Center;
                graphCanvas.Children.Add(tb);
                return;
            }
            Debug.Write("Llamado Draw graficaGlobal");
            double xMax = graphCanvas.ActualWidth;
            double yMax = graphCanvas.ActualHeight;
            int contador = 0;
            // xStep es la distancia entre elementos de la gráfica.
            double xStep = xMax / Model.Instance.Dias.Count;
            double margin = 12.5;
            double width = xStep / 2;
            double maxCalorias = 0;
            // Obtenemos el día con las calorías totales máximas, que será el que más se acerque a la parte superior de la gráfica, y el que usaremos para escalar el resto.
            foreach (Dia d in Model.Instance.Dias)
            {
                if (d.caloriasTotales > maxCalorias)
                {
                    maxCalorias = d.caloriasTotales;
                }
            }
            double ratio = yMax / maxCalorias;
            foreach (Dia d in Model.Instance.Dias)
            {
                double xDesayuno, xComida, xCena, xSnacks;
                double yDesayuno, yComida, yCena, ySnacks;
                double caloriasMaximas = new[] { d.caloriasDesayuno, d.caloriasComida, d.caloriasCena, d.caloriasSnacks }.Max();
                
                double xOrigen = margin + contador * xStep;
                String dateString = d.Fecha.Day + "/" + d.Fecha.Month;
                // Usando geometría y el ratio calculado previamente, generamos las barras apiladas de cada día.
                // Cada día usa 4 polígonos, cada uno empezando donde acaba verticalmente el anterior y representando una comida distinta.
                yDesayuno = d.caloriasDesayuno * ratio;
                yComida = d.caloriasComida * ratio;
                yCena = d.caloriasCena * ratio;
                ySnacks = d.caloriasSnacks * ratio - margin;

                Polygon pDesayuno, pComida, pCena, pSnacks; 
                pDesayuno = Utils.Drawing.getBar(new Point(xOrigen, 0), width, yDesayuno, Brushes.Red);
                pComida = Utils.Drawing.getBar(new Point(xOrigen, yDesayuno), width, yComida, Brushes.Blue);
                pCena = Utils.Drawing.getBar(new Point(xOrigen, yDesayuno + yComida), width, yCena, Brushes.Green);
                pSnacks = Utils.Drawing.getBar(new Point(xOrigen, yDesayuno + yComida + yCena), width, ySnacks, Brushes.Black);
                TextBlock tDia = Utils.Drawing.getText(new Point(xOrigen - 5, yDesayuno + yComida + yCena + ySnacks + margin), dateString, 8, Brushes.Black);
                // Finalmente, añadimos los datos al gráfico.
                graphCanvas.Children.Add(pDesayuno);
                graphCanvas.Children.Add(pComida);
                graphCanvas.Children.Add(pCena);
                graphCanvas.Children.Add(pSnacks);
                graphCanvas.Children.Add(tDia);
                contador++;
            }
            
        }
    }
}
