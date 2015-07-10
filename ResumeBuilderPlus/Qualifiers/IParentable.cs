using System.Collections.ObjectModel;
using System.Windows.Input;
using Newtonsoft.Json;

namespace ResumeBuilderPlus.Qualifiers
{
    public interface IParentable<T>
    {
        ObservableCollection<T> Parent { get; set; }

        [JsonIgnore]
        ICommand RemoveCommand { get; }

        void RemoveFromCommand();
    }
}
