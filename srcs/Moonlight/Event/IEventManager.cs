namespace Moonlight.Event
{
    public interface IEventManager
    {
        void Emit<T>(T notification) where T : IEventNotification;
        void RegisterListener<T>(EventListener<T> listener) where T : IEventNotification;

        void RemoveListener<T>(EventListener<T> listener) where T : IEventNotification;
    }
}