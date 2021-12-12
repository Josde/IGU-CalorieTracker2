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
        // Usamos RelayCommands estáticos en vez de eventos para comunicación entre ventanas y ViewModels.
        // Podríamos usar eventos o delegados alternativamente.
        // La ventaja de usar RelayCommands de esta forma es que los podemos llamar desde cualquier parte de nuestro código
        // sin tener que recurrir a la inyección de dependencias u otros patrones, cosa que complicaría mucho el código.
        public static RelayCommand SingleDayViewCommand;
        public static RelayCommand GlobalViewCommand;
        public static RelayCommand RedrawCommand;
        TipoGrafica tipo;
        // En MainWindow hacemos binding a CurrentView, asi que tenemos que implementar OnPropertyChanged para que el binding se actualice.
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
            // Estos comandos cambian el tipo de gráfica y la vuelven a redibujar.
            // NOTA: Por alguna razón, en la primera ejecución no se redibujan correctamente. Por ello, hay que dar dos clicks para que funcione bien.
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
            // Comando que hace que se redibuje el tipo de gráfica actual.
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
