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
}
