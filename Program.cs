using DadosComCSV.Models;
using DadosComCSV.Repositories;
using DadosComCSV.Services;

var repository = new GameRepository();
var service = new GameService(repository);

bool executing = true;

while (executing)
{
    Console.Write
    ("""
    
    ======= GAME MANAGEMENT =======
    [1] REGISTER GAME
    [2] GAMES LIST
    [0] EXIT

    Choose an option: 
    """);

    string option = Console.ReadLine()!;

    try
    {
        switch (option)
        {
            case "1":
                Register();
                break;

            case "2":
                GameList();
                break;

            case "0":
                executing = false;
                break;

            default:
                Console.WriteLine("Invalid Option, Choose Another Option!");
                break;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

void GameList()
{
    Console.Clear();

    Console.WriteLine("======== REGISTERED GAMES ========");
    var games = service.Listar();
    if (games.Count == 0) { Console.WriteLine("NO GAMES REGISTERED"); }
    else
    {
        foreach (var item in games)
        {
            ShowGame(item);
        }
    }
}

void ShowGame(Game game)
{
    Console.WriteLine
    ($"""
    -----------------------------------------
    ID: {game.Id}
    Name: {game.Nome}
    Developer: {game.Desenvolvedora}
    Gender: {game.Genero}
    Year: {game.AnoLancamento}
    Price: {game.Preco}
    """);

    Pausar();
}

void Pausar()
{
    Console.WriteLine("\nPress ENTER to continue");
    Console.ReadLine();
}

void Register()
{
    Console.Clear();

    Console.WriteLine("======= REGISTER GAME =======");

    var game = ReadGameDate();

    service.Register(game);

    Console.WriteLine("\nGame registered successfully!");
    Pausar();
}

Game ReadGameDate()
{
    Game newGame = new();

    Console.Write("Enter the game name: ");         
    newGame.Nome = Console.ReadLine() ?? "Name not provided";
         
    Console.Write("Enter the developer name: ");
    newGame.Desenvolvedora = Console.ReadLine() ?? "Developer not provided" ;
         
    Console.Write("Enter the game gender: ");
    newGame.Genero = Console.ReadLine()?? "Gender not provided";
         
    Console.Write("Enter the release year: ");
    newGame.AnoLancamento = int.Parse(Console.ReadLine() ?? "Year not provided");

    Console.Write("Enter the game price: ");
    newGame.Preco = decimal.Parse(Console.ReadLine() ?? "Price not provided");

    return newGame;
}