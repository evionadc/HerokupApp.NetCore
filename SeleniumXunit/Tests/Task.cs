using System;
using SeleniumXunit.Core;
using SeleniumXunit.Pages;
using Xunit;

namespace SeleniumXunit.Tests
{
    public class Task : CoreTest
    {
        readonly TaskPage pg = new TaskPage();

        [Fact(DisplayName = "Criar uma tarefa")]
        public void Teste01Tasks()
        {

            String nomeTarefa = "Tarefa automatizada";
            String tags = "testes,selenium,";
            CriarTarefa(nomeTarefa, tags);

            Assert.Equal(nomeTarefa, pg.ObterTarefacriada(nomeTarefa));

        }

        [Fact]
        public void Teste02EditarTask()
        {

            String nomeTarefa = "Tarefa para edição";
            String tags = "testes,selenium,";

            CriarTarefa(nomeTarefa, tags);

            pg.clicarEmEditarTarefa(nomeTarefa);
            pg.editarTarefa(nomeTarefa + "editada");
            pg.ClicarEmSalvar();


            Assert.Equal("Finalizado", pg.ObterStatusTarefa(nomeTarefa + "editada"));
        }

        [Fact]
        public void Teste03ExcluirTask()
        {
            String nomeTarefa = "Tarefa para exclusão";

            CriarTarefa(nomeTarefa, "testes,selenium,");

            pg.ClicarEmExcluirTarefa(nomeTarefa);
            pg.ConfirmarExclusao();

            Assert.False(pg.ObterTarefa(nomeTarefa));
        }

        private void CriarTarefa(String nomeTarefa, String tags)
        {
            pg.ClicarEmNovaTarefa();
            pg.PreencherTarefa(nomeTarefa, tags);
            pg.ClicarEmSalvar();
        }
    }
}

