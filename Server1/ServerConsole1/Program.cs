using System.Net.Sockets;  
using System.Net;
using System.Runtime.InteropServices;
  
namespace TcpServer  
{  
    class Program  
    {  
        static void Main(string[] args)  
        {  
            IPEndPoint ep = new IPEndPoint(IPAddress.Loopback, 1234);  
            TcpListener listener = new TcpListener(ep);  
            listener.Start();  
  
            Console.WriteLine(@"  
            ===================================================  
                   Started listening requests at: {0}:{1}  
            ===================================================",   
            ep.Address, ep.Port);

            ushort packetNum = 0;
            var rand = new Random();
  
            // Run the loop continuously; this is the server.  
            while (true)  
            { 

                Packet packet = new Packet();

                int packetSize = Marshal.SizeOf(packet);
  
                byte[] buffer = new byte[packetSize];  
  
                var sender = listener.AcceptTcpClient();  
                sender.GetStream().Read(buffer, 0, packetSize);
  
                // Save the data sent by the client;
                Console.WriteLine("Empty packet received.");
                socketByteArrayToPacketStructure(buffer, ref packet);

                packet.PacketNumber = packetNum;
                packetNum++;

                byte[] bytes = new byte[Marshal.SizeOf(packet)];
                socketPacketStructureToByteArray(bytes, ref packet);

                sender.GetStream().Write(bytes, 0, bytes.Length); // Send the response  

            }  
        }

        public static void socketByteArrayToPacketStructure(byte[] bytearray, ref Packet obj)// for Packet only
        {
            int Length = Marshal.SizeOf(obj);
            IntPtr ptr = Marshal.AllocHGlobal(Length);
            Marshal.Copy(bytearray, 0, ptr, Length);
            obj = (Packet)Marshal.PtrToStructure(ptr, typeof(Packet));
            Marshal.FreeHGlobal(ptr);
        }

        public static void socketPacketStructureToByteArray(byte[] bytearray, ref Packet obj)
        {
            int Length = Marshal.SizeOf(obj);
            IntPtr ptr = Marshal.AllocHGlobal(Length);
            Marshal.StructureToPtr(obj, ptr, false);
            Marshal.Copy(ptr, bytearray, 0, Length);
            Marshal.FreeHGlobal(ptr);
        }

    }  

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct Packet
        {
            public ushort InstrumentID;     
            public ushort PacketType;       
            public ushort PacketTrigger;    
            public ushort PacketNumber;    
            public ushort InstrumentMode;  
            public ushort MMIMode;          
            public ushort timer0;         
            public ushort timer1;          
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8)]
            public ushort[] wOption;

            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16)]
            public ushort[] wOption1;

            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8)]
            public ushort[] wData0;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8)]
            public ushort[] wData1;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8)]
            public ushort[] wData2;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8)]
            public ushort[] wData3;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8)]
            public ushort[] wData4;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8)]
            public ushort[] wData5;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8)]
            public ushort[] wData6;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8)]
            public ushort[] wData7;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 448)]
            public ushort[] wData8;

        } //size =    32 + 1024 = 1056

}  
