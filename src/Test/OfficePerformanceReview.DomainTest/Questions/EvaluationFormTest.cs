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
                  questionType: Enumeration.GetRandomEnumValue<QuestionType>()
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
            //Assert
            evaluationForm.Name.Should().Be(name);
            evaluationForm.IsDeleted.Should().BeFalse();
            evaluationForm.IsActive.Should().BeFalse();
            evaluationForm.EvaluationType.Should().Be(evaluationType);
            evaluationForm.Questions.Should().BeEmpty();
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
                action.Should().Throw<ArgumentNullException>()
                    .WithMessage("Value cannot be null. (Parameter 'name')");
            }
            else
            {
                action.Should().Throw<ArgumentException>()
                    .WithMessage("Required input name was empty. (Parameter 'name')");
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
            _evaluationForm.Name.Should().Be(newName);
        }
        [TestCase("")]
        [TestCase(null)]

        public void SetEvaluationForm_Should_Throw_Exception_If_Name_Is_Null_Or_Empty(string? name)
        {
            // Arrange


            // Act
            Action action = () => _evaluationForm.SetEvaluationForm(name);

            // Assert
            // Assert
            if (name == null)
            {
                action.Should().Throw<ArgumentNullException>()
                    .WithMessage("Value cannot be null. (Parameter 'name')");
            }
            else
            {
                action.Should().Throw<ArgumentException>()
                    .WithMessage("Required input name was empty. (Parameter 'name')");
            }
        }

        [Test]
        public void SetDelete_Should_Set_IsDeleted_To_True()
        {
            // Act
            _evaluationForm.SetDelete();
            //Assert
            _evaluationForm.IsDeleted.Should().BeTrue();
        }

        [Test]
        public void SetDeActivate_Should_Set_IsActive_To_False()
        {
            // Act
            _evaluationForm.SetDeActivate();
            //Assert
            _evaluationForm.IsActive.Should().BeFalse();
        }
        [Test]
        public void AddQuestion_Should_Add_Questions_To_List()
        {
            // Arrange
            var questions = Fixture.CreateMany<Question>(3);
            // Act
            _evaluationForm.AddQuestion(questions);
            //Assert
            _evaluationForm.Questions.Should().HaveCount(3);
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
            _evaluationForm.Questions.First().IsActive.Should().BeFalse();
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
            _evaluationForm.Questions.First().IsDeleted.Should().BeTrue();
        }

        [Test]
        public void SetQuestion_Should_Be_Succeed()
        {
            // Arrange

            var question = _question;
            _evaluationForm.AddQuestion(new[] { question });
            var newQuestion = Faker.Lorem.Sentence();
            // Act
            _evaluationForm.SetQuestion(question.QuestionGuid, newQuestion, Enumeration.GetRandomEnumValue<QuestionType>());

            // Assert

            _evaluationForm.Questions.Should().Contain(q => q.QuestionText == newQuestion);
        }
    }
}
