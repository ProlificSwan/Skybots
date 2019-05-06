using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotics.GUI.Model
{
    class TeamDataModel : BaseModel
    {
        public TeamDataModel(string nameVal = "Team")
        {
            Name = nameVal;
            TeamControl = new TeamControlModel(-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,-1,-1);
            //_teamGame = new TeamGameModel(TeamScore, TeamControl);
        }

        public TeamDataModel(string nameVal, CountdownModel countdown, Int16 plat1, Int16 plat2, Int16 obs1, Int16 obs2, Int16 hover, 
          Int16 start, Int16 motor1, Int16 motor2, Int16 frontLim, Int16 backLim)
        {
            Name = nameVal;
            TeamControl = new TeamControlModel(plat1, plat2, obs1, obs2, hover, start,motor1, motor2,frontLim,backLim);
            TeamGame = new TeamGameModel(TeamScore, TeamControl, countdown);
        }

        public TeamDataModel(string nameVal, CountdownModel countdown, Int16 plat1, Int16 plat2, Int16 obs1, Int16 obs2, Int16 hover, 
          Int16 start, Int16 motor1, Int16 motor2, int fwdTime, int backTime, Int16 frontLim, Int16 backLim)
        {
            Name = nameVal;
            TeamControl = new TeamControlModel(plat1, plat2, obs1, obs2, hover, start, motor1, motor2, fwdTime, backTime, frontLim, backLim);
            TeamGame = new TeamGameModel(TeamScore, TeamControl, countdown);
        }

        private string _name = "Team";
        
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        public TeamScoreModel TeamScore
        {
            get;
        } = new TeamScoreModel();

        public TeamScoringMethodModel TeamScoringMethod {
            get;
        } = new TeamScoringMethodModel();

        public TeamSensorModel TeamSensorModel
        {
            get;
        } = new TeamSensorModel();

        public TeamControlModel TeamControl { get; }
        public TeamGameModel TeamGame { get; }

        //Resets all outputs, scores, and game state to idle values (motors will continue until back to start position)
        public void Reset()
        {
            TeamGame.Reset();
            TeamControl.Reset();
            TeamScore.Reset();  
        }

        public void Shutdown()
        {
            TeamGame.Reset();
            TeamControl.Shutdown();
        }
    }
}
