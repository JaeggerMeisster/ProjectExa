using UCommandConsole;

namespace Exa.Debugging.Commands {
    public class ToggleDiagnosticsCommand : Command {
        public override string GetName() {
            return "tgl-diag";
        }

        public override void Execute(Console host) {
            var dp = S.UI.Diagnostics.gameObject;
            dp.SetActive(!dp.activeSelf);
        }
    }
}