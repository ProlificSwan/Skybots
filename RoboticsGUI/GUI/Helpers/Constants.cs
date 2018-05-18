using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robotics.GUI.Helpers
{
    public static class Constants
    {
        //Default time motors spend moving in each direction.
        public const int defaultMotorFwdTime = 1000; //milliseconds.
        public const int defaultMotorBackTime = 1000; //milliseconds.

        //game time variables
        public const int gameTime = 150; //total game time in seconds - may change this to be configurable later
        //time variables below should indicate the time when that item must finish by. Order of states is covered in TeamGameModel.
        public const int startTime = 5000; //start time in msecs
        public const int hoverTime = 30000; //hover time in total elapsed msecs before absolute end
        public const int obstacleTime = 120000; //obstacle time in total elapsed msecs before absolute end
        public const int platformTime = 150000; //platform time

        //Scoring increments for the scoring areas.
        public const uint plat1Inc = 300; //high platform 
        public const uint plat2Inc = 200; //low platform
        public const uint obs1Inc = 20; // Moving obstacle
        public const uint obs2Inc = 10; // Fixed obstacle
        public const uint hoverInc = 50; // Hover

        //Red Team Control Pins
        public const short rplat1   = 22; //Platform 1
        public const short rplat2   = 24; //Platform 2
        public const short robs1    = 26; //Obstacle 1
        public const short robs2    = 28; //Obstacle 2
        public const short rhover   = 30; //Hover
        public const short rstart   = 32; //Start
        public const short rmotor1  = 2;  //Motor1
        public const short rmotor2  = 3;  //Motor2

        //Blue Team Control Pins
        public const short bplat1   = 23; //Platform 1
        public const short bplat2   = 25; //Platform 2
        public const short bobs1    = 27; //Obstacle 1
        public const short bobs2    = 29; //Obstacle 2
        public const short bhover   = 31; //Hover
        public const short bstart   = 33; //Start
        public const short bmotor1  = 4;  //Motor1
        public const short bmotor2  = 5;  //Motor2

        public const int preStartBlinkInterval = 75;
        public const short keepAliveLed = 13; //Pin 13 is the built in Arduino pin we are using to physically show communication is OK
        public const int keepAliveInterval = 500; //how often to blink LED in msec
    }
}
