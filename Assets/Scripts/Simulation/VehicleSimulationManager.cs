using UnityEngine;

namespace Simulation
{
    public class VehicleSimulationManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _vehiclePrefab;

        [SerializeField]
        private Transform _vehicleSpawnPoint;

        private GameObject _vehicle;

        public void Spawn()
        {
            if (_vehicle != null) return;
            _vehicle = Instantiate(_vehiclePrefab, _vehicleSpawnPoint.position, _vehicleSpawnPoint.rotation);
        }

        public void Reset()
        {
            if (_vehicle != null)
            {
                Destroy(_vehicle);
            }
            _vehicle = null;
        }
    }
}
