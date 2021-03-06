This was used for the 2018 and 2019 Skybot challenges. Find more info here: https://skybotchallenge.us/

Competition field in testing: https://youtu.be/0A1lKNkgoMs
![Skybot Scoring GUI](https://github.com/ProlificSwan/Skybots/raw/master/skybotController.png)
![Skybot Field Test](https://github.com/ProlificSwan/Skybots/raw/master/competitionFieldPieces.png)

The code is designed for a system where a PC/laptop communicates over USB with an Arduino which then updates I/O pins to reflect sent by the PC. The Arduino is connected to a number of LEDs and motors and there are some future plans to also hook up sensors to automate scoring.  If you would like to re-use this code for your own project, you will likely want to replace the constants found in Skybots/RoboticsGUI/GUI/Helpers/Constants.cs

# Skybot Setup Instructions
1.  Install Arduino IDE: https://www.arduino.cc/en/Main/Software
2.  Download this repo
3.  Open Skybot\SkybotFirmata.ino in Arduino IDE.
4.  Select Arduino type, Board:Arduino/Genuino Mega or Mega 2560
5.  Select Arduino COM port (should be obvious)
6.  Upload to Arduino
7.  Wire LEDs and motors to Arduino using separate pinout (currently in project drive)
8.  Open RoboticsGUI\bin\Release\Robotics.exe . Arduino builtin LED (typically pin 13) should begin blinking as program starts. If the LED stops blinking or Manual Control Boxes gray out, communication has failed.
9.  Run the game! Details on buttons below:

### Game Controls
- **Start:** Start/Resume game. Motors and LEDs will be automatically commanded through game states (setup in TeamGameModel.cs)
- **Stop:** Pause game. Motors will stop, LEDs will not change. Press Start to unpause.
- **Reset:** Resets all game state and LED/motor states. Motors will attempt to move back to their starting position before stopping.
- **Pre-Start:** Blinks starting zone LEDs. Press again to turn off or press Start to continue to normal function.

### Manual Controls
**Motor Controls**

- **Left**: Manually tell motor to move "back" until force stop is pressed or some other command is received (game state change, test play).
- **Right**: Manually tell motor to move "forward" until force stop is pressed or some other command is received (game state change, test play).
- **Force Stop**: Immediately stop motors without regard for state or current position. Resets motor "start" position (see test play).
- **Test Play**: Test auto motor movement until Return Stop or force stop pressed. Will command forward for time in forward textbox and backward for time in backward textbox. "Start" position is usually considered as whatever position motor is currently in unless motor is paused (using Stop button next to timer).
- **Return Stop**: Continue motor movement until motor returns to "Start" position
- **Textboxes**: (adjacent motor direction arrows): Sets time adjacent to that direction for duration motor will move when game is running or test play is in progress. If updated while motors are running, values will not be set for motor until it returns to "Start" position.

**LED Controls**

- **Checkboxes**: Manually turn on and off LEDs.
- **Start LEDs Checkbox**: Manually turn on and off both start zone LEDs.
- **Radio Buttons**: Turn sensor control on and off (currently does nothing)

### Motor Calibration
Since motor movement has no feedback, we track the motor location using time. This will naturally not be entirely accurate. One direction of motor movement may take less time than the other, such that if the times are equal, the motor will slowly makes its way closer to the edge of the track and get stuck.

When setting up, verify which direction the right arrow (-->) moves the motor. This is the direction the motor will move when the obstacle game phase starts. Move the motors manually to their start position (end of the track opposite previously mentioned direction). Time the amount of time it takes the motor to reach each end of the track (forward and backward) in msecs. Enter those times in milliseconds in the textboxs for that motor. Run test play and return stop soon after to make the motors do one test loop before starting the game. Do this for the other team motor as well.

### Troubleshooting
- **_Problem_**: Lights or motors not coming on when manual buttons pressed? **_Solutions_**: Check pinouts, verify connections are correct. If some connections work and not others, likely have a loose connection. If it is believed to be a code problem, change pin numbers in Constants.cs file and rebuild in visual studio
- **_Problem_**: Controls are all grayed out and Arduino not blinking? **_Solutions_**: Check USB connection to Arduino. Ensure Arduino driver is installed. Ensure Arduino has been flashed with Arduino software. Test serial communication in the Arduino IDE using serial monitor (recommend 9600 baud for testing)--note that if serial monitor is running, program will need to be restarted after monitor is closed to attempt connection again. 
- **_Problem_**: Controls are all grayed out and Arduino **is** blinking? **_Solutions_**: Verify no other programs have active serial communication ports. Close those programs (Visual Studio designer can open a COM port on its own) and restart GUI. If all else fails, reset computer and try again.

---------------------------
