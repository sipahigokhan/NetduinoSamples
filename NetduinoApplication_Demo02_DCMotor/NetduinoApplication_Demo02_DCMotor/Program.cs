using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoApplication_Demo02_DCMotor
{
    public class Program
    {
        static OutputPort ledOnboard = new OutputPort(Pins.ONBOARD_LED, false);
        static OutputPort pin0 = new OutputPort(Pins.GPIO_PIN_D0, false);

        public static void Main()
        {
            InterruptPort btnOnboard = new InterruptPort(Pins.ONBOARD_BTN, 
                false, Port.ResistorMode.Disabled, 
                Port.InterruptMode.InterruptEdgeBoth);
            btnOnboard.OnInterrupt += btnOnboard_OnInterrupt;

            while (true)
            {

            }
        }

        static void btnOnboard_OnInterrupt(uint data1, uint data2, DateTime time)
        {
            if (data2 == 0)
            {
                ledOnboard.Write(true);

                pin0.Write(!pin0.Read());

                ledOnboard.Write(false);
            }
        }
    }
}
