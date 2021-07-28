using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Services;
using ApiCatalogoJogos.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]

    public class JogosController : ControllerBase
    {
        private readonly IJogoService jogoService;

        public JogosController(IJogoService jogoService)
        {
            this.jogoService = jogoService;
        }
        ///<sumary>
        /// Buscar todos os jogos de forma paginada
        ///</sumary>
        /// <param name="pagina">Indica qual página está sendo consultada.</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página.</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade =  5)
        {
            var jogos = await jogoService.Obter(pagina, quantidade);

            if (jogos.Count() == 0)
                return NoContent();
            return Ok(jogos);
        }
        
        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid idJogo)
        {
            var jogo = await jogoService.Obter(idJogo);

            if (jogo == null)
                return NoContent();
            return Ok(jogo);
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await jogoService.Inserir(jogoInputModel);
                return Ok();
            }
            catch(JogoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }

        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                await jogoService.Atualizar(idJogo, jogoInputModel);
                return Ok();
            }
            catch(JogoNaoCadastradoException ex)
            {
                return NotFound("Não existe este jogo");

            }
        }

            [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await jogoService.Atualizar(idJogo, preco);
                return Ok();
            }
            catch(JogoNaoCadastradoException ex)
            {
                return NotFound("Não existe este jogo");

            }
        }

        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> ApagarJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await jogoService.Remover(idJogo);
                return Ok();
            }
            catch(JogoNaoCadastradoException ex)
            {
                return NotFound("Não Cadastrado");
            }
             
        }
    }
}
