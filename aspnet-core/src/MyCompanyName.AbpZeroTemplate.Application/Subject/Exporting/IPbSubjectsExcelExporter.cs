using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.Subject.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.Subject.Exporting
{
    public interface IPbSubjectsExcelExporter
    {
        FileDto ExportToFile(List<GetPbSubjectForViewDto> pbSubjects);
    }
}