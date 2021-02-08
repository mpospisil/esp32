using System;
using System.Diagnostics;
using System.Threading;
using Windows.Devices.Gpio;
using nanoFramework.Hardware.Esp32;

namespace CarController
{
	public class Program
	{
		static GpioPin goLedPin;
		static GpioPin goPin;

		public static void Main()
		{
			Debug.WriteLine("Hello from nanoFramework!");

			goLedPin = GpioController.GetDefault().OpenPin(Gpio.IO12);
			goLedPin.SetDriveMode(GpioPinDriveMode.Output);
			goLedPin.Write(GpioPinValue.Low);

			goPin = GpioController.GetDefault().OpenPin(Gpio.IO27);
			goPin.SetDriveMode(GpioPinDriveMode.Output);
			goPin.Write(GpioPinValue.High);

			GpioPin engineControlPin = GpioController.GetDefault().OpenPin(Gpio.IO14);
			engineControlPin.SetDriveMode(GpioPinDriveMode.InputPullUp);
			engineControlPin.DebounceTimeout = TimeSpan.FromMilliseconds(2);
			engineControlPin.ValueChanged += EngineControlPin_ValueChanged;

			while (true)
			{
				//goPin.Toggle();
				//var enginePinVal = engineControlPin.Read();
				//Debug.WriteLine(enginePinVal.ToString());
				Thread.Sleep(1000);
			}

			
		}

		private static void EngineControlPin_ValueChanged(object sender, GpioPinValueChangedEventArgs e)
		{
			//Debug.WriteLine(e.Edge.ToString());
			var engineButtonVal = e.Edge.ToString();
			if(!string.IsNullOrEmpty(engineButtonVal) && engineButtonVal.Equals("1"))
			{
				goLedPin.Toggle();
				goPin.Toggle();
			}
		}
	}
}
