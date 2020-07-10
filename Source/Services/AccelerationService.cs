using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class AccelerationService : IAccelerationService
    {
        CodenationContext data;
        public AccelerationService(CodenationContext context)
        {
            data = context;
        }

        public IList<Acceleration> FindByCompanyId(int companyId)
        {
            return data.Accelerations
                .Where(x => x.Candidates.Any(y => y.CompanyId == companyId))
                .ToList();

        }

        public Acceleration FindById(int id)
        {
            return data.Accelerations.Find(id);
                
        }

        public Acceleration Save(Acceleration acceleration)
        {
            if (acceleration.Id == 0)
            {
                data.Add(acceleration);
            }
            else
            {
                data.Update(acceleration);
            }
            data.SaveChanges();
            return acceleration;
        }
    }
}
