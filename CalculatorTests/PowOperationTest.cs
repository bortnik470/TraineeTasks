using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class PowOperationTest
    {
        [Fact]
        public void StandartPowOperationTest()
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
            List<double> result = new List<double>();

            foreach (var item in testData)
            {
                result.Add(Calculator.PowOperation(item[0], item[1]));
            }

            Assert.Equal(1, result[0]);
            Assert.Equal(1, result[1]);
            Assert.Equal(16, result[2]);
            Assert.Equal(-8, result[3]);
            Assert.Equal(34, result[4]);
            Assert.Equal(0.25, result[5]);
        }
    }
}
