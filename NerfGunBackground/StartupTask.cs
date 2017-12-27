using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace NerfGunBackground
{
    public sealed class StartupTask : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral;
        ComponentsController _controller;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            _controller = new ComponentsController();
            _controller.InitializeComponents();
            while (true)
            {
                _controller.FireOnMotion();
                _controller.Delay(1000);
            }
        }
    }
}
