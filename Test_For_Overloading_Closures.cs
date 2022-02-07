using System;
using FluentAssertions;
using Xunit;

namespace Floatingman.Closures
{
    public class Test_For_Overloading_Closures
    {

        Func<int, int> Inc(int step) => x => x + step;

        Func<double, double> Inc(double step) => x => x + step;


        [Fact]
        public void Closures_With_Overload()
        {
            var incBy3 = Inc(3);
            var decBy3 = Inc(-Math.PI);

            incBy3(7).Should().Be(10);
            // the BeApproximately is just a way to round expectations when dealing with doubles
            decBy3(13).Should().BeApproximately(9.7, 1);

        }
    }
}
