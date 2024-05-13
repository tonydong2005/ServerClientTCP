# ServerClientTCP
Simple test application for running a server (Server1), an intermediate client and server (Client1), and a client (Client2) using TCP/IP socket creation.

Overview:
* Server1 is hosted at 127.0.0.1:1234.
* Client1 sends requests to 127.0.0.1:1234 at a regular interval, currently clocked at 1 per second.
* Server1 fills in packet information upon receiving a request and returns packet to client.
* Client1 hosts an intermediate server at 127.0.0.1:3456.
* Client2 sends requests to 127.0.0.1:3456 at a regular interval, currently clokced at 1 per second.
* Client1 adds on to packet information upon receiving a request and returns packet to client.
* Client2 displays packet information through Web App graphics.

Tools/Libraries:
* .NET Console App for hosting and connecting
* Blazor Web App's WebAssembly for displaying graphics
* System.Net for network endpoint and stream creation
* System.Net.Sockets for network socket creation
* System.Timers.Timer for clocking packet send frequency
* System.Runtime.InteropServices.Marshal for struct and byte array conversion

```mermaid
graph LR;
    id[Initial Source]-->Server1;
    id[Initial Source]-->Client1;
    Server1-->|127.0.0.1:1234|Client1;
    Client1-->|127.0.0.1:3456|Client2;
```