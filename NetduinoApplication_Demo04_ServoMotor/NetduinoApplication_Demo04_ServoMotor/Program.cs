using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;


namespace NetduinoApplication_Demo04_ServoMotor
{
    public class Program
    {
        static SecretLabs.NETMF.Hardware.PWM servo = new SecretLabs.NETMF.Hardware.PWM(Pins.GPIO_PIN_D10);
        static short currentPosition;

        public static void Main()
        {
            InterruptPort btnOnboard = new InterruptPort(Pins.ONBOARD_BTN, true, Port.ResistorMode.Disabled,
                Port.InterruptMode.InterruptEdgeBoth);
            btnOnboard.OnInterrupt += btnOnboard_OnInterrupt;

            currentPosition = 10;
            SetServoPosition(currentPosition);

            while (true)
            {

            }
        }

        static void btnOnboard_OnInterrupt(uint data1, uint data2, DateTime time)
        {
            if (data2 == 0)
            {
                currentPosition += 30;
                SetServoPosition(currentPosition);
            }
        }

        private static void SetServoPosition(int position)
        {
            position = (position % 180) + 10;
            servo.SetPulse(20000, 500 + (uint)(position * 11.11));
        }
    }
}
