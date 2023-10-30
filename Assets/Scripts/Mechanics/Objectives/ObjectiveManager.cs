using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.Objectives
{
    public class ObjectiveManager : MonoBehaviour
    {
        private List<Objective> _objectives = new List<Objective>();

        private void Awake()
        {
            _objectives.AddRange(GetComponents<Objective>());
            if (_objectives.Count == 0)
            {
                Debug.LogWarning("No objectives found on ObjectiveManager");
                enabled = false;
                return;
            }
        }

        private void OnEnable()
        {
            Objective.OnObjectiveCompleted += OnObjectiveCompleted;
        }

        private void OnDisable()
        {
            Objective.OnObjectiveCompleted -= OnObjectiveCompleted;
        }

        private void OnObjectiveCompleted()
        {
            foreach (var objective in _objectives)
            {
                if (!objective.IsComplete())
                {
                    return;
                }
            }

            Debug.Log("All objectives complete!");
        }

        public void ResetObjectives()
        {
            foreach (var objective in _objectives)
            {
                objective.ResetObjective();
            }
        }

        public bool AreAllObjectivesComplete()
        {
            foreach (var objective in _objectives)
            {
                if (!objective.IsComplete())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
