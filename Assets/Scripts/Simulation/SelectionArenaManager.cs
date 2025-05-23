using Helpers;
using System.Collections.Generic;
using UnityEngine;
using World;

namespace Simulation
{
    public class SelectionArenaManager : MonoBehaviour
    {
        [Header("Prefabs & arena")]
        public VehicleBody genericVehiclePrefab;
        public Transform vehicleSpawnPointsParent;
        public Transform spawnedVehiclesParent;
        public int populationSize = 12;

        private readonly List<VehicleLife> _live = new();

        public static SelectionArenaManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void Reset()
        {
            foreach (var vehicle in _live)
            {
                Destroy(vehicle.gameObject);
            }
            _live.Clear();
        }

        public void StartSimulation()
        {
            for (int i = 0; i < populationSize; i++)
                SpawnRandom(new VehicleGenome(GetRandomGains(genericVehiclePrefab.wires.Count, 1)));
        }

        private Transform GetSpawnPoint()
        {
            int index = Random.Range(0, vehicleSpawnPointsParent.childCount);
            var spawnPoint = vehicleSpawnPointsParent.GetChild(index);
            return spawnPoint;
        }

        private void SpawnRandom(VehicleGenome genome)
        {
            if (_live.Count >= populationSize) return;

            var spawnPoint = GetSpawnPoint();
            var go = Instantiate(genericVehiclePrefab, spawnPoint.position, spawnPoint.rotation, spawnedVehiclesParent);
            var life = go.gameObject.AddComponent<VehicleLife>();
            go.ApplyGenome(genome);
            _live.Add(life);
        }

        public void NotifyDeath(VehicleLife victim)
        {
            _live.Remove(victim);

            // choose parent proportional to lifespan
            float totalAge = 0f;
            foreach (var v in _live) totalAge += v.Age;
            float pick = Random.Range(0, totalAge);
            float accum = 0f;
            VehicleLife parent = _live[0];
            foreach (var v in _live)
            {
                accum += v.Age;
                if (accum >= pick) { parent = v; break; }
            }

            var body = parent.GetComponent<VehicleBody>();
            var parentGenome = new VehicleGenome(body.GetGains());
            SpawnRandom(parentGenome.Mutate());

            // keep population constant
            while (_live.Count < populationSize)
                SpawnRandom(parentGenome.Mutate());
        }

        private float[] GetRandomGains(int wireAmount, float spread)
        {
            float[] g = new float[wireAmount];
            for (int i = 0; i < wireAmount; i++)
                g[i] = Random.Range(-spread, spread);

            return g;
        }
    }
}
