using IGU_CalorieTracker2.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace IGU_CalorieTracker2.MVVM.View
{
    /// <summary>
    /// Interaction logic for DateChooserView.xaml
    /// </summary>
    public partial class DateChooserView : Window
    {
        
        public class Comida
        {
            // Esto es una clase temporal que usamos para instanciar el GridView que nos da los detalles de las comidas de cada dia.
            // Podriamos obviarlo introduciendo nuevas propiedades en Dia y cambiando la manera en la que usamos los datos y los bindings.
            // Pero esta es la solucion rapida. Si tuviera mas tiempo, haria lo mencionado previamente.
            public Comida(string nombre, float calorias)
            {
                Nombre = nombre;
                Calorias = calorias;
            }

            public string Nombre { get; set; }
            public float Calorias { get; set; }
        }

        ObservableCollection<Comida> cc;
        public DateChooserView(MainViewModel main)
        {
            cc = new ObservableCollection<Comida>();
            InitializeComponent();
            this.DataContext = this;
            ListViewDatos.ItemsSource = Model.Instance.Dias;
            ListViewDiaActual.ItemsSource = this.cc;
        }

        public void Datos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Cuando seleccionamos un elemento, cambiamos diaSeleccionado en el Modelo y redibujamos la grafica, pero ahora en modo de un solo dia.
            // Además, instanciamos la coleccion que da los datos al GridView de detalle de la parte inferior de la ventana.
            Dia select = ListViewDatos.SelectedItem as Dia;
            Model.Instance.diaSeleccionado = select;
            this.cc.Clear();
            this.cc.Add(new Comida("Desayuno", select.caloriasDesayuno));
            this.cc.Add(new Comida("Comida", select.caloriasComida));
            this.cc.Add(new Comida("Cena", select.caloriasCena));
            this.cc.Add(new Comida("Snacks", select.caloriasSnacks));
            
            MainViewModel.SingleDayViewCommand?.Execute(null);
        }
        // Al igual que en AddDataView, escondemos la ventana en vez de cerrarla para no perder los datos que teniamos seleccionados.
        private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
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
