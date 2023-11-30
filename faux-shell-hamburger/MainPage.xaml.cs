using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace faux_shell_hamburger
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public MainPage() => InitializeComponent();
        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
        private async void ShowFlyout(object sender, EventArgs e)
        {
            Overlay.IsVisible = true;
            await Flyout.TranslateTo(0, 0, 250, Easing.SinInOut);
        }
        private async void HideFlyout(object sender, EventArgs e)
        {
            await Flyout.TranslateTo(-250, 0, 250, Easing.SinInOut);
            Overlay.IsVisible = false;
        }
        private void OnMenuItemTap(object sender, TappedEventArgs e)
        {
            if(e.Parameter is string text)
            {
                switch (text) 
                {
                    case "Flights":
                        break;
                    case "Beach Activities":
                        break;
                    case "Mail":
                        break;
                    case "Photos":
                        break;
                    case "Awards":
                        break;
                }
            }
            HideFlyout(sender, e);
        }
        private void OnOverlayTap(object sender, TappedEventArgs e) =>
            HideFlyout(sender, e);
    }
}
