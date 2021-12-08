namespace Banks
{
    public interface IObservable
    {
        void AddObserver(IObserver iObserver);
        void RemoveObserver(IObserver iObserver);
        void NotifyObservers(string message);
    }
}