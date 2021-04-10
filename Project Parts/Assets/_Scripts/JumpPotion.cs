public class JumpPotion : Item
{
    private void Awake()
    {
        price = 15;
    }

    public override string getName()
    {
        return "JumpPotion (5 s)";
    }

    public override void Use(PlayerController player)
    {
        player.JumpHigh(5);
    }
}
