namespace Exa.Generics {
    public interface ICloneable<T>
        where T : class {
        T Clone();
    }
}