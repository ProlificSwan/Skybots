﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Timers;
using Solid.Arduino;
using Solid.Arduino.Firmata;
using Robotics.GUI.Helpers;

namespace Robotics.GUI.Model
{
  class ArduinoModel : BaseModel
  {
    private ArduinoSession _session;
    private ISerialConnection _connection;
    private bool _comOK = true;
    private Timer _keepAliveTimer;
    private LedModel _keepAliveLed = new LedModel(Constants.keepAliveLed);
    private TeamDataModel _teamData1;
    private TeamDataModel _teamData2;

    public ArduinoModel(TeamDataModel teamData1, TeamDataModel teamData2)
    {
      SetConnection();
      if (_comOK)
      {
        _session = new ArduinoSession(_connection);
        _keepAliveTimer = new Timer(Constants.keepAliveInterval) { AutoReset = true };
        _keepAliveTimer.Elapsed += _keepAliveTimer_Elapsed;
        _keepAliveTimer.Start();
        //_session.SetSamplingInterval(400);
        _session.SetDigitalPinMode(12, PinMode.InputPullup); //change in future        
        //_session.SetDigitalPinMode(35, PinMode.InputPullup);        
        var tracker = _session.CreateDigitalStateMonitor(Constants.limitSwitchPort);        
        teamData1.TeamControl.BackLimSwitch.Subscribe(tracker);
        teamData1.TeamControl.FrontLimSwitch.Subscribe(tracker);
        teamData2.TeamControl.BackLimSwitch.Subscribe(tracker);
        teamData2.TeamControl.FrontLimSwitch.Subscribe(tracker);

        //use code below to enable event managed pin updates
        //_session.SetDigitalReportMode(1, true); 
        //_session.DigitalStateReceived += DigitalStateReceivedHandler;
      }

      //Subscribe to controllable data changes
      //Planned: publish sensor updates using TeamSensor object in teamData
      _teamData1 = teamData1;
      _teamData1.TeamControl.HoverLed.PropertyChanged += OnLedChanged;
      _teamData1.TeamControl.Obstacle1Led.PropertyChanged += OnLedChanged;
      _teamData1.TeamControl.Obstacle2Led.PropertyChanged += OnLedChanged;
      _teamData1.TeamControl.Platform1Led.PropertyChanged += OnLedChanged;
      _teamData1.TeamControl.Platform2Led.PropertyChanged += OnLedChanged;
      _teamData1.TeamControl.StartLed.PropertyChanged += OnLedChanged;
      _teamData1.TeamControl.Motor.PropertyChanged += OnMotorChanged;

      _teamData2 = teamData2;
      _teamData2.TeamControl.HoverLed.PropertyChanged += OnLedChanged;
      _teamData2.TeamControl.Obstacle1Led.PropertyChanged += OnLedChanged;
      _teamData2.TeamControl.Obstacle2Led.PropertyChanged += OnLedChanged;
      _teamData2.TeamControl.Platform1Led.PropertyChanged += OnLedChanged;
      _teamData2.TeamControl.Platform2Led.PropertyChanged += OnLedChanged;
      _teamData2.TeamControl.StartLed.PropertyChanged += OnLedChanged;
      _teamData2.TeamControl.Motor.PropertyChanged += OnMotorChanged;
    }

    private void OnLedChanged(object sender, PropertyChangedEventArgs e)
    {
      LedModel led = (LedModel)sender;
      SetLed(led);
    }

    private void OnMotorChanged(object sender, PropertyChangedEventArgs e)
    {
      MotorModel motor = (MotorModel)sender;
      SetMotor(motor);
    }

    private void DigitalStateReceivedHandler(Object sender, EventArgs e)
    {
      FirmataEventArgs<DigitalPortState> theState = (FirmataEventArgs<DigitalPortState>)e;
      Console.WriteLine(theState.Value.Port);
      Console.WriteLine(theState.Value.Pins);
    }

    private void _keepAliveTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      //Console.WriteLine(_session.GetPinState(12).Value);
      ToggleLed(_keepAliveLed);
    }

    public bool ComOK
    {
        get
        {
            return _comOK;
        }
    }

    //Commands Arduino to toggle pin state
    public void ToggleLed(LedModel led)
    {
        if (_comOK && led.PinNumber >= 0)
        {
            led.Value = !led.Value;
            try
            {
                _session.SetDigitalPin(led.PinNumber, led.Value);
            }
            catch
            {
                _comOK = false;
                led.Value = false;
            }
        }
    }

    //Forces LedModel and Arduino to change physical led state to specified value.
    //Returns either commanded state confirmation or false if communication is bad.
    public bool SetLed(LedModel led, bool value)
    {
        if (_comOK && led.PinNumber >= 0)
        {
            led.Value = value;
            try
            {
                _session.SetDigitalPin(led.PinNumber, led.Value);
            }
            catch
            {
                _comOK = false;
                led.Value = false;
            }
        }
        return (_comOK && led.Value);
    }

    //Commands Arduino to change physical led state to specified value.
    //Returns either commanded state confirmation or false if communication is bad.
    public bool SetLed(LedModel led)
    {
        if (_comOK && led.PinNumber >= 0)
        {
            try
            {
                _session.SetDigitalPin(led.PinNumber, led.Value);
            }
            catch
            {
                _comOK = false;
                led.Value = false;
            }
        }
        return (_comOK && led.Value);
    }

    //Commands Arduino to change physical motor state to match current motor state values.
    //Returns false if communication or motor pins are bad.
    public bool SetMotor(MotorModel motor)
    {
        bool success = false;
        if (_comOK && motor.PinNumber1 >= 0 && motor.PinNumber2 >= 0)
        {
            try
            {
                success = true;
                _session.SetDigitalPin(motor.PinNumber1, motor.In1);
                _session.SetDigitalPin(motor.PinNumber2, motor.In2);
            }
            catch
            {
                success = false;
                _comOK = false;
                motor.In1 = false;
                motor.In2 = false;
            }
        }
        return success;
    }

    public void CloseConnection()
    {
        if (_connection != null)
        {
            _keepAliveTimer.Stop();
            _comOK = false;
            _connection.Close();
        }
    }

    private void SetConnection()
    {
        try
        {
            _connection = EnhancedSerialConnection.Find();
            if (_connection == null)
            {
                _comOK = false;
            }
            else
            {
                _comOK = true;
            }
                
        }
        catch
        {
            _comOK = false;
            _connection.Close();
        }
    }

    /*//For setting the connection manually.
    //Example: setExactConnection("COM2", SerialBaudRate.Bps_57600);
    private void SetExactConnection(string COM_Port, SerialBaudRate baudRate)
    {
        CloseConnection(); //close any existing connection

        try
        {
            _connection = new EnhancedSerialConnection(COM_Port, baudRate);
            if (_connection == null)
            {
                _comOK = false;
            }
            else
            {
                _comOK = true;
            }

        }
        catch
        {
            _comOK = false;
            _connection.Close();
        }
    }*/

    public void Reconnect()
    {
        if (_comOK)
        {
            _keepAliveTimer.Stop();
            _session.Clear();
            _keepAliveTimer.Start();
        }
        else
        {
            SetConnection();
            if (_comOK)
            {
                _session = new ArduinoSession(_connection);
                _keepAliveTimer.Start();
            }
        }
    }

    public void Reset()
    {
        _session.ResetBoard();
    }
    }
}
