using OfficeReview.Domain.Questions.Enum;
using OfficeReview.Domain.Questions.Entities;
using OfficePerformanceReview.Domain.Questions.Enum;

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
            _evaluationForm = Fixture.Create<EvaluationFormTemplate>();
            Fixture.Customize<Question>(composer => composer
              .FromFactory(() => new Question(
                  question: Faker.Lorem.Sentence(),
                  questionType: Enumeration.GetAll<QuestionType>().First()
              ))
          );
            _question = Fixture.Create<Question>();
        }

        [Test]
        public void EvaluationForm_Initialize_Should_Be_Succeed()
        {
            // Arrange
            var name = Faker.Lorem.Sentence();
            var evaluationType = FormEvaluation.SelfManagerEvaluation;

            // Act
            var evaluationForm = new EvaluationFormTemplate(name, evaluationType);

            // Assert
            Assert.That(evaluationForm.Name, Is.EqualTo(name));
            Assert.That(evaluationForm.IsDeleted, Is.False);
            Assert.That(evaluationForm.IsActive, Is.False);
            Assert.That(evaluationForm.EvaluationType, Is.EqualTo(evaluationType));
            Assert.That(evaluationForm.Questions, Is.Empty);
        }

        [TestCase("")]
        [TestCase(null)]
        public void EvaluationForm_Should_Throw_Exception_With_Invalid_Name(string? name)
        {
            // Act
            Action action = () => new EvaluationFormTemplate(name, FormEvaluation.SelfManagerEvaluation);

            // Assert
            if (name == null)
            {
                Assert.Throws<ArgumentNullException>(() => action(), "Value cannot be null. (Parameter 'name')");
            }
            else
            {
                Assert.Throws<ArgumentException>(() => action(), "Required input name was empty. (Parameter 'name')");
            }
        }

        [Test]
        public void SetEvaluationForm_Should_Be_Succeed()
        {
            // Arrange
            var newName = Faker.Lorem.Sentence();

            // Act
            _evaluationForm.SetEvaluationForm(newName);

            // Assert
            Assert.That(_evaluationForm.Name, Is.EqualTo(newName));
        }

        [TestCase("")]
        [TestCase(null)]
        public void SetEvaluationForm_Should_Throw_Exception_If_Name_Is_Null_Or_Empty(string? name)
        {
            // Act
            Action action = () => _evaluationForm.SetEvaluationForm(name);

            // Assert
            if (name == null)
            {
                Assert.Throws<ArgumentNullException>(() => action(), "Value cannot be null. (Parameter 'name')");
            }
            else
            {
                Assert.Throws<ArgumentException>(() => action(), "Required input name was empty. (Parameter 'name')");
            }
        }

        [Test]
        public void SetDelete_Should_Set_IsDeleted_To_True()
        {
            // Act
            _evaluationForm.SetDelete();

            // Assert
            Assert.That(_evaluationForm.IsDeleted, Is.True);
        }

        [Test]
        public void SetDeActivate_Should_Set_IsActive_To_False()
        {
            // Act
            _evaluationForm.SetDeActivate();

            // Assert
            Assert.That(_evaluationForm.IsActive, Is.False);
        }

        [Test]
        public void AddQuestion_Should_Add_Questions_To_List()
        {
            // Arrange
            var questions = Fixture.CreateMany<Question>(3);

            // Act
            _evaluationForm.AddQuestion(questions);

            // Assert
            Assert.That(_evaluationForm.Questions.Count, Is.EqualTo(3));
        }

        [Test]
        public void SetDeActivateQuestion_Should_Succeed()
        {
            // Arrange
            var question = _question;
            _evaluationForm.AddQuestion(new[] { question });

            // Act
            _evaluationForm.SetDeActivateQuestion(question.QuestionGuid);

            // Assert
            Assert.That(_evaluationForm.Questions.First().IsActive, Is.False);
        }

        [Test]
        public void SetDeleteQuestion_Should_Be_Succeed()
        {
            // Arrange
            var question = _question;
            _evaluationForm.AddQuestion(new[] { question });

            // Act
            _evaluationForm.SetDeleteQuestion(question.QuestionGuid);

            // Assert
            Assert.That(_evaluationForm.Questions.First().IsDeleted, Is.True);
        }

        [Test]
        public void SetQuestion_Should_Be_Succeed()
        {
            // Arrange
            var question = _question;
            _evaluationForm.AddQuestion(new[] { question });
            var newQuestion = Faker.Lorem.Sentence();

            // Act
            _evaluationForm.SetQuestion(question.QuestionGuid, newQuestion, Enumeration.GetAll<QuestionType>().First());

            // Assert
            Assert.That(_evaluationForm.Questions, Has.Some.Matches<Question>(q => q.QuestionText == newQuestion));
        }
    }
}
