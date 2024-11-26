namespace MNF_PORTAL_Core.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsList(string module)
        {

            return new List<string>
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Update",
                $"Permissions.{module}.Delete"
            };
        }

        public static List<string> GenerateAllPermissions()
        {
            var allpermissions = new List<string>();
            var modules = Enum.GetValues(typeof(Modules));
            foreach (var module in modules)
                allpermissions.AddRange(GeneratePermissionsList(module.ToString()));

            return allpermissions;
        }

    }
}
