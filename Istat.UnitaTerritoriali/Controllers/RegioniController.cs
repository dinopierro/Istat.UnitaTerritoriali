using System.Dynamic;
using Istat.UnitaTerritoriali.Entities;
using Istat.UnitaTerritoriali.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Istat.UnitaTerritoriali.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegioniController : ControllerBase
    {
        private readonly ILogger<RegioniController> _logger;
        private readonly IUnitaTerritorialiService _service;
        public RegioniController(ILogger<RegioniController> logger, IUnitaTerritorialiService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<List<Regione>> GetRegioni()
        {
            return await _service.GetRegioni();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Regione> GetRegioni(string id)
        {
            var regioni = await _service.GetRegioni();
            return regioni.First(r => r.Id.IsEquals(id));
        }

        [HttpGet]
        [Route("Like/{like}")]
        public async Task<List<Regione>> GetRegioniLike(string like)
        {
            var items = await _service.GetRegioni(); ;
            return items.Where(i => i.Nome.Contain(like) || i.NomeEstero.Contain(like)).ToList();
        }
    }
}


