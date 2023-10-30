using Mechanics.Objectives;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        private ObjectiveManager _objectiveManager;

        private void Awake()
        {
            _objectiveManager = FindObjectOfType<ObjectiveManager>();

            if (!_objectiveManager)
            {
                Debug.LogWarning("No ObjectiveManager found in scene");
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            // Null check
            if (_objectiveManager && !_objectiveManager.AreAllObjectivesComplete()) return;

            var p = collider.gameObject.GetComponent<PlayerController>();
            if (p != null)
            {
                var ev = Schedule<PlayerEnteredVictoryZone>();
                ev.victoryZone = this;
            }
        }
    }
}
