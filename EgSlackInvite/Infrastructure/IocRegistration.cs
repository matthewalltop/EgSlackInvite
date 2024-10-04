namespace EgSlackInvite.Infrastructure
{
    
    using System.Reflection;

    using Autofac;
    using CloudProvider.Abstract;
    using SlackApiClient.Abstract.Service;
    using SlackApiClient.Concrete.Strategy;

    public static class IocRegistration
    {
        private static IContainer _container;
        public static IContainer Container
        {
            get
            {
                if(_container == null)
                    BuildContainer();
                return _container;
            }
        }

        private static void BuildContainer()
        {
            var builder = new ContainerBuilder();

            var assemblies = new[]
            {
                Assembly.GetCallingAssembly(),
                Assembly.GetAssembly(typeof(IChatClientUserService)),
                Assembly.GetAssembly(typeof(IUserSettingsClient))
            };

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Service") 
                            || t.Name.EndsWith("Factory") 
                            || t.Name.EndsWith("Handler")
                            || t.Name.EndsWith("Client"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<SlackPublicChannelStrategy>()
                .Keyed<ISlackChannelStrategy>(SlackChannelAccess.Public);
            builder.RegisterType<SlackPrivateChannelStrategy>()
                .Keyed<ISlackChannelStrategy>(SlackChannelAccess.Private);

            _container = builder.Build();
        }
    }
}
