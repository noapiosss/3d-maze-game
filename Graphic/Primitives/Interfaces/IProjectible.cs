using System.Collections.Generic;
using maze.Engine;

namespace maze.Graphic.Primitives.Interfaces
{
    public interface IProjectible
    {
        public ICollection<ProjectedVertice> Project(Screen screen);
    }
}