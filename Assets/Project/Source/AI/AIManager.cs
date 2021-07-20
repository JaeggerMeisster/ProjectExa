using System.Collections.Generic;
using UnityEngine;

namespace Exa.AI {
    public class AIManager : MonoBehaviour {
        [SerializeField] private int ticksPerSecond = 10;
        private readonly List<IAgent> agents = new List<IAgent>();
        private float elapsedSinceLastTick;
        private float updateDeltaThreshold;

        private void Awake() {
            updateDeltaThreshold = 1f / ticksPerSecond;
        }

        private void Update() {
            if (elapsedSinceLastTick > updateDeltaThreshold) {
                foreach (var agent in agents) {
                    agent.AIUpdate();
                }

                elapsedSinceLastTick = 0f;
            }

            elapsedSinceLastTick += Time.deltaTime;
        }

        public void Register(IAgent agent) {
            agents.Add(agent);
        }

        public void Unregister(IAgent agent) {
            agents.Remove(agent);
        }
    }
}