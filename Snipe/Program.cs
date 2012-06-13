using Autofac;

namespace Snipe
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceLocator = IocRegistration.Register();
            var application = (SnipeConsole) serviceLocator.Resolve(typeof (SnipeConsole));

            application.Run(args);
        }
    }
}
