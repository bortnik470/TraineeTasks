using System.Windows;
using WPFStudent.Models;

namespace WPFStudent
{
    public partial class MainWindow : Window
    {
        public Data _data;

        public MainWindow()
        {
            InitializeComponent();

            _data = new Data();

            DataContext = _data;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            _data.StudentsViewModel.IsSearchState = false;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            _data.StudentsViewModel.IsSearchState = true;
        }

        private void CreateCourseBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateStudentBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}