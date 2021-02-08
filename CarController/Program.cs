using System;
using System.Diagnostics;
using System.Threading;
using Windows.Devices.Gpio;
using nanoFramework.Hardware.Esp32;

namespace CarController
{
	public class Program
	{
		public static void Main()
		{
			Debug.WriteLine("Hello from nanoFramework!");

			GpioPin goPin = GpioController.GetDefault().OpenPin(Gpio.IO12);
			goPin.SetDriveMode(GpioPinDriveMode.Output);

			while(true)
			{
				goPin.Toggle();
				Thread.Sleep(1000);
			}

			
		}
	}
}
