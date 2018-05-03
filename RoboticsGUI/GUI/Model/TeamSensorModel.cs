using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotics.GUI.Model
{
    class TeamSensorModel:BaseModel
    {
        public SensorModel Hover { get; } = new SensorModel();
        public SensorModel Platform1 { get; } = new SensorModel();
        public SensorModel Platform2 { get; } = new SensorModel();
        public SensorModel Obstacle1 { get; } = new SensorModel();
        public SensorModel Obstacle2 { get; } = new SensorModel();
        public SensorModel HoverLed { get; } = new SensorModel();
    }
}
