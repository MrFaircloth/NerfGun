using System;
using System.Reactive.Linq;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;

namespace NerfGun
{
    public sealed class ComponentsController
    {
        private const int MOTOR_PIN = 27; // Pi pin 13
        private const int TRIGGER_PIN = 22; // Pi pin 15
        private const int MOTION_PIN = 26; // Pi pin 37

        NerfGun _gun;
        MotionSensor _motionSensor;
        GpioController _gpio;

        DispatcherTimer _scanner, _ScannerReset, _fireTimer;

        int _scanTime = 100, _fireTime = 500, _delayTime = 2000;
        bool _forceFire = false, _firing = false;

        public ComponentsController()
        {
            _gpio = GpioController.GetDefault();
            _gun = new NerfGun(_gpio);
            _motionSensor = new MotionSensor(_gpio);
            
        }

        // infinite fire on motion
        public void Run( )
        {
            _scanner.Start();
        }

        // creates gpio, nerfgun, motion sensor objects and passes pins
        public void InitializeComponents()
        {
            _gpio = GpioController.GetDefault();
            _gun = new NerfGun(_gpio);
            _motionSensor = new MotionSensor(_gpio);

            _motionSensor.SetPins(MOTION_PIN);
            _gun.SetPins(MOTOR_PIN, TRIGGER_PIN);

            SetTimers();
        }

        public void SetTimers()
        {
            // Checks for motion
            _scanner = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(_scanTime) };
            _scanner.Tick += (sender, args) =>
            {
                if ((_motionSensor.ReadSensor() || _forceFire) && !_firing)
                {
                    _firing = true;
                    _forceFire = false;
                    _scanner.Stop();
                    _gun.Fire();
                    _fireTimer.Start();
                }
            };

            // stops firing after x time
            _fireTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(_fireTime) };
            _fireTimer.Tick += (sender, args) =>
            {
                _gun.CeaseFire();
                _fireTimer.Stop();
                _ScannerReset.Start();
            };

            // resets the scanner after x time
            _ScannerReset = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(_delayTime) };
            _ScannerReset.Tick += (sender, args) =>
            {
                _scanner.Start();
                _ScannerReset.Stop();
                _firing = false;
            };
        }

        // returns motion sensor reading
        // *expected to add multiple motion sensors* 
        public bool TestMotionSensors()
        {
            return _motionSensor.ReadSensor();
        }

        // shoots once (or at least for 0.5 second)
        public void TestFire()
        {
            _forceFire = true;
        }

        public bool CleanUp()
        {
            _gun.CleanUp();
            _motionSensor.CleanUp();
            
            return true;
        }

        public void ScannerDelay()
        {
            //var Result = Observable.Range(0, 1);
            //var frequency = TimeSpan.FromMilliseconds(time);
            //var delay = Result.Delay(frequency);
            //delay.Subscribe(x => _gun.CeaseFire());

        }
    }
}
