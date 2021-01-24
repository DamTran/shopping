namespace Taste.Enums
{
    using System.ComponentModel;

    public enum ErrorCodeEnum
    {
        [Description("An error occurred while processing your request.")]
        UnhandledError = 1,
    }
}