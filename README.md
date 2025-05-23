# Braitenberg Playground 🧩🚗

Live demo → **GitHub Pages** (eventually will be auto-built on every push).

## What it does
Tweak one-to-one sensor-motor wires and watch complex behaviour emerge.

## Why
To question the boundary between living beings and machines by showing how simple wiring can give rise to behaviors we use as proof of life and mind

## Quick start
```bash
git clone https://github.com/NikaKogho/BraitenbergPlayground
unity -projectPath BraitenbergPlayground
```

## 🧠 Core API (Assets/Scripts/Core)

These four plain-C# classes contain **all decision-making**; they never touch UnityEngine.

| Element | Responsibility | One-line mental model |
|---------|----------------|-----------------------|
| **Sensor** | `float Sense()` calls an injected delegate and stores the latest `Value`. | “Feel the world and remember what you felt.” |
| **Motor** | `SetPower(float power)` writes to the public `Power` field. | “Remember how hard you should push.” |
| **Wire** | Holds a **from Sensor**, **to Motor**, and a gain. `TransmitPower()` multiplies sensor `Value × gain` and sends it to the motor. | “One axon with adjustable synaptic weight.” |
| **Vehicle** | Aggregates lists of Sensors and Wires. A `Tick()` samples *all* sensors, then propagates through *all* wires. | “Run the whole nervous system for this frame.” |

## 🚗 Vehicle catalogue – why multiple prefabs?

Braitenberg’s book shows how **tiny wiring tweaks** create wildly different personalities.  
Each prefab in `/Assets/Prefabs/` is a *chapter* in that story — no extra scripts, just different sensor→motor hookups.

### Vehicle 01 – “The Thermophile”
* **Scene:** `Assets/Scenes/Vehicle Demos/Vehicle 1 Demo.unity`
* **Prefab:** **Vehicle 1 - Light seeker**
* **Wiring:** 1 light sensor ➜ 1 wheel (**gain = +1**)
* **Behaviour:** Sits still in the dark, crawls in dim light, and rockets forward under a bright lamp.  
  To the casual observer it *“likes warmth.”*

_Run the scene, press **L** to show the sensor rays, and drag the yellow suns around to feel the speed scaling._

---

### Vehicle 02 – Fear & Aggression  
**Scene:** `Assets/Scenes/Vehicle Demos/Vehicle 2 Demo.unity`

| Prefab | Wiring (sensor → motor) | Gain | Sprite tint | Observed behaviour |
|--------|-------------------------|------|-------------|--------------------|
| **Vehicle 2a - Fear** | Left ➜ Left<br>Right ➜ Right | **+1** | Yellow | Turns **away** from the lamp and accelerates while fleeing. |
| **Vehicle 2b - Aggressor** | Left ➜ Right<br>Right ➜ Left | **+1** | Red | Curves **toward** the lamp and accelerates straight through it. |

*Spawn a creature, drag the yellow lamp, press **L** to toggle vision rays.*

---

### Vehicle 03 – Love & Exploration  
**Scene:** `Assets/Scenes/Vehicle Demos/Vehicle 3 Demo.unity`

| Prefab | Wiring | Gain | Sprite tint | Observed behaviour |
|--------|--------|------|-------------|--------------------|
| **Vehicle 3a - Love** | Left ➜ Left<br>Right ➜ Right | **–1** | Orange | Approaches the lamp but **slows down** as it closes in, then bumps gently. |
| **Vehicle 3b - Explorer** | Left ➜ Right<br>Right ➜ Left | **–1** | Green | Turns **away** from bright spots yet drifts in mid-light zones—looks curious. |

*Both vehicles include a small tonic motor baseline; wheels never reverse (power is clamped ≥ 0).*

---

### Vehicle 04 - we skip because it's a repetition on 03

---

### Vehicle 05 – Logic Gates  
**Scene:** `Assets/Scenes/Vehicle Demos/Vehicle 5 Logic Demo.unity`

| Prefab | Wiring type | Gate logic | Sprite tint | Observed behaviour |
|--------|-------------|------------|-------------|--------------------|
| **Vehicle 5 AND** | 2 sensors ➜ 1 threshold unit ➜ thruster | Fires if **both eyes** bright | Red | Stays still unless both lamps shine in; then darts forward. |
| **Vehicle 5 OR** | 2 sensors ➜ 1 threshold unit ➜ thruster | Fires if **either eye** bright | Orange | Launches as soon as *any* lamp lights one eye. |
| **Vehicle 5 NAND** | Inhibitory wiring ➜ threshold ➜ thruster | Fires if **not both** eyes are bright | Purple | Moves in partial shade, stalls in full glare. |
| **Vehicle 5 XOR** | Sensor ➜ OR and AND ➜ XOR | Fires if **exactly one** eye sees light | Cyan | Moves only in **01** or **10** configurations. |

*All vehicles are powered by a pure-C# neural microcircuit.  
You can extend them with more logic layers or feedback by adding more units in the Inspector.*

---

### Chapter 06 – Natural Selection Arena
**Scene:** `Assets/Scenes/Evolutionary/Vehicle 6 - Selection Arena.unity`

Demonstration of a simple genetic algorithm.
A dozen clones of the EvoVehicle (new type) start with random sensor-to-motor weights.  
Whenever a creature tumbles off the edge (no more light ➜ drives straight into the void) the arena:

1. Picks a **parent** proportional to time-alive.  
2. Clones its genome with a ±10 % mutation per wire.  
3. Spawns the baby back on the table.

After a minute the population converges on big *positive* gains → all individuals hug the bright centre or spin in place, either way they rarely fall.  
*Variation + differential survival = apparent design – without a designer.*

*Genome:* **4 floats** = gains for all four sensor→motor axons.  Natural selection sifts
through Fear/Aggressor/Love/Explorer variants and settles on the survivor.
