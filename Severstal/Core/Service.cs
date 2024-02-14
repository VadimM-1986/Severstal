using Severstal.Core.Contracts;
using Severstal.Data;
using Severstal.ExportDocument;
using Severstal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Severstal.Core
{
    public class Service : IService
    {
        private readonly IRepository _repository;
        private readonly IExport _export;

        public Service(IRepository repository, IExport export)
        {
            _repository = repository;
            _export = export;
        }


        public async Task startDataProcessing()
        {
            List<LoadReport> resultLoadReport = await _repository.GetTaskLoadReportAsync();
            _export.ExportFileWord(resultLoadReport);
        }
    }
}
