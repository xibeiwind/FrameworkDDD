﻿namespace GeekTime
{
    public interface IKnownException
    {
        string Message { get; }

        int ErrorCode { get; }

        object[] ErrorData { get; }
    }
}
