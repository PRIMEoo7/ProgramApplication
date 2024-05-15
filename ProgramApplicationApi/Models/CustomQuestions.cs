namespace ProgramApplicationApi.Models
{
    public class CustomQuestions
    {
        public string? Text { get; set; }
        public QuestionType? Type { get; set; } = null;
        public List<string>? MultipleChoiceValues { get; set; }
        public List<string>? DropdownValues { get; set; }
        public string? ParagraphValue { get; set; }
    }

    public enum QuestionType
    {
        YesNo,
        MultipleChoice,
        Paragraph,
        Dropdown,
        Date,
        Number

    }
}
