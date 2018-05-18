using System;
using System.ComponentModel;
using Robotics.GUI.Model;
using Robotics.GUI.Helpers;
using Robotics.Properties;

namespace Robotics.GUI.ViewModel
{
    internal class MainWindowViewModel : BaseViewModel, IDisposable
    {
        RelayCommand _cmdStart = null;
        RelayCommand _cmdStop = null;
        RelayCommand _cmdReset = null;
        RelayCommand _cmdPreStart = null;

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

        RelayCommand _T1MotorBack = null;
        RelayCommand _T1MotorFwd = null;
        RelayCommand _T1MotorStop = null;
        RelayCommand _T1MotorPause = null;
        RelayCommand _T1MotorReturn = null;
        RelayCommand _T1MotorPlay = null;


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

        RelayCommand _T2MotorBack = null;
        RelayCommand _T2MotorFwd = null;
        RelayCommand _T2MotorStop = null;
        RelayCommand _T2MotorPause = null;
        RelayCommand _T2MotorPlay = null;
        RelayCommand _T2MotorReturn = null;

        public MainWindowViewModel()
        {

        }

        public static CountdownModel Countdown { get; } = new CountdownModel(TimeSpan.FromSeconds(Constants.gameTime));

        public static TeamDataModel Team1 { get; } = new TeamDataModel("Red Alliance", Countdown, Constants.rplat1, Constants.rplat2, Constants.robs1, Constants.robs2, Constants.rhover,
            Constants.rstart, Constants.rmotor1, Constants.rmotor2, Properties.Settings.Default.RedMotorFwdTime, Properties.Settings.Default.RedMotorBackTime); //assumption: this is the red team
        public static TeamDataModel Team2 { get; } = new TeamDataModel("Blue Alliance", Countdown, Constants.bplat1, Constants.bplat2, Constants.bobs1, Constants.bobs2, Constants.bhover,
            Constants.bstart, Constants.bmotor1, Constants.bmotor2, Properties.Settings.Default.BlueMotorFwdTime, Properties.Settings.Default.BlueMotorBackTime); //assumption: this is the blue team
        //TODO Research: does this need to be public?
        public static ArduinoModel Arduino { get; } = new ArduinoModel(Team1, Team2);

        public bool StartLedUnionProperty
        {
            get
            {
                return Team1.TeamControl.StartLed.Value && Team2.TeamControl.StartLed.Value;
            }
            set
            {
                Team1.TeamControl.StartLed.Value = value;
                Team2.TeamControl.StartLed.Value = value;
            }
        }

        public RelayCommand StartCommand => _cmdStart ?? (_cmdStart = new RelayCommand(execute => {
            Countdown.Start();
            Team1.TeamGame.Continue();
            Team2.TeamGame.Continue();
        }, canExecute => { return !Countdown.IsRunning; }));
        public RelayCommand StopCommand => _cmdStop ?? (_cmdStop = new RelayCommand(execute => {
            Countdown.Stop();
            Team1.TeamGame.Pause();
            Team2.TeamControl.Motor.Pause();
        }, canExecute => { return Countdown.IsRunning; }));
        public RelayCommand ResetCommand => _cmdReset ?? (_cmdReset = new RelayCommand(execute => {
            Countdown.Reset();
            Team1.Reset();
            Team2.Reset();
            //TODO reset scores as well.
        }));
        public RelayCommand PreStartCommand => _cmdPreStart ?? (_cmdPreStart = new RelayCommand(execute => {
            Team1.TeamGame.PreStart();
            Team2.TeamGame.PreStart(); }, canExecute => { return Arduino.ComOK; }));

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
        public RelayCommand T1MotorBack => _T1MotorBack ?? (_T1MotorBack = new RelayCommand(execute => { Team1.TeamControl.Motor.Backward(); }, canExecute => { return Arduino.ComOK; }));
        public RelayCommand T1MotorFwd => _T1MotorFwd ?? (_T1MotorFwd = new RelayCommand(execute => { Team1.TeamControl.Motor.Forward(); }, canExecute => { return Arduino.ComOK; }));
        public RelayCommand T1MotorStop => _T1MotorStop ?? (_T1MotorStop = new RelayCommand(execute => { Team1.TeamControl.Motor.EmergencyStop(); }, canExecute => { return Arduino.ComOK; }));
        public RelayCommand T1MotorReturn => _T1MotorReturn ?? (_T1MotorReturn = new RelayCommand(execute => { Team1.TeamControl.Motor.RollingStop(); }, canExecute => { return Arduino.ComOK; }));
        public RelayCommand T1MotorPlay => _T1MotorPlay ?? (_T1MotorPlay = new RelayCommand(execute => { Team1.TeamControl.Motor.Play(); }, canExecute => { return Arduino.ComOK; }));
        public RelayCommand T1MotorPause => _T1MotorPause ?? (_T1MotorPause = new RelayCommand(execute => { Team1.TeamControl.Motor.Pause(); }, canExecute => { return Arduino.ComOK; }));

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
        public RelayCommand T2MotorBack => _T2MotorBack ?? (_T2MotorBack = new RelayCommand(execute => { Team2.TeamControl.Motor.Backward(); }, canExecute => { return Arduino.ComOK; }));
        public RelayCommand T2MotorFwd => _T2MotorFwd ?? (_T2MotorFwd = new RelayCommand(execute => { Team2.TeamControl.Motor.Forward(); }, canExecute => { return Arduino.ComOK; }));
        public RelayCommand T2MotorStop => _T2MotorStop ?? (_T2MotorStop = new RelayCommand(execute => { Team2.TeamControl.Motor.EmergencyStop(); }, canExecute => { return Arduino.ComOK; }));
        public RelayCommand T2MotorReturn => _T2MotorReturn ?? (_T2MotorReturn = new RelayCommand(execute => { Team2.TeamControl.Motor.RollingStop(); }, canExecute => { return Arduino.ComOK; }));
        public RelayCommand T2MotorPlay => _T2MotorPlay ?? (_T2MotorPlay = new RelayCommand(execute => { Team2.TeamControl.Motor.Play(); }, canExecute => { return Arduino.ComOK; }));
        public RelayCommand T2MotorPause => _T2MotorPause ?? (_T2MotorPause = new RelayCommand(execute => { Team2.TeamControl.Motor.Pause(); }, canExecute => { return Arduino.ComOK; }));

        public override void OnClosing(object sender, CancelEventArgs e)
        {
            Team1.Shutdown();
            Team2.Shutdown();
            Arduino.CloseConnection();
            UpdateSettings();
            Properties.Settings.Default.Save();
            Dispose();
        }

        private static void UpdateSettings()
        {
            Properties.Settings.Default.RedMotorFwdTime = Team1.TeamControl.Motor.ForwardTime;
            Properties.Settings.Default.RedMotorBackTime = Team1.TeamControl.Motor.BackwardTime;
            Properties.Settings.Default.BlueMotorFwdTime = Team2.TeamControl.Motor.ForwardTime;
            Properties.Settings.Default.BlueMotorBackTime = Team2.TeamControl.Motor.BackwardTime;
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