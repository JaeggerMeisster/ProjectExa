namespace Exa.Grids.Blocks.BlockTypes {
    public class Controller : Block {
        protected override void OnAdd() {
            base.OnAdd();

            if (!GridInstance) {
                return;
            }

            GridInstance.Controller = this;
        }

        protected override void OnRemove() {
            base.OnRemove();

            if (!GridInstance) {
                return;
            }

            GridInstance.OnControllerDestroyed();
            GridInstance.Controller = null;
        }
    }
}