namespace NotificationService.Helpers
{
    public class Utils
    {
        public const string DefaultConnectionKeyName = "DefaultConnection";
        public const string ConnectionStringsSectionName = "ConnectionStrings";

        public const string CreateValidationRuleSetName = "Create";

        public const string JsonContentType = "application/json";
        public const string API_VERSION_1 = "1.0";
        public const string API_BERSION_2 = "2.0";

        public const string PurchaseMessage = "{0} on {1}, Card is {2}";
        public const string VerifyCard = "{0} on {1}, Card is {2}";
        public const string SendOtp = "Your verification code is (some code) to Card {0}";

        public const string HealthCheckEndpoint = "_health";
        public static string HealthCheckName => "DataBase";
        public static string HealthCheckErrorMessage => "DataBase is unhealthy";

        public const string ApiVersionTag = "api-version";


    }
}
