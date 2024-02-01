namespace SquadForger.Services
{
    public class ServiceLocator
    {
        private static readonly ServiceLocator _instance = new ServiceLocator();
        public static ServiceLocator Instance => _instance;

        public EventAggregator EventAggregator { get; } = new EventAggregator();
    }
}