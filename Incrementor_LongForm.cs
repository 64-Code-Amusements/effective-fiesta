namespace Floatingman.Closures
{
    public class Incrementor_LongForm
    {
        private int _step;

        public static Incrementor_LongForm GetIncrementor(int step)
        {
            return new Incrementor_LongForm(step);
        }

        private Incrementor_LongForm(int step)
        {
            _step = step;
        }

        public int Step(int value)
        {
            return value + _step;
        }
    }
}
