using System.Collections.ObjectModel;
using System.Windows.Input;
using ResumeBuilderPlus.VVM;

namespace ResumeBuilderPlus.Qualifiers.Descriptors
{
    public class Tag : ObservableObject, IParentable<Tag>
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

        public override string ToString()
        {
            return Text;
        }

        public ObservableCollection<Tag> Parent { get; set; }
        public ICommand RemoveCommand { get { return new DelegateCommand(RemoveFromCommand); } }
        public void RemoveFromCommand()
        {
            Parent.Remove(this);
        }
    }
}
