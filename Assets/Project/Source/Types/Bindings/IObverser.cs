namespace Exa.Types.Binding {
    public interface IObserver<T> {
        void OnUpdate(T data);
    }
}