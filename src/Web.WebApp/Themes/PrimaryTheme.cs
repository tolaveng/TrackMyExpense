using MudBlazor;

namespace Web.WebApp.Themes
{
    public static class PrimaryTheme
    {
        public static MudTheme Default => new ()
        {
            Palette = new Palette()
            {
                Primary = "#009688",
                Secondary = "#7C4DFF",
                AppbarBackground = "#FFFFFF",
            },
            PaletteDark = new Palette()
            {
                Primary = Colors.Blue.Lighten1
            },
        };
    }
}
