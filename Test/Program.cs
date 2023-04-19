using Elevador.Domain.Enums;
using Elevador.Domain.Interfaces;
using Elevador.Domain.Extensions;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Test
{
    class Program
    {
        
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = Configuracao.ConfigurarServicos();

            IElevadorService ElevadorService = serviceProvider.GetService<IElevadorService>();

            Console.WriteLine("\n==================== RESULTADO DAS PESQUISAS ====================");

            List<int> andaresMenosUtilizados = ElevadorService.andarMenosUtilizado();
            Console.WriteLine("\nO(s) andar(es) menos utilizado(s) pelos usuários são (ordenados a partir do menos utilizado para o mais utilizado): ");
            andaresMenosUtilizados.ForEach(andar => Console.WriteLine("- " + andar));

            Console.WriteLine("\n------------------------------------");

            Console.WriteLine("O(s) elevador(es) mais frequentado(s) com seu respectivo período de maior fluxo: ");
            List<char> periodoMaiorFluxoElevadorMaisFrequentado = ElevadorService.periodoMaiorFluxoElevadorMaisFrequentado();

            Console.WriteLine("\n------------------------------------");

            Console.WriteLine("O(s) elevador(es) menos frequentado(s) com seu respectivo período de menor fluxo: ");
            List<char> periodoMenorFluxoElevadorMenosFrequentado = ElevadorService.periodoMenorFluxoElevadorMenosFrequentado();

            Console.WriteLine("\n------------------------------------");
            List<char> periodoMaiorUtilizacaoConjuntoElevadores = ElevadorService.periodoMaiorUtilizacaoConjuntoElevadores();
            Console.WriteLine("O período de maior utilização do conjunto de elevadores (do mais utilizado para o menos utilizado): ");
            periodoMaiorUtilizacaoConjuntoElevadores.ForEach(turno => Console.WriteLine("- " + turno.ToString().ToEnum<EnumTurno>()));

            Console.WriteLine("\n------------------------------------");
            Console.WriteLine("O percentual de uso de cada elevador com relação a todos os serviços prestados: ");
            float percentualDeUso = ElevadorService.percentualDeUsoElevadorA();
            Console.WriteLine(" - A: " + percentualDeUso + "%");

            percentualDeUso = ElevadorService.percentualDeUsoElevadorB();
            Console.WriteLine(" - B: " + percentualDeUso + "%");

            percentualDeUso = ElevadorService.percentualDeUsoElevadorC();
            Console.WriteLine(" - C: " + percentualDeUso + "%");

            percentualDeUso = ElevadorService.percentualDeUsoElevadorD();
            Console.WriteLine(" - D: " + percentualDeUso + "%");

            percentualDeUso = ElevadorService.percentualDeUsoElevadorE();
            Console.WriteLine(" - E: " + percentualDeUso + "%");

            Console.WriteLine("\n==================== FIM DO RESULTADO DAS PESQUISAS ====================");
        }
    }

    static class Configuracao
    {
        public static ServiceProvider ConfigurarServicos()
        {
            ServiceProvider serviceProvider = new ServiceCollection()
            .AddScoped<IElevadorService, ElevadorService>()
            .AddScoped<ILeitorJsonService, LeitorJsonService>()
            .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
