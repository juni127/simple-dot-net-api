using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using SimpleDotNETAPI.Models;
using SimpleDotNETAPI.Interfaces;
using SimpleDotNETAPI.DTO;

namespace SimpleDotNETAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        
        public ClientController(IClientRepository clientRepository, IMapper mapper)
        {
            this._clientRepository = clientRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Client>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetClients()
        {
            var clients = this._mapper.Map<List<ClientDTO>>(this._clientRepository.GetClients());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(clients.Count == 0)
                return NoContent();

            return Ok(clients);
        }

        [HttpGet("{uid}")]
        [ProducesResponseType(200, Type = typeof(Client))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetClient(string uid)
        {
            if(!this._clientRepository.ClientExists(uid))
                return NotFound();

            var client = this._mapper.Map<ClientDTO>(this._clientRepository.GetClient(uid));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(client);
        }

        [HttpGet("{clientUid}/month-queries-count")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetClientMonthQueriesCount(string clientUid)
        {
            if(!this._clientRepository.ClientExists(clientUid))
                return NotFound();

            var count = this._clientRepository.GetClientMonthQueriesCount(clientUid);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(count);
        }

        [HttpGet("{clientUid}/plan-history")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Plan>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetClientPlanHistory(string clientUid)
        {
            if(!this._clientRepository.ClientExists(clientUid))
                return NotFound();

            var plans = this._clientRepository.GetClientPlanHistory(clientUid);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(plans.Count == 0)
                return NoContent();            
            
            return Ok(plans);
        }

        [HttpGet("{clientUid}/plan")]
        [ProducesResponseType(200, Type = typeof(Plan))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetClientPlan(string clientUid)
        {
            if(!this._clientRepository.ClientExists(clientUid))
                return NotFound();

            var count = this._mapper.Map<Plan>(this._clientRepository.GetClientPlan(clientUid));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(count);
        }

        [HttpGet("{clientUid}/queries")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Query>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetClientQueries(string clientUid)
        {
            if(!this._clientRepository.ClientExists(clientUid))
                return NotFound();

            var queries = this._clientRepository.GetClientQueries(clientUid);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(queries.Count == 0)
                return NoContent();
            
            return Ok(queries);
        }

        [HttpGet("{clientUid}/queries/from={start:datetime}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Query>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetClientQueriesFrom(string clientUid, DateTime start)
        {
            if(!this._clientRepository.ClientExists(clientUid))
                return NotFound();

            var queries = this._clientRepository.GetClientQueries(clientUid, start);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(queries.Count == 0)
                return NoContent();
            
            return Ok(queries);
        }

        [HttpGet("{clientUid}/queries/upto={end:datetime}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Query>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetClientQueriesUpTo(string clientUid, DateTime end)
        {
            if(!this._clientRepository.ClientExists(clientUid))
                return NotFound();

            var queries = this._clientRepository.GetClientQueries(clientUid, new DateTime(), end);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(queries.Count == 0)
                return NoContent();
            
            return Ok(queries);
        }

        [HttpGet("{clientUid}/queries/from={start:datetime}/upto={end:datetime}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Query>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetClientQueriesFromUpTo(string clientUid, DateTime start, DateTime end)
        {
            if(!this._clientRepository.ClientExists(clientUid))
                return NotFound();

            var queries = this._clientRepository.GetClientQueries(clientUid, start, end);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(queries.Count == 0)
                return NoContent();
            
            return Ok(queries);
        }

        [HttpGet("{clientUid}/queries/count")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetClientQueriesCount(string clientUid)
        {
            if(!this._clientRepository.ClientExists(clientUid))
                return NotFound();

            var count = this._clientRepository.GetClientQueriesCount(clientUid);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(count);
        }

        [HttpGet("{clientUid}/queries/count/from={start:datetime}")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetClientQueriesCountFrom(string clientUid, DateTime start)
        {
            if(!this._clientRepository.ClientExists(clientUid))
                return NotFound();

            var count = this._clientRepository.GetClientQueriesCount(clientUid, start);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(count);
        }

        [HttpGet("{clientUid}/queries/count/upto={end:datetime}")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetClientQueriesCountUpTo(string clientUid, DateTime end)
        {
            if(!this._clientRepository.ClientExists(clientUid))
                return NotFound();

            var count = this._clientRepository.GetClientQueriesCount(clientUid, new DateTime(), end);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(count);
        }

        [HttpGet("{clientUid}/queries/count/from={start:datetime}/upto={end:datetime}")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetClientQueriesCountFromUpTo(string clientUid, DateTime start, DateTime end)
        {
            if(!this._clientRepository.ClientExists(clientUid))
                return NotFound();

            var count = this._clientRepository.GetClientQueriesCount(clientUid, start, end);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(count);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(405)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateClient([FromBody] ClientDTO client)
        {
            if(client == null)
                return BadRequest(ModelState);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var clientMap = this._mapper.Map<Client>(client);

            // TODO: Generate private key here
            clientMap.PrivateKey = "";

            clientMap.MonthQueriesCount = 0;
            clientMap.Created = DateTime.Now;
            clientMap.Updated = DateTime.Now;

            if(!this._clientRepository.CreateClient(clientMap))
            {
                ModelState.AddModelError("", "Something went wrong while creating the Client.");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    }
}