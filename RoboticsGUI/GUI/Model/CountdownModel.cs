using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotics.GUI.Model
{
    class CountdownModel : BaseModel, IDisposable
    {
        public CountdownModel(TimeSpan timeout)
        {
            Timeout = timeout;
            _stopwatch.PropertyChanged += OnStopwatchChanged;
        }

        private TimeSpan _timeout;

        private StopwatchModel _stopwatch = new StopwatchModel();
        
        public TimeSpan Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                SetProperty(ref _timeout, value);
            }
        }

        public TimeSpan TimeRemaining
        {
            get
            {
                var time = Timeout - _stopwatch.CurrentElapsedTime;
                if (time.CompareTo(TimeSpan.Zero) < 0) time = TimeSpan.Zero;                
                return time;
            }
            set
            {
                Timeout = value;
                OnPropertyChanged();
            }
        }

        public bool IsRunning => _stopwatch.IsRunning;

        public void Start() => _stopwatch.Start();

        public void Stop() => _stopwatch.Stop();

        public void Reset() => _stopwatch.Reset();

        public void Toggle() => _stopwatch.Toggle();
        
        private void OnStopwatchChanged(object sender, PropertyChangedEventArgs e)
        {
            var time = Timeout - _stopwatch.CurrentElapsedTime;
            if (time.CompareTo(TimeSpan.Zero) < 0) _stopwatch.Stop();
            OnPropertyChanged(nameof(TimeRemaining));
        }

        #region IDisposable Support

        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                _stopwatch.Dispose();
            }

            _disposedValue = true;
        }

        // This code added to correctly implement the disposable pattern.
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        public void Dispose() => Dispose(true);

        #endregion
    }
}
