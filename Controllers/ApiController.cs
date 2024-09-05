using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController, Route("[controller]/country")]
public class ApiController(DataContext db) : ControllerBase
{
    private readonly DataContext _dataContext = db;

    // http get entire collection
    [HttpGet, SwaggerOperation(summary: "return entire collection", null)]
    public IEnumerable<Country> Get()
    {
        return _dataContext.Countries;
    }
    // http get specific member of collection
    [HttpGet("{id}"), SwaggerOperation(summary: "return specific member of collection", null)]
    public Country? Get(int id)
    {
        return _dataContext.Countries.Find(id);
    }
    // http post member to collection
    [HttpPost, SwaggerOperation(summary: "add member to collection", null), ProducesResponseType(typeof(Country), 201), SwaggerResponse(201, "Created")]
    public async Task<ActionResult<Country>> Post([FromBody] Country country) {
        _dataContext.Add(country);
        await _dataContext.SaveChangesAsync();
        return country;
    }
    // http delete member from collection
    [HttpDelete("{id}"), SwaggerOperation(summary: "delete member from collection", null), ProducesResponseType(typeof(Country), 204), SwaggerResponse(204, "No Content")]
    public async Task<ActionResult> Delete(int id){
        Country? country = await _dataContext.Countries.FindAsync(id);
        if (country == null){
            return NotFound();
        }
        _dataContext.Remove(country);
        await _dataContext.SaveChangesAsync();
        return NoContent();
    }
}
