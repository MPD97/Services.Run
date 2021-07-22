namespace Services.Run.Application.Services
{
    public interface IDistanceMeasure
    {
        double GetDistanceBetween(decimal lat1, decimal lon1, decimal lat2, decimal lon2);
    }
}