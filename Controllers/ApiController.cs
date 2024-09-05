using Microsoft.AspNetCore.Mvc;

[ApiController, Route("[controller]/country")]
public class ApiController(DataContext db) : ControllerBase
{
    private readonly DataContext _dataContext = db;

    // http get entire collection
    [HttpGet]
    public IEnumerable<Country> Get()
    {
        return _dataContext.Countries;
    }
    // http get specific member of collection
    [HttpGet("{id}")]
    public Country? Get(int id)
    {
        return _dataContext.Countries.Find(id);
    }
    // http post member to collection
    [HttpPost]
    public async Task<ActionResult<Country>> Post([FromBody] Country country) {
        _dataContext.Add(country);
        await _dataContext.SaveChangesAsync();
        return country;
    }
}
