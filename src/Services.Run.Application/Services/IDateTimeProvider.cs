using System;

namespace Services.Run.Application.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}