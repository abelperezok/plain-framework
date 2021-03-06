﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plain.Library.Reflection
{
    /// <summary>
    /// A class that uses reflection to instantiate types using their assembly qualified names.
    /// </summary>
    public class PluginInstantiator<TPluginType>
    {
        public IList<TPluginType> GetInstances(IEnumerable<string> typeNames, params object[] args)
        {
            var result = new List<TPluginType>();
            foreach (var typeName in typeNames)
            {
                var type = Type.GetType(typeName, true);
                var plugin = GetInstance(type, args);
                if (plugin != null)
                {
                    result.Add(plugin);
                }
            }
            return result;
        }

        private TPluginType GetInstance(Type type, params object[] args)
        {
            return (TPluginType)Activator.CreateInstance(type, args);
        }

    }
}
