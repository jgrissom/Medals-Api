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
}
