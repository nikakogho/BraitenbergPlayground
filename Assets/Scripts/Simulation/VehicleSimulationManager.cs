using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
    public class VehicleSimulationManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _vehicleSpawnPointsParent;

        private readonly List<GameObject> _spawnedVehicles = new List<GameObject>();

        private Transform GetSpawnPoint()
        {
            int index = Random.Range(0, _vehicleSpawnPointsParent.childCount);

            var spawnPoint = _vehicleSpawnPointsParent.GetChild(index);

            return spawnPoint;
        }

        public void SpawnVehicle(GameObject prefab)
        {
            var spawnPoint = GetSpawnPoint();
            _spawnedVehicles.Add(Instantiate(prefab, spawnPoint.position, spawnPoint.rotation));
        }

        public void Reset()
        {
            foreach (var vehicle in _spawnedVehicles)
            {
                Destroy(vehicle);
            }
            _spawnedVehicles.Clear();
        }
    }
}
