using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Solid.Arduino;
using Solid.Arduino.Firmata;

namespace Robotics.GUI.Model
{
    public class ArduinoModel: BaseModel
    {
        private ArduinoSession _session;
        private ISerialConnection _connection;
        private bool _comOK = true;
        //private LedModel RedLed = new LedModel(12);
        private Timer _keepAliveTimer;
        private const int _keepAliveInterval = 500; //in msec
        private LedModel _keepAliveLed = new LedModel(13); //pin 13 is a built-in Arduino pin.


        public ArduinoModel()
        {
            //ISerialConnection connection = GetConnection();

            SetConnection();
            if (_comOK)
            {
                _session = new ArduinoSession(_connection);
                _keepAliveTimer = new Timer(_keepAliveInterval);
                _keepAliveTimer.AutoReset = true;
                _keepAliveTimer.Elapsed += _keepAliveTimer_Elapsed;
                _keepAliveTimer.Start();
            }
        }

        private void _keepAliveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ToggleLed(_keepAliveLed);
        }

        public bool ComOK
        {
            get
            {
                return _comOK;
            }
        }

        //TODO add method of creating sensor listener objects for SensorModel or ScoreModel objects to consume for score updated by sensor

        //Commands Arduino to toggle pin state
        public void ToggleLed(LedModel led)
        {
            if (_comOK && led.PinNumber >= 0)
            {
                led.Value = !led.Value;
                //TODO add exception handling for random unplugs of serial (so user cannot crash program by unplugging arduino)
                _session.SetDigitalPin(led.PinNumber, led.Value);
            }
        }

        //Commands LedModel and Arduino to change physical led state to specified value.
        //Returns either commanded state confirmation or false if communication is bad.
        public bool SetLed(LedModel led, bool value)
        {
            if (_comOK && led.PinNumber >= 0)
            {
                led.Value = value;
                _session.SetDigitalPin(led.PinNumber, led.Value);
            }
            return (_comOK && led.Value);
        }

        //Commands Arduino to change physical led state to specified value.
        //Returns either commanded state confirmation or false if communication is bad.
        public bool SetLed(LedModel led)
        {
            if (_comOK && led.PinNumber >= 0)
            {
                _session.SetDigitalPin(led.PinNumber, led.Value);
            }
            return (_comOK && led.Value);
        }

        //Commands Arduino to change physical motor state to match current motor state values.
        //Returns false if communication or motor pins are bad.
        public bool SetMotor(MotorModel motor)
        {
            if (_comOK && motor.PinNumber1 >= 0 && motor.PinNumber2 >= 0)
            {
                _session.SetDigitalPin(motor.PinNumber1, motor.In1);
                _session.SetDigitalPin(motor.PinNumber2, motor.In2);
            }
            else
            {
                return false;
            }
            return true;
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
