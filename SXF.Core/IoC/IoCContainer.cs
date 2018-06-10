using System;
using System.Collections.Generic;
using System.Linq;

namespace SXF.Core.IoC
{
    public class IoCContainer
    {
        private static readonly Dictionary<string, object> _singetons = new Dictionary<string, object>();
        private static readonly Dictionary<string, Func<CtorParams, object>> _objects = new Dictionary<string, Func<CtorParams, object>>();

        /// <summary>
        /// Register new type with injected dependencies.
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="resolver"></param>
        public IoCContainer Register<TObject, TKey>()
            where TObject : class
        {
            var key = typeof(TKey).FullName;
            if (_objects.ContainsKey(key))
            {
                throw new IoCKeyAlreadyDefinedException();
            }

            _objects.Add(key, (parameters) =>
            {
                var (values, param) = ResolveDependencies<TObject>();

                if (param)
                {
                    values.Add(() => parameters ?? new CtorParams());
                }
                return Activator.CreateInstance(typeof(TObject), values.Select(v => v()).ToArray());
            });
            return this;
        }

        /// <summary>
        /// Register new type with injected dependencies.
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="resolver"></param>
        public IoCContainer RegisterConstant<TObject, TKey>()
            where TObject : class
        {
            var key = typeof(TKey).FullName;
            if (_objects.ContainsKey(key))
            {
                throw new IoCKeyAlreadyDefinedException();
            }

            var (values, param) = ResolveDependencies<TObject>();

            _singetons.Add(key, Activator.CreateInstance(typeof(TObject),
                values.Select(v => v()).ToArray()));
            return this;
        }

        /// <summary>
        /// Resolve factory method to create new object with injected dependencies.
        /// </summary>
        /// <typeparam name="TObject">Type of the object to create.</typeparam>
        /// <param name="resolver"></param>
        /// <returns>returns list of factory methods to all dependencies for that <typeparamref name="TObject"/></returns>
        private (List<Func<object>> dependencied, bool Params) ResolveDependencies<TObject>()
            where TObject : class
        {
            var concreteType = typeof(TObject);

            // Must be a single constructor
            var constructors = concreteType.GetConstructors().Single();

            var values = new List<Func<object>>();
            var param = false;
            foreach (var parameter in constructors.GetParameters())
            {
                if (parameter.ParameterType.IsInterface == false && parameter.ParameterType != typeof(CtorParams))
                {
                    throw new InvalidOperationException($"The type {concreteType.Name} has constructor parameters that are not interfaces or {nameof(CtorParams)}.");
                }

                if (_singetons.ContainsKey(parameter.ParameterType.FullName))
                {
                    values.Add(() => _singetons[parameter.ParameterType.FullName]);
                }
                if (parameter.ParameterType == typeof(CtorParams) && param != true)
                {
                    param = true;
                }
            }

            return (values, param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="IoCKeyNotFoundException">Thrown when container cannot find registered object of type <typeparam name="TKey"/></exception>
        public static TKey Resolve<TKey>(CtorParams param = null)
        {
            try
            {
                var name = typeof(TKey).FullName;
                return (TKey)(_singetons.ContainsKey(name) ? _singetons[name] : _objects[name](param));
            }
            catch (IoCKeyNotFoundException)
            {
                throw;
            }
            catch (Exception e) when (e.InnerException != null)
            {
                throw e.InnerException;
            }
        }
    }
}
