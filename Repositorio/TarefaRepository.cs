using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Interface;
using Microsoft.EntityFrameworkCore;
using MVC.Models;
using MVC.Context;

namespace MVC.Repositorio
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly TarefaMvcContext _context;
        public TarefaRepository(TarefaMvcContext context) => _context = context;

        public IQueryable<TarefaMvc> ObterTarefasFiltradas(string? titulo, string? descricao, DateTime? data, EnumStatusTarefa? status)
        {
            var query = _context.Tarefas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(titulo))
                query = query.Where(t => t.Titulo.Contains(titulo));

            if (!string.IsNullOrWhiteSpace(descricao))
                query = query.Where(t => t.Descricao.Contains(descricao));

            if (data.HasValue)
                query = query.Where(t => t.Data.Date == data.Value.Date);

            if (status.HasValue)
                query = query.Where(t => t.Status == status);

            return query; // Retorna IQueryable, a query não foi executada ainda
        }

        public async Task<TarefaMvc?> ObterPorIdAsync(int id)
        {
            // FindAsync rastreia a entidade por padrão, ideal para edições
            return await _context.Tarefas.FindAsync(id);
        }

        // public void Adicionar(TarefaMvc tarefa)
        // {
        //     _context.Tarefas.Add(tarefa);
        //     _context.SaveChanges(); // Ou use Unit of Work
        // }

        public void Adicionar(TarefaMvc tarefa)
        {
            _context.Tarefas.Add(tarefa);
        }

        // 2. Atualizar: O EF Core já rastreia a entidade, basta garantir que ela foi modificada
        public void Atualizar(TarefaMvc tarefa)
        {
            _context.Tarefas.Update(tarefa);
        }

        // 3. Deletar
        public void Deletar(TarefaMvc tarefa)
        {
            _context.Tarefas.Remove(tarefa);
        }
    }
}