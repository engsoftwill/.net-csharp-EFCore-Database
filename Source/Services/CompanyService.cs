using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class CompanyService : ICompanyService
    {
        CodenationContext data;
        public CompanyService(CodenationContext context)
        {
            data = context;
        }

        public IList<Company> FindByAccelerationId(int accelerationId)
        {
            return data.Candidates
                .Where(x => x.AccelerationId == accelerationId)
                .Select(x => x.Company)
                .ToList();
        }

        public Company FindById(int id)
        {
            return data.Companies.Find(id);
        }

        public IList<Company> FindByUserId(int userId)
        {
            return data.Companies
                .Where(x => x.Candidates.Any(y=>y.UserId == userId))
                .OrderByDescending(x => x.Id)
                .ToList();
        }

        public Company Save(Company company)
        {
            if (company.Id == 0)
            {
                data.Add(company);
            }
            else
            {
                data.Update(company);
            }
            data.SaveChanges();
            return company;
        }
    }
}