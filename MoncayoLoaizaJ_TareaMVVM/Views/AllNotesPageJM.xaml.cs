namespace MoncayoLoaizaJ_TareaMVVM.Views;

public partial class AllNotesPageJM : ContentPage
{
	public AllNotesPageJM()
	{
		InitializeComponent();
        
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }

}