using Core;

namespace Tests
{
    public class VehicleTests
    {
        [Fact]
        public void Tick_SamplesAllSensorsAndTransmits()
        {
            var leftLight = 0f;
            var rightLight = 0f;

            var v = new Vehicle();

            // Sensors read from captured variables
            var sL = new Sensor(() => leftLight);
            var sR = new Sensor(() => rightLight);
            var mL = new Motor(0);
            var mR = new Motor(0);

            v.AddSensor(sL);
            v.AddSensor(sR);
            v.AddWire(new Wire(sL, mL, +1f));
            v.AddWire(new Wire(sR, mR, +1f));

            leftLight = 1f; // pretend environment changed
            rightLight = 0.2f;
            v.Tick();

            Assert.Equal(1f, mL.Power);
            Assert.Equal(0.2f, mR.Power);
        }
    }
}
