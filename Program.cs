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
                GameRegister();
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
    var gamesList = service.Listar();
    foreach (var item in gamesList)
    {
        Console.WriteLine($"Id: {item.Id} Name: {item.Nome} - Price: {item.Preco:c2}");
    }
}

void GameRegister()
{
    Game newGame = new();

    Console.Write("Enter the game name: ");
    newGame.Nome = Console.ReadLine()!;

    Console.Write("Enter the developer name: ");
    newGame.Desenvolvedora = Console.ReadLine()!;

    Console.Write("Enter the game gender: ");
    newGame.Genero = Console.ReadLine()!;

    Console.Write("Enter the release year: ");
    newGame.AnoLancamento = int.Parse(Console.ReadLine()!);
    
    Console.Write("Enter the game price: ");
    newGame.Preco = decimal.Parse(Console.ReadLine()!);

    if (!service.GameRegister(newGame))
    {
        Console.WriteLine("Error: Some data was recorded incorrectly. Please check again!");
        return;
    }

    Console.WriteLine("REGISTERED GAME");
}