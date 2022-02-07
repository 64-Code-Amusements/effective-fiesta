namespace Floatingman.Closures
{
    public class Incrementor_ShortForm
    {
        private int _step;

        public static Incrementor_ShortForm GetIncrementor(int step)
            => new Incrementor_ShortForm(step);

        private Incrementor_ShortForm(int step)
            => _step = step;

        public int Step(int value)
            => value + _step;
    }
}
