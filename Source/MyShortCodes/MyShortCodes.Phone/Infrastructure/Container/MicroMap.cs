using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
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
            MicroMap.Register<TInterface>(concreteClass);
        }

        public void Register<TInterface>(Func<IContainer, TInterface> definition) where TInterface : class
        {
            MicroMap.Register<TInterface>(definition);
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

        public static void Register<TInterface, TClass>()
            where TClass : TInterface
            where TInterface : class
        {
            if (!_store.ContainsKey(typeof(TInterface)))
            {
                throw new InvalidOperationException("Duplicate Interface Definition");
            }
            _store.Add(typeof(TInterface), typeof(TClass)); 
        }

        public static void Register<TInterface>(TInterface concreteClass) where TInterface : class
        {
            if (!_store.ContainsKey(typeof(TInterface)))
            {
                throw new InvalidOperationException("Duplicate Interface Definition");
            }
            _store.Add(typeof(TInterface), concreteClass);
        }

        public static void Register<TInterface>(Func<IContainer, TInterface> definition) where TInterface : class
        {
            if(!_store.ContainsKey(typeof(TInterface)))
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

            if (definitionType == interfaceType)
            {
                return definition;
            }
            else if (definitionType == typeof(Func<IContainer, object>))
            {
                var definitionFunc = definition as Func<IContainer, object>;

                return definitionFunc(new MicroMapContainer());
            }
            else if (definitionType == typeof(Type))
            {
                var concreteType = definitionType as Type;

                // find the constructor with the most arguments
                var constructors = concreteType.GetConstructors(BindingFlags.Public);
                ConstructorInfo longestConstructor = null;
                foreach (var constructor in constructors)
                {
                    if (longestConstructor == null || constructor.GetParameters().Length > longestConstructor.GetParameters().Length)
                    {
                        longestConstructor = constructor;
                    }
                }

                // get all the arugments
                var parameters = longestConstructor.GetParameters();
                var arguments = new List<object>();
                foreach (var parameter in parameters)
                {
                    arguments.Add(GetInstance(parameter.ParameterType));
                }

                // get our instance

                return Activator.CreateInstance(concreteType, arguments);
            }

            throw new InvalidOperationException(String.Format("No valid definition for {0}", interfaceType.Name));
        }


        public static TInterface GetInstance<TInterface>() where TInterface : class
        {
            return GetInstance(typeof(TInterface)) as TInterface;
        }
    }
}
