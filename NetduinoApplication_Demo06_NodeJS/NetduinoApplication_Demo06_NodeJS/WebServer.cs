using System;
using Microsoft.SPOT;
using System.Net.Sockets;
using Microsoft.SPOT.Hardware;
using System.Net;
using System.Text;
using System.Threading;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoApplication_Demo06_NodeJS
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

        public WebServer()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, 80));
            Debug.Print(Microsoft.SPOT.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()[0].IPAddress);
            socket.Listen(10);
            ListenForRequest();
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
                    
                    Program.Command = (Command)(Convert.ToInt16(request));
                    
                    if (Program.Command == Command.D0_0)
                    {
                        led0.Write(false);
                    }
                    else if (Program.Command == Command.D0_1)
                    {
                        led0.Write(true);
                    }
                    else if (Program.Command == Command.D1_0)
                    {
                        led1.Write(false);
                    }
                    else if (Program.Command == Command.D1_1)
                    {
                        led1.Write(true);
                    }
                    else if (Program.Command == Command.D2_0)
                    {
                        led2.Write(false);
                    }
                    else if (Program.Command == Command.D2_1)
                    {
                        led2.Write(true);
                    }
                    else if (Program.Command == Command.D3_0)
                    {
                        led3.Write(false);
                    }
                    else if (Program.Command == Command.D3_1)
                    {
                        led3.Write(true);
                    }
                    else if (Program.Command == Command.D4_0)
                    {
                        led4.Write(false);
                    }
                    else if (Program.Command == Command.D4_1)
                    {
                        led4.Write(true);
                    }
                    else if (Program.Command == Command.D5_0)
                    {
                        led5.Write(false);
                    }
                    else if (Program.Command == Command.D5_1)
                    {
                        led5.Write(true);
                    }
                    else if (Program.Command == Command.D6_0)
                    {
                        led6.Write(false);
                    }
                    else if (Program.Command == Command.D6_1)
                    {
                        led6.Write(true);
                    }
                    else if (Program.Command == Command.D7_0)
                    {
                        led7.Write(false);
                    }
                    else if (Program.Command == Command.D7_1)
                    {
                        led7.Write(true);
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
        D0_1 = 10,
        D1_1 = 11,
        D2_1 = 12,
        D3_1 = 13,
        D4_1 = 14,
        D5_1 = 15,
        D6_1 = 16,
        D7_1 = 17,
        D8_1 = 18,
        D9_1 = 19,

        D0_0 = 20,
        D1_0 = 21,
        D2_0 = 22,
        D3_0 = 23,
        D4_0 = 24,
        D5_0 = 25,
        D6_0 = 26,
        D7_0 = 27,
        D8_0 = 28,
        D9_0 = 29
    }
}
