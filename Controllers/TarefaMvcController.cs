using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Context;
using MVC.Interface;
using MVC.Models;

namespace MVC.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    //public class TarefasMvcController : ControllerBase
    public class TarefaMvcController : Controller
    {
        private readonly TarefaMvcContext _context;
        public TarefaMvcController(TarefaMvcContext context)
        {
            _context = context;
        }

        //     private readonly ITarefaRepository _repo;
        //     private readonly IUnitOfWork _uow; // Padrão recomendado para SaveChangesAsync

        //     public TarefasMvcController(ITarefaRepository repo, IUnitOfWork uow)
        //     {
        //         _repo = repo;
        //         _uow = uow;
        //     }

        //     // private readonly ITarefaRepository _repository;
        //     // public TarefasController(ITarefaRepository repository)
        //     // {
        //     //     _repository = repository;
        //     // }

        //     [HttpGet]
        //     public async Task<IActionResult> ObterTarefas(
        // [FromQuery] string? titulo,
        // [FromQuery] string? descricao,
        // [FromQuery] DateTime? data,
        // [FromQuery] EnumStatusTarefa? status)
        //     {
        //         // Usa IQueryable no repositório e materializa aqui com ToListAsync
        //         // Clean Code: O controller apenas inicia o fluxo, a query é construída no repo.
        //         var tarefas = await _uow.TarefaRepository
        //             .ObterTarefasFiltradas(titulo, descricao, data, status)
        //             .ToListAsync();

        //         return Ok(tarefas);
        //     }


        //     [HttpGet("{id}")]
        //     public async Task<IActionResult> ObterPorId(int id)
        //     {
        //         var tarefa = await _uow.TarefaRepository.ObterPorIdAsync(id);

        //         if (tarefa == null) return NotFound();

        //         return Ok(tarefa);
        //     }

        //     [HttpPost]
        //     public async Task<IActionResult> CriarTarefa([FromBody] TarefaMvc tarefa)
        //     {
        //         if (!ModelState.IsValid) return BadRequest(ModelState);

        //         _uow.TarefaRepository.Adicionar(tarefa);

        //         // Unit of Work garante salvar tudo em uma transação atomic
        //         if (await _uow.CommitAsync())
        //         {
        //             return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        //         }

        //         return BadRequest("Erro ao salvar a tarefa.");
        //     }

        //     [HttpPut("{id}")]
        //     public async Task<IActionResult> AtualizarTarefa(int id, [FromBody] TarefaMvc tarefa)
        //     {
        //         if (id != tarefa.Id) return BadRequest();

        //         _uow.TarefaRepository.Atualizar(tarefa);

        //         if (await _uow.CommitAsync())
        //         {
        //             return NoContent();
        //         }

        //         return BadRequest("Erro ao atualizar a tarefa.");
        //     }

        //     [HttpDelete("{id}")]
        //     public async Task<IActionResult> DeletarTarefa(int id)
        //     {
        //         var tarefa = await _uow.TarefaRepository.ObterPorIdAsync(id);
        //         if (tarefa == null) return NotFound();

        //         _uow.TarefaRepository.Deletar(tarefa);

        //         if (await _uow.CommitAsync())
        //         {
        //             return NoContent();
        //         }

        //         return BadRequest("Erro ao deletar a tarefa.");
        //     }

        //     [HttpGet]
        //     public async Task<ActionResult<IEnumerable<TarefaMvc>>> Get(
        //         [FromQuery] string? titulo,
        //         [FromQuery] string? descricao,
        //         [FromQuery] DateTime? data,
        //         [FromQuery] EnumStatusTarefa? status)
        //     {
        //         // 1. Obtém o IQueryable (nenhum dado foi buscado ainda)
        //         var query = _uow.TarefaRepository.ObterTarefasFiltradas(titulo, descricao, data, status);

        //         // 2. Executa a query no banco de dados assincronamente (ToLisAsync)
        //         var tarefas = await query.ToListAsync();

        //         return Ok(tarefas);
        //     }

        //     [HttpGet]
        //     public async Task<IActionResult> Index([FromQuery] string? titulo, [FromQuery] string? descricao, [FromQuery] DateTime? data, [FromQuery] EnumStatusTarefa? status)
        //     {
        //         var tarefas = await _uow.TarefaRepository
        //             .ObterTarefasFiltradas(titulo, descricao, data, status)
        //             .ToListAsync();

        //         var viewModel = new TarefasFiltroViewModel
        //         {
        //             Tarefas = tarefas,
        //             Titulo = titulo,
        //             Descricao = descricao,
        //             Data = data,
        //             Status = status
        //         };

        //         return View(viewModel); // Retorna a view Index.cshtml
        //     }

        // [HttpGet]
        // public async Task<IActionResult> Index(TarefasFiltroViewModel filtro)
        // {
        //     var query = _repo.ObterTarefasFiltradas(filtro.Titulo, filtro.Descricao, filtro.Data, filtro.Status);

        //     // Executa a query no banco de dados apenas aqui
        //     filtro.Tarefas = await query.AsNoTracking().ToListAsync();

        //     return View(filtro);
        // }

        // public IActionResult Index()
        // {
        //     var tarefasBanco = _context.Tarefas.ToList();
        //     return View(tarefasBanco);
        // }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(TarefaMvc tarefa)
        {
            if (ModelState.IsValid)
            {
                _context.Tarefas.Add(tarefa);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }

        // [HttpPost]
        // [ValidateAntiForgeryToken] // Boa prática de segurança
        // public IActionResult Criar(TarefaMvc tarefa)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         // 2. CORREÇÃO: Utilizar o repositório em vez do _context diretamente
        //         _repo.Adicionar(tarefa);

        //         return RedirectToAction(nameof(Index));
        //     }

        //     return View(tarefa);
        // }


        public IActionResult Editar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);
            if (tarefaBanco == null)
                return RedirectToAction(nameof(Index));

            return View(tarefaBanco);
        }

        [HttpPost]
        public IActionResult Editar(TarefaMvc tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(tarefa.Id);
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalhar(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
                return RedirectToAction(nameof(Index));

            return View(tarefa);
        }

        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return RedirectToAction(nameof(Index));

            return View(tarefaBanco);
        }

        [HttpPost]
        public IActionResult Deletar(TarefaMvc tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(tarefa.Id);
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        //Index com Filtro
        public IActionResult Index(string? titulo, string? descricao, DateTime? data, EnumStatusTarefa? status)
        {
            // 1. Inicia a query base (pega todos, sem executar no banco ainda)
            var tarefaBanco = _context.Tarefas.AsQueryable();

            // 2. Aplica filtros condicionalmente
            if (!string.IsNullOrEmpty(titulo))
                tarefaBanco = tarefaBanco.Where(p => p.Titulo.Contains(titulo));

            if (!string.IsNullOrEmpty(descricao))
                tarefaBanco = tarefaBanco.Where(p => p.Descricao.Contains(descricao));

            if (data.HasValue)
                tarefaBanco = tarefaBanco.Where(p => p.Data.Date == data.Value.Date);

            if (status.HasValue)
                tarefaBanco = tarefaBanco.Where(t => t.Status == status.Value);


            // 3. Mantém os valores preenchidos na View (ViewBag)
            ViewBag.Titulo = titulo;
            ViewBag.Descricao = descricao;
            ViewBag.Data = data?.ToString("yyyy-MM-dd"); // Formato correto para input date
            //ViewBag.status = status;

            return View(tarefaBanco.ToList());
        }


        // [HttpGet]
        // public async Task<IActionResult> Index(TarefasFiltroViewModel model)
        // {
        //     // Aplica o filtro usando IQueryable para otimizar a query (só executa no ToListAsync)
        //     var tarefasQuery = _repository.GetAll();
        //     //var tarefasQuery = _context.GetAll();

        //     if (!string.IsNullOrEmpty(model.Titulo))
        //         tarefasQuery = tarefasQuery.Where(t => t.Titulo.Contains(model.Titulo));

        //     if (!string.IsNullOrEmpty(model.Descricao))
        //         tarefasQuery = tarefasQuery.Where(t => t.Descricao.Contains(model.Descricao));

        //     if (model.Data.HasValue)
        //         tarefasQuery = tarefasQuery.Where(t => t.Data.Date == model.Data.Value.Date);

        //     if (model.Status.HasValue)
        //         tarefasQuery = tarefasQuery.Where(t => t.Status == model.Status);

        //     model.Tarefas = await tarefasQuery.ToListAsync(); // Otimização: Apenas uma chamada ao BD

        //     return View(model);
        // }


    }
}