using LightInject;

namespace Enexure.MicroBus.LightInject
{
    internal class LightInjectDependencyResolver :  IDependencyResolver
    {
        private readonly IServiceContainer _container;

        public LightInjectDependencyResolver(IServiceContainer container)
        {
            
            _container = container;
        }

        public void Dispose()
        {

            _container.Dispose();
        }

        public IDependencyScope BeginScope()
        {
            Scope scope = _container.BeginScope();
            return new LightInjectDependencyScope(scope, _container);
        }
    }
}
