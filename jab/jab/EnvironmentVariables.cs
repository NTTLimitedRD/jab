namespace jab
{
    /// <summary>
    /// Constants.
    /// </summary>
    public static class EnvironmentVariables
    {
        /// <summary>
        /// Environment variable passing the API base URL into the tests.
        /// </summary>
        public static string BaseUrl = "JAB_API_BASE_URL";

        /// <summary>
        /// Environment variable containng a boolean whether to run active tests or not.
        /// </summary>
        public static string ActiveTestsFlag = "JAB_ACTIVE_TESTS";
    }
}
