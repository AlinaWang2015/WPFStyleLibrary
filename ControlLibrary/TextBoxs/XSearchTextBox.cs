using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ControlLibrary.TextBoxs
{
    public class XSearchTextBox : TextBox
    {
        static XSearchTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XSearchTextBox), new FrameworkPropertyMetadata(typeof(XSearchTextBox)));
        }

        [CategoryAttribute("自定义属性"), DescriptionAttribute("获取或设置默认文字")]
        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }
        [CategoryAttribute("自定义属性"), DescriptionAttribute("获取或设置默认文字")]
        public static readonly DependencyProperty PlaceHolderProperty = DependencyProperty.Register("PlaceHolder", typeof(string), typeof(XSearchTextBox));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ButtonBase clearBtn = this.Template.FindName("PART_ContentHostClearButton", this) as ButtonBase;
            ButtonBase searchBtn = this.Template.FindName("PART_ContentHostSeachButton", this) as ButtonBase;

            searchBtn.Visibility = Visibility.Visible;
            clearBtn.Visibility = Visibility.Collapsed;
            this.Text = PlaceHolder;
            this.Opacity = 0.4;

            clearBtn.Click += (s, e) =>
            {
                this.Text = "";
            };

            this.LostFocus += (s, e) =>
            {
                if (this.Text.Length == 0)
                {
                    this.Text = PlaceHolder;
                    this.Opacity = 0.4;
                    searchBtn.Visibility = Visibility.Visible;
                    clearBtn.Visibility = Visibility.Collapsed;
                }
                else
                {
                    clearBtn.Visibility = Visibility.Visible;
                    searchBtn.Visibility = Visibility.Collapsed;
                }
            };

            this.GotFocus += (s, e) =>
            {
                if (this.Text == PlaceHolder)
                {
                    this.Text = "";
                }
                clearBtn.Visibility = Visibility.Visible;
                searchBtn.Visibility = Visibility.Collapsed;
            };
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (this.Text != PlaceHolder)
            {
                base.OnTextChanged(e);
            }

            if (this.Template != null)
            {
                ButtonBase clearBtn = this.Template.FindName("PART_ContentHostClearButton", this) as ButtonBase;
                ButtonBase searchBtn = this.Template.FindName("PART_ContentHostSeachButton", this) as ButtonBase;
                this.Opacity = 1;

                if (this.Text.Length > 0)
                {
                    if (this.Text != PlaceHolder)
                    {
                        clearBtn.Visibility = Visibility.Visible;
                        searchBtn.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        this.Opacity = 0.4;
                        searchBtn.Visibility = Visibility.Visible;
                        clearBtn.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    if (this.IsFocused)
                    {
                        clearBtn.Visibility = Visibility.Visible;
                        searchBtn.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        this.Text = PlaceHolder;
                    }
                }
            }
        }
    }
}
