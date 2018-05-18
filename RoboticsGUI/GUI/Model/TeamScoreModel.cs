using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robotics.GUI.Helpers;

namespace Robotics.GUI.Model
{
    internal class TeamScoreModel : BaseModel
    {
        public TeamScoreModel()
        {
            Hover.PropertyChanged += OnScoreChanged;
            Platform1.PropertyChanged += OnScoreChanged;
            Platform2.PropertyChanged += OnScoreChanged;
            Obstacle1.PropertyChanged += OnScoreChanged;
            Obstacle2.PropertyChanged += OnScoreChanged;
        }

        private void OnScoreChanged(object sender, PropertyChangedEventArgs e) => OnPropertyChanged(nameof(TotalScore));

        public ScoreModel Hover { get; } = new ScoreModel(Constants.hoverInc);
        public ScoreModel Platform1 { get; } = new ScoreModel(Constants.plat1Inc); //High platform
        public ScoreModel Platform2 { get; } = new ScoreModel(Constants.plat2Inc); //Low platform
        public ScoreModel Obstacle1 { get; } = new ScoreModel(Constants.obs1Inc); //Moving Obstacle
        public ScoreModel Obstacle2 { get; } = new ScoreModel(Constants.obs2Inc); //Fixed Obstacle

        public uint TotalScore => Hover.Score + Platform1.Score + Platform2.Score + Obstacle1.Score + Obstacle2.Score;

        public void Reset()
        {
            Hover.Reset();
            Platform1.Reset();
            Platform2.Reset();
            Obstacle1.Reset();
            Obstacle2.Reset();
        }
    }
}
