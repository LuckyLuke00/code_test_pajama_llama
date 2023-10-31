using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This class exposes the the game model in the inspector, and ticks the
    /// simulation.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        //This model field is public and can be therefore be modified in the
        //inspector.
        //The reference actually comes from the InstanceRegister, and is shared
        //through the simulation and events. Unity will deserialize over this
        //shared reference when the scene loads, allowing the model to be
        //conveniently configured inside the inspector.
        public PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        private CheckPoint[] _checkPoints;

        private void Awake()
        {
            _checkPoints = FindObjectsOfType<CheckPoint>();
            if (_checkPoints.Length == 0)
            {
                Debug.LogWarning("No CheckPoints found in the scene.");
            }
        }

        private void OnEnable()
        {
            Instance = this;
        }

        private void OnDisable()
        {
            if (Instance == this) Instance = null;
        }

        private void Update()
        {
            if (Instance == this) Simulation.Tick();
        }

        // Function that returns the furthest activated checkpoint
        public Transform GetBestCheckPoint()
        {
            Transform bestCheckpoint = model.spawnPoint;
            foreach (CheckPoint checkPoint in _checkPoints)
            {
                Debug.Log(checkPoint.Activated);
                if (!checkPoint.Activated) continue;

                if (checkPoint.transform.position.x > bestCheckpoint.position.x)
                {
                    bestCheckpoint = checkPoint.transform;
                }
            }

            return bestCheckpoint;
        }
    }
}
