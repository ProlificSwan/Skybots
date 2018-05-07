using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotics.GUI.Model
{
    class TeamDataModel : BaseModel
    {
        public TeamDataModel(string name = "Team")
        {
            name = _name;
            TeamControl = new TeamControlModel(-1, -1, -1, -1, -1, -1, -1, -1);
            _teamGame = new TeamGameModel(TeamScore, TeamControl);
        }

        public TeamDataModel(string name, Int16 plat1, Int16 plat2, Int16 obs1, Int16 obs2, Int16 hover, Int16 start, Int16 motor1, Int16 motor2)
        {
            name = _name;
            TeamControl = new TeamControlModel(plat1, plat2, obs1, obs2, hover, start,motor1, motor2);
            _teamGame = new TeamGameModel(TeamScore, TeamControl);
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
        private TeamGameModel _teamGame;
    }
}
