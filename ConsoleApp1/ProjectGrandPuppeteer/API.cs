using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
using Rug.Osc;
using LibUsbDotNet;
using LibUsbDotNet.Descriptors;
using LibUsbDotNet.Info;
using LibUsbDotNet.LibUsb;
using LibUsbDotNet.Main;
using Newtonsoft.Json;


namespace ProjectGrandPuppeteer
{
    public class API
    {
        public static API Api { get; set; }

        public static void Enable()
        {
            Api = new API();
        }

        internal ArduinoHandler ArduinoHandler { get; set; }
        internal OSCHandler handler;
        public API()
        {
            handler = new OSCHandler(8001, 8000, new IPAddress(new byte[4] { 192, 168, 1, 2 }));
            ArduinoHandler = new ArduinoHandler();
            
        }

        public void SendOSC(string info, string args)
        {
            handler.sendOSC(info, args);
        }
    }

    public class ArduinoHandler
    {
        public ArduinoHandler()
        {
            StartHandler();
        }

        public void StartHandler()
        {
            // Port_#0008.Hub_#0001   
            // address 00000008
            // Vid 0403   "1027"
            // Pid 6014   "24596"
            // Rev 0900   "900"
            // Device Id "USB\VID_0403&PID_6014\6&258DE23&0&8"
            // Device GUID "{5ebc5238-fdee-4447-ba74-b056853fc0b0}"
            // Device Path "\\?\usb#vid_0403&pid_6014#6&258de23&0&8#{5ebc5238-fdee-4447-ba74-b056853fc0b0}"
            // Symbolic Name "\\?\usb#vid_0403&pid_6014#6&258de23&0&8#{5ebc5238-fdee-4447-ba74-b056853fc0b0}"
            // Device Full Name "Future Technology Devices International, Ltd - USB Serial Converter"
            // Device Name "USB Serial Converter"

            //LibUsbDotNet.Device;
            //LibUsbDotNet.Context;
            //LibUsbDotNet.DeviceHandle;
            //LibUsbDotNet.Main.UsbDeviceFinder;
            //Device device = new Device();
            //Context context = new Context();
            //DeviceHandle handle = new DeviceHandle();
            int vid = 0403;
            int pid = 6014;
            int rev = 0900;
            int port = 0008;
            int hub = 0001;
            string classGuid = "{ecfb0cfd-74c4-4f52-bbf7-343461cd72ac}";
            //UsbDeviceFinder usbDeviceFinder = new UsbDeviceFinder(vid, pid, rev);
            //search();
            //if (MyUsbDevice == null)
            //    searchMethodTwo();
            //if(MyUsbDevice is null)
            //    return;
            //MyUsbDevice.

            Thread thread = new Thread(processOutput);
            thread.Start();
            //processOutput();

            Log.Debug($"output test started");

        }

        public UsbDevice MyUsbDevice;
        public UsbRegistry registry;

        public void processOutput()
        {
            Log.Debug($"Started Test Loop");
            int vid = 1027;
            int pid = 24596;
            int rev = 0900;
            UsbRegistry Device = null;
            UsbDeviceFinder finder = new UsbDeviceFinder(vid, pid, rev);

            UsbDevice MyUsbDevice = UsbDevice.OpenUsbDevice(finder);
            ErrorCode ec = ErrorCode.None;
            try
            {
                // Find and open the usb device.
                //MyUsbDevice = UsbDevice.OpenUsbDevice(MyUsbFinder);

                // If the device is open and ready
                if (MyUsbDevice == null) throw new Exception("Device Not Found.");

                // If this is a "whole" usb device (libusb-win32, linux libusb-1.0)
                // it exposes an IUsbDevice interface. If not (WinUSB) the 
                // 'wholeUsbDevice' variable will be null indicating this is 
                // an interface of a device; it does not require or support 
                // configuration and interface selection.
                IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
                if (!ReferenceEquals(wholeUsbDevice, null))
                {
                    // This is a "whole" USB device. Before it can be used, 
                    // the desired configuration and interface must be selected.

                    // Select config #1
                    wholeUsbDevice.SetConfiguration(1);

                    // Claim interface #0.
                    wholeUsbDevice.ClaimInterface(0);
                }

                // open read endpoint 1.
                UsbEndpointReader reader = MyUsbDevice.OpenEndpointReader(ReadEndpointID.Ep01);


                byte[] readBuffer = new byte[1024];
                while (ec == ErrorCode.None)
                {
                    int bytesRead;

                    // If the device hasn't sent data in the last 5 seconds,
                    // a timeout error (ec = IoTimedOut) will occur. 

                    ec = reader.Read(readBuffer, 5000, out bytesRead);

                    if (bytesRead == 0) throw new Exception(string.Format("{0}:No more bytes!", ec));
                    Log.Debug($"{bytesRead} bytes read: \"{Encoding.Default.GetString(readBuffer, 0, bytesRead)}\"");
                    Thread.Sleep(50);
                }

                Console.WriteLine("\r\nDone!\r\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine((ec != ErrorCode.None ? ec + ":" : String.Empty) + ex.Message);
            }
            finally
            {
                if (MyUsbDevice != null)
                {
                    if (MyUsbDevice.IsOpen)
                    {
                        // If this is a "whole" usb device (libusb-win32, linux libusb-1.0)
                        // it exposes an IUsbDevice interface. If not (WinUSB) the 
                        // 'wholeUsbDevice' variable will be null indicating this is 
                        // an interface of a device; it does not require or support 
                        // configuration and interface selection.
                        IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
                        if (!ReferenceEquals(wholeUsbDevice, null))
                        {
                            // Release interface #0.
                            wholeUsbDevice.ReleaseInterface(0);
                        }

                        MyUsbDevice.Close();
                    }

                    MyUsbDevice = null;

                    // Free usb resources
                    UsbDevice.Exit();

                }

                // Wait for user input..
                Log.Debug($"Thread Ended because no more output was detected");
            }

        }

        private void searchMethodTwo()
        {
            int vid = 1027;
            int pid = 24596;
            int rev = 0900;
            UsbRegistry Device = null;
            UsbDeviceFinder finder = new UsbDeviceFinder(vid, pid, rev);
            foreach (UsbRegistry device in UsbDevice.AllDevices)
            {
                if (finder.Check(device))
                { 
                    string json = JsonConvert.SerializeObject(device);
                    //Log.Debug($"USB Device Found. Device: \n{json}");
                    Device = device;
                    break;
                }
            }

            if (Device == null)
                return;
            registry = Device;
            if(Device.Device == null)
                Log.Error($"Device is null.");

        }

        private void search()
        {
            var allDevices = UsbDevice.AllDevices;
            foreach (UsbRegistry usbRegistry in allDevices)
            {
                if (usbRegistry.Open(out MyUsbDevice))
                {
                    break;
                }
            }


            // Free usb resources.
            // This is necessary for libusb-1.0 and Linux compatibility.
            UsbDevice.Exit();
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
            //Log.Debug($"Message: {message.tost}");
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