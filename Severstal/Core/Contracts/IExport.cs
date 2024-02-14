using Microsoft.Office.Interop.Word;
using Severstal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Severstal.Core.Contracts
{
    public interface IExport
    {
        void ExportFileWord(List<LoadReport> dataRusultLoad);
        void InsertDataWord(Document wordDocument, List<LoadReport> dataRusultLoad);
    }
}
