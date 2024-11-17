namespace OfficeReview.Domain.Questions.Enum
{
    public class FormEvaluation : Enumeration
    {
        public static FormEvaluation SelfManagerEvaluation = new(1, "Self-Manager Evaluation");
        public static FormEvaluation PeerEvaluation = new(2, nameof(PeerEvaluation));
        public static FormEvaluation ProbationEvaluation = new(3, nameof(ProbationEvaluation));
        public static FormEvaluation InternEvaluation = new(4, nameof(InternEvaluation));
        public FormEvaluation(int id, string name) : base(id, name)
        {
        }
    }
}
