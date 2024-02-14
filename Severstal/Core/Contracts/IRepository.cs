using Severstal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Severstal.Core
{
    public interface IRepository
    {
        Task<List<LoadReport>> GetTaskLoadReportAsync();
    }
}