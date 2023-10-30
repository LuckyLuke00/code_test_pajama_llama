using UnityEngine;

namespace Mechanics.Objectives
{
    public abstract class Objective : MonoBehaviour
    {
        // variable that stores if the objective was completed
        protected bool _isComplete = false;

        // Create an abstract event for when the objective is completed
        public delegate void ObjectiveCompleted();

        public static event ObjectiveCompleted OnObjectiveCompleted;

        public abstract bool IsComplete();

        protected virtual void Complete()
        {
            if (_isComplete) return;

            _isComplete = true;
            OnObjectiveCompleted?.Invoke();
        }

        // Reset
        public abstract void ResetObjective();
    }
}
