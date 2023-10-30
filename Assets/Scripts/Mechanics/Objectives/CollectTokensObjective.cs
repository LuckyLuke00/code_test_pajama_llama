using Platformer.Mechanics;
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
            if (!IsComplete()) return;
            Complete();
        }
    }
}
