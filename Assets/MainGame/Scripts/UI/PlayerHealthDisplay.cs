public class PlayerHealthDisplay : HealthDisplay
{   
    
    
    protected override void Update()
    {
        // UpdateHealth();
        UpdateBlock();
    }

    protected override void UpdateBlock()
    {
        if (blockDurability == null)
        {
            var player = FindObjectOfType<PlayerStateMachine>();
            blockDurability = player.BlockDurability;
            health          = player.Health;
        }
        base.UpdateBlock();
        
    }
}