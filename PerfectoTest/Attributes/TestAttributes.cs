using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace PerfectoTest.Attributes
{
    public static class TestAttributes
    {
        public static IEnumerable<TAttribute> GetAll<TAttribute>() where TAttribute : Attribute
        {
            IMethodInfo method = TestExecutionContext.CurrentContext.CurrentTest.Method;
            return ((IEnumerable<TAttribute>) method.GetCustomAttributes<TAttribute>(true)).Concat<TAttribute>((IEnumerable<TAttribute>) method.TypeInfo.GetCustomAttributes<TAttribute>(true));
        }
        public static TAttribute Get<TAttribute>() where TAttribute : Attribute
        {
            return TestAttributes.GetAll<TAttribute>().SingleOrDefault<TAttribute>();
        }
    }
}
