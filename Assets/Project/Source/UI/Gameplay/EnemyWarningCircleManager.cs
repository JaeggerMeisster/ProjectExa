using System.Collections.Generic;
using Exa.Ships;
using Exa.Utils;
using UnityEngine;

namespace Exa.Project.Source.UI.Gameplay {
    public interface IEnemyWarningCircleSource {
        Vector2 GetPosition();

        float GetSize();
    }

    public class EnemyWarningCircleManager : MonoBehaviour {
        [SerializeField] private GameObject enemyWarningCirclePrefab;

        private IDictionary<IEnemyWarningCircleSource, EnemyWarningCircle> circles;

        private void Awake() {
            circles = new Dictionary<IEnemyWarningCircleSource, EnemyWarningCircle>();
        }

        private void Update() {
            foreach (var view in circles.Values) {
                view.UpdateLocals();
            }
        }

        public void Add(IEnemyWarningCircleSource source) {
            var circle = enemyWarningCirclePrefab.Create<EnemyWarningCircle>(transform);
            circle.Setup(source);
            circles.Add(source, circle);
            
            // Debug.Log($"Function: Add, {(source as EnemyGrid).gameObject.name}");
        }

        public void Remove(IEnemyWarningCircleSource source) {
            if (!circles.ContainsKey(source)) {
                throw new KeyNotFoundException($"Cannot remove source: {(source as MonoBehaviour).gameObject.name}");
            }
            
            circles[source].gameObject.Destroy();
            circles.Remove(source);
            
            // Debug.Log($"Function: Remove, {(source as EnemyGrid).gameObject.name}");
        }
    }
}