using System.Reflection;
using LightInject;


namespace Enexure.MicroBus.LightInject
{
    public static class ContainerExtensions
    {
        public static ServiceContainer RegisterMicroBus(this ServiceContainer serviceContainer, BusBuilder busBuilder)
        {
            return RegisterMicroBus(serviceContainer, busBuilder, new BusSettings());
        }
        public static ServiceContainer RegisterMicroBus(this ServiceContainer serviceContainer, BusBuilder busBuilder, BusSettings busSettings)
        {
            serviceContainer.RegisterInstance(busSettings);

            var pipelineBuilder = new PipelineBuilder(busBuilder);
            pipelineBuilder.Validate();

            RegisterHandlersWithLightInjector(serviceContainer, busBuilder);

            serviceContainer.RegisterInstance<IPipelineBuilder>(pipelineBuilder);
            serviceContainer.Register<IOuterPipelineDetector,OuterPipelineDetector>( new PerScopeLifetime());
            serviceContainer.Register<IOuterPipelineDetertorUpdater,OuterPipelineDetector>( new PerScopeLifetime());

            var outerPipelineDetector = new OuterPipelineDetector();
            var lightInjectDependencyScope = new LightInjectDependencyScope(serviceContainer.BeginScope(), serviceContainer);

            serviceContainer.Register<IPipelineRunBuilder>(factory => new PipelineRunBuilder(busSettings, pipelineBuilder, outerPipelineDetector, lightInjectDependencyScope));

            serviceContainer.Register<IDependencyResolver>(factory => new LightInjectDependencyResolver(serviceContainer));

            

            serviceContainer.Register<IMicroBus, MicroBus>( new PerRequestLifeTime());
            serviceContainer.Register<IMicroMediator, MicroMediator>(new PerRequestLifeTime());
            serviceContainer.Register<ICancelableMicroBus, MicroBus>( new PerRequestLifeTime());
            serviceContainer.Register<ICancelableMicroMediator, MicroMediator>(new PerRequestLifeTime());

            return serviceContainer;
        }

        private static void RegisterHandlersWithLightInjector(ServiceContainer serviceContainer, BusBuilder busBuilder)
        {
            foreach (var globalHandlerRegistration in busBuilder.GlobalHandlerRegistrations)
            {
                serviceContainer.Register(globalHandlerRegistration.HandlerType);

                foreach (var dependency in globalHandlerRegistration.Dependencies)
                {
                    serviceContainer.Register(dependency);
                }
            }

            foreach (var registration in busBuilder.MessageHandlerRegistrations)
            {
                serviceContainer.Register(registration.HandlerType, new PerRequestLifeTime());

                foreach (var dependency in registration.Dependencies)
                {
                    serviceContainer.Register(dependency, new PerRequestLifeTime());
                    var interfaces = dependency.GetTypeInfo().ImplementedInterfaces;
                    foreach (var @interface in interfaces)
                    {
                        serviceContainer.Register(@interface, dependency, new PerRequestLifeTime());
                    }
                }
            }
        }
    }
}