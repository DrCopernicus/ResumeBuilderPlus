using System.Collections.ObjectModel;
using System.Windows.Input;
using ResumeBuilderPlus.VVM;

namespace ResumeBuilderPlus.Qualifiers.Collections
{
    public class ManipulatableCollection<T> : ObservableCollection<T> where T : IParentable<T>, new()
    {
        public ICommand AddCommand
        {
            get { return new DelegateCommand(AddFromCommand); }
        }

        private void AddFromCommand()
        {
            Add(new T(){Parent = this});
        }
    }
}
