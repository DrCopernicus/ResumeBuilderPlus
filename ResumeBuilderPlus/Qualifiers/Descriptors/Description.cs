using System.Collections.ObjectModel;
using System.Windows.Input;
using ResumeBuilderPlus.Qualifiers.Collections;
using ResumeBuilderPlus.VVM;

namespace ResumeBuilderPlus.Qualifiers.Descriptors
{
    public class Description : ObservableObject, IParentable<Description>
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

        private ManipulatableCollection<Tag> _tags = new ManipulatableCollection<Tag>();

        public ManipulatableCollection<Tag> Tags
        {
            get { return _tags; }
            set
            {
                _tags = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return Text;
        }

        public ObservableCollection<Description> Parent { get; set; }
        public ICommand RemoveCommand { get { return new DelegateCommand(RemoveFromCommand); } }
        public void RemoveFromCommand()
        {
            Parent.Remove(this);
        }
    }
}
