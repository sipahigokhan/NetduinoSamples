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
            #region Second Step

#if SecondStep
            InterruptPort btnOnboard = new InterruptPort(Pins.ONBOARD_BTN, false, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeBoth);
            btnOnboard.OnInterrupt += btnOnboard_OnInterrupt;
            timer = new Timer(_ => TimerOnCallBack(), null, Timeout.Infinite, Timeout.Infinite);
            while (true)
            {
            }
#endif
            #endregion

            #region First Step

#if FirstStep

            while (true)
            {
                pin8.Write(step == 0);
                pin10.Write(step == 1);
                pin9.Write(step == 2);
                pin11.Write(step == 3);

                step = (step + 1) % 4;
                Thread.Sleep(interval);
            }

#endif
            #endregion
        }

        #region Second Step
#if SecondStep
        static bool state = false;
        static System.Threading.Timer timer;

        static void btnOnboard_OnInterrupt(uint data1, uint data2, DateTime time)
        {
            if (data2 == 0)
            {
                state = !state;
                if(state)
                    timer.Change(0, interval);
                else
                    timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }

        static void TimerOnCallBack()
        {
            pin8.Write(step == 0);
            pin10.Write(step == 1);
            pin9.Write(step == 2);
            pin11.Write(step == 3);

            step = (step + 1) % 4;
        }
#endif
        #endregion
    }
}
