using Firebase.Storage;
using FirebaseAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Repository.Helpers;
using Service.Interfaces;
using Service.Models.Firebase;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Service.Services
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        private readonly FirebaseApp _firebaseApp;
        private readonly string _storageBucket;

        // Constructor that accepts FirebaseApp and FirebaseSettings
        public FirebaseStorageService(FirebaseApp firebaseApp, IOptions<FirebaseSettings> firebaseSettings)
        {
            _firebaseApp = firebaseApp ?? throw new ArgumentNullException(nameof(firebaseApp));
            _storageBucket = firebaseSettings.Value.StorageBucket ?? throw new ArgumentNullException(nameof(firebaseSettings));
        }

        // Method to upload a file
        public async Task<List<string>> UploadFile(List<IFormFile> files)
        {
            var uploadUrls = new List<string>();

            foreach (var file in files)
            {
                // Check if the file is valid
                if (file == null || file.Length == 0)
                {
                    throw new ArgumentException("One of the files is invalid.");
                }

                try
                {
                    // Generate a unique file name
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var bucket = new FirebaseStorage(_storageBucket);

                    // Upload the file stream to Firebase Storage
                    using (var stream = file.OpenReadStream())
                    {
                        var uploadTask = await bucket.Child("images/" + fileName).PutAsync(stream);

                        // Add the download URL to the list
                        uploadUrls.Add(uploadTask); // The uploadTask variable should hold the download URL
                    }
                }
                catch (Exception ex)
                {
                    throw new CustomException($"Failed to upload image {file.FileName}: " + ex.Message);
                }
            }

            return uploadUrls; // Return the list of download URLs
        }
    }
}
