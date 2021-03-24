using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMealNotification.Model
{
    public class QParamModel : BindableBase
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}
