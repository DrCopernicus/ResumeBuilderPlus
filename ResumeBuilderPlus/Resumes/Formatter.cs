namespace ResumeBuilderPlus.Resumes
{
    public static class Formatter
    {
        public static string LaTeXify(this string input)
        {
            return input.Replace("#", "\\#").Replace("LaTeX", "\\LaTeX{}").Replace("%", "\\%");
        }
    }
}
