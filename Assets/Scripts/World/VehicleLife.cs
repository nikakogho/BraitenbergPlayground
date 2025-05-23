using Simulation;
using UnityEngine;
using World;

[RequireComponent(typeof(VehicleBody))]
public class VehicleLife : MonoBehaviour
{
    public float Age { get; private set; } = 0f;
    public bool IsAlive { get; private set; } = true;

    private void Update()
    {
        if (IsAlive) Age += Time.deltaTime;
    }

    public void Die()
    {
        if (!IsAlive) return;

        IsAlive = false;
        SelectionArenaManager.Instance.NotifyDeath(this);
        Destroy(gameObject);
    }
}
