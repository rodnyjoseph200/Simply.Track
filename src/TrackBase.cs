namespace Simply.Track;

public abstract record TrackBase
{
    private DateTimeOffset _createdOn;
    public DateTimeOffset CreatedOn
    {
        get => _createdOn;
        protected set
        {
            if (value == default)
                throw new Exception($"{nameof(CreatedOn)} cannot be default value {value}.");

            _createdOn = value;
        }
    }

    private DateTimeOffset _updatedOn;
    public DateTimeOffset UpdatedOn
    {
        get => _updatedOn;
        protected set
        {
            if (value == default)
                throw new Exception($"{nameof(UpdatedOn)} cannot be default value {value}.");

            _updatedOn = value;
        }
    }

    private DateTimeOffset? _voidOn;
    public DateTimeOffset? VoidOn
    {
        get => _voidOn;
        protected set
        {
            if (value is not null && value == DateTimeOffset.MinValue)
                throw new Exception($"{nameof(VoidOn)} cannot be min value {value}.");
            _voidOn = value;
        }
    }

    public string CreatedBy { get; protected set; }
    public string UpdatedBy { get; protected set; }
    public bool IsVoid { get; protected set; }
    public string? VoidBy { get; protected set; }
    public string? VoidMessage { get; protected set; }
    public VoidReasons? VoidReason { get; protected set; }

    protected TrackBase(DateTimeOffset createdOn, string createdBy, DateTimeOffset updatedOn, string updatedBy, bool isVoid, DateTimeOffset? voidOn, string? voidBy, string? voidMessage, VoidReasons? voidReason)
    {
        CreatedOn = createdOn;
        CreatedBy = createdBy;
        UpdatedOn = updatedOn;
        UpdatedBy = updatedBy;
        IsVoid = isVoid;
        VoidOn = voidOn;
        VoidBy = voidBy;
        VoidMessage = voidMessage;
        VoidReason = voidReason;
    }
}