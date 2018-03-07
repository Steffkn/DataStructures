using System;

public class Submission : IComparable
{
    public int Id { get; set; }

    public int Points { get; set; }

    public SubmissionType Type { get; set; }

    public int ContestId { get; set; }

    public int UserId { get; set; }

    public Submission(int id, int points, SubmissionType type, int contestId, int userId)
    {
        this.Id = id;
        this.Points = points;
        this.Type = type;
        this.ContestId = contestId;
        this.UserId = userId;
    }

    public int CompareTo(object obj)
    {
        return Id.CompareTo((obj as Submission).Id);
    }
}
