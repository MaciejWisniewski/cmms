using CMMS.Domain.Maintenance.Operators;
using CMMS.Domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Operators.CreateOperator
{
    public class CreateOperatorCommandHandler : IRequestHandler<CreateOperatorCommand>
    {
        private readonly IOperatorRepository _operatorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOperatorCommandHandler(IOperatorRepository operatorRepository, IUnitOfWork unitOfWork)
        {
            _operatorRepository = operatorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateOperatorCommand request, CancellationToken cancellationToken)
        {
            var worker = Operator.Create(
                    request.Id,
                    request.UserName,
                    request.Email,
                    request.FullName,
                    request.PhoneNumber
                );

            await _operatorRepository.AddAsync(worker);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
