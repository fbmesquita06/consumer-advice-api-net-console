using System.Text.Json;
using System.Net.Http;

// Classes de mapeamento do JSON
public class Slip
{
    public string? advice { get; set; }
    public int id { get; set; }
}

public class AdviceResponse
{
    public Slip? slip { get; set; }
}

// Código principal (Top-Level Statements)
const string API_ENDPOINT = "https://api.adviceslip.com/advice";

// Cria o cliente HTTP
using HttpClient client = new HttpClient();

Console.WriteLine("Iniciando requisição para obter dados de um conselho:");
Console.WriteLine(API_ENDPOINT);
Console.WriteLine("-----------------------------------------------------");

try
{
    // O await funciona diretamente aqui
    HttpResponseMessage response = await client.GetAsync(API_ENDPOINT);
    response.EnsureSuccessStatusCode();

    string jsonResponse = await response.Content.ReadAsStringAsync();
    AdviceResponse? data = JsonSerializer.Deserialize<AdviceResponse>(jsonResponse);

    if (data?.slip?.advice != null)
    {
        Console.WriteLine("\nConselho de Hoje:");
        Console.WriteLine(data.slip.advice);
    }
    else
    {
        Console.WriteLine("\nErro: A API não retornou um conselho válido.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"\nOcorreu um erro: {ex.Message}");
}