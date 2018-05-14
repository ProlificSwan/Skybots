﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using Robotics.GUI.Helpers;

namespace Robotics.GUI.Model
{
    //Stores LED state and Arduino pin number
    public class MotorModel : BaseModel
    {
        public int ForwardTime
        {
            get
            {
                return _forwardTime;
            }
            set
            {
                SetProperty(ref _forwardTime, value);
            }
        }
        public int BackwardTime
        {
            get
            {
                return _backwardTime;
            }
            set
            {
                SetProperty(ref _backwardTime, value);
            }
        }

        private int _forwardTime;
        private int _backwardTime;
        private int _lastForwardTime;
        private int _lastBackwardTime;
        private MoveState _state = MoveState.Start;
        private bool _stopping = false;
        private Int16 _pinNumber1;
        private Int16 _pinNumber2;
        private bool _in1;
        private bool _in2;
        private PauseableTimer _forwardTimer;
        private PauseableTimer _backwardTimer;

        private enum MoveState
        {
            Start,
            MoveFwd,
            MoveBack,
            PauseFwd,
            PauseBack
        }

        public MotorModel(short pinNumber1, short pinNumber2, bool in1, bool in2, int fwdTime = Constants.defaultMotorFwdTime, int backTime = Constants.defaultMotorBackTime)
        {
            this._pinNumber1 = pinNumber1;
            this._pinNumber2 = pinNumber2;
            this._in1 = in1;
            this._in2 = in2;
            ForwardTime = fwdTime;
            _lastForwardTime = ForwardTime;
            BackwardTime = backTime;
            _lastBackwardTime = BackwardTime;
            _forwardTimer = new PauseableTimer(ForwardTime);
            _forwardTimer.Elapsed += OnForwardTimerElapsed;
            _backwardTimer = new PauseableTimer(BackwardTime);
            _backwardTimer.Elapsed += OnBackwardTimerElapsed;
        }

        public MotorModel(short pinNumber1, short pinNumber2, int fwdTime = Constants.defaultMotorFwdTime, int backTime = Constants.defaultMotorBackTime)
        {
            this._pinNumber1 = pinNumber1;
            this._pinNumber2 = pinNumber2;
            this._in1 = false;
            this._in2 = false;
            ForwardTime = fwdTime;
            _lastForwardTime = ForwardTime;
            BackwardTime = backTime;
            _lastBackwardTime = BackwardTime;
            _forwardTimer = new PauseableTimer(ForwardTime);
            _forwardTimer.Elapsed += OnForwardTimerElapsed;
            _backwardTimer = new PauseableTimer(BackwardTime);
            _backwardTimer.Elapsed += OnBackwardTimerElapsed;

        }
        
        //Start timed motor movement or continue movement if paused.
        public void Play()
        {
            _stopping = false; //allows user to cancel rolling stop by pressing play again.
            if (_state == MoveState.Start)
            {
                _state = MoveState.MoveFwd;
                Forward();
                CheckIntervalChange();
                _forwardTimer.Start();
            }
            else if (_state == MoveState.PauseBack || _state ==MoveState.PauseFwd)
            {
                Continue();
            }
        }

        //Intended to update motor movement timer/interval(s) as needed when in MoveState.Start state.
        private void CheckIntervalChange()
        {
            if (_forwardTime != _lastForwardTime)
            {
                _lastForwardTime = _forwardTime;
                _forwardTimer.Interval = _forwardTime;
            }
            if (_backwardTime != _lastBackwardTime)
            {
                _lastBackwardTime = _backwardTime;
                _backwardTimer.Interval = _backwardTime;
            }
        }

        private void OnForwardTimerElapsed(object sender, EventArgs e)
        {
            _forwardTimer.Reset();
            Backward();
            _state = MoveState.MoveBack;
            _backwardTimer.Start();
        }

        private void OnBackwardTimerElapsed(object sender, EventArgs e)
        {
            _backwardTimer.Reset();
            _state = MoveState.Start;
            CompleteLoop(); //keep running until told to do otherwise.
        }

        //helper functon to ensure timers aren't affected by indecisive users (playing after rolling stop but before return to Start)
        private void CompleteLoop()
        {
            if (_stopping)
            {
                Stop();
                _stopping = false;
            }
            else
            {
                Play();
            }
        }

        //For use when a Play() is intenionally called close after
        public void CancelRollingStop()
        {
            _stopping = false;
        }

        //Returns motors to known starting positions (based on forward/backward times) and then stops.
        //Does nothing if Play() was never called.
        public void RollingStop()
        {
            _stopping = true;
            if (_state == MoveState.Start)
            {
                Stop();
                _stopping = false;
            }
            else if(_state == MoveState.PauseBack || _state == MoveState.PauseFwd)
            {
                Continue();
            }
        }

        //Sets aside current motor state and immediately stops motor
        //Intended for use in concert with Continue()
        public void Pause()
        {
            Stop();
            if (_state == MoveState.MoveFwd)
            {
                _state = MoveState.PauseFwd;
                _forwardTimer.Pause();
            }
            else if (_state == MoveState.MoveBack)
            {
                _state = MoveState.PauseBack;
                _backwardTimer.Pause();
            }
        }

        //Retreives past motor state to continue motor movement in direction was moving before pause.
        //Intended for use in concert with Pause()
        public void Continue()
        {
            if (_state == MoveState.PauseFwd)
            {
                _state = MoveState.MoveFwd;
                Forward();
                _forwardTimer.Continue();
            }
            else if (_state == MoveState.PauseBack)
            {
                _state = MoveState.MoveBack;
                Backward();
                _backwardTimer.Continue();
            }

        }

        //Helper function - stops motors
        private void Stop()
        {
            _in1 = false;
            _in2 = false;
            OnPropertyChanged();
        }

        //Immediately stops motor, prevents pending automatic movement, and resets internal state
        //such that current position will be considered the "start" location.
        public void EmergencyStop()
        {
            Stop();
            _stopping = false;
            _state = MoveState.Start;
            _forwardTimer.Reset();
            _backwardTimer.Reset();
        }


        //Moves motor Forward until Stop() commanded
        public void Forward()
        {
            _in1 = true;
            _in2 = false;
            OnPropertyChanged();
        }

        //Moves motor backward until Stop() commanded
        public void Backward()
        {
            _in1 = false;
            _in2 = true;
            OnPropertyChanged();
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
        }

        #endregion
    }
}
