using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ResumeBuilderPlus.Qualifiers;
using ResumeBuilderPlus.VVM;

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
        private ObservableCollection<Education> _education = new ObservableCollection<Education>();
 
        public ObservableCollection<Education> Education
        {
            get { return _education; }
            set
            {
                _education = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Cvitem> _skills = new ObservableCollection<Cvitem>();

        public ObservableCollection<Cvitem> Skills
        {
            get { return _skills; }
            set
            {
                _skills = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Project> _projects = new ObservableCollection<Project>();

        public ObservableCollection<Project> Projects
        {
            get { return _projects; }
            set
            {
                _projects = value;
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

        public string ToString(IEnumerable<string> relevantTags)
        {
            string body = Parse(CvobjectType.Cventry, Format, Education, relevantTags, "Education") + ParseCvitems(Skills, Format, "Skills") + Parse(CvobjectType.Cvitem, Format, Projects, relevantTags, "Projects");
            string ending;
            using (StreamReader r = new StreamReader("ending.txt"))
            {
                ending = r.ReadToEnd();
            }
            return "\\documentclass[11pt,a4paper,sans]{moderncv}\n\\moderncvstyle{classic}\n"
                + "\\definecolor{color0}{RGB}{" + Format.Color0.R + "," + Format.Color0.G + "," + Format.Color0.B + "}\n"
                + "\\definecolor{color1}{RGB}{" + Format.Color1.R + "," + Format.Color1.G + "," + Format.Color1.B + "}\n"
                + "\\definecolor{color2}{RGB}{" + Format.Color2.R + "," + Format.Color2.G + "," + Format.Color2.B + "}\n"
                + "\\usepackage[scale=" + Format.Borders + "]{geometry}\n"
                + "\\firstname{" + PersonalInfo.NameFirst + "}\n"
                + "\\familyname{" + PersonalInfo.NameLast + "}\n"
                + "\\begin{document}\n"
                + "\\title{R\\'esum\\'e}\n"
                + (PersonalInfo.AddressLocalDisplay || PersonalInfo.AddressRegionalDisplay ?
                    "\\address{" + (PersonalInfo.AddressLocalDisplay ? PersonalInfo.AddressLocal : "") + "}{" + (PersonalInfo.AddressRegionalDisplay ? PersonalInfo.AddressRegional : "") + "}\n" : "")
                + (PersonalInfo.PhoneDisplay ? "\\phone{" + PersonalInfo.Phone + "}\n" : "")
                + (PersonalInfo.EmailDisplay ? "\\email{" + PersonalInfo.Email + "}\n" : "")
                + "\\makecvtitle\n"
                + body + "\n"
                + ending + "\n"
                + CoverLetter.Text + "\n"
                + "\\makeletterclosing\n"
                + "\\end{document}";
        }

        private string Parse(CvobjectType type, FormatViewModel format, IReadOnlyCollection<Cvobject> cventries, IEnumerable<string> relevantTags, string name)
        {
            if (cventries == null || cventries.Count <= 0)
                return "";

            string str = "\\section{" + name + "}\n";

            return cventries.Where(cventry => cventry.IsRelevant(relevantTags)).Aggregate(str, (working, cventry) =>
                working + cventry.ToString(type, format) + "\n");
        }

        private string ParseCvitems(ObservableCollection<Cvitem> cvitems, FormatViewModel format, string name)
        {
            if (cvitems == null || cvitems.Count <= 0)
                return "";

            string str = "\\section{"+name+"}\n";

            Dictionary<string, List<string>> addedCvitems = new Dictionary<string, List<string>>();

            foreach (var cvitem in cvitems.Where(cvitem => cvitem.Relevant))
            {
                if (!addedCvitems.ContainsKey(cvitem.Type))
                    addedCvitems[cvitem.Type] = new List<string>();

                addedCvitems[cvitem.Type].Add(cvitem.Text);
            }

            return addedCvitems.Aggregate(str, (working, category) => working + MakeCvitem(category.Key, string.Join(", ", category.Value)) + "\n");
        }

        private string MakeCvitem(string type, string text)
        {
            return "\\cvitem{" + type.LaTeXify() + "}{" + text.LaTeXify() + "}";
        }
    }
}
