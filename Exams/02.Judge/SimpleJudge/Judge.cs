using System;
using System.Collections.Generic;
using System.Linq;

public class Judge : IJudge
{
    public Dictionary<int, Submission> Submissions { get; set; }

    public HashSet<int> Users { get; set; }

    public HashSet<int> Contests { get; set; }

    public Judge()
    {
        this.Submissions = new Dictionary<int, Submission>();
        this.Users = new HashSet<int>();
        this.Contests = new HashSet<int>();
    }

    public void AddContest(int contestId)
    {
        if (!this.Contests.Contains(contestId))
        {
            this.Contests.Add(contestId);
        }
    }

    public void AddSubmission(Submission submission)
    {
        if (!this.Users.Contains(submission.UserId) || !this.Contests.Contains(submission.ContestId))
        {
            throw new InvalidOperationException();
        }

        if (!this.Submissions.ContainsKey(submission.Id))
        {
            this.Submissions.Add(submission.Id, submission);
        }
    }

    public void AddUser(int userId)
    {
        if (!this.Users.Contains(userId))
        {
            this.Users.Add(userId);
        }
    }

    public void DeleteSubmission(int submissionId)
    {
        if (this.Submissions.ContainsKey(submissionId))
        {
            this.Submissions.Remove(submissionId);
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    public IEnumerable<Submission> GetSubmissions()
    {
        return this.Submissions.Values.OrderBy(s => s.Id).ToList();
    }

    public IEnumerable<int> GetUsers()
    {
        return this.Users.OrderBy(u => u);
    }

    public IEnumerable<int> GetContests()
    {
        return this.Contests.OrderBy(c => c);
    }

    public IEnumerable<Submission> SubmissionsWithPointsInRangeBySubmissionType(int minPoints, int maxPoints, SubmissionType submissionType)
    {
        return this.Submissions
            .Where(s => s.Value.Type == submissionType
                && s.Value.Points >= minPoints
                && s.Value.Points <= maxPoints)
            .Select(s => s.Value);
    }

    public IEnumerable<int> ContestsByUserIdOrderedByPointsDescThenBySubmissionId(int userId)
    {
        if (!this.Users.Contains(userId))
        {
            throw new InvalidOperationException();
        }

        return this.Submissions
            .Where(s => s.Value.UserId == userId)
            .OrderByDescending(s => s.Value.Points)
            .ThenBy(s => s.Key)
            .Select(s => s.Value.ContestId)
            .Distinct();
    }

    public IEnumerable<Submission> SubmissionsInContestIdByUserIdWithPoints(int points, int contestId, int userId)
    {
        if (!this.Users.Contains(userId) || !this.Contests.Contains(userId))
        {
            throw new InvalidOperationException();
        }

        var result = this.Submissions
            .Where(s => s.Value.UserId == userId
                    && s.Value.ContestId == contestId
                    && s.Value.Points == points)
            .Select(s => s.Value);

        if (!result.GetEnumerator().MoveNext())
        {
            throw new InvalidOperationException();
        }
        else
        {
            return result;
        }
    }

    public IEnumerable<int> ContestsBySubmissionType(SubmissionType submissionType)
    {
        return this.Submissions
            .Where(s => s.Value.Type == submissionType)
            .Select(s => s.Value.ContestId)
            .Distinct();
    }
}
