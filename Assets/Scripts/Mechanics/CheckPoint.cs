using UnityEngine;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Collider2D))]
    public class CheckPoint : MonoBehaviour
    {
        public bool Activated { get; private set; } = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            // We can't do a tag check because enemies also have a player tag for some reason
            if (!Activated && other.gameObject.TryGetComponent(out PlayerController playerController))
            {
                Activated = true;
            }
        }

        public void ResetCheckpoint()
        {
            Activated = false;
        }
    }
}
