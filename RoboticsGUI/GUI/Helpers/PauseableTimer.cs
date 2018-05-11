using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Robotics.GUI.Helpers
{
    //A pauseable millisecond timer.
    //Start the timer by setting its Interval (msecs) property and Enabled = true, pause it with Enabled = false. Alternatively use Start() and Pause()
    //Use the Reset() method to clear the residual time.
    public class PauseableTimer
    {
        public event EventHandler Elapsed;
        private Timer mTimer;
        private bool mEnabled;
        private int mResidue;
        private DateTime mStart;
        private object mLocker;

        public PauseableTimer()
        {
            mTimer = new Timer(callback);
            mLocker = new object();
        }

        public PauseableTimer(int interval)
        {
            mTimer = new Timer(callback);
            mLocker = new object();
            Interval = interval;
        }

        public int Interval { get; set; }

        public bool Enabled
        {
            get { return mEnabled; }
            set
            {
                lock (mLocker)
                {
                    if (value == mEnabled) return;
                    mEnabled = value;
                    if (value)
                    {
                        mStart = DateTime.Now;
                        mTimer.Change(Interval - mResidue, Interval);
                    }
                    else
                    {
                        mTimer.Change(0, 0);
                        mResidue = Math.Min(Interval, (int)(DateTime.Now - mStart).TotalMilliseconds);
                    }
                }
            }
        }

        public void Start()
        {
            Enabled = true;
        }

        public void Pause()
        {
            Enabled = false;
        }

        public void Continue()
        {
            Enabled = true;
        }

        public void Reset()
        {
            lock (mLocker)
            {
                Enabled = false;
                mResidue = 0;
            }
        }

        private void callback(object dummy)
        {
            bool fire;
            lock (mLocker)
            {
                mStart = DateTime.Now;
                fire = Elapsed != null && mEnabled;
            }
            try
            {  // System.Timers.Timer.Elapsed swallows exceptions, bah
                if (fire) Elapsed(this, EventArgs.Empty);
            }
            catch { }
        }
    }
}
