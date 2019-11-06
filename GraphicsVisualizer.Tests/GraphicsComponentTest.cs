using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Media.Media3D;

namespace GraphicsVisualizer.Tests
{
    /// <summary>
    /// Summary description for GraphicsComponentTest
    /// </summary>
    [TestClass]
    public class GraphicsComponentTest
    {
        private int id;

        [TestInitialize]
        public void Initialize()
        {
            id = 0;
        }

        [TestMethod]
        public void TestGraphicsComponentEqualWithItself()
        {
            var uut = CreateUnitUnderTest();
            Assert.AreEqual(uut, uut);
        }

        [TestMethod]
        public void TestGraphicsComponentsAreEqualWithOnlySameIds()
        {
            var uut1 = CreateUnitUnderTest();
            var uut2 = CreateUnitUnderTest();
            uut2.position = new Vector3D(2, 2, 2);
            uut2.color = new System.Windows.Media.Color();
            uut2.color.R = 10;
            uut2.color.B = 20;
            uut2.color.G = 255;
            uut2.color.A = 50;
            Assert.AreEqual(uut1, uut2);
        }

        [TestMethod]
        public void TestGraphicsComponentsAreNotEqualWhenIDsDiffer()
        {
            var uut1 = CreateUnitUnderTest();
            id += 1;
            var uut2 = CreateUnitUnderTest();
            Assert.AreNotEqual(uut1, uut2);
        }

        private GraphicsComponent CreateUnitUnderTest()
        {
            return new GraphicsComponent(id);
        }
    }
}
