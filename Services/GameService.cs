using DadosComCSV.Models;
using DadosComCSV.Repositories;

namespace DadosComCSV.Services
{
    public class GameService
    {
        private readonly GameRepository? _repository;

        public GameService(GameRepository repository)
        {
            _repository = repository;
        }

        public List<Game> Listar()
        {
            return _repository!.Listar();
        }

        public bool GameRegister(Game gameInfo)
        {
            if
            (string.IsNullOrWhiteSpace(gameInfo.Nome) || string.IsNullOrWhiteSpace(gameInfo.Desenvolvedora) ||
            string.IsNullOrWhiteSpace(gameInfo.Genero) || gameInfo.Preco < 0 || gameInfo.AnoLancamento < 1950 ||
            gameInfo.AnoLancamento > DateTime.Now.Year
            )
            {
                return false;
            }
            gameInfo.Id = NewId();
            return _repository!.GameRegister(gameInfo);
        }

        public bool FindId(int idNumber){return _repository!.Listar().Any(idFinded => idFinded.Id == idNumber);}

        public int NewId()
        {
            var gameList = _repository!.Listar();

            if (gameList.Count == 0)
            {
                return 1;
            }

            return gameList.Max(idMax => idMax.Id) + 1;
        }
    }
}