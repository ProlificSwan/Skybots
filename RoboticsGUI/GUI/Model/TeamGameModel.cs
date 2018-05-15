using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Robotics.GUI.Helpers;

namespace Robotics.GUI.Model
{
    class TeamGameModel : BaseModel
    {
        public TeamGameModel(TeamScoreModel teamScore, TeamControlModel teamControl, CountdownModel countdown)
        {
            _teamScore = teamScore;
            _teamControl = teamControl;

            //time event setup
            _countdown = countdown;
            startLeft = countdown.Timeout.TotalMilliseconds - startTime;
            hoverLeft = countdown.Timeout.TotalMilliseconds - hoverTime;
            obstacleLeft = countdown.Timeout.TotalMilliseconds - obstacleTime;
            platformLeft = countdown.Timeout.TotalMilliseconds - platformTime;
            _countdown.PropertyChanged += OnTimeChange;
            _blinkTimer = new Timer(Constants.preStartBlinkInterval) { AutoReset = true };
            _blinkTimer.Elapsed += OnBlinkElapsed;

        }

        private TeamScoreModel _teamScore;
        private TeamControlModel _teamControl;
        private CountdownModel _countdown;
        private Timer _blinkTimer;

        public enum gameState
        {
            Idle,
            PreStart,
            Start,
            Hover,
            Obstacles,
            Platforms
        }

        private gameState _state = gameState.Idle;
        public gameState State
        {
            get
            {
                return _state;
            }
            private set
            {
                SetProperty(ref _state, value);
            }
        }
        //TODO - make game state times configurable
        private const int startTime = 5000; //start time in msecs
        private const int hoverTime = 30000; //hover time in total elapsed msecs before absolute end
        private const int obstacleTime = 105000; //obstacle time in total elapsed msecs before absolute end
        private const int platformTime = 150000; //platform time
        //Time left after above times have elapsed
        private double startLeft;
        private double hoverLeft;
        private double obstacleLeft;
        private double platformLeft;

        private void OnTimeChange(object sender, PropertyChangedEventArgs e)
        {
            double timeLeft = _countdown.TimeRemaining.TotalMilliseconds;
            switch (_state)
            {
                case gameState.Idle:
                    if (timeLeft > 0 && timeLeft < _countdown.Timeout.TotalMilliseconds)
                    {
                        _state = gameState.Start;
                        _teamControl.StartLed.Value = true;
                        _teamControl.HoverLed.Value = true;
                    }
                    break;
                case gameState.Start:
                    if (timeLeft <= startLeft)
                    {
                        _state = gameState.Hover;
                        _teamControl.StartLed.Value = false;
                        
                    }

                    /*Actually it's impossible to finish the hover during the first five seconds, so don't allow the state to change early.
                    if (_teamScore.Hover.Score > 0)
                    {
                        _state = gameState.Obstacles;
                        _teamControl.StartLed.Value = false;
                        _teamControl.HoverLed.Value = false;
                    }*/
                    break;
                case gameState.Hover:
                    if (_teamScore.Hover.Score > 0 || timeLeft <= hoverLeft)
                    {
                        _state = gameState.Obstacles;
                        _teamControl.HoverLed.Value = false;
                        _teamControl.Obstacle1Led.Value = true;
                        _teamControl.Obstacle2Led.Value = true;
                        _teamControl.Motor.Play();
                    }
                    break;
                case gameState.Obstacles:
                    if (timeLeft <= obstacleLeft)
                    {
                        _state = gameState.Platforms;
                        _teamControl.Obstacle1Led.Value = false;
                        _teamControl.Obstacle2Led.Value = false;
                        _teamControl.Motor.RollingStop();
                        _teamControl.Platform1Led.Value = true;
                        _teamControl.Platform2Led.Value = true;
                    }
                    else if (timeLeft > hoverLeft && _teamScore.Hover.Score == 0) //covers accidental hover score change case (go back to previous state)
                    {
                        _state = gameState.Hover;
                        _teamControl.HoverLed.Value = true;
                        _teamControl.Obstacle1Led.Value = false;
                        _teamControl.Obstacle2Led.Value = false;
                        _teamControl.Motor.RollingStop();
                    }
                    break;
                case gameState.Platforms:
                    if (timeLeft <= platformLeft || (_teamScore.Platform1.Score > 0 && _teamScore.Platform2.Score > 0))
                    {
                        _state = gameState.Idle;
                        FinishGame();
                    }
                    break;
                default:
                    break;
            }
        }

        private void FinishGame()
        {
            _teamControl.Platform1Led.Value = false;
            _teamControl.Platform2Led.Value = false;
            //do something special?
        }

        public void Pause()
        {
            if (_state == gameState.Obstacles)
            {
                _teamControl.Motor.Pause();
            }
        }

        public void Continue()
        {
            if (_state == gameState.Obstacles)
            {
                _teamControl.Motor.Continue();
            }
        }

        public void Reset()
        {
            _state = gameState.Idle;
            //Note that TeamControl.Reset() resets motors and lights itself.
        }

        //Toggles pre-start blinking on/off for start leds if game hasn't started yet.
        public void PreStart()
        {
            if (_state == gameState.Idle)
            {
                _state = gameState.PreStart;
                _blinkTimer.Start();
            }
            else if (_state == gameState.PreStart)
            {
                _blinkTimer.Stop();
                _teamControl.StartLed.Value = false;
                _state = gameState.Start;
            }
        }

        private void OnBlinkElapsed(object sender, ElapsedEventArgs e)
        {
            _teamControl.StartLed.Toggle();
            if (_state != gameState.PreStart) //This should only happen if start button is pressed during prestart
            {
                _teamControl.StartLed.Value = true; 
                _blinkTimer.Stop();
            }
        }
    }
}
