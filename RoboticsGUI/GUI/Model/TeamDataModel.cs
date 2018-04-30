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
    }
}
