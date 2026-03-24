using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Context;
using MVC.Interface;

namespace MVC.Repositorio
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TarefaMvcContext _context;
        private ITarefaRepository _tarefaRepository;

        public UnitOfWork(TarefaMvcContext context)
        {
            _context = context;
        }

        // Lazy loading do repositório: cria apenas se necessário
        public ITarefaRepository TarefaRepository =>
            _tarefaRepository ??= new TarefaRepository(_context);

        public async Task<bool> CommitAsync()
        {
            // Retorna true se pelo menos uma entidade foi salva
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}