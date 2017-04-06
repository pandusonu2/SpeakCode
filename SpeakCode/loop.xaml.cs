using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SpeakCode
{
    public sealed partial class loop : UserControl
    {
        public loop()
        {
            this.InitializeComponent();
            lop.Text = App.current.@for;
        }
        public string genString()
        {
            string ret = "";
            ret += "for (int " + vari.Text + " = " + start.Text + "; " + condition.Text + "; " + vari.Text + " += " + incre.Text + ") {\n";
            ret += body.genString();
            ret += "}\n";
            ret += below.genString();
            return ret;
        }
    }
}
