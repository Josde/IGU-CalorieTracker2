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
using System.Windows.Shapes;
using IGU_CalorieTracker2.MVVM.ViewModel;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace IGU_CalorieTracker2.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddDataView.xaml
    /// </summary>
    public partial class AddDataView : Window
    {
        AddDataViewModel VM;
        public AddDataView()
        {
            InitializeComponent();
            eleccionFecha.DisplayDateEnd = DateTime.Today;
            VM = new AddDataViewModel();
        }

        private void BotonAccept_Click(object sender, RoutedEventArgs e)
        {
            String textoDesayuno, textoComida, textoCena, textoSnacks;
            textoDesayuno = desayunoBox.Text.Trim();
            textoComida = comidaBox.Text.Trim();
            textoCena = cenaBox.Text.Trim();
            textoSnacks = snacksBox.Text.Trim();
            DateTime? date = eleccionFecha.SelectedDate;
            if (VM.Accept(textoDesayuno, textoComida, textoCena, textoSnacks, date)) {
                // Si la validacion ha sido correcta y se han guardado los datos en el modelo, reseteamos el estado de la ventana.
                // Hacemos esto por si la volvemos a abrir. Después, la escondemos como hacemos para cerrarla y redibujamos la gráfica.
                datosIncorrectosBlock.Visibility = Visibility.Hidden;
                eleccionFecha.Text = "";
                desayunoBox.Text = "";
                comidaBox.Text = "";
                cenaBox.Text = "";
                snacksBox.Text = "";
                MainViewModel.RedrawCommand?.Execute(null);
                Hide();
            } else
            {
                datosIncorrectosBlock.Visibility = Visibility.Visible;
            }
        }
        private void BotonCancel_Click(object sender, RoutedEventArgs e)
        {
            // Usamos Hide() para no perder los datos si volvemos a abrir la ventana.
            Hide();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = sender as TextBox;
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            // Hacemos override de este evento para permitir que podamos mover la ventana con el raton.
            // Es necesario porque usamos WindowStyle = None para que no haya barra de titulo, que es por donde moveriamos la ventana normalmente.
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
