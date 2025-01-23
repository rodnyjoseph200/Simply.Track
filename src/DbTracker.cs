namespace Simply.Track;

public record DbTracker : TrackBase
{
    private DbTracker(DateTimeOffset createdOn, string createdBy, DateTimeOffset updatedOn, string updatedBy,
        bool isVoid, DateTimeOffset? voidOn, string? voidBy, string? voidMessage, VoidReasons? voidReason) :
        base(createdOn, createdBy, updatedOn, updatedBy, isVoid, voidOn, voidBy, voidMessage, voidReason)
    { }

    public static DbTracker Create(string createdBy)
    {
        if (string.IsNullOrWhiteSpace(createdBy))
            throw new ArgumentNullException(nameof(createdBy));

        var now = DateTimeOffset.UtcNow;
        return new DbTracker(now, createdBy, now, createdBy, false, null, null, null, null);
    }

    public void TrackUpdate(Tracker tracker, string updatedBy)
    {
        if (string.IsNullOrWhiteSpace(updatedBy))
            throw new ArgumentNullException(nameof(updatedBy));

        if (tracker.IsVoid)
            throw new Exception($"{nameof(TrackUpdate)} failed. Tracker is void");

        CreatedOn = tracker.CreatedOn;
        CreatedBy = tracker.CreatedBy;
        UpdatedOn = DateTimeOffset.UtcNow;
        UpdatedBy = updatedBy;
    }

    public void TrackVoid(Tracker tracker, string updateBy,
        string voidBy, string voidMessage, VoidReasons voidReason)
    {
        if (string.IsNullOrWhiteSpace(updateBy))
            throw new ArgumentNullException(nameof(updateBy));

        if (string.IsNullOrWhiteSpace(voidBy))
            throw new ArgumentNullException(nameof(voidBy));

        if (string.IsNullOrWhiteSpace(voidMessage))
            throw new ArgumentNullException(nameof(voidMessage));

        if (tracker.IsVoid)
            throw new Exception($"{nameof(TrackVoid)} failed. Tracker is already void.");

        var now = DateTimeOffset.UtcNow;
        CreatedOn = tracker.CreatedOn;
        CreatedBy = tracker.CreatedBy;
        UpdatedOn = now;
        UpdatedBy = updateBy;
        IsVoid = true;
        VoidOn = now;
        VoidBy = voidBy;
        VoidMessage = voidMessage;
        VoidReason = voidReason;
    }

    public void TrackNoLongerVoid(Tracker tracker, string createdBy)
    {
        if (string.IsNullOrWhiteSpace(createdBy))
            throw new ArgumentNullException(nameof(createdBy));

        if (!tracker.IsVoid)
            throw new Exception($"{nameof(TrackNoLongerVoid)} failed. Tracker is not void.");

        var now = DateTimeOffset.UtcNow;

        CreatedOn = tracker.CreatedOn;
        CreatedBy = tracker.CreatedBy;
        UpdatedOn = now;
        UpdatedBy = createdBy;
        VoidOn = null;
        VoidBy = null;
        VoidMessage = null;
        VoidReason = null;
    }
}

