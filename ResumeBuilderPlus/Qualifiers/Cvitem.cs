using System.Collections.ObjectModel;
using System.Windows.Input;
using ResumeBuilderPlus.VVM;

namespace ResumeBuilderPlus.Qualifiers
{
    public class Cvitem : ObservableObject, IParentable<Cvitem>
    {
        private string _type = "";

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

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

        private bool _relevant = true;

        public bool Relevant
        {
            get { return _relevant; }
            set
            {
                _relevant = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Cvitem> Parent { get; set; }
        public ICommand RemoveCommand { get { return new DelegateCommand(RemoveFromCommand); } }
        public void RemoveFromCommand()
        {
            Parent.Remove(this);
        }
    }
}
