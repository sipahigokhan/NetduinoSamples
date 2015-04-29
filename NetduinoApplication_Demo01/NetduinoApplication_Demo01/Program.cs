using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoApplication_Demo01
{
    public class Program
    {
        static OutputPort ledOnboard = new OutputPort(Pins.ONBOARD_LED, false);
        static OutputPort led0 = new OutputPort(Pins.GPIO_PIN_D0, false);
        static OutputPort led1 = new OutputPort(Pins.GPIO_PIN_D1, false);
        static OutputPort led2 = new OutputPort(Pins.GPIO_PIN_D2, false);
        static OutputPort led3 = new OutputPort(Pins.GPIO_PIN_D3, false);
        static OutputPort led4 = new OutputPort(Pins.GPIO_PIN_D4, false);
        static OutputPort led5 = new OutputPort(Pins.GPIO_PIN_D5, false);
        static OutputPort led6 = new OutputPort(Pins.GPIO_PIN_D6, false);
        static OutputPort led7 = new OutputPort(Pins.GPIO_PIN_D7, false);

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

        static void btnOnboard_OnInterrupt(uint data1, uint data2, 
            DateTime time)
        {
            if (data2 == 0)
            {
                ledOnboard.Write(true);

                SequentialLight(false);
                Thread.Sleep(1000);
                SequentialLight(true);

                ledOnboard.Write(false);
            }
        }

        private static void SequentialLight(bool buttonState)
        {
            led0.Write(!buttonState);
            led1.Write(buttonState);

            Thread.Sleep(200);
            led1.Write(!buttonState);
            led2.Write(buttonState);

            Thread.Sleep(200);
            led2.Write(!buttonState);
            led3.Write(buttonState);

            Thread.Sleep(200);
            led3.Write(!buttonState);
            led4.Write(buttonState);

            Thread.Sleep(200);
            led4.Write(!buttonState);
            led5.Write(buttonState);

            Thread.Sleep(200);
            led5.Write(!buttonState);
            led6.Write(buttonState);

            Thread.Sleep(200);
            led6.Write(!buttonState);
            led7.Write(buttonState);

            Thread.Sleep(200);
            led7.Write(!buttonState);
        }
    }
}
