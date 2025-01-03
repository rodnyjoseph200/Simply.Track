namespace Simply.Track
{
    public abstract record TrackBase
    {
        public DateTimeOffset CreatedOn { get; protected set; }
        public string? CreatedBy { get; protected set; }
        public DateTimeOffset UpdatedOn { get; protected set; }
        public string? UpdatedBy { get; protected set; }
        public bool IsVoid { get; protected set; }
        public DateTimeOffset? VoidOn { get; protected set; }
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
}
