using Elevador.Domain.Entities;
using Elevador.Domain.Interfaces;
using System.Text.Json;
using System;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class LeitorJsonService : ILeitorJsonService
    {
        public List<UsuarioPesquisa> lerJson()
        {
            List<UsuarioPesquisa> usuarioPesquisa = new List<UsuarioPesquisa>();

            usuarioPesquisa = JsonConvert.DeserializeObject<List<UsuarioPesquisa>>(File.ReadAllText(@"../../../input.json"));

            return usuarioPesquisa;
        }

    }
}
