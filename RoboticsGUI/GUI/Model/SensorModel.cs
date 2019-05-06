using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotics.GUI.Model
{
    class SensorModel:BaseModel
    {
      private bool _currentState = false; //this is an active variable that should be updated by some sensor update event
      private Int16 _pinNumber;

      public Int16 PinNumber
      {
        get
        {
          return _pinNumber;
        }
        set
        {
          SetProperty(ref _pinNumber, value);
        }
      }

      public bool CurrentState
      {
          get
          {
            return _currentState;
          }
          set
          {
            SetProperty(ref _currentState, value);
          }
      }

      public SensorModel()
      {
        this._pinNumber = -1;
        this._currentState = false;
      }

      public SensorModel(short pinNumber)
      {
        this._pinNumber = pinNumber;
        this._currentState = false;
      }

    //Returns true if valid pinNumber given, else return false.
    public bool UpdateState(short pinNumber, bool state)
    {
      if (pinNumber == _pinNumber)
      {
        _currentState = state;
        return true;
      }
      return false;
    }

  }
}
