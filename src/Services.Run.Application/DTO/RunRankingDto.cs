using System;

namespace Services.Run.Application.DTO
{
    public class RunRankingDto
    {
        public Guid UserId { get; }
        public DateTime RunDate { get; }
        public TimeSpan Time { get; }

        public RunRankingDto(Guid userId, DateTime runDate, TimeSpan time)
        {
            UserId = userId;
            RunDate = runDate;
            Time = time;
        }
    }
}