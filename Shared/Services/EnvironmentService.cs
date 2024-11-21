using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

public class EnvironmentService
{
    private readonly IHostEnvironment _environment;
    private readonly IConfiguration _configuration;

    public EnvironmentService(IHostEnvironment environment, IConfiguration configuration)
    {
        _environment = environment;
        _configuration = configuration;
    }

    public bool IsDevelopment()
    {
        return _environment.IsDevelopment();
    }

    public bool IsProduction()
    {
        return _environment.IsProduction();
        
    }

    public bool IsQA()
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
        string initialCatalog = GetInitialCatalog(connectionString);

        if (initialCatalog.Equals("MateQA"))
        {
            return true;
        }

        return false;

    }

    public string getName()
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
        string initialCatalog = GetInitialCatalog(connectionString);

        if(initialCatalog.Equals("MateProd")) {
            return "Production";
        }
        else
        {
            return "QA";
        }

    }

    static string GetInitialCatalog(string connectionString)
    {
        string initialCatalog = string.Empty;

        int initialCatalogIndex = connectionString.IndexOf("Initial Catalog =", StringComparison.OrdinalIgnoreCase);
        if (initialCatalogIndex >= 0)
        {
            initialCatalogIndex += "Initial Catalog=".Length;
            int catalogValueStartIndex = connectionString.IndexOf("=", initialCatalogIndex) + 1;
            int catalogValueEndIndex = connectionString.IndexOf(";", catalogValueStartIndex);
            if (catalogValueEndIndex < 0)
            {
                catalogValueEndIndex = connectionString.Length;
            }

            initialCatalog = connectionString.Substring(catalogValueStartIndex, catalogValueEndIndex - catalogValueStartIndex).Trim();
        }

        return initialCatalog;
    }

}