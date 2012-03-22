using System;

namespace GoCardlessSdk.Api
{
    public class UserResponse
    {
        public DateTimeOffset CreatedAt { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}