using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();

        static void Main(string[] args)
        {            
            string opcaoUsuario = ObterOpcao();

            while(opcaoUsuario.ToUpper() != "X")
            {
                switch(opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
                opcaoUsuario = ObterOpcao();
            }
        }

        private static void ListarSeries()
        {
            System.Console.WriteLine("Listar séries: ");

            var lista = repositorio.Lista();

            if(lista.Count == 0)
            {
                System.Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }
            foreach(var serie in lista )
            {
                var excluido = serie.retornaExcluido();
                System.Console.WriteLine("#ID {0} - {1} - {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "---Excluida---" : "1"));
            }
        }

        public static void InserirSerie()
        {
            System.Console.WriteLine("Inserir nova série: ");
            foreach(int i in Enum.GetValues(typeof(Genero)))
            {
               System.Console.WriteLine("#ID {0}: - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            System.Console.WriteLine("Digite o genêro: ");
            int entradaGenero = int.Parse(System.Console.ReadLine());

            System.Console.WriteLine("Digite o título da série: ");
            string entradaTitulo = System.Console.ReadLine();

            System.Console.WriteLine("Digite o ano da série: ");
            int entradaAno = int.Parse(System.Console.ReadLine());

            System.Console.WriteLine("Digite a descrição da série: ");
            string entradaDescricao = System.Console.ReadLine();

            System.Console.WriteLine("Série cadastrada com sucesso!");
            
            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                            genero: (Genero)entradaGenero,
                                            titulo: entradaTitulo,
                                            ano: entradaAno,
                                            descricao: entradaDescricao);            
            repositorio.Insere(novaSerie);            
        }
        public static void AtualizarSerie()
        {
            System.Console.WriteLine("Insira o ID da série: ");
            int idSerie = int.Parse(System.Console.ReadLine());

            foreach(int i in Enum.GetValues(typeof(Genero)))
            {
               System.Console.WriteLine("#ID {0}: - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            System.Console.WriteLine("Digite o genêro: ");
            int entradaGenero = int.Parse(System.Console.ReadLine());

            System.Console.WriteLine("Digite o título da série: ");
            string entradaTitulo = System.Console.ReadLine();

            System.Console.WriteLine("Digite o ano da série: ");
            int entradaAno = int.Parse(System.Console.ReadLine());

            System.Console.WriteLine("Digite a descrição da série: ");
            string entradaDescricao = System.Console.ReadLine();

            System.Console.WriteLine("Série cadastrada com sucesso!");
            
            Serie atualizaSerie = new Serie(id: idSerie,
                                            genero: (Genero)entradaGenero,
                                            titulo: entradaTitulo,
                                            ano: entradaAno,
                                            descricao: entradaDescricao);            
            repositorio.Atualiza(idSerie, atualizaSerie);
        }
        
        public static void ExcluirSerie()
        {
            System.Console.WriteLine("Digite o ID da série: ");
            int idSerie = int.Parse(Console.ReadLine());

            repositorio.Exclui(idSerie);
        }
        public static void VisualizarSerie()
        {
            System.Console.WriteLine("Digite o ID da série: ");
            int idSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(idSerie);
            System.Console.WriteLine(serie);
        }

        private static string ObterOpcao()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Bem-Vindo ao DIO Series!");
            System.Console.WriteLine("Informe a opção desejada: ");
            System.Console.WriteLine("1 - Listar séries");
            System.Console.WriteLine("2 - Inserir nova série");
            System.Console.WriteLine("3 - Atualizar série");
            System.Console.WriteLine("4 - Excluir série");
            System.Console.WriteLine("5 - Exibir série");
            System.Console.WriteLine("C - Limpar tela");
            System.Console.WriteLine("X - Sair");
            System.Console.WriteLine("_______________________");

            string opcaoUsuario = System.Console.ReadLine().ToUpper();
            System.Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
