using System.Windows.Controls;
using WPFStudent.Views;
using WPFStudent.Utility;
using WPFStudent.Models;

namespace WPFStudent.Controls
{
    public partial class StudentControl : UserControl
    {
        private StudentsView StudentsView; 

        public StudentControl()
        {
            InitializeComponent();

            StudentsView = new StudentsView(new StudentDbLoader());

            DataContext = StudentsView;

            StudentsView.LoadAsync();
        }
    }
}
