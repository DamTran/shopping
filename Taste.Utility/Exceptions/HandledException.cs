namespace Taste.Utility.Exceptions
{
    using System;
    using Taste.Utility.Enums;
    using Taste.Utility.Helpers;

    public sealed class HandledException : Exception
    {
        public ErrorCodeEnum ErrorCode { get; }
        public int ErrorNumber => (int)ErrorCode;
        public override string Message => EnumHelpers.GetDescription(ErrorCode);

        public HandledException(ErrorCodeEnum errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}