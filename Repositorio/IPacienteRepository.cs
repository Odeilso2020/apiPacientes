using System.Collections.Generic;
using apiPacientes.Models;

namespace apiPacientes.Repositorio
{
    public interface IPacienteRepository
    {
        //Rotinas que nossa aplicação vai fazer
        // Adicionar
        void Add(Paciente paciente);

        // Recuperar
        IEnumerable<Paciente> GetAll(); // Uma rotina, vai pegar o Paciente e transformar em um array

        // Busca
        Paciente Find(long id);

        // Remove
        void Remove(long id);

        // Update
        void Update(Paciente paciente);
    }
}