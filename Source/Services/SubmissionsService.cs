using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class SubmissionService : ISubmissionService
    {
        public CodenationContext codenationsubmission;

        public SubmissionService(CodenationContext context)
        {
            codenationsubmission = context;
        }

        public IList<Submission> FindByChallengeIdAndAccelerationId(int challengeId, int accelerationId)
        {
            return (codenationsubmission.Candidates
                        .Where(can => can.AccelerationId == accelerationId)
                        .Join(codenationsubmission.Users, can => can.UserId, usu => usu.Id, (can, usu) => usu)
                        .Join(codenationsubmission.Submissions, usu => usu.Id, sub => sub.UserId, (usu, sub) => sub)
                        .Where(sub => sub.ChallengeId == challengeId)
                        .Distinct()).ToList();


        }

        public decimal FindHigherScoreByChallengeId(int challengeId)
        {
            var maiorScore = codenationsubmission.Submissions
                .OrderByDescending(x => x.Score)
                .Where(x => x.ChallengeId == challengeId)
                .Select(x => x.Score).First();

            return maiorScore;

        }

        public Submission Save(Submission submission)
        {
            var temSubmissao = codenationsubmission.Submissions.Find(submission.UserId, submission.ChallengeId);

            if (temSubmissao is null)
            {
                codenationsubmission.Submissions.Add(submission);

            }
            else
            {
                codenationsubmission.Submissions.Update(temSubmissao);
            }

            codenationsubmission.SaveChanges();

            return submission;
        }
    }
}