using System.Reflection;

namespace La_Subasta
{
    public static class AccessControl
    {
        public static bool CheckAccess(object target, User user)
        {
            var targetType = target.GetType();

            var resourceAttribute = targetType.GetCustomAttribute<ResourceAttribute>();
            var roleAttribute = targetType.GetCustomAttribute<RoleAttribute>();

            if (resourceAttribute != null && roleAttribute != null)
            {
                // Verificar si el usuario tiene el rol requerido para el recurso
                if (user.Role == roleAttribute.RequiredRole)
                {
                    Console.WriteLine($"El usuario con rol '{user.Role}' tiene acceso a la '{resourceAttribute.ResourceType}'.");
                    Console.ReadLine();
                    return true;
                }
                else
                {
                    Console.WriteLine($"Acceso denegado, ya que el usuario con rol '{user.Role}' no tiene permisos para la '{resourceAttribute.ResourceType}'.");
                    Console.ReadLine();
                }
            }

            return false;
        }
    }
}
