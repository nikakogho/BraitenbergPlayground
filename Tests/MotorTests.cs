using Core;

namespace Tests
{
    public class MotorTests
    {
        [Fact]
        public void SetPower_StoresValue()
        {
            var m = new Motor(0.1f);
            m.SetPower(0.75f);
            Assert.Equal(0.85f, m.Power);
        }
    }
}
