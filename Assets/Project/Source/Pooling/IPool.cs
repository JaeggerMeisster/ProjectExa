namespace Exa.Pooling {
    public interface IPool<out T> : IPool
        where T : PoolMember {
        T Retrieve();
    }

    public interface IPool {
        void Configure(PoolSettings poolSettings);

        bool Return(PoolMember poolMember);

        void OnDestroyMember();
    }
}