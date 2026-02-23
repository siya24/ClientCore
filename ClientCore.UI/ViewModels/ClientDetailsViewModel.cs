namespace ClientCore.UI.ViewModels
{
    public class ClientDetailsViewModel
    {
        public GetClientDTO? Client { get; set; }
        public List<GetContactDTO>? Contacts { get; set; }
    }
}