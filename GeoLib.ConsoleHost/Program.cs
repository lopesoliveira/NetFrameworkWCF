﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using GeoLib.Services;
using System.ServiceModel.Channels;
using GeoLib.Contracts;

namespace GeoLib.ConsoleHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost hostGeoManager = new ServiceHost(typeof(GeoManager));

            /*string address = "net.tcp://localhost:8009/GeoService";
            Binding binding = new NetTcpBinding();
            Type contract = typeof(IGeoService);

            hostGeoManager.AddServiceEndpoint(contract, binding, address);*/

            hostGeoManager.Open();

            Console.WriteLine("Services started. Press [Enter] to exit.");
            Console.ReadLine();

            if (hostGeoManager.State != CommunicationState.Faulted)
            {
                hostGeoManager.Close();
                Console.WriteLine("Services Closed");
            }
            else
            {
                hostGeoManager.Abort();
                Console.WriteLine("Service Aborted");
            }
            Console.ReadLine();
        }
    }
}
