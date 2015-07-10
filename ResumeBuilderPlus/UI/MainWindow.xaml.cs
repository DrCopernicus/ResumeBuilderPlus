using System.Windows;

namespace ResumeBuilderPlus.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            InsertTemplateTextDialog window = new InsertTemplateTextDialog();
            window.DataContext = this.DataContext;
            window.Show();
        }
    }
}
