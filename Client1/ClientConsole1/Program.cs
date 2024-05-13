// See https://aka.ms/new-console-template for more information
 
using System.Net.Sockets;  
using System.Net;
using System.Timers;
using System.Runtime.InteropServices;

class Program {

    private static Packet returnedPacket;

    static void Main(string[] args) {  
 
        //one second interval
        System.Timers.Timer timer = new System.Timers.Timer(1000);
        timer.AutoReset = true;
        timer.Elapsed += OnTimerTick;
        timer.Enabled = true;

        IPEndPoint ep = new IPEndPoint(IPAddress.Loopback, 3456);  
        TcpListener listener = new TcpListener(ep);  
        listener.Start();  

        Console.WriteLine(@"  
        ===================================================  
                Started listening requests at: {0}:{1}  
        ===================================================",   
        ep.Address, ep.Port);

        var rand = new Random();

        // Run the loop continuously; this is the server.  
        while (true)  
        {  

            var sender = listener.AcceptTcpClient(); 

            // Save the data sent by the client;  
            Packet packet = new Packet();

            // Insert previously known data
            packet.PacketNumber = returnedPacket.PacketNumber;
            // whatever else needed

            // Put in new data
            byte[] randBytes = new byte[2];
            rand.NextBytes(randBytes);
            packet.PacketType= BitConverter.ToUInt16(randBytes);

            // Convert packet struct back into byte array
            byte[] bytes = new byte[Marshal.SizeOf(packet)];
            socketPacketStructureToByteArray(bytes, ref packet);

            sender.GetStream().Write(bytes, 0, bytes.Length); // Send the response  

        }

    }

    private static void OnTimerTick(Object sender, ElapsedEventArgs e) {
        
        Packet packet = new Packet();

        int packetSize = Marshal.SizeOf(packet);

        // Send empty packet
        byte[] bytes = new byte[packetSize];
        socketPacketStructureToByteArray(bytes, ref packet);
        bytes = sendMessage(bytes);

        socketByteArrayToPacketStructure(bytes, ref packet);

        returnedPacket = packet;
        Console.WriteLine(returnedPacket.PacketNumber);

    }

    private static byte[] sendMessage(byte[] messageBytes) {
        
        int packetSize = Marshal.SizeOf(typeof(Packet));

        try // Try connecting and send the message bytes  
        {  
            System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient("127.0.0.1", 1234); // Create a new connection  
            NetworkStream stream = client.GetStream();  
    
            stream.Write(messageBytes, 0, messageBytes.Length); // Write the bytes  
            Console.WriteLine("================================");  
            Console.WriteLine("=   Connected to the server    =");  
            Console.WriteLine("================================");  
            Console.WriteLine("Waiting for response...");  
    
            messageBytes = new byte[packetSize]; // Clear the message   
    
            // Receive the stream of bytes  
            stream.Read(messageBytes, 0, messageBytes.Length);  
    
            // Clean up  
            stream.Dispose();  
            client.Close();  
        }  
        catch (Exception e) // Catch exceptions  
        {  
            Console.WriteLine(e.Message);  
        }  
    
        return messageBytes; // Return response
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