namespace Exa.AI {
    public interface IAction {
        /// <summary>
        ///     The lanes required for an action to be updated
        /// </summary>
        ActionLane Lanes { get; }

        /// <summary>
        ///     The lanes that this action blocks
        /// </summary>
        ActionLane Blocking { get; set; }

        /// <summary>
        ///     The priority of this action
        /// </summary>
        float Priority { get; }

        string DebugString { get; }

        /// <summary>
        ///     Update the action
        /// </summary>
        /// <param name="blockedLanes">Currently blocked lanes on the action list</param>
        /// <returns>The lanes that this action has blocked</returns>
        ActionLane Update(ActionLane blockedLanes);

        /// <summary>
        ///     Updates the priority of the action
        /// </summary>
        void UpdatePriority();
    }
}