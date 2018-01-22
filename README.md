# NerfGun
Source code to an IOT project which controls a motion activated Nerf gun.

The overall goal of this project is to create an automated nerf gun turret which will trigger by motion detection. 

In its current state:
  The nerf gun must be directly wired to a raspberry pi and will fire on motion as well as fire 
  when a button is pressed on the test UI. The nerf gun runs on a timer which regularly checks the 
  motion sensor for any readings of motion and if so, triggers the motors to start firing. After 
  x amount of time the program will turn off the motors and go back to scanning the motion sensor.

Future plans:
  Since the absolute minimum requirements for this project have been met to deem it as a success,
  the next step is making it useful. 
  This will include setting up a camera to record or take a picture once the motion sensor is triggered.
  There is also a plan to collect data from the nerf gun including:
   - Time the nerf gun was activated
   - The amount of time the nerf gun was running during that incident
   - The amount of shots taken by the nerf gun
   - The amount of darts currently left inside the gun

  With the current set up it is posible to accomplish these tasks however a different model
  of Nerf gun may be used for future tests as the current is not very reliable or easy to
  control just by the way the motors work.
