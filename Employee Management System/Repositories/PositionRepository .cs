using Employee_Management_System.Entites;
using Employee_Management_System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management_System.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly WorkflowDbContext _context;

        public PositionRepository(WorkflowDbContext context)
        {
            _context = context;
        }

        public void AddPosition(Position position)
        {
            _context.Positions.Add(position);
            _context.SaveChanges();
        }

        public IEnumerable<Position> GetAllPositions()
        {
            return _context.Positions.ToList();
        }
    }
}
