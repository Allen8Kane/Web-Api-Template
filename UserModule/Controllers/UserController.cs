using Microsoft.AspNetCore.Mvc;

namespace ProjectOne;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly ProjectContext db;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
        db = new ProjectContext();
    }

    [HttpGet]
    public IEnumerable<User>? Get()
    {
        IQueryable<User>? users;
        users = db?.Users?.OrderBy(u => u.FirstName);
        return users;
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetById(int id)
    {
        User? user;
        user = db.Users?.Find(id);
        return user != null ? user : NotFound();
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        int? createdId;
        var _ = new User { FirstName = user.FirstName, Salary = user.Salary };
        db.Add(_);
        db.SaveChanges();
        createdId = _.Id;
        return Created($"~/api/User/{user.Id}", createdId);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, User user)
    {
        var userResult = db.Users?.Find(id);
        if (userResult == null) return NotFound();
        userResult.FirstName = user.FirstName;
        userResult.Salary = user.Salary;
        db.SaveChanges();
        return NoContent();

    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        User? user = db.Users?.Find(id);
        System.Console.WriteLine(user);
        if (user == null) return NotFound();
        db.Remove(user);
        db.SaveChanges();
        return NoContent();
    }
}
