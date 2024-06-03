using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPFStudent.Models;
using WPFStudent.Utility;

namespace WPFStudent.Views
{
    public abstract class BaseView : BaseModel
    {
        public abstract Task LoadAsync();
    }
}