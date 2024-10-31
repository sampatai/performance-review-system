using OfficeReview.Domain.Profile.ValueObjects;
using OfficeReview.Shared.Exceptions;
using System.Net.Mail;

namespace OfficeReview.Domain.Profile.Root;

public class Staff : Entity, IAggregateRoot
{
    private List<ReviewInitiator> _reviewers = new();
    protected Staff()
    {

    }
    public Guid StaffGuid { get; private set; }
    public string Name { get; private set; }
    public string PositionTitle { get; private set; }
    public string Email { get; private set; }
    public IReadOnlyList<ReviewInitiator> Reviewers => _reviewers.AsReadOnly();

    public Staff(string name, string position, string email)
    {
        Guard.Against.NullOrEmpty(name, nameof(name));
        Guard.Against.NullOrEmpty(position, nameof(position));
        Guard.Against.InvalidInput(email, nameof(email), e => _IsValidEmail(e), "Invalid email");
        this.StaffGuid = Guid.NewGuid();
        this.Name = name;
        this.PositionTitle = position;
        this.Email = email;
    }
    public void AddReviewer(string reviewer, int reviewerId)
    {
        _reviewers.Clear();
        _reviewers.Add(new ReviewInitiator(reviewerId, reviewer));
    }
    #region helper

    private bool _IsValidEmail(string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);
            return mailAddress.Address == email;
        }
        catch { return false; }
    }
    #endregion
}

