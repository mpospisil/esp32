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

			GpioPin engineControlPin = GpioController.GetDefault().OpenPin(Gpio.IO14);
			engineControlPin.SetDriveMode(GpioPinDriveMode.Input);
			engineControlPin.DebounceTimeout = TimeSpan.FromMilliseconds(2);
			engineControlPin.ValueChanged += EngineControlPin_ValueChanged;

			while (true)
			{
				goPin.Toggle();
				Thread.Sleep(1000);
			}

			
		}

		private static void EngineControlPin_ValueChanged(object sender, GpioPinValueChangedEventArgs e)
		{
			Debug.WriteLine(e.Edge.ToString());
		}
	}
}
