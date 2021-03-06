﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Plain.Library.Reflection
{
    /// <summary>
    /// Extensions to the Type type.
    /// </summary>
    public static class TypeExtensions
    {
        public static string ShortAssemblyQualifiedName(this Type type)
        {
            var assemblyName = new AssemblyName(type.Assembly.FullName);
            var shortName = type.FullName + ", " + assemblyName.Name;
            return shortName;
        }
    }
}
