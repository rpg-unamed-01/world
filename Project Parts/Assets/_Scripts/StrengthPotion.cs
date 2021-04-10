public class StrengthPotion : Item
{
    private void Awake()
    {
        price = 20;
    }

    public override string getName()
    {
        return "StrengthPotion (5 s)";
    }

    public override void Use(PlayerController player)
    {
        player.HitHard(5);
    }
}