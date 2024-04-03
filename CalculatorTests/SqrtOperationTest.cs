using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class SqrtOperationTest
    {
        [Theory]
        [InlineData([1, 1])]
        [InlineData([4, 2])]
        [InlineData([100, 10])]
        [InlineData([0, 0])]
        public void StandartSqrtOperationsTest(double x, double expectedResult)
        {
            double result = Calculator.SqrtOperation(x);

            Assert.Equal(expectedResult, result, 2);
        }

        [Fact]
        public void MinusSqrtOperetaionTest()
        {
            Assert.Throws<InvalidDataException>(() => { Calculator.SqrtOperation(-1); });
        }
    }
}
