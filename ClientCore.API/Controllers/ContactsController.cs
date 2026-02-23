namespace ClientCore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController(IContactService contactService) : ControllerBase
    {
        private readonly IContactService _contactService = contactService;
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateContactDTO createContactDTO)
        {
            try
            {
                return Ok(await _contactService.AddAsync(createContactDTO));
            }
            catch
            {
                return BadRequest("An error occurred while adding the contact.");
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _contactService.GetAllAsync());
            }
            catch
            {
                return BadRequest("An error occurred while viewing the contact.");
            }

        }
    }
}
