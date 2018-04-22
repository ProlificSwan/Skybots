using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotics.GUI.Model
{
    class TeamScoringMethodModel : BaseModel
    {
        public ScoringMethodModel Hover { get; } = new ScoringMethodModel();
        public ScoringMethodModel Platform1 { get; } = new ScoringMethodModel();
        public ScoringMethodModel Platform2 { get; } = new ScoringMethodModel();
        public ScoringMethodModel Obstacle1 { get; } = new ScoringMethodModel();
        public ScoringMethodModel Obstacle2 { get; } = new ScoringMethodModel();
    }
}
