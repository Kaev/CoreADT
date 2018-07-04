using System.IO;

namespace CoreADT.Helper
{
    public class Flowvs
    {
        public Vector3<float> Position { get; set; }
        public float Radius { get; set; }
        public Vector3<float> Direction { get; set; }
        public float Velocity { get; set; }
        public float Amplitude { get; set; }
        public float Frequency { get; set; }

        public void Read(BinaryReader reader)
        {
            Position = reader.ReadVector3Float();
            Radius = reader.ReadSingle();
            Direction = reader.ReadVector3Float();
            Velocity = reader.ReadSingle();
            Amplitude = reader.ReadSingle();
            Frequency = reader.ReadSingle();
        }

        public void Write(BinaryWriter writer)
        {
            writer.WriteVector3Float(Position);
            writer.Write(Radius);
            writer.WriteVector3Float(Direction);
            writer.Write(Velocity);
            writer.Write(Amplitude);
            writer.Write(Frequency);
        }

    }
}
