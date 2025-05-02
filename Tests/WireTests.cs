using Core;

namespace Tests
{
    public class WireTests
    {
        [Fact]
        public void Transmit_SendsScaledSignal()
        {
            var s = new Sensor(() => 2f);
            var m = new Motor();
            var w = new Wire(s, m, gain: 0.5f);

            s.Sense(); // Value becomes 2
            w.TransmitPower();

            Assert.Equal(1f, m.Power);
        }
    }
}
