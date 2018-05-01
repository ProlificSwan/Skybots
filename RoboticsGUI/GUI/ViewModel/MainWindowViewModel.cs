using System;
using System.ComponentModel;
using Robotics.GUI.Model;
using Robotics.GUI.Helpers;

namespace Robotics.GUI.ViewModel
{
    internal class MainWindowViewModel : BaseViewModel, IDisposable
    {
        RelayCommand _cmdStart = null;
        RelayCommand _cmdStop = null;
        RelayCommand _cmdReset = null;

        RelayCommand _T1HoverScoreIncr = null;
        RelayCommand _T1HoverScoreDecr = null;
        RelayCommand _T1Plat1ScoreIncr = null;
        RelayCommand _T1Plat1ScoreDecr = null;
        RelayCommand _T1Plat2ScoreIncr = null;
        RelayCommand _T1Plat2ScoreDecr = null;
        RelayCommand _T1Obs1ScoreIncr = null;
        RelayCommand _T1Obs1ScoreDecr = null;
        RelayCommand _T1Obs2ScoreIncr = null;
        RelayCommand _T1Obs2ScoreDecr = null;

        RelayCommand _T2HoverScoreIncr = null;
        RelayCommand _T2HoverScoreDecr = null;
        RelayCommand _T2Plat1ScoreIncr = null;
        RelayCommand _T2Plat1ScoreDecr = null;
        RelayCommand _T2Plat2ScoreIncr = null;
        RelayCommand _T2Plat2ScoreDecr = null;
        RelayCommand _T2Obs1ScoreIncr = null;
        RelayCommand _T2Obs1ScoreDecr = null;
        RelayCommand _T2Obs2ScoreIncr = null;
        RelayCommand _T2Obs2ScoreDecr = null;

        public CountdownModel Countdown { get; } = new CountdownModel(TimeSpan.FromSeconds(150));

        public TeamDataModel Team1 { get; } = new TeamDataModel("Team 1");
        public TeamDataModel Team2 { get; } = new TeamDataModel("Team 2");

        public RelayCommand StartCommand => _cmdStart ?? (_cmdStart = new RelayCommand(execute => Countdown.Start(), canExecute => { return !Countdown.IsRunning; }));
        public RelayCommand StopCommand => _cmdStop ?? (_cmdStop = new RelayCommand(execute => Countdown.Stop(), canExecute => { return Countdown.IsRunning; }));
        public RelayCommand ResetCommand => _cmdReset ?? (_cmdReset = new RelayCommand(execute => Countdown.Reset()));

        public RelayCommand T1HoverIncr => _T1HoverScoreIncr ?? (_T1HoverScoreIncr = new RelayCommand(execute => { Team1.TeamScore.Hover.Increment(); }, canExecute => { return !Team1.TeamScoringMethod.Hover.IsEnabled; }));
        public RelayCommand T1HoverDecr => _T1HoverScoreDecr ?? (_T1HoverScoreDecr = new RelayCommand(execute => { Team1.TeamScore.Hover.Decrement(); }, canExecute => { return !Team1.TeamScoringMethod.Hover.IsEnabled; }));
        public RelayCommand T1Plat1Incr => _T1Plat1ScoreIncr ?? (_T1Plat1ScoreIncr = new RelayCommand(execute => { Team1.TeamScore.Platform1.Increment(); }, canExecute => { return !Team1.TeamScoringMethod.Platform1.IsEnabled; }));
        public RelayCommand T1Plat1Decr => _T1Plat1ScoreDecr ?? (_T1Plat1ScoreDecr = new RelayCommand(execute => { Team1.TeamScore.Platform1.Decrement(); }, canExecute => { return !Team1.TeamScoringMethod.Platform1.IsEnabled; }));
        public RelayCommand T1Plat2Incr => _T1Plat2ScoreIncr ?? (_T1Plat2ScoreIncr = new RelayCommand(execute => { Team1.TeamScore.Platform2.Increment(); }, canExecute => { return !Team1.TeamScoringMethod.Platform2.IsEnabled; }));
        public RelayCommand T1Plat2Decr => _T1Plat2ScoreDecr ?? (_T1Plat2ScoreDecr = new RelayCommand(execute => { Team1.TeamScore.Platform2.Decrement(); }, canExecute => { return !Team1.TeamScoringMethod.Platform2.IsEnabled; }));
        public RelayCommand T1Obs1Incr => _T1Obs1ScoreIncr ?? (_T1Obs1ScoreIncr = new RelayCommand(execute => { Team1.TeamScore.Obstacle1.Increment(); }, canExecute => { return !Team1.TeamScoringMethod.Obstacle1.IsEnabled; }));
        public RelayCommand T1Obs1Decr => _T1Obs1ScoreDecr ?? (_T1Obs1ScoreDecr = new RelayCommand(execute => { Team1.TeamScore.Obstacle1.Decrement(); }, canExecute => { return !Team1.TeamScoringMethod.Obstacle1.IsEnabled; }));
        public RelayCommand T1Obs2Incr => _T1Obs2ScoreIncr ?? (_T1Obs2ScoreIncr = new RelayCommand(execute => { Team1.TeamScore.Obstacle2.Increment(); }, canExecute => { return !Team1.TeamScoringMethod.Obstacle2.IsEnabled; }));
        public RelayCommand T1Obs2Decr => _T1Obs2ScoreDecr ?? (_T1Obs2ScoreDecr = new RelayCommand(execute => { Team1.TeamScore.Obstacle2.Decrement(); }, canExecute => { return !Team1.TeamScoringMethod.Obstacle2.IsEnabled; }));

        public RelayCommand T2HoverIncr => _T2HoverScoreIncr ?? (_T2HoverScoreIncr = new RelayCommand(execute => { Team2.TeamScore.Hover.Increment(); }, canExecute => { return !Team2.TeamScoringMethod.Hover.IsEnabled; }));
        public RelayCommand T2HoverDecr => _T2HoverScoreDecr ?? (_T2HoverScoreDecr = new RelayCommand(execute => { Team2.TeamScore.Hover.Decrement(); }, canExecute => { return !Team2.TeamScoringMethod.Hover.IsEnabled; }));
        public RelayCommand T2Plat1Incr => _T2Plat1ScoreIncr ?? (_T2Plat1ScoreIncr = new RelayCommand(execute => { Team2.TeamScore.Platform1.Increment(); }, canExecute => { return !Team2.TeamScoringMethod.Platform1.IsEnabled; }));
        public RelayCommand T2Plat1Decr => _T2Plat1ScoreDecr ?? (_T2Plat1ScoreDecr = new RelayCommand(execute => { Team2.TeamScore.Platform1.Decrement(); }, canExecute => { return !Team2.TeamScoringMethod.Platform1.IsEnabled; }));
        public RelayCommand T2Plat2Incr => _T2Plat2ScoreIncr ?? (_T2Plat2ScoreIncr = new RelayCommand(execute => { Team2.TeamScore.Platform2.Increment(); }, canExecute => { return !Team2.TeamScoringMethod.Platform2.IsEnabled; }));
        public RelayCommand T2Plat2Decr => _T2Plat2ScoreDecr ?? (_T2Plat2ScoreDecr = new RelayCommand(execute => { Team2.TeamScore.Platform2.Decrement(); }, canExecute => { return !Team2.TeamScoringMethod.Platform2.IsEnabled; }));
        public RelayCommand T2Obs1Incr => _T2Obs1ScoreIncr ?? (_T2Obs1ScoreIncr = new RelayCommand(execute => { Team2.TeamScore.Obstacle1.Increment(); }, canExecute => { return !Team2.TeamScoringMethod.Obstacle1.IsEnabled; }));
        public RelayCommand T2Obs1Decr => _T2Obs1ScoreDecr ?? (_T2Obs1ScoreDecr = new RelayCommand(execute => { Team2.TeamScore.Obstacle1.Decrement(); }, canExecute => { return !Team2.TeamScoringMethod.Obstacle1.IsEnabled; }));
        public RelayCommand T2Obs2Incr => _T2Obs2ScoreIncr ?? (_T2Obs2ScoreIncr = new RelayCommand(execute => { Team2.TeamScore.Obstacle2.Increment(); }, canExecute => { return !Team2.TeamScoringMethod.Obstacle2.IsEnabled; }));
        public RelayCommand T2Obs2Decr => _T2Obs2ScoreDecr ?? (_T2Obs2ScoreDecr = new RelayCommand(execute => { Team2.TeamScore.Obstacle2.Decrement(); }, canExecute => { return !Team2.TeamScoringMethod.Obstacle2.IsEnabled; }));

        public override void OnClosing(object sender, CancelEventArgs e)
        {
            Dispose();
        }

        #region IDisposable Support

        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                Countdown.Dispose();
            }

            _disposedValue = true;
        }

        // This code added to correctly implement the disposable pattern.
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        public void Dispose() => Dispose(true);

        #endregion
    }
}