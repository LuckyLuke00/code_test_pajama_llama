using Platformer.UI;
using UnityEngine;

namespace Mechanics.Objectives
{
    public abstract class Objective : MonoBehaviour
    {
        protected bool _isComplete = false;

        public delegate void ObjectiveUpdated();

        public static event ObjectiveUpdated OnObjectiveUpdated;

        [SerializeField] protected string _objectiveText = string.Empty;

        public abstract bool IsComplete();

        protected virtual void CheckComplete()
        {
            if (_isComplete) return;
            _isComplete = IsComplete();
        }

        protected virtual void UpdateObjective()
        {
            OnObjectiveUpdated?.Invoke();
        }

        // Reset
        public abstract void ResetObjective();

        public abstract void UpdateObjectiveText(ObjectiveText objectiveText);
    }
}
