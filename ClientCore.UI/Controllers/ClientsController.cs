namespace ClientCore.UI.Controllers
{
    public class ClientsController(IClientService clientService, IContactService contactService, IConfiguration configuration) : Controller
    {
        private readonly IClientService _clientService = clientService;
        private readonly IContactService _contactService = contactService;
        private readonly RestClient client = new(configuration["BaseUrl"]);

        public async Task<IActionResult> Index()
        {
            try
            {
                var request = new RestRequest("clients", Method.Get);

                var clients = await client.GetAsync<List<GetClientDTO>>(request);

                return View(clients);
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
        public async Task<IActionResult> Create(CreateClientDTO CreateClientDTO)
        {
            var request = new RestRequest("clients", Method.Post);

            request.AddJsonBody(new { name = CreateClientDTO.Name });

            var response = await client.ExecuteAsync<Client>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error creating client: " + response.ErrorMessage);
            return View(CreateClientDTO);
        }
        public async Task<IActionResult> Details(string id)
        {
            try
            {

                var clientRequest = new RestRequest("clients/{id}", Method.Get);
                clientRequest.AddParameter("id", id, ParameterType.UrlSegment);
                var clients = await client.ExecuteAsync<GetClientDTO>(clientRequest);


                var contactsRequest = new RestRequest("clients/{id}/contacts", Method.Get);
                contactsRequest.AddParameter("id", id, ParameterType.UrlSegment);
                var contactsResponse = await client.ExecuteAsync<List<GetContactDTO>>(contactsRequest);


                var viewModel = new ClientDetailsViewModel
                {
                    Client = clients.Data,
                    Contacts = contactsResponse.Data ?? new List<GetContactDTO>()
                };


                return View(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception: {ex.Message}");
            }
        }

        public async Task<IActionResult> LinkContact(string id)
        {
            try
            {
                var contactRequest = new RestRequest("contacts", Method.Get);
                var contacts = await client.GetAsync<List<GetContactDTO>>(contactRequest);

                var clientRequest = new RestRequest("clients/{id}", Method.Get);
                clientRequest.AddParameter("id", id, ParameterType.UrlSegment);
                var clients = await client.GetAsync<GetClientDTO>(clientRequest);

                var viewModel = new LinkContactViewModel
                {
                    Client = clients,
                    Contacts = contacts
                };



                ViewData["clientName"] = clients?.Name;
                ViewData["clientCode"] = clients?.Code;
                ViewData["contacts"] = contacts;
                ViewData["clientId"] = clients?.Id;

                return View();
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception: {ex.Message}");
            }

        }
          
        [HttpPost]
        public async Task<IActionResult> LinkContact(LinkContactDTO linkContactDTO)
        {
            try
            {
                var request = new RestRequest("clients/link-contact", Method.Post);

                request.AddJsonBody(linkContactDTO);

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    return RedirectToAction("Details", new { id = linkContactDTO.ClientId });
                }
                else
                {
                    return RedirectToAction("LinkContact", new { id = linkContactDTO.ClientId });
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception: {ex.Message}");
            }
        }

    }
}
