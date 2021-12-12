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
using IGU_CalorieTracker2.MVVM.ViewModel;
using IGU_CalorieTracker2.MVVM.View;
using IGU_CalorieTracker2.Utils;
using Microsoft.Win32;

namespace IGU_CalorieTracker2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AddDataView addDataWindow;
        DateChooserView dateChooserWindow;
        public static RelayCommand ShowArrowButtonCommand;
        public MainWindow()
        {
            InitializeComponent();
            addDataWindow = new AddDataView();
            dateChooserWindow = new DateChooserView(this.DataContext as MainViewModel);
            ShowArrowButtonCommand = new RelayCommand(o =>
            {
                if (o == null) return;
                bool? val = o as bool?;
                if (val == true)
                {
                    BotonFlechaIzquierda.Visibility = Visibility.Visible;
                    BotonFlechaDerecha.Visibility = Visibility.Visible;
                }
                else
                {
                    BotonFlechaIzquierda.Visibility = Visibility.Hidden;
                    BotonFlechaDerecha.Visibility = Visibility.Hidden;
                }
            });
        }


        public void AddData_Click(object sender, RoutedEventArgs e)
        {
            addDataWindow.Show();
        }

        public void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        } 

        public void ContextMenuButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton btn = sender as RadioButton;
            ContextMenu cm = this.FindName(btn.Name + "Menu") as ContextMenu;
            if (cm != null)
            {
                cm.PlacementTarget = btn;
                cm.IsOpen = true;
                e.Handled = true;
            }
            
        }

        public void DebugMenu_Generate_Click(object sender, RoutedEventArgs e)
        {
            Data.GenerateRandomData(15);
            Model.Instance.Sort();
            MainViewModel.RedrawCommand?.Execute(null);
            e.Handled = true;
        }

        public void DebugMenu_Clear_Click(object sender, RoutedEventArgs e)
        {
            Data.Reset();
            MainViewModel.RedrawCommand?.Execute(null);
            e.Handled = true;
        }
        
        public void FileButton_Import_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.csv";
            ofd.Filter = "Comma-separated values (.csv)|*.csv";
            if (ofd.ShowDialog() == true)
            {
                Model.Instance.Dias.Clear();
                Model.Instance.importFromCsv(ofd.FileName);
                MainViewModel.RedrawCommand?.Execute(null);
            }
            
            e.Handled = true;
        }
        public void FileButton_Export_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Untitled";
            sfd.DefaultExt = "*.csv";
            sfd.Filter = "Comma-separated values (.csv)|*.csv";
            bool? result = sfd.ShowDialog();
            if (result == true)
            {
                Model.Instance.saveToCsv(sfd.FileName);
            } 
            e.Handled = true;
        }
        public void DateChooserButton_Click(object sender, RoutedEventArgs e)
        {
            dateChooserWindow.Show();
        }

        public void VistaGlobalButton_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.GlobalViewCommand?.Execute(null);
        }

        public void BotonFlecha_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == "BotonFlechaIzquierda")
            {
                Model.Instance.ChangeDay(-1);
            } else if (btn.Name == "BotonFlechaDerecha")
            {
                Model.Instance.ChangeDay(1);
            }
            MainViewModel.RedrawCommand?.Execute(null);
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        
    }
}
