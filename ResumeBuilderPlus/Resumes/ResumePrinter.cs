using ResumeBuilderPlus.Qualifiers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace ResumeBuilderPlus.Resumes
{
    public class ResumePrinter
    {
        public string Print(Resume resume, IEnumerable<string> relevantTags)
        {
            string ending;
            using (StreamReader r = new StreamReader("ending.txt"))
            {
                ending = r.ReadToEnd();
            }
            return "\\documentclass[11pt,a4paper,sans]{moderncv}\n\\moderncvstyle{" + resume.Format.SelectedLayoutStyle + "}\n"
                + "\\definecolor{color0}{RGB}{" + resume.Format.Color0.R + "," + resume.Format.Color0.G + "," + resume.Format.Color0.B + "}\n"
                + "\\definecolor{color1}{RGB}{" + resume.Format.Color1.R + "," + resume.Format.Color1.G + "," + resume.Format.Color1.B + "}\n"
                + "\\definecolor{color2}{RGB}{" + resume.Format.Color2.R + "," + resume.Format.Color2.G + "," + resume.Format.Color2.B + "}\n"
                + "\\usepackage[scale=" + resume.Format.Borders + "]{geometry}\n"
                + "\\firstname{" + resume.PersonalInfo.NameFirst + "}\n"
                + "\\familyname{" + resume.PersonalInfo.NameLast + "}\n"
                + "\\begin{document}\n"
                + "\\title{R\\'esum\\'e}\n"
                + (resume.PersonalInfo.AddressLocalDisplay || resume.PersonalInfo.AddressRegionalDisplay ?
                    "\\address{" + (resume.PersonalInfo.AddressLocalDisplay ? resume.PersonalInfo.AddressLocal : "") + "}{" + (resume.PersonalInfo.AddressRegionalDisplay ? resume.PersonalInfo.AddressRegional : "") + "}\n" : "")
                + (resume.PersonalInfo.PhoneDisplay ? "\\phone{" + resume.PersonalInfo.Phone + "}\n" : "")
                + (resume.PersonalInfo.EmailDisplay ? "\\email{" + resume.PersonalInfo.Email + "}\n" : "")
                + (resume.PersonalInfo.WebsiteDisplay ? "\\extrainfo{\\httplink{" + resume.PersonalInfo.Website + "}}\n" : "")
                + "\\makecvtitle\n"
                + Parse(CvobjectType.Cventry, resume.Format, resume.Education, relevantTags, "Education") + "\n"
                + Parse(CvobjectType.Cventry, resume.Format, resume.Experience, relevantTags, "Experience") + "\n"
                + Parse(CvobjectType.Cvitem, resume.Format, resume.Projects, relevantTags, "Projects") + "\n"
                + ParseCvitems(resume.Skills, resume.Format, "Skills") + "\n"
                + ending + "\n"
                + resume.CoverLetter.Text + "\n"
                + "\\makeletterclosing\n"
                + "\\end{document}";
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