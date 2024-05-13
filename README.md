# ServerClientTCP
Simple test application for running a server (Server1), an intermediate client and server (Client1), and a client (Client2) using TCP/IP socket creation

Tools/Libraries:
.NET Console App for hosting and connecting
Blazor Web App's WebAssembly for displaying graphics
System.Net for network endpoint and stream creation
System.Net.Sockets for network socket creation
System.Timers.Timer for clocking packet send frequency
System.Runtime.InteropServices.Marshal for struct and byte array conversion

```mermaid
graph LR;
    id[InitialSource]-->Server1;
    id[InitialSource]-->Client1;
    Server1-->|127.0.0.1:1234|Client1;
    Client1-->|127.0.0.1:3456|Client2;
```