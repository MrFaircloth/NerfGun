using System.ComponentModel;
using Windows.Devices.Gpio;

namespace NerfGun
{
    public class NerfGun : INotifyPropertyChanged
    {
        GpioController _gpio;
        GpioPin _motor, _trigger;

        private int _ammCount = 10;
        
        public int AmmoCount
        {
            get { return _ammCount; }
            set
            {
                _ammCount = value;
                RaisePropertyChanged("AmmoCount");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public NerfGun(GpioController gpio)
        {
            // Controller to access GPIO pins
            _gpio = gpio;
        }

        public bool SetPins(int motor, int trigger)
        {
            // Open pins for usage
            _motor = _gpio.OpenPin(motor);
            _trigger = _gpio.OpenPin(trigger);

            // Sets pins to output a signal
            _motor.SetDriveMode(GpioPinDriveMode.Output);
            _trigger.SetDriveMode(GpioPinDriveMode.Output);

            // Eventually add some checks to make sure that everything runs smoothly.
            return true;
        }

        public void Fire()
        {
            // Write to pins (low since relay holds at high)
            _motor.Write(GpioPinValue.Low);
            _trigger.Write(GpioPinValue.Low);
        }

        public void CeaseFire()
        {
            // Write to pins (low since relay holds at high)
            _motor.Write(GpioPinValue.High);
            _trigger.Write(GpioPinValue.High);
        }

        public void CleanUp()
        {
            // *should* reset the pins.
            _motor.Dispose();
            _trigger.Dispose();
        }
    }
}
