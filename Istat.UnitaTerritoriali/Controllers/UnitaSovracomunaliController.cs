using Istat.UnitaTerritoriali.Entities;
using Istat.UnitaTerritoriali.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Istat.UnitaTerritoriali.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UnitaSovracomunaliController : ControllerBase
    {
        private readonly ILogger<UnitaSovracomunaliController> _logger;
        private readonly IUnitaTerritorialiService _service;
        public UnitaSovracomunaliController(ILogger<UnitaSovracomunaliController> logger, IUnitaTerritorialiService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("Tipologie")]
        public async Task<List<TipologiaUnitaSovracomunale>> GetTipologieUnitaSovracomunali()
        {
            return await _service.GetTipologieUnitaSovracomunali();
        }

        [HttpGet]
        [Route("Tipologie/{id}")]
        public async Task<TipologiaUnitaSovracomunale> GetTipologieUnitaSovracomunali(string id)
        {
            var items = await _service.GetTipologieUnitaSovracomunali();
            return items.Where(i => i.Id.IsEquals(id)).First();
        }

        [HttpGet]
        public async Task<List<UnitaSovracomunale>> GetUnitaSovracomunali()
        {
            return await _service.GetUnitaSovracomunali();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<UnitaSovracomunale> GetUnitaSovracomunaliById(string id)
        {
            var items = await GetUnitaSovracomunali();
            return items.Where(i => i.Id.IsEquals(id)).First();
        }

        [HttpGet]
        [Route("ByRegioneId/{regioneId}")]
        public async Task<List<UnitaSovracomunale>> GetUnitaSovracomunaliByRegioneId(string regioneId)
        {
            var items = await GetUnitaSovracomunali();
            return items.Where(i => i.RegioneId.IsEquals(regioneId)).OrderBy(i => i.Id).ToList();
        }

        [HttpGet]
        [Route("ByTipologiaId/{tipologiaId}")]
        public async Task<List<UnitaSovracomunale>> GetUnitaSovracomunaliByTipologiaId(string tipologiaId)
        {
            var items = await GetUnitaSovracomunali();
            return items.Where(i => i.TipologiaId.IsEquals(tipologiaId)).OrderBy(i => i.Id).ToList();
        }

        [HttpGet]
        [Route("Like/{like}")]
        public async Task<List<UnitaSovracomunale>> GetUnitaSovracomunaliLike(string like)
        {
            var items = await GetUnitaSovracomunali();
            return items.Where(i => i.Nome.Contain(like) || i.NomeEstero.Contain(like)).ToList();
        }
    }
}

