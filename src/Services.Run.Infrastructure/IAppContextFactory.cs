using Services.Run.Application;

namespace Services.Run.Infrastructure
{
    public interface IAppContextFactory
    {
        IAppContext Create();
    }
}