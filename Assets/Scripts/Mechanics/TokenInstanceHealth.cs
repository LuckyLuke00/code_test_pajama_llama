namespace Platformer.Mechanics
{
    public class TokenInstanceHealth : TokenInstance
    {
        protected override void OnPlayerEnter(PlayerController player)
        {
            if (player.TryGetComponent(out Health health))
            {
                health.Increment();
            }

            base.OnPlayerEnter(player);
        }
    }
}
