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
    }
}
