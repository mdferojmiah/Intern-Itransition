using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using MySqlConnector;
using UserManagement.Models;

namespace UserManagement.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString){
            _connectionString = connectionString;
        }

        public bool RegisterUser(RegisterViewModel model){
            try{

                using(var connection = new MySqlConnection(_connectionString)){
                    connection.Open();

                    var sql = @"insert into Users(Name, Email, Password, RegistrationTime, Status) 
                                values(@Name, @Email, @Password, @RegistrationTime, 'active')";

                    using(var commad = new MySqlCommand(sql, connection)){
                        commad.Parameters.AddWithValue("@Name", model.Name);
                        commad.Parameters.AddWithValue("@Email", model.Email);
                        commad.Parameters.AddWithValue("@Password", model.Password);
                        commad.Parameters.AddWithValue("@RegistrationTime", DateTime.Now);
                        commad.ExecuteNonQuery();
                        return true;
                    }
                }

            }catch(MySqlException e){
                if(e.Number == 1062){
                    return false;
                }
                throw;
            }
        }

        public User? AuthenticateUser(string email, string password){
            User? user = null;

            using(var connection = new MySqlConnection(_connectionString)){
                connection.Open();

                var sql = @"select * from Users where Email = @Email and Password = @Password and IsDeleted = false";
                using(var command = new MySqlCommand(sql, connection)){
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    using(var reader = command.ExecuteReader()){
                        if(reader.Read()){
                            user = new User{
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString(),
                                RegistrationTime = Convert.ToDateTime(reader["RegistrationTime"]),
                                LastLoginTime = reader["LastLoginTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LastLoginTime"]),
                            };
                        }
                    }
                }

                if(user != null && user.Status == "active"){
                    var updateSql = @"update Users set LastLoginTime = @LastLoginTime where Id = @Id";
                    using(var updateCommand = new MySqlCommand(updateSql, connection)){
                        updateCommand.Parameters.AddWithValue("@LastLoginTime", DateTime.Now);
                        updateCommand.Parameters.AddWithValue("@Id", user.Id);
                        updateCommand.ExecuteNonQuery();
                    }

                }else if(user != null && user.Status == "blocked"){
                    return null;
                }
            }
            return user;
        }

        public bool IsUserValid(string email){
            using(var connection = new MySqlConnection(_connectionString)){
                connection.Open();

                var sql = @"select Status, IsDeleted from Users where Email = @Email";
                using(var command = new MySqlCommand(sql, connection)){
                    command.Parameters.AddWithValue("@Email", email);

                    using(var reader = command.ExecuteReader()){
                        if(reader.Read()){
                            var status = reader["Status"].ToString();
                            var isDeleted = Convert.ToBoolean(reader["IsDeleted"]);

                            return !isDeleted && status == "active";
                        }
                    }
                }
                
            }
            return false;
        }

        public List<User> GetAllUsers(){
            List<User> users = new List<User>();

            using(var connection = new MySqlConnection(_connectionString)){
                connection.Open();

                var sql = @"select * from Users where IsDeleted = false order by LastLoginTime desc";
                using(var command = new MySqlCommand(sql, connection)){
                    using(var reader = command.ExecuteReader()){
                        while(reader.Read()){
                            users.Add(new User{
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString(),
                                RegistrationTime = Convert.ToDateTime(reader["RegistrationTime"]),
                                LastLoginTime = reader["LastLoginTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LastLoginTime"]),
                            });
                        }
                    }
                }
            }
            return users;
        }

        public void BlockUsers(List<string> Emails){
            using(var connection = new MySqlConnection(_connectionString)){
                connection.Open();

                foreach(var email in Emails){
                    var sql = @"update Users set Status = 'blocked' where Email = @Email";
                    using(var commnad = new MySqlCommand(sql, connection)){
                        commnad.Parameters.AddWithValue("@Email", email);
                        commnad.ExecuteNonQuery();
                    }
                }
            }
        }

        public void UnblockUsers(List<string> Emails){
            using(var connection = new MySqlConnection(_connectionString)){
                connection.Open();

                foreach(var email in Emails){
                    var sql = @"update Users set Status = 'active' where Email = @Email";
                    using(var commnad = new MySqlCommand(sql, connection)){
                        commnad.Parameters.AddWithValue("@Email", email);
                        commnad.ExecuteNonQuery();
                    }
                }
            }
        } 

        public void DeleteUsers(List<string> Emails){
            using(var connection = new MySqlConnection(_connectionString)){
                connection.Open();

                foreach(var email in Emails){
                    var sql = @"update Users set IsDeleted = true where Email = @Email";
                    using(var commnad = new MySqlCommand(sql, connection)){
                        commnad.Parameters.AddWithValue("@Email", email);
                        commnad.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}