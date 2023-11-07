using System.Windows;
using TechnoService.Data;

namespace TechnoService
{
    public partial class App : Application
    {
        private TechnoServiceEntities technoServiceEntities;
        private TechnoServiceRepository technoServiceRepository;
        public TechnoServiceEntities GetTechnoServiceEntities()
        {
            if (technoServiceEntities == null)
            {
                technoServiceEntities = new TechnoServiceEntities();
            }
            return technoServiceEntities;
        }

        public TechnoServiceRepository GetTechnoServiceRepository()
        {
            if (technoServiceRepository == null)
            {
                TechnoServiceEntities technoServiceEntities = GetTechnoServiceEntities();
                technoServiceRepository = new TechnoServiceRepository(technoServiceEntities);
            }
            return technoServiceRepository;
        }
    }
}
