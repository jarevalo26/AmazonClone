using System.Data;
using AutoMapper;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Addresses.ViewModels;
using Ecommerce.Application.Features.Auths.Users.ViewModels;
using Ecommerce.Application.Persistences;
using Ecommerce.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Auths.Users.Commands;


#region Validator
public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email cannot be null.");
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password cannot be null.");
    }
}
#endregion

#region Command
public class LoginUserCommand : IRequest<AuthResponse>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
#endregion

#region CommandHandler
public class LoginUserCommandHandler(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IAuthService authService,
    IMapper mapper,
    IUnitOfWork unitOfWork)
    : IRequestHandler<LoginUserCommand, AuthResponse>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly IAuthService _authService = authService;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email!);
        if (user == null) throw new NotFoundException(nameof(User), request.Email!);
        if (!user.IsActive) throw new Exception($"The user {request.Email} is blocked, contact the administrator.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password!, false);
        if (!result.Succeeded) throw new Exception("User credentials are invalid.");

        var address = await _unitOfWork.Repository<Address>().GetEntityAsync(
            a => a.Username == user.UserName
        );
        
        var roles = await _userManager.GetRolesAsync(user);
        var token = _authService.CreateToken(user, roles);
        var authResponse = new AuthResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.Phone,
            Email = user.Email,
            Username = user.UserName,
            Avatar = user.AvatarUrl,
            Address = _mapper.Map<AddressViewModel>(address),
            Token = token,
            Roles = roles
        };
        
        return authResponse;
    }
}
#endregion

