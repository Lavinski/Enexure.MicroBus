using System;
using System.Collections.Generic;
using System.Linq;
using LightInject;

namespace Enexure.MicroBus.LightInject
{
    internal class LightInjectDependencyScope : LightInjectDependencyResolver, IDependencyScope
    {
        private readonly Scope _scope;
        private readonly IServiceContainer _container;

        public LightInjectDependencyScope(Scope scope, IServiceContainer container) : base(container)
        {
            _scope = scope;
            _container = container;

        }

        protected LightInjectDependencyScope(IServiceContainer container) : base( container)
        {
            _container = container;
        }

        public new virtual void Dispose()
        {
            _scope.Dispose();
        }

        public virtual object GetService(Type serviceType)
        {
            object res = _container.TryGetInstance(serviceType);
            return res;
        }

        public virtual IEnumerable<object> GetServices(Type serviceType)
        {
            IEnumerable<object> res = _container.GetAllInstances(serviceType);
            return res;
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public IEnumerable<T> GetServices<T>()
        {
            return GetServices(typeof(T)).Cast<T>();
        }
    }
}