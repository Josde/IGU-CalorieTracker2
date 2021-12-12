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
using IGU_CalorieTracker2.Utils;
namespace IGU_CalorieTracker2.MVVM.View
{
    /// <summary>
    /// Interaction logic for GlobalChartView.xaml
    /// </summary>
    public partial class GlobalChartView : UserControl
    {
        public static RelayCommand DrawCommand;
        public GlobalChartView()
        {
            InitializeComponent();
            DrawCommand = new RelayCommand(o =>
            {
                this.Draw();
            });
        }

        //TODO: Cambiar esto
        public void Draw()
        {
            GraphCanvas.Children.Clear();
            GlobalChartViewModel.Draw(GraphCanvas);
        }
    }
}
