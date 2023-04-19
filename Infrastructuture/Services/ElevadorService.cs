using Elevador.Domain.Entities;
using Elevador.Domain.Enums;
using Elevador.Domain.Extensions;
using Elevador.Domain.Interfaces;

namespace Infrastructure.Services
{
    public class ElevadorService : IElevadorService
    {
        public List<UsuarioPesquisa> UsuarioPesquisa { get; set; }

        public ElevadorService(ILeitorJsonService leitorJsonService)
        {
            UsuarioPesquisa = leitorJsonService.lerJson();
        }

        public List<int> andarMenosUtilizado()
        {
            return UsuarioPesquisa.GroupBy(pesquisa => pesquisa.Andar)
                   .OrderBy(grupo => grupo.Count()).ThenBy(grupo => grupo.Key)
                   .Select(grupo => grupo.Key).ToList();
        }

        public List<char> elevadorMaisFrequentado()
        {
            var elevadores = UsuarioPesquisa.GroupBy(pesquisa => pesquisa.Elevador);
            int maxQuantidade = elevadores.Max(grupo => grupo.Count());

            List<char> elevadoresComMaiorFrequencia = new();

            var listaElevadoresMaisFrequentados = elevadores.Where(grupo => grupo.Count() == maxQuantidade)
                                      .Select(grupo => grupo.Key);

            foreach (var elevador in listaElevadoresMaisFrequentados)
            {
                elevadoresComMaiorFrequencia.Add(elevador);
            }

            return elevadoresComMaiorFrequencia;
        }

        public List<char> elevadorMenosFrequentado()
        {
            var elevadores = UsuarioPesquisa.GroupBy(pesquisa => pesquisa.Elevador);
            int minQuantidade = elevadores.Min(grupo => grupo.Count());

            List<char> elevadoresComMenorFrequencia = new();

            var listaElevadoresMenosFrequentados = elevadores.Where(grupo => grupo.Count() == minQuantidade)
                                      .Select(grupo => grupo.Key);

            foreach (var elevador in listaElevadoresMenosFrequentados)
            {
                elevadoresComMenorFrequencia.Add(elevador);
            }

            return elevadoresComMenorFrequencia;
        }

        public float percentualDeUsoElevadorA()
        {
            return calcularPercentual('A');
        }

        public float percentualDeUsoElevadorB()
        {
            return calcularPercentual('B');
        }

        public float percentualDeUsoElevadorC()
        {
            return calcularPercentual('C');
        }

        public float percentualDeUsoElevadorD()
        {
            return calcularPercentual('D');
        }

        public float percentualDeUsoElevadorE()
        {
            return calcularPercentual('E');
        }

        public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
        {

            List<char> maisFrequentados = elevadorMaisFrequentado();


            var pesquisaAgrupadaPorElevadorTurno = UsuarioPesquisa.Where(objeto => maisFrequentados.Contains(objeto.Elevador))
                        .GroupBy(objeto => new { objeto.Elevador, objeto.Turno })
                        .Select(grupo => new { Elevador = grupo.First().Elevador, Turno = grupo.Key, Quantidade = grupo.Count() });

            List<char> turnos = new();

            foreach (var elevador in maisFrequentados)
            {
                var query = from objeto in pesquisaAgrupadaPorElevadorTurno
                            where objeto.Elevador == elevador
                            orderby objeto.Quantidade descending
                            select objeto.Turno;

                char turnoMaiorFluxo = query.FirstOrDefault().Turno;
                turnos.Add(turnoMaiorFluxo);

                Console.WriteLine("- Elevador " + elevador + " com o período de maior fluxo sendo " + turnoMaiorFluxo.ToString().ToEnum<EnumTurno>());
            }

            return turnos;

        }

        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {
            return UsuarioPesquisa.GroupBy(pesquisa => pesquisa.Turno)
                .OrderByDescending(grupo => grupo.Count())
                .Select(grupo => grupo.Key)
                .ToList();
        }

        public List<char> periodoMenorFluxoElevadorMenosFrequentado()
        {
            List<char> menosFrequentados = elevadorMenosFrequentado();


            var pesquisaAgrupadaPorElevadorTurno = UsuarioPesquisa.Where(objeto => menosFrequentados.Contains(objeto.Elevador))
                        .GroupBy(objeto => new { objeto.Elevador, objeto.Turno })
                        .Select(grupo => new { Elevador = grupo.First().Elevador, Turno = grupo.Key, Quantidade = grupo.Count() });

            List<char> turnos = new();

            foreach (var elevador in menosFrequentados)
            {
                var query = from objeto in pesquisaAgrupadaPorElevadorTurno
                            where objeto.Elevador == elevador
                            orderby objeto.Quantidade
                            select objeto.Turno;

                char turnoMaiorFluxo = query.FirstOrDefault().Turno;
                turnos.Add(turnoMaiorFluxo);

                Console.WriteLine("- Elevador " + elevador + " com o período de menor fluxo sendo " + turnoMaiorFluxo.ToString().ToEnum<EnumTurno>());
            }

            return turnos;
        }

        private float calcularPercentual(char nomeDoElevador)
        {
            List<int> elevadores = new List<int>();
            UsuarioPesquisa.ForEach(pesquisa => elevadores.Add(pesquisa.Elevador));

            float quantidadeElevadores = elevadores.Count(elevador => elevador == nomeDoElevador);

            return (float)Math.Round((quantidadeElevadores) / elevadores.Count * 100, 2);
        }

    }
}
