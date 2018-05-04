using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotics.GUI.Model
{
    //Stores LED state and Arduino pin number
    public class MotorModel : BaseModel
    {
        private Int16 _pinNumber1;
        private Int16 _pinNumber2;
        private bool _in1;
        private bool _in2;
        private bool _lastIn1;
        private bool _lastIn2;

        //TODO - supply arduino with event for changes to in1 and in2?

        //Sets aside current motor values and sets motor state to stopped (still need arduino to update)
        //Intended for use in concert with Continue()
        public void Stop()
        {
            _lastIn1 = _in1;
            _lastIn2 = _in2;
            _in1 = false;
            _in2 = false;
        }

        public void Forward()
        {
            _in1 = true;
            _in2 = false;
        }

        public void Backward()
        {
            _in1 = false;
            _in2 = true;
        }

        //Retreives past motor values to continue motor movement in direction was moving before pause.
        //Intended for use in concert with Stop()
        public void Continue()
        {
            _in1 = _lastIn1;
            _in2 = _lastIn2;
        }

        #region classSupport

        public Int16 PinNumber1
        {
            get
            {
                return _pinNumber1;
            }
            set
            {
                SetProperty(ref _pinNumber1, value);
            }
        }

        public Int16 PinNumber2
        {
            get
            {
                return _pinNumber2;
            }
            set
            {
                SetProperty(ref _pinNumber2, value);
            }
        }

        public bool In1
        {
            get
            {
                return _in1;
            }
            set
            {
                SetProperty(ref _in1, value);
            }
        }

        public bool In2
        {
            get
            {
                return _in2;
            }
            set
            {
                SetProperty(ref _in2, value);
            }
        }

        public MotorModel()
        {
            this._pinNumber1 = -1;
            this._pinNumber2 = -1;
            this._in1 = false;
            this._in2 = false;
            this._lastIn1 = false;
            this._lastIn2 = false;
        }

        public MotorModel(short pinNumber1, short pinNumber2, bool in1, bool in2)
        {
            this._pinNumber1 = pinNumber1;
            this._pinNumber2 = pinNumber2;
            this._in1 = in1;
            this._in2 = in2;
            this._lastIn1 = false;
            this._lastIn2 = false;
        }

        public MotorModel(short pinNumber1, short pinNumber2)
        {
            this._pinNumber1 = pinNumber1;
            this._pinNumber2 = pinNumber2;
            this._in1 = false;
            this._in2 = false;
            this._lastIn1 = false;
            this._lastIn2 = false;
        }

        public override bool Equals(object obj)
        {
            var model = obj as MotorModel;
            return model != null &&
                   _pinNumber1 == model._pinNumber1 &&
                   _pinNumber2 == model._pinNumber2 &&
                   _in1 == model._in1 &&
                   _in2 == model._in2 &&
                   PinNumber1 == model.PinNumber1 &&
                   PinNumber2 == model.PinNumber2 &&
                   In1 == model.In1 &&
                   In2 == model.In2;
        }

        public override int GetHashCode()
        {
            var hashCode = -353953787;
            hashCode = hashCode * -1521134295 + _pinNumber1.GetHashCode();
            hashCode = hashCode * -1521134295 + _pinNumber2.GetHashCode();
            hashCode = hashCode * -1521134295 + _in1.GetHashCode();
            hashCode = hashCode * -1521134295 + _in2.GetHashCode();
            hashCode = hashCode * -1521134295 + _lastIn1.GetHashCode();
            hashCode = hashCode * -1521134295 + _lastIn2.GetHashCode();
            hashCode = hashCode * -1521134295 + PinNumber1.GetHashCode();
            hashCode = hashCode * -1521134295 + PinNumber2.GetHashCode();
            hashCode = hashCode * -1521134295 + In1.GetHashCode();
            hashCode = hashCode * -1521134295 + In2.GetHashCode();
            return hashCode;
        }

        #endregion
    }
}
