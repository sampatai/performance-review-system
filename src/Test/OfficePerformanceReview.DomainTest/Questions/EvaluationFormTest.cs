using OfficePerformanceReview.Domain.Questions.Enum;
using OfficePerformanceReview.Domain.Questions.ValueObjects;
using OfficeReview.Domain.Questions.Entities;
using OfficeReview.Domain.Questions.Enum;

namespace OfficePerformanceReview.DomainTest.QuestionsTest
{
    internal class EvaluationFormTest : TestBase
    {
        private EvaluationFormTemplate _evaluationForm;
        private Question _question;

        public override void ExtendSetup()
        {
            base.ExtendSetup();

            Fixture.Customize<EvaluationFormTemplate>(composer => composer
                .FromFactory(() => new EvaluationFormTemplate(
                    name: Faker.Lorem.Sentence(),
                    formEvaluation: Enumeration.GetRandomEnumValue<FormEvaluation>()
                ))
            );

            var options = new List<QuestionOption>
            {
                new QuestionOption("Option A"),
                new QuestionOption("Option B"),
            };

            Fixture.Customize<Question>(composer => composer
                .FromFactory(() => new Question(
                    question: Faker.Lorem.Sentence(),
                    questionType: QuestionType.SingleChoice,
                    isRequired: true,
                    options: options
                ))
            );

            _evaluationForm = Fixture.Create<EvaluationFormTemplate>();
            _question = Fixture.Create<Question>();
        }

        [Test]
        public void EvaluationForm_Initialize_Should_Be_Succeed()
        {
            var name = Faker.Lorem.Sentence();
            var evaluationType = FormEvaluation.SelfEvaluation;

            var evaluationForm = new EvaluationFormTemplate(name, evaluationType);

            Assert.That(evaluationForm.Name, Is.EqualTo(name));
            Assert.That(evaluationForm.IsDeleted, Is.False);
            Assert.That(evaluationForm.IsActive, Is.True);
            Assert.That(evaluationForm.EvaluationType, Is.EqualTo(evaluationType));
            Assert.That(evaluationForm.Questions, Is.Empty);
        }

        [TestCase("")]
        [TestCase(null)]
        public void EvaluationForm_Should_Throw_Exception_With_Invalid_Name(string? name)
        {
            Action action = () => new EvaluationFormTemplate(name, FormEvaluation.SelfEvaluation);

            if (name == null)
                Assert.Throws<ArgumentNullException>(() => action());
            else
                Assert.Throws<ArgumentException>(() => action());
        }

        [Test]
        public void SetEvaluationForm_Should_Be_Succeed()
        {
            var newName = Faker.Lorem.Sentence();
            var newType = FormEvaluation.ManagerEvaluation;

            _evaluationForm.SetEvaluationForm(newName, newType);

            Assert.That(_evaluationForm.Name, Is.EqualTo(newName));
            Assert.That(_evaluationForm.EvaluationType, Is.EqualTo(newType));
        }

        [TestCase("")]
        [TestCase(null)]
        public void SetEvaluationForm_Should_Throw_Exception_If_Name_Is_Null_Or_Empty(string? name)
        {
            Action action = () => _evaluationForm.SetEvaluationForm(name, FormEvaluation.PeerEvaluation);

            if (name == null)
                Assert.Throws<ArgumentNullException>(() => action());
            else
                Assert.Throws<ArgumentException>(() => action());
        }

        [Test]
        public void SetDelete_Should_Set_IsDeleted_To_True()
        {
            _evaluationForm.SetDelete();
            Assert.That(_evaluationForm.IsDeleted, Is.True);
        }

        [Test]
        public void SetDeActivate_Should_Set_IsActive_To_False()
        {
            _evaluationForm.SetDeActivate();
            Assert.That(_evaluationForm.IsActive, Is.False);
        }

        [Test]
        public void AddQuestion_Should_Add_Questions_To_List()
        {
            var questions = Fixture.CreateMany<Question>(3);
            _evaluationForm.AddQuestion(questions);
            Assert.That(_evaluationForm.Questions.Count, Is.EqualTo(3));
        }

        [Test]
        public void SetDeActivateQuestion_Should_Succeed()
        {
            var question = _question;
            _evaluationForm.AddQuestion(new[] { question });

            _evaluationForm.SetDeActivateQuestion(question.QuestionGuid);

            Assert.That(_evaluationForm.Questions.First().IsActive, Is.False);
        }

        [Test]
        public void SetDeleteQuestion_Should_Be_Succeed()
        {
            var question = _question;
            _evaluationForm.AddQuestion(new[] { question });

            _evaluationForm.SetDeleteQuestion(question.QuestionGuid);

            Assert.That(_evaluationForm.Questions.First().IsDeleted, Is.True);
        }

        [Test]
        public void SetQuestion_Should_Be_Succeed()
        {
            var question = _question;
            _evaluationForm.AddQuestion(new[] { question });

            var newText = Faker.Lorem.Sentence();
            var newType = QuestionType.MultipleChoice;
            var newOptions = new List<QuestionOption>
            {
                new QuestionOption("Updated A"),
                new QuestionOption("Updated B")
            };

            _evaluationForm.SetQuestion(
                question.QuestionGuid,
                newText,
                newType,
                true,
                newOptions
            );

            var updated = _evaluationForm.Questions.First();
            Assert.That(updated.QuestionText, Is.EqualTo(newText));
            Assert.That(updated.Options.Count, Is.EqualTo(2));
        }
    }
}
