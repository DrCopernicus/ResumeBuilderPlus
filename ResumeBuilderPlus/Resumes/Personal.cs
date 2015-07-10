using ResumeBuilderPlus.VVM;

namespace ResumeBuilderPlus.Resumes
{
    public class Personal : ObservableObject
    {
        private string _addressLocal = "";

        public string AddressLocal
        {
            get { return _addressLocal; }
            set
            {
                _addressLocal = value;
                OnPropertyChanged();
            }
        }

        private bool _addressLocalDisplay = true;

        public bool AddressLocalDisplay
        {

            get { return _addressLocalDisplay; }
            set
            {
                _addressLocalDisplay = value;
                OnPropertyChanged();
            }
        }

        private string _addressRegional = "";

        public string AddressRegional
        {
            get { return _addressRegional; }
            set
            {
                _addressRegional = value;
                OnPropertyChanged();
            }
        }

        private bool _addressRegionalDisplay = true;

        public bool AddressRegionalDisplay
        {

            get { return _addressRegionalDisplay; }
            set
            {
                _addressRegionalDisplay = value;
                OnPropertyChanged();
            }
        }

        private string _nameFirst = "";

        public string NameFirst
        {
            get { return _nameFirst; }
            set
            {
                _nameFirst = value;
                OnPropertyChanged();
            }
        }

        private string _nameLast = "";

        public string NameLast
        {
            get { return _nameLast; }
            set
            {
                _nameLast = value;
                OnPropertyChanged();
            }
        }

        private string _phone = "";

        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }


        private bool _phoneDisplay = true;

        public bool PhoneDisplay
        {

            get { return _phoneDisplay; }
            set
            {
                _phoneDisplay = value;
                OnPropertyChanged();
            }
        }

        private string _email = "";

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private bool _emailDisplay = true;

        public bool EmailDisplay
        {

            get { return _emailDisplay; }
            set
            {
                _emailDisplay = value;
                OnPropertyChanged();
            }
        }
    }
}
