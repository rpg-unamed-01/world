public class InvisPotion1 : Item
{
    private void Awake()
    {
        price = 20;
    }
    public override string getName()
    {
        return "Invisibility Potion (1 s)";
    }

    public override void Use(PlayerController player)
    {
        player.GoInvisible(1);
    }
}

