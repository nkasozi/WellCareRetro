using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WellCare.Core.Interface;
using WellCare.Models;
using WellCare.Repositories.Entities;
using WellCare.Repositories.Interface;

namespace WellCare.Core
{
    public class HealthScoreManager : IHealthScoreManager
    {
        private readonly IBaseRepository<HealthScore> _repository;
        public HealthScoreManager(IBaseRepository<HealthScore> repository)
        {
            _repository = repository;
        }

        public HealthScoreDetails Get(int id)
        {
            //find the user with the id specified
            var score = _repository.AsQuery().FirstOrDefault(p => p.Id == id);

            //the user hasnt been found
            if (score == null)
            {
                return new HealthScoreDetails
                {
                    status = new Status
                    {
                        StatusCode = Status.FAILURE_STATUS_CODE,
                        StatusDesc = $"SCORE WITH ID {id} NOT FOUND"
                    }
                };
            }

            //we can safely return him
            var details = Mapper.Map<HealthScoreDetails>(score);
            details.status = Status.SUCCESS;

            return details;
        }

        public ICollection<HealthScoreListItem> List()
        {
            return Mapper.Map<List<HealthScoreListItem>>(_repository.AsQuery());
        }

        public Status Remove(int id)
        {
            //find the user with the id specified
            var score = _repository.AsQuery().FirstOrDefault(p => p.Id == id);

            //the user hasnt been found
            if (score == null)
            {
                return Status.SUCCESS;
            }

            //we can safely remove him
            _repository.Remove(score);

            //success
            return Status.SUCCESS;
        }

        public Status Save(HealthScoreDetails details)
        {
            //user details missing something
            if (!details.IsValid())
            {
                //return error
                return details.status;
            }

            HealthScore savedEntity;
            Status result;

            //look for the user
            var existing = _repository.AsQuery().FirstOrDefault(p => p.Id == details.Id);

            //user doesnt exist..so we add him
            if (existing == null)
            {
                savedEntity = Mapper.Map<HealthScore>(details);

                _repository.Add(savedEntity);

                //success
                result = Status.SUCCESS;
                return result;
            }

            //update the user
            savedEntity = Mapper.Map<HealthScoreDetails, HealthScore>(details, existing);
            savedEntity.DateModified = DateTime.UtcNow;
            _repository.Update(savedEntity);

            //success
            result = Status.SUCCESS;
            return result;
        }
    }
}
