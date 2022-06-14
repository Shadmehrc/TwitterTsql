using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.RepositoryInterfaces;
using Application.ServiceInterfaces;
using Core.Entities;
using Core.Models;

namespace Application.Services
{
    public class TagCrudService : ITagCrudService
    {
        private readonly ITagRepository _iTagRepository;

        public TagCrudService(ITagRepository iTagRepository)
        {
            _iTagRepository = iTagRepository;
        }

        public async Task<bool> Create(string word)
        {
             var tag = new Tag() { Word = word };
             var result = await _iTagRepository.Create(tag);
            return result;
        }

        public async Task<Tag> Get(int id)
        {
            var result = await _iTagRepository.Get(id);
            return result;
        }

        public async Task<bool> Edit(EditTweetDTO model)
        {
            var result = await Task.FromResult(_iTagRepository.Edit(model).Result);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await Task.FromResult(_iTagRepository.Delete(id).Result);
            return result;
        }
    }

}
