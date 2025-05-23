using Core;

namespace Tests
{
    public class WireTests
    {
        [Fact]
        public void Transmit_SendsScaledSignal()
        {
            var s = new Sensor(() => 2f);
            var m = new Motor(0.1f);
            var w = new Wire(s.SensingLogic, m.SetPower, weight: 0.3f);

            s.Sense(); // Value becomes 2
            w.TransmitPower();

            Assert.Equal(0.7f, m.Power, 1);
        }
    }
}
