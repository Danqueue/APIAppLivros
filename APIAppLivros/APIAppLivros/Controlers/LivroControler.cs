using LivrosAPI.Models;
using LivrosAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LivrosAPI.Controllers
{
    [Route("api/livros")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly LivroRepository _livroRepository;

        public LivroController(LivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        // GET: api/livros/listar
        [HttpGet]
        [Route("listar")]
        [SwaggerOperation(Summary = "Listar todos os livros", Description = "Este endpoint retorna uma listagem de todos os livros cadastrados.")]
        public async Task<IEnumerable<Livro>> Listar()
        {
            return await _livroRepository.ListarTodosLivros();
        }

        // GET api/livros/detalhes/{id}
        [HttpGet("detalhes/{id}")]
        [SwaggerOperation(
            Summary = "Obtém dados de um livro pelo ID",
            Description = "Este endpoint retorna todos os dados de um livro cadastrado filtrando pelo ID.")]
        public async Task<Livro> BuscarPorId(int id)
        {
            return await _livroRepository.BuscarPorId(id);
        }

        // POST api/livros
        [HttpPost]
        [SwaggerOperation(
            Summary = "Cadastrar um novo Livro",
            Description = "Este endpoint é responsável por cadastrar um novo livro no banco.")]
        public async Task<IActionResult> Post([FromBody] Livro dados)
        {
            var livroExistente = await _livroRepository.BuscarPorTitulo(dados.Titulo);
            if (livroExistente != null)
                return BadRequest("Um livro com esse título já existe.");

            await _livroRepository.Salvar(dados);
            return Ok();
        }

        // PUT api/livros/{id}
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualizar os dados de um livro filtrando pelo ID",
            Description = "Este endpoint é responsável por atualizar os dados de um livro no banco.")]
        public async Task<IActionResult> Put(int id, [FromBody] Livro dados)
        {
            dados.Id = id;
            await _livroRepository.Atualizar(dados);
            return Ok();
        }

        // DELETE api/livros/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remover um livro filtrando pelo ID",
            Description = "Este endpoint é responsável por remover os dados de um livro no banco.")]
        public async Task<IActionResult> Delete(int id)
        {
            await _livroRepository.DeletarPorId(id);
            return Ok();
        }
    }
}