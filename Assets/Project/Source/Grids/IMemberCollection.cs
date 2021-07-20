using System.Collections.Generic;

namespace Exa.Grids {
    public interface IMemberCollection {
        public delegate void MemberChange(IGridMember member);

        public event MemberChange MemberAdded;
        public event MemberChange MemberRemoved;

        public IEnumerable<IGridMember> GetMembers();
    }
}