namespace Prism.Core.WebApi.Constants;

public static class DbConstants
{
    public static class TableNames
    {
        public const string Users = "users";

        public const string Themes = "themes";

        public const string ThemeVersions = "theme_versions";

        public const string ThemeFields = "theme_fields";

        public const string ThemeFieldVersions = "theme_field_versions";

        public const string Records = "records";

        public const string RecordVersions = "record_versions";

        public const string RecordValues = "record_values";

        public const string RecordValueVersions = "record_value_versions";

        public const string UserRates = "user_rates";

        public const string Rates = "rates";
    }

    public static class ColumnNames
    {

    }

    public static class ColumnSettings
    {
        public const int DefaultNameMaxLength = 64;
    }
}
