using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.InputModels
{
    public class NewProjectInputModel
    {
        public NewProjectInputModel(string title, string description, int idClient, int idFreelancer, decimal totalCost)
        {
            Title = title;
            Description = description;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            TotalCost = totalCost;
        }

        public string Title { get; set; }
        public string Description { get;  set; }
        public int IdClient { get; set; }
        public int IdFreelancer { get; set; }
        public Decimal TotalCost { get;  set; }
    }
}
