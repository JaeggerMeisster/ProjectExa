using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exa.Utils;

namespace Exa.AI {
    public class ActionList : IEnumerable<IAction> {
        private readonly List<IAction> actions;
        private readonly float priorityThreshold;

        public ActionList(float priorityThreshold, IEnumerable<IAction> actions) {
            this.actions = new List<IAction>(actions);
            this.priorityThreshold = priorityThreshold;
        }

        public IEnumerator<IAction> GetEnumerator() {
            return actions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Add(IAction action) {
            actions.Add(action);
        }

        public void RunActions() {
            ActionLane mask = 0;

            foreach (var action in SortActions()) {
                var shouldRun = (mask & action.Lanes) == 0;

                if (shouldRun) {
                    action.Blocking = action.Update(mask);
                    mask |= action.Blocking;
                } else {
                    action.Blocking = ActionLane.None;
                }
            }
        }

        private IEnumerable<IAction> SortActions() {
            return actions
                .OrderByDescending(
                    action => {
                        action.UpdatePriority();

                        return action.Priority;
                    }
                )
                .Where(action => action.Priority > priorityThreshold);
        }

        public string ToString(int tabs = 0) {
            var sb = new StringBuilder();

            var tableString = actions
                .OrderByDescending(action => action.Priority)
                .ToStringTable(
                    new[] {
                        "Action name",
                        "Priority",
                        "Blocking lane",
                        "Debug string"
                    },
                    action => action.GetType().Name,
                    action => action.Priority,
                    action => action.Blocking,
                    action => action.DebugString
                );

            sb.AppendLineIndented(tableString, tabs);

            return sb.ToString();
        }
    }
}