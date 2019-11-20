using System.Collections.Generic;
using MyCompanyName.AbpZeroTemplate.SubjectEducation.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;

namespace MyCompanyName.AbpZeroTemplate.SubjectEducation.Exporting
{
    public interface IPbSubjectEducationsExcelExporter
    {
        FileDto ExportToFile(List<GetPbSubjectEducationForViewDto> pbSubjectEducations);
    }
}