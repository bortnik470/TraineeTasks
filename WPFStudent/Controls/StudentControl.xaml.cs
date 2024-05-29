using System.Windows.Controls;
using WPFStudent.Views;
using WPFStudent.Utility;
using WPFStudent.Models;
using System.ComponentModel;

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
            if (e.AddedItems.Count == 0) return;

            var data = (Parent as Grid)?.DataContext as Data;
            var student = e.AddedItems[0] as StudentModel;
            data.CoursesViewModel.CurrentStudenId = student.StudentId;
        }
    }
}