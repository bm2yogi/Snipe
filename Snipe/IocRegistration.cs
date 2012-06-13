using System.Reflection;
using Autofac;
using SnipeLib;

namespace Snipe
{
    public class IocRegistration
    {
         public static IContainer Register()
         {
             var builder = new ContainerBuilder();
             var libAssembly = Assembly.Load("SnipeLib");
             var executingAssembly = Assembly.GetExecutingAssembly();

             builder.RegisterAssemblyTypes(libAssembly, executingAssembly).AsImplementedInterfaces();
             //builder.Register(context => new SnipeConsole(context.Resolve<IApplicationHost>()));
             return builder.Build();
         }
    }
}