

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyCompanyName.AbpZeroTemplate.Place.Exporting;
using MyCompanyName.AbpZeroTemplate.Place.Dtos;
using MyCompanyName.AbpZeroTemplate.Dto;
using Abp.Application.Services.Dto;
using MyCompanyName.AbpZeroTemplate.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MyCompanyName.AbpZeroTemplate.Place
{
	[AbpAuthorize(AppPermissions.Pages_Administration_PbPlaces)]
    public class PbPlacesAppService : AbpZeroTemplateAppServiceBase, IPbPlacesAppService
    {
		 private readonly IRepository<PbPlace> _pbPlaceRepository;
		 private readonly IPbPlacesExcelExporter _pbPlacesExcelExporter;
		 

		  public PbPlacesAppService(IRepository<PbPlace> pbPlaceRepository, IPbPlacesExcelExporter pbPlacesExcelExporter ) 
		  {
			_pbPlaceRepository = pbPlaceRepository;
			_pbPlacesExcelExporter = pbPlacesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetPbPlaceForViewDto>> GetAll(GetAllPbPlacesInput input)
         {
			
			var filteredPbPlaces = _pbPlaceRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PlaceGroup.Contains(input.Filter) || e.PlaceName.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.PlaceGroupFilter),  e => e.PlaceGroup.ToLower() == input.PlaceGroupFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PlaceNameFilter),  e => e.PlaceName.ToLower() == input.PlaceNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var pagedAndFilteredPbPlaces = filteredPbPlaces
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pbPlaces = from o in pagedAndFilteredPbPlaces
                         select new GetPbPlaceForViewDto() {
							PbPlace = new PbPlaceDto
							{
                                PlaceGroup = o.PlaceGroup,
                                PlaceName = o.PlaceName,
                                Description = o.Description,
                                Id = o.Id
							}
						};

            var totalCount = await filteredPbPlaces.CountAsync();

            return new PagedResultDto<GetPbPlaceForViewDto>(
                totalCount,
                await pbPlaces.ToListAsync()
            );
         }
		 
		 public async Task<GetPbPlaceForViewDto> GetPbPlaceForView(int id)
         {
            var pbPlace = await _pbPlaceRepository.GetAsync(id);

            var output = new GetPbPlaceForViewDto { PbPlace = ObjectMapper.Map<PbPlaceDto>(pbPlace) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Administration_PbPlaces_Edit)]
		 public async Task<GetPbPlaceForEditOutput> GetPbPlaceForEdit(EntityDto input)
         {
            var pbPlace = await _pbPlaceRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPbPlaceForEditOutput {PbPlace = ObjectMapper.Map<CreateOrEditPbPlaceDto>(pbPlace)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPbPlaceDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbPlaces_Create)]
		 protected virtual async Task Create(CreateOrEditPbPlaceDto input)
         {
            var pbPlace = ObjectMapper.Map<PbPlace>(input);

			

            await _pbPlaceRepository.InsertAsync(pbPlace);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbPlaces_Edit)]
		 protected virtual async Task Update(CreateOrEditPbPlaceDto input)
         {
            var pbPlace = await _pbPlaceRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, pbPlace);
         }

		 [AbpAuthorize(AppPermissions.Pages_Administration_PbPlaces_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _pbPlaceRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPbPlacesToExcel(GetAllPbPlacesForExcelInput input)
         {
			
			var filteredPbPlaces = _pbPlaceRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PlaceGroup.Contains(input.Filter) || e.PlaceName.Contains(input.Filter) || e.Description.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.PlaceGroupFilter),  e => e.PlaceGroup.ToLower() == input.PlaceGroupFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PlaceNameFilter),  e => e.PlaceName.ToLower() == input.PlaceNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DescriptionFilter),  e => e.Description.ToLower() == input.DescriptionFilter.ToLower().Trim());

			var query = (from o in filteredPbPlaces
                         select new GetPbPlaceForViewDto() { 
							PbPlace = new PbPlaceDto
							{
                                PlaceGroup = o.PlaceGroup,
                                PlaceName = o.PlaceName,
                                Description = o.Description,
                                Id = o.Id
							}
						 });


            var pbPlaceListDtos = await query.ToListAsync();

            return _pbPlacesExcelExporter.ExportToFile(pbPlaceListDtos);
         }


    }
}