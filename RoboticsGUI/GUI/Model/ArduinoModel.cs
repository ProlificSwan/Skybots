using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solid.Arduino;
using Solid.Arduino.Firmata;

namespace Robotics.GUI.Model
{
    public class ArduinoModel: BaseModel
    {
        private ArduinoSession _session;
        private ISerialConnection _connection;
        private bool _comOK = true;
        private Led RedLed = new Led(13);

        public ArduinoModel()
        {
            //ISerialConnection connection = GetConnection();

            //TODO - implement proper exception or handling for when connection not found.
            SetConnection();
            if (_comOK)
            {
                _session = new ArduinoSession(_connection);
            }
        }

        public void SetRedLed(bool value)
        {
            if (_comOK)
            {
                RedLed.value = value;
                _session.SetDigitalPin(RedLed.pinNumber, RedLed.value);
            }
        }

        public bool GetRedLed()
        {
            return RedLed.value;
        }

        public void CloseConnection()
        {
            if (_connection != null)
            {
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



        private void Reconnect()
        {
            if (_comOK)
            {
                _session.Clear();
            }
            else
            {
                SetConnection();
                if (_comOK)
                {
                    _session = new ArduinoSession(_connection);
                }
            }
        }

        private void Reset()
        {
            _session.ResetBoard();
        }
    }

    //Stores LED state and Arduino pin number
    class Led
    {
        public Int16 pinNumber;
        public bool value;

        public Led()
        {
            this.pinNumber = -1;
            this.value = false;
        }

        public Led(short pinNumber, bool value)
        {
            this.pinNumber = pinNumber;
            this.value = value;
        }

        public Led(short pinNumber)
        {
            this.pinNumber = pinNumber;
            this.value = false;
        }

        public override bool Equals(object obj)
        {
            var led = obj as Led;
            return led != null &&
                   pinNumber == led.pinNumber &&
                   value == led.value;
        }

        public override int GetHashCode()
        {
            var hashCode = 1549015223;
            hashCode = hashCode * -1521134295 + pinNumber.GetHashCode();
            hashCode = hashCode * -1521134295 + value.GetHashCode();
            return hashCode;
        }
    }
}
