using System;
using System.Reactive.Linq;
using Windows.Devices.Gpio;

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

        public ComponentsController()
        {
            _gpio = GpioController.GetDefault();
            _gun = new NerfGun(_gpio);
            _motionSensor = new MotionSensor(_gpio);
        }

        // infinite fire on motion
        public void Run( )
        {
            InitializeComponents();
            while (true)
            {
                FireOnMotion();
                Delay(1000);
            }
        }

        // creates gpio, nerfgun, motion sensor objects and passes pins
        public void InitializeComponents()
        {
            _gpio = GpioController.GetDefault();
            _gun = new NerfGun(_gpio);
            _motionSensor = new MotionSensor(_gpio);

            _motionSensor.SetPins(MOTION_PIN);
            _gun.SetPins(MOTOR_PIN, TRIGGER_PIN);
        }

        // returns motion sensor reading
        // *expected to add multiple motion sensors* 
        public bool TestMotionSensors()
        {
            return _motionSensor.ReadSensor();
        }

        // shoots once (or at least for 0.5 second)
        public bool TestFire()
        {
            _gun.Fire();
            Delay(500);
            //_gun.CeaseFire();
            return true;
        }

        // Fires after motion is detected
        public bool FireOnMotion()
        {
            while (!_motionSensor.ReadSensor()) { }
            _gun.Fire();
            Delay(500);
            
            return true;
        }

        public bool CleanUp()
        {
            _gun.CleanUp();
            _motionSensor.CleanUp();
            return true;
        }

        // I kinda understand this
        public void Delay(int time)
        {
            var Result = Observable.Range(0, 10);
            var frequency = TimeSpan.FromMilliseconds(time);
            var delay = Result.Delay(frequency);
            delay.Subscribe(x => _gun.CeaseFire());
        }
    }
}
