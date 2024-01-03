namespace MoncayoLoaizaJ_TareaMVVM
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.NotePageJM), typeof(Views.NotePageJM));
        }
    }
}