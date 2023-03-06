using System.Collections.Generic;
using System.Numerics;
using maze.Engine;

namespace maze.Graphic.Primitives.Interfaces
{
    public interface IProjectible
    {
        public ICollection<ProjectedVertice> Project(Screen screen, Vector3 light);
    }
}