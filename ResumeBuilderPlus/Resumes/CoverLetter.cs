using ResumeBuilderPlus.VVM;

namespace ResumeBuilderPlus.Resumes
{
    public class CoverLetter : ObservableObject
    {
        private string _text = "";

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }
    }
}
