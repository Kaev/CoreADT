namespace CoreADT.Helper
{
    public class Vector3<T>
    {
        public T X { get; set; }
        public T Y { get; set; }
        public T Z { get; set; }

        public Vector3(T x, T y, T z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
