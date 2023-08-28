# BlazorWebView
A [WebKitGtkSharp](https://github.com/GtkSharp/GtkSharp) WebView for running [Blazor Hybrid](https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/) applications on Linux without the need to compile a native library.

Blazor Hybrid apps allow one to create a Blazor desktop application that uses the local machine's .NET runtime (not Blazor WASM), has full access to local resources (not sandboxed), and does not require hosting through a web server (e.g. Blazor server). It is just like any other desktop application written in C#, but uses Blazor and web technologies to implement the user interface.

## Why?
Microsoft has decided to not support Maui on Linux, so there is currently no way to create Blazor Hybrid apps on Linux using *only* C#.

GtkSharp's WebKit implementation is currently [incomplete](https://github.com/GtkSharp/GtkSharp/pull/274).  It'd be best to move [GTKSharp's PR #274](https://github.com/GtkSharp/GtkSharp/pull/274) along, but this project provides a working alternative and a proof of concept until then.

## How?
BlazorWebView uses some of the same code as [Steve Sanderson's WebWindow](https://github.com/SteveSandersonMS/WebWindow) and leverages [Microsoft's WebView infrastructure](https://github.com/dotnet/aspnetcore/tree/main/src/Components/WebView) to get Blazor Hybrid working.  However, it differs from WebWindow in that it doesn't require one to compile a native shared library in C++, instead utilizing P/Invoke to call into the native libraries.   This has the benefit that, as long as the native libraries are installed on the Linux system, one only needs to use the `dotnet` CLI to build and run BlazorWebView.

## Demonstration
```
git clone https://github.com/lytico/BlazorWebView.git
cd BlazorWebView/BlazorWebKit.Test
dotnet run
```

## Usage
See the project in [BlazorWebKit.Test](https://github.com/JinShil/BlazorWebView/tree/main/BlazorWebKit.Test) for an example illustrating how to create a Blazor Hybrid application using the BlazorWebView.

## Status
This poject was tested on Windows Subsystem for Linux, Raspberry Pi Bullseye 64-bit, and Debian Bullseye 64-bit.  In the [BlazorWebKit.Test/.vscode](https://github.com/JinShil/BlazorWebView/tree/main/BlazorWebKit.Test/.vscode) directory the necessary configuration to build, deploy, and debug a Raspberry Pi from a Debian Bullseyse 64-bit workstation PC can be found.
