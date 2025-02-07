namespace Simply.Track;

public record Tracker : TrackBase
{
    private Tracker(DateTimeOffset createdOn, string createdBy, DateTimeOffset updatedOn, string updatedBy,
        bool isVoid, DateTimeOffset? voidOn, string? voidBy, string? voidMessage, VoidReasons? voidReason) :
        base(createdOn, createdBy, updatedOn, updatedBy, isVoid, voidOn, voidBy, voidMessage, voidReason)
    {
    }

    public static Tracker Load(DateTimeOffset createdOn, string createdBy, DateTimeOffset updatedOn, string updatedBy,
        bool isVoid, DateTimeOffset? voidOn = null, string? voidBy = null, string? voidMessage = null, VoidReasons? voidReason = null)
    {
        if (isVoid && (voidOn is null || voidBy is null || voidMessage is null || voidReason is null))
        {
            var message = $"Tracker is void but voidOn, voidBy, voidMessage, and voidReason must be provided. voidOn: {voidOn}, voidBy: {voidBy}, voidMessage: {voidMessage}, voidReason: {voidReason}";
            throw new ArgumentNullException(message);
        }

        return new Tracker(createdOn, createdBy, updatedOn, updatedBy, isVoid, voidOn, voidBy, voidMessage, voidReason);
    }
}