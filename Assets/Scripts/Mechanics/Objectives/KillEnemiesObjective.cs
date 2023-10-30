using Platformer.Gameplay;
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
            if (!IsComplete()) return;
            Complete();
        }
    }
}
