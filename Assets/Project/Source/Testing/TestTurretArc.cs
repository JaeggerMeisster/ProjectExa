using Exa.Gameplay;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Testing
{
    public class TestTurretArc : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera targetCamera;
        [SerializeField] private Autocannon autocannon;
        [SerializeField] private AutocannonTemplate template;

        private void Awake() {
            template.SetContextlessValues(autocannon);
            autocannon.turretBehaviour.AutoFire = false;
            autocannon.ForceActive();
            autocannon.SetTarget(new MouseCursorTarget(targetCamera));
        }
    }
}
