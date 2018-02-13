# Playr SLA 3D Printer Host Software Windows

# The Printer I built

This is 3D printer desktop software I wrote in Visual Studio 2015. I used this back in 2016 to control a 3D printer I built from scratch.

I built this printer from sawing/drilling aluminum into a rectangular prism frame. From here, I built some mounts for a resin tank.
Then, I mounted a 1080p DLP ViewSonic Projector on the top of the frame, and I 3D printed some custom-designed alignment plates from another 3D printer I purchases. Three metal rods were mounted in between the top and bottom of the back frame, and a stepper motor was mounted in the center of the rods. I attached a metal plate to the stepper motor's vertical shaft, and aligned everything so the plate could rise up and down into the resin tank smoothly.

Lastly, I installed a power supply, motor controller board, and an arduino. I wrote custom software for the arduino and Windows, and in the end I was getting perfect 3D prints that looked like perfect, finished factory-made objects.

The arduino firmware I wrote is in another Github project on my account.

# How to use the software

1. Build and run the project
    a. Make sure to build and send over the arduino software, so your printer has the compatible firmware
    b. When the arduino is loaded, go to the Playr SLA Windows Controller, and put in the correct COM port for the arduino (e.g. COM3). Then connect to it.
2. Fill in all relevant details to your SLA printer
    a. Exposure time (around 30s is what I use for my projector's luminence)
    b. Layer distance (0.05mm is typical)
    c. Calibrate everything (Only press the calibrate button once you measure your physical printer with a measuring tool and determine the distance between the higest point and lowest point that the motor can set the plate at. E.g. lowest point is 10cm, and highest point is 80cm, therefore the distance it travels is 70cm. Use the step up and step down buttons to log the total amount of steps that it takes to travel the full 70cm. Once you have those values, you can finish calibrating the printer, and the printer will know the exact amount of steps the motor must take to reach specific vertical distances to the accuracy of the milimeter)
    d. Use a program to convert your STL files to a folder of PNG images that account for each individual, vertical slice of the 3D object.
    e. After setting all parameters and loading the image folder, open the projector's window, and Save the Settings to the printer.
    f. From here, everything should be ready to print.
