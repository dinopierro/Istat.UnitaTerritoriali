using System.Net;
using Istat.UnitaTerritoriali.Entities;
using Istat.UnitaTerritoriali.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Istat.UnitaTerritoriali.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComuniController : ControllerBase
    {
        private readonly ILogger<ComuniController> _logger;
        private readonly IUnitaTerritorialiService _service;
        public ComuniController(ILogger<ComuniController> logger, IUnitaTerritorialiService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<List<Comune>> GetComuni()
        {
            return await _service.GetComuni();
        }

        [HttpGet]
        [Route("RipartizioniGeografiche")]
        public async Task<List<RipartizioneGeografica>> GetRipartizioni()
        {
            return await _service.GetRipartizioniGeografiche();
        }

        [HttpGet]
        [Route("Tipologie")]
        public async Task<List<TipologiaComune>> GetTipologie()
        {
            return await _service.GetTipologieComuni();
        }

        [HttpGet]
        [Route("ById/{id}")]
        public async Task<Comune> GetComuneById(string id)
        {
            var items = await GetComuni();
            return items.Where(i => i.Id.IsEquals(id)).First();
        }

        [HttpGet]
        [Route("ByUnitaSovracomunaleId/{id}")]
        public async Task<List<Comune>> GetComuneByUnitaSovracomunaleId(string id)
        {
            var items = await GetComuni();
            return items.Where(i => i.UnitaSovracomunaleId.IsEquals(id)).ToList();
        }

        [HttpGet]
        [Route("ByRegioneId/{id}")]
        public async Task<List<Comune>> GetComuneByRegioneId(string id)
        {
            var items = await GetComuni();
            return items.Where(i => i.RegioneId.IsEquals(id)).ToList();
        }

        [HttpGet]
        [Route("ByRipartizioneGeograficaId/{id}")]
        public async Task<List<Comune>> GetComuneByRipartizioneGeograficaId(string id)
        {
            var items = await GetComuni();
            return items.Where(i => i.RipartizioneGeograficaId.IsEquals(id)).ToList();
        }

        [HttpGet]
        [Route("ByTipologiaId/{id}")]
        public async Task<List<Comune>> GetComuneByTipologiaId(string id)
        {
            var items = await GetComuni();
            return items.Where(i => i.TipologiaComuneId.IsEquals(id)).ToList();
        }

        [HttpGet]
        [Route("Like/{like}")]
        public async Task<List<Comune>> GetComuneLike(string like)
        {
            var items = await GetComuni();
            return items.Where(i => i.Nome.Contain(like) || i.NomeEstero.Contain(like)).ToList();
        }
    }
}