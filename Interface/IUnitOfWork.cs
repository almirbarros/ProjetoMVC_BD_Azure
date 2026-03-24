using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Repositorio;

namespace MVC.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        // Acessa o repositório de Tarefas
        ITarefaRepository TarefaRepository { get; }

        // Essencial para o Unit of Work: salva tudo de uma vez
        Task<bool> CommitAsync();
    }
}