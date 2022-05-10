using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.InputModels
{
    public class UpdateProjectInputModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; private set; }
        public Decimal TotalCost { get; private set; }
    }
}
