using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace NerfGun
{
    public class UIController
    {
        Page page;
        private ObservableCollection<Detection> list;
        private PropertyInfo[] properties;

        public int AmmoCount = 10;

        public UIController(Page page)
        {
            this.page = page;
            
            properties = this.page.GetType().GetProperties();
           
        }


        //private void FillWithTestData()
        //{
        //    //Filler data for UI testing
        //    page.StatusText.Text = "Idle";
        //    page.TargetStatus.Text = "NA";
        //    page.AmmoCount.Text = "??";
        //    page.RefillStatus.Text = "???";

        //    list = new ObservableCollection<Detection>();
        //    list.Add(new Detection() { TargetDetected = "Mathuzalem", SystemResponse = "No Response" });
        //    list.Add(new Detection() { TargetDetected = "Mitch", SystemResponse = "No Response" });
        //    list.Add(new Detection() { TargetDetected = "Uka", SystemResponse = "Missed" });
        //    list.Add(new Detection() { TargetDetected = "Anna", SystemResponse = "Missed" });
        //    list.Add(new Detection() { TargetDetected = "Shiva", SystemResponse = "Turned on lights" });
        //    list.Add(new Detection() { TargetDetected = "Oscar", SystemResponse = "KIA" });
        //    page.DataGrid.ItemsSource = list;
        //}
    }
}
