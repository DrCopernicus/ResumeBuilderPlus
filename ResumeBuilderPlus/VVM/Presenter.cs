using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Newtonsoft.Json;
using ResumeBuilderPlus.Qualifiers;
using ResumeBuilderPlus.Resumes;

namespace ResumeBuilderPlus.VVM
{
    public class Presenter : ObservableObject
    {
        private Resume _resume = new Resume();

        public Resume Resume
        {
            get { return _resume; }
            set
            {
                _resume = value;
                OnPropertyChanged();
            }
        }

        private List<TemplatedString> _templates = new List<TemplatedString>()
        {
            new TemplatedString("Introduction 1", new List<string>()
            {
                "Dear ",""," my name is ",""," and I want a job!"
            })
        };

        public List<TemplatedString> Templates
        {
            get { return _templates; }
            set
            {
                _templates = value;
                OnPropertyChanged();
            }
        }

        private TemplatedString _selectedTemplate;

        public TemplatedString SelectedTemplate
        {
            get { return _selectedTemplate; }
            set
            {
                _selectedTemplate = value;
                OnPropertyChanged();
            }
        }

        public ICommand WriteTemplateCommand
        {
            get { return new DelegateCommand(WriteTemplate);}
        }

        public void WriteTemplate()
        {
            Resume.CoverLetter.Text += SelectedTemplate.ToString();
        }

        public ICommand WriteCommand
        {
            get { return new DelegateCommand(Write); }
        }

        public void Write()
        {
            File.WriteAllText(@"output.tex", Resume.ToString(ParsedTags.Where(tag => tag.Bool).Select(tag => tag.Text)));
        }

        public ICommand SaveCommand
        {
            get { return new DelegateCommand(Save); }
        }

        public void Save()
        {
            using (StreamWriter w = new StreamWriter("qualifiers.json"))
            {
                var json = JsonConvert.SerializeObject(_resume, Formatting.Indented, new JsonSerializerSettings{ PreserveReferencesHandling = PreserveReferencesHandling.All });
                w.Write(json);
            }
        }

        public ICommand LoadCommand
        {
            get { return new DelegateCommand(Load); }
        }

        public void Load()
        {
            try
            {
                using (StreamReader r = new StreamReader("qualifiers.json"))
                {
                    var json = r.ReadToEnd();
                    Resume = JsonConvert.DeserializeObject<Resume>(json,
                        new JsonSerializerSettings {PreserveReferencesHandling = PreserveReferencesHandling.All});
                }
            }
            catch (FileNotFoundException e)
            {
            }
            catch (IOException e)
            {
            }
        }

        public ICommand ParseCommand
        {
            get { return new DelegateCommand(Parse);}
        }

        public void Parse()
        {
            ParsedTags.Clear();
            var tags = Resume.AllTags;
            string lowerReqs = JobRequirements.ToLower();
            foreach (var tag in tags)
            {
                if (lowerReqs.Contains(tag.Text.ToLower()))
                    tag.Bool = true;
                ParsedTags.Add(tag);
            }
        }

        public ICommand SelectAllTagsCommand
        {
            get { return new DelegateCommand(SelectAllTags); }
        }

        public void SelectAllTags()
        {
            foreach (var tag in ParsedTags)
            {
                tag.Bool = true;
            }
        }

        public ICommand DeselectAllTagsCommand
        {
            get { return new DelegateCommand(DeselectAllTags); }
        }

        public void DeselectAllTags()
        {
            foreach (var tag in ParsedTags)
            {
                tag.Bool = false;
            }
        }

        private string _jobRequirements = "";

        public string JobRequirements
        {
            get { return _jobRequirements; }
            set
            {
                _jobRequirements = value;
                OnPropertyChanged();
            }
        }

        private readonly ObservableCollection<StringAndBool> _parsedTags = new ObservableCollection<StringAndBool>();

        public ObservableCollection<StringAndBool> ParsedTags
        {
            get { return _parsedTags; }
        }

        public Presenter()
        {
            Load();
        }
    }
}
