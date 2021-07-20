namespace Exa.Grids {
    public abstract class MemberCollectionListener<T>
        where T : IMemberCollection {
        protected T source;

        protected MemberCollectionListener(T source) {
            this.source = source;
        }

        public void AddListeners() {
            source.MemberAdded += OnMemberAdded;
            source.MemberRemoved += OnMemberRemoved;
        }

        public void RemoveListeners() {
            source.MemberAdded -= OnMemberAdded;
            source.MemberRemoved -= OnMemberRemoved;
        }

        protected abstract void OnMemberAdded(IGridMember member);

        protected abstract void OnMemberRemoved(IGridMember member);

        public virtual void Reset() {
            foreach (var member in source.GetMembers()) {
                OnMemberAdded(member);
            }
        }
    }
}