namespace ClientCore.UI.Controllers
{
      public class ContactsController(IClientService clientService, IContactService contactService, IConfiguration configuration) : Controller
    {
        private readonly IClientService _clientService = clientService;
        private readonly IContactService _contactService = contactService;
        private readonly RestClient contact = new(configuration["BaseUrl"]);

        public async Task<IActionResult> Index()
        {
            try
            {
                var request = new RestRequest("contacts", Method.Get);

                var contacts = await contact.GetAsync<List<GetContactDTO>>(request);
         
                return View(contacts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateContactDTO createContactDTO)
        {
            var request = new RestRequest("contacts", Method.Post);

             request.AddJsonBody(new { name = createContactDTO.Name, surname = createContactDTO.Surname, email = createContactDTO.Email });

            var response = await contact.ExecuteAsync<Contact>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error creating contact: " + response.ErrorMessage);
            return View(createContactDTO);
        }
        
    }
}
