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
    public sealed partial class ifelse : UserControl
    {
        public static string ifs;
        public static string elses;
        public ifelse()
        {
            this.InitializeComponent();
            elses= App.current.if_else.Substring(App.current.if_else.IndexOf(" ") + 1);
            startElse.Content += elses;
            ifs = App.current.if_else.Substring(0, App.current.if_else.IndexOf(" "));
            startIf.Text = ifs;
        }

        public string genString()
        {
            string ret = "";
            for(int i = 0; i < MainGrid.Children.Count; i ++)
            {
                UIElement child = MainGrid.Children.ElementAt(i);
                Button bt = child as Button;
                if (bt != null)
                    break;
                TextBlock txt = child as TextBlock;
                if (txt != null)
                {
                    ret +=  "else \n";
                    continue;
                }
                Ring rng = child as Ring;
                if (rng != null)
                {
                    ret += "{\n";
                    ret += rng.genString();
                    ret += "}\n";
                }
                StackPanel st = child as StackPanel;
                if (st != null)
                {
                    foreach(UIElement innerChild in st.Children)
                    {
                        TextBlock txt2 = innerChild as TextBlock;
                        if (txt2 != null)
                        {
                            if (txt2.Text.Equals(ifs))
                                ret += "if";
                            else if (txt2.Text.Equals(elses))
                                ret += "else";
                            else if (txt2.Text.Equals(elses + " " + ifs))
                                ret += "else if";
                        }
                        TextBox txtbox = innerChild as TextBox;
                        if (txtbox != null)
                        {
                            ret += "(" + txtbox.Text + ")\n";
                        }
                    }
                }
            }
            ret += below.genString();
            return ret;
        }

        private void ExtendTree(object sender, RoutedEventArgs e)
        {
            Button sendie = (Button)sender;
            int indexAt = MainGrid.Children.IndexOf(sendie);
            if(sendie.Content.Equals("+ " + elses))
            {
                MainGrid.Children.RemoveAt(indexAt);

                TextBlock el = new TextBlock();
                el.Text = elses;
                el.SetValue(Grid.RowProperty, indexAt);

                Ring rng = new SpeakCode.Ring();
                rng.SetValue(Grid.RowProperty, indexAt + 1);

                Button bt = new Button();
                bt.Content = "+ " + ifs;
                bt.Click += ExtendTree;
                bt.SetValue(Grid.RowProperty, indexAt + 2);

                MainGrid.Children.Add(el);
                MainGrid.Children.Add(rng);
                MainGrid.Children.Add(bt);

                RowDefinition defineRow2 = new RowDefinition();
                defineRow2.SetValue(RowDefinition.HeightProperty, (new GridLength(50, GridUnitType.Pixel)));
                MainGrid.RowDefinitions.Add(defineRow2);

                RowDefinition defineRow3 = new RowDefinition();
                defineRow3.SetValue(RowDefinition.HeightProperty, (new GridLength(1, GridUnitType.Auto)));
                MainGrid.RowDefinitions.Add(defineRow3);
            }
            else
            {
                MainGrid.Children.RemoveAt(indexAt - 2);

                StackPanel st = new StackPanel();
                st.Orientation = Orientation.Horizontal;
                st.SetValue(Grid.RowProperty, indexAt - 2);

                TextBlock ifel = new TextBlock();
                ifel.Text = elses + " " + ifs;

                TextBox cond = new TextBox();

                st.Children.Add(ifel);
                st.Children.Add(cond);

                sendie.Content = "+ " + elses;

                MainGrid.Children.Insert(indexAt - 2, st);
            }
        }
    }
}
