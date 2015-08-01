using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using ResumeBuilderPlus.Qualifiers.Collections;
using ResumeBuilderPlus.Qualifiers.Descriptors;
using ResumeBuilderPlus.Resumes;
using ResumeBuilderPlus.VVM;

namespace ResumeBuilderPlus.Qualifiers
{
    public class Experience : Cvobject, IParentable<Experience>
    {
        private string _years = "";

        public string Years
        {
            get { return _years; }
            set
            {
                _years = value;
                OnPropertyChanged();
            }
        }

        private string _title = "";

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _institution = "";

        public string Institution
        {
            get { return _institution; }
            set
            {
                _institution = value;
                OnPropertyChanged();
            }
        }

        private ManipulatableCollection<Description> _description = new ManipulatableCollection<Description>();

        public ManipulatableCollection<Description> Description
        {
            get { return _description; }
            set
            {
                _description = value;
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

        public override bool IsRelevant(IEnumerable<string> relevantTags)
        {
            return Locked == true || ((Tags.Any(tag => relevantTags.Contains(tag.Text)) ||
                Description.SelectMany(desc => desc.Tags).Any(tag => relevantTags.Contains(tag.Text))) && Locked == null);
        }

        public override string ToString(CvobjectType type, FormatViewModel format)
        {
            switch (type)
            {
                case CvobjectType.Cventry:
                    return MakeCventry(format, Years, Title, Institution, "", "", ParseDescription(Description));
                case CvobjectType.Cvitem:
                    return MakeCvitem(format, Title + (format.ProjectYearsNewline ? "\\\\" : " ") + Years, ParseDescription(Description));
                default:
                    throw new InvalidEnumArgumentException(@"Unhandled value: " + type.ToString());
            }
        }

        public ObservableCollection<Experience> Parent { get; set; }
        public ICommand RemoveCommand { get {return new DelegateCommand(RemoveFromCommand);} }
        public void RemoveFromCommand()
        {
            Parent.Remove(this);
        }
    }
}
