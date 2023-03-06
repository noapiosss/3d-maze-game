using System.Numerics;

namespace maze.Graphic.Primitives.Interfaces
{
    public interface IBrightable
    {
        public float GetBrightness(Vector3 pointPosition, Vector3 pointNormal, Vector3 light);
    }
}