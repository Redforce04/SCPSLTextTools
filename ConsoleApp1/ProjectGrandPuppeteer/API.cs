using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
using Rug.Osc;

namespace ProjectGrandPuppeteer
{
    public class API
    {
        public static API Api { get; set; }

        public static void Enable()
        {
            Api = new API();
        }

        internal OSCHandler handler;
        public API()
        {
            handler = new OSCHandler(8001, 8000, new IPAddress(new byte[4] { 192, 168, 1, 2 }));
        }

        public void SendOSC(string info, string args)
        {
            handler.sendOSC(info, args);
        }
    }

    public class OSCHandler
    {
        public OSCHandler(int listenPort, int sendPort, IPAddress address)
        {
            try
            {
                AddressManager = new OscAddressManager();
                Receiver = new OscReceiver(listenPort);
                Sender = new OscSender(address, sendPort);
            }
            catch (Exception e)
            {
                Log.Error($"Could not initialize OSC Properly. Exception: {e}");
                return;
            }

            AddressManager.Attach("/*", HandleMessage);
            AddressManager.Attach("/eos/out", HandleMessage);
            AddressManager.Attach("/eos/out/ping", HandleMessage);
            listenerThread = new Thread(ListenLoop);
            try
            {

                Receiver.Connect();
                Sender.Connect();

                listenerThread.Start();
            }
            catch (Exception e)
            {
                Log.Error($"Could not connect or start listen loop. Exception: {e}");
                return;
            }
            Log.Debug($"Initialized OSC Properly");
        }

        private Thread listenerThread { get; set; }
        private OscAddressManager AddressManager { get; set; }
        private OscReceiver Receiver { get; set; }
        private OscSender Sender { get; set; }

        private void HandleMessage(OscMessage message)
        {
            string outp = "";
            foreach (var x in message)
            {
                outp += x.ToString();
            }
            Log.Debug($"Message: {outp}");
            Log.Debug($"Message: {message.ToString()}");
        }
        internal void sendOSC(string path, string args)
        {
            Sender.Send(new OscMessage(path, args));
        }
        private void ListenLoop()
        {
            try
            {
                while (Receiver.State != OscSocketState.Closed)
                {
                    // if we are in a state to recieve
                    if (Receiver.State == OscSocketState.Connected)
                    {
                        // get the next message 
                        // this will block until one arrives or the socket is closed
                        OscPacket packet = Receiver.Receive();

                        var arr = packet.ToByteArray();
                        string output = "";
                        string outputText = "";
                        var utf8 = ASCIIEncoding.Convert(Encoding.ASCII, Encoding.UTF32, arr, 0, arr.Length);
                        foreach (var y in utf8)
                        {
                            outputText += char.ConvertFromUtf32(y);
                        }
                        foreach (var x in arr)
                        {
                            output += $"{x} ";
                        }
                        Log.Debug($"Packet: {packet.ToString()}");
                        
                        switch (AddressManager.ShouldInvoke(packet))
                        {
                            case OscPacketInvokeAction.Invoke:
                                Log.Debug("Received packet");
                                AddressManager.Invoke(packet);
                                break;
                            case OscPacketInvokeAction.DontInvoke:
                                Log.Debug("Cannot invoke");
                                Log.Debug(packet.ToString());
                                break;
                            case OscPacketInvokeAction.HasError:
                                Log.Debug("Error reading osc packet, " + packet.Error);
                                Log.Debug(packet.ErrorMessage);
                                break;
                            case OscPacketInvokeAction.Pospone:
                                Log.Debug("Posponed bundle");
                                Log.Debug(packet.ToString());
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // if the socket was connected when this happens
                // then tell the user
                if (Receiver.State == OscSocketState.Connected)
                {
                    Log.Debug("Exception in listen loop");
                    Log.Debug(ex.Message);
                }
            }
        }
    }

}