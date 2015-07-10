using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilderPlus.VVM
{
    public class TemplateStringBool : ObservableObject
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

        private bool _editable;

        public bool Editable
        {
            get { return _editable; }
            set
            {
                _editable = value;
                OnPropertyChanged();
            }
        }

        public static TemplateStringBool Create(string text, bool editable)
        {
            return new TemplateStringBool(){Text = text, Editable = editable};
        }
    }

    public class TemplatedString : ObservableObject
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private List<TemplateStringBool> _strings;

        public List<TemplateStringBool> Strings
        {
            get { return _strings; }
            set
            {
                _strings = value;
                OnPropertyChanged();
            }
        }

        public TemplatedString(string title, List<TemplateStringBool> strings)
        {
            Title = title;
            Strings = strings;
        }

        public TemplatedString(string title, List<string> strings)
        {
            Title = title;
            Strings = strings.Select(s => TemplateStringBool.Create(s, s != "")).ToList();
        }

        public override string ToString()
        {
            return Strings.Aggregate("", (working, s) => (working + s.Text));
        }
    }
}
