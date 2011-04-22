using System;
using System.Collections.Generic;
using System.Reflection;

namespace MyShortCodes.Phone.Infrastructure.Container
{
    public interface IContainer
    {
        void Register<TInterface, TClass>()
            where TClass : TInterface
            where TInterface : class;
        void Register<TInterface>(TInterface concreteClass) where TInterface : class;
        void Register<TInterface>(Func<IContainer, TInterface> definition) where TInterface : class;
        TInterface GetInstance<TInterface>() where TInterface : class;
    }

    public class MicroMapContainer : IContainer
    {
        public void Register<TInterface, TClass>()
            where TClass : TInterface
            where TInterface : class
        {
            MicroMap.Register<TInterface, TClass>();
        }

        public void Register<TInterface>(TInterface concreteClass) where TInterface : class
        {
            MicroMap.Register(concreteClass);
        }

        public void Register<TInterface>(Func<IContainer, TInterface> definition) where TInterface : class
        {
            MicroMap.Register(definition);
        }

        public TInterface GetInstance<TInterface>() where TInterface : class 
        {
            return MicroMap.GetInstance<TInterface>();
        }
    }

    public static class MicroMap
    {
        private static Dictionary<Type, object> _store;

        public static void Initialize(Action<IContainer> initialization) 
        {
            _store = new Dictionary<Type, object>();
            Register<IContainer>(new MicroMapContainer());
            initialization(new MicroMapContainer());
        }

        public static void Initialize()
        {
            Initialize(x => { });
        }

        public static void Register<TInterface, TClass>()
            where TClass : TInterface
            where TInterface : class
        {
            if (_store.ContainsKey(typeof(TInterface)))
            {
                throw new InvalidOperationException("Duplicate Interface Definition");
            }
            var interfaceType = typeof (TInterface);
            var concreteType = typeof (TClass).UnderlyingSystemType;
            _store.Add(interfaceType, concreteType); 
        }

        public static void Register<TInterface>(TInterface concreteClass) where TInterface : class
        {
            if (_store.ContainsKey(typeof(TInterface)))
            {
                throw new InvalidOperationException("Duplicate Interface Definition");
            }
            _store.Add(typeof(TInterface), concreteClass);
        }

        public static void Register<TInterface>(Func<IContainer, TInterface> definition) where TInterface : class
        {
            if(_store.ContainsKey(typeof(TInterface)))
            {
                throw new InvalidOperationException("Duplicate Interface Definition");
            }
            _store.Add(typeof(TInterface), definition);
        }

        public static object GetInstance(Type interfaceType)
        {
            if (!_store.ContainsKey(interfaceType))
            {
                throw new InvalidOperationException(String.Format("No definition found for {0}", interfaceType.Name));
            }

            var definition = _store[interfaceType];
            var definitionType = definition.GetType();

            if (interfaceType.IsAssignableFrom(definitionType))
            {
                return definition;
            }
            if (definitionType.IsGenericType && typeof(Func<,>).IsAssignableFrom(definitionType.GetGenericTypeDefinition()))
            {
                var invokeMethod = definitionType.GetMethod("Invoke");

                return invokeMethod.Invoke(definition, new object[] { new MicroMapContainer() });
            }
            if (definitionType.IsInstanceOfType(typeof(Type)))
            {
                var concreteType = definition as Type;
                if(concreteType == null)
                {
                    throw new InvalidOperationException("Invalid definition");
                }

                // find the constructor with the most arguments
                var constructors = concreteType.GetConstructors();
                ConstructorInfo longestConstructor = null;
                foreach (var constructor in constructors)
                {
                    if (longestConstructor == null || constructor.GetParameters().Length > longestConstructor.GetParameters().Length)
                    {
                        longestConstructor = constructor;
                    }
                }

                if(longestConstructor == null)
                {
                    return Activator.CreateInstance(concreteType);
                } 

                // get all the arugments
                var parameters = longestConstructor.GetParameters();
                var arguments = new List<object>();
                foreach (var parameter in parameters)
                {
                    var dependancyObject = GetInstance(parameter.ParameterType);
                    arguments.Add(dependancyObject);
                }

                // get our instance
                if(arguments.Count == 0)
                {
                    return Activator.CreateInstance(concreteType);
                }
                return longestConstructor.Invoke(arguments.ToArray());
            }

            throw new InvalidOperationException(String.Format("No valid definition for {0}", interfaceType.Name));
        }

        public static TInterface GetInstance<TInterface>() where TInterface : class
        {
            return GetInstance(typeof(TInterface)) as TInterface;
        }

    }
}
