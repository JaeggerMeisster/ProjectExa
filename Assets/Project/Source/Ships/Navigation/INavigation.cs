using Exa.Ships.Targeting;

namespace Exa.Ships.Navigation {
    public interface INavigation {
        ITarget LookAt { set; }
        ITarget MoveTo { set; }
        IThrustVectors ThrustVectors { get; }

        void Update(float deltaTime);
    }
}