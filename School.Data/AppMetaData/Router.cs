namespace School.Data.AppMetaData;
public static class Router
{
    public const string root = "Api";
    public const string version = "V1";
    public const string rule = $"{root}/{version}";

    public static class StudentRouting
    {
        public const string Prefix = $"{rule}/Student";
        public const string List = $"{Prefix}/List";
        public const string GetById = Prefix + "/{id}";
        public const string Create = Prefix + "/Create";
        public const string Edit = Prefix + "/Edit";
        public const string Delete = Prefix + "/{id}";
        public const string Paginated = Prefix + "/Paginated";
    }

    public static class DepartmentRouting
    {
        public const string Prefix = $"{rule}/Department";
        public const string GetById = Prefix;
    }
    public static class UserRouting
    {
        public const string Prefix = $"{rule}/User";
        public const string Create = Prefix + "/Create";
        public const string GetById = Prefix + "/{id}";
        public const string List = $"{Prefix}/List";

    }
}
