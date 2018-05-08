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

        //TODO - get LED pin numbers from some standard constants file instead of hardcoding here.
        //Research: any issue with using static in this case? Most of the variables in this window should technically only be created once...
        public static TeamDataModel Team1 { get; } = new TeamDataModel("Team 1",22,24,26,28,30,32,2,3);
        public static TeamDataModel Team2 { get; } = new TeamDataModel("Team 2",23,25,27,29,31,33,2,3);

        //TODO Research: does this need to be public?
        public static ArduinoModel arduino = new ArduinoModel(Team1,Team2);        

        public RelayCommand StartCommand => _cmdStart ?? (_cmdStart = new RelayCommand(execute => Countdown.Start(), canExecute => { return !Countdown.IsRunning; }));
        public RelayCommand StopCommand => _cmdStop ?? (_cmdStop = new RelayCommand(execute => Countdown.Stop(), canExecute => { return Countdown.IsRunning; }));
        public RelayCommand ResetCommand => _cmdReset ?? (_cmdReset = new RelayCommand(execute => Countdown.Reset()));
       
        //T1 score and sensor control
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
        //T1 LED and motor control
        /*
        public RelayCommand T1HoverLed => _T1HoverLed ?? (_T1HoverLed = new RelayCommand(execute => { arduino.SetLed(Team1.TeamControl.HoverLed); }, canExecute => { return arduino.ComOK; }));
        public RelayCommand T1Plat1Led => _T1Plat1Led ?? (_T1Plat1Led = new RelayCommand(execute => { arduino.SetLed(Team1.TeamControl.Platform1Led); }, canExecute => { return arduino.ComOK; }));
        public RelayCommand T1Plat2Led => _T1Plat2Led ?? (_T1Plat1Led = new RelayCommand(execute => { arduino.SetLed(Team1.TeamControl.Platform2Led); }, canExecute => { return arduino.ComOK; }));
        public RelayCommand T1Obs1Led => _T1Obs1Led ?? (_T1Obs1Led = new RelayCommand(execute => { arduino.SetLed(Team1.TeamControl.Obstacle1Led); }, canExecute => { return arduino.ComOK; }));
        public RelayCommand T1Obs2Led => _T1Obs2Led ?? (_T1Obs1Led = new RelayCommand(execute => { arduino.SetLed(Team1.TeamControl.Obstacle2Led); }, canExecute => { return arduino.ComOK; }));
        public RelayCommand T1StartLed => _T1StartLed ?? (_T1StartLed = new RelayCommand(execute => { arduino.SetLed(Team1.TeamControl.StartLed); }, canExecute => { return arduino.ComOK; }));*/

        //T2 score and sensor control
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
        //T2 LED and motor control
        /*public RelayCommand T2HoverLed => _T2HoverLed ?? (_T2HoverLed = new RelayCommand(execute => { arduino.SetLed(Team2.TeamControl.HoverLed); }, canExecute => { return arduino.ComOK; }));
        public RelayCommand T2Plat1Led => _T2Plat1Led ?? (_T2Plat1Led = new RelayCommand(execute => { arduino.SetLed(Team2.TeamControl.Platform1Led); }, canExecute => { return arduino.ComOK; }));
        public RelayCommand T2Plat2Led => _T2Plat2Led ?? (_T2Plat1Led = new RelayCommand(execute => { arduino.SetLed(Team2.TeamControl.Platform2Led); }, canExecute => { return arduino.ComOK; }));
        public RelayCommand T2Obs1Led => _T2Obs1Led ?? (_T2Obs1Led = new RelayCommand(execute => { arduino.SetLed(Team2.TeamControl.Obstacle1Led); }, canExecute => { return arduino.ComOK; }));
        public RelayCommand T2Obs2Led => _T2Obs2Led ?? (_T2Obs1Led = new RelayCommand(execute => { arduino.SetLed(Team2.TeamControl.Obstacle2Led); }, canExecute => { return arduino.ComOK; }));
        public RelayCommand T2StartLed => _T2StartLed ?? (_T2StartLed = new RelayCommand(execute => { arduino.SetLed(Team2.TeamControl.StartLed); }, canExecute => { return arduino.ComOK; }));*/

        public override void OnClosing(object sender, CancelEventArgs e)
        {
            arduino.CloseConnection();
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