using Newtonsoft.Json;
using ResumeBuilderPlus.Qualifiers;
using ResumeBuilderPlus.Qualifiers.Collections;
using ResumeBuilderPlus.VVM;
using System.Collections.Generic;
using System.Linq;

namespace ResumeBuilderPlus.Resumes
{
    public class StringAndBool : ObservableObject
    {
        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private bool _bool;

        public bool Bool
        {
            get { return _bool; }
            set
            {
                _bool = value;
                OnPropertyChanged();
            }
        }

        public static StringAndBool Create(string text, bool bol)
        {
            return new StringAndBool() {Text = text, Bool = bol};
        }

        public override bool Equals(object o)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }
    }

    public class Resume : ObservableObject
    {
        private ManipulatableCollection<Education> _education = new ManipulatableCollection<Education>();

        public ManipulatableCollection<Education> Education
        {
            get { return _education; }
            set
            {
                _education = value;
                OnPropertyChanged();
            }
        }

        private ManipulatableCollection<Experience> _experience = new ManipulatableCollection<Experience>();

        public ManipulatableCollection<Experience> Experience
        {
            get { return _experience; }
            set
            {
                _experience = value;
                OnPropertyChanged();
            }
        }

        private ManipulatableCollection<Project> _projects = new ManipulatableCollection<Project>();

        public ManipulatableCollection<Project> Projects
        {
            get { return _projects; }
            set
            {
                _projects = value;
                OnPropertyChanged();
            }
        }

        private ManipulatableCollection<Cvitem> _skills = new ManipulatableCollection<Cvitem>();

        public ManipulatableCollection<Cvitem> Skills
        {
            get { return _skills; }
            set
            {
                _skills = value;
                OnPropertyChanged();
            }
        }

        private FormatViewModel _format = new FormatViewModel();

        public FormatViewModel Format
        {
            get { return _format; }
            set
            {
                _format = value;
                OnPropertyChanged();
            }
        }

        private Personal _personalInfo = new Personal();

        public Personal PersonalInfo
        {
            get { return _personalInfo; }
            set
            {
                _personalInfo = value;
                OnPropertyChanged();
            }
        }

        private CoverLetter _coverLetter = new CoverLetter();

        public CoverLetter CoverLetter
        {
            get { return _coverLetter; }
            set
            {
                _coverLetter = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public IEnumerable<StringAndBool> AllTags
        {
            get
            {
                IEnumerable<StringAndBool> list = new List<StringAndBool>();

                list = Education.SelectMany(ed => ed.Description)
                    .Aggregate(list, (current, desc) => current.Union(desc.Tags.Select(tag => StringAndBool.Create(tag.Text, false))));

                list = Projects.Aggregate(list, (current, pj) => current.Union(pj.Tags.Select(tag => StringAndBool.Create(tag.Text, false))));

                list = Projects.SelectMany(pj => pj.Description)
                    .Aggregate(list, (current, desc) => current.Union(desc.Tags.Select(tag => StringAndBool.Create(tag.Text, false))));

                return list;
            }
        }
    }
}
