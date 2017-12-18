using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace NerfGun
{
    class MotionSensor
    {
        GpioController _gpio;
        GpioPin _sensorPin;

        public MotionSensor(GpioController gpio)
        {
            // Controller to access GPIO pins
            _gpio = gpio;
        }

        public bool SetPins(int motionSensor)
        {
            // Opens pin for usage
            _sensorPin = _gpio.OpenPin(motionSensor);
            // Sets pin to read in data
            _sensorPin.SetDriveMode(GpioPinDriveMode.Input);

            return true;
        }

        public void CleanUp()
        {
            _sensorPin.Dispose();
        }

        public bool ReadSensor()
        {
            // check if pin reads in High (Detected motion)
            return _sensorPin.Read() == GpioPinValue.High;
        }

    }
}
