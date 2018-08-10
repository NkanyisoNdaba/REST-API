using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;

namespace NancyFXExercise
{
    public class UsersModule : NancyModule
    {
       private readonly List<User> _users = new List<User>();

        public UsersModule()
        {   
            Get["/hello"] = parameters => "Hello World";

            Get["/{api}/{_users}/{id}"] = parameters =>
            {
                int id = parameters.id;
                var user = GetUserById(id);

                if (user == null)
                {
                    return Negotiate
                        .WithStatusCode(HttpStatusCode.NotFound);
                }

                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(user);
            };

            Get["/{api}/{_users}"] = parameters =>
            {
               var listUsers= GetList();

                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(listUsers);
            };

            Post["/hello"] = parameters => "Recieved user request";

            Post["/{api}/{user}"] = parameters =>
            {
               
                var addUser = this.Bind<User>();

                Random random = new Random();
                int id = random.Next(3, 10);

                _users.Add
                (
                    new User
                    {
                    Id = id,
                    Name = addUser.Name,
                    Age = addUser.Age

                    }
                );                 
                
                return Negotiate.WithModel(_users);
            };

            Delete["/{api}/{user}{id}"] = parameters =>
            {
                int id = parameters.id;

                var findUser = GetUserById(id);


                if (findUser == null)
                {
                    return Negotiate
                        .WithStatusCode(HttpStatusCode.Gone);
                }

                var index = _users.IndexOf(_users.FirstOrDefault(x => x.Id == id));
                _users.RemoveAt(index);
                

                return 
                    Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(_users.ToList());
            };

        }

        public List<User> GetList()
        {
            var listUsers = new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "xxx",
                    Age = 37
                },
                new User
                {
                    Id = 2,
                    Name = "yyy",
                    Age = 32
                }
            };

            return listUsers.ToList();
        }

     

        public User GetUserById(int id)
        {  
            var getbyId = GetList().FirstOrDefault(i => i.Id.Equals(id));
            
            return getbyId;
        }

   

    }

    public class User
    {
        public  int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

 
}
