using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BydloForms.Helpers
{
    public readonly struct PropertySetter<T>
    {

        private readonly Dictionary<string, PropertyDefination> _definations = new();

        public PropertySetter() { }

        public void Property<U>(string name, Expression<Func<T, U>> expression)
        {
            if (expression.Body is not MemberExpression member)
            {
                throw new ArgumentException("Need reference to property with setter", nameof(expression));
            }

            var property = member.Member as PropertyInfo;

            if (property == null)
            {
                throw new ArgumentException("Need reference to property with setter", nameof(expression));
            }

            var setter = property.SetMethod ?? throw new ArgumentException("Need reference to property with setter", nameof(expression));

            var propertyDefination = new PropertyDefination(TypeOf<U>.Type, CreateInvoker<U>(setter));

            _definations.Add(name, propertyDefination);
        }

        public void Set(IReadOnlyDictionary<string, string> keyValues, object target)
        {
            foreach(var kv in keyValues)
            {
                var defination = _definations[kv.Key];
                var value = TypeDescriptor.GetConverter(defination.type)
                                          .ConvertFromInvariantString(kv.Value);

                defination.invoker(target, value);
            }
        }

        Invoker CreateInvoker<U>(MethodInfo method)
        {
            var instanceParam = Expression.Parameter(TypeOf<object>.Type);
            var argumentParam = Expression.Parameter(TypeOf<object>.Type);

            var body = Expression.Call(
                Expression.Convert(instanceParam, TypeOf<T>.Type),
                method,
                Expression.Convert(argumentParam, TypeOf<U>.Type));

            return Expression.Lambda<Invoker>(body, instanceParam, argumentParam).Compile();
        }

        delegate void Invoker(object target, object value);

        readonly struct PropertyDefination
        {
            public readonly Type type;
            public readonly Invoker invoker;

            public PropertyDefination(Type type, Invoker invoker)
            {
                this.type = type;
                this.invoker = invoker;
            }
        }
    }
}
