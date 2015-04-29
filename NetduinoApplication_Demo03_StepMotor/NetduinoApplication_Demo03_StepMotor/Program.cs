using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoApplication_Demo03_StepMotor
{
    public class Program
    {
        static OutputPort pin9 = new OutputPort(Pins.GPIO_PIN_D9, false);
        static OutputPort pin8 = new OutputPort(Pins.GPIO_PIN_D8, false);
        static OutputPort pin10 = new OutputPort(Pins.GPIO_PIN_D10, false);
        static OutputPort pin11 = new OutputPort(Pins.GPIO_PIN_D11, false);
        static int step = 0, interval = 10;

        public static void Main()
        {
            while (true)
            {
                pin8.Write(step == 0);
                pin10.Write(step == 1);
                pin9.Write(step == 2);
                pin11.Write(step == 3);

                step = (step + 1) % 4;
                Thread.Sleep(interval);
            }
        }
    }
}
