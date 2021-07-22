using System;

namespace Services.Run.Application.Exceptions
{
    public class UserNotFoundException : AppException
    {
        public override string Code { get; } = "user_not_found";
        public Guid UserId { get; }

        public UserNotFoundException(Guid userId) 
            : base($"User with id: {userId} was not found.")
        {
            UserId = userId;
        }
    }
}