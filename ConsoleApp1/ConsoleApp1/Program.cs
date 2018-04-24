using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solid.Arduino;
using Solid.Arduino.Firmata;

namespace ConsoleApp1

{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayPinValues();
            //DisplayPortCapabilities();
            Console.WriteLine("Press a key");
            Console.ReadKey(true);
        }

        private static void DisplayPortCapabilities()
        {
            using (var session = new ArduinoSession(new EnhancedSerialConnection("COM3", SerialBaudRate.Bps_57600)))
            {
                BoardCapability cap = session.GetBoardCapability();
                BoardAnalogMapping ana = session.GetBoardAnalogMapping(); 
                Console.WriteLine();
                Console.WriteLine("Board Capability:");

                foreach (var pin in cap.Pins)
                {
                    Console.WriteLine("Pin {0}: Input: {1}, Output: {2}, Analog: {3}, Analog-Res: {4}, PWM: {5}, PWM-Res: {6}, Servo: {7}, Servo-Res: {8}, Serial: {9}, Encoder: {10}, Input-pullup: {11}",
                        pin.PinNumber,
                        pin.DigitalInput,
                        pin.DigitalOutput,
                        pin.Analog,
                        pin.AnalogResolution,
                        pin.Pwm,
                        pin.PwmResolution,
                        pin.Servo,
                        pin.ServoResolution,
                        pin.Serial,
                        pin.Encoder,
                        pin.InputPullup);
                }

                foreach (var pin in ana.PinMappings)
                {
                    Console.WriteLine("Pin {0}: Channel {1}",
                        pin.PinNumber,
                        pin.Channel);
                }
            }
        }

        private static void DisplayPinValues()
        {
            using (var session = new ArduinoSession(new EnhancedSerialConnection("COM3", SerialBaudRate.Bps_57600)))
            {
                PinState ps;
                session.SetDigitalPinMode(13, PinMode.DigitalOutput);
                session.SetDigitalPin(13, true);
                session.SetAnalogReportMode(0, true);
                for (int i = 2; i < 70; i++)
                {
                    ps = session.GetPinState(i);
                    Console.WriteLine("Pin {0}: Value: {1}, Mode: {2}",
                        ps.PinNumber,
                        ps.Value,
                        ps.Mode);
                }
                Console.ReadKey(true);
                session.SetDigitalPin(13, false);
                session.SetAnalogReportMode(0, false);
            }
        }
    }
}