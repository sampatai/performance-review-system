namespace OfficeReview.Domain.Questions.Enum
{
    public class FormEvaluation : Enumeration
    {
        public static FormEvaluation SelfManagerEvaluation = new(1, "Self-Manager Evaluation");
        public static FormEvaluation PeerEvaluation = new(2, nameof(PeerEvaluation));
        public FormEvaluation(int id, string name) : base(id, name)
        {
        }
    }
}
