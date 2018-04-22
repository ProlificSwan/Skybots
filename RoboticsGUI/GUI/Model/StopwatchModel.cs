using System;
using System.Timers;
using Robotics.GUI.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Robotics.GUI.Model
{
    internal class StopwatchModel : BaseModel, IDisposable
    {
        public StopwatchModel()
        {
            _timer = new Timer(1);
            _timer.AutoReset = true;
            _timer.Elapsed += OnTimerElapsed;
        }
        
        private Timer _timer;
        private bool _isRunning = false;

        public DateTime _startTime = DateTime.MinValue;
        public TimeSpan _currentElapsedTime = TimeSpan.Zero;
        public TimeSpan _totalElapsedTime = TimeSpan.Zero;

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            private set
            {
                SetProperty(ref _isRunning, value);
            }
        }

        public DateTime StartTime
        {
            get
            {
                return _startTime;
            }
            private set
            {
                SetProperty(ref _startTime, value);
            }
        }

        public TimeSpan CurrentElapsedTime
        {
            get
            {
                return _currentElapsedTime;
            }
            private set
            {
                SetProperty(ref _currentElapsedTime, value);
                OnPropertyChanged(nameof(TotalElapsedTime));
            }
        }

        public TimeSpan TotalElapsedTime
        {
            get
            {
                return _totalElapsedTime;
            }
            private set
            {
                SetProperty(ref _totalElapsedTime, value);
            }
        }
        
        /// <summary>
        /// Handle the Timer's Tick event
        /// </summary>
        /// <param name="sender">System.Windows.Forms.Timer instance</param>
        /// <param name="e">EventArgs object</param>
        private void OnTimerElapsed(object sender, EventArgs e)
        {
            // The current elapsed time is the time since the start button
            // was clicked, plus the total time elapsed since the last reset
            CurrentElapsedTime = (DateTime.Now - StartTime) + TotalElapsedTime;
        }

        public void Start()
        {
            // Set the start time to Now
            StartTime = DateTime.Now;

            // Store the total elapsed time so far
            TotalElapsedTime = CurrentElapsedTime;

            _timer.Start();
            _isRunning = true;
        }

        public void Stop()
        {
            _timer.Stop();
            _isRunning = false;
        }

        public void Reset()
        {
            // Stop and reset the timer if it was running
            _timer.Stop();
            _isRunning = false;

            // Reset the elapsed time TimeSpan objects
            TotalElapsedTime = TimeSpan.Zero;
            CurrentElapsedTime = TimeSpan.Zero;
        }

        public void Toggle()
        {
            // If the timer isn't already running
            if (!_isRunning)
            {
                Start();
            }
            else // If the timer is already running
            {
                Stop();
            }
        }

        #region IDisposable Support

        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                _timer.Dispose();
            }

            _disposedValue = true;
        }

        // This code added to correctly implement the disposable pattern.
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        public void Dispose() => Dispose(true);

        #endregion
    }
}