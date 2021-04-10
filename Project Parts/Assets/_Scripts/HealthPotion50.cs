public class HealthPotion50 : Item
{
    private void Awake()
    {
        price = 25;
    }
    public override string getName() {
        return "Health Potion (50 hp)";
    }

    public override void Use(PlayerController player) {
        player.AddHealth(50);
    }
}
