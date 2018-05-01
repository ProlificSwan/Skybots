using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Solid.Arduino;
using Solid.Arduino.Firmata;

/// <summary>
/// TODO - make it so this runs on startup using xaml, figure out how to get rid of the while loop
/// </summary>

namespace Robotics.ArduinoControl
{
    class SerialInterface
    {
        public SerialInterface()
        {        }

        static void Main()
        {
            var session = new ArduinoSession(new EnhancedSerialConnection("COM3", SerialBaudRate.Bps_57600));
            session.SetDigitalPinMode(13, PinMode.DigitalOutput);
            // Create an AutoResetEvent to signal the timeout threshold in the
            // timer callback has been reached.
            //var autoEvent = new AutoResetEvent(false);

            var statusChecker = new StatusChecker(session);

            // Create a timer that invokes CheckStatus after one second, 
            // and every 1/4 second thereafter.
            Console.WriteLine("{0:h:mm:ss.fff} Creating timer.\n",
                              DateTime.Now);
            var stateTimer = new Timer(statusChecker.CheckStatus,
                                       null, 0, 500);

            // When autoEvent signals, change the period to every half second.
            //autoEvent.WaitOne();
            //stateTimer.Change(0, 500);
            //Console.WriteLine("\nChanging period to .5 seconds.\n");
            //stateTimer.Dispose();
            while (true);
        }

        
    }

    class StatusChecker
    {
        private bool ledState;
        private ArduinoSession currentSession;

        public StatusChecker(ArduinoSession session)
        {
            currentSession = session;
            ledState = false;
        }

        // This method is called by the timer delegate.
        public void CheckStatus(Object stateInfo)
        {
            //AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            ledState = !ledState;
            currentSession.SetDigitalPin(13, ledState);
        }
    }
}
