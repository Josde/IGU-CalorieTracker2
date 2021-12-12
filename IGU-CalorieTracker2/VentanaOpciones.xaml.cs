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

namespace IGU_CalorieTracker2
{
    /// <summary>
    /// Interaction logic for VentanaOpciones.xaml
    /// </summary>
    public partial class VentanaOpciones : Window
    {
        public VentanaOpciones()
        {
            FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/Roboto/"), "./#Roboto");
            InitializeComponent();
            System.Reflection.PropertyInfo[] source = typeof(Colors).GetProperties();
            EleccionColoresBorde.ItemsSource = EleccionColoresEjes.ItemsSource = EleccionColoresRelleno.ItemsSource = source;
            ColorConverter cc = new ColorConverter();
            foreach (System.Reflection.PropertyInfo c in typeof(Colors).GetProperties())
            {
                Color current = (Color)c.GetValue(0);
                if (current == Model.Instance.Opciones.ColorStroke)
                {
                    EleccionColoresBorde.SelectedIndex = Array.IndexOf(typeof(Colors).GetProperties(), current);
                }
                else if (current == Model.Instance.Opciones.ColorFill)
                {
                    EleccionColoresRelleno.SelectedIndex = Array.IndexOf(typeof(Colors).GetProperties(), current);
                }
                else if (current == Model.Instance.Opciones.ColorEjes)
                {
                    EleccionColoresEjes.SelectedIndex = Array.IndexOf(typeof(Colors).GetProperties(), current);
                }
            }
            ThicknessSlider.Value = Model.Instance.Opciones.StrokeSizeLinea;
            ThicknessSliderEjes.Value = Model.Instance.Opciones.StrokeSizeEjes;
            LimiteXSlider.Value = Model.Instance.Opciones.LimiteX;
            LimiteYSlider.Value = Model.Instance.Opciones.LimiteY;
            CheckBoxRelleno.IsChecked = Model.Instance.Opciones.IsFillEnabled;
        }
        public void BotonAceptar_Click(Object sender, RoutedEventArgs e)
        {
            int limiteX, limiteY;
            if (EleccionColoresBorde.SelectedItem != null)
            {
                Model.Instance.Opciones.ColorStroke = (Color)ColorConverter.ConvertFromString(typeof(Colors).GetProperties()[EleccionColoresBorde.SelectedIndex].Name);

            }
            if (EleccionColoresEjes.SelectedItem != null)
            {
                Model.Instance.Opciones.ColorEjes = (Color)ColorConverter.ConvertFromString(typeof(Colors).GetProperties()[EleccionColoresEjes.SelectedIndex].Name);
            }
            if (EleccionColoresRelleno.SelectedItem != null)
            {
                Model.Instance.Opciones.ColorFill = (Color)ColorConverter.ConvertFromString(typeof(Colors).GetProperties()[EleccionColoresRelleno.SelectedIndex].Name);
            }

            Model.Instance.Opciones.LimiteX = (int)LimiteXSlider.Value;
            Model.Instance.Opciones.LimiteY = (int)LimiteYSlider.Value;


            Model.Instance.Opciones.IsFillEnabled = (bool)CheckBoxRelleno.IsChecked;
            Model.Instance.Opciones.StrokeSizeLinea = ThicknessSlider.Value;
            Model.Instance.Opciones.StrokeSizeEjes = ThicknessSliderEjes.Value;
            Close();
        }

        private void LimiteBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;
            bool isNumber = (e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9);
            System.Diagnostics.Debug.WriteLine(tb.Text);
            if (!isNumber)
            {
                e.Handled = true;
            }
        }
    }
}
