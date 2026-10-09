using GoKinoGo.Constants;
using GoKinoGo.DataAccess.UnitOfWork;
using GoKinoGo.DTOs.Cart;
using GoKinoGo.Exceptions;
using GoKinoGo.Services.Interfaces;
using static GoKinoGo.Services.Interfaces.ICartService;

namespace GoKinoGo.Services;

public class CartService(IUnitOfWork unitOfWork) : ICartService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<(IEnumerable<ValidatedCartItemDto>, CartValidationStatus)> ValidateAsync(IEnumerable<CartItemDto> items)
    {
        var cartItems = items.ToList();
        if (cartItems.Count == 0)
            throw new BadRequestException(ErrorMessages.Cart.EmptyCart);

        var movieIds = cartItems
            .Select(x => x.MovieId)
            .Distinct()
            .ToList();

        var movies = await _unitOfWork.Movies.GetByIdsAsync(movieIds);

        var movieDict = movies.ToDictionary(x => x.Id);

        var result = new List<ValidatedCartItemDto>();

        var hasMissingMovies = false;
        var hasPriceChanges = false;

        foreach (var item in cartItems)
        {
            if (!movieDict.TryGetValue(item.MovieId, out var movie))
            {
                hasMissingMovies = true;
                continue;
            }

            if (item.Price != movie.Price)
            {
                hasPriceChanges = true;
            }

            result.Add(new ValidatedCartItemDto
            {
                MovieId = movie.Id,
                Name = movie.Name,
                PosterUrl = movie.PosterUrl,
                Price = movie.Price,
                Quantity = item.Quantity,
            });
        }

        CartValidationStatus cartValidationStatus = hasPriceChanges
            ? CartValidationStatus.PriceChanged
            : CartValidationStatus.Valid;
        var status = hasMissingMovies
            ? CartValidationStatus.MovieNotFound
            : cartValidationStatus;

        return new(result, status);
    }
}
