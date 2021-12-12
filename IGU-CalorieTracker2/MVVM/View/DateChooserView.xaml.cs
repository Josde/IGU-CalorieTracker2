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
            Dia select = ListViewDatos.SelectedItem as Dia;
            Model.Instance.diaSeleccionado = select;
            this.cc.Clear();
            this.cc.Add(new Comida("Desayuno", select.caloriasDesayuno));
            this.cc.Add(new Comida("Comida", select.caloriasComida));
            this.cc.Add(new Comida("Cena", select.caloriasCena));
            this.cc.Add(new Comida("Snacks", select.caloriasSnacks));
            
            MainViewModel.SingleDayViewCommand?.Execute(null);
        }

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
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
