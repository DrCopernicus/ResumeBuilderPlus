using Newtonsoft.Json;
using ResumeBuilderPlus.VVM;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ResumeBuilderPlus.Resumes
{
    public class FormatViewModel : ObservableObject
    {
        [JsonIgnore]
        public string MinimumCvitemSpacingImage { get { return @"/Res/Image/mincvitemspacing.png"; } }

        [JsonIgnore]
        public string MaximumCvitemSpacingImage { get { return @"/Res/Image/maxcvitemspacing.png"; } }

        private Color _color0 = Color.FromRgb(0, 0, 0);

        public Color Color0
        {
            get { return _color0; }
            set
            {
                _color0 = value;
                OnPropertyChanged();
            }
        }

        private Color _color1 = Color.FromRgb(0, 0, 0);

        public Color Color1
        {
            get { return _color1; }
            set
            {
                _color1 = value;
                OnPropertyChanged();
            }
        }

        private Color _color2 = Color.FromRgb(0, 0, 0);

        public Color Color2
        {
            get { return _color2; }
            set
            {
                _color2 = value;
                OnPropertyChanged();
            }
        }

        private float _cvitemSpacing = -0.8f;

        public float CvitemSpacing
        {
            get { return _cvitemSpacing; }
            set
            {
                _cvitemSpacing = value;
                OnPropertyChanged();
            }
        }

        private float _borders = 0.8f;

        public float Borders
        {
            get { return _borders; }
            set
            {
                _borders = value;
                OnPropertyChanged();
            }
        }

        private bool _projectYearsNewline = false;

        public bool ProjectYearsNewline
        {
            get { return _projectYearsNewline; }
            set
            {
                _projectYearsNewline = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        private readonly ObservableCollection<string> _layoutStyles = new ObservableCollection<string>()
        {
            "casual", "classic", "oldstyle", "banking"
        };

        [JsonIgnore]
        public ObservableCollection<string> LayoutStyles
        {
            get { return _layoutStyles; }
        }

        private string _selectedLayoutStyle = "classic";

        public string SelectedLayoutStyle
        {
            get { return _selectedLayoutStyle; }
            set
            {
                _selectedLayoutStyle = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        private readonly ObservableCollection<string> _coverLetterStyles = new ObservableCollection<string>()
        {
            "no cover letter", "cover letter before resume", "cover letter after resume", "cover letter and resume separate"
        };

        [JsonIgnore]
        public ObservableCollection<string> CoverLetterStyles
        {
            get { return _coverLetterStyles; }
        }

        private string _selectedCoverLetterStyle = "cover letter before resume";

        public string SelectedCoverLetterStyle
        {
            get { return _selectedCoverLetterStyle; }
            set
            {
                _selectedCoverLetterStyle = value;
                OnPropertyChanged();
            }
        }
    }
}
