using ResumeBuilderPlus.Resumes;

namespace ResumeBuilderPlus.Printing
{
    public class CoverLetterWriter
    {
        public string Print(CoverLetter coverLetter)
        {
            return
                "\\clearpage\n\n\\recipient{HR Department}{Corporation\\\\123 Pleasant Lane\\\\12345 City, State}\n\\date{\\today}\n\\opening{Dear Sir or Madam,}\n\\closing{Sincerly,}\n\\enclosure[Attached]{r\\\'esum\\\'e}\n\n"
                + "\\title{Cover Letter}\n"
                + "\\makelettertitle\n"
                + coverLetter.Text + "\n"
                + "\\makeletterclosing\n";
        }
    }
}