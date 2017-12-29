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
        private ComponentsController _CController;
        private UIController _UIController;
        // private ObservableCollection<Detection> list;
        private Task mainThread;

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

            this.AmmoCount.Text = "10";

            _CController = new ComponentsController();
            //controller.InitializeComponents();

            mainThread = SetUp();
            //StartProgram();



            //// This is a static public property that allows downstream pages to get a handle to the MainPage instance
            //// in order to call methods that are in this class.
            //Current = this;

            //MainPageDispatcher = Window.Current.Dispatcher;

            //this.Loaded += async (sender, e) =>
            //{
            //    await MainPageDispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
            //    {
            //        StartProgram();
            //    });
            //};
            // FillWithTestData();
        }
        
        
        public async void StartProgram()
        {
            await SetUp();
        }

        public Task SetUp()
        {
            return Task.Run(() =>
            {
                _CController = new ComponentsController();
                _UIController = new UIController(this);

                _CController.InitializeComponents();
                // system.FireOnMotion();
                while (true)
                {
                    _CController.FireOnMotion();
                    _UIController.UpdateAmmunition(-1);
                    // list.Add(new Detection("Unknown", "Fired"));
                    // system.CleanUp();
                    _CController.Delay(2000);
                    _CController.CleanUp();
                    _CController.InitializeComponents();
                }
            });

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
            _CController.FireOnMotion();
        }
    }
}
