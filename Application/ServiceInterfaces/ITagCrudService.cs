using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Models;

namespace Application.ServiceInterfaces
{
    public interface ITagCrudService
    {
        public Task<bool> Create(string word);
        public Task<Tag> Get(int id);
        public Task<bool> Edit(EditTweetDTO model);
        public Task<bool> Delete(int id);
    }
}
