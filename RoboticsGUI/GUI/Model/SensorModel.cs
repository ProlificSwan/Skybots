using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotics.GUI.Model
{
    class SensorModel:BaseModel
    {
        private bool IsEnabled = false;

        public bool SensorStatus
        {
            get
            {
                return IsEnabled;
            }
            set
            {
                SetProperty(ref IsEnabled, value);
            }
        }


    }
}
