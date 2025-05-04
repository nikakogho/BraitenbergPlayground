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
