using MediatR;
using OnlineWallet.Application.Users.Models;
using OnlineWallet.Application.Wallets.Models;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Application.Users.Queries
{
    public record GetUserQuery(Guid userId) : IRequest<Result<GetUserModel>>;

    internal class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<GetUserModel>>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserQueryHandler(IGenericRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetUserModel>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userId);

            if (user.Value == null)
            {
                throw new EntityNotFoundException(ErrorMessages.UserNotFound);
            }

            var userModel = new GetUserModel
            {
                FirstName = user.Value.FirstName,
                LastName = user.Value.LastName,
                Email = user.Value.Email,
                Wallets = null
            };

            return Result<GetUserModel>.Succeed(userModel);
        }
    }
}
