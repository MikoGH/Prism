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
        public const string Id = "id";

        public const string UserId = "user_id";

        public const string ThemeId = "theme_id";

        public const string ThemeFieldId = "theme_field_id";

        public const string RecordId = "record_id";

        public const string RecordValueId = "record_value_id";

        public const string RateId = "rate_id";

        public const string Name = "name";

        public const string ImageUrl = "image_url";

        public const string Password = "password";

        public const string Priority = "priority";

        public const string Type = "type";

        public const string Role = "role";

        public const string Value = "value";

        public const string Letter = "letter";

        public const string Color = "color";

        public const string WriteDate = "write_date";

        public const string IsAccepted = "is_accepted";

        public const string IsDeleted = "is_deleted";

        public const string IntValue = "int_value";

        public const string DoubleValue = "double_value";

        public const string StringValue = "string_value";

        public const string BoolValue = "bool_value";

        public const string DateValue = "date_value";
    }

    public static class ColumnSettings
    {
        public const int DefaultNameMaxLength = 64;
    }
}
