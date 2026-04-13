using ComplaintManagement.API.Models;
using ComplaintManagement.Business.Interfaces.UserManagement;
using ComplaintManagement.Util.Models.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/UserRoles")]
public class UserRolesController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAssociateService _associateService;
    private readonly IApproverService _approverService;

    public UserRolesController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IAssociateService associateService,
        IApproverService approverService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _associateService = associateService;
        _approverService = approverService;
    }

    // Get All Users
    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        var users = _userManager.Users
            .Select(u => new
            {
                u.Id,
                u.UserName,
                u.Email
            })
            .ToList();

        return Ok(users);
    }

    // Get Roles for Specific User
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserRoles(string userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("User not found");

            var roles = new List<RoleSelectionDto>();

            var allRoles = _roleManager.Roles.ToList();

            foreach (var role in allRoles)
            {
                if (string.IsNullOrEmpty(role.Name))
                    continue;

                var isInRole = await _userManager.IsInRoleAsync(user, role.Name);

                roles.Add(new RoleSelectionDto
                {
                    RoleName = role.Name,
                    IsSelected = isInRole
                });
            }

            return Ok(new ManageUserRolesDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // Update Roles
    [HttpPost("update")]
    public async Task<IActionResult> UpdateUserRoles([FromBody] ManageUserRolesDto model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
            return NotFound();

        //foreach (var role in _roleManager.Roles)
        //{
        //    if (string.IsNullOrEmpty(role.Name))
        //        continue;

        //    var selected = model.Roles
        //        .FirstOrDefault(r => r.RoleName == role.Name);

        //    if (selected != null && selected.IsSelected)
        //    {
        //        if (!await _userManager.IsInRoleAsync(user, role.Name))
        //            await _userManager.AddToRoleAsync(user, role.Name);
        //    }
        //    else
        //    {
        //        if (await _userManager.IsInRoleAsync(user, role.Name))
        //            await _userManager.RemoveFromRoleAsync(user, role.Name);
        //    }
        //}

        var selectedRoles = model.Roles
    .Where(r => r.IsSelected)
    .Select(r => r.RoleName!)
    .ToList();

        var currentRoles = await _userManager.GetRolesAsync(user);

        var rolesToAdd = selectedRoles.Except(currentRoles);
        var rolesToRemove = currentRoles.Except(selectedRoles);

        var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
        if (!addResult.Succeeded)
            return BadRequest(addResult.Errors);

        var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
        if (!removeResult.Succeeded)
            return BadRequest(removeResult.Errors);

        // Get final updated roles
        var finalRoles = await _userManager.GetRolesAsync(user);

        // Sync Associate table
        await _associateService.SyncAssociateByRolesAsync(user.UserName, finalRoles.ToList());

        // 🔥 NEW: Sync Manager table (Approver role)
        await _approverService.SyncManagerByRolesAsync(user.UserName, finalRoles.ToList());

        return Ok("Roles updated successfully");
    }
}
