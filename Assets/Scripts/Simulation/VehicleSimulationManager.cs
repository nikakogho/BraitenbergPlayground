using System.Collections.Generic;
using UnityEngine;

namespace Simulation
{
    public class VehicleSimulationManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _vehiclePrefab;

        [SerializeField]
        private Transform _vehicleSpawnPointsParent;

        private GameObject _vehicle;

        private List<GameObject> _spawnedVehicles = new List<GameObject>();

        private Transform GetSpawnPoint()
        {
            int index = Random.Range(0, _vehicleSpawnPointsParent.childCount);

            var spawnPoint = _vehicleSpawnPointsParent.GetChild(index);

            return spawnPoint;
        }

        public void Spawn()
        {
            if (_vehicle != null) return;
            var spawnPoint = GetSpawnPoint();
            _vehicle = Instantiate(_vehiclePrefab, spawnPoint.position, spawnPoint.rotation);
        }

        public void SpawnVehicle(GameObject prefab)
        {
            var spawnPoint = GetSpawnPoint();
            _spawnedVehicles.Add(Instantiate(prefab, spawnPoint.position, spawnPoint.rotation));
        }

        public void Reset()
        {
            if (_vehicle != null)
            {
                Destroy(_vehicle);
                _vehicle = null;
            }
            foreach (var vehicle in _spawnedVehicles)
            {
                Destroy(vehicle);
            }
            _spawnedVehicles.Clear();
        }
    }
}
