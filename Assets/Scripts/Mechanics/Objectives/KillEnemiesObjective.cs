using Platformer.Gameplay;
using Platformer.UI;
using UnityEngine;

namespace Mechanics.Objectives
{
    public class KillEnemiesObjective : Objective
    {
        [SerializeField] private int _enemiesToKill = 10;
        private int _enemiesKilled = 0;

        public override bool IsComplete()
        {
            return _enemiesKilled >= _enemiesToKill;
        }

        public override void ResetObjective()
        {
            _enemiesKilled = 0;
            _isComplete = false;
        }

        private void OnEnable()
        {
            EnemyDeath.OnEnemyDeath += OnEnemyKilled;
        }

        private void OnDisable()
        {
            EnemyDeath.OnEnemyDeath -= OnEnemyKilled;
        }

        private void OnEnemyKilled()
        {
            ++_enemiesKilled;
            base.UpdateObjective();

            if (!IsComplete()) return;
            CheckComplete();
        }

        public override void UpdateObjectiveText(ObjectiveText objectiveText)
        {
            objectiveText.UpdateText(_objectiveText, _enemiesKilled, _enemiesToKill);
            if (IsComplete()) objectiveText.CompleteText();
        }
    }
}
