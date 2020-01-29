using AutoMapper;
using System;
using System.Collections.Generic;
using WellCare.Core.Interface;
using WellCare.Models;
using WellCare.Repositories.Entities;
using WellCare.Repositories.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace WellCare.Core
{
    public class UserManager : IEntityManager<UserDetails, UserListItem, string>
    {
        private readonly IBaseRepository<User> _userRepository;

        public UserManager(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDetails> GetByIdAsync(string id)
        {
            //find the user with the id specified
            var user = (await _userRepository.AsQueryAsync()).FirstOrDefault(p => p.UserId == id);

            //the user hasnt been found
            if (user == null)
            {
                return new UserDetails
                {
                    status = new Status
                    {
                        StatusCode = Status.FAILURE_STATUS_CODE,
                        StatusDesc = $"USER WITH ID {id} NOT FOUND"
                    }
                };
            }

            //we can safely remove him
            return Mapper.Map<UserDetails>(user);

        }

        public async Task<ICollection<UserListItem>> List()
        {
            return Mapper.Map<List<UserListItem>>((await _userRepository.AsQueryAsync()));
        }

        public async Task<Status> RemoveByIdAsync(string id)
        {
            //find the user with the id specified
            var user = (await _userRepository.AsQueryAsync()).FirstOrDefault(p => p.UserId == id);

            //the user hasnt been found
            if (user == null)
            {
                return Status.SUCCESS;
            }

            //we can safely remove him
            await _userRepository.RemoveAsync(user);

            //success
            return Status.SUCCESS;
        }

        public async Task<Status> SaveAsync(UserDetails userDetails)
        {
            //user details missing something
            if (!userDetails.IsValid())
            {
                //return error
                return userDetails.status;
            }

            User savedEntity;
            Status result;

            //look for the user
            var existingUser = (await _userRepository.AsQueryAsync()).FirstOrDefault(p => p.UserId == userDetails.UserId);

            //user doesnt exist..so we add him
            if (existingUser == null)
            {
                savedEntity = Mapper.Map<User>(userDetails);

                await _userRepository.AddAsync(savedEntity);

                //success
                result = Status.SUCCESS;
                return result;
            }

            //update the user
            savedEntity = Mapper.Map(userDetails, existingUser);
            savedEntity.DateModified = DateTime.UtcNow;
            await _userRepository.UpdateAsync(savedEntity);

            //success
            result = Status.SUCCESS;
            return result;

        }
    }
}
