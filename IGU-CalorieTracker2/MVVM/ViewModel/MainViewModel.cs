using IGU_CalorieTracker2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGU_CalorieTracker2.MVVM.View;
using System.Windows;
using System.Diagnostics;

namespace IGU_CalorieTracker2.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        SingleDayViewModel singleDayVM;
        GlobalChartViewModel globalChartVM;
        Object currentView;
        public static RelayCommand SingleDayViewCommand;
        public static RelayCommand GlobalViewCommand;
        public static RelayCommand RedrawCommand;
        TipoGrafica tipo;
        public object CurrentView { get => currentView; 
        set {
                currentView = value;
                OnPropertyChanged();
            } 
        }
        public MainViewModel()
        {
            singleDayVM = new SingleDayViewModel();
            globalChartVM = new GlobalChartViewModel();
            CurrentView = globalChartVM;
            tipo = TipoGrafica.Global;

            SingleDayViewCommand = new RelayCommand(o =>
            {
                Debug.WriteLine("Llamado SingleDayViewCommand");
                CurrentView = singleDayVM;
                tipo = TipoGrafica.Dia;
                MainWindow.ShowArrowButtonCommand?.Execute(true);
                SingleDayView.DrawCommand?.Execute(null);
            });

            GlobalViewCommand = new RelayCommand(o =>
            {
                Debug.WriteLine("Llamado GlobalViewCommand");
                CurrentView = globalChartVM;
                tipo = TipoGrafica.Global;
                MainWindow.ShowArrowButtonCommand?.Execute(false);
                GlobalChartView.DrawCommand?.Execute(null);
            });

            RedrawCommand = new RelayCommand(o =>
            {
                if (tipo == TipoGrafica.Dia)
                {
                    SingleDayView.DrawCommand?.Execute(new object());
                } else
                {
                    GlobalChartView.DrawCommand?.Execute(new object());
                }
                
            });
        }


    }
}
