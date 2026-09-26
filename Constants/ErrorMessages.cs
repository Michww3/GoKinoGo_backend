namespace GoKinoGo.Constants;

public static class ErrorMessages
{
    public static class General
    {
        public const string NotFound = "The requested resource was not found.";
        public const string Unauthorized = "You are not authorized to perform this action.";
        public const string Forbidden = "You do not have permission to perform this action.";
        public const string Conflict = "The request conflicts with the current state of the server.";
        public const string ValidationError = "The request contains validation errors.";
        public const string InternalServerError = "An unexpected error occurred. Please try again later.";
    }

    public static class User
    {
        public const string NotFound = "User not found.";
        public const string EmailExists = "User with this email already exists.";
        public const string UserNameExists = "User with this username already exists.";
        public const string InvalidCredentials = "Invalid email or password.";
        public const string CannotUpdateOtherUser = "You do not have permission to update this user.";
        public const string CannotDeleteOtherUser = "You do not have permission to delete this user.";
        public const string Unauthorized = "You must be logged in to perform this action.";
        public const string IncorrectCurrentPassword = "The current password is incorrect.";
    }

    public static class Movie
    {
        public const string NotFound = "Movie not found.";
        public const string GenreNotFound = "One or more genre IDs are not found.";
        public const string InvalidReleaseDate = "Release date cannot be in the future.";
        public const string InvalidLength = "Movie length must be greater than zero.";
    }

    public static class Comment
    {
        public const string NotFound = "Comment not found.";
        public const string CannotDeleteOtherComment = "You do not have permission to delete this comment.";
        public const string CannotLoadCreated = "Created comment cannot be loaded.";
    }

    public static class Genre
    {
        public const string NotFound = "Genre not found.";
        public const string NameExists = "Genre with this name already exists.";
    }

    public static class Auth
    {
        public const string InvalidCredentials = "Invalid email or password.";
        public const string InvalidVerificationToken = "Invalid email verification token.";
        public const string EmailAlreadyConfirmed = "Email is already confirmed.";
        public const string EmailVerificationCooldown = "A verification email was sent recently. Please wait before requesting another.";
    }

    public static class MovieCollection
    {
        public const string NotFound = "Movie collection not found.";
        public const string NameExists = "Movie collection with this name already exists.";
        public const string InvalidType = "Invalid collection type.";
    }

    public static class CollectionItem
    {
        public const string NotFound = "Collection item not found.";
        public const string PositionExists = "An item already exists at this position.";
        public const string ItemAlreadyInCollection = "This movie is already in the collection.";
    }

    public static class MovieRating
    {
        public const string NotFound = "Movie rating not found.";
        public const string InvalidValue = "Rating value must be between 1 and 10.";
        public const string CannotDeleteOtherRating = "You do not have permission to delete this rating.";
    }
}
