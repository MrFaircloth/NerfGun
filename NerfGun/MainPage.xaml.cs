using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Windows.ApplicationModel.Background;
using Windows.UI.Core;
using System.Collections.ObjectModel;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NerfGun
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ComponentsController _CController;
        private UIController _UIController;
        private ObservableCollection<Detection> list;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            // tbd
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            // tbd
        }

        private void Fire1_Click(object sender, RoutedEventArgs e)
        {
            _CController.TestFire();
        }

        private void FireAll_Click(object sender, RoutedEventArgs e)
        {
            // tbd
        }

        private void FillWithTestData()
        {
            // Filler data for UI testing

            list = new ObservableCollection<Detection>();
            list.Add(new Detection() { TargetDetected = "Mathuzalem", SystemResponse = "No Response" });
            list.Add(new Detection() { TargetDetected = "Mitch", SystemResponse = "No Response" });
            //list.Add(new Detection() { TargetDetected = "Uka", SystemResponse = "Missed" });
            //list.Add(new Detection() { TargetDetected = "Anna", SystemResponse = "Missed" });
            //list.Add(new Detection() { TargetDetected = "Shiva", SystemResponse = "Turned on lights" });
            //list.Add(new Detection() { TargetDetected = "Oscar", SystemResponse = "KIA" });
            this.DataGrid.ItemsSource = list;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_CController == null)
            {
                _UIController = new UIController(this);
                _CController = new ComponentsController(ref _UIController);
                _CController.InitializeComponents(); // prepare sensors
                _CController.Run(); // start running fire on motion
            }
            FillWithTestData();
        }
    }
}
