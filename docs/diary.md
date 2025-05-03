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
