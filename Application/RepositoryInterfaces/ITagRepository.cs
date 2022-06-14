using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Models;

namespace Application.RepositoryInterfaces
{
    public interface ITagRepository
    {
        public Task<bool> Create(Tag tag);
        public Task<Tag> Get(int id);
        public Task<bool> Edit(EditTweetDTO model);
        public Task<bool> Delete(int id);
    }
}
