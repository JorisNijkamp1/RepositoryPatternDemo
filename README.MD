# Repository pattern

1. Eerst ga ga je nieuwe database aanmaken in Microsft SQL Server Management Studio, op basis van het AirBNB.bacpac bestand. Volg de stappen die getoond worden in je 
2. Om te testen of je database goed is opgezet doe je rechtermuisknop op de database en voor je onderstaande query uit. Als je resultaat ziet is je database goed opgezet. Onthou de naam van je database en van je server. Deze hebben we later nodig.
```sh
select * from neighbourhoods
```
3. Als dit werkt moet je dit project clonen en in je appsettings de juist database settings invullen. [GitHubRepositoryDemo](https://github.com/JorisNijkamp1/RepositoryPatternDemo)
4. Als dit werkt en je neigbourhoods kan verwijderen en toevoegen kan je verder met de volgende stappen.
5. Nu gaan we het repository pattern in bouwen.
6. Maak een folder Interfaces aan
7. Maak een folder Repositories aan.
8. Maak in folder Interfaces een interface aan genaamd INeighbourhoodsRepository
9. Hierin zet je onderstaande code.
 ```sh
// Get all Neighbourhood
Task<List<Neighbourhood>> GetAllNeighbourhoodsAsync();

// Get one Neighbourhood
Task<Neighbourhood> GetNeighbourhoodByIdAsync(string id);
        
// Create one Neighbourhood
Task<Neighbourhood> CreateNeighbourhoodAsync(Neighbourhood neighbourhood);
```
10. Vervolgens maak je in de folder repositories een class aan genaamd. NeighbourhoodRepository
11. Deze NeighbourhoodRepository laat je INeighbourhoodRepository implementeren.
12. Vervolgens moeten we de DBContext injecteren.
```sh
private readonly RepositoryPatternDemoDoneDBContext _context;

public NeighbourhoodRepository(RepositoryPatternDemoDoneDBContext context)
{
    _context = context;
}
```
13. Nu we de DBContext geinjecteerd hebben kunnen we de methodes gaan aanmaken die in de geimplementeerde interface staan.
```sh
 public async Task<Neighbourhood> CreateNeighbourhoodAsync(Neighbourhood neighbourhood)
 {
      _context.Add(neighbourhood);
    await _context.SaveChangesAsync();
    return neighbourhood;
}

public async Task<List<Neighbourhood>> GetAllNeighbourhoodsAsync()
{
    return await _context.Neighbourhoods.ToListAsync();
}

public async Task<Neighbourhood> GetNeighbourhoodByIdAsync(string id)
{
    return await _context.Neighbourhoods.FirstOrDefaultAsync(m => m.Neighbourhood1 == id);
}
```
14. Nu de details van de crud operaties verplaats zijn naar de repository. Kunnen de controllers omgebouwd worden. Ook hoef je de DBContext niet meer te injecten. Deze verantwoordelijkheid is nu verplaatst. De repository injecteer je moet onderstaande code.
```sh
private readonly INeighbourhoodsRepository _neighbourhoods;

public NeighbourhoodsController(INeighbourhoodsRepository neighbourhoods)
{
    _neighbourhoods = neighbourhoods;
}
```
15. Na dit is geinjecteerd kunnen de functies gemaakt worden.
```sh
// GET: AllNeighbourhoods
public async Task<IActionResult> Index()
{
    return View(await _neighbourhoods.GetAllNeighbourhoodsAsync());
}

// GET: Neighbourhoods/Details/5
public async Task<IActionResult> Details(string id)
{
    return View(await _neighbourhoods.GetNeighbourhoodByIdAsync(id));
}

// GET: Neighbourhoods/Create
public IActionResult Create()
{
    return View();
}

// POST: Neighbourhoods/Create
[HttpPost]
public async Task<IActionResult> Create([Bind("NeighbourhoodGroup,Neighbourhood1")] Neighbourhood neighbourhood)
{
    return View(await _neighbourhoods.CreateNeighbourhoodAsync(neighbourhood));
}
```
16. We hebben nu de repository geinjecteerd maar nog beschikbaar gemaakt van dependency injection. Dit doen we daar in de startup.cs in de configureservices methode een regel code toe tevoegen.
```sh
services.AddScoped<INeighbourhoodsRepository, NeighbourhoodRepository>();
```
17. Als je alles volgens de stappen hebt gevolgd heb je nu een werkend applicatie met het repository pattern. Met een aantal crud operaties.


// Voordelen
    Door dependency injection kan je het overal gemakkelijk gebruiken ne benaderen
    Abstractie
    Details verbergen
    Als je na het repository pattern 1 ding wilt veranderen doe je dat op1 plek. Dus niet overal
    Biojvoorbeeld ipv neighbourhood een stad pas je dat aan in interface en repository en klaar.
    Onderhoudbaarheid
    Loose coupling
    Beter testbaar
