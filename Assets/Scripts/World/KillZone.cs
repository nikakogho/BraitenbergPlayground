using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class KillZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var vehicleLife = collision.transform.GetComponent<VehicleLife>();

        if (vehicleLife != null) vehicleLife.Die();
    }
}
