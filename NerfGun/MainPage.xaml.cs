﻿using System;
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
        private ComponentsController controller;
        private ObservableCollection<Detection> list;

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
            controller = new ComponentsController();
            controller.InitializeComponents();

            FillWithTestData();
            StartProgram();



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

        }
        
        private void FillWithTestData()
        {
            // Filler data for UI testing
            this.StatusText.Text = "Idle";
            this.TargetStatus.Text = "NA";
            this.AmmoCount.Text = "??";
            this.RefillStatus.Text = "???";

            list = new ObservableCollection<Detection>();
            list.Add(new Detection() { TargetDetected = "Mathuzalem", SystemResponse = "No Response" });
            list.Add(new Detection() { TargetDetected = "Mitch", SystemResponse = "No Response" });
            list.Add(new Detection() { TargetDetected = "Uka", SystemResponse = "Missed" });
            list.Add(new Detection() { TargetDetected = "Anna", SystemResponse = "Missed" });
            list.Add(new Detection() { TargetDetected = "Shiva", SystemResponse = "Turned on lights" });
            list.Add(new Detection() { TargetDetected = "Oscar", SystemResponse = "KIA" });
            this.DataGrid.ItemsSource = list;
        }
        
        public async void StartProgram()
        {
            await SetUp();
        }

        public Task SetUp()
        {
            return Task.Factory.StartNew(() =>
            {
                var system = new ComponentsController();

                system.InitializeComponents();
                // system.FireOnMotion();
                while (system.FireOnMotion())
                {
                    list.Add(new Detection("Unknown", "Fired"));
                    // system.CleanUp();
                    system.Delay(2000);
                    system.CleanUp();
                    system.InitializeComponents();
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
            controller.TestFire();
        }

        private void FireAll_Click(object sender, RoutedEventArgs e)
        {
            controller.FireOnMotion();
        }
    }
}
