using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FormsStackPanel
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Title = "Favorite languages";

            Content = new FormsStackPanel();
        }
    }
}
