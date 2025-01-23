namespace Simply.Track
{
    public class TrackedUser
    {
        public string UserName { get; init; }

        private TrackedUser(string userName) => UserName = userName;

        public static TrackedUser Create(string userName) => new(userName);
    }
}
