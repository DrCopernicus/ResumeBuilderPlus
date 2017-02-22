using ResumeBuilderPlus.Qualifiers.Descriptors;
using ResumeBuilderPlus.Resumes;
using ResumeBuilderPlus.VVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ResumeBuilderPlus.Qualifiers
{
    public enum CvobjectType
    {
        Cvitem,
        Cventry
    }

    public abstract class Cvobject : ObservableObject
    {
        private bool? _locked = null;

        public bool? Locked
        {
            get { return _locked; }
            set
            {
                _locked = value;
                OnPropertyChanged();
            }
        }

        public abstract bool IsRelevant(IEnumerable<string> relevantTags);

        protected string MakeCvitem(FormatViewModel format, string type, string text)
        {
            return "\\cvitem[" + format.CvitemSpacing + "em]{" + type.LaTeXify() + "}{" + text.LaTeXify() + "}";
        }

        protected string MakeCventry(FormatViewModel format, string years, string title, string institution, string localization, string grade, string description)
        {
            return "\\cventry{"
            + years.LaTeXify() + "}{"
            + title.LaTeXify() + "}{"
            + institution.LaTeXify() + "}{"
            + localization.LaTeXify() + "}{"
            + grade.LaTeXify() + "}{"
            + description.LaTeXify() + "}";
        }

        protected string ParseDescription(ObservableCollection<Description> description)
        {
            var relevantDescriptions = description.Where(desc => desc.Relevant).ToList();

            if (!relevantDescriptions.Any())
                return "";

            return "\n\\begin{itemize}\n"
                   + "\\item " + string.Join("\n\\item ", relevantDescriptions)
                   + "\n\\end{itemize}\n";
        }

        public abstract string ToString(CvobjectType type, FormatViewModel format);
    }
}
