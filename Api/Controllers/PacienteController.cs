using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Core.Services;
using Core.ViewModels;
using Core.Models;
using Core.Exceptions;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using Api.Security;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/paciente")]
    public class PacienteController : ControllerBase
    {

        private readonly PacienteService service;

        public PacienteController(PacienteService service)
        {
            this.service = service;
        }

        [ClaimsAuthorize("paciente", "view")]
        [Route("{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetPacienteAsync(int id)
        {
            try
            {
                var paciente = await service.GetPacienteAsync(id);
                return Ok(paciente);
            }
            catch (PacienteException ex)
            {
                return BadRequest(ex);
            }            
        }

        [ClaimsAuthorize("paciente", "view")]
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetPacientesAsync(int page, int pagesize, string orderby, string searchtext)
        {
            try
            {
                Pager pager = new Pager(page, pagesize, orderby, searchtext);
                var pacientes = await service.GetPacientesAsync(pager);
                return Ok(pacientes);
            }
            catch (PacienteException ex)
            {
                return BadRequest(ex);
            }
            
        }

        [ClaimsAuthorize("paciente", "add")]
        [Route("")]
        [HttpPost]
        public async Task<IActionResult> NewPacienteAsync([FromBody] Paciente paciente)
        {
            try
            {
                var retorno = await service.NewPacienteAsync(paciente);
                return Ok(retorno);
            }
            catch (PacienteException ex)
            {
                return BadRequest(ex);
            }
        }

        [ClaimsAuthorize("paciente", "edit")]
        [Route("{id:int}")]
        [HttpPut]
        public async Task<IActionResult> UpdatePacienteAsync([FromBody] Paciente paciente)
        {
            try
            {
                var retorno = await service.UpdatePacienteAsync(paciente);
                return Ok(retorno);
            }
            catch (PacienteException ex)
            {
                return BadRequest(ex);                
            }
            
        }

        [ClaimsAuthorize("paciente", "del")]
        [Route("{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> DeletePacienteAsync(int id)
        {
            try
            {
                ResultViewModel retorno = await service.DeletePacienteAsync(id);
                return Ok(retorno);
            }
            catch(PacienteException ex)
            {
                return BadRequest(ex);
            }                
            
        }

    }
}