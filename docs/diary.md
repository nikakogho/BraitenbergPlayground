# Dev diary

---

## 2025-05-02

- **Core nervous-system classes complete.**  
  `Sensor`, `Motor`, `Wire`, `Vehicle` now live in `Assets/Scripts/Core` and pass xUnit tests.

---

## 2025-05-03

- **LightSensor gets a field of vision.**  
  Switched to a multi-ray cone and added an **L** key toggle to draw rays in Game view.

- **Physical body wired up.**  
  Added `VehicleBody` (root Rigidbody2D) and `WheelMotor2D`; wheels apply force at their positions while the brain stays pure C#.

- **First creature comes alive – Vehicle 01.**  
  One sensor, one wheel, positive gain. It clearly speeds up as light increases—exactly the textbook behaviour.

- **Vehicle1Demo scene.**  
  Three “suns” (dim, medium, bright) laid out left-to-right so newcomers instantly see the speed gradient.

- **Documentation pass #1.**  
  README now explains the Core API and introduces the vehicle catalogue; Vehicle 01 is fully documented with scene name and usage tips.

---

## 2025-05-04

- **Vehicle 02 pair implemented.**  
  *Fear* (ipsilateral +gain) and *Love* (cross +gain) prefabs created with distinct yellow/red tints.

- **Vehicle 2 Demo scene.**  
  • Multiple spawn pads; clicking a UI button instantiates the chosen vehicle at a random pad.  
  • Draggable lamp lets players provoke both behaviours live.  
  • **L** key toggles vision rays.

- **Physics tuning.**  
  Added small tonic motor baseline and adjusted wheel thrust / drag so vehicles drift gently in darkness but neither stall nor accelerate without bound.

- **README updated.**  
  Vehicle catalogue now includes Fear & Love with wiring table, colour legend, scene name, and usage tips.

---

## 2025-05-04 (afternoon) – Chapter-2 names fixed, Chapter-3 added

* **Renamed and rewired Chapter 2.**  
  *Blue Fear* (same-side +gain) and *Red Aggressor* (cross +gain) now match the book.

* **Implemented Chapter 3 vehicles.**  
  *Orange Love* (same-side –gain) slows as it nears the lamp.  
  *Green Explorer* (cross –gain with higher baseline) roams the mid-light.

* **Demo scenes updated.**  
  Vehicle 2 Demo spawns Fear or Aggressor at random pads.  
  Vehicle 3 Demo spawns Love or Explorer, includes two static lamps plus a draggable one.

* **Motor clamp bug fixed.**  
  Wheel power is now `max(0, baseline + wires)` so inhibitory wiring never drives wheels backward.

* **README brought in line with textbook terminology.**

## 2025-05-05

* Setup combined demo of vehicles 2 and 3 and can now reset HUD

---

## 2025-05-10

Logic gates now fully supported.
ThresholdUnit introduced as a stageable logic node. Vehicles can now do AND, OR, NAND, XOR via wiring alone.

Rewrote Vehicle engine loop.
One-pass tick system allows any-direction wiring: feedforward, feedback, even recurrent loops.

Prefab library updated.
Vehicle5a–5d created for all four logic gates. Central-thruster design with colored tints for clarity.

LogicArena scene added.
Four pad inputs simulate binary inputs (00/01/10/11). Visualizes gate truth tables with a single click—no dragging required.

## 2025-05-23 – Chapter 6 “Selection Arena”

- **Evolutionary playground implemented.**  
  New `Vehicle 6 - Selection Arena` scene: brightly lit table, cliff-edge KillZone.  
  *Population of 12* EvoVehicle (new vehicle described below) offspring roam; falling off the table counts as death.

- **Genetic reproduction & mutation.**  
  `VehicleGenome` + `SelectionArenaManager` clone a random vehicle (higher chance of picking the longer the vehicle has lived) when a vehicle dies, mutating its wire gains ±10%. Over a 60-s run you can watch the average lifetime rise, and in a few minutes they all develop tactics to stay alive. Observed tactics in multiple runs include spinning in place, keeping a distance like "Love" vehicle and aggressively smashing into the light zone like "aggressor"

- **New scripts:** `VehicleGenome`, `VehicleLife`, `SelectionArenaManager`, `KillZone`, plus gain-getter/setter helpers on `VehicleBody`.

- **4-gene genome for EvoVehicle**  Each vehicle 6 carries gains for LL, LR, RL, RR wires.
  Random founders display every textbook behaviour; selection quickly amplifies the ‘Aggressor’ pattern
  (cross-positive), demonstrating emergence from a common genetic substrate.

## 2025-05-23 night – Chapter 7 “Pavlovian Conditioning”

- **Vehicle 7 – Pavlovian** prefab finished.  
  *Wiring summary*  
  | Src → Dst | Kind | Gain | Purpose |
  |-----------|------|------|---------|
  | Left LightSensor → Unit Food | regular | **+1** | innate “see food” |
  | Right LightSensor → Unit Food | regular | **+1** | — |
  | Unit Food → Left Wheel | regular | **+1** | drive |
  | Unit Food → Right Wheel | regular | **+1** | drive |
  | **Key Bell → Unit Food** | **Mnemotrix** | PlasticWeight (0 → 1) | learned bell-food link |

  - Two light sensors activate the **Food** threshold unit (τ = 0.7); Food drives *both* wheels.  
  - A plastic Mnemotrix wire (Bell KeySensor → Food) starts silent; the first time bell & food fire together, its weight jumps to **1** and then **decays 5 % s⁻¹** if not reinforced.

- **ConditioningArena** scene added: central lamp, on-screen hint “Press B to ring bell”.  Demo shows Pavlovian learning in < 30 s.

- New serialisable helpers: `PlasticWeight` (mutable gain) and `MnemotrixConfig : VehicleWireConfig`.  
  `VehicleBody` now processes a `mnemotrixWires` list (≈ 12 LoC patch, core engine untouched).
