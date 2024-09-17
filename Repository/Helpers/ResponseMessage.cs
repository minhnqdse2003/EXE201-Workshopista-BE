using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helpers
{
    public static class ResponseMessage
    {
        //Success message for Auth
        public const string LoginSuccess = "User successfully logged in.";
        public const string RegisterSuccess = "User successfully registered.";
        public const string TokenRefreshed = "Token successfully refreshed.";

        // Error messages for Auth
        public const string InvalidLogin = "Invalid login credentials.";
        public const string UserNotFound = "User not found.";
        public const string TokenInvalid = "Invalid token.";
        public const string TokenExpired = "Token has expired.";
        public const string Unauthorized = "Unauthorized access.";

        // Validation messages
        public const string InvalidInput = "One or more fields are invalid.";
        public const string MissingRequiredFields = "Required fields are missing.";

        // Server error messages
        public const string InternalServerError = "An unexpected error occurred. Please try again later.";

        // Success messages for CRUD operations
        public const string CreateSuccess = "Item successfully created.";
        public const string ReadSuccess = "Item successfully retrieved.";
        public const string UpdateSuccess = "Item successfully updated.";
        public const string DeleteSuccess = "Item successfully deleted.";

        // Error messages for CRUD operations
        public const string CreateFail = "Failed to create the item.";
        public const string ReadFail = "Failed to retrieve the item.";
        public const string UpdateFail = "Failed to update the item.";
        public const string DeleteFail = "Failed to delete the item.";

        // Not found messages
        public const string ItemNotFound = "The requested item could not be found.";

        // Validation messages for CRUD
        public const string InvalidCreateRequest = "The create request contains invalid data.";
        public const string InvalidUpdateRequest = "The update request contains invalid data.";
    }
}
