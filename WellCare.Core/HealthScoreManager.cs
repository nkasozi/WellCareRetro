﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<HealthScoreDetails> GetByIdAsync(int id)
        {
            //find the user with the id specified
            var score = (await _repository.AsQueryAsync()).FirstOrDefault(p => p.Id == id);

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

        public async Task<ICollection<HealthScoreListItem>> List()
        {
            return Mapper.Map<List<HealthScoreListItem>>((await _repository.AsQueryAsync()));
        }

        public async Task<Status> RemoveByIdAsync(int id)
        {
            //find the user with the id specified
            var score = (await _repository.AsQueryAsync()).FirstOrDefault(p => p.Id == id);

            //the user hasnt been found
            if (score == null)
            {
                return Status.SUCCESS;
            }

            //we can safely remove him
            await _repository.RemoveAsync(score);

            //success
            return Status.SUCCESS;
        }

        public async Task<Status> SaveAsync(HealthScoreDetails details)
        {
            //details invalid..missing something
            if (!details.IsValid())
            {
                //return error
                return details.status;
            }

            HealthScore savedEntity;
            Status result;

            //look for the exisitng
            var existing = (await _repository.AsQueryAsync()).FirstOrDefault(p => p.Id == details.Id);

            //no exisiting found..so we add him
            if (existing == null)
            {
                savedEntity = Mapper.Map<HealthScore>(details);

                _repository.AddAsync(savedEntity);

                //success
                result = Status.SUCCESS;
                return result;
            }

            //update the exisiting
            savedEntity = Mapper.Map<HealthScoreDetails, HealthScore>(details, existing);
            savedEntity.DateModified = DateTime.UtcNow;
            await _repository.UpdateAsync(savedEntity);

            //success
            result = Status.SUCCESS;
            return result;
        }
    }
}
