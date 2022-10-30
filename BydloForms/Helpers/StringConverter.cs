using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BydloForms.Helpers
{
    public static class StringConverter<T>
    {
        public static readonly TypeConverter Converter = TypeDescriptor.GetConverter(TypeOf<T>.Type);
        public static U GetObjectFromString<U>(string text)
        {
            return (U)Converter.ConvertFromInvariantString(text);
        }
    }
}
