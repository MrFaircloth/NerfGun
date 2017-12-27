using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Devices.Gpio;

namespace NerfGunBackground
{
    public sealed class ComponentsController
    {
        private const int MOTOR_PIN = 27; // Pi pin 13
        private const int TRIGGER_PIN = 22; // Pi pin 15
        private const int MOTION_PIN = 26; // Pi pin 37

        BackgroundTaskDeferral _deferral;
        NerfGun _gun;
        MotionSensor _motionSensor;
        GpioController _gpio;

        public ComponentsController()
        {
            _gpio = GpioController.GetDefault();
            _gun = new NerfGun(_gpio);
            _motionSensor = new MotionSensor(_gpio);
        }

        public void InitializeComponents()
        {
            _motionSensor.SetPins(MOTION_PIN);
            _gun.SetPins(MOTOR_PIN, TRIGGER_PIN);
        }

        public bool TestMotionSensors()
        {
            return _motionSensor.ReadSensor();
        }

        public bool TestFire()
        {
            _gun.Fire();
            Delay(1000);
            //_gun.CeaseFire();
            return true;
        }

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

        public void Delay(int time)
        {
            var Result = Observable.Range(0, 10);
            var frequency = TimeSpan.FromMilliseconds(time);
            var delay = Result.Delay(frequency);
            delay.Subscribe(x => _gun.CeaseFire());
        }


    }
}
