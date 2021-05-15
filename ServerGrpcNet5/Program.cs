using Grpc.Core;
using GrpcGreeter;
using ProtoBuf.Grpc.Server;
using SharedStandard;
using System;
using System.Collections.Generic;
using System.IO;

namespace ServerGrpcNet5
{
    class Program
    {
        static void Main(string[] args)
        {
            //var cacert = File.ReadAllText(@"ca.crt");
            //var servercert = File.ReadAllText(@"server.crt");
            //var serverkey = File.ReadAllText(@"server.key");
            //var keypair = new KeyCertificatePair(servercert, serverkey);
            //var sslCredentials = new SslServerCredentials(new List<KeyCertificatePair>() { keypair }, cacert, false);

            Server server = new Server
            {
                Ports = { new ServerPort("localhost", 10042, ServerCredentials.Insecure) },
            };

            var calc = new MyCalculator();
            server.Services.AddCodeFirst<ICalculator>(calc);

            var greeter = new GreeterService();
            server.Services.AddCodeFirst<Greeter.GreeterBase>(greeter);

            server.Start();
            Console.WriteLine("Server running... press any key");
            Console.ReadKey();
            server.ShutdownAsync().Wait();
            Console.WriteLine("Server disposed... press any key");
            Console.ReadKey();
        }
    }
}
