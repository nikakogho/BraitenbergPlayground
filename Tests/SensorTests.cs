using Core;

namespace Tests
{
    public class SensorTests
    {
        [Fact]
        public void Sample_StoresLatestReading()
        {
            var s = new Sensor(() => 42f);
            s.Sense();
            Assert.Equal(42f, s.Value);
        }
    }
}
