using Exa.AI;
using Exa.Gameplay;
using Exa.Gameplay.Missions;
using Exa.Ships;
using Exa.UI.Components;
using Exa.UI.Gameplay;
using Exa.Utils;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa {
    public class GS : MonoSingleton<GS> {
        [SerializeField] private Raycaster raycaster;
        [SerializeField] private Navigateable navigateable;
        [SerializeField] private ShipFactory shipFactory;
        [SerializeField] private GameplayUI gameplayUI;
        [SerializeField] private AIManager aIManager;
        [SerializeField] private SpawnLayer spawnLayer;
        [SerializeField] private BlockGridManager blockGridManager;
        [SerializeField] private PopupManager popupManager;
        [SerializeField] private MissionManager missionManager;

        public static Raycaster Raycaster {
            get => Instance.raycaster;
        }

        public static Navigateable Navigateable {
            get => Instance.navigateable;
        }

        public static ShipFactory ShipFactory {
            get => Instance.shipFactory;
        }

        public static GameplayUI UI {
            get => Instance.gameplayUI;
        }

        public static AIManager AI {
            get => Instance.aIManager;
        }

        public static SpawnLayer SpawnLayer {
            get => Instance.spawnLayer;
        }

        public static BlockGridManager BlockGridManager {
            get => Instance.blockGridManager;
        }

        public static PopupManager PopupManager {
            get => Instance.popupManager;
        }

        public static MissionManager MissionManager {
            get => Instance.missionManager;
        }

        public static GameObject GameObject {
            get => Instance.gameObject;
        }

        public static bool IsQuitting {
            get => S.IsQuitting || S.Scenes.GetSceneIsUnloading("Game");
        }

        public static bool IsPaused {
            get => UI.pauseMenu.Paused || MissionManager.IsEditing;
        }
    }
}