using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC.Models;

namespace MVC.Interface
{
    public interface ITarefaRepository
    {
        // Usa IQueryable para adiar a execução até o ToListAsync (melhor performance)
        IQueryable<TarefaMvc> ObterTarefasFiltradas(string? titulo, string? descricao, DateTime? data, EnumStatusTarefa? status);
        // Obter específico para operações de Edição/Exclusão
        Task<TarefaMvc?> ObterPorIdAsync(int id);

        void Adicionar(TarefaMvc tarefa);
        void Atualizar(TarefaMvc tarefa);
        void Deletar(TarefaMvc tarefa);
    }
}