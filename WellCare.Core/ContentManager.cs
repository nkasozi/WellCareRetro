using AutoMapper;
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
    public class ContentManager : IContentManager
    {
        IBaseRepository<Content> _repository;

        public ContentManager(IBaseRepository<Content> repository)
        {
            _repository = repository;
        }

        public async Task<ContentDetails> GetByIdAsync(int id)
        {

            //find the user with the id specified
            var score = (await _repository.AsQueryAsync()).FirstOrDefault(p => p.Id == id);

            //the user hasnt been found
            if (score == null)
            {
                return new ContentDetails
                {
                    status = new Status
                    {
                        StatusCode = Status.FAILURE_STATUS_CODE,
                        StatusDesc = $"SCORE WITH ID {id} NOT FOUND"
                    }
                };
            }

            //we can safely return him
            var details = Mapper.Map<ContentDetails>(score);

            details.status = Status.SUCCESS;

            return details;
        }

        public async Task<ICollection<ContentListItem>> List()
        {
            return Mapper.Map<List<ContentListItem>>((await _repository.AsQueryAsync()));
        }

        public async Task<ICollection<ContentListItem>> List(FilterResultsRequest filter)
        {
            List<Content> filteredContent = (await _repository.AsQueryAsync())
                                                .Where(i => (i != null && i.Title != null && !string.IsNullOrEmpty(filter.Term) && i.Title.Contains(filter.Term)))
                                                .ToList();

            var results = Mapper.Map<List<ContentListItem>>(filteredContent);

            return results;


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

        public async Task<Status> SaveAsync(ContentDetails details)
        {
            //details invalid..missing something
            if (!details.IsValid())
            {
                //return error
                return details.status;
            }

            Content savedEntity;
            Status result;

            //look for the exisitng
            var existing = (await _repository.AsQueryAsync()).FirstOrDefault(p => p.Id == details.Id);

            //no exisiting found..so we add him
            if (existing == null)
            {
                savedEntity = Mapper.Map<Content>(details);

                await _repository.AddAsync(savedEntity);

                //success
                result = Status.SUCCESS;
                return result;
            }

            //update the exisiting
            savedEntity = Mapper.Map<ContentDetails, Content>(details, existing);
            savedEntity.DateModified = DateTime.UtcNow;
            await _repository.UpdateAsync(savedEntity);

            //success
            result = Status.SUCCESS;
            return result;
        }
    }
}
