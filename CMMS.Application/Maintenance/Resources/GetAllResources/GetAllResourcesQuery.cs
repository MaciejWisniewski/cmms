using MediatR;
using System.Collections.Generic;

namespace CMMS.Application.Maintenance.Resources.GetAllResources
{
    public class GetAllResourcesQuery : IRequest<List<ResourceDto>>
    {

    }
}
