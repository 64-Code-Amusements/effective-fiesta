using System;
using FluentAssertions;
using Xunit;

namespace Floatingman.Closures
{
    public class ClosureTester
    {
        [Fact]
        public void Static_Factory_Tests()
        {
            var incBy3 = Incrementor.GetIncrementor(3);
            var decBy3 = Incrementor.GetIncrementor(-3);

            incBy3.Step(7).Should().Be(10);
            decBy3.Step(13).Should().Be(10);
        }

        [Fact]
        public void Static_Factory_Tests_LongForm()
        {
            var incBy3 = Incrementor_LongForm.GetIncrementor(3);
            var decBy3 = Incrementor_LongForm.GetIncrementor(-3);

            incBy3.Step(7).Should().Be(10);
            decBy3.Step(13).Should().Be(10);
        }

        [Fact]
        public void Static_Factory_Tests_Delegated()
        {
            var inc = Incrementor_Delegated.GetIncrementor(3);
            var dec = Incrementor_Delegated.GetIncrementor(-3);

            var incBy3 = inc.GetTransformer();
            var decBy3 = dec.GetTransformer();

            incBy3(7).Should().Be(10);
            decBy3(13).Should().Be(10);
        }

        [Fact]
        public void Static_Factory_Tests_Delegated_Scoped()
        {
            Transformer incBy3;
            {
                var inc = Incrementor_Delegated.GetIncrementor(3);

                incBy3 = inc.GetTransformer();
            }
            incBy3(7).Should().Be(10);
        }

        [Fact]
        public void Static_Factory_Tests_Generic_Scoped()
        {
            Func<int, int> incBy3;
            {
                var inc = Incrementor_Generic.
                GetIncrementor(3);

                incBy3 = inc.GetTransformer();
            }
            incBy3(7).Should().Be(10);
        }
    }
}
