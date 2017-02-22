using ResumeBuilderPlus.Resumes;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ResumeBuilderPlus.Printing
{
    public class DocumentPrinter
    {
        private readonly CoverLetterWriter _coverLetterWriter;
        private readonly ResumeWriter _resumeWriter;

        public DocumentPrinter()
        {
            _coverLetterWriter = new CoverLetterWriter();
            _resumeWriter = new ResumeWriter();
        }

        public void PrintDocuments(Resume resume, CoverLetterFormatType type, IEnumerable<string> relevantTags)
        {
            foreach (var kvp in GetDocuments(resume, type, relevantTags.ToArray()))
            {
                File.WriteAllText(kvp.Key, kvp.Value);
            }
        }

        private Dictionary<string, string> GetDocuments(Resume resume, CoverLetterFormatType type,
            string[] relevantTags)
        {
            var dictionary = new Dictionary<string, string>();
            switch (type)
            {
                case CoverLetterFormatType.NoCoverLetter:
                    dictionary.Add("resume.tex", GetHeader(resume)
                                                 + _resumeWriter.Print(resume, relevantTags)
                                                 + GetFooter());
                    break;
                case CoverLetterFormatType.BeforeResume:
                    dictionary.Add("resume.tex", GetHeader(resume)
                                                 + _coverLetterWriter.Print(resume.CoverLetter)
                                                 + GetClearPage()
                                                 + _resumeWriter.Print(resume, relevantTags)
                                                 + GetFooter());
                    break;
                case CoverLetterFormatType.AfterResume:
                    dictionary.Add("resume.tex", GetHeader(resume)
                                                 + _resumeWriter.Print(resume, relevantTags)
                                                 + _coverLetterWriter.Print(resume.CoverLetter)
                                                 + GetFooter());
                    break;
                case CoverLetterFormatType.SeparateFiles:
                    dictionary.Add("resume.tex", GetHeader(resume)
                                                 + _resumeWriter.Print(resume, relevantTags)
                                                 + GetFooter());
                    dictionary.Add("cover.tex", GetHeader(resume)
                                                + _coverLetterWriter.Print(resume.CoverLetter)
                                                + GetFooter());
                    break;
            }
            return dictionary;
        }

        private string GetClearPage()
        {
            return "\\clearpage\n";
        }

        private string GetHeader(Resume resume)
        {
            return "\\documentclass[11pt,a4paper,sans]{moderncv}\n\\moderncvstyle{" + resume.Format.SelectedLayoutStyle + "}\n"
                + "\\definecolor{color0}{RGB}{" + resume.Format.Color0.R + "," + resume.Format.Color0.G + "," + resume.Format.Color0.B + "}\n"
                + "\\definecolor{color1}{RGB}{" + resume.Format.Color1.R + "," + resume.Format.Color1.G + "," + resume.Format.Color1.B + "}\n"
                + "\\definecolor{color2}{RGB}{" + resume.Format.Color2.R + "," + resume.Format.Color2.G + "," + resume.Format.Color2.B + "}\n"
                + "\\usepackage[scale=" + resume.Format.Borders + "]{geometry}\n"
                + "\\firstname{" + resume.PersonalInfo.NameFirst + "}\n"
                + "\\familyname{" + resume.PersonalInfo.NameLast + "}\n"
                + "\\begin{document}\n"
                + (resume.PersonalInfo.AddressLocalDisplay || resume.PersonalInfo.AddressRegionalDisplay ?
                    "\\address{" + (resume.PersonalInfo.AddressLocalDisplay ? resume.PersonalInfo.AddressLocal : "") + "}{" + (resume.PersonalInfo.AddressRegionalDisplay ? resume.PersonalInfo.AddressRegional : "") + "}\n" : "")
                + (resume.PersonalInfo.PhoneDisplay ? "\\phone{" + resume.PersonalInfo.Phone + "}\n" : "")
                + (resume.PersonalInfo.EmailDisplay ? "\\email{" + resume.PersonalInfo.Email + "}\n" : "")
                + (resume.PersonalInfo.WebsiteDisplay ? "\\extrainfo{\\httplink{" + resume.PersonalInfo.Website + "}}\n" : "");
        }

        private string GetFooter()
        {
            return "\\end{document}\n";
        }
    }
}