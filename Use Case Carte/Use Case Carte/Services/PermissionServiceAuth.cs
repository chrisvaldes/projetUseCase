using Microsoft.AspNetCore.Components.Authorization;

public class PermissionServiceAuth
{
    private readonly AuthenticationStateProvider _auth;

    public PermissionServiceAuth(AuthenticationStateProvider auth)
    {
        _auth = auth;
    }

    public async Task<HashSet<string>> GetPermissions()
    {
        var state = await _auth.GetAuthenticationStateAsync();

        return state.User.Claims
            .Where(c => c.Type == "permission")
            .Select(c => c.Value)
            .ToHashSet();
    }

    public async Task<bool> HasPermission(string permission)
    {
        var permissions = await GetPermissions();
        return permissions.Contains(permission);
    }
}