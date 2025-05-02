using Core;

namespace Tests
{
    public class MotorTests
    {
        [Fact]
        public void SetPower_StoresValue()
        {
            var m = new Motor();
            m.SetPower(0.75f);
            Assert.Equal(0.75f, m.Power);
        }
    }
}
