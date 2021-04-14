using UnityEngine;

public class Survivor : Weapon
{
    public GameObject grenade;
    public GameObject grenadeHolder;
    public LayerMask whatIsGrenadable;
    public float grenadeCoolDown;

    private float grenadeCurrent;

    private GameObject g;
    public override void Ability(PlayerController player) {
        player.GoInvisible(5);
    }

    public override void Special()
    {
        if (grenadeCurrent <= 0)
        {
            grenadeHolder.SetActive(true);
            if (Input.GetMouseButtonDown(1))
            {
                leftArmAnimator.Play("ThrowGrenade");
                grenadeHolder.SetActive(false);
                grenadeCurrent = grenadeCoolDown;
                g = Instantiate(grenade, camera.transform.position, Quaternion.identity);
                g.transform.forward = camera.forward;
            }
        }
        grenadeCurrent -= Time.deltaTime;
    }

    public override string Description()
    {
        return "Survivor: Revolver Does 10 damage per shot, 6 bullets per reload. \n" +
               "          Grenade Launcher deals knockback to player and enemies. \n" +
               "          Ability: Go invisible for 5 seconds";
    }
}
