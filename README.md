# ServerClientTCP
Simple test application for running a server, an intermediate client and server, and a client using TCP/IP socket creation

Uses .NET Console App for hosting and connecting

Uses Blazor Web App's WebAssembly for displaying graphics

```mermaid
graph TD;
    InitialSource-->Server1
    InitialSource-->Client1;
    Server1-->|127.0.0.1:1234|Client1;
    Client1-->|127.0.0.1:3456|Client2;
```