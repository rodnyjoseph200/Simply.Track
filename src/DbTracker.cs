namespace Simply.Track;

public record DbTracker
{
    public DateTimeOffset CreatedOn { get; init; }
    public required string CreatedBy { get; init; }
    public DateTimeOffset UpdatedOn { get; init; }
    public required string UpdatedBy { get; init; }
    public bool IsVoid { get; init; }
    public DateTimeOffset? VoidOn { get; init; }
    public string? VoidBy { get; init; }
    public string? VoidMessage { get; init; }
    public VoidReasons? VoidReason { get; init; }

    public static Tracker Convert(DbTracker dbTracker)
    {
        if (dbTracker is null)
            throw new ArgumentNullException(nameof(dbTracker));

        var tracker = Tracker.Load(
            createdOn: dbTracker.CreatedOn,
            createdBy: dbTracker.CreatedBy,
            updatedOn: dbTracker.UpdatedOn,
            updatedBy: dbTracker.UpdatedBy,
            isVoid: dbTracker.IsVoid,
            voidOn: dbTracker.VoidOn,
            voidBy: dbTracker.VoidBy,
            voidMessage: dbTracker.VoidMessage,
            voidReason: dbTracker.VoidReason
        );

        return tracker;
    }

    public static DbTracker Create(string createdBy)
    {
        if (string.IsNullOrWhiteSpace(createdBy))
            throw new ArgumentNullException(nameof(createdBy));

        var now = DateTimeOffset.UtcNow;

        return new DbTracker
        {
            CreatedOn = now,
            CreatedBy = createdBy,
            UpdatedOn = now,
            UpdatedBy = createdBy,
            IsVoid = false
        };
    }

    public static DbTracker Update(Tracker tracker, string updatedBy)
    {
        if (string.IsNullOrWhiteSpace(updatedBy))
            throw new ArgumentNullException(nameof(updatedBy));

        if (tracker.IsVoid)
            throw new Exception($"{nameof(Update)} failed. Tracker is void");

        var now = DateTimeOffset.UtcNow;

        return new DbTracker
        {
            CreatedOn = tracker.CreatedOn,
            CreatedBy = tracker.CreatedBy,
            UpdatedOn = now,
            UpdatedBy = updatedBy,
            IsVoid = tracker.IsVoid,
            VoidOn = tracker.VoidOn,
            VoidBy = tracker.VoidBy,
            VoidMessage = tracker.VoidMessage,
            VoidReason = tracker.VoidReason
        };
    }

    public static DbTracker TrackVoid(Tracker tracker, string updateBy,
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

        return new DbTracker
        {
            CreatedOn = tracker.CreatedOn,
            CreatedBy = tracker.CreatedBy,
            UpdatedOn = now,
            UpdatedBy = updateBy,
            IsVoid = true,
            VoidOn = now,
            VoidBy = voidBy,
            VoidMessage = voidMessage,
            VoidReason = voidReason
        };
    }

    public static DbTracker TrackNoLongerVoid(Tracker tracker, string createdBy)
    {
        if (string.IsNullOrWhiteSpace(createdBy))
            throw new ArgumentNullException(nameof(createdBy));

        if (!tracker.IsVoid)
            throw new Exception($"{nameof(TrackNoLongerVoid)} failed. Tracker is not void.");

        var now = DateTimeOffset.UtcNow;

        return new DbTracker
        {
            CreatedOn = tracker.CreatedOn,
            CreatedBy = tracker.CreatedBy,
            UpdatedOn = now,
            UpdatedBy = createdBy,
            IsVoid = false,
            VoidOn = null,
            VoidBy = null,
            VoidMessage = null,
            VoidReason = null
        };
    }
}