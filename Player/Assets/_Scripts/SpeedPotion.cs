public class SpeedPotion : Item
{
    private void Awake()
    {
        price = 15;
    }

    public override string getName()
    {
        return "SpeedPotion (5 s)";
    }

    public override void Use(PlayerController player)
    {
        player.GoFast(5);
    }
}
