using System;
using FluentAssertions;
using Xunit;

namespace Floatingman.Closures
{
    public class Tests_For_Evolving_Pattern
    {
        [Fact]
        public void Static_Factory_Tests_SHortForm()
        {
            var incBy3 = Incrementor_ShortForm.GetIncrementor(3);
            var decBy3 = Incrementor_ShortForm.GetIncrementor(-3);

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
            Transformer decBy3;
            {
                var inc = Incrementor_Delegated.GetIncrementor(3);
                var dec = Incrementor_Delegated.GetIncrementor(-3);

                incBy3 = inc.GetTransformer();
                decBy3 = dec.GetTransformer();
            }
            incBy3(7).Should().Be(10);
            decBy3(13).Should().Be(10);
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

        [Fact]
        public void Static_Factory_Anonymous()
        {
            var incBy3 = Incrementor_Anonymous.GetIncrementor_2(3);
            var incBy2 = Incrementor_Anonymous.GetIncrementor_1(2);
            var incBy1 = Incrementor_Anonymous.GetIncrementor(1);

            incBy3(7).Should().Be(10);
            incBy2(8).Should().Be(10);
            incBy1(9).Should().Be(10);
        }

        [Fact]
        public void Closure_As_Local_Function()
        {
            var incBy3 = Incrementor(3);
            var decBy3 = Incrementor(-3);

            incBy3(7).Should().Be(10);
            decBy3(13).Should().Be(10);

            Func<int, int> Incrementor(int step) => x => x + step;
        }
    }
}
