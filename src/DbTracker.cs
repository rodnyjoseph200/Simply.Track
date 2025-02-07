namespace Simply.Track;

public record DbTracker : TrackBase
{
    private DbTracker(DateTimeOffset createdOn, string createdBy, DateTimeOffset updatedOn, string updatedBy,
        bool isVoid, DateTimeOffset? voidOn, string? voidBy, string? voidMessage, VoidReasons? voidReason) :
        base(createdOn, createdBy, updatedOn, updatedBy, isVoid, voidOn, voidBy, voidMessage, voidReason)
    { }

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
        return new(now, createdBy, now, createdBy, false, null, null, null, null);
    }

    public static DbTracker Update(Tracker tracker, string updatedBy)
    {
        if (string.IsNullOrWhiteSpace(updatedBy))
            throw new ArgumentNullException(nameof(updatedBy));

        if (tracker.IsVoid)
            throw new Exception($"{nameof(Update)} failed. Tracker is void");

        var now = DateTimeOffset.UtcNow;

        return new(
            createdOn: tracker.CreatedOn,
            createdBy: tracker.CreatedBy,
            updatedOn: now,
            updatedBy: updatedBy,
            isVoid: tracker.IsVoid,
            voidOn: tracker.VoidOn,
            voidBy: tracker.VoidBy,
            voidMessage: tracker.VoidMessage,
            voidReason: tracker.VoidReason
        );
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

        return new(
            createdOn: tracker.CreatedOn,
            createdBy: tracker.CreatedBy,
            updatedOn: now,
            updatedBy: updateBy,
            isVoid: true,
            voidOn: now,
            voidBy: voidBy,
            voidMessage: voidMessage,
            voidReason: voidReason
        );
    }

    public static DbTracker TrackNoLongerVoid(Tracker tracker, string createdBy)
    {
        if (string.IsNullOrWhiteSpace(createdBy))
            throw new ArgumentNullException(nameof(createdBy));

        if (!tracker.IsVoid)
            throw new Exception($"{nameof(TrackNoLongerVoid)} failed. Tracker is not void.");

        var now = DateTimeOffset.UtcNow;

        return new(
            createdOn: tracker.CreatedOn,
            createdBy: tracker.CreatedBy,
            updatedOn: now,
            updatedBy: createdBy,
            isVoid: false,
            voidOn: null,
            voidBy: null,
            voidMessage: null,
            voidReason: null
        );
    }
}