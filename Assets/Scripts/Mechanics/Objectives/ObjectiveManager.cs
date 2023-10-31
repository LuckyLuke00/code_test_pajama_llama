using Platformer.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.Objectives
{
    public class ObjectiveManager : MonoBehaviour
    {
        private List<Objective> _objectives = new List<Objective>();
        private ObjectiveListController _objectiveListController;

        private void Awake()
        {
            _objectives.AddRange(GetComponents<Objective>());
            if (_objectives.Count == 0)
            {
                Debug.LogWarning("No objectives found on ObjectiveManager");
                enabled = false;
                return;
            }

            _objectiveListController = FindObjectOfType<ObjectiveListController>();
            if (!_objectiveListController)
            {
                Debug.LogWarning("No ObjectiveListController found in scene");
            }

            // For every objective, add it to the objective list controller
            if (_objectiveListController)
            {
                foreach (var objective in _objectives)
                {
                    _objectiveListController.AddObjective(objective);
                }
            }
        }

        private void OnEnable()
        {
            Objective.OnObjectiveUpdated += UpdateObjectives;
        }

        private void OnDisable()
        {
            Objective.OnObjectiveUpdated -= UpdateObjectives;
        }

        private void UpdateObjectives()
        {
            if (!_objectiveListController) return;
            _objectiveListController.UpdateObjectiveList();
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
