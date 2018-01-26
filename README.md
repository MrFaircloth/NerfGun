# NerfGun
Source code to an IOT project which controls a motion activated Nerf gun.

The project was first developed in a Systems programming course at CCSU. It involves a Nerf gun which will fire after detecting motion. 

There were four main components which helped accomplish this task:

	- A Nerf gun was dismantled and modified by removing any safety features that would prevent the gun from firing. This included removing the check for the trigger to be pulled in order for the motors to spin. There were two triggers which when pulled would activate the gun's motors. The buttons attached to these triggers were removed and the wire connecting the motor to he batteries was split and extended to be outside the gun for easy access. These wires were then connected to a relay which when receiving a signal, would connect the wires, triggering the motors.

	- A motion sensor was used to send a signal when motion was detected. This was placed inside a tube in order to make it only trigger when motion was detected in front of the gun.

	- In order to stop the gun from firing, we needed a signal that showed the target was no longer there. Using the motion sensor was not an option as once the gun was firing, it would always read high since the darts would trigger the sensor. So instead we made a pressure plate using cardboard and aluminum foil. It worked similar to a switch where two strips of aluminum foil, which had wires coming off each, were held at roughly a centimeter apart and when pressed together would complete the the circuit. We used this by placing it behind where the target would be, and once hit, send a signal that there is no longer something there. If there was, it would be getting hit by the darts.

	- Lastly, we used a raspberry pi as a way to control all these components by writing a program in C and using a library called WiringPi to access the GPIO pins which all these components would be connected to.


The original project was written in C for the course but since has been updated to C#. The pressure plate was also removed and instead the gun would fire for one second then scan again to see if there is still motion. 

The next step:
	A current problem is because of inconsistency of the gun, there is no way to program the gun to fire once and only once. What this means is that it cannot be predicted when the gun will start firing once the motors start spinning so there are cases where it will fire twice before the timer ends or not fire at all. The plan is to drill a hole through the barrel of the gun and place a LED on once side and a light sensor on the other. This will be used to check for a break in the light and register as a dart passing by. This will remove the need for the timer and the program can keep the motors running until it senses a dart has been fired.
