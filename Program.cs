using System;
using System.Net.NetworkInformation;
using System.Text;
using CommandLine.Utility;

namespace Program {
    class pingApp {
        public static int Main(string[] args) {
            try {
                Arguments CommandLine=new Arguments(args);
                if (CommandLine["ip"]!=null) {
                    string pingIP = CommandLine["ip"];
                    Ping pingSender = new Ping();
                    PingOptions options = new PingOptions();
                    options.DontFragment=true;
                    string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                    byte[] buffer = Encoding.ASCII.GetBytes(data);
                    int timeout = 120;
                    PingReply reply = pingSender.Send(pingIP,timeout,buffer,options);
                    if (reply.Status==IPStatus.Success) {
                        WriteToConsole("RoundTrip time: "+reply.RoundtripTime);
                    } else if (reply.Status==IPStatus.TimedOut) {
                        return (int) ExitCode.Timeout;
                    }
                } else {
                    printHelp();
                }
                return (int) ExitCode.Success;
            } catch (Exception) {
                return (int) ExitCode.Error;
            }
        }

        private static void printHelp() {
            WriteToConsole("Usage: pingApp.exe -ip (ip address).");
            WriteToConsole("Ex: pingApp.exe -ip 127.0.0.1");
        }
        public static void WriteToConsole(string message="") {
            if (message.Length>0) {
                Console.WriteLine(message);
            }
        }
        enum ExitCode:int {
            Success=0,
            Error=1,
            Timeout=2,
            UnknownError=10
        }
    }
}
