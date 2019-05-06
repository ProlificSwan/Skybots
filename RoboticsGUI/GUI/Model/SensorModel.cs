using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solid.Arduino.Firmata;

namespace Robotics.GUI.Model
{
  public class SensorModel:BaseModel, IObserver<DigitalPortState>
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

    public virtual void Subscribe(IObservable<DigitalPortState> provider)
    {
      if (provider != null)
        provider.Subscribe(this);
    }

    public virtual void OnCompleted()
    {
      Console.WriteLine("Complete!");
      //below code was taken from MS example. Currently don't need unsubscribe functionality, so ignoring.
      //Console.WriteLine("The Location Tracker has completed transmitting data to {0}.", this.Name)
      //this.Unsubscribe();
    }

    public virtual void OnError(Exception e)
    {
      Console.WriteLine("Error!");
      //Console.WriteLine("{0}: The location cannot be determined.", this.Name);
    }

    public virtual void OnNext(DigitalPortState value)
    {
      //Each "port" is 8 bits long and covers 8 pins, 0-indexed. So arduino pins 0-7 map to port 0 bits 0-7
      //and pins 8-15 map to port 1 bits 0-7. Port 2 maps pins 16-23 to bits 0-7, and so on.
      int portPin = _pinNumber % 8;
      int portNum = (_pinNumber / 8);

      if (value.Port == portNum)
      {
        this.CurrentState = value.IsSet(portPin);
        //Console.WriteLine("Pin Number: {0} Pin value: {1}",_pinNumber, _currentState);
      }
      //Console.WriteLine("Pin Number: {0} Pin value: {1}",_pinNumber, value.IsSet(portPin));
      //Console.WriteLine("Bit Mask: {0}", Convert.ToString(PinToBitMask(_pinNumber), 2));
    }

    //TODO: move this to helper functions
    private int PinToBitMask(Int16 PinNumber)
    {
      int mask = 0b0;
      if (PinNumber > 0)
      {
        mask = 1 << (PinNumber % 8);
      }
      return mask;
    }
  }
}
