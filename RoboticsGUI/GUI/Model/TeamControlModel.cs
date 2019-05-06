using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotics.GUI.Model
{
    //Defines all controllable elements on a team's side of the field along with their control pin numbers
    class TeamControlModel : BaseModel
    {
        public TeamControlModel(Int16 plat1, Int16 plat2, Int16 obs1, Int16 obs2, 
          Int16 hover, Int16 start, Int16 motor1, Int16 motor2, int fwdTime, int backTime, Int16 frontLim, Int16 backLim)
        {
            Platform1Led = new LedModel(plat1);
            Platform2Led = new LedModel(plat2);
            Obstacle1Led = new LedModel(obs1);
            Obstacle2Led = new LedModel(obs2);
            HoverLed = new LedModel(hover);
            StartLed = new LedModel(start);
            Motor = new MotorModel(motor1, motor2, fwdTime, backTime);
            BackLimSwitch = new SensorModel(backLim);
            FrontLimSwitch = new SensorModel(frontLim);
            EnableLimitSwitches = true;
    }

        public TeamControlModel(Int16 plat1, Int16 plat2, Int16 obs1, Int16 obs2, 
          Int16 hover, Int16 start, Int16 motor1, Int16 motor2, Int16 frontLim, Int16 backLim)
        {
          Platform1Led = new LedModel(plat1);
          Platform2Led = new LedModel(plat2);
          Obstacle1Led = new LedModel(obs1);
          Obstacle2Led = new LedModel(obs2);
          HoverLed = new LedModel(hover);
          StartLed = new LedModel(start);
          Motor = new MotorModel(motor1, motor2);
          BackLimSwitch = new SensorModel(backLim);
          FrontLimSwitch = new SensorModel(frontLim);
          EnableLimitSwitches = true;
        }

        public LedModel Platform1Led { get; }
        public LedModel Platform2Led { get; }
        public LedModel Obstacle1Led { get; }
        public LedModel Obstacle2Led { get; }
        public MotorModel Motor { get; }
        public LedModel HoverLed { get; }
        public LedModel StartLed { get; }
        public SensorModel BackLimSwitch { get; }
        public SensorModel FrontLimSwitch { get; }
        public bool EnableLimitSwitches { get; set; }

        //Turn off all lights and reset motor to the "start" position
        public void Reset()
        {
            Platform1Led.Reset();
            Platform2Led.Reset();
            Obstacle1Led.Reset();
            Obstacle2Led.Reset();
            HoverLed.Reset();
            StartLed.Reset();
            Motor.RollingStop();
        }

        //Immediately shutdown all controllable items
        public void Shutdown()
        {
            Platform1Led.Reset();
            Platform2Led.Reset();
            Obstacle1Led.Reset();
            Obstacle2Led.Reset();
            HoverLed.Reset();
            StartLed.Reset();
            Motor.EmergencyStop();
        }
    }
}
