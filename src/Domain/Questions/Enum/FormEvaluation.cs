namespace OfficeReview.Domain.Questions.Enum
{
    public class FormEvaluation : Enumeration
    {
        public static FormEvaluation ManagerEvaluation = new(1, "Manager Evaluation");        
        public static FormEvaluation PeerEvaluation = new(2, nameof(PeerEvaluation));
        public static FormEvaluation ProbationEvaluation = new(3, nameof(ProbationEvaluation));
        public static FormEvaluation InternEvaluation = new(4, nameof(InternEvaluation));
        public static FormEvaluation ExternalEvaluation = new(5, "External Evaluation");
        public static FormEvaluation SelfEvaluation = new(6, "Self Evaluation");

        public FormEvaluation(int id, string name) : base(id, name)
        {
        }
    }
}
