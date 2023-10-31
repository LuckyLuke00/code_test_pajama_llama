using Platformer.Mechanics;
using Platformer.UI;
using UnityEngine;

namespace Mechanics.Objectives
{
    public class CollectTokensObjective : Objective
    {
        [SerializeField] private int _tokensToCollect = 10;
        private int _tokensCollected = 0;

        public override bool IsComplete()
        {
            return _tokensCollected >= _tokensToCollect;
        }

        public override void ResetObjective()
        {
            _tokensCollected = 0;
            _isComplete = false;
        }

        private void OnEnable()
        {
            TokenInstance.OnTokenCollected += OnTokenCollected;
        }

        private void OnDisable()
        {
            TokenInstance.OnTokenCollected -= OnTokenCollected;
        }

        private void OnTokenCollected()
        {
            ++_tokensCollected;
            CheckComplete();

            base.UpdateObjective();
        }

        public override void UpdateObjectiveText(ObjectiveText objectiveText)
        {
            objectiveText.UpdateText(_objectiveText, _tokensCollected, _tokensToCollect);

            if (_isComplete) objectiveText.CompleteText();
            else objectiveText.ResetText();
        }
    }
}
