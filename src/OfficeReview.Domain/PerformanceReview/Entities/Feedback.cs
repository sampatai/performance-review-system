using OfficePerformanceReview.Domain.PerformanceReview.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficePerformanceReview.Domain.PerformanceReview.Entities
{
    public class Feedback : Entity
    {
        public Guid FeedbackGuid { get; private set; }
        public string? EmployeeComment { get; set; }
        public string? ManagerComment { get; set; }
        public OverallRating? PotentialLevel { get; private set; }

    }
}
