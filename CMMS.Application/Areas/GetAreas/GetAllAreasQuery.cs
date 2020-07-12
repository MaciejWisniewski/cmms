using System.Collections.Generic;
using MediatR;

namespace CMMS.Application.Areas.GetAreas
{
    public class GetAllAreasQuery : IRequest<List<AreaDto>>
    {
    }
}
