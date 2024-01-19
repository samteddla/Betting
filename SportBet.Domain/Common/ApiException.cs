using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace SportBet.Domain.Common;

/// <summary>
/// Exception for the whole project
/// </summary>
[Serializable]
public class ApiException : Exception, ISerializable
{
    public string ErrorType { get; init; } = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
    public string Title { get; } = "exception occurred.";
    public object? ExtendedInfo { get; }

    public ApiException()
    {
        ExtendedInfo = new object();
    }

    public ApiException(string message)
        : base(message)
    {
        ExtendedInfo = new object();
    }

    public ApiException(string message, Exception innerException)
        : base(message, innerException)
    {
        ExtendedInfo = new object();
    }

#pragma warning disable S3427 
    public ApiException(
        [NotNull] string message,
        object? extendedInfo = null,
        string? type = null,
        string? title = null)
        : base(message)
    {
        ExtendedInfo = extendedInfo ?? new object();
        ErrorType = type ?? ErrorType;
        Title = title ?? Title;
    }
#pragma warning restore S3427
    protected ApiException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        ErrorType = info.GetString("ErrorType") ?? string.Empty;
        Title = info.GetString("Title") ?? string.Empty;
        ExtendedInfo = info.GetValue("ExtendedInfo", typeof(object)) ?? new object();
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        info.AddValue("Type", ErrorType);
        info.AddValue("Title", Title);
        info.AddValue("ExtendedInfo", ExtendedInfo);
    }
}
