using System.Windows.Controls;
using WPFStudent.Models;

namespace WPFStudent.Controls
{
    public partial class StudentControl : UserControl
    {
        public StudentControl()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (sender as ListView).Items.Refresh();

            if (e.AddedItems.Count == 0) return;

            var data = (Parent as Grid)?.DataContext as Data;
            var student = e.AddedItems[0] as StudentModel;
            data.CoursesViewModel.CurrentStudenId = student?.StudentId;
            data.CoursesViewModel.CurrentCourse = null;
        }
    }
}