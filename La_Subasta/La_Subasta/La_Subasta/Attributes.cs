using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace La_Subasta
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ResourceAttribute : Attribute
    {
        public string ResourceType { get; }

        public ResourceAttribute(string resourceType)
        {
            ResourceType = resourceType;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class RoleAttribute : Attribute
    {
        public string RequiredRole { get; }

        public RoleAttribute(string requiredRole)
        {
            RequiredRole = requiredRole;
        }
    }

}
