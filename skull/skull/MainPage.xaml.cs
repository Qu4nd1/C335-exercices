namespace skull
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void Onclick(object sender, EventArgs e)
        {
            var m = Target.Margin;
            Button button = (Button)sender;
            if (button == leftButton)
            {
                Target.TranslationX -= 5;
            }
            else
            {
                Target.TranslationX += 5;

            }
        }
    }
}
