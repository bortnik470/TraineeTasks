using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class PowOperationTest
    {
        [Theory]
        [InlineData([20, 0, 1])]
        [InlineData([-1, 4, 1])]
        [InlineData([4, 2, 16])]
        [InlineData([-2, 3, -8])]
        [InlineData([34, 1, 34])]
        [InlineData([4, -1, 0.25])]
        public void StandartPowOperationTest(double x, double y, double expectedResult)
        {
            var testData = new double[][]
            {
                [20, 0],
                [-1, 4],
                [4, 2],
                [-2, 3],
                [34, 1],
                [4, -1]
            };
            double result = Calculator.PowOperation(x, y);

            Assert.Equal(result, expectedResult, 2);
        }
    }
}
