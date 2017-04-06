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
    public sealed partial class Ring : UserControl
    {
        public Ring()
        {
            this.InitializeComponent();
        }

        public string genString()
        {
            string ret = "";
            for(int i=0; i < box.Children.Count; i++)
            {
                UIElement child = box.Children.ElementAt(i);
                ifelse vari = child as ifelse;
                if(vari != null)
                {
                    ret += (vari).genString();
                }
                else
                {
                    loop vari2 = child as loop;
                    if(vari2 != null)
                    {
                        ret += (vari2).genString();
                    }
                    else
                    {
                        TextBlock vari3 = child as TextBlock;
                        if (vari3 != null)
                        {
                            if (vari3.Text.Equals(App.current.@break))
                                ret += "break;\n";
                            else ret += "continue;\n";
                        }
                        else
                        {
                            StackPanel st = child as StackPanel;
                            TextBlock x = (TextBlock)st.Children.ElementAt(0);
                            TextBox y = (TextBox)st.Children.ElementAt(1);
                            if (x.Text.Equals(App.current.print))
                            {
                                ret += "cout << " + y.Text + ";\n";
                            }
                            else if (x.Text.Equals(App.current.read))
                            {
                                ret += "cin >> " + y.Text + ";\n";
                            }
                            else if (x.Text.Equals(App.current.@int))
                            {
                                ret += "long long int " + y.Text + ";\n";
                            }
                            else if (x.Text.Equals(App.current.@string))
                            {
                                ret += "string " + y.Text + ";\n";
                            }
                            else if (x.Text.Equals("equation"))
                            {
                                ret += y.Text + ";\n";
                            }
                        }
                    }
                }
            }
            return ret;
        }

        private void dragOver(object sender, DragEventArgs e)
        {
            Ring rng = sender as Ring;
            e.DragUIOverride.Caption = "Drop to create the function" + rng.box.Children.Count;
            e.DragUIOverride.IsCaptionVisible = true;
            e.DragUIOverride.IsGlyphVisible = true;
            e.DragUIOverride.IsContentVisible = true;
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
        }

        private async void DropItem(object sender, DragEventArgs e)
        {
            var def = e.GetDeferral();
            string s = await e.DataView.GetTextAsync();
            def.Complete();
            if (s.Equals(App.current.if_else) || s.Equals(App.current.@for))
            {
                RowDefinition defineRow = new RowDefinition();
                defineRow.SetValue(RowDefinition.HeightProperty, (new GridLength(1, GridUnitType.Star)));
                box.RowDefinitions.Add(defineRow);

                if (s.Equals(App.current.if_else))
                {
                    ifelse widget = new SpeakCode.ifelse();
                    widget.SetValue(Grid.RowProperty, box.Children.Count);
                    widget.SetValue(Grid.ColumnProperty, 0);

                    box.Children.Add(widget);
                }
                else if (s.Equals(App.current.@for))
                {
                    loop widget = new SpeakCode.loop();
                    widget.SetValue(Grid.RowProperty, box.Children.Count);
                    widget.SetValue(Grid.ColumnProperty, 0);

                    box.Children.Add(widget);
                }
                /*else if (s.Equals("function"))
                {
                    func widget = new SpeakCode.func();
                    widget.SetValue(Grid.RowProperty, box.Children.Count);
                    widget.SetValue(Grid.ColumnProperty, 0);

                    box.Children.Add(widget);
                }
                else if (s.Equals("return"))
                {
                    defineRow.SetValue(RowDefinition.HeightProperty, (new GridLength(1, GridUnitType.Auto)));

                    StackPanel st = new StackPanel();
                    st.Orientation = Orientation.Horizontal;
                    st.SetValue(Grid.RowProperty, box.Children.Count);
                    st.SetValue(Grid.ColumnProperty, 0);

                    TextBlock txt = new TextBlock();
                    txt.Text = "return";

                    st.Children.Add(txt);
                    st.Children.Add(new TextBox());

                    box.Children.Add(st);
                }*/
                
                this.AllowDrop = false;
            }
            else
            {
                RowDefinition defineRow = new RowDefinition();
                defineRow.SetValue(RowDefinition.HeightProperty, (new GridLength(1, GridUnitType.Auto)));
                box.RowDefinitions.Add(defineRow);

                if (s.Equals(App.current.print))
                {
                    StackPanel st = new StackPanel();
                    st.Orientation = Orientation.Horizontal;
                    st.SetValue(Grid.RowProperty, box.Children.Count);
                    st.SetValue(Grid.ColumnProperty, 0);

                    TextBlock txt = new TextBlock();
                    txt.Text = App.current.print;

                    st.Children.Add(txt);
                    st.Children.Add(new TextBox());

                    box.Children.Add(st);
                }
                else if (s.Equals(App.current.read))
                {
                    StackPanel st = new StackPanel();
                    st.Orientation = Orientation.Horizontal;
                    st.SetValue(Grid.RowProperty, box.Children.Count);
                    st.SetValue(Grid.ColumnProperty, 0);

                    TextBlock txt = new TextBlock();
                    txt.Text = App.current.read;

                    st.Children.Add(txt);
                    st.Children.Add(new TextBox());

                    box.Children.Add(st);
                }
                else if (s.Equals(App.current.@break))
                {
                    TextBlock txt = new TextBlock();
                    txt.Text = App.current.@break;
                    txt.SetValue(Grid.RowProperty, box.Children.Count);
                    txt.SetValue(Grid.ColumnProperty, 0);

                    box.Children.Add(txt);
                }
                else if (s.Equals(App.current.@continue))
                {
                    TextBlock txt = new TextBlock();
                    txt.Text = App.current.@continue;
                    txt.SetValue(Grid.RowProperty, box.Children.Count);
                    txt.SetValue(Grid.ColumnProperty, 0);

                    box.Children.Add(txt);
                }
                else if (s.Equals(App.current.@int))
                {
                    StackPanel st = new StackPanel();
                    st.Orientation = Orientation.Horizontal;
                    st.SetValue(Grid.RowProperty, box.Children.Count);
                    st.SetValue(Grid.ColumnProperty, 0);

                    TextBlock txt = new TextBlock();
                    txt.Text = App.current.@int;

                    st.Children.Add(txt);
                    st.Children.Add(new TextBox());

                    box.Children.Add(st);
                }
                else if (s.Equals(App.current.@string))
                {
                    StackPanel st = new StackPanel();
                    st.Orientation = Orientation.Horizontal;
                    st.SetValue(Grid.RowProperty, box.Children.Count);
                    st.SetValue(Grid.ColumnProperty, 0);

                    TextBlock txt = new TextBlock();
                    txt.Text = App.current.@string;

                    st.Children.Add(txt);
                    st.Children.Add(new TextBox());

                    box.Children.Add(st);
                }
                else if (s.Equals("equation"))
                {
                    StackPanel st = new StackPanel();
                    st.Orientation = Orientation.Horizontal;
                    st.SetValue(Grid.RowProperty, box.Children.Count);
                    st.SetValue(Grid.ColumnProperty, 0);

                    TextBlock txt = new TextBlock();
                    txt.Text = "equation";

                    st.Children.Add(txt);
                    st.Children.Add(new TextBox());

                    box.Children.Add(st);
                }
            }
        }
    }
}
