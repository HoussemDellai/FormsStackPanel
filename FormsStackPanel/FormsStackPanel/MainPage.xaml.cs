using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FormsStackPanel
{
    public partial class MainPage : ContentPage
    {
        private readonly List<string> _tags = new List<string>
        {
            "Python", "PHP", "Java", "Pascal","C#",
            "Scala", "C++", "Objective C", "Swift"
        };

        private const double MarginsWidth = 4;
        private readonly StackLayout _childStack;
        private readonly StackLayout _parentStack;
        private double _widthChildStack;
        private const double WidthParentStack = 320;

        public MainPage()
        {
            InitializeComponent();

            Title = "Favorite languages";

            var entry = new Entry
            {
                Text = "Ruby"
            };
            var button = new Button
            {
                Text = "Add",
                BackgroundColor = Color.DarkGreen,
                TextColor = Color.White
            };
            button.Clicked += async (sender, args) =>
            {
                var tag = entry.Text;

                await AddTag(tag);
            };

            _childStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 4
            };
            _parentStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                WidthRequest = WidthParentStack,
                Spacing = 4,
                Children = { entry, button, _childStack }
            };

            Content = new Grid
            {
                Children = { _parentStack },
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center
            };

            CreateStackPanel();
        }

        protected async void CreateStackPanel()
        {

            foreach (var tag in _tags)
            {
                await AddTag(tag);
            }
        }

        private async Task AddTag(string tag)
        {
            var button = new Button
            {
                Text = "#" + tag,
                TextColor = Color.White,
                BackgroundColor = Color.BlueViolet,
                BorderRadius = 6,
            };
            button.Clicked += (sender, args) =>
            {
                foreach (var child in _parentStack.Children)
                {
                    var stack = child as StackLayout;
                    if (stack != null && stack.Children.Contains(button))
                    {
                        stack.Children.Remove(button);
                    }
                }
            };

            _childStack.Children.Add(button);

            await Task.Delay(950);

            _widthChildStack += button.Width;

            if (WidthParentStack <= _widthChildStack)
            {
                _childStack.Children.Remove(button);
                _parentStack.Children.Add(CopyOfStackLayout());

                _childStack.Children.Clear();

                _childStack.Children.Add(button);
                _widthChildStack = button.Width + MarginsWidth;
            }
        }

        private View CopyOfStackLayout()
        {
            var copyStackLayout = new StackLayout
            {
                Orientation = _childStack.Orientation,
                HorizontalOptions = _childStack.HorizontalOptions,
                Spacing = _childStack.Spacing
            };

            var children = _childStack.Children.ToList();
            foreach (var stackLayoutChild in children)
            {
                _childStack.Children.Remove(stackLayoutChild);
                copyStackLayout.Children.Add(stackLayoutChild);
            }

            return copyStackLayout;
        }
    }
}
