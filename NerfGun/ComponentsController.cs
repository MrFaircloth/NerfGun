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

        public NerfGun _gun;
        MotionSensor _motionSensor;
        GpioController _gpio;
        UIController _UIController;

        DispatcherTimer _scanner, _ScannerReset, _fireTimer;
        // Firing variables
        const int SCANTIME = 200, FIRETIME = 500, DELAYTIME = 1000;
        bool _forceFire = false, _firing = false;

        public ComponentsController(ref UIController controller)
        {
            _UIController = controller;
        }

        // infinite fire on motion
        public void Run()
        {
            _scanner.Start();
        }

        // creates gpio, nerfgun, motion sensor objects and passes pins
        public void InitializeComponents()
        {
            // Builds components - Creates NerfGun and Motion Sensor Objects which are connected to the pi
            _gpio = GpioController.GetDefault();
            _gun = new NerfGun(_gpio);
            _motionSensor = new MotionSensor(_gpio);

            // sets pins which the pi will be sending/receiving signals to/from in order to communicate with nerf gun && motion sensor
            _motionSensor.SetPins(MOTION_PIN);
            _gun.SetPins(MOTOR_PIN, TRIGGER_PIN);

            // creates timers that will determine how the nerf gun will behave
            SetTimers();
        }

        public void SetTimers()
        {
            // Checks for motion
            _scanner = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(SCANTIME) };
            _scanner.Tick += (sender, args) =>
            {
                // Triggers if the motion sensor is tripped || user initiates a test fire && the gun isn't already firing
                if ((_motionSensor.ReadSensor() || _forceFire) && !_firing)
                {
                    _firing = true;
                    _forceFire = false;
                    _scanner.Stop(); // Stop scanning
                    _gun.Fire(); // Start Firing
                    _fireTimer.Start(); // Start delay to keep firing until X time
                }
            };

            // stops firing after x time
            _fireTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(FIRETIME) };
            _fireTimer.Tick += (sender, args) =>
            {
                _gun.CeaseFire(); // Stops firing
                _fireTimer.Stop(); // Stops timer
                _gun.AmmoCount--;
                
                _ScannerReset.Start(); // Starts timer to delay scanning for targets
            };

            // resets the scanner after x time
            _ScannerReset = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(DELAYTIME) };
            _ScannerReset.Tick += (sender, args) =>
            {
                _scanner.Start(); // Starts scanning motion sensor for targets
                _ScannerReset.Stop();
                _firing = false; // Declares that it the program has finished firing
                // _UIController.Refresh();
            };
        }

        // returns motion sensor reading
        // *expected to add multiple motion sensors* 
        public bool TestMotionSensors()
        {
            return _motionSensor.ReadSensor();
        }

        // shoots once
        public void TestFire()
        {
            _forceFire = true;
        }

        // Clean up! Clean up! Everybody! Everywhere!
        public bool CleanUp()
        {
            _gun.CleanUp();
            _motionSensor.CleanUp();
            
            return true;
        }
    }
}
