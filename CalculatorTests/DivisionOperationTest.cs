using Xunit.Sdk;

namespace Calculator
{
    public class DivisionOperationTest
    {
        [Fact]
        public void StandartDivisionOperationsTest()
        {
            var testData = new double[][]
            {
                [1, 1],
                [-1, 4],
                [4, 2],
                [-2, -1],
                [0, 1]
            };
            List<double> result = new List<double>();

            foreach (var item in testData)
            {
                result.Add(Calculator.DivisionOperation(item[0], item[1]));
            }

            Assert.Equal(1, result[0]);
            Assert.Equal(-0.25, result[1]);
            Assert.Equal(2, result[2]);
            Assert.Equal(2, result[3]);
            Assert.Equal(0, result[4]);
        }

        [Fact]
        public void DivisionByZeroTest()
        {
            Assert.Throws<DivideByZeroException>(() => Calculator.DivisionOperation(1, 0));
        }
    }
}
