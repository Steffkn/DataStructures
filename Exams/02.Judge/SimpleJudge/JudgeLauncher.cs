using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public static class JudgeLauncher
{
    public static void Main()
    {
        IJudge judge = new Judge();
        Random idGen = new Random();
        Dictionary<int, Submission> submissions = new Dictionary<int, Submission>();
        List<int> ids = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            int submissionId = idGen.Next(0, 100000);
            int userId = idGen.Next(0, 100000);
            int contestId = idGen.Next(0, 100000);
            int points = idGen.Next(0, 100000);
            SubmissionType type = (SubmissionType)idGen.Next(0, 3);

            Submission submission = new Submission(submissionId, points, type, contestId, userId);

            if (!submissions.ContainsKey(submissionId))
            {
                submissions.Add(submissionId, submission);
                ids.Add(submissionId);
            }

            judge.AddContest(contestId);
            judge.AddUser(userId);
            judge.AddSubmission(submission);
        }
        int subId = idGen.Next(0, ids.Count);
        Submission sub = submissions[ids[subId]];

        judge.DeleteSubmission(sub.Id);
        submissions.Remove(sub.Id);

        IEnumerable<Submission> result = judge.GetSubmissions();

        var r = submissions.Values.OrderBy(x => x);
        Console.WriteLine("{0} {1}", submissions.Count, result.Count());
        Console.WriteLine(string.Join(", ", r));
        Console.WriteLine();
        Console.WriteLine(string.Join(", ", result));

    }
}

