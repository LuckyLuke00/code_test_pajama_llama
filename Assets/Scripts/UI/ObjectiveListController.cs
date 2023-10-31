using Mechanics.Objectives;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UI
{
    public class ObjectiveListController : MonoBehaviour
    {
        [SerializeField] private GameObject _objectivePrefab;

        // Store the vertical layout group component
        [SerializeField] private VerticalLayoutGroup _layoutGroup;

        private readonly List<Objective> _objectives = new List<Objective>();
        private readonly List<ObjectiveText> _objectiveTexts = new List<ObjectiveText>();

        private void Awake()
        {
            // Check if the objective prefab is set and contains the ObjectiveText script
            if (!_objectivePrefab)
            {
                Debug.LogError("ObjectiveListController: Objective prefab is not set!");
                enabled = false;
                return;
            }

            if (!_objectivePrefab.GetComponent<ObjectiveText>())
            {
                Debug.LogError("ObjectiveListController:  Objective prefab does not contain ObjectiveText script!");
                enabled = false;
                return;
            }

            // Check if the layout group is set
            if (!_layoutGroup)
            {
                Debug.LogError("ObjectiveListController: Layout group is not set!");
                enabled = false;
            }
        }

        public void AddObjective(Objective objective)
        {
            if (_objectives.Contains(objective)) return;
            _objectives.Add(objective);

            // Instantiate the objective prefab and add it to the layout group
            GameObject objectiveText = Instantiate(_objectivePrefab, _layoutGroup.transform);

            // Try to get the ObjectiveText component from the instantiated objective prefab
            if (!objectiveText.TryGetComponent(out ObjectiveText objectiveTextComponent))
            {
                Debug.LogError("ObjectiveListController: Objective prefab does not contain ObjectiveText script!");
                return;
            }

            // Add the ObjectiveText component to the list
            _objectiveTexts.Add(objectiveTextComponent);

            UpdateObjectiveList();
        }

        public void UpdateObjectiveList()
        {
            foreach (var objective in _objectives)
            {
                int index = _objectives.IndexOf(objective);
                objective.UpdateObjectiveText(_objectiveTexts[index]);
            }
        }
    }
}
