using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotics.GUI.Model
{
    class TeamGameModel : BaseModel
    {
        public TeamGameModel(TeamScoreModel teamScore, TeamControlModel teamControl)
        {
            _teamScore = teamScore;
            _teamControl = teamControl;
            //TODO add stopwatch listening event or reference
        }

        private TeamScoreModel _teamScore;
        private TeamControlModel _teamControl;

        public enum gameState
        {
            Idle,
            StartHover,
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
        //TODO - make times configurable
        private const int startTime = 5000; //start time in msecs
        private const int hoverTime = 30000; //hover time in total elapsed msecs before absolute end
        private const int obstacleTime = 105000; //obstacle time in total elapsed msecs before absolute end
        private const int platformTime = 150000; //platform time

        //TODO ATTACH THIS TO ACTUAL EVENT, loop, or callback
        private void OnTimeElapsed(int time)
        {
            switch (_state)
            {
                case gameState.Idle:
                    if (time > 0 && time < hoverTime)
                    {
                        _state = gameState.StartHover;
                        _teamControl.StartLed.Value = true;
                        _teamControl.HoverLed.Value = true;
                    }
                    break;
                case gameState.StartHover:
                    if (time > startTime)
                    {
                        _state = gameState.Hover;
                        _teamControl.StartLed.Value = false;
                        //TODO add LEDModel event or propertyChanged event so that changing LED value causes arduino model to update physical lights.
                        
                    }

                    if (_teamScore.Hover.Score > 0)
                    {
                        _state = gameState.Obstacles;
                        _teamControl.StartLed.Value = false;
                        _teamControl.HoverLed.Value = false;
                    }
                    break;
                case gameState.Hover:
                    if (_teamScore.Hover.Score > 0 || time > hoverTime)
                    {
                        _state = gameState.Obstacles;
                        _teamControl.HoverLed.Value = false;
                        _teamControl.Obstacle1Led.Value = true;
                        _teamControl.Obstacle2Led.Value = true;
                        _teamControl.Motor.Forward();
                    }
                    break;
                case gameState.Obstacles:
                    if (time > obstacleTime)
                    {
                        _state = gameState.Platforms;
                        _teamControl.Obstacle1Led.Value = false;
                        _teamControl.Obstacle2Led.Value = false;
                        _teamControl.Motor.Stop();
                        _teamControl.Platform1Led.Value = true;
                        _teamControl.Platform2Led.Value = true;
                    }
                    break;
                case gameState.Platforms:
                    if (time > platformTime || (_teamScore.Platform1.Score > 0 && _teamScore.Platform2.Score > 0))
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

    }
}
