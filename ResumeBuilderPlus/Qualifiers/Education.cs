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
    public class Education : Cvobject, IParentable<Education>
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

        private string _localization = "";

        public string Localization
        {
            get { return _localization; }
            set
            {
                _localization = value;
                OnPropertyChanged();
            }
        }

        private string _grade = "";

        public string Grade
        {
            get { return _grade; }
            set
            {
                _grade = value;
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

        public override bool IsRelevant(IEnumerable<string> relevantTags)
        {
            return Locked == true || (Description.SelectMany(desc => desc.Tags).Any(tag => relevantTags.Contains(tag.Text)) && Locked == null);
        }

        public override string ToString(CvobjectType type, FormatViewModel format)
        {
            switch (type)
            {
                case CvobjectType.Cventry:
                    return MakeCventry(format, Years, Title, Institution, Localization, Grade, ParseDescription(Description));
                case CvobjectType.Cvitem:
                    return MakeCvitem(format, Title, ParseDescription(Description));
                default:
                    throw new InvalidEnumArgumentException(@"Unhandled value: " + type.ToString());
            }
        }

        public ObservableCollection<Education> Parent { get; set; }
        public ICommand RemoveCommand { get { return new DelegateCommand(RemoveFromCommand); } }
        public void RemoveFromCommand()
        {
            Parent.Remove(this);
        }
    }
}
