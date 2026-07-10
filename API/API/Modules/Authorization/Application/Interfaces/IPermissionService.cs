using Authorization.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<List<string>> GetPermissionsByUserId(Guid userId);
        Task<List<PermissionTreeDto>> GetAllAsync();
    }
}
