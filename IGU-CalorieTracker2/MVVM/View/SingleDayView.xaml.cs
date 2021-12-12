using IGU_CalorieTracker2.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IGU_CalorieTracker2.Utils;
namespace IGU_CalorieTracker2.MVVM.View
{
    /// <summary>
    /// Interaction logic for SingleDayView.xaml
    /// </summary>
    public partial class SingleDayView : UserControl
    {
       public static RelayCommand DrawCommand;
       public SingleDayView()
        {
            InitializeComponent();
            DrawCommand = new RelayCommand(o =>
            {
                this.Draw();
            });
        }

        public void Draw()
        {
            List<UIElement> toRemove = new List<UIElement>();
            // Limpiamos el canvas excepto los elementos estaticos de la vista (los textblocks de desayuno, comida, cena, el dia...)
            foreach (FrameworkElement obj in GraphCanvas.Children)
            {
                String name = obj.Name;
                switch(name)
                {
                    case "GridTexto":
                    case "diaBlock":
                    case "desayunoBlock":
                    case "comidaBlock":
                    case "cenaBlock":
                    case "snacksBlock":
                        break;
                    default:
                        toRemove.Add(obj);
                        break;
                }
            }
            // Dos bucles for para evitar cambiar la coleccion mientras iteramos sobre ella, ya que esto causa una excepción.
            foreach (UIElement obj in toRemove)
            {
                GraphCanvas.Children.Remove(obj);
            }
            diaBlock.Text = Model.Instance.diaSeleccionado.Fecha.ToShortDateString();
            // Finalmente, redibujamos la gráfica.
            SingleDayViewModel.Draw(GraphCanvas);
        }



        
    }
}
