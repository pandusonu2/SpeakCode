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
    public sealed partial class func : UserControl
    {
        public func()
        {
            this.InitializeComponent();
        }

        private void AddElement(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            if (bt.Content.Equals("Integer"))
            {
                TextBlock txt = new TextBlock();
                txt.Text = "int var" + args.Children.Count; 

                args.Children.Add(txt);
            }
            else
            {
                TextBlock txt = new TextBlock();
                txt.Text = "string var" + args.Children.Count;

                args.Children.Add(txt);
            }
        }
    }
}
