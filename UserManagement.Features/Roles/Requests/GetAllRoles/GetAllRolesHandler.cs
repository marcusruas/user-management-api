using UserManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.SharedKernel.Messaging;
using UserManagement.Features.Roles.ValueObjects;
using UserManagement.Infrastructure.Repositories.Roles;
using UserManagement.Domain.Users.Specifications;
using UserManagement.Domain.Users.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace UserManagement.Features.Roles.Requests.GetAllRoles
{
    public class GetAllRolesHandler : FeatureHandler<GetAllRolesRequest, PaginatedList<RoleDto>>
    {
        public GetAllRolesHandler(IRolesRepository repository, IMessaging messaging, ILogger<FeatureHandler<GetAllRolesRequest, PaginatedList<RoleDto>>> logger) : base(messaging, logger)
        {
            _repository = repository;
        }

        private readonly IRolesRepository _repository;
        private PaginatedList<Role> _roles;
        private PaginatedList<RoleDto> _result;

        public override async Task<PaginatedList<RoleDto>> HandleRequest(GetAllRolesRequest request, CancellationToken cancellationToken)
        {
            _roles = await _repository.QueryAsync(new SearchAllRolesSpecification(request.Page.Value, request.RecordsPerPage.Value));

            CastPagination();

            return _result;
        }

        private void CastPagination()
        {
            var items = _roles.Items.Select(TinyMapper.Map<RoleDto>);
            _result = PaginatedList<RoleDto>.CreateFromPaginatedList(items, _roles);
        }
    }
}
