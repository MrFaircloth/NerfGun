using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace NerfGun
{
    class NerfGun
    {
        GpioController _gpio;
        GpioPin _motor, _trigger;

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
            // FIRE
            _motor.Write(GpioPinValue.Low);
            _trigger.Write(GpioPinValue.Low);
        }

        public void CeaseFire()
        {
            // Write to pins (low since relay holds at high)
            // HOLD
            _motor.Write(GpioPinValue.High);
            _trigger.Write(GpioPinValue.High);
        }

        public void CleanUp()
        {
            _motor.Dispose();
            _trigger.Dispose();
        }
    }
}
