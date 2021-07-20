namespace Exa.UI.Components {
    public interface INavigateable {
        void OnExit();

        void OnNavigate(INavigateable navigateable);
    }
}