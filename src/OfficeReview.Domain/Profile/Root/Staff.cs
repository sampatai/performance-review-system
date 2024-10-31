using OfficeReview.Domain.Profile.Enums;
using OfficeReview.Domain.Questions.Entities;
using System.Net.Mail;

namespace OfficeReview.Domain.Profile.Root;

public class Staff : AuditableEntity, IAggregateRoot
{
    private List<Role> _Role = new();


    protected Staff()
    {

    }
    public Guid StaffGuid { get; private set; }
    public string Name { get; private set; }
    public string PositionTitle { get; private set; }
    public string Email { get; private set; }
    public Team Team { get; private set; }
    public IReadOnlyList<Role> Role => _Role.AsReadOnly();
    public bool IsDeleted { get; private set; }
    public bool IsActive { get; private set; }

    public Staff(string name, string position,
        string email, Role role, Team team)
    {
        Guard.Against.Null(role, nameof(role));
        Guard.Against.Null(team, nameof(team));
        Guard.Against.NullOrEmpty(name, nameof(name));
        Guard.Against.NullOrEmpty(position, nameof(position));
        Guard.Against.InvalidInput(email, nameof(email), e => _IsValidEmail(e), "Invalid email");
        this.StaffGuid = Guid.NewGuid();
        this.Name = name;
        this.PositionTitle = position;
        this.Email = email;
        Role = role;
        IsActive = true;
        IsDeleted = false;
    }

    public void SetStaff(string name, string position,
         Role role, Team team)
    {
        Guard.Against.Null(role, nameof(role));
        Guard.Against.Null(team, nameof(team));
        Guard.Against.NullOrEmpty(name, nameof(name));
        Guard.Against.NullOrEmpty(position, nameof(position));
        this.StaffGuid = Guid.NewGuid();
        this.Name = name;
        this.PositionTitle = position;
        Role = role;
    }
    public void SetDelete()
    {
        this.IsDeleted = true;
    }
    public void SetDeActivate()
    {
        this.IsActive = false;
    }

    public void ChangeTeam(Team team)
    {
        this.Team = team;
    }
    public void SetChangeRole(Role role)
    {
        this.Role = role;
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

