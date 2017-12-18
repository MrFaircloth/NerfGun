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
        
        public static MainPage Current;
        private CoreDispatcher MainPageDispatcher;

        ObservableCollection<Detection> list;

        public CoreDispatcher UIThreadDispatcher
        {
            get
            {
                return MainPageDispatcher;
            }

            set
            {
                MainPageDispatcher = value;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();

            list = new ObservableCollection<Detection>();
            list.Add(new Detection() { TargetDetected = "Mathuzalem", SystemResponse = "No Response" });
            list.Add(new Detection() { TargetDetected = "Mitch", SystemResponse = "No Response" });
            list.Add(new Detection() { TargetDetected = "Uka",  SystemResponse = "Missed" });
            list.Add(new Detection() { TargetDetected = "Anna", SystemResponse = "Missed" });
            list.Add(new Detection() { TargetDetected = "Shiva", SystemResponse = "Turned on lights" });
            list.Add(new Detection() { TargetDetected = "Oscar", SystemResponse = "KIA" });
            this.DataGrid.ItemsSource = list;




            // This is a static public property that allows downstream pages to get a handle to the MainPage instance
            // in order to call methods that are in this class.
            Current = this;

            MainPageDispatcher = Window.Current.Dispatcher;

        
            this.Loaded += async (sender, e) =>
            {
                await MainPageDispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
                {
                    StartProgram();
                });
            };
        }            

        public void StartProgram()
        {
            var system = new ComponentsController();
            system.InitializeComponents();
            while (true)
            {
               
                
                system.TestFireOnMotionAsync();
                list.Add(new Detection("Unknown", "Fired"));
                this.DataGrid.ItemsSource = list;
               // system.CleanUp();
                system.Delay(2000);
                
            }
            
        }
    }
}
