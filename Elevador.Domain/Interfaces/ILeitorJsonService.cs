using Elevador.Domain.Entities;

namespace Elevador.Domain.Interfaces
{
    public interface ILeitorJsonService
    {
        /// <summary> Lê um arquivo JSON e retorna uma lista de UsuarioPesquisa (contendo o elevador, andar e turno). </summary> /**
        public List<UsuarioPesquisa> lerJson();
    }
}
