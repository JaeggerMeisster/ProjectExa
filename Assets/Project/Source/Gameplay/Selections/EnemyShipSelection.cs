namespace Exa.Gameplay {
    public class EnemyShipSelection : ShipSelection {
        public EnemyShipSelection(Formation formation)
            : base(formation) { }

        public override ShipSelection Clone() {
            var selection = new EnemyShipSelection(formation);

            foreach (var ship in this) {
                selection.Add(ship);
            }

            return selection;
        }
    }
}