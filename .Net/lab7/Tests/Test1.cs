using Microsoft.VisualStudio.TestTools.UnitTesting;
using lab7;

namespace lab7
{
    [TestClass]
    public class ConveyorTests
    {
        [TestMethod]
        public void NormalWeight_ShouldPass()
        {
            var conveyor = new ConveyorBelt(100);

            bool result = conveyor.CheckWeight(50);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Overweight_ShouldStopConveyor()
        {
            var conveyor = new ConveyorBelt(100);
            bool eventRaised = false;

            conveyor.CriticalStop += () =>
                eventRaised = true;

            conveyor.ProcessItem(120);

            Assert.IsTrue(conveyor.IsStopped);
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void Reset_ShouldRestartConveyor()
        {
            var conveyor = new ConveyorBelt(100);
            bool eventRaised = false;

            conveyor.ProcessItem(120);
            conveyor.Restarted += () =>
                eventRaised = true;

            conveyor.Reset();

            Assert.IsFalse(conveyor.IsStopped);
            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void NormalWeight_ShouldProcessItem()
        {
            var conveyor = new ConveyorBelt(100);
            bool eventRaised = false;

            conveyor.ItemProcessed += () =>
                eventRaised = true;

            conveyor.ProcessItem(70);

            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public void NegativeWeight_ShouldThrowException()
        {
            var conveyor = new ConveyorBelt(100);

            Assert.ThrowsException<
                System.ArgumentOutOfRangeException>(
                () => conveyor.ProcessItem(-10));
        }
    }
}
