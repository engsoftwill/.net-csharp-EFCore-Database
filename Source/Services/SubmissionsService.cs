using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class SubmissionService : ISubmissionService
    {
        public CodenationContext submissionR;

        public SubmissionService(CodenationContext context)
        {
            submissionR = context;
        }

        public IList<Submission> FindByChallengeIdAndAccelerationId(int challengeId, int accelerationId)
        {
            return (submissionR.Candidates
                        .Where(can => can.AccelerationId == accelerationId)
                        .Join(submissionR.Users, can => can.UserId, usu => usu.Id, (can, usu) => usu)
                        .Join(submissionR.Submissions, usu => usu.Id, sub => sub.UserId, (usu, sub) => sub)
                        .Where(sub => sub.ChallengeId == challengeId)
                        .Distinct()).ToList();


        }

        public decimal FindHigherScoreByChallengeId(int challengeId)
        {
            var maiorScore = submissionR.Submissions
                .OrderByDescending(x => x.Score)
                .Where(x => x.ChallengeId == challengeId)
                .Select(x => x.Score).First();

            return maiorScore;

        }

        public Submission Save(Submission submission)
        {
            var temSubmissao = submissionR.Submissions.Find(submission.UserId, submission.ChallengeId);

            if (temSubmissao is null)
            {
                submissionR.Submissions.Add(submission);

            }
            else
            {
                submissionR.Submissions.Update(temSubmissao);
            }

            submissionR.SaveChanges();

            return submission;
        }
    }
}