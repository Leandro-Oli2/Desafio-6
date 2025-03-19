using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FarmacySystem.data;
using FarmacySystem.model;
using Npgsql;

namespace FarmacySystem.controller
{
    public class CrudUser
    {
        public void InsertUser(string name, string role, string cpf, string password)
        {
           try
            {
                if (role != "farmaceutico" && role != "gerente" && role != "atendente")
                    throw new Exception($"Invalid Role for User: {role}\nOnly can be: farmaceutico, gerente or atendente");
                using (var db = new AppDbContext())
                {
                    db.Users.Add(new User {Name = name, Role = role, Cpf = cpf, Password = password});
                    db.SaveChanges();
                    Console.WriteLine($"User {name} sucessfully created!\n");
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"ERROR DB: {ex.Message}");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            } 
        }

        public List<User>? ListUser(int? id = null, string? name = null, string? role = null, string? cpf = null)
        {
            try
            {
                List<User> result = new List<User>();
                var users = new List<User>();
                using (var db = new AppDbContext())
                {
                    if (id == null && name == null && role == null && cpf == null)   // GET ALL
                    {
                        users = [.. db.Users];
                    } else if (id != null && name == null && role == null && cpf == null)    // GET BY ID
                    {
                        users = [.. db.Users.Where(u => u.Id == id)];
                    } else if (id == null && name != null && role == null && cpf == null)   // GET BY NAME
                    {
                        users = [.. db.Users.Where(u => u.Name == name)];
                    }
                    else
                    {
                        throw new Exception("ERRO: Invalid parameters");
                    }
                    
                    foreach (var user in users)
                    {
                        string linha  = $"{user.Id}{user.Name}{user.Role}{user.Cpf}{user.Password}";
                        result.Add(user);
                    }
                    return result;
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"ERROR DB: {ex.Message}");
                return null;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                return null;
            }
        }

        public void UpdateUser(int id, string? name = null, string? role = null, string? password = null)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var user = db.Users.Find(id) ?? throw new Exception($"Unable to find user {id}");
                    
                    user.Name = name ?? user.Name;
                    user.Password = password ?? user.Password;
                    user.Role = role ?? user.Role;

                    db.SaveChanges();
                    Console.WriteLine($"User {id} updated sucessfully\n");
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"ERROR DB: {ex.Message}");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var user = db.Users.Find(id) ?? throw new Exception($"Unable to find user {id}");

                    db.Users.Remove(user);
                    db.SaveChanges();
                    Console.WriteLine($"User {id} deleted sucessfully\n");
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"ERROR DB: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public (bool, string) LoginUser(string cpf, string password)
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Cpf.Equals(cpf)) ?? throw new Exception($"Unable to find login {cpf}");
                    return (user != null && user.Password.Equals(password), $"{user!.Role.ToLower()}");
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"ERROR DB: {ex.Message}");
                return (false, ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                return (false, ex.Message);
            }
        }
    }
}