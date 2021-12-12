using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Microsoft.Win32;
using System.Globalization;
using System.Text.RegularExpressions;
using Path = System.IO.Path;
namespace IGU_CalorieTracker2
{
    /// <summary>
    /// Interaction logic for VentanaGrid.xaml
    /// </summary>
    public partial class VentanaGrid : Window
    {
        public ObservableCollection<Dia> coleccionPuntos;
        public EventHandler<EventoDatoArgs> nuevoDato;
        public EventHandler<EventArgs> nuevasOpciones;
        public EventHandler<EventArgs> eventoActualizacionGrafica;
        private GridViewColumnHeader _ultimoHeader;
        private ListSortDirection _ultimaDireccion;
        private String fileName;
        private String filePath;
        public Model m;
        Boolean hasFileBeenModified = true;
        public VentanaGrid()
        {
            FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/Roboto/"), "./#Roboto");
            InitializeComponent();
            MenuItem[] menuOpciones = { NewFile, OpenFile, Save, SaveAs, Close, Settings };
            DataContext = this;
            fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Untitled.csv");
            filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            foreach (MenuItem o in menuOpciones)
            {
                //https://stackoverflow.com/a/30285
                o.Icon = new System.Windows.Controls.Image
                {
                    Source = new BitmapImage(new Uri(String.Format("/Images/{0}/{0}_16x.png", o.Name), UriKind.Relative))
                };
                o.UpdateLayout();
            }
        }
        private void CrearVentanaOpcionesCommandExecute(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Llamado CrearVentanaOpcionesCommand");
            VentanaOpciones nuevaVentana = new VentanaOpciones();
            nuevaVentana.ShowDialog();
            this.nuevasOpciones?.Invoke(this, EventArgs.Empty);

        }

        private void OpenExecute(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Llamado OpenExecute");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = filePath;
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                if (!Model.Instance.importFromCsv(openFileDialog.FileName))
                {
                    MessageBox.Show(String.Format("Error cargando el archivo {0}", openFileDialog.FileName));
                    e.Handled = true;
                }
            }

            fileName = openFileDialog.FileName;
            hasFileBeenModified = false;
        }

        private void CloseExecute(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Llamado CloseExecute");
            if (hasFileBeenModified)
            {
                MessageBoxResult result = MessageBox.Show("Tienes cambios por guardar en el fichero. Quieres guardarlos?", "Cerrado", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Model.Instance.saveToCsv(fileName);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        e.Handled = true;
                        return;

                }
            }
            Application.Current.Shutdown();
        }

        private void SaveExecute(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Llamado SaveExecute");
            Model.Instance.saveToCsv(fileName);
            hasFileBeenModified = false;

        }
        private void SaveAsExecute(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Llamado SaveAsExecute");
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "*.csv";
            saveFileDialog.Filter = "Comma-separated Values|*.csv";
            saveFileDialog.InitialDirectory = filePath;
            saveFileDialog.DefaultExt = "csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                fileName = saveFileDialog.FileName;
                if (!Model.Instance.saveToCsv(fileName))
                {
                    System.Diagnostics.Debug.WriteLine("Error en SaveAsExecute");
                    e.Handled = true;
                }
                hasFileBeenModified = false;
            }
        }
        private void NewExecute(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Llamado NewExecute");
            {
                MessageBoxResult result = MessageBox.Show("Tienes cambios por guardar en el fichero. Quieres guardarlos?", "Cerrado", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Model.Instance.saveToCsv(fileName);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        e.Handled = true;
                        return;

                }
            }
            fileName = "Untitled.csv";
            hasFileBeenModified = false;
            Model.Instance.Dias.Clear();
            // TODO: MEJORAR ESTO
        }

        private void PolyBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;
            bool isNumber = (e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9);
            if (!isNumber)
            {
                e.Handled = true;
            }
        }
    }
    public class EventoDatoArgs : EventArgs
    {
        private Object m_o;
        public EventoDatoArgs(Object _o)
        {
            m_o = _o;
        }
        public Object o { get { return m_o; } }
    }
}
