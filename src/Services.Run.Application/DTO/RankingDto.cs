using System;

namespace Services.Run.Application.DTO
{
    public class RankingDto
    {
        public Guid UserId { get; }
        public DateTime? RunDate { get; }
        public TimeSpan? Time { get; }

        public RankingDto(Guid userId, DateTime? runDate, TimeSpan? time)
        {
            UserId = userId;
            RunDate = runDate;
            Time = time;
        }
    }
}