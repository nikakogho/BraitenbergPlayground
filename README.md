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
* **Scene:** `Assets/Scenes/Vehicle 1 Demo.unity`
* **Wiring:** 1 light sensor ➜ 1 wheel (**gain = +1**)
* **Behaviour:** Sits still in the dark, crawls in dim light, and rockets forward under a bright lamp.  
  To the casual observer it *“likes warmth.”*

_Run the scene, press **L** to show the sensor rays, and drag the yellow suns around to feel the speed scaling._
