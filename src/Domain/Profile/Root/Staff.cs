using OfficeReview.Domain.Profile.Enums;
using OfficeReview.Domain.Questions.Entities;
using System.Data;
using System.Net.Mail;

namespace OfficeReview.Domain.Profile.Root;

public class Staff : AuditableEntity, IAggregateRoot
{
    private List<Role> _roles = new();


    protected Staff()
    {

    }
    public Guid StaffGuid { get; private set; }
    public string Name { get; private set; }
    public string PositionTitle { get; private set; }
    public string Email { get; private set; }
    public Team Team { get; private set; }
    public IReadOnlyList<Role> Roles => _roles.AsReadOnly();
    public bool IsDeleted { get; private set; }
    public bool IsActive { get; private set; }

    public Staff(string name, string position,
        string email, IEnumerable<Role> roles, Team team)
    {
        Guard.Against.Null(roles, nameof(roles));
        Guard.Against.Null(team, nameof(team));
        Guard.Against.NullOrEmpty(name, nameof(name));
        Guard.Against.NullOrEmpty(position, nameof(position));
        Guard.Against.InvalidInput(email, nameof(email), e => _IsValidEmail(e), "Invalid email");
        this.StaffGuid = Guid.NewGuid();
        this.Name = name;
        this.PositionTitle = position;
        this.Email = email;
        this.Team = team;
        IsActive = true;
        IsDeleted = false;
        _roles.AddRange(roles);
    }

    public void SetStaff(string name, string position,
         IEnumerable<Role> roles, Team team)
    {
        Guard.Against.Null(roles, nameof(roles));
        Guard.Against.Null(team, nameof(team));
        Guard.Against.NullOrEmpty(name, nameof(name));
        Guard.Against.NullOrEmpty(position, nameof(position));
        this.StaffGuid = Guid.NewGuid();
        this.Name = name;
        this.PositionTitle = position;
        this.Team = team;
        _roles.Clear();
        _roles.AddRange(roles);
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
        _roles.Add(role);
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

