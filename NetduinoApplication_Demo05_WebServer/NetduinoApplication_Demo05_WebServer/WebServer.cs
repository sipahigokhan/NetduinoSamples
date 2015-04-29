using System;
using Microsoft.SPOT;
using System.Net.Sockets;
using Microsoft.SPOT.Hardware;
using System.Net;
using System.Text;
using System.Threading;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoApplication_Demo05_WebServer
{
    public class WebServer : IDisposable
    {
        private Socket socket = null;
        //open connection to onbaord led so we can blink it with every request
        private OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);

        private OutputPort led0 = new OutputPort(Pins.GPIO_PIN_D0, false);
        private OutputPort led1 = new OutputPort(Pins.GPIO_PIN_D1, false);
        private OutputPort led2 = new OutputPort(Pins.GPIO_PIN_D2, false);
        private OutputPort led3 = new OutputPort(Pins.GPIO_PIN_D3, false);
        private OutputPort led4 = new OutputPort(Pins.GPIO_PIN_D4, false);
        private OutputPort led5 = new OutputPort(Pins.GPIO_PIN_D5, false);
        private OutputPort led6 = new OutputPort(Pins.GPIO_PIN_D6, false);
        private OutputPort led7 = new OutputPort(Pins.GPIO_PIN_D7, false);

        private System.Threading.Timer timer;

        public WebServer()
        {
            timer = new Timer(_ => TimerOnCallBack(), null, Timeout.Infinite, Timeout.Infinite);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, 80));
            Debug.Print(Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0].IPAddress);
            socket.Listen(10);
            ListenForRequest();
        }

        private void TimerOnCallBack()
        {
            if (Program.Command == Command.BlinkEnabled)
            {
                BlinkLights(true);
                Thread.Sleep(200);

                BlinkLights(false);
                Thread.Sleep(200);
            }
        }

        private void BlinkLights(bool state)
        {
            led0.Write(state);
            led1.Write(state);
            led2.Write(state);
            led3.Write(state);
            led4.Write(state);
            led5.Write(state);
            led6.Write(state);
            led7.Write(state);
        }

        private void SequentialLight(bool buttonState)
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

        public void ListenForRequest()
        {
            while (true)
            {
                using (Socket clientSocket = socket.Accept())
                {
                    IPEndPoint clientIP = clientSocket.RemoteEndPoint as IPEndPoint;
                    EndPoint clientEndPoint = clientSocket.RemoteEndPoint;
                    int bytesReceived = 0, loopCount = 0;
                    while (true)
                    {
                        loopCount++;
                        bytesReceived = clientSocket.Available;
                        if (bytesReceived > 0 || loopCount > 1000000)
                            break;
                    }

                    byte[] buffer = new byte[bytesReceived];
                    int byteCount = clientSocket.Receive(buffer, bytesReceived, SocketFlags.None);
                    string request = new string(Encoding.UTF8.GetChars(buffer));
                    string firstLine = request.Substring(0, request.IndexOf('\n'));
                    string[] words = firstLine.Split(' ');
                    if (words.Length > 2)
                    {
                        string method = words[0];
                        Program.Command = (Command)(Convert.ToInt16(words[1].TrimStart('/').TrimStart('?').Split('=')[1]));
                    }

                    if (Program.Command == Command.Sequential)
                    {
                        SequentialLight(false);
                        Thread.Sleep(1000);
                        SequentialLight(true);
                    }
                    else if (Program.Command == Command.BlinkEnabled)
                    {
                        timer.Change(0, 500);
                    }
                    else if (Program.Command == Command.BlinkDisabled)
                    {
                        timer.Change(Timeout.Infinite, Timeout.Infinite);
                    }
                }
            }
        }
        
        #region IDisposable Members

        ~WebServer()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (socket != null)
                socket.Close();
        }

        #endregion
    }

    public enum Command
    {
        BlinkEnabled = 1,
        BlinkDisabled = 2,
        Sequential = 3
    }
}
