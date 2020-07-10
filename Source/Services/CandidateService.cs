using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class CandidateService : ICandidateService
    {
        public CodenationContext codenationcandidate;

        public CandidateService(CodenationContext context)
        {
            codenationcandidate = context;
        }

        public IList<Candidate> FindByAccelerationId(int accelerationId)
        {
            return (codenationcandidate.Candidates
                .Where(x => x.Acceleration.Id == accelerationId).ToList());
        }

        public IList<Candidate> FindByCompanyId(int companyId)
        {
            return (codenationcandidate.Candidates
                .Where(x => x.Company.Id == companyId).ToList());
        }

        public Candidate FindById(int userId, int accelerationId, int companyId)
        {
            return codenationcandidate.Candidates.Find(userId, accelerationId, companyId);
        }

        public Candidate Save(Candidate candidate)
        {
            var hascandidate = codenationcandidate.Candidates.Find(candidate.UserId, candidate.AccelerationId, candidate.CompanyId);

            if (hascandidate is null)
            {
                codenationcandidate.Candidates.Add(candidate);

            }
            else
            {
                codenationcandidate.Candidates.Update(hascandidate);
            }

            codenationcandidate.SaveChanges();

            return candidate;
        }
    }
}