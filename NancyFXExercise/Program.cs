﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Hosting.Self;

namespace NancyFXExercise
{
    class Program
    {
       public static void Main(string[] args)
        {
            HostConfiguration hostConfigs = new HostConfiguration();
            hostConfigs.UrlReservations.CreateAutomatically = true;

            Uri uri = new Uri("http://localhost");
            var host = new NancyHost(hostConfigs, uri);
            host.Start();

            Console.ReadKey();
        }
    }
}
