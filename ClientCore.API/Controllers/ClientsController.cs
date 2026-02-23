namespace ClientCore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController(IClientService clientService, IContactService contactService) : ControllerBase
    {
        private readonly IClientService _clientService = clientService;
        private readonly IContactService _contactService = contactService;

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateClientDTO createClientDTO)
        {
            try 
            {
                return Ok(await _clientService.AddAsync(createClientDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _clientService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            try
            {
                return Ok(await _clientService.GetAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{id}/contacts")]
        public async Task<IActionResult> GetContactsAsync(string id)
        {
            try
            {
                return Ok(await _contactService.GetContactsAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("link-contact")]
        public async Task<IActionResult> LinkContact([FromBody] LinkContactDTO linkContactDTO)
        {
            try
            {
                await _clientService.LinkContactAsync(linkContactDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
