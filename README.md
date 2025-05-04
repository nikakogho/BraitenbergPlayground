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
