using System;
using FluentAssertions;
using Xunit;

namespace Floatingman.Closures
{
    // this means that all the static methods in this class are available without class name decoration in this namespace
    using static Floatingman.Closures.Incrementor;

    public class Tests_For_Final_State
    {

        [Fact]
        public void Closure_From_Static_Class()
        {
            var incBy3 = GetIncrementor(3);
            var decBy3 = GetIncrementor(-3);

            incBy3(7).Should().Be(10);
            decBy3(13).Should().Be(10);
        }

        [Fact]
        public void Closure_From_Static_Class_Overloads()
        {
            var incBy3 = GetIncrementor(3);
            var decBy3 = GetIncrementor(-Math.PI);

            incBy3(7).Should().Be(10);
            // the BeApproximately is just a way to round expectations when dealing with doubles
            decBy3(13).Should().BeApproximately(9.7, 1);
        }

    }
}
