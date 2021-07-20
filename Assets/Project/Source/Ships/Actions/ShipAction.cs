namespace Exa.Ships {
    /// <summary>
    ///     Base class for actions a grid can make that use energy
    /// </summary>
    public abstract class ShipAction {
        protected GridInstance gridInstance;

        protected ShipAction(GridInstance gridInstance) {
            this.gridInstance = gridInstance;
        }

        public abstract float CalculateConsumption(float deltaTime);

        /// <summary>
        ///     Execute the action with the given energy coefficient
        /// </summary>
        /// <param name="energyNormalization">
        ///     0-1 based float that determines how much energy the action can use.
        ///     1 being fully powered, while 0 essentially not doing anything
        /// </param>
        public abstract void Update(float energyCoefficient, float deltaTime);
    }
}