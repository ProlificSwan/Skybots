using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotics.GUI.Model
{
    //Stores LED state and Arduino pin number
    public class LedModel : BaseModel
    {
        private Int16 _pinNumber;
        private bool _value;

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

        public bool Value
        {
            get
            {
                return _value;
            }
            set
            {
                SetProperty(ref _value, value);
            }
        }

        public LedModel()
        {
            this._pinNumber = -1;
            this._value = false;
        }

        public LedModel(short pinNumber, bool value)
        {
            this._pinNumber = pinNumber;
            this._value = value;
        }

        public LedModel(short pinNumber)
        {
            this._pinNumber = pinNumber;
            this._value = false;
        }

        public void Toggle()
        {
            Value = !_value;
        }

        public override bool Equals(object obj)
        {
            var led = obj as LedModel;
            return led != null &&
                   _pinNumber == led._pinNumber &&
                   _value == led._value;
        }

        public override int GetHashCode()
        {
            var hashCode = 1549015223;
            hashCode = hashCode * -1521134295 + _pinNumber.GetHashCode();
            hashCode = hashCode * -1521134295 + _value.GetHashCode();
            return hashCode;
        }
    }
}
