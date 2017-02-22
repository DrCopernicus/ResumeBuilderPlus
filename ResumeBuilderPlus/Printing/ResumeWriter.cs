using ResumeBuilderPlus.Qualifiers;
using ResumeBuilderPlus.Resumes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ResumeBuilderPlus.Printing
{
    public class ResumeWriter
    {
        public string Print(Resume resume, IEnumerable<string> relevantTags)
        {
            return "\\title{R\\'esum\\'e}\n"
                   + "\\makecvtitle\n"
                   + Parse(CvobjectType.Cventry, resume.Format, resume.Education, relevantTags, "Education") + "\n"
                   + Parse(CvobjectType.Cventry, resume.Format, resume.Experience, relevantTags, "Experience") + "\n"
                   + Parse(CvobjectType.Cvitem, resume.Format, resume.Projects, relevantTags, "Projects") + "\n"
                   + ParseCvitems(resume.Skills, resume.Format, "Skills") + "\n";
        }

        private string Parse(CvobjectType type, FormatViewModel format, IReadOnlyCollection<Cvobject> cventries, IEnumerable<string> relevantTags, string name)
        {
            if (cventries == null || cventries.Count <= 0)
                return "";

            var str = "\\section{" + name + "}\n";

            return cventries.Where(cventry => cventry.IsRelevant(relevantTags)).Aggregate(str, (working, cventry) =>
                working + cventry.ToString(type, format) + "\n");
        }

        private string ParseCvitems(ObservableCollection<Cvitem> cvitems, FormatViewModel format, string name)
        {
            if (cvitems == null || cvitems.Count <= 0)
                return "";

            var str = "\\section{" + name + "}\n";

            var addedCvitems = new Dictionary<string, List<string>>();

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