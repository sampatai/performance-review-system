﻿namespace OfficeReview.Domain.Questions.Enum
{
    public class FormEvaluation : Enumeration
    {
        public static FormEvaluation ManagerEvaluation = new(1, "Manager Evaluation");        
        public static FormEvaluation PeerEvaluation = new(2, "Peer Evaluation");
        public static FormEvaluation ProbationEvaluation = new(3, "Probation Evaluation");
        public static FormEvaluation InternEvaluation = new(4, "Intern Evaluation");
        public static FormEvaluation ExternalEvaluation = new(5, "External Evaluation");
        public static FormEvaluation SelfEvaluation = new(6, "Self Evaluation");
        public static FormEvaluation CustomerClientReviews = new(7, "Customer/Client Reviews");
        public FormEvaluation(int id, string name) : base(id, name)
        {
        }
    }
}
