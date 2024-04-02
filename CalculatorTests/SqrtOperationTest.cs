using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class SqrtOperationTest
    {
        [Fact]
        public void StandartSqrtOperationsTest()
        {
            var testData = new double[]
            {
                1,
                4,
                100,
                0
            };
            List<double> result = new List<double>();

            foreach (var item in testData)
            {
                result.Add(Calculator.SqrtOperation(item));
            }

            Assert.Equal(1, result[0]);
            Assert.Equal(2, result[1]);
            Assert.Equal(10, result[2]);
            Assert.Equal(0, result[3]);
        }

        [Fact]
        public void MinusSqrtOperetaionTest()
        {
            Assert.Throws<InvalidDataException>(() => { Calculator.SqrtOperation(-1); });
        }
    }
}
