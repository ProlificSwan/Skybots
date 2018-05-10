using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotics.GUI.Model
{
    class ScoreModel : BaseModel
    {
        public uint _score = 0;

        private uint _scoreDelta;

        public ScoreModel(uint scoreIncrement)
        {
            _scoreDelta = scoreIncrement;
        }


        /// <value>Gets the current score.</value>
        public uint Score
        {
            get
            {
                return _score;
            }
            set
            {
                SetProperty(ref _score, value);
            }
        }

        public uint Increment()
        {
            Score+= _scoreDelta;
            return Score;
        }

        public uint Decrement()
        {
            if (Score != 0) Score-= _scoreDelta;
            return Score;
        }

    }
}
