using Employee_Management_System.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System.Interfaces
{
    public interface IPositionRepository
    {
        void AddPosition(Position position);
        IEnumerable<Position> GetAllPositions();
    }
}
