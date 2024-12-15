using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using SwiftDocs.Models;

namespace SwiftDocs.Services
{
    public class UserManager
    {
        private const string FilePath = "users.csv";
        private readonly List<User> users = new List<User>();

        public UserManager()
        {
            LoadUsersFromFile();
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool RegisterUser(string firstName, string middleName, string lastName, string sex, string dateOfBirth,
            string province, string cityMunicipality, string barangay, string street, string email, string password)
        {
            if (users.Exists(u => u.Email == email)) return false;

            users.Add(new User
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Sex = sex,
                DateOfBirth = dateOfBirth,
                Province = province,
                CityMunicipality = cityMunicipality,
                Barangay = barangay,
                Street = street,
                Email = email,
                HashedPassword = HashPassword(password)
            });
            return true;
        }

        public bool ValidateUser(string email, string password)
        {
            var user = users.Find(u => u.Email == email);
            if (user == null) throw new Exception();

            return user.HashedPassword == HashPassword(password);
        }

        public User GetUser(string email)
        {
            return users.Find(u => u.Email == email);
        }

        public void SaveUsersToFile()
        {
            try
            {
                using (var writer = new StreamWriter(FilePath))
                {
                    foreach (var user in users)
                        writer.WriteLine(
                            $"{user.FirstName},{user.MiddleName},{user.LastName},{user.Sex},{user.DateOfBirth},{user.Province},{user.CityMunicipality},{user.Barangay},{user.Street},{user.Email},{user.HashedPassword}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving users to file: {ex.Message}");
            }
        }

        public void LoadUsersFromFile()
        {
            if (!File.Exists(FilePath)) return;

            try
            {
                using (var reader = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        if (parts.Length == 11)
                            users.Add(new User
                            {
                                FirstName = parts[0],
                                MiddleName = parts[1],
                                LastName = parts[2],
                                Sex = parts[3],
                                DateOfBirth = parts[4],
                                Province = parts[5],
                                CityMunicipality = parts[6],
                                Barangay = parts[7],
                                Street = parts[8],
                                Email = parts[9],
                                HashedPassword = parts[10]
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading users from file: {ex.Message}");
            }
        }
    }
}