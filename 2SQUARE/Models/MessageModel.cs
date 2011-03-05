namespace _2SQUARE.Models
{
    public class MessageModel
    {
        public bool ShowWarningIcon { get; set; }
        public bool ShowErrorIcon { get; set; }
        public string Message { get; set; }

        public MessageModel(string message, bool showWarning = false, bool showError = false)
        {
            Message = message;
            ShowWarningIcon = showWarning;
            ShowErrorIcon = showError;
        }
    }
}