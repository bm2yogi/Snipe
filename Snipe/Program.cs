using System;
using Autofac;

namespace Snipe
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var serviceLocator = IocRegistration.Register();
                var application = serviceLocator.Resolve<ISnipeConsole>();

                application.Run(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
