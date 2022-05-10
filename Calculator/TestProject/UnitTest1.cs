using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator;
using System.Linq;

namespace TestProject
{
    [TestClass]
    public class ComputeManagerTest
    {
        [TestMethod]
        [DataRow(0,10)]
        [DataRow(-10.111, 10.111)]
        public void AddingTest(double a, double b)
        {
            var computer = new ComputeManager();
            Assert.AreEqual(computer.Add(a, b), a + b);
        }
    }

    [TestClass]
    public class UIManagerTest
    {
        [TestMethod]
        [DataRow("111", "10")]
        [DataRow("131", "10000")]
        [DataRow("111", "1525210")]
        [DataRow("5298", "1110")]
        public void IntTest(string a, string b)
        {
            var manager = new UIManager();
            Assert.AreEqual(CalcData(manager, a, "+", b), int.Parse(a) + int.Parse(b));
            Assert.AreEqual(CalcData(manager, a, "-", b), int.Parse(a) - int.Parse(b));
            Assert.AreEqual(CalcData(manager, a, "*", b), int.Parse(a) * int.Parse(b));
            Assert.AreEqual(CalcData(manager, a, "/", b), int.Parse(a) / int.Parse(b));
        }

        private int CalcData(UIManager manager, string a, string operatorString, string b)
        {
            manager.OperatorButtonClicked("C");
            a.ToCharArray().ToList().ForEach(c => manager.NumberButtonClicked(int.Parse(c.ToString())));
            manager.OperatorButtonClicked(operatorString);
            b.ToCharArray().ToList().ForEach(c => manager.NumberButtonClicked(int.Parse(c.ToString())));
            manager.OperatorButtonClicked("=");
            return (int)manager.GetResultValue();
        }
    }
}
