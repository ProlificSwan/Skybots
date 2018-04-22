using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ScoreModel Hover { get; } = new ScoreModel();
        public ScoreModel Platform1 { get; } = new ScoreModel();
        public ScoreModel Platform2 { get; } = new ScoreModel();
        public ScoreModel Obstacle1 { get; } = new ScoreModel();
        public ScoreModel Obstacle2 { get; } = new ScoreModel();

        public uint TotalScore => Hover.Score + Platform1.Score + Platform2.Score + Obstacle1.Score + Obstacle2.Score;
    }
}
