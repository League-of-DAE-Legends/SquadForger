using System.Windows;
using System.Windows.Controls;

namespace SquadForger.Components
{
    public partial class BindabalePasswordBox : UserControl
    {
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register(
           "Password", typeof(string), typeof(BindabalePasswordBox), new PropertyMetadata("Discord Webhook ID"));

        public string Password  
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }
        
        public BindabalePasswordBox()
        {
            InitializeComponent();
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password=passwordBox.Password;
        }
    }
}